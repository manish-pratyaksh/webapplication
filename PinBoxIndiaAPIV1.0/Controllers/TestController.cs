using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PinBoxIndiaAPIV1._0.DAL;
using PinBoxIndiaAPIV1._0.Models;
using System;
using System.Collections.Generic;
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
    public class TestController : WhatsAppController
    {
        #region Images64
        [Route("api/Test/TestImageToBase64")]
        public HttpResponseMessage GetTestImageToBase64()
        {
            Response objResponse = new Response();
            try
            {
                string imcommingLog = "";
                imcommingLog += Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: GetTestImageToBase64 " + Environment.NewLine;

                string ImagePagth = "/content/WhatsAppFile/IMG_20200213_151436.jpg";
                String path = HostingEnvironment.MapPath(ImagePagth);
                string bit64imagepath = ImageToBase64(path);
                objResponse.Data = bit64imagepath;
                imcommingLog = "Path:" + ImagePagth + " |bit64image:" + bit64imagepath;
                ErrorHandler.ErrorLog(imcommingLog, "WHATSAPP");
                if (bit64imagepath != "")
                    objResponse.Success = true;
                return Request.CreateResponse(HttpStatusCode.OK, objResponse);
            }
            catch (Exception ex)
            {
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: TestImageToBase64 | Contoller Name: Test Controller" + Environment.NewLine;
                logMsg += ":" + DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion set exception response
                objResponse.Success = false;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, objResponse);
                return response;
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
                #region set exception response
                string logMsg = Environment.NewLine + DateTime.Now + Environment.NewLine + "Method Name: ImageToBase64 | Contoller Name: Test Controller " + Environment.NewLine;
                logMsg += DateTime.Now + "|" + ex.ToString() + "-" + Environment.NewLine;
                ErrorHandler.ErrorLog(logMsg);
                #endregion
            }
            return _base64String;
        }
        #endregion  
    }
}
