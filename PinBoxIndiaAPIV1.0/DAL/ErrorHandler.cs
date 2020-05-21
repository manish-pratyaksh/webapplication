using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace PinBoxIndiaAPIV1._0.DAL
{
    public class ErrorHandler
    {
        public static void ErrorLog(string errorMessage, string FileName = "")
        {
            try
            {
                string serverPath = "";
                string fileName = "";

                if (FileName.ToLower() == "WHATSAPP".ToLower())
                {
                    serverPath = HostingEnvironment.ApplicationPhysicalPath + "Content\\WhatsappRequestLog";
                    if (!Directory.Exists(serverPath)) { Directory.CreateDirectory(serverPath); }
                    fileName = "WhatsappRequestLog" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".txt";
                }
                else if (FileName.ToLower() == "WEBREG".ToLower())
                {
                    serverPath = HostingEnvironment.ApplicationPhysicalPath + "Content\\WebReg";
                    if (!Directory.Exists(serverPath)) { Directory.CreateDirectory(serverPath); }
                    fileName = "WebRegLog" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".txt";
                }
                else
                {
                    serverPath = HostingEnvironment.ApplicationPhysicalPath + "Content\\Log";
                    if (!Directory.Exists(serverPath)) { Directory.CreateDirectory(serverPath); }
                    fileName = "log" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".txt";
                }
                string filePath = serverPath + "\\" + fileName;
                if ((!File.Exists(filePath))) { File.Create(filePath).Close(); }
                using (StreamWriter w = File.AppendText(filePath))
                {
                    w.WriteLine(errorMessage);
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex) { }
        }
    }
}