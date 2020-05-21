using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PinBoxIndiaAPIV1._0.DAL;
using PinBoxIndiaAPIV1._0.Models;
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
    public class CommonController : ApiController
    {
        #region state and city master

        [Route("api/Common/GetStateDDL")]
        public HttpResponseMessage GetStateDDL()
        {
            string imcommingLog = "";
            imcommingLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetStateDDL " + Environment.NewLine;
            CommonDAL _objCommonDAL = new CommonDAL();
            Response objResponse = new Response();
            mststate _objmststate = new mststate();
            List<state> _objstate = new List<state>();
            try
            {
                DataSet dsResult = new DataSet();
                string jsonData = string.Empty;
                dsResult = _objCommonDAL.mstStateDDL();
                imcommingLog += "Count: " + dsResult.Tables[0].Rows.Count + " " + Environment.NewLine;
                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                            {
                                _objstate.Add(new state
                                {
                                    stateid = dsResult.Tables[0].Rows[i]["StateSNo"].ToString(),
                                    statename = dsResult.Tables[0].Rows[i]["STATE NAME"].ToString()
                                });
                            }

                            _objmststate.statelist = _objstate;

                            objResponse.Data = _objmststate;
                            objResponse.Success = true;
                            ErrorHandler.ErrorLog(imcommingLog);
                            return Request.CreateResponse(HttpStatusCode.OK, objResponse);
                        }
                        else
                        {
                            objResponse.Success = false;
                            objResponse.Data = "-2";
                        }
                    }
                }
                ErrorHandler.ErrorLog(imcommingLog);
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetStateDDL | Contoller Name: common Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                imcommingLog += logMsg;
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        [Route("api/Common/GetGetCityDDL")]
        public HttpResponseMessage PostGetCityDDL(mstcity objmstcity)
        {
            CommonDAL _objCommonDAL = new CommonDAL();
            Response objResponse = new Response();
            mstcity _objmstcity = new mstcity();
            List<city> _objcitylist = new List<city>();
            try
            {
                DataSet dsResult = new DataSet();
                string jsonData = string.Empty;
                dsResult = _objCommonDAL.GetCityDDLDal(objmstcity);

                if (dsResult != null)
                {
                    if (dsResult.Tables[0] != null)
                    {
                        if (dsResult.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                            {
                                _objcitylist.Add(new city
                                {
                                    cityid = dsResult.Tables[0].Rows[i]["cityId"].ToString(),
                                    cityname = dsResult.Tables[0].Rows[i]["cityname"].ToString()
                                });
                            }

                            _objmstcity.citylist = _objcitylist;

                            objResponse.Data = _objmstcity;
                            objResponse.Success = true;
                            return Request.CreateResponse(HttpStatusCode.OK, objResponse);
                        }
                        else
                        {
                            _objmstcity.citylist = _objcitylist;

                            objResponse.Data = _objmstcity;
                            objResponse.Success = true;
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetGetCityDDL | Contoller Name: common Controller" + Environment.NewLine;
                logMsg += "stateid: " + objmstcity.stateid + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response

                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        #endregion 
        #region RightGroupMenu
        [Route("api/home/GetRightGroupMenu/")]
        public HttpResponseMessage POSTRightGroupMenu(BindMenuFilter _objBindMenuFilter)
        {
            CommonDAL _objCommonDAL = new CommonDAL();
            Response objResponse = new Response();
            string JsonData = string.Empty;
            DataSet dsResult = new DataSet();
            try
            {
                RightGroupMenu objRightGPMenu = new RightGroupMenu();
                List<GetMainPage> objMainPage = new List<GetMainPage>();
                List<GetSubpage> objsubPage = new List<GetSubpage>();
                dsResult = _objCommonDAL.GETRightGroupLeftMenu(_objBindMenuFilter);
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
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetRightGroupMenu | Contoller Name: Common Controller" + Environment.NewLine;
                logMsg += "UserCategoryID:" + _objBindMenuFilter.UserCategoryID + "|" + "RightGroupID:" + _objBindMenuFilter.RightGroupID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
            }
        }
        #endregion 
    }
}
