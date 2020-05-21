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
    public class LoginDAL
    {

        #region User Login Detail
        public DataSet ValidateUser(UserLogin objUserLogin)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@MobileNo", objUserLogin.MobileNo), 
                 new SqlParameter("@Password", objUserLogin.Password),
                 new SqlParameter("@OTPBased", objUserLogin.Otpbased) };

                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.ValidateUser, P);
                return dsResult;
            }
            catch { throw; }
        }
        public StringBuilder ResetPassword(string userinfoid)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@UserInfoID ", userinfoid) };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.ResetPassword, P);
                return jsonResult;
            }
            catch { throw; }
        }
        public StringBuilder ChangePassword(Changepassword changePass)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { 
                    new SqlParameter("@UserInfoID",changePass.UserInfoID ),
                    new SqlParameter("@NewPassword",changePass.newpass ),
                    new SqlParameter("@OldPassword",String.IsNullOrEmpty(changePass.oldpass)? DBNull.Value : (object)changePass.oldpass),
                    new SqlParameter("@Type",String.IsNullOrEmpty(changePass.type)? DBNull.Value : (object)changePass.type),
                    };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.changePassword, P);
                return jsonResult;
            }
            catch { throw; }
        }
        public DataSet GetUserPageRight(string MobileNo)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@MobileNo", MobileNo), };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.GetUserPageRight, P);
                return dtResult;
            }
            catch { throw; }
        }
        public StringBuilder ProfileSelection(ProfileSelection _objProfileSelection)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@UserInfoID", _objProfileSelection.UserInfoID), };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.SetLoginSuccessLog, P);
                return jsonResult;
            }
            catch { throw; }
        }
        #endregion
        #region forget password
        public StringBuilder Setforgotpass(UserRegistration userregistration)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{                   
                    new SqlParameter("@Email",userregistration.Email), 
             };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.forgotpassword, P);
                return jsonResult;

            }
            catch { throw; }
        }
        #endregion
    }
}