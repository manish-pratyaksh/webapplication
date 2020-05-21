using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PinBoxIndiaAPIV1._0.DAL;
using PinBoxIndiaAPIV1._0.Models;
using PinBoxIndiaAPIV1._0.Models.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    public class AdminController : ApiController
    {
        #region Rights Group
        [Route("api/admin/GetRightsGroupDDL")]
        public HttpResponseMessage PostGetCountyDDL(RightsGroupDDL RightsGroupDDLA)
        {
            AdminDAL _objadminDAL = new AdminDAL();
            Response objResponse = new Response();
            RightsGroupMaster _objRightsGroupMaster = new RightsGroupMaster();
            List<RightsGroupDDL> _objRightsGroupDDL = new List<RightsGroupDDL>();
            //string logMsg = string.Empty;
            try
            {
                DataSet dsResult = new DataSet();
                string jsonData = string.Empty;
                dsResult = _objadminDAL.RightsGroupDDL(RightsGroupDDLA);

                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                            {
                                _objRightsGroupDDL.Add(new RightsGroupDDL
                                {
                                    PRGID = dsResult.Tables[0].Rows[i]["PRGID"].ToString(),
                                    RightGroupName = dsResult.Tables[0].Rows[i]["RightGroupName"].ToString()
                                });
                            }

                            _objRightsGroupMaster.RightsGroupDDL = _objRightsGroupDDL;

                            objResponse.Data = _objRightsGroupMaster;
                            objResponse.Success = true;
                            return Request.CreateResponse(HttpStatusCode.OK, objResponse);
                        }
                        else
                        {
                            _objRightsGroupMaster.RightsGroupDDL = _objRightsGroupDDL;

                            objResponse.Data = _objRightsGroupMaster;
                            objResponse.Success = true;
                            //objResponse.Success = false;
                            //objResponse.Data = "-2";
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetRightsGroupDDL | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "UserCategoryID: " + RightsGroupDDLA.UserCategoryID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response

                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }

        [Route("api/admin/GetRightGroup/{UserCategoryID}/{RightGroupID}")]
        public HttpResponseMessage GetRightGroup(string UserCategoryID, string RightGroupID)
        {
            AdminDAL _objDAL = new AdminDAL();
            Response _objResponse = new Response();
            string jsonData = string.Empty;
            DataSet dsResult = new DataSet();
            RightGroupAdmin _objRightGP = new RightGroupAdmin();
            try
            {
                dsResult = _objDAL.GETRightGroup(UserCategoryID, RightGroupID);
                List<RightGPMasterModue> RGMasterModule = new List<RightGPMasterModue>();
                List<RightGPPageDetails> RGPageDetails = new List<RightGPPageDetails>();
                List<ModulewiseAction> RGModulewiseAction = new List<ModulewiseAction>();
                List<PageActionDetails> RGPageActionDetails = new List<PageActionDetails>();
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            RGMasterModule.Add(new RightGPMasterModue
                            {
                                PageID = dsResult.Tables[0].Rows[i]["PageID"].ToString(),
                                Page = dsResult.Tables[0].Rows[i]["Page"].ToString(),
                                URL = dsResult.Tables[0].Rows[i]["Page"].ToString()
                            });
                        }
                        _objRightGP.RGMasterModule = RGMasterModule;
                    }
                    else
                    {
                        _objRightGP.RGMasterModule = null;
                    }

                    if (dsResult.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[1].Rows.Count; i++)
                        {
                            RGModulewiseAction.Add(new ModulewiseAction
                            {
                                ActionID = dsResult.Tables[1].Rows[i]["ActionID"].ToString(),
                                ParentID = dsResult.Tables[1].Rows[i]["ParentPageID"].ToString(),
                                Action = dsResult.Tables[1].Rows[i]["Action"].ToString()
                            });
                        }
                        _objRightGP.ModulewiseAction = RGModulewiseAction;
                    }
                    else
                    {
                        _objRightGP.ModulewiseAction = null;
                    }
                    if (dsResult.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[2].Rows.Count; i++)
                        {
                            RGPageDetails.Add(new RightGPPageDetails
                            {
                                PageID = dsResult.Tables[2].Rows[i]["PageID"].ToString(),
                                ParentPageID = dsResult.Tables[2].Rows[i]["ParentPageID"].ToString(),
                                Page = dsResult.Tables[2].Rows[i]["Page"].ToString(),
                                PageURL = dsResult.Tables[2].Rows[i]["URL"].ToString()
                            });
                        }
                        _objRightGP.RightGPPageDetails = RGPageDetails;
                    }
                    else
                    {
                        _objRightGP.RightGPPageDetails = null;
                    }
                    if (dsResult.Tables[3].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[3].Rows.Count; i++)
                        {
                            RGPageActionDetails.Add(new PageActionDetails
                            {
                                PageActionID = dsResult.Tables[3].Rows[i]["PageActionID"].ToString(),
                                PageID = dsResult.Tables[3].Rows[i]["PageID"].ToString(),
                                ActionID = dsResult.Tables[3].Rows[i]["ActionID"].ToString(),
                                ActionName = dsResult.Tables[3].Rows[i]["Action"].ToString(),
                                ParentPageID = dsResult.Tables[3].Rows[i]["ParentPageID"].ToString(),
                                AvlAction = dsResult.Tables[3].Rows[i]["AvlAction"].ToString(),
                                RightAvl = dsResult.Tables[3].Rows[i]["Right"].ToString()
                            });
                        }
                        _objRightGP.PageActionDetails = RGPageActionDetails;
                    }
                    else
                    {
                        _objRightGP.PageActionDetails = null;
                    }
                    _objResponse.Data = _objRightGP;
                }
                else
                {
                    _objResponse.Data = null;
                }
                _objResponse.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, _objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetRightGroup | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "UserCategoryID:" + UserCategoryID + "RightGroupID:" + RightGroupID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                _objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, _objResponse);
            }
        }



        [Route("api/admin/SetRightGroup")]
        public HttpResponseMessage PostSetRightGroup(RightGroups _objRightGp)
        {

            AdminDAL objAdminDal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objAdminDal.SetRightGroups(_objRightGp);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetRightGroup | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "UserCategoryID:" + _objRightGp.UserCategory + "RightGroupID:" + _objRightGp.RightGroupID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }


        [Route("api/home/GetRightGroupMenu/{UserCategoryID}/{RightGroupID}")]
        public HttpResponseMessage GetRightGroupMenu(string UserCategoryID, string RightGroupID)
        {
            AdminDAL objUserDAL = new AdminDAL();
            Response objResponse = new Response();
            string JsonData = string.Empty;
            DataSet dsResult = new DataSet();
            try
            {
                RightGroupMenu objRightGPMenu = new RightGroupMenu();
                List<GetMainPage> objMainPage = new List<GetMainPage>();
                List<GetSubpage> objsubPage = new List<GetSubpage>();
                dsResult = objUserDAL.GETRightGroupLeftMenu(UserCategoryID, RightGroupID);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            objMainPage.Add(new GetMainPage
                            {
                                PageId = dsResult.Tables[0].Rows[i]["PageID"].ToString().Trim(),
                                PageName = dsResult.Tables[0].Rows[i]["Page"].ToString().Trim(),
                                URL = dsResult.Tables[0].Rows[i]["URL"].ToString().Trim(),
                                ChildCount = dsResult.Tables[0].Rows[i]["ChildCount"].ToString().Trim(),
                                lnkID = dsResult.Tables[0].Rows[i]["STATICURL"].ToString().Trim(),
                                IconCSS = dsResult.Tables[0].Rows[i]["IconCSS"].ToString().Trim()
                            });
                        }
                        objRightGPMenu.mainPage = objMainPage;
                    }
                    else
                    {
                        objRightGPMenu.mainPage = null;
                    }
                    if (dsResult.Tables[1].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsResult.Tables[1].Rows.Count; j++)
                        {

                            objsubPage.Add(new GetSubpage
                            {
                                PageId = dsResult.Tables[1].Rows[j]["PageID"].ToString().Trim(),
                                PageName = dsResult.Tables[1].Rows[j]["Page"].ToString().Trim(),
                                URL = dsResult.Tables[1].Rows[j]["URL"].ToString().Trim(),
                                ParentPageID = dsResult.Tables[1].Rows[j]["ParentPageID"].ToString().Trim(),
                                lnkID = dsResult.Tables[1].Rows[j]["STATICURL"].ToString().Trim()
                            });
                        }

                        objRightGPMenu.subPage = objsubPage;
                    }
                    else
                    {
                        objRightGPMenu.subPage = null;
                    }

                    objResponse.Data = objRightGPMenu;
                }
                else
                {
                    objResponse.Data = null;

                }
                objResponse.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);

            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetRightGroupMenu | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "UserCategoryID:" + UserCategoryID + "|" + "RightGroupID:" + RightGroupID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region User Management
        [Route("api/admin/ManagementList")]
        public HttpResponseMessage PostManagementList(ManageListFilter objManageListFilter)
        {
            AdminDAL objadminDAL = new AdminDAL();
            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            Management objManagement = new Management();
            List<Managementuser> objManagementuserList = new List<Managementuser>();
            try
            {
                dsResult = objadminDAL.GetManagementList(objManageListFilter);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0 && dsResult.Tables[1].Rows.Count > 0)
                    {
                        objManagement.TotalCount = dsResult.Tables[1].Rows[0]["TotalCount"].ToString();
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            objManagementuserList.Add(new Managementuser
                            {
                                UserInfoID = dsResult.Tables[0].Rows[i]["UserInfoID"].ToString(),
                                IncID = common.EncodeTo64(dsResult.Tables[0].Rows[i]["UserInfoID"].ToString()),
                                AdminUser = dsResult.Tables[0].Rows[i]["FullName"].ToString(),
                                Mobile = dsResult.Tables[0].Rows[i]["MobileNo"].ToString(),
                                Email = dsResult.Tables[0].Rows[i]["EmailAddress"].ToString(),
                                DELETED = dsResult.Tables[0].Rows[i]["DELETED"].ToString()
                            });
                        }
                        objManagement.Managementuserlist = objManagementuserList;
                    }
                    else if (dsResult.Tables[0].Rows.Count == 0 || dsResult.Tables[1].Rows.Count == 0)
                    {
                        objResponse.Data = null;
                    }
                }
                else
                {
                    objResponse.Data = null;
                }
                objResponse.Data = objManagement;
                objResponse.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: ManagementList | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                objResponse.Data = null;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }

 
        [Route("api/admin/GetUserManagementByUserId/{UserInfoID}")]
        public HttpResponseMessage GetUserManagementByUserId(string UserInfoID)
        {
            AdminDAL admindal = new AdminDAL();
            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            UserAdminManage _objUserAdminManage = new UserAdminManage();

            string jsonData = string.Empty;
            try
            {
                dsResult = admindal.GetUsereditdetails(UserInfoID);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        var ProfilePhoto = "";
                        //if (dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() != "" && dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() != null)
                        //{
                        //    ProfilePhoto = System.Configuration.ConfigurationManager.AppSettings["ZamaraVunaProfilePhoto"].ToString() + dsResult.Tables[0].Rows[0]["ProfileImage"].ToString();

                        //}
                        _objUserAdminManage.UserInfoID = dsResult.Tables[0].Rows[0]["UserInfoID"].ToString();
                        _objUserAdminManage.FirstName = dsResult.Tables[0].Rows[0]["FirstName"].ToString();
                        _objUserAdminManage.LastName = dsResult.Tables[0].Rows[0]["LastName"].ToString();
                        _objUserAdminManage.MobileNo = dsResult.Tables[0].Rows[00]["MobileNo"].ToString();
                        _objUserAdminManage.Email = dsResult.Tables[0].Rows[0]["EmailAddress"].ToString();
                        _objUserAdminManage.RightGroup = dsResult.Tables[0].Rows[0]["RightGroup"].ToString();
                        _objUserAdminManage.EmployeeCode = dsResult.Tables[0].Rows[0]["EmployeeCode"].ToString();
                        
                       
                        //_objUserAdminManage.ProfileImage = dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() == "" ? "NoImage.jpg" : dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() == null ? "NoImage.jpg" : ProfilePhoto;

                       //EmailAddress _objUserAdminManage.ImageName = dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() == "" ? "NoImage.jpg" : dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() == null ? "NoImage.jpg" : dsResult.Tables[0].Rows[0]["ProfileImage"].ToString();
                    }
                }
                objResponse.Success = true;
                objResponse.Data = _objUserAdminManage;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetUserManagementByUserId | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "UserInfoID:" + UserInfoID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }

        [Route("api/admin/SetProgramAdminActiveInactive/")]
        public HttpResponseMessage PostSetProgramAdminActiveInactive(ActiveInactiveAgencySearch activeInactiveAgencySearch)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.ProgramAdminActiveInactive(activeInactiveAgencySearch);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetProgramAdminActiveInactive | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "UserInfoID:" + activeInactiveAgencySearch.UserInfoID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }

        [Route("api/admin/UserManagementEdit")]
        public HttpResponseMessage UserManagementEdit(MemberRegistration _objMemberRegistrationA)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {

                if (_objMemberRegistrationA.UserPhoto == "" && _objMemberRegistrationA.UserImageName != "")
                {
                    string attachment = "../../Content/img/ProfilePhoto/";
                    DateTime centuryBegin = new DateTime(2001, 1, 1);
                    DateTime currentDate = DateTime.Now;
                    long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
                    string imagename = SaveImage(attachment, elapsedTicks.ToString(), _objMemberRegistrationA.UserImageName);
                    _objMemberRegistrationA.UserImageName = imagename;

                }
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.UpdaterManagementDetails(_objMemberRegistrationA);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: UserManagementEdit | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "UserInfoID:" + _objMemberRegistrationA.UserInfoID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }


        public static string ImageToBase64(string _imagePath)
        {
            string _base64String = null;
            try
            {

                using (System.Drawing.Image _image = System.Drawing.Image.FromFile(_imagePath))
                {
                    using (MemoryStream _mStream = new MemoryStream())
                    {
                        _image.Save(_mStream, _image.RawFormat);
                        byte[] _imageBytes = _mStream.ToArray();
                        _base64String = Convert.ToBase64String(_imageBytes);
                    }
                }
            }
            catch (Exception ex)
            {

                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: ImageToBase64 | Contoller Name: Admin Controller " + Environment.NewLine;
                logMsg += DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);

            }
            return _base64String;
        }


        public string SaveImage(string path, string usercode, string base64)
        {

            string spath = HttpContext.Current.Server.MapPath(path + usercode + ".jpg");
            int width = 131;
            int height = 131;
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                using (Bitmap bm2 = new Bitmap(ms))
                {
                    bm2.Save(spath);
                    bm2.Dispose();
                }
                return usercode + ".jpg";
            }
        }


        [Route("api/admin/SetUserManagementCreate")]
        public HttpResponseMessage PostSetUserManagementCreate(MemberRegistration _objMemberRegstA) 
        {
            AdminDAL _objadminDAL = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objadminDAL.UserManagementCreateDal(_objMemberRegstA);

                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-2000")
                {
                    objResponse.Success = true;
                }
                else
                {
                    objResponse.Success = false;
                }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetUserManagementCreate | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "Temp Reg ID:" + _objMemberRegstA.TempRegID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }


        #endregion
        #region ReminderMatrix
        [Route("api/admin/getReminderProductList")]
        public HttpResponseMessage GetReminderProductList()
        {
            AdminDAL masterdal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                ExcalationHelpGroup objExHelp = new ExcalationHelpGroup();
                List<HelpProduct> ProductList = new List<HelpProduct>();
                DataSet objDataset = masterdal.GeProductList();
                for (int i = 0; i < objDataset.Tables[0].Rows.Count; i++)
                {
                    ProductList.Add(new HelpProduct
                    {
                        id = Convert.ToString(objDataset.Tables[0].Rows[i]["id"]),
                        Product = Convert.ToString(objDataset.Tables[0].Rows[i]["Product"])
                    });
                }
                objExHelp.HelpProductList = ProductList;
                objResponse.Data = objExHelp;
                objResponse.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: getReminderProductList | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }

        [Route("api/admin/PostReminderMatrix/")]
        public HttpResponseMessage PostReminderMatrixAdd(ReminderMatrix reminderMatrix)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.SetReminderMatrixDal(reminderMatrix);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-2000") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PostReminderMatrix | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "ReminderMatrix ID:" + reminderMatrix.RMID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }

        [Route("api/admin/getReminderMatrix/{RMID}/{PageSize}/{PageNo}")]
        public HttpResponseMessage getReminderMatrix(string RMID, string PageSize, string PageNo)
        {
            AdminDAL admindal = new AdminDAL();
            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            ReminderMatrix objReminderMatrix = new ReminderMatrix();
            List<ReminderMatrixList> _objReminderMatrixList = new List<ReminderMatrixList>();
            string jsonData = string.Empty;
            try
            {
                dsResult = admindal.GetReminderMatrixDal(RMID, PageSize, PageNo);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            _objReminderMatrixList.Add(new ReminderMatrixList
                            {
                                RMID = dsResult.Tables[0].Rows[i]["RMID"].ToString(),
                                ProductType = dsResult.Tables[0].Rows[i]["ProductType"].ToString(),
                                BeforeDueDate = dsResult.Tables[0].Rows[i]["BeforeDueDate"].ToString(),
                                ReminderText = dsResult.Tables[0].Rows[i]["ReminderText"].ToString(),
                                TemplateName = dsResult.Tables[0].Rows[i]["TemplateName"].ToString(),
                                Days = dsResult.Tables[0].Rows[i]["Days"].ToString(),
                                ProductTypeID = dsResult.Tables[0].Rows[i]["ProductTypeID"].ToString(),


                            });
                        }
                        objReminderMatrix.objReminderMatrixList = _objReminderMatrixList;

                        if (dsResult.Tables[1].Rows.Count > 0 && dsResult.Tables[1].Rows.Count > 0)
                        {
                            objReminderMatrix.TotalCount = dsResult.Tables[1].Rows[0]["TotalCount"].ToString();
                        }

                    }
                }
                objResponse.Success = true;
                objResponse.Data = objReminderMatrix;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: getReminderMatrix | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "ReminderMatrix ID:" + RMID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }

        [Route("api/admin/PostDelReminderMatrix/")]
        public HttpResponseMessage PostDelReminderMatrix(ReminderMatrix objReminderMatrix)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.DelReminderMatrixDal(objReminderMatrix.RMID);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-2000") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {

                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PostDelReminderMatrix | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "ReminderMatrix ID:" + objReminderMatrix.RMID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }
        #endregion
        #region Agency Step2
        [Route("api/admin/agencyregstep2/")]
        public HttpResponseMessage SetAgencyBankAccount(AgencyBankAccount agentBankInfo)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.SetAgencyBankAccountStep2(agentBankInfo);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-2000") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: agencyregstep2 | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "TmpAgencyID: " + agentBankInfo.TmpAgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }
        [Route("api/admin/GetTempAgencyBankAccountDtl")]
        public HttpResponseMessage PostGetTempAgencyBankAccountStep2(AgencyBankAccount _objAgencyBankAccount)
        {
            AdminDAL _objadminDAL = new AdminDAL();
            Response objResponse = new Response();
            AgencyBankAccount _objAgencyBankAccountData = new AgencyBankAccount();
            string jsonData = string.Empty;
            DataSet dsResult = new DataSet();
            try
            {
                dsResult = _objadminDAL.GetTempAgencyBankAccountDal(_objAgencyBankAccount.TmpAgencyID);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            _objAgencyBankAccountData.TmpAgencyID = dsResult.Tables[0].Rows[0]["AgencyID"].ToString();
                            _objAgencyBankAccountData.AccountName = dsResult.Tables[0].Rows[0]["AccountName"].ToString();
                            _objAgencyBankAccountData.AccountNo = dsResult.Tables[0].Rows[0]["AccountNo"].ToString(); 
                            _objAgencyBankAccountData.IFSCCode = dsResult.Tables[0].Rows[0]["IFSCCode"].ToString();
                            objResponse.Success = true;
                        }
                        else if (dsResult.Tables[0].Rows.Count == 0)
                        {
                            objResponse.Success = false;
                        }


                    }
                }
                objResponse.Data = _objAgencyBankAccountData;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);

            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetTempAgencyBankAccountDtl | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "TempAgencyID:" + _objAgencyBankAccountData.TmpAgencyID + "" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region Agency Registration
        [Route("api/admin/agencyregstep1/")]
        public HttpResponseMessage Postupdateagencydetails(AgencyRegistraction agentInfo)
        {
            AdminDAL objAdminDAL = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                if (agentInfo.ProfileImage != "")
                {
                    if (agentInfo.ImageName == "")
                    {
                        string attachment = "../../Content/img/ProfilePhoto/";
                        DateTime centuryBegin = new DateTime(2001, 1, 1);
                        DateTime currentDate = DateTime.Now;
                        long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
                        string imagename = SaveImage(attachment, elapsedTicks.ToString(), agentInfo.ProfileImage);
                        agentInfo.ProfileImage = imagename;

                    }
                }
                jsonResult = objAdminDAL.SetAgentRegistrationStep1(agentInfo);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-2000") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: agencyregstep1 | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "AgencyId: " + agentInfo.AgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response

                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }
        #endregion
        #region AgencyRegistractionstep4
        [Route("api/admin/GetAgencyServiceChargesStep4")]
        public HttpResponseMessage GetAgencyServiceChargesStep4()
        {
            AdminDAL userdal = new AdminDAL();

            AgencyServiceChargesContainer _objAgencyServiceChargesContainer = new AgencyServiceChargesContainer();
            List<AgencyServiceCharges> _objAgencyServiceChargesList = new List<AgencyServiceCharges>();

            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            string jsonData = string.Empty;
            try
            {
                dsResult = userdal.GetAgencyServiceChargesStep4();
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            _objAgencyServiceChargesList.Add(new AgencyServiceCharges
                            {
                                SAChkID = (i + 1).ToString(),
                                Task = dsResult.Tables[0].Rows[i]["Task"].ToString(),
                                TaskID = dsResult.Tables[0].Rows[i]["TaskID"].ToString(),
                                TaskParentID = dsResult.Tables[0].Rows[i]["TaskParentID"].ToString(),
                                TaskOrder = dsResult.Tables[0].Rows[i]["TaskOrder"].ToString(),
                                VarValue = dsResult.Tables[0].Rows[i]["VarValue"].ToString(),
                                variableOption = dsResult.Tables[0].Rows[i]["variableOption"].ToString(),
                            });
                        }

                        _objAgencyServiceChargesContainer.AgencyServiceChargesList = _objAgencyServiceChargesList;
                    }
                }
                objResponse.Success = true;
                objResponse.Data = _objAgencyServiceChargesContainer;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetAgencyServiceChargesStep4 | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }

        [Route("api/admin/SetAgencyServiceChargesStep4/")]
        public HttpResponseMessage AgencyServiceChargesStep4(AgencyWithAdmin agencyAdminInfo)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.SetAgencyServiceChargesStep4Dal(agencyAdminInfo);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-2000") { objResponse.Success = true; }
                else { objResponse.Success = false; }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetAgencyServiceChargesStep4 | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "TmpAgencyID: " + agencyAdminInfo.TmpAgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region Agency Registration step3

        [Route("api/admin/agencyregstep3/")]
        public HttpResponseMessage SetAgencyAdminInfoStep3(AgencyAdminInfo agencyAdminInfo)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.SetAgencyAdminInfoStep3DAL(agencyAdminInfo);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-2000") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: agencyregstep3 | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "TmpAgencyID:" + agencyAdminInfo.TmpAgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }

        #endregion
        #region Agency reg step 1 get 
        [Route("api/admin/GetTempAgencyRegstep1")]
        public HttpResponseMessage PostGetTempAgencyBankAccountStep2(AgencyRegistraction agencyRegistraction)
        {
            AdminDAL _objadminDAL = new AdminDAL();
            Response objResponse = new Response();
            AgencyRegistraction _objAgencyRegistraction = new AgencyRegistraction();
            string jsonData = string.Empty;
            DataSet dsResult = new DataSet();
            try
            {
                dsResult = _objadminDAL.GetTempAgencyRegstep1Dal(agencyRegistraction.AgencyID);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {

                            var ProfilePhoto = "";
                            if (dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() != "" && dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() != null)
                            {
                                ProfilePhoto = System.Configuration.ConfigurationManager.AppSettings["PinBoxIndiaProfilePhoto"].ToString() + dsResult.Tables[0].Rows[0]["ProfileImage"].ToString();

                            }

 
                            _objAgencyRegistraction.AgencyID = dsResult.Tables[0].Rows[0]["AgencyID"].ToString();
                            _objAgencyRegistraction.AgencyTypeID = dsResult.Tables[0].Rows[0]["AgencyTypeID"].ToString();
                            _objAgencyRegistraction.AgencyName = dsResult.Tables[0].Rows[0]["AgencyName"].ToString();
                            _objAgencyRegistraction.Address1 = dsResult.Tables[0].Rows[0]["Address1"].ToString();
                            _objAgencyRegistraction.Address2 = dsResult.Tables[0].Rows[0]["Address2"].ToString();
                            _objAgencyRegistraction.stateID = dsResult.Tables[0].Rows[0]["StateID"].ToString();
                            _objAgencyRegistraction.CityID = dsResult.Tables[0].Rows[0]["CityID"].ToString();
                            _objAgencyRegistraction.Pincode = dsResult.Tables[0].Rows[0]["Pincode"].ToString();
                            _objAgencyRegistraction.ProfileImage = dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() == "" ? "NoImage.jpg" : dsResult.Tables[0].Rows[0]["ProfileImage"].ToString() == null ? "NoImage.jpg" : ProfilePhoto;
                                _objAgencyRegistraction.ImageName = dsResult.Tables[0].Rows[0]["ProfileImage"].ToString();
                            _objAgencyRegistraction.AgencyName = dsResult.Tables[0].Rows[0]["agencyname"].ToString();
                            _objAgencyRegistraction.AgencyTypeID = dsResult.Tables[0].Rows[0]["AgencyTypeID"].ToString();
                            objResponse.Success = true;
                        }
                        else if (dsResult.Tables[0].Rows.Count == 0)
                        {
                            objResponse.Success = false;
                        }


                    }
                }
                objResponse.Data = _objAgencyRegistraction;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);

            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetTempAgencyRegstep1 | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "TempAgencyID:" + _objAgencyRegistraction.AgencyID + "" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region Agency reg step 1 get
        [Route("api/admin/GetTmpAgencyAdminInfo")]
        public HttpResponseMessage PostGetTmpAgencyAdminInfo(AgencyAdminInfo agencyAdminInfo)
        {
            AdminDAL _objadminDAL = new AdminDAL();
            Response objResponse = new Response();
            AgencyAdminInfo _objAgencyAdminInfo = new AgencyAdminInfo();
            string jsonData = string.Empty;
            DataSet dsResult = new DataSet();
            try
            {
                dsResult = _objadminDAL.GetTmpAgencyAdminInfoDal(agencyAdminInfo.TmpAgencyID);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {

                            
                            _objAgencyAdminInfo.MobileNo = dsResult.Tables[0].Rows[0]["MobileNo"].ToString();
                            _objAgencyAdminInfo.EmployeeCode = dsResult.Tables[0].Rows[0]["EmployeeCode"].ToString();
                            _objAgencyAdminInfo.FirstName = dsResult.Tables[0].Rows[0]["FirstName"].ToString();
                            _objAgencyAdminInfo.LastName = dsResult.Tables[0].Rows[0]["LastName"].ToString();
                            _objAgencyAdminInfo.Email = dsResult.Tables[0].Rows[0]["Email"].ToString(); 
                            objResponse.Success = true;
                        }
                        else if (dsResult.Tables[0].Rows.Count == 0)
                        {
                            objResponse.Success = false;
                        }


                    }
                }
                objResponse.Data = _objAgencyAdminInfo;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);

            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetTmpAgencyAdminInfo | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "TempAgencyID:" + _objAgencyAdminInfo.TmpAgencyID + "" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region Agency existing  list
        [Route("api/admin/GetAgencyExistingList")]
        public HttpResponseMessage PostGetAgencyExistingList(AgencyExisting agencyExisting)
        {
            AdminDAL objadminDAL = new AdminDAL();
            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            AgencyExisting objAgencyExisting = new AgencyExisting();
            List<AgencyExistingList> objAgencyExistingList = new List<AgencyExistingList>();
            try
            {
                dsResult = objadminDAL.GetAgencyExistingListDAL(agencyExisting);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0 && dsResult.Tables[1].Rows.Count > 0)
                    {
                        objAgencyExisting.TotalRows = dsResult.Tables[1].Rows[0]["TotalRows"].ToString();
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            objAgencyExistingList.Add(new AgencyExistingList
                            {
                                AgencyID = dsResult.Tables[0].Rows[i]["AgencyID"].ToString(),
                                IncID = common.EncodeTo64(dsResult.Tables[0].Rows[i]["AgencyID"].ToString()),
                                AgencyName = dsResult.Tables[0].Rows[i]["AgencyName"].ToString(),
                                mobileno = dsResult.Tables[0].Rows[i]["mobileno"].ToString(),
                                EmailAddress = dsResult.Tables[0].Rows[i]["EmailAddress"].ToString(),
                                statename = dsResult.Tables[0].Rows[i]["statename"].ToString(),
                                cityname = dsResult.Tables[0].Rows[i]["cityname"].ToString(),
                                KeyPersonName = dsResult.Tables[0].Rows[i]["KeyPersonName"].ToString(),
                                Inactive = dsResult.Tables[0].Rows[i]["Inactive"].ToString(),
                                
                            });
                        }
                        objAgencyExisting.AgencyExistingList = objAgencyExistingList;
                    }
                    else if (dsResult.Tables[0].Rows.Count == 0 || dsResult.Tables[1].Rows.Count == 0)
                    {
                        objResponse.Data = null;
                    }
                }
                else
                {
                    objResponse.Data = null;
                }
                objResponse.Data = objAgencyExisting;
                objResponse.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetAgencyExistingList | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                objResponse.Data = null;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }

        #endregion
        #region Update agency 
      
        [Route("api/admin/updateNewAgencyAdmin/")]
        public HttpResponseMessage SetNewAgencyAdmin(AgencyAdminInfo agencyAdminInfo)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.SetSetNewAgencyAdminDAL(agencyAdminInfo);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: updateNewAgencyAdmin | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "AgencyID: " + agencyAdminInfo.TmpAgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }

         


           
        #endregion
        #region Agency Registration edit
        [Route("api/admin/getagencydedetails/{agencyid}")]
        public HttpResponseMessage getagencydedetails(string agencyid)
        {
            AdminDAL admindal = new AdminDAL();
            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            List<AgencyRegistraction> _objAgentregistration = new List<AgencyRegistraction>();

            string jsonData = string.Empty;
            try
            {
                dsResult = admindal.GetAgentdetailsDAL(agencyid);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {

                            var ProfilePhoto = "";
                            if (dsResult.Tables[0].Rows[i]["ProfileImage"].ToString() != "" && dsResult.Tables[0].Rows[i]["ProfileImage"].ToString() != null)
                            {
                                ProfilePhoto = System.Configuration.ConfigurationManager.AppSettings["PinBoxIndiaProfilePhoto"].ToString() + dsResult.Tables[0].Rows[i]["ProfileImage"].ToString();

                            }
                            _objAgentregistration.Add(new AgencyRegistraction
                            {
                                AgencyID = dsResult.Tables[0].Rows[i]["AgencyID"].ToString(),
                                AgencyTypeID = dsResult.Tables[0].Rows[i]["AgencyTypeID"].ToString(),
                                AgencyName = dsResult.Tables[0].Rows[i]["AgencyName"].ToString(),
                                stateID = dsResult.Tables[0].Rows[i]["stateid"].ToString(),
                                CityID = dsResult.Tables[0].Rows[i]["cityid"].ToString(),
                                Address1 = dsResult.Tables[0].Rows[i]["Address1"].ToString(),
                                Address2 = dsResult.Tables[0].Rows[i]["Address2"].ToString(),
                                Pincode = dsResult.Tables[0].Rows[i]["pincode"].ToString(), 
                                ProfileImage = dsResult.Tables[0].Rows[i]["ProfileImage"].ToString() == "" ? "NoImage.jpg" : dsResult.Tables[0].Rows[i]["ProfileImage"].ToString() == null ? "NoImage.jpg" : ProfilePhoto,
                                ImageName = dsResult.Tables[0].Rows[i]["ProfileImage"].ToString(),
                            });
                        }
                    }
                }
                objResponse.Success = true;
                objResponse.Data = _objAgentregistration;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: getagencydedetails | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "AgencyID: " + agencyid + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }


        [Route("api/admin/updateagencydedetails")]
        public HttpResponseMessage updateagencydedetails(AgencyRegistraction agentInfo)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {

                if (agentInfo.ImageName == "" && agentInfo.ProfileImage != "")
                {
                    string attachment = "../../Content/img/ProfilePhoto/";
                    DateTime centuryBegin = new DateTime(2001, 1, 1);
                    DateTime currentDate = DateTime.Now;
                    long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
                    string imagename = SaveImage(attachment, elapsedTicks.ToString(), agentInfo.ProfileImage);
                    agentInfo.ProfileImage = imagename;

                }
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.UpdateAgencyDetailsDal(agentInfo);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: updateagencydedetails | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "AgencyID: " + agentInfo.AgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }


        [Route("api/admin/GetAgencyBankAccountStep2")]
        public HttpResponseMessage PostGetAgencyBankAccountStep2(AgencyBankAccount _objAgencyBankAccount)
        {
            AdminDAL _objadminDAL = new AdminDAL();
            Response objResponse = new Response();
            AgencyBankAccount _objAgencyBankAccountData = new AgencyBankAccount();

            string jsonData = string.Empty;
            DataSet dsResult = new DataSet();

            try
            {
                dsResult = _objadminDAL.GetAgencyBankAccountStep2Dal(_objAgencyBankAccount.TmpAgencyID);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            _objAgencyBankAccountData.TmpAgencyID = dsResult.Tables[0].Rows[0]["AgencyID"].ToString();
                            _objAgencyBankAccountData.AccountName = dsResult.Tables[0].Rows[0]["AccountName"].ToString();
                            _objAgencyBankAccountData.AccountNo = dsResult.Tables[0].Rows[0]["AccountNo"].ToString();
                            _objAgencyBankAccountData.IFSCCode = dsResult.Tables[0].Rows[0]["IFSCCode"].ToString(); 
                            objResponse.Success = true;
                        }
                        else if (dsResult.Tables[0].Rows.Count == 0)
                        {
                            objResponse.Success = false;
                        }


                    }
                }
                objResponse.Data = _objAgencyBankAccountData;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);

            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetAgencyBankAccountStep2 | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "TempAgencyID:" + _objAgencyBankAccountData.TmpAgencyID + "" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }

        [Route("api/admin/updateagencyBankDetails/")]
        public HttpResponseMessage PostupdateagencyBankDetailsStep2(AgencyBankAccount agentInfo)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.UpdateAgentBankDetailsDl(agentInfo);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: updateagencyBankDetails | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "AgencyID: " + agentInfo.TmpAgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }


        [Route("api/admin/SetAgencyActiveInactive/")]
        public HttpResponseMessage PostSetAgencyActiveInactive(ActiveInactiveAgencySearch activeInactiveAgencySearch)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.AgencyActiveInactiveDAL(activeInactiveAgencySearch);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetAgencyActiveInactive | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "AgencyID: " + activeInactiveAgencySearch.AgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }
        [Route("api/admin/AgencyServiceChargeByAgenctID/{agencyid}")]
        public HttpResponseMessage GetAgencyServiceChargeByAgenctID(string agencyid)
        {
            AdminDAL admindal = new AdminDAL();
            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            AgencyServiceChargesContainer _objAgencyServiceChargesContainer = new AgencyServiceChargesContainer();
            List<AgencyServiceCharges> _objAgencyServiceChargesList = new List<AgencyServiceCharges>();


            string jsonData = string.Empty;
            try
            {
                dsResult = admindal.GetAGServiceChargesByIDDAL(agencyid);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            _objAgencyServiceChargesList.Add(new AgencyServiceCharges
                            {
                                SAChkID = (i + 1).ToString(),
                                Task = dsResult.Tables[0].Rows[i]["Task"].ToString(),
                                TaskID = dsResult.Tables[0].Rows[i]["TaskID"].ToString(),
                                TaskParentID = dsResult.Tables[0].Rows[i]["TaskParentID"].ToString(),
                                TaskOrder = dsResult.Tables[0].Rows[i]["TaskOrder"].ToString(),
                                VarValue = dsResult.Tables[0].Rows[i]["VarValue"].ToString(),
                                variableOption = dsResult.Tables[0].Rows[i]["variableOption"].ToString(),
                            });
                        }

                        _objAgencyServiceChargesContainer.AgencyServiceChargesList = _objAgencyServiceChargesList;
                    }
                }
                objResponse.Success = true;
                objResponse.Data = _objAgencyServiceChargesContainer;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: AgencyServiceChargeByAgenctID | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "AgencyId: " + agencyid + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }


        [Route("api/admin/SetUpdateAgencyCharge/")]
        public HttpResponseMessage UpdateAgencyCharge(AgencyWithAdmin agencyAdminInfo)
        {
            AdminDAL objadmindal = new AdminDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objadmindal.SetUpdateAgencyChargeDal(agencyAdminInfo);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetUpdateAgencyCharge | Contoller Name: Admin Controller" + Environment.NewLine;
                logMsg += "AgencyID: " + agencyAdminInfo.TmpAgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }


        #endregion 
    }
}
