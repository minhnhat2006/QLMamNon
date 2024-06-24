using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using QLMamNon.Properties;
using Newtonsoft.Json;

namespace QLMamNon.Service.SMS
{
    public class SpeedSMS : ISMS
    {
        public const int TYPE_QC = 1;
        public const int TYPE_CSKH = 2;
        public const int TYPE_BRANDNAME = 3;
        public const int TYPE_BRANDNAME_NOTIFY = 4; // Gửi sms sử dụng brandname Notify
        public const int TYPE_GATEWAY = 5; // Gửi sms sử dụng app android từ số di động cá nhân, download app tại đây: https://play.google.com/store/apps/details?id=com.speedsms.gateway

        public Dictionary<string, bool> SendSMS(Dictionary<string, string> phoneNumberToSmsContentDic)
        {
            //string response = this.sendSMS(phoneNumbers.ToArray(), content, Settings.Default.SpeedSMSType, Settings.Default.SpeedSMSSender);
            return null;
        }

        public String getUserInfo()
        {
            String url = Settings.Default.SpeedSMSRootUrl + "/user/info";
            NetworkCredential myCreds = new NetworkCredential(Settings.Default.SpeedSMSAccessToken, ":x");
            WebClient client = new WebClient();
            client.Credentials = myCreds;
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            return reader.ReadToEnd();
        }

        private String sendSMS(String[] phones, String content, int type, String sender)
        {
            String url = Settings.Default.SpeedSMSRootUrl + "/sms/send";
            if (phones.Length <= 0)
                return "";
            if (content.Equals(""))
                return "";
            if (type < TYPE_QC || type > TYPE_GATEWAY)
                return "";
            if (type == TYPE_BRANDNAME && sender.Equals(""))
                return "";
            if (!sender.Equals("") && sender.Length > 11)
                return "";

            content = JsonConvert.SerializeObject(content, new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii });
            content = content.Trim().Substring(1, content.Length - 2).Trim();

            NetworkCredential myCreds = new NetworkCredential(Settings.Default.SpeedSMSAccessToken, ":x");
            WebClient client = new WebClient();
            client.Credentials = myCreds;
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            string builder = "{\"to\":[";

            for (int i = 0; i < phones.Length; i++)
            {
                builder += "\"" + phones[i] + "\"";
                if (i < phones.Length - 1)
                {
                    builder += ",";
                }
            }
            builder += "], \"content\": \"" + content + "\", \"type\":" + type + ", \"sender\": \"" + sender + "\"}";

            String json = builder.ToString();
            return client.UploadString(url, json);
        }
    }
}
