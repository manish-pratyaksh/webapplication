using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    #region Common Function
    public class common
    {
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            List<Dictionary<string, object>>
            lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in dtData.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in dtData.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        }
    }
    #endregion 
    #region Master state city

    public class mststate
    {
        public string stateid { get; set; }
        public List<state> statelist { get; set; }

    }
    public class state
    {
        public string stateid { get; set; }
        public string statename { get; set; }

    }
    public class mstcity
    {
        public string cityid { get; set; }
        public string stateid { get; set; }
        public List<city> citylist { get; set; }

    }
    public class city
    {
        public string cityid { get; set; }
        public string cityname { get; set; }

    }
    #endregion

}