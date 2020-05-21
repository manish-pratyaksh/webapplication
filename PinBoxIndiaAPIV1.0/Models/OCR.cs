using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    public class OCR
    {
    }
    public class OCRRequest
    {
        public string language { get; set; }
        public string isOverlayRequired { get; set; }
        public string base64Image { get; set; }
        public string iscreatesearchablepdf { get; set; }
        public string issearchablepdfhidetextlayer { get; set; }
    }
    public class OCRResponse
    {
        public List<ParsedResult> ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ProcessingTimeInMilliseconds { get; set; }
        public string SearchablePDFURL { get; set; }
    }
    public class TextOverlay
    {
        public List<object> Lines { get; set; }
        public bool HasOverlay { get; set; }
        public string Message { get; set; }
    }
    public class ParsedResult
    {
        public TextOverlay TextOverlay { get; set; }
        public string TextOrientation { get; set; }
        public int FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
}