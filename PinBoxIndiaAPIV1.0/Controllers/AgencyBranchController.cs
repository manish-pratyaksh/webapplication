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
    public class AgencyBranchController : ApiController
    {
        #region  new agent addd
        [Route("api/AgencyBranch/Addnewagent")]
        public HttpResponseMessage PostAddnewagent(NewAgentADD _objNewAgentADD)
        {

            AgencyBranchDAL objAgencyBranchDal = new AgencyBranchDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objAgencyBranchDal.AddnewAgent(_objNewAgentADD);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: Addnewagent | Contoller Name: Agency Branch Controller" + Environment.NewLine;
                logMsg += "UserCategoryID:" + _objNewAgentADD.UserCategory + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
      #endregion 
        #region get agent list
        [Route("api/AgencyBranch/Getagent")]
        public HttpResponseMessage PostGetagent(ExistingUserSearch _objExistingUserSearch)
        {

            AgencyBranchDAL objAgencyBranchDal = new AgencyBranchDAL();
            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            ExistingUserSearch objAgentExistingDetails = new ExistingUserSearch();
            List<ExistingUser> objAgentExistingDataList = new List<ExistingUser>();
            try
            {
                dsResult = objAgencyBranchDal.GetAgent(_objExistingUserSearch);
                
                if (dsResult != null)
                {
                    if (dsResult.Tables.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                            {
                               
                                objAgentExistingDataList.Add(new ExistingUser
                                {

                                    IncID = common.EncodeTo64(dsResult.Tables[0].Rows[i]["UserInfoID"].ToString()),
                                    UserInfoID = dsResult.Tables[0].Rows[i]["UserInfoID"].ToString(),
                                    FirstName = dsResult.Tables[0].Rows[i]["FirstName"].ToString(),
                                    LastName = dsResult.Tables[0].Rows[i]["LastName"].ToString(),
                                    EmployeeCode = dsResult.Tables[0].Rows[i]["EmployeeCode"].ToString(),
                                    Email = dsResult.Tables[0].Rows[i]["EmailAddress"].ToString(),
                                    Inactive = dsResult.Tables[0].Rows[i]["Inactive"].ToString(),
                                    Mobile = dsResult.Tables[0].Rows[i]["MobileNo"].ToString(),
                                 

                                });
                            }
                            objAgentExistingDetails.ExistingUserList = objAgentExistingDataList;
                        }
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            objAgentExistingDetails.TotalCount = dsResult.Tables[1].Rows[0]["Column1"].ToString();
                        }
                        else
                        {
                            objAgentExistingDetails.TotalCount = "0";
                        }

                        objResponse.Data = objAgentExistingDetails;
                        objResponse.Success = true;
                    }
                    else
                    {
                        objResponse.Data = null; ;
                        objResponse.Success = true;
                    }
                }
                else
                {
                    objResponse.Data = null; ;
                    objResponse.Success = true;
                }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: Getagent | Contoller Name: Agency Branch Controller" + Environment.NewLine;               
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion
        #region AgentDetails
        [Route("api/AgencyBranch/getAgentDetailsByUserInfoID/{UserInfoID}")]
        public HttpResponseMessage getAgentDetails(string UserInfoID)
        {
            AgencyBranchDAL agentdal = new AgencyBranchDAL();
            Response objResponse = new Response();
            DataSet dsResult = new DataSet();
            ExistingUserSearch objAgentExistingDetails = new ExistingUserSearch();
            List<ExistingUser> objAgentExistingDataList = new List<ExistingUser>();
            string jsonData = string.Empty;
            try
            {
                dsResult = agentdal.GetAgentdetailsByUserinfoID(UserInfoID);
                if (dsResult != null)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                        {
                            objAgentExistingDataList.Add(new ExistingUser
                            {
                                FirstName = dsResult.Tables[0].Rows[i]["FirstName"].ToString(),
                                LastName = dsResult.Tables[0].Rows[i]["LastName"].ToString(),
                                EmployeeCode = dsResult.Tables[0].Rows[i]["EmployeeCode"].ToString(),
                                UserInfoID = dsResult.Tables[0].Rows[i]["UserInfoID"].ToString(),
                                Mobile = dsResult.Tables[0].Rows[i]["MobileNo"].ToString(),
                                Email = dsResult.Tables[0].Rows[i]["EmailAddress"].ToString(),
                            });
                        }
                        objAgentExistingDetails.ExistingUserList = objAgentExistingDataList;
                    }
                }
                objResponse.Success = true;
                objResponse.Data = objAgentExistingDetails;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: getAgentDetailsByUserInfoID | Contoller Name: AgencyBranch Controller" + Environment.NewLine;
                logMsg += "UserInfoID:" + UserInfoID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }
        #endregion
        #region Agent Activation Detail
        [Route("api/AgencyBranch/SetAgentActiveInactive/")]
        public HttpResponseMessage PostSetAgentActiveInactive(AgenctActiveInactive agenctActiveInactive)
        {
            AgencyBranchDAL objAgencyBranchDAL = new AgencyBranchDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objAgencyBranchDAL.AgentActiveInactiveDAL(agenctActiveInactive);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: SetAgentActiveInactive | Contoller Name: AgencyBranch Controller" + Environment.NewLine;
                logMsg += "userinfoid:" + agenctActiveInactive.UserInfoID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }

        }
        #endregion
        #region UpdateAgent
        [Route("api/AgencyBranch/Updateagent")]
        public HttpResponseMessage PostUpdateagent(NewAgentADD _objNewAgentADD)
        {

            AgencyBranchDAL objAgencyBranchDal = new AgencyBranchDAL();
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = objAgencyBranchDal.AddnewAgent(_objNewAgentADD);
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }

                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: Updateagent | Contoller Name: Agency Branch Controller" + Environment.NewLine;
                logMsg += "UserCategoryID:" + _objNewAgentADD.UserCategory + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
                objResponse.ErrorList.Add(new Error { errorCode = "EX", errorMsg = ex.Message });
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
      #endregion     
    }
}
