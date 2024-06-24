using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using QLMamNon.Constant;
using QLMamNon.Facade;
using SMSPDULib;

namespace QLMamNon.Service.SMS
{
    public class USB3GSms : ISMS
    {
        private SerialPort serialPort;

        private int timeInsecondsToSleepBetweenSms;

        public USB3GSms(SerialPort serialPort, int timeInsecondsToSleepBetweenSms)
        {
            this.serialPort = serialPort;
            this.timeInsecondsToSleepBetweenSms = timeInsecondsToSleepBetweenSms;
            serialPort.ErrorReceived += SerialPort_ErrorReceived;
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new Exception($"Error occurs on SerialPort {serialPort.PortName}");
        }

        public Dictionary<string, bool> SendSMS(Dictionary<string, string> phoneNumberToSmsContentDic)
        {
            Dictionary<string, bool> sendingStatusDic = new Dictionary<string, bool>();

            foreach (var phoneNumberToSmsContent in phoneNumberToSmsContentDic)
            {
                string phoneNumberKey = phoneNumberToSmsContent.Key;

                if (!string.IsNullOrWhiteSpace(phoneNumberKey))
                {
                    string phoneNumber = phoneNumberKey.Substring(0, phoneNumberKey.IndexOf("-"));
                    bool sendingSuccess = sendSmsMessage(phoneNumber, phoneNumberToSmsContent.Value);
                    sendingStatusDic.Add(phoneNumberToSmsContent.Key, sendingSuccess);

                    Thread.Sleep(timeInsecondsToSleepBetweenSms * 1000);
                }
            }

            return sendingStatusDic;
        }

        /// <summary>
        /// Compose SMS message parts
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <param name="isUnicode"></param>
        /// <returns></returns>
        /// <see cref="http://www.codeproblem.com/articles/languages/81-net-framework/76-encoding-sms-in-pdu-format-in-net?showall=1&limitstart="/>
        /// <see cref="https://developer.gemalto.com/threads/how-send-long-sms"/>
        private string[] composePduSms(string phoneNumber, string message, bool isUnicode)
        {
            //Compose PDU SMS
            SMSPDULib.SMS sms = new SMSPDULib.SMS();
            //Setting direction of sms
            sms.Direction = SMSDirection.Submited;
            //Sets the flash property of SMS
            sms.Flash = false;
            //Set the recipient number
            sms.PhoneNumber = phoneNumber;

            //Sets the Message encoding for this SMS
            if (isUnicode)
            {
                sms.MessageEncoding = SMSPDULib.SMS.SMSEncoding.UCS2;
            }
            else
            {
                sms.MessageEncoding = SMSPDULib.SMS.SMSEncoding._7bit;
            }

            //Set the SMS Message
            sms.Message = message;

            //now check for multipart sms
            int messageMaxLength = isUnicode ? 70 : 160;
            string[] messagesParts;

            if (sms.Message.Length > messageMaxLength)
            {
                messagesParts = sms.ComposeLongSMS();
            }
            else
            {
                messagesParts = new string[] { sms.Compose() };
            }

            return messagesParts;
        }

        private bool sendSmsMessage(string phoneNumber, string message)
        {
            bool isUnicode = containsUnicodeCharacter(message);
            //string encodedPhoneNumber = phoneNumberToSemiOctets(phoneNumber);
            //string encodedMessage = unicodeStr2HexStr(message).ToLower();
            //string messageLengthHex = isUnicode ? (message.Length * 2).ToString("x2") : message.Length.ToString("x2");
            //string messageToSend = string.Format(Settings.Default.PDUSendMessageTemplate, phoneNumber.Length.ToString("x2"), encodedPhoneNumber, messageLengthHex, encodedMessage);
            string[] messagesParts = composePduSms(phoneNumber, message, isUnicode);

            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

                serialPort.Write("AT" + Environment.NewLine);
                Thread.Sleep(2000);
                string statusMsg = serialPort.ReadExisting();
                if (!isStatusOk(statusMsg))
                {
                    return false;
                }

                // Set mode to PDU
                serialPort.DiscardInBuffer();
                serialPort.Write("AT+CMGF=0" + Environment.NewLine);
                Thread.Sleep(2000);
                statusMsg = serialPort.ReadExisting();
                if (!isStatusOk(statusMsg))
                {
                    return false;
                }

                // Set encoding to UCS2
                serialPort.DiscardInBuffer();
                string cscsValue = isUnicode ? "UCS2" : "IRA";
                serialPort.Write("AT+CSCS=" + (char)34 + cscsValue + (char)34 + Environment.NewLine);
                Thread.Sleep(2000);
                statusMsg = serialPort.ReadExisting();
                if (!isStatusOk(statusMsg))
                {
                    return false;
                }

                foreach (var messagePart in messagesParts)
                {
                    string cmgsValue = (Math.Ceiling((double)messagePart.Length / 2) - 1).ToString();

                    // Send message
                    serialPort.DiscardInBuffer();
                    serialPort.Write("AT+CMGS=" + cmgsValue + Environment.NewLine);

                    serialPort.Write(messagePart + (char)26);
                    Thread.Sleep(5000);
                }

                statusMsg = serialPort.ReadExisting();
                serialPort.DiscardInBuffer();
                if (!isStatusOk(statusMsg))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                string errorMsg = $"{ex.Message}\n{ex.StackTrace}";
                LoggerFacade.Error(errorMsg, AppForms.FormTinNhanPhuHuynh);
            }

            return false;
        }

        private bool isStatusOk(string statusMsg)
        {
            if (string.IsNullOrWhiteSpace(statusMsg))
            {
                return false;
            }

            if ("\r\n> \r\n" == statusMsg)
            {
                return true;
            }

            if (statusMsg.IndexOf("OK", StringComparison.CurrentCultureIgnoreCase) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <see cref="https://stackoverflow.com/questions/3438340/convert-to-ucs2/25155746"/>
        private string unicodeStr2HexStr(String strMessage)
        {
            byte[] ba = Encoding.BigEndianUnicode.GetBytes(strMessage);
            String strHex = BitConverter.ToString(ba);
            strHex = strHex.Replace("-", "");
            return strHex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        /// <see cref="https://www.gsmfavorites.com/documents/sms/pdutext/"/>
        private string phoneNumberToSemiOctets(string phoneNumber)
        {
            string semiOctetsValue = "";

            if (phoneNumber.Length % 2 == 1)
            {
                phoneNumber = $"{phoneNumber}F";
            }

            for (int i = 0; i < phoneNumber.Length; i += 2)
            {
                semiOctetsValue += $"{phoneNumber[i + 1]}{phoneNumber[i]}";
            }

            return semiOctetsValue;
        }

        private bool containsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;
            return input.Any(c => c > MaxAnsiCode);
        }
    }
}