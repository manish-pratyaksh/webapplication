using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Configuration;

namespace PinBoxIndiaAPIV1._0.Models.Filter
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;

                // decoding authToken we get decode value in 'Username:Password' format
                var decodeauthToken = System.Text.Encoding.ASCII.GetString(
                    Convert.FromBase64String(authToken));

                // spliting decodeauthToken using ':' 
                var arrUserNameandPassword = decodeauthToken.Split(':');

                // at 0th postion of array we get username and at 1st we get password
                if (IsAuthorizedUser(arrUserNameandPassword[0], arrUserNameandPassword[1]))
                {
                    // setting current principle
                    Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(arrUserNameandPassword[0]), null);
                }
                else
                {
                    HandleUnathorized(actionContext);
                    //actionContext.Response = actionContext.Request
                    //.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                HandleUnathorized(actionContext);
                //actionContext.Response = actionContext.Request
                // .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
        private static void HandleUnathorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate",
                    string.Format("Basic realm=\"{0}\"", "ZamaraPinbox Unauthorized"));
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
           
        }
        public static bool IsAuthorizedUser(string Username, string Password)
        {// Check if it is valid credential  
            ////if (true)//CheckUserInDB(username, password))  
            ////{
            ////    return true;
            ////}
            ////else
            ////{
            ////    return false;
            ////}      
            // In this method we can handle our database logic here...
            return Username == SecureLayer.SecureUID() && Password == SecureLayer.SecurePWD();
        }
    }
}