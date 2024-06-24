using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ACG.Core.WinForm.Util;
using QLMamNon.Properties;

namespace QLMamNon.Service.SMS
{
    public class ESms : ISMS
    {
        private const int MaxPhoneNumbersPerRequest = 50;

        public Dictionary<string, bool> SendSMS(Dictionary<string, string> phoneNumberToSmsContentDic)
        {
            // Send message to 50 phone numbers per request
            List<string> tempPhoneNumbers = new List<string>();
            int i = 0;

            foreach (var phoneNumberToSmsContent in phoneNumberToSmsContentDic)
            {
                tempPhoneNumbers.Add(phoneNumberToSmsContent.Key);

                if (i % MaxPhoneNumbersPerRequest == 0 || i == phoneNumberToSmsContentDic.Count)
                {
                    string requestUrl = string.Format(Settings.Default.SMSWSSendMessageUrl, StringUtil.Join(tempPhoneNumbers, ","), phoneNumberToSmsContent.Value);
                    sendGetRequest(requestUrl);

                    // Reset tempPhoneNumbers
                    tempPhoneNumbers = new List<string>();
                }

                i++;
            }

            return null;
        }

        private string sendGetRequest(string RequestUrl)
        {
            Uri address = new Uri(RequestUrl);
            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;

            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            try
            {
                request = WebRequest.Create(address) as HttpWebRequest;
                request.UserAgent = "QLMamNon";
                request.KeepAlive = false;
                request.Timeout = 15 * 1000;
                response = request.GetResponse() as HttpWebResponse;

                if (request.HaveResponse == true && response != null)
                {
                    reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    result = result.Replace("</string>", "");
                    Console.WriteLine("The SMS was sent with returned message [{0}]", result);
                    return null;
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                        string errorMsg = String.Format("The server returned '{0}' with the status code {1} ({2:d}).",
                            errorResponse.StatusDescription, errorResponse.StatusCode,
                            errorResponse.StatusCode);
                        Console.WriteLine(errorMsg);
                        throw new ApplicationException(errorMsg);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }

            return null;
        }
    }
}
