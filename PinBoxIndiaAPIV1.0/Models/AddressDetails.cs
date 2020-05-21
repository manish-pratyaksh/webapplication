using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    public class Address
    {
        public string CreatedBy { get; set; }    
        public List<AddressDetails> addressDetails { get; set; }  

    }
    public class AddressDetails
    {
        public string SNo { get; set; }
        public string AddressCode { get; set; }
        public string Address_Type { get; set; }
        public string Flat_Room_Door_Block_No { get; set; }
        public string Premises_Village { get; set; }
        public string Road_Street { get; set; }
        public string Area_Locality_Taluka { get; set; }
        public string Landmark { get; set; }
        public string Pin_Code { get; set; }
        public string City_Town_District { get; set; }
        public string State_Union_Teritory { get; set; }
        public string Country { get; set; }
        public string Landline_With_STD_Code { get; set; }
        public string Address_Proof { get; set; }
        public string Document_Proof_ID { get; set; }
      
    }
}