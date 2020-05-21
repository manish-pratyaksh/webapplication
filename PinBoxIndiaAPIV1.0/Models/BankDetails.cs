using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    #region Bank Details
    public class BankDetails
    {
        public string SNo { get; set; }
        public string UserInfoID { get; set; }
        public string Account_Type { get; set; }
        public string Bank_Account_Type { get; set; }
        public string Bank_Account_Number { get; set; }
        public string Bank_IFS_Code { get; set; }
        public string Bank_Name { get; set; }
        public string Branch_Address { get; set; }
        public string Pin_Code { get; set; }
        public string City_Town_District { get; set; }
        public string State_UT { get; set; }
        public string Country { get; set; }
        public string Bank_MICR_Code { get; set; }
        public string Bank_A_C_Linked_to_Aadhaar { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Deleted { get; set; }
    }
    #endregion
  
}