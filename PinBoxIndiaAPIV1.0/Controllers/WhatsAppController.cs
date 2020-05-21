using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PinBoxIndiaAPIV1._0.Models;
using PinBoxIndiaAPIV1._0.DAL;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Web;
using PinBoxIndiaAPIV1._0.Models.WhatsAppSendingMessageResponse;
using System.Web.Hosting;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace PinBoxIndiaAPIV1._0.Controllers
{
    public class WhatsAppController : ApiController
    {
        [Route("api/whatsapp/IncommingMessage")]
        public HttpResponseMessage PostIncommingMessage(WhatsappImcomming _whatsappImcomming)
        {
            string imcommingLog = "", jsonString = "";
            imcommingLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PostIncommingMessage " + Environment.NewLine;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new DataTableConverter());
            jsonString = JsonConvert.SerializeObject(_whatsappImcomming, Newtonsoft.Json.Formatting.None, serializerSettings);
            imcommingLog += "Request:" + jsonString.ToString() + " " + Environment.NewLine;
            Response objResponse = new Response();
            DataTable dtCurrentScreen = new DataTable();
            WhatsAppDAL _objWhatsAppDAL = new WhatsAppDAL();
            try
            {
                string UserType = "6";
                string imgUrl = string.Empty, inputdetails = string.Empty, filename = string.Empty, parsedText = string.Empty, result = string.Empty,
                    ScreenText = string.Empty, SecondScreenText = string.Empty, RequestFrom = string.Empty, MobileNo = "", Text = "", MessageType = "", ScreenID = "", AadharSaveResult = "", imagename = string.Empty; ;
                string attachment = ConfigurationManager.AppSettings["WhatsappFilePath"].ToString();
                List<Result> _objResult = _whatsappImcomming.results;
                foreach (var _objIncomingResponse in _objResult)
                {
                    RequestFrom = _objIncomingResponse.from.ToString();
                    MobileNo = RequestFrom.Replace("91", "");
                    MessageType = _objIncomingResponse.message.type.ToString();
                    #region Text
                    if (MessageType.ToUpper() == "TEXT".ToUpper())
                    {
                        if (ConfigurationManager.AppSettings["AppInstance"].ToString().ToUpper() == "DEV")
                            Text = _objIncomingResponse.message.text.ToString().Substring(7, _objIncomingResponse.message.text.ToString().Length - 7);
                        if (Text != "")
                        {
                            dtCurrentScreen = GetScreenDetails(MobileNo, UserType, Text);
                            if (dtCurrentScreen != null && dtCurrentScreen.Rows.Count > 0)
                            {
                                ScreenID = dtCurrentScreen.Rows[0]["CurrentScreenID"].ToString();
                                ScreenText = dtCurrentScreen.Rows[0]["ScreenText"].ToString();
                                SecondScreenText = dtCurrentScreen.Rows[0]["ScreenText2"].ToString();
                                imcommingLog += "Mobile No:" + MobileNo + ", Text:" + Text + " " + Environment.NewLine;
                                string SubscriberMobileNo = dtCurrentScreen.Rows[0]["SubuscriberMobileNo"].ToString();
                                #region SendOTP
                                if (dtCurrentScreen.Rows[0]["CurrentScreenID"].ToString() == "3")
                                {
                                    if (SubscriberMobileNo != "" && SubscriberMobileNo.Length == 10)
                                    {
                                        string OTP = generateOTP();
                                        string setOTPresult = _objWhatsAppDAL.SetSubscriberOTP(SubscriberMobileNo, OTP);
                                        if (setOTPresult == "2000")
                                        {
                                            string OTPMessage = "Your OTP is " + OTP;
                                            PostSendingMessage(OTPMessage, MobileNo);
                                        }
                                    }
                                }
                                #endregion
                                #region QRCode
                                if (dtCurrentScreen.Rows[0]["CurrentScreenID"].ToString() == "5" && Text != "1")                              
                                {
                                    imcommingLog += "CurrentScreenID :" + dtCurrentScreen.Rows[0]["CurrentScreenID"].ToString() + " " + Environment.NewLine;
                                    ScreenText = "";
                                    PrintLetterBarcodeData printLetterBarcodeData = new PrintLetterBarcodeData();
                                    try
                                    {
                                        printLetterBarcodeData = (PrintLetterBarcodeData)(ObjectToXML(Text.Replace("</?xml", "<?xml"), typeof(PrintLetterBarcodeData)));
                                        AadhaarDetails _objAadhaarDetails = new AadhaarDetails();
                                        string Aadhar = printLetterBarcodeData.Uid.ToString();
                                        Int64 AadhaarNo;
                                        if (Aadhar.ToString().Trim().Length == 12 && Int64.TryParse(Aadhar.ToString().Trim(), out AadhaarNo) == true)
                                            _objAadhaarDetails.AadhaarNumber = AadhaarNo.ToString();

                                        if (printLetterBarcodeData.Name != null && printLetterBarcodeData.Name != "")
                                        {
                                            string name = printLetterBarcodeData.Name.ToString().Trim();
                                            string[] nameArray = name.Split(' ');
                                            if (nameArray.Length == 1)
                                            {
                                                _objAadhaarDetails.First_Name = nameArray[0].Trim();
                                                _objAadhaarDetails.Middle_Name = "";
                                                _objAadhaarDetails.Last_Name = "";
                                            }
                                            else if (nameArray.Length == 2)
                                            {
                                                _objAadhaarDetails.First_Name = nameArray[0].Trim();
                                                _objAadhaarDetails.Middle_Name = "";
                                                _objAadhaarDetails.Last_Name = nameArray[1].Trim();
                                            }
                                            else if (nameArray.Length == 3)
                                            {
                                                _objAadhaarDetails.First_Name = nameArray[0].Trim();
                                                _objAadhaarDetails.Middle_Name = nameArray[1].Trim();
                                                _objAadhaarDetails.Last_Name = nameArray[2].Trim();
                                            }
                                            else if (nameArray.Length > 3)
                                            {
                                                _objAadhaarDetails.First_Name = nameArray[0].Trim();
                                                string middlename = "";
                                                for (int i = 1; i < nameArray.Length - 2; i++)
                                                {
                                                    middlename += nameArray[i] + " ";
                                                }
                                                _objAadhaarDetails.Middle_Name = middlename.Trim();
                                                _objAadhaarDetails.Last_Name = nameArray[nameArray.Length - 1].Trim();
                                            }
                                        }
                                        if (printLetterBarcodeData.Dob !=null && printLetterBarcodeData.Dob != "")
                                        {
                                            string dob = printLetterBarcodeData.Dob;
                                            if (dob != "" && dob.Length == 10)
                                            {
                                                string[] dobArray = dob.Split('/');
                                                if (dobArray.Length == 3)
                                                    _objAadhaarDetails.Date_of_Birth = dobArray[2] + "/" + dobArray[1] + "/" + dobArray[0];
                                            }
                                        }
                                        _objAadhaarDetails.Gender = printLetterBarcodeData.Gender;
                                        if (printLetterBarcodeData.Co !=null && printLetterBarcodeData.Co != "")
                                        {
                                            string codetails = printLetterBarcodeData.Co.ToString();     
                                            string coPart="";
                                            if (codetails.Length > 4)
                                                coPart = codetails.Substring(0, 3);
                                            if (coPart.ToString().ToUpper() == "S/O" || coPart.ToString().ToUpper() == "D/O")
                                            {
                                                string fatherdetails = codetails.ToString().Substring(3, codetails.Length - 3).Replace(",", "").Replace(":", "").Trim();
                                                string[] fatherdetailsArray = fatherdetails.Split(' ');
                                                if (fatherdetailsArray.Length == 1)
                                                {
                                                    _objAadhaarDetails.Fathers_First_Name = fatherdetailsArray[0].ToString().Trim();
                                                    _objAadhaarDetails.Fathers_Middle_Name = "";
                                                    _objAadhaarDetails.Fathers_Last_Name = "";
                                                }
                                                else if (fatherdetailsArray.Length == 2)
                                                {
                                                    _objAadhaarDetails.Fathers_First_Name = fatherdetailsArray[0].ToString().Trim();
                                                    _objAadhaarDetails.Fathers_Middle_Name = "";
                                                    _objAadhaarDetails.Fathers_Last_Name = fatherdetailsArray[1].ToString().Trim();
                                                }
                                                else if (fatherdetailsArray.Length == 3)
                                                {
                                                    _objAadhaarDetails.Fathers_First_Name = fatherdetailsArray[0].ToString().Trim();
                                                    _objAadhaarDetails.Fathers_Middle_Name = fatherdetailsArray[1].ToString().Trim();
                                                    _objAadhaarDetails.Fathers_Last_Name = fatherdetailsArray[2].ToString().Trim();
                                                }
                                                else if (fatherdetailsArray.Length > 3)
                                                {
                                                    _objAadhaarDetails.Fathers_First_Name = fatherdetailsArray[0].ToString().Trim();
                                                    string fathermiddlename = "";
                                                    for (int i = 1; i < fatherdetailsArray.Length - 2; i++)
                                                    {
                                                        fathermiddlename += fatherdetailsArray[i] + " ";
                                                    }
                                                    _objAadhaarDetails.Fathers_Middle_Name = fathermiddlename.ToString().Trim();
                                                    _objAadhaarDetails.Fathers_Last_Name = fatherdetailsArray[fatherdetailsArray.Length - 1].ToString().Trim();
                                                }
                                            }
                                        }
                                        _objAadhaarDetails.Flat_Room_Door_BlockNo = printLetterBarcodeData.House;
                                        _objAadhaarDetails.Permises_Village = printLetterBarcodeData.Vtc;
                                        _objAadhaarDetails.Road_Street = printLetterBarcodeData.Street;
                                        _objAadhaarDetails.Area_Locality_Taluka = printLetterBarcodeData.Loc;
                                        _objAadhaarDetails.Landmark = printLetterBarcodeData.Lm;
                                        _objAadhaarDetails.Pin_Code = printLetterBarcodeData.Pc;
                                        _objAadhaarDetails.City_Town_District = printLetterBarcodeData.Dist;
                                        _objAadhaarDetails.State_Union_Teritory = printLetterBarcodeData.State;
                                        _objAadhaarDetails.CreatedMobileNo = MobileNo;
                                        _objAadhaarDetails.SubuscriberMobileNo = SubscriberMobileNo;
                                        AadharSaveResult = _objWhatsAppDAL.SetAadhaarFrontDetails(_objAadhaarDetails);
                                        imcommingLog += "Aadhar Save :" + AadharSaveResult + " " + Environment.NewLine;
                                    }
                                    catch
                                    {
                                        AadharSaveResult = "1";
                                    }
                                    dtCurrentScreen = GetScreenDetails(MobileNo, UserType, AadharSaveResult);
                                    if (dtCurrentScreen != null && dtCurrentScreen.Rows.Count > 0)
                                    {
                                        ScreenID = dtCurrentScreen.Rows[0]["CurrentScreenID"].ToString();
                                        ScreenText = dtCurrentScreen.Rows[0]["ScreenText"].ToString();
                                        SecondScreenText = dtCurrentScreen.Rows[0]["ScreenText2"].ToString();
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion
                    #region Image
                    else if (MessageType.ToUpper() == "IMAGE".ToUpper())
                    {
                        imgUrl = _objIncomingResponse.message.url.ToString();
                        imcommingLog += "Img URL :" + imgUrl + " " + Environment.NewLine;
                        if (imgUrl != "")
                        {
                            dtCurrentScreen = GetScreenDetails(MobileNo, UserType, imgUrl);
                            if (dtCurrentScreen != null && dtCurrentScreen.Rows.Count > 0)
                            {
                                ScreenID = dtCurrentScreen.Rows[0]["CurrentScreenID"].ToString();
                                SecondScreenText = dtCurrentScreen.Rows[0]["ScreenText2"].ToString();
                                imcommingLog += "Screen ID :" + ScreenID + " " + Environment.NewLine;
                                if (ScreenID == "39" || ScreenID == "40" || ScreenID == "41" || ScreenID == "42")
                                {
                                    DateTime centuryBegin = new DateTime(2001, 1, 1);
                                    DateTime currentDate = DateTime.Now;
                                    long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
                                    imagename = getImageByUrl(imgUrl, elapsedTicks.ToString(), attachment);
                                    imcommingLog += "Img Name :" + imagename + " " + Environment.NewLine;
                                    #region Img Save
                                    string imgSave = "";
                                    if (imagename != "")
                                        imgSave = "2000";
                                    else
                                        imgSave = "-2000";
                                    #endregion                                    
                                    dtCurrentScreen = GetScreenDetails(MobileNo, UserType, imgSave);
                                    if (dtCurrentScreen != null && dtCurrentScreen.Rows.Count > 0)
                                    {                                       
                                        ScreenText = dtCurrentScreen.Rows[0]["ScreenText"].ToString();
                                        SecondScreenText = dtCurrentScreen.Rows[0]["ScreenText2"].ToString();
                                    }
                                }
                            }
                            objResponse.Data = inputdetails;
                            if (imagename != "")
                                objResponse.Success = true;
                        }
                    }
                    #endregion
                }
                #region Send Whatsapp Response
                if (ScreenText != "" && RequestFrom != "")
                    result = PostSendingMessage(ScreenText.Trim(), RequestFrom.Trim());
                if (result != "" && result != "-1" && result != "0" && SecondScreenText != "" && SecondScreenText != null)
                    result = PostSendingMessage(SecondScreenText.Trim(), RequestFrom.Trim());
                #endregion
                imcommingLog += "Response :" + objResponse.Data + " " + Environment.NewLine;
                ErrorHandler.ErrorLog(imcommingLog, "WHATSAPP");
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PostIncommingMessage | Contoller Name: Whatsapp Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(imcommingLog, "WHATSAPP");
                #endregion
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
            }
        }
        
        public static string PostSendingMessage(string ScreenText, string PhoneNumber)
        {
            string sendingLog = string.Empty, jsonString = string.Empty, responseText = string.Empty, returnresponse = string.Empty;
            sendingLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PostSendingMessage " + Environment.NewLine;
            sendingLog += "ScreenText:" + ScreenText + ",PhoneNumber:" + PhoneNumber + "" + Environment.NewLine;
            ErrorHandler.ErrorLog(sendingLog, "WHATSAPP");
            WhatsappSending _objWhatsappSending = new WhatsappSending();
            try
            {
                _objWhatsappSending.scenarioKey = ConfigurationManager.AppSettings["ScenarioKey"].ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["WhatsappBaseURL"].ToString());
                WhatsApp _objWhatsApp = new WhatsApp();
                _objWhatsApp.text = ScreenText;
                _objWhatsappSending.whatsApp = _objWhatsApp;
                List<Destination> _objDestinationLst = new List<Destination>();
                Destination _objDestination = new Destination();
                PinBoxIndiaAPIV1._0.Models.To _objTo = new PinBoxIndiaAPIV1._0.Models.To();
                _objTo.phoneNumber = PhoneNumber;
                _objDestination.to = _objTo;
                _objDestinationLst.Add(_objDestination);
                _objWhatsappSending.destinations = _objDestinationLst;
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                serializerSettings.Converters.Add(new DataTableConverter());
                jsonString = JsonConvert.SerializeObject(_objWhatsappSending, Newtonsoft.Json.Formatting.None, serializerSettings);
                sendingLog += "Sending Request:" + jsonString.ToString() + " " + Environment.NewLine;

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Authorization", "Basic UGluYm94V0E6UGdhbl8wMTA0");
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) { streamWriter.Write(jsonString); streamWriter.Flush(); streamWriter.Close(); }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) { responseText = streamReader.ReadToEnd(); }
                WhatsAppSendingResponse responseResult = JsonConvert.DeserializeObject<WhatsAppSendingResponse>(responseText);
                sendingLog += "Sending Response:" + responseText + " " + Environment.NewLine;
                List<PinBoxIndiaAPIV1._0.Models.WhatsAppSendingMessageResponse.Message> _objResultMessage = responseResult.messages;
                if (_objResultMessage.Count > 0)
                {
                    foreach (var _objResponse in _objResultMessage)
                    {
                        returnresponse = _objResponse.status.groupId.ToString();
                    }
                }
                else
                {
                    returnresponse = "0";
                }

            }
            catch (WebException ex)
            {
                returnresponse = "-1";
                sendingLog += "Sending Execption:" + ex.Message + " " + Environment.NewLine;
            }
            ErrorHandler.ErrorLog(sendingLog, "WHATSAPP");
            return returnresponse;
        }
        public DataTable GetScreenDetails(string MobileNo, string UserType, string Text)
        {
            WhatsAppDAL _objWhatsAppDAL = new WhatsAppDAL();
            DataTable dtScreen = _objWhatsAppDAL.GetScreenDetails(MobileNo, UserType, Text).Tables[0];
            return dtScreen;
        }
        public string generateOTP()
        {
            var chars1 = "1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();
            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }
            var opt = new String(stringChars1);
            return opt;
        }
        public string getImageByUrl(string url, string pcode, string path)
        {
            string imcommingLog = "";
            imcommingLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: getImageByUrl " + Environment.NewLine;
            imcommingLog += "Request:" + url + Environment.NewLine;
            string filename = System.IO.Path.GetFileName(url);
            string imageType = "";
            WebResponse response = null;
            Stream remoteStream = null;
            StreamReader readStream = null;
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Headers.Add("Authorization", "Basic UGluYm94V0E6UGdhbl8wMTA0");
                if (request != null)
                {
                    response = request.GetResponse();
                    if (response != null)
                    {
                        remoteStream = response.GetResponseStream();
                        string content_type = response.Headers["Content-type"];
                        if (content_type == "image/jpeg" || content_type == "image/jpg")
                        {
                            imageType = "jpg";
                        }
                        else if (content_type == "image/png")
                        {
                            imageType = "png";
                        }
                        else if (content_type == "image/gif")
                        {
                            imageType = "gif";
                        }
                        else
                        {
                            imageType = "png";
                        }
                        string spath = HttpContext.Current.Server.MapPath(path + pcode + "." + imageType);
                        readStream = new StreamReader(remoteStream);

                        System.Drawing.Image img = System.Drawing.Image.FromStream(
                        remoteStream);
                        if (img == null)
                            return "";
                        img.Save(spath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        img.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                imcommingLog += "Error:" + ex.Message.ToString() + Environment.NewLine;
                ErrorHandler.ErrorLog(imcommingLog, "");
                return null;
            }
            finally
            {
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (readStream != null) readStream.Close();
            }
            imcommingLog += "Image Name:" + pcode + "." + imageType + Environment.NewLine;
            ErrorHandler.ErrorLog(imcommingLog, "");
            return pcode + "." + imageType;
        }
        public Object ObjectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }

            return obj;
        }

        [Route("api/whatsapp/IncommingMessageQRCode")]
        public HttpResponseMessage PostIncommingMessageQRCode(WhatsappImcomming _whatsappImcomming)
        {
            string imcommingLog = "", jsonString = "";
            imcommingLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: PostIncommingMessage " + Environment.NewLine;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new DataTableConverter());
            jsonString = JsonConvert.SerializeObject(_whatsappImcomming, Newtonsoft.Json.Formatting.None, serializerSettings);
            imcommingLog += "Request:" + jsonString.ToString() + " " + Environment.NewLine;
            Response objResponse = new Response();
            DataTable dtCurrentScreen = new DataTable();
            WhatsAppDAL _objWhatsAppDAL = new WhatsAppDAL();
            try
            {
             
                string imgUrl = string.Empty, inputdetails = string.Empty, filename = string.Empty, parsedText = string.Empty, result = string.Empty, ScreenText = string.Empty, RequestFrom = string.Empty, MobileNo = "", Text = "", MessageType = "", ScreenID = "", AadharSaveResult = "", imagename = string.Empty; ;
                string attachment = ConfigurationManager.AppSettings["WhatsappFilePath"].ToString();
                List<Result> _objResult = _whatsappImcomming.results;
                foreach (var _objIncomingResponse in _objResult)
                {
                    RequestFrom = _objIncomingResponse.from.ToString();
                    MobileNo = RequestFrom.Replace("91", "");
                    MessageType = _objIncomingResponse.message.type.ToString();
                    #region Text
                    if (MessageType.ToUpper() == "TEXT".ToUpper())
                    {
                        if (ConfigurationManager.AppSettings["AppInstance"].ToString().ToUpper() == "DEV")
                            Text = _objIncomingResponse.message.text.ToString().Substring(7, _objIncomingResponse.message.text.ToString().Length - 7);
                        if (Text != "")
                        {                      
                            #region QRCode                         
                            ScreenText = "";

                            PrintLetterBarcodeData printLetterBarcodeData = new PrintLetterBarcodeData();
                            try
                            {
                                printLetterBarcodeData = (PrintLetterBarcodeData)(ObjectToXML(Text.Replace("</?xml", "<?xml"), typeof(PrintLetterBarcodeData)));
                                AadhaarDetails _objAadhaarDetails = new AadhaarDetails();
                                string Aadhar = printLetterBarcodeData.Uid.ToString();
                                Int64 AadhaarNo;
                                if (Aadhar.ToString().Trim().Length == 12 && Int64.TryParse(Aadhar.ToString().Trim(), out AadhaarNo) == true)
                                    _objAadhaarDetails.AadhaarNumber = AadhaarNo.ToString();

                                if (printLetterBarcodeData.Name !=null && printLetterBarcodeData.Name != "")
                                {
                                    string name = printLetterBarcodeData.Name.ToString().Trim();
                                    string[] nameArray = name.Split(' ');
                                    if (nameArray.Length == 1)
                                    {
                                        _objAadhaarDetails.First_Name = nameArray[0].Trim();
                                        _objAadhaarDetails.Middle_Name = "";
                                        _objAadhaarDetails.Last_Name = "";
                                    }
                                    else if (nameArray.Length == 2)
                                    {
                                        _objAadhaarDetails.First_Name = nameArray[0].Trim();
                                        _objAadhaarDetails.Middle_Name = "";
                                        _objAadhaarDetails.Last_Name = nameArray[1].Trim();
                                    }
                                    else if (nameArray.Length == 3)
                                    {
                                        _objAadhaarDetails.First_Name = nameArray[0].Trim();
                                        _objAadhaarDetails.Middle_Name = nameArray[1].Trim();
                                        _objAadhaarDetails.Last_Name = nameArray[2].Trim();
                                    }
                                    else if (nameArray.Length > 3)
                                    {
                                        _objAadhaarDetails.First_Name = nameArray[0].Trim();
                                        string middlename = "";
                                        for (int i = 1; i < nameArray.Length - 2; i++)
                                        {
                                            middlename += nameArray[i] + " ";
                                        }
                                        _objAadhaarDetails.Middle_Name = middlename.Trim();
                                        _objAadhaarDetails.Last_Name = nameArray[nameArray.Length - 1].Trim();
                                    }
                                }
                                if (printLetterBarcodeData.Dob !=null && printLetterBarcodeData.Dob != "")
                                {
                                    string dob = printLetterBarcodeData.Dob;
                                    if (dob != "" && dob.Length == 10)
                                    {
                                        string[] dobArray = dob.Split('/');
                                        if (dobArray.Length == 3)
                                            _objAadhaarDetails.Date_of_Birth = dobArray[2] + "/" + dobArray[1] + "/" + dobArray[0];
                                    }
                                }
                                _objAadhaarDetails.Gender = printLetterBarcodeData.Gender;
                                if (printLetterBarcodeData.Co !=null && printLetterBarcodeData.Co != "")
                                {
                                    string codetails = printLetterBarcodeData.Co.ToString();
                                    string coPart = "";
                                    if (codetails.Length > 4)
                                        coPart = codetails.Substring(0, 3);
                                    if (coPart.ToString().ToUpper() == "S/O" || coPart.ToString().ToUpper() == "D/O")
                                    {
                                        string fatherdetails = codetails.ToString().Substring(3, codetails.Length - 3).Replace(",", "").Replace(":", "").Trim();
                                        string[] fatherdetailsArray = fatherdetails.Split(' ');
                                        if (fatherdetailsArray.Length == 1)
                                        {
                                            _objAadhaarDetails.Fathers_First_Name = fatherdetailsArray[0].ToString().Trim();
                                            _objAadhaarDetails.Fathers_Middle_Name = "";
                                            _objAadhaarDetails.Fathers_Last_Name = "";
                                        }
                                        else if (fatherdetailsArray.Length == 2)
                                        {
                                            _objAadhaarDetails.Fathers_First_Name = fatherdetailsArray[0].ToString().Trim();
                                            _objAadhaarDetails.Fathers_Middle_Name = "";
                                            _objAadhaarDetails.Fathers_Last_Name = fatherdetailsArray[1].ToString().Trim();
                                        }
                                        else if (fatherdetailsArray.Length == 3)
                                        {
                                            _objAadhaarDetails.Fathers_First_Name = fatherdetailsArray[0].ToString().Trim();
                                            _objAadhaarDetails.Fathers_Middle_Name = fatherdetailsArray[1].ToString().Trim();
                                            _objAadhaarDetails.Fathers_Last_Name = fatherdetailsArray[2].ToString().Trim();
                                        }
                                        else if (fatherdetailsArray.Length > 3)
                                        {
                                            _objAadhaarDetails.Fathers_First_Name = fatherdetailsArray[0].ToString().Trim();
                                            string fathermiddlename = "";
                                            for (int i = 1; i < fatherdetailsArray.Length - 2; i++)
                                            {
                                                fathermiddlename += fatherdetailsArray[i] + " ";
                                            }
                                            _objAadhaarDetails.Fathers_Middle_Name = fathermiddlename.ToString().Trim();
                                            _objAadhaarDetails.Fathers_Last_Name = fatherdetailsArray[fatherdetailsArray.Length - 1].ToString().Trim();
                                        }
                                    }
                                }
                                _objAadhaarDetails.Flat_Room_Door_BlockNo = printLetterBarcodeData.House;
                                _objAadhaarDetails.Permises_Village = printLetterBarcodeData.Vtc;
                                _objAadhaarDetails.Road_Street = printLetterBarcodeData.Street;
                                _objAadhaarDetails.Area_Locality_Taluka = printLetterBarcodeData.Loc;
                                _objAadhaarDetails.Landmark = printLetterBarcodeData.Lm;
                                _objAadhaarDetails.Pin_Code = printLetterBarcodeData.Pc;
                                _objAadhaarDetails.City_Town_District = printLetterBarcodeData.Dist;
                                _objAadhaarDetails.State_Union_Teritory = printLetterBarcodeData.State;
                                _objAadhaarDetails.CreatedMobileNo = MobileNo;
                                _objAadhaarDetails.SubuscriberMobileNo = "9868417129";
                                AadharSaveResult = _objWhatsAppDAL.SetAadhaarFrontDetails(_objAadhaarDetails);
                                imcommingLog += "Aadhar Save :" + AadharSaveResult + " " + Environment.NewLine;
                            }
                            catch
                            {
                                AadharSaveResult = "1";
                            }


                            #endregion
                        }
                    }
                }
                    #endregion
            }
            catch (Exception ex)
            {

            }
         return Request.CreateResponse(HttpStatusCode.OK, objResponse);
        }
    }
}
