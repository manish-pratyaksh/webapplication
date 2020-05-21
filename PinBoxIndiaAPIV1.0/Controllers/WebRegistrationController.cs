using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PinBoxIndiaAPIV1._0.DAL;
using PinBoxIndiaAPIV1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace PinBoxIndiaAPIV1._0.Controllers
{
    public class WebRegistrationController : ApiController
    {
        [Route("api/WebReg/PersonalDetail")]
        public HttpResponseMessage PostPersonalDetail(PersonalDetails _personalDetails)
        {
            WebRegDAL _objWebRegDAL = new WebRegDAL();
            string webregLog = "", jsonString = "";
            webregLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PersonalDetail " + Environment.NewLine;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new DataTableConverter());
            jsonString = JsonConvert.SerializeObject(_personalDetails, Formatting.None, serializerSettings);
            webregLog += "Request:" + jsonString.ToString() + " " + Environment.NewLine;        
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objWebRegDAL.SetPersonalDetails(_personalDetails);
                webregLog += "Response:" + jsonResult.ToString() + " " + Environment.NewLine;       
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }
                ErrorHandler.ErrorLog(webregLog, "WEBREG");
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PersonalDetail | Contoller Name: WebRegistration Controller" + Environment.NewLine;
                logMsg += "Mobile Number: " + _personalDetails.Mobile_Number + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                webregLog += logMsg;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                ErrorHandler.ErrorLog(webregLog, "WEBREG");
                return response;
            }

        }
        [Route("api/WebReg/AddressDetails")]
        public HttpResponseMessage PostAddressDetails(Address _address)
        {
            WebRegDAL _objWebRegDAL = new WebRegDAL();
            string webregLog = "", jsonString = "";
            webregLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: AddressDetails " + Environment.NewLine;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new DataTableConverter());
            jsonString = JsonConvert.SerializeObject(_address, Formatting.None, serializerSettings);
            webregLog += "Request:" + jsonString.ToString() + " " + Environment.NewLine;
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objWebRegDAL.SetAddressDetails(_address);
                webregLog += "Response:" + jsonResult.ToString() + " " + Environment.NewLine;
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }
                ErrorHandler.ErrorLog(webregLog, "WEBREG");
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: AddressDetails | Contoller Name: WebRegistration Controller" + Environment.NewLine;
                logMsg += "Created By " + _address.CreatedBy + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                webregLog += logMsg;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                ErrorHandler.ErrorLog(webregLog, "WEBREG");
                return response;
            }

        }
        [Route("api/WebReg/BankDetail")]
        public HttpResponseMessage PostBankDetail(BankDetails _bankDetails)
        {
            WebRegDAL _objWebRegDAL = new WebRegDAL();
            string webregLog = "", jsonString = "";
            webregLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: BankDetail " + Environment.NewLine;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new DataTableConverter());
            jsonString = JsonConvert.SerializeObject(_bankDetails, Formatting.None, serializerSettings);
            webregLog += "Request:" + jsonString.ToString() + " " + Environment.NewLine;
            Response objResponse = new Response();
            try
            {
                var jsonResult = new StringBuilder();
                jsonResult = _objWebRegDAL.SetBankDetails(_bankDetails);
                webregLog += "Response:" + jsonResult.ToString() + " " + Environment.NewLine;
                objResponse.Data = jsonResult.ToString();
                if (!string.IsNullOrEmpty(jsonResult.ToString()) && jsonResult.ToString() != "-1") { objResponse.Success = true; }
                else { objResponse.Success = false; }
                ErrorHandler.ErrorLog(webregLog, "WEBREG");
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: BankDetail | Contoller Name: WebRegistration Controller" + Environment.NewLine;
                logMsg += "UserInfoID: " + _bankDetails.UserInfoID + "|" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                webregLog += logMsg;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                ErrorHandler.ErrorLog(webregLog, "WEBREG");
                return response;
            }

        }
    }
}
