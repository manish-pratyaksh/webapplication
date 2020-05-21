using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PinBoxIndiaAPIV1._0.Models
{
    public class Whatsapp
    {
    }
    public class WhatsappImcomming
    {
        public List<Result> results { get; set; }
        public string messageCount { get; set; }
        public string pendingMessageCount { get; set; }
    }
    public class WhatsappSending
    {
        public string scenarioKey { get; set; }
        public List<Destination> destinations { get; set; }
        public WhatsApp whatsApp { get; set; }
        public Sms sms { get; set; }
    }
    public class Result
    {
        public string from { get; set; }
        public string to { get; set; }
        public string integrationType { get; set; }
        public string receivedAt { get; set; }
        public string messageId { get; set; }
        public string pairedMessageId { get; set; }
        public string callbackData { get; set; }
        public Message message { get; set; }
        //public Contact contact { get; set; }
        public Price price { get; set; }
    }
    public class Message
    {
        public string type { get; set; }
        public string caption { get; set; }
        public string url { get; set; }
        public string text { get; set; }
        //public string longitude { get; set; }
        //public string latitude { get; set; }
        //public List<Contacts> contacts { get; set; }

    }
    public class Contact
    {
        public string name { get; set; }
    }
    public class Price
    {
        public string pricePerMessage { get; set; }
        public string currency { get; set; }
    }  
    public class To
    {
        public string phoneNumber { get; set; }
    }
    public class Destination
    {
        public To to { get; set; }
    }
    public class WhatsApp
    {
        public string text { get; set; }
        public string imageUrl { get; set; }
    }
    public class Sms
    {
        public string text { get; set; }
        public int validityPeriod { get; set; }
    }
    public class AadhaarDetails
    {
        public string SubuscriberMobileNo { get; set; }
        public string AadhaarNumber { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Date_of_Birth { get; set; }
        public string Gender { get; set; }
        public string Fathers_First_Name { get; set; }
        public string Fathers_Middle_Name { get; set; }
        public string Fathers_Last_Name { get; set; }
        public string Flat_Room_Door_BlockNo { get; set; }
        public string Permises_Village { get; set; }
        public string Road_Street { get; set; }
        public string Area_Locality_Taluka { get; set; }
        public string Landmark { get; set; }
        public string Pin_Code { get; set; }
        public string City_Town_District { get; set; }
        public string State_Union_Teritory { get; set; }
        public string CreatedMobileNo { get; set; }

    }
    namespace WhatsAppSendingMessageResponse
    {
        public class WhatsAppSendingResponse
        {
            public List<Message> messages { get; set; }
        }
        public class To
        {
            public string phoneNumber { get; set; }
        }
        public class Status
        {
            public int groupId { get; set; }
            public string groupName { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }
        public class Message
        {
            public To to { get; set; }
            public Status status { get; set; }
            public string messageId { get; set; }
        }
    }
    [Serializable()]
    [XmlRoot(ElementName = "PrintLetterBarcodeData")]
    public class PrintLetterBarcodeData
    {
        [XmlAttribute(AttributeName = "uid")]
        public string Uid { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "gender")]
        public string Gender { get; set; }
        [XmlAttribute(AttributeName = "yob")]
        public string Yob { get; set; }
        [XmlAttribute(AttributeName = "co")]
        public string Co { get; set; }
        [XmlAttribute(AttributeName = "house")]
        public string House { get; set; }
        [XmlAttribute(AttributeName = "street")]
        public string Street { get; set; }
        [XmlAttribute(AttributeName = "lm")]
        public string Lm { get; set; }
        [XmlAttribute(AttributeName = "loc")]
        public string Loc { get; set; }
        [XmlAttribute(AttributeName = "vtc")]
        public string Vtc { get; set; }
        [XmlAttribute(AttributeName = "po")]
        public string Po { get; set; }
        [XmlAttribute(AttributeName = "dist")]
        public string Dist { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "pc")]
        public string Pc { get; set; }
        [XmlAttribute(AttributeName = "dob")]
        public string Dob { get; set; }

        [XmlAttribute(AttributeName = "gname")]
        public string gname { get; set; }

    }

}