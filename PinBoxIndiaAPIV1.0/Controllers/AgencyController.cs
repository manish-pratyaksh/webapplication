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
    public class AgencyController : ApiController
    {
        #region CreateBranchStep1     

        [Route("api/Agency/SetNewBranchStep1")]
        public HttpResponseMessage PostSetNewBranchStep1(CreateNewBranchStep1 _objCreateNewBranchStep1A)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();                
                jsonResult = _objAgencyDAL.SetNewBranchStep1(_objCreateNewBranchStep1A);

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
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetNewBranchStep1 | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "Temp Reg ID:" + _objCreateNewBranchStep1A.AgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }

        [Route("api/Agency/GetNewBranchStep1")]
        public HttpResponseMessage PostGetNewBranchStep1(CreateNewBranchStep1 _objCreateNewBranchStep1A)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            CreateNewBranchStep1 _objCreateNewBranchStep1 = new CreateNewBranchStep1();
            try
            {
                DataSet dsResult = new DataSet();
                string jsonData = string.Empty;
                dsResult = _objAgencyDAL.GetNewBranchStep1(_objCreateNewBranchStep1A);

                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            _objCreateNewBranchStep1.AgencyBranchID = dsResult.Tables[0].Rows[0]["TmpAgencyBranchID"].ToString();
                            _objCreateNewBranchStep1.AgencyID = dsResult.Tables[0].Rows[0]["AgencyID"].ToString();
                            _objCreateNewBranchStep1.BranchName = dsResult.Tables[0].Rows[0]["BranchName"].ToString();
                            _objCreateNewBranchStep1.AddressLine1 = dsResult.Tables[0].Rows[0]["Address1"].ToString();
                            _objCreateNewBranchStep1.AddressLine2 = dsResult.Tables[0].Rows[0]["Address2"].ToString();
                            _objCreateNewBranchStep1.State = dsResult.Tables[0].Rows[0]["StateID"].ToString();
                            _objCreateNewBranchStep1.City = dsResult.Tables[0].Rows[0]["CityID"].ToString();
                            _objCreateNewBranchStep1.POBoxNo = dsResult.Tables[0].Rows[0]["POBox"].ToString();                            
                        }
                        objResponse.Data = _objCreateNewBranchStep1;
                        objResponse.Success = true;
                    }
                }
                else
                {
                    objResponse.Success = false;
                    objResponse.Data = "-2";
                }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetNewBranchStep1 | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "TempReg ID:" + _objCreateNewBranchStep1A.AgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }

        #endregion
        #region CreateBranchStep2
        [Route("api/Agency/SetNewBranchStep2/")]
        public HttpResponseMessage SetNewBranchStep2(CreateNewBranchStep2 _objCreateNewBranchStep2)
        {

            AgencyDAL objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objAgencyDAL.SetNewBranchStep2(_objCreateNewBranchStep2);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetNewBranchStep2 | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "UserCategoryID:" + _objCreateNewBranchStep2.UserCategory + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
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
        #endregion
        #region ManageBranch
        [Route("api/Agency/ViewExistingBranchesList")]
        public HttpResponseMessage PosViewExistingBranchesList(AgencyBranchFilter _objAgencyBranchFilterA)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            string jsonData = string.Empty;
            DataSet dsResult = new DataSet();
            AgencyBranchFilter _objAgencyBranch = new AgencyBranchFilter();
            List<ExistingBranches> _objExistingBranchesList = new List<ExistingBranches>();
            try
            {
                dsResult = _objAgencyDAL.ViewExistingBranchesList(_objAgencyBranchFilterA);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            _objExistingBranchesList.Add(new ExistingBranches
                            {
                                AgencyBranchID = dsResult.Tables[0].Rows[i]["AgencyBranchID"].ToString(),
                                BranchName = dsResult.Tables[0].Rows[i]["BranchName"].ToString(),
                                AgencyName = dsResult.Tables[0].Rows[i]["AgencyName"].ToString(),
                                FirstName = dsResult.Tables[0].Rows[i]["FirstName"].ToString(),                                
                                AgencyAdminInfoID = dsResult.Tables[0].Rows[i]["AgencyAdminInfoID"].ToString(),
                                Mobile = dsResult.Tables[0].Rows[i]["Mobile"].ToString(),
                                Email = dsResult.Tables[0].Rows[i]["Email"].ToString(),
                                State = dsResult.Tables[0].Rows[i]["STATE NAME"].ToString(),
                                City = dsResult.Tables[0].Rows[i]["cityname"].ToString(),
                                Inactive = dsResult.Tables[0].Rows[i]["Inactive"].ToString(),
                                IncID = common.EncodeTo64(dsResult.Tables[0].Rows[i]["AgencyBranchID"].ToString())
                            });
                        }

                        _objAgencyBranch.ExistingBranchesList = _objExistingBranchesList;
                        if (dsResult.Tables[1].Rows.Count > 0 && dsResult.Tables[1].Rows.Count > 0)
                        {
                            _objAgencyBranch.TotalCount = dsResult.Tables[1].Rows[0]["TotalCount"].ToString();
                        }

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
                objResponse.Data = _objAgencyBranch;
                objResponse.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: ViewExistingBranchesList | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "AgencyID:" + _objAgencyBranchFilterA.AgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                objResponse.Data = null;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        [Route("api/Agency/GetAgencyBranchUpdate")]
        public HttpResponseMessage PostGetAgencyBranchUpdate(CreateNewBranchStep1 _objGetBranchUpdateA)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            CreateNewBranchStep1 _objCreateNewBranchStep1 = new CreateNewBranchStep1();
            try
            {
                DataSet dsResult = new DataSet();
                string jsonData = string.Empty;
                dsResult = _objAgencyDAL.GetAgencyBranchUpdate(_objGetBranchUpdateA);

                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            _objCreateNewBranchStep1.AgencyBranchID = dsResult.Tables[0].Rows[0]["AgencyBranchID"].ToString();
                            _objCreateNewBranchStep1.AgencyID = dsResult.Tables[0].Rows[0]["AgencyID"].ToString();
                            _objCreateNewBranchStep1.BranchName = dsResult.Tables[0].Rows[0]["BranchName"].ToString();
                            _objCreateNewBranchStep1.AddressLine1 = dsResult.Tables[0].Rows[0]["Address1"].ToString();
                            _objCreateNewBranchStep1.AddressLine2 = dsResult.Tables[0].Rows[0]["Address2"].ToString();
                            _objCreateNewBranchStep1.State = dsResult.Tables[0].Rows[0]["StateID"].ToString();
                            _objCreateNewBranchStep1.City = dsResult.Tables[0].Rows[0]["CityID"].ToString();
                            _objCreateNewBranchStep1.POBoxNo = dsResult.Tables[0].Rows[0]["Pincode"].ToString();                            
                            _objCreateNewBranchStep1.IncID = common.EncodeTo64(dsResult.Tables[0].Rows[0]["AgencyBranchID"].ToString());
                        }
                        objResponse.Data = _objCreateNewBranchStep1;
                        objResponse.Success = true;
                    }
                }
                else
                {
                    objResponse.Success = false;
                    objResponse.Data = "-2";
                }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetAgencyBranchUpdate | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "Temp Reg ID:" + _objGetBranchUpdateA.AgencyBranchID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        [Route("api/Agency/SetAgencyBranchUpdate")]
        public HttpResponseMessage PostSetAgencyBranchUpdate(CreateNewBranchStep1 _objBranchUpdateA)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objAgencyDAL.SetUpdateBranchDetails(_objBranchUpdateA);

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
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetAgencyBranchUpdate | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "Temp Reg ID:" + _objBranchUpdateA.AgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        [Route("api/Agency/ChangeStatusBranch")]
        public HttpResponseMessage PostChangeStatusBranch(ChangeBranchStatus _objChangeBranchStatusA)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objAgencyDAL.ChangeStatusBranch(_objChangeBranchStatusA);

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
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: ChangeStatusBranch | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "Temp Reg ID:" + _objChangeBranchStatusA.AgencyBranchID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        [Route("api/Agency/UpdateMemberProfile")]
        public HttpResponseMessage PostUpdateMemberProfile(AgencyAdminInfo objAgentStepA)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            MemberRegistration _objNewMemberRegt = new MemberRegistration();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objAgencyDAL.SetMemberProfile(objAgentStepA);

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
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: UpdateMemberProfile | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "Temp Reg ID:" + objAgentStepA.TmpAgencyID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
   #endregion
        #region AgencyuserExecting
        [Route("api/Agency/AgencyExistingUser")]
        public HttpResponseMessage PosAgencyExistingUser(AgencyExectingUser _objAgencyExectingUser)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            string jsonData = string.Empty;
            DataSet dsResult = new DataSet();
            AgencyExistinglist _objAgencyExistinglist = new AgencyExistinglist();
            List<AgencyUsers> _objAgencyUsersList = new List<AgencyUsers>();
            try
            {
                dsResult = _objAgencyDAL.ViewExistingagencyuserList(_objAgencyExectingUser);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            _objAgencyUsersList.Add(new AgencyUsers
                            {
                                
                                AUIID = dsResult.Tables[0].Rows[i]["AUIID"].ToString(),
                                AgentName = dsResult.Tables[0].Rows[i]["AgentName"].ToString(),
                                mobileno = dsResult.Tables[0].Rows[i]["mobileno"].ToString(),
                                EmailAddress = dsResult.Tables[0].Rows[i]["EmailAddress"].ToString(),
                                AgencyID = dsResult.Tables[0].Rows[i]["AgencyID"].ToString(),
                                AgencyBranchID = dsResult.Tables[0].Rows[i]["AgencyBranchID"].ToString(),
                                statename = dsResult.Tables[0].Rows[i]["statename"].ToString(),
                                cityname = dsResult.Tables[0].Rows[i]["cityname"].ToString(),
                                Inactive = dsResult.Tables[0].Rows[i]["Inactive"].ToString()
                              
                            });
                        }

                        _objAgencyExistinglist.agencyuser = _objAgencyUsersList;
                        if (dsResult.Tables[1].Rows.Count > 0 && dsResult.Tables[1].Rows.Count > 0)
                        {
                            _objAgencyExistinglist.TotalRows = dsResult.Tables[1].Rows[0]["TotalRows"].ToString();
                        }

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
                objResponse.Data = _objAgencyExistinglist;
                objResponse.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: AgencyExistingUser | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "AgencyID:" + "" + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                objResponse.Data = null;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region Activate/Deactivate Agent
        [Route("api/agency/ChangeStatusManageAgent")]
        public HttpResponseMessage PostChangeStatusManageAgent(ChangeBranchStatus _objChangeBranchStatusA)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objAgencyDAL.PostChangeStatusManageAgent(_objChangeBranchStatusA);

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
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: ChangeStatusManageAgent | Contoller Name: Agency Controller" + Environment.NewLine;
                logMsg += "Temp Reg ID:" + _objChangeBranchStatusA.AgencyBranchID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        #endregion
        #region Agency Branch ddl 
        [Route("api/Agency/GetAgBranchDDL")]
        public HttpResponseMessage PostGetAgBranchDDL(mstAgencyBranch objmstAgencyBranch)
        {
            AgencyDAL _objAgencyDAL = new AgencyDAL();
            Response objResponse = new Response();
            mstAgencyBranch _objmstAgencyBranch = new mstAgencyBranch();
            List<AgBranchDDL> _objAgBranchDDLList = new List<AgBranchDDL>();
            try
            {
                DataSet dsResult = new DataSet();
                string jsonData = string.Empty;
                dsResult = _objAgencyDAL.GetAgencyBranchDdlDAL(objmstAgencyBranch);

                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                            {
                                _objAgBranchDDLList.Add(new AgBranchDDL 
                                {
                                    AgencyBranchID = dsResult.Tables[0].Rows[i]["AgencyBranchID"].ToString(),
                                    BranchName = dsResult.Tables[0].Rows[i]["BranchName"].ToString()
                                });
                            }

                            _objmstAgencyBranch.AgencyBranchList = _objAgBranchDDLList;

                            objResponse.Data = _objmstAgencyBranch;
                            objResponse.Success = true;
                            return Request.CreateResponse(HttpStatusCode.OK, objResponse);
                        }
                        else
                        {
                            _objmstAgencyBranch.AgencyBranchList = _objAgBranchDDLList;

                            objResponse.Data = _objmstAgencyBranch;
                            objResponse.Success = true;
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {

                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetAgBranchDDL | Contoller Name: Agency Controller" + Environment.NewLine;
               
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        #endregion
    }
}
