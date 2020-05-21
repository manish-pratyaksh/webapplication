using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.DAL
{
    public class SPKeys
    {
        #region Web Registration
        public const string SetPersonalDetails = "SET_PersonalDetail";
        public const string SetAddressDetails = "Set_AddressDetails";
        public const string SetBankDetails = "Set_UserBankDetails";
        #endregion
        #region Rights Group
       
        public const string SPGETRightGroup = "GETRightGroup";
        public const string SPSetRightGroup = "SetPageRight";
        public const string SPGETRightGroupMenu = "GETRightGroupMenu";
        public const string SPGetCategoryRightGroup = "GetCategoryRightGroup";
        #endregion Rights Group    
        #region ReminderMatrix
        public const string SPHP_GETHelpProduct = "HP_GETHelpProduct";
        public const string SPSetReminderMatrix = "SetReminderMatrix";
        public const string SPGetReminderMatrix = "GetReminderMatrix";
        public const string SPDelReminderMatrix = "DelReminderMatrix";
        #endregion
        #region Login
        public const string ValidateUser = "validateuser";
        public const string ResetPassword = "GetCheckPassword";
        public const string changePassword = "SetChangePassword";
        public const string GetUserPageRight = "GETUserPageRight";
        public const string SetLoginSuccessLog = "SetLoginSuccessLog";
        #endregion
        #region Agency        
        public const string SPSetTmpAgencyBranch = "SetTmpAgencyBranch";
        public const string SPGetTmpAgencyBranch = "GetTmpAgencyBranch";

        public const string SPSetAgencyBrachDetails = "SetAgencyBrachDetails";
        public const string SPGetAgencyExistingUser = "GetAgentExistingByAgency";
       
        #endregion
        #region User Management
        public const string SPGetManagementList = "GetUserManagementAdmin";
        public const string SPSetAdminUserRegistration = "SetAdminUserRegistration";
        public const string SPGetUserManagementByUserId = "GetUserManagementByUserId";
        public const string SPSetUserManagementActiveInactive = "SetUserManagementActiveInactive";

        #endregion
        #region master   
        public const string SPGetMstStates = "GetMstStates";
        public const string SPGetMstcity = "GetMstcity";
        
        #endregion
        #region Agency Bank Account Detail
        public const string SPSetTmpAgencyBankAccount = "SetTmpAgencyBankAccount";
        #endregion
        #region Agency Registration
        public const string SPSetTmpAgencyRegStep1 = "SetTmpAgencyRegStep1";
        public const string SPSetTmpAgencyAdminInfo = "SetTmpAgencyAdminInfo";
        public const string SPGetTempAgencyBankAccount = "GetTempAgencyBankAccount";   //////   Get Agency Service Charges
        public const string SPGetTask = "GetTask";
        public const string SPSetAgencyWithAdmin = "SetAgencyWithAdmin";
        public const string SPGetTempAgencyRegstep1 = "GetTempAgencyRegstep1";
        public const string SPGetTmpAgencyAdminInfo = "GetTmpAgencyAdminInfo";
        public const string SPGetAgencyExisting = "GetAgencyExisting";
        public const string SPGetAgencyEdit = "GetAgencyDetail";
        public const string SPSetAgencyUpdate = "SetAgencyUpdate";
        public const string SPGetAgencyBankAccount = "GetAgencyBankAccount";
        public const string SPSetUpdateAgencyBankAccount = "SetUpdateAgencyBankAccount";
        public const string SPSetAgencyActiveInactive = "SetAgencyActiveInactive";
        public const string SPGetAgencyTaskID = "GetAgencyTaskID"; 
         public const string SPSetUpdateAgencyCharge = "SetUpdateAgencyCharge";
         public const string SPGetManageAgentBranch = "GetManageAgentBranch";
        #endregion
        #region Add new Agent
        public const string SPAddNewAgent = "SetAgentRegistration";
        public const string SPGetAgent = "GetAgentDetails";
        public const string SPSetAgentuserActiveInactive = "SetAgentActiveInactive"; 
        #endregion
        #region UpdateAgent
        public const string SPGetAgenDetailByUserInfoId = "GetAgenDetailByUserInfoId";
        #endregion
        #region Update agency keyperson
        public const string SPSetAgencykeypersonUpdate = "SetNewAgencyAdmin";
        #endregion
        #region ManageBranch
        public const string SPGetAgencyBranchSearch = "GetAgencyBranchSearch";
        public const string SPGetAgencyBranchUpdate = "GetAgencyBranchUpdate";
        public const string SPSetAgencyBranchUpdate = "SetAgencyBranchUpdate";
        public const string SPSetAgencyBranchActiveInactive = "SetAgencyBranchActiveInactive";
        public const string SPSetNewAgencyBranchAdmin = "SetNewAgencyBranchAdmin";
        #endregion
        #region WhatsApp
        public const string GetScreenDetails = "Get_Validate_WA";
        public const string SetSubscriberOTP = "Set_SubuscriberMobileOTPValid";
        public const string SetAadharFrontDetails = "SetSubuscriber_FrontPAge";  
        #endregion
        #region forget password
        public const string forgotpassword = "ForgetPassword";
        #endregion
        #region Activate/Deactivate Agent
        public const string SPSetAgentActiveInactive = "SetAgentActiveInactiveByAgency";
        #endregion
    }
}