using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PinBoxIndiaAPIV1._0.DAL;
using PinBoxIndiaAPIV1._0.Models;
using PinBoxIndiaAPIV1._0.Models.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace PinBoxIndiaAPIV1._0.Controllers
{
    [BasicAuthentication]
    public class LoginController : ApiController
    {
        #region User Login Details
        [Route("api/Login/loginuser")]
        public HttpResponseMessage PostValidateUser(UserLogin objUserLogin)
        {
            LoginDAL userdal = new LoginDAL();
            Response objResponse = new Response();
            MultipleUserLogin objMultipleUserLogin = new MultipleUserLogin();
            try
            {
                DataSet dsResult = new DataSet();
                List<UserLoginResponse> lst = new List<UserLoginResponse>();
                string jsonData = string.Empty;
                dsResult = userdal.ValidateUser(objUserLogin);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            lst.Add(new UserLoginResponse
                            {
                                UserInfoID = dsResult.Tables[0].Rows[i]["UserInfoID"].ToString(),
                                FirstName = dsResult.Tables[0].Rows[i]["FirstName"].ToString(),
                                PRANID = dsResult.Tables[0].Rows[i]["PRANID"].ToString(),
                                MobileNo = dsResult.Tables[0].Rows[i]["MobileNo"].ToString(),
                                Age = dsResult.Tables[0].Rows[i]["Age"].ToString(),
                                CreatedDate = dsResult.Tables[0].Rows[i]["CreatedDate"].ToString(),
                                UserCategoryID = dsResult.Tables[0].Rows[i]["UserCategoryID"].ToString(),
                                RightGroup = dsResult.Tables[0].Rows[i]["RightGroup"].ToString(),
                                ProfileImage = dsResult.Tables[0].Rows[i]["ProfileImage"].ToString(),//System.Configuration.ConfigurationManager.AppSettings["ZamaraPinBoxProfilePhoto"].ToString() + dsResult.Tables[0].Rows[i]["ProfileImage"].ToString(),
                                Process = dsResult.Tables[0].Rows[i]["Process"].ToString(),
                                OtherUserInfoID = dsResult.Tables[0].Rows[i]["OtherUserInfoID"].ToString(),
                                TranDisable = dsResult.Tables[0].Rows[i]["TranDisable"].ToString(),
                                EmailAddress = dsResult.Tables[0].Rows[i]["EmailAddress"].ToString(),
                                AgencyID = dsResult.Tables[0].Rows[i]["AgencyID"].ToString(),
                                AgencyBranchID = dsResult.Tables[0].Rows[i]["AgencyBranchID"].ToString(),
                                UserCategory = dsResult.Tables[0].Rows[i]["UserCategory"].ToString(),
                            });
                        }
                        objMultipleUserLogin.userLoginResponse = lst;
                        objMultipleUserLogin.UserCount = dsResult.Tables[0].Rows.Count.ToString();
                        objResponse.Success = true;
                    }
                    if (dsResult.Tables[1].Rows.Count > 0)
                    {
                        objMultipleUserLogin.LoginBlockNo = dsResult.Tables[1].Rows[0]["LoginBlockNo"].ToString();
                        objMultipleUserLogin.userLoginResponse = lst;
                        objResponse.Success = true;
                    }
                    objResponse.Data = objMultipleUserLogin;

                    JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                    serializerSettings.Converters.Add(new DataTableConverter());
                    jsonData = JsonConvert.SerializeObject(dsResult, Newtonsoft.Json.Formatting.None, serializerSettings);
                }
                else
                {
                    objResponse.Data = objMultipleUserLogin;
                    objResponse.Success = false;
                }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: loginuser | Contoller Name: Login Controller" + Environment.NewLine;
                logMsg += "Mobile No:" + objUserLogin.MobileNo + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        [Route("api/Login/PasswordReset/{userinfoid}")]
        public HttpResponseMessage GetPasswordReset(string userinfoid)
        {
            LoginDAL userdal = new LoginDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = userdal.ResetPassword(userinfoid);
                objResponse.Data = jsonResult.ToString();
                //if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() == "1") { objResponse.Success = true; }
                //else { objResponse.Success = false; }
                objResponse.Success = true;

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PasswordReset | Contoller Name: Login Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        [Route("api/Login/changePassword")]
        public HttpResponseMessage PostChangePassword(Changepassword changePass)
        {
            LoginDAL userdal = new LoginDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = userdal.ChangePassword(changePass);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: changePassword | Contoller Name: Login Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region UserPageRight
        [Route("api/Login/UserPageRight/{MobileNo}")]
        public HttpResponseMessage GetUserPageRight(string MobileNo)
        {
            LoginDAL _objloginDAL = new LoginDAL();
            Response objResponse = new Response();
            List<UserPageRight> _objUserPageRight = new List<UserPageRight>();
            try
            {
                DataSet dsResult = new DataSet();
                string jsonData = string.Empty;
                dsResult = _objloginDAL.GetUserPageRight(MobileNo);

                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                            {
                                _objUserPageRight.Add(new UserPageRight
                                {
                                    PageID = dsResult.Tables[0].Rows[i]["PageID"].ToString(),
                                    Page = dsResult.Tables[0].Rows[i]["Page"].ToString(),
                                    URL = dsResult.Tables[0].Rows[i]["URL"].ToString(),
                                    BindInMenu = dsResult.Tables[0].Rows[i]["BindInMenu"].ToString()
                                });
                            }

                            objResponse.Data = _objUserPageRight;
                            objResponse.Success = true;
                            return Request.CreateResponse(HttpStatusCode.OK, objResponse);
                        }
                        else
                        {
                            objResponse.Success = false;
                            objResponse.Data = "-2";
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: UserPageRight | Contoller Name: Login Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        [Route("api/Login/UserProfileSelection")]
        public HttpResponseMessage PostUserProfileSelection(ProfileSelection _objProfileSelection)
        {
            LoginDAL _objLoginDAL = new LoginDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objLoginDAL.ProfileSelection(_objProfileSelection);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: UserProfileSelection | Contoller Name: Login Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region forget password
        [Route("api/Booking/Setforgotpass/")]
        public HttpResponseMessage Setforgotpass(UserRegistration userregistration)
        {
            LoginDAL objLoginDAL = new LoginDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objLoginDAL.Setforgotpass(userregistration);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: Setforgotpass | Contoller Name:  Login Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                objResponse.Data = null;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }
        #endregion
    }
}
