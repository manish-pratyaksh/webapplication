using PinBoxIndiaAPIV1._0.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PinBoxIndiaAPIV1._0.DAL
{
    public class WhatsAppDAL
    {
        public DataSet GetScreenDetails(string MobileNo, string UserType, string Text)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@MobileNo", MobileNo),new SqlParameter("@UserCatID", UserType),
                    new SqlParameter("@Text", Text),
                };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.GetScreenDetails, P);
                return dsResult;
            }
            catch { throw; }
        }
        public string SetSubscriberOTP(string MobileNo, string OTP)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@SubsMobileNo",MobileNo),
                    new SqlParameter("@OTPNo",OTP),
               };
                string jsonResult = "";
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SetSubscriberOTP, P).ToString();
                return jsonResult;

            }
            catch { throw; }
        }
        public string SetAadhaarFrontDetails(AadhaarDetails _aadhaarDetails)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@SubuscriberMobileNo",String.IsNullOrEmpty(_aadhaarDetails.SubuscriberMobileNo)?DBNull.Value:(object)_aadhaarDetails.SubuscriberMobileNo.Trim() ),
                    new SqlParameter("@AadhaarNumber",String.IsNullOrEmpty(_aadhaarDetails.AadhaarNumber)?DBNull.Value:(object)_aadhaarDetails.AadhaarNumber.Trim() ),
                    new SqlParameter("@First_Name",String.IsNullOrEmpty(_aadhaarDetails.First_Name)?DBNull.Value:(object)_aadhaarDetails.First_Name.Trim() ),
                    new SqlParameter("@Middle_Name",String.IsNullOrEmpty(_aadhaarDetails.Middle_Name)?DBNull.Value:(object)_aadhaarDetails.Middle_Name.Trim() ),
                    new SqlParameter("@Last_Name",String.IsNullOrEmpty(_aadhaarDetails.Last_Name)?DBNull.Value:(object)_aadhaarDetails.Last_Name.Trim() ),
                    new SqlParameter("@Date_of_Birth",String.IsNullOrEmpty(_aadhaarDetails.Date_of_Birth)?DBNull.Value:(object)_aadhaarDetails.Date_of_Birth.Trim() ),
                    new SqlParameter("@Gender",String.IsNullOrEmpty(_aadhaarDetails.Gender)?DBNull.Value:(object)_aadhaarDetails.Gender.Trim() ), 
                    new SqlParameter("@Fathers_First_Name",String.IsNullOrEmpty(_aadhaarDetails.Fathers_First_Name)?DBNull.Value:(object)_aadhaarDetails.Fathers_First_Name.Trim() ), 
                    new SqlParameter("@Fathers_Middle_Name",String.IsNullOrEmpty(_aadhaarDetails.Fathers_Middle_Name)?DBNull.Value:(object)_aadhaarDetails.Fathers_Middle_Name.Trim() ), 
                    new SqlParameter("@Fathers_Last_Name",String.IsNullOrEmpty(_aadhaarDetails.Fathers_Last_Name)?DBNull.Value:(object)_aadhaarDetails.Fathers_Last_Name.Trim() ), 
                    new SqlParameter("@Flat_Room_Door_BlockNo",String.IsNullOrEmpty(_aadhaarDetails.Flat_Room_Door_BlockNo)?DBNull.Value:(object)_aadhaarDetails.Flat_Room_Door_BlockNo.Trim() ), 
                    new SqlParameter("@Permises_Village",String.IsNullOrEmpty(_aadhaarDetails.Permises_Village)?DBNull.Value:(object)_aadhaarDetails.Permises_Village.Trim() ), 
                    new SqlParameter("@Road_Street",String.IsNullOrEmpty(_aadhaarDetails.Road_Street)?DBNull.Value:(object)_aadhaarDetails.Road_Street.Trim() ), 
                    new SqlParameter("@Area_Locality_Taluka",String.IsNullOrEmpty(_aadhaarDetails.Area_Locality_Taluka)?DBNull.Value:(object)_aadhaarDetails.Area_Locality_Taluka.Trim() ), 
                    new SqlParameter("@Landmark",String.IsNullOrEmpty(_aadhaarDetails.Landmark)?DBNull.Value:(object)_aadhaarDetails.Landmark.Trim() ), 
                    new SqlParameter("@Pin_Code",String.IsNullOrEmpty(_aadhaarDetails.Pin_Code)?DBNull.Value:(object)_aadhaarDetails.Pin_Code.Trim() ), 
                    new SqlParameter("@City_Town_District",String.IsNullOrEmpty(_aadhaarDetails.City_Town_District)?DBNull.Value:(object)_aadhaarDetails.City_Town_District.Trim() ), 
                    new SqlParameter("@State_Union_Teritory",String.IsNullOrEmpty(_aadhaarDetails.State_Union_Teritory)?DBNull.Value:(object)_aadhaarDetails.State_Union_Teritory.Trim() ), 
                    new SqlParameter("@CreatorMobileNo",String.IsNullOrEmpty(_aadhaarDetails.CreatedMobileNo)?DBNull.Value:(object)_aadhaarDetails.CreatedMobileNo.Trim() ),                     
                };
                string jsonResult = "";
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SetAadharFrontDetails, P).ToString();
                return jsonResult;
            }
            catch { throw; }
        }
        //public string SetAadhaarBackDetails(AadhaarDetails _aadhaarDetails)
        //{
        //    try
        //    {
        //        SqlParameter[] P = new SqlParameter[]{ 
        //            new SqlParameter("@SubuscriberMobileNo",_aadhaarDetails.SubuscriberMobileNo),
        //            new SqlParameter("@Fathers_First_Name",_aadhaarDetails.Fathers_First_Name),
        //            new SqlParameter("@Fathers_Middle_Name",_aadhaarDetails.Fathers_Middle_Name),
        //            new SqlParameter("@Fathers_Last_Name",_aadhaarDetails.Fathers_Last_Name),
        //            new SqlParameter("@Flat_Room_Door_BlockNo",_aadhaarDetails.Flat_Room_Door_BlockNo),
        //            new SqlParameter("@Permises_Village",_aadhaarDetails.Permises_Village),
        //            new SqlParameter("@Road_Street",_aadhaarDetails.Road_Street), 
        //            new SqlParameter("@Area_Locality_Taluka",_aadhaarDetails.Area_Locality_Taluka), 
        //            new SqlParameter("@Landmark",_aadhaarDetails.Landmark), 
        //            new SqlParameter("@Pin_Code",_aadhaarDetails.Pin_Code), 
        //            new SqlParameter("@City_Town_District",_aadhaarDetails.City_Town_District), 
        //            new SqlParameter("@State_Union_Teritory",_aadhaarDetails.State_Union_Teritory), 
        //            new SqlParameter("@updatorMobileNo",_aadhaarDetails.CreatedMobileNo), 
        //        };
        //        string jsonResult = "";
        //        jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SetAadharBackDetails, P).ToString();
        //        return jsonResult;
        //    }
        //    catch { throw; }
        //}
    }

}