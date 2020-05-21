using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models.Filter
{
  public  static class SecureLayer
    {
        public static string SecureKey { get; set; }
        public static string SKeyUID { get; set; }
        public  static string SKeyPWD { get; set; }
      
         public static string SecureUID()
        {
            SecureKey= "UGluYm94V2ViOlAhbkA5MTJCb3g=";
           var decodeToken = System.Text.Encoding.ASCII.GetString(
            Convert.FromBase64String(SecureKey));
           // spliting decodeauthToken using ':' 
            var arrUserNPWD = decodeToken.Split(':');
            SKeyUID = arrUserNPWD[0];
            return SKeyUID ;
        }
         public static string SecurePWD()
         {   SecureKey = "UGluYm94V2ViOlAhbkA5MTJCb3g=";
             var decodeToken = System.Text.Encoding.ASCII.GetString(
              Convert.FromBase64String(SecureKey));
             // spliting decodeauthToken using ':' 
             var arrUserNPWD = decodeToken.Split(':');
            SKeyPWD = arrUserNPWD[1];
             return SKeyPWD;
         }
    }
}