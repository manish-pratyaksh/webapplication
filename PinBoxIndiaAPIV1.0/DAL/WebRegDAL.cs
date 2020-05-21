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
    public class WebRegDAL
    {
        public StringBuilder SetPersonalDetails(PersonalDetails _personalDetails)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@Category",String.IsNullOrEmpty(_personalDetails.Category)?DBNull.Value:(object)_personalDetails.Category ), 
                    new SqlParameter("@NRI_Flag",String.IsNullOrEmpty(_personalDetails.NRI_Flag)?DBNull.Value:(object)_personalDetails.NRI_Flag ),
                    new SqlParameter("@PRAN",String.IsNullOrEmpty(_personalDetails.PRAN)?DBNull.Value:(object)_personalDetails.PRAN ),
                    new SqlParameter("@Title",String.IsNullOrEmpty(_personalDetails.Title)?DBNull.Value:(object)_personalDetails.Title ),
                    new SqlParameter("@First_Name",String.IsNullOrEmpty(_personalDetails.First_Name)?DBNull.Value:(object)_personalDetails.First_Name ),
                    new SqlParameter("@Middle_Name",String.IsNullOrEmpty(_personalDetails.Middle_Name)?DBNull.Value:(object)_personalDetails.Middle_Name ),
                    new SqlParameter("@Last_Name",String.IsNullOrEmpty(_personalDetails.Last_Name)?DBNull.Value:(object)_personalDetails.Last_Name ),
                    new SqlParameter("@Subscribers_Maiden_Name",String.IsNullOrEmpty(_personalDetails.Subscribers_Maiden_Name)?DBNull.Value:(object)_personalDetails.Subscribers_Maiden_Name ),
                    new SqlParameter("@Date_of_Birth",String.IsNullOrEmpty(_personalDetails.Date_of_Birth)?DBNull.Value:(object)_personalDetails.Date_of_Birth ),
                    new SqlParameter("@City_of_Birth",String.IsNullOrEmpty(_personalDetails.City_of_Birth)?DBNull.Value:(object)_personalDetails.City_of_Birth ),
                    new SqlParameter("@Country_of_Birth",String.IsNullOrEmpty(_personalDetails.Country_of_Birth)?DBNull.Value:(object)_personalDetails.Country_of_Birth ),
                    new SqlParameter("@Gender",String.IsNullOrEmpty(_personalDetails.Gender)?DBNull.Value:(object)_personalDetails.Gender ),
                    new SqlParameter("@Name_to_be_Printed_on_PRAN_Card",String.IsNullOrEmpty(_personalDetails.Name_to_be_Printed_on_PRAN_Card)?DBNull.Value:(object)_personalDetails.Name_to_be_Printed_on_PRAN_Card ),
                    new SqlParameter("@Fathers_First_Name",String.IsNullOrEmpty(_personalDetails.Fathers_First_Name)?DBNull.Value:(object)_personalDetails.Fathers_First_Name ),
                    new SqlParameter("@Fathers_Middle_Name",String.IsNullOrEmpty(_personalDetails.Fathers_Middle_Name)?DBNull.Value:(object)_personalDetails.Fathers_Middle_Name ),
                    new SqlParameter("@Fathers_Last_Name",String.IsNullOrEmpty(_personalDetails.Fathers_Last_Name)?DBNull.Value:(object)_personalDetails.Fathers_Last_Name ),
                    new SqlParameter("@Mothers_First_Name",String.IsNullOrEmpty(_personalDetails.Mothers_First_Name)?DBNull.Value:(object)_personalDetails.Mothers_First_Name ),
                    new SqlParameter("@Mothers_Middle_Name",String.IsNullOrEmpty(_personalDetails.Mothers_Middle_Name)?DBNull.Value:(object)_personalDetails.Mothers_Middle_Name ),
                    new SqlParameter("@Mothers_Last_Name",String.IsNullOrEmpty(_personalDetails.Mothers_Last_Name)?DBNull.Value:(object)_personalDetails.Mothers_Last_Name ),
                    new SqlParameter("@Marital_Status",String.IsNullOrEmpty(_personalDetails.Marital_Status)?DBNull.Value:(object)_personalDetails.Marital_Status ),
                    new SqlParameter("@Spouse_First_Name",String.IsNullOrEmpty(_personalDetails.Spouse_First_Name)?DBNull.Value:(object)_personalDetails.Spouse_First_Name ),
                    new SqlParameter("@Spouse_Middle_Name",String.IsNullOrEmpty(_personalDetails.Spouse_Middle_Name)?DBNull.Value:(object)_personalDetails.Spouse_Middle_Name ),
                    new SqlParameter("@Spouse_Last_Name",String.IsNullOrEmpty(_personalDetails.Spouse_Last_Name)?DBNull.Value:(object)_personalDetails.Spouse_Last_Name ),
                    new SqlParameter("@Mobile_Number",String.IsNullOrEmpty(_personalDetails.Mobile_Number)?DBNull.Value:(object)_personalDetails.Mobile_Number ),
                    new SqlParameter("@Email_ID",String.IsNullOrEmpty(_personalDetails.Email_ID)?DBNull.Value:(object)_personalDetails.Email_ID ),
                    new SqlParameter("@PAN",String.IsNullOrEmpty(_personalDetails.PAN)?DBNull.Value:(object)_personalDetails.PAN ),
                    new SqlParameter("@Aadhaar",String.IsNullOrEmpty(_personalDetails.Aadhaar)?DBNull.Value:(object)_personalDetails.Aadhaar ),
                    new SqlParameter("@Communication_Address",String.IsNullOrEmpty(_personalDetails.Communication_Address)?DBNull.Value:(object)_personalDetails.Communication_Address ),
                    new SqlParameter("@Reg_ReceiptNumber",String.IsNullOrEmpty(_personalDetails.Reg_ReceiptNumber)?DBNull.Value:(object)_personalDetails.Reg_ReceiptNumber ),
                    new SqlParameter("@ProofOfIdentityNumber",String.IsNullOrEmpty(_personalDetails.ProofOfIdentityNumber)?DBNull.Value:(object)_personalDetails.ProofOfIdentityNumber ),
                    new SqlParameter("@T1Scheme_Pref",String.IsNullOrEmpty(_personalDetails.T1Scheme_Pref)?DBNull.Value:(object)_personalDetails.T1Scheme_Pref ),
                    new SqlParameter("@CreatedBy",String.IsNullOrEmpty(_personalDetails.CreatedBy)?DBNull.Value:(object)_personalDetails.CreatedBy ),
                    
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SetPersonalDetails, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public StringBuilder SetAddressDetails(Address _address)
        {
            try
            {
                DataTable dtaddress = new DataTable();
                dtaddress.Columns.Add("AddressCode", typeof(System.String));
                dtaddress.Columns.Add("Address_Type", typeof(System.String));
                dtaddress.Columns.Add("Flat_Room_Door_Block_No", typeof(System.String));
                dtaddress.Columns.Add("Premises_Village", typeof(System.String));
                dtaddress.Columns.Add("Road_Street", typeof(System.String));
                dtaddress.Columns.Add("Area_Locality_Taluka", typeof(System.String));
                dtaddress.Columns.Add("Landmark", typeof(System.String));
                dtaddress.Columns.Add("Pin_Code", typeof(System.String));
                dtaddress.Columns.Add("City_Town_District", typeof(System.String));
                dtaddress.Columns.Add("State_Union_Teritory", typeof(System.String));
                dtaddress.Columns.Add("Country", typeof(System.String));
                dtaddress.Columns.Add("Landline_With_STD_Code", typeof(System.String));
                dtaddress.Columns.Add("Address_Proof", typeof(System.String));
                dtaddress.Columns.Add("Document_Proof_ID", typeof(System.String));
                List<AddressDetails> lstaddtressdetails = _address.addressDetails;
                foreach (var lst in lstaddtressdetails)
                {
                    DataRow drBatch = dtaddress.NewRow();
                    drBatch["AddressCode"] = lst.AddressCode;
                    drBatch["Address_Type"] = lst.Address_Type;
                    drBatch["Flat_Room_Door_Block_No"] = lst.Flat_Room_Door_Block_No;
                    drBatch["Premises_Village"] = lst.Premises_Village;
                    drBatch["Road_Street"] = lst.Road_Street;
                    drBatch["Area_Locality_Taluka"] = lst.Area_Locality_Taluka;
                    drBatch["Landmark"] = lst.Landmark;
                    drBatch["Pin_Code"] = lst.Pin_Code;
                    drBatch["City_Town_District"] = lst.City_Town_District;
                    drBatch["State_Union_Teritory"] = lst.State_Union_Teritory;
                    drBatch["Country"] = lst.Country;
                    drBatch["Landline_With_STD_Code"] = lst.Landline_With_STD_Code;
                    drBatch["Address_Proof"] = lst.Address_Proof;
                    drBatch["Document_Proof_ID"] = lst.Document_Proof_ID;
                    dtaddress.Rows.Add(drBatch);
                    dtaddress.AcceptChanges();
                }
                SqlParameter[] P = new SqlParameter[]{                   
                    new SqlParameter("@CreatedBy",String.IsNullOrEmpty(_address.CreatedBy)?DBNull.Value:(object)_address.CreatedBy ),
                    new SqlParameter("@tblAddDet",dtaddress ){SqlDbType=SqlDbType.Structured}
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SetAddressDetails, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public StringBuilder SetBankDetails(BankDetails _bankDetails)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@UserInfoID",String.IsNullOrEmpty(_bankDetails.UserInfoID)?DBNull.Value:(object)_bankDetails.UserInfoID ), 
                    new SqlParameter("@CreatedBy",String.IsNullOrEmpty(_bankDetails.CreatedBy)?DBNull.Value:(object)_bankDetails.CreatedBy ),
                    new SqlParameter("@Account_Type",String.IsNullOrEmpty(_bankDetails.Account_Type)?DBNull.Value:(object)_bankDetails.Account_Type ),
                    new SqlParameter("@Bank_Account_Type",String.IsNullOrEmpty(_bankDetails.Bank_Account_Type)?DBNull.Value:(object)_bankDetails.Bank_Account_Type ),
                    new SqlParameter("@Bank_Account_Number",String.IsNullOrEmpty(_bankDetails.Bank_Account_Number)?DBNull.Value:(object)_bankDetails.Bank_Account_Number ),
                    new SqlParameter("@Bank_IFS_Code",String.IsNullOrEmpty(_bankDetails.Bank_IFS_Code)?DBNull.Value:(object)_bankDetails.Bank_IFS_Code ),
                    new SqlParameter("@Bank_Name",String.IsNullOrEmpty(_bankDetails.Bank_Name)?DBNull.Value:(object)_bankDetails.Bank_Name ),
                    new SqlParameter("@Branch_Address",String.IsNullOrEmpty(_bankDetails.Branch_Address)?DBNull.Value:(object)_bankDetails.Branch_Address ),
                    new SqlParameter("@Pin_Code",String.IsNullOrEmpty(_bankDetails.Pin_Code)?DBNull.Value:(object)_bankDetails.Pin_Code ),
                    new SqlParameter("@City_Town_District",String.IsNullOrEmpty(_bankDetails.City_Town_District)?DBNull.Value:(object)_bankDetails.City_Town_District ),
                    new SqlParameter("@State_UT",String.IsNullOrEmpty(_bankDetails.State_UT)?DBNull.Value:(object)_bankDetails.State_UT ),
                    new SqlParameter("@Country",String.IsNullOrEmpty(_bankDetails.Country)?DBNull.Value:(object)_bankDetails.Country ),
                    new SqlParameter("@Bank_MICR_Code",String.IsNullOrEmpty(_bankDetails.Bank_MICR_Code)?DBNull.Value:(object)_bankDetails.Bank_MICR_Code ),
                    new SqlParameter("@Bank_A_C_Linked_to_Aadhaar",String.IsNullOrEmpty(_bankDetails.Bank_A_C_Linked_to_Aadhaar)?DBNull.Value:(object)_bankDetails.Bank_A_C_Linked_to_Aadhaar ),                    
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SetBankDetails, P);
                return jsonResult;

            }
            catch { throw; }
        }
    }
}