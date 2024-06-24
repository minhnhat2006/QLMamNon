using System.Collections.Generic;

namespace QLMamNon.Service.SMS
{
    public interface ISMS
    {
        Dictionary<string, bool> SendSMS(Dictionary<string, string> phoneNumberToSmsContentDic);
    }
}
