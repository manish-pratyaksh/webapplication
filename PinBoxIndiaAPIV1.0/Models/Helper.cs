using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public List<Error> ErrorList { get; set; }

        public T CastValue<T>()
        {
            return (T)System.Convert.ChangeType(Data, typeof(T));
        }
    }
    public class Error
    {
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
    }
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}