using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;

namespace JaysSakti
{
    static public class BLSMS
    {
        //static public void SendSMS(string Mobile, string Message)
        //{
        //    try
        //    {
        //        string SMSAPI = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=SabkaMart[AND]password=aapka123[AND]sender=APKBIZ[AND]to=[MOBILE][AND]message=[MESSAGE][AND]reqid=1[AND]format={json}[AND]route_id=39[AND]callback=#[AND]unique=0[AND]sendondate=" + DateTime.Now.ToString();

        //        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
        //        HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        public static string SendSMSNew(string mobile, string msg)
        {
            try
            {
                string baseurl = "https://www.hellotext.live/vb/apikey.php?apikey=RTsDI5gnm1Zw80Wp&senderid=SKPLES&number="+mobile+"&message="+msg+"";
                //string baseurl = "https://api.authkey.io/request?authkey=1f3a7c058e998305&mobile=" + mobile + "&country_code=91&sms=" + msg + "&sender=MYADDS&pe_id=1201160578232705664&template_id=1207161735839113865";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(baseurl, false));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        static public string Registration(string MemberName, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["REGISTRATION"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }

        static public string OTP(string MemberName, string OTP)
        {
            string Message = ConfigurationSettings.AppSettings["OTP"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[OTP]", OTP);

            return Message;
        }

    }
}
