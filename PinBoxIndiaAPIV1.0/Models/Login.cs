using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    #region User Login Details
    public class Login
    {
    }
    public class UserLogin
    {
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Otpbased { get; set; }

    }
    public class MultipleUserLogin
    {
        public string UserCount { get; set; }
        public string LoginBlockNo { get; set; }
        public List<UserLoginResponse> userLoginResponse = new List<UserLoginResponse>();
    }
    public class UserLoginResponse
    {
        public string UserInfoID { get; set; }
        public string FirstName { get; set; }
        public string PRANID { get; set; }
        public string MobileNo { get; set; }
        public string Age { get; set; }
        public string UserCategoryID { get; set; }
        public string CreatedDate { get; set; }
        public string OtherUserInfoID { get; set; }
        public string TranDisable { get; set; }
        public string ProfileImage { get; set; }
        public string EmailAddress { get; set; }
        public string RightGroup { get; set; }
        public string Process { get; set; }
        public string AgencyID { get; set; }
        public string AgencyBranchID { get; set; }
        public string UserCategory { get; set; }
    }
    public class Changepassword
    {
        public string UserInfoID { get; set; }
        public string oldpass { get; set; }
        public string newpass { get; set; }
        public string type { get; set; }
    }
    public class UserPageRight
    {
        public string PageID { get; set; }
        public string Page { get; set; }
        public string URL { get; set; }
        public string BindInMenu { get; set; }
    }
    public class ProfileSelection
    {
        public string UserInfoID { get; set; }
        public string UserCategoryID { get; set; }
    }
    #endregion
    #region forget password
    public class UserRegistration
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    #endregion
}