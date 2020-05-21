using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    public class Admin
    {
    }
    #region Menu master
    public class GetMainPage
    {

        public string PageId { get; set; }
        public string PageName { get; set; }
        public string URL { get; set; }
        public string ChildCount { get; set; }
        public string lnkID { get; set; }
        public string IconCSS { get; set; }

    }
    public class GetSubpage : GetMainPage
    {
        public string ParentPageID { get; set; }
    }

    public class RightGroupMenu
    {

        public List<GetMainPage> mainPage { get; set; }
        public List<GetSubpage> subPage { get; set; }
    }

    #endregion
    #region Rights Group
    public class RightsGroupMaster
    {
        public List<RightsGroupDDL> RightsGroupDDL { get; set; }
       
    }

    public class RightsGroupDDL
    {
        public string UserCategoryID { get; set; }
        public string PRGID { get; set; }
        public string RightGroupName { get; set; }
    }
    public class RightGroupAdmin
    {
        public List<RightGPMasterModue> RGMasterModule { get; set; }
        public List<ModulewiseAction> ModulewiseAction { get; set; }
        public List<RightGPPageDetails> RightGPPageDetails { get; set; }
        public List<PageActionDetails> PageActionDetails { get; set; }
    }

    public class RightGPMasterModue
    {
        public string PageID { get; set; }
        public string Page { get; set; }
        public string URL { get; set; }
    }

    public class ModulewiseAction
    {
        public string ActionID { get; set; }
        public string ParentID { get; set; }
        public string Action { get; set; }
    }

    public class RightGPPageDetails
    {
        public string PageID { get; set; }
        public string ParentPageID { get; set; }
        public string Page { get; set; }
        public string PageURL { get; set; }
    }

    public class PageActionDetails
    {
        public string PageActionID { get; set; }
        public string PageID { get; set; }
        public string ActionID { get; set; }
        public string ActionName { get; set; }
        public string ParentPageID { get; set; }
        public string AvlAction { get; set; }
        public string RightAvl { get; set; }
    }
    public class RightGroups
    {

        public string UserCategory { get; set; }
        public string RightGroupID { get; set; }
        public string RightName { get; set; }
        public string CreatedBy { get; set; }
        public List<RightGPDetails> rgGrouplist { get; set; }

    }
    public class RightGPDetails
    {
        public string PageActionID { get; set; }
        public string PageID { get; set; }
    }


    #endregion  Rights Group
    #region User Management

    public class MemberRegistration
    {
      
        public string EmployeeCode { get; set; }
        public string KRAPin { get; set; }
        public string Email { get; set; }
        public string TmpID { get; set; }
        public string SNo { get; set; }
        public string IDType { get; set; }
        public string ID_Number { get; set; }
        public string UserInfoID { get; set; }
        public string TempRegID { get; set; }
        public string First_Name_Status { get; set; }
        public string First_Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateMobileNo { get; set; }
        public string UserCategory { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string Date_of_Birth { get; set; }
        public string Other_Name { get; set; }
        public string CreatedBy { get; set; }
        public string MemberOTP { get; set; }
        public string MemberPassword { get; set; }
        public string SMSDetails { get; set; }
        public string RegistrationID { get; set; }
        public string Place_of_Live { get; set; }
        public string PIN { get; set; }
        public string Photo { get; set; }
        public string MaritalID { get; set; }
        public string KRAPIN { get; set; }
        public string EmailAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentPOBox { get; set; }
        public string PermanentLocation { get; set; }
        public string PermanentDivision { get; set; }
        public string PermanentDistrict { get; set; }
        public string UserPhoto { get; set; }
        public string UserImageName { get; set; }
        public string OccupationID { get; set; }
        public string OtherOccupation { get; set; }
        public string GroupID { get; set; }
        public string USSDType { get; set; }
        public string UssdSessionID { get; set; }
        public string DialerID { get; set; }
        public string IPRSOccupation { get; set; }
        public string Occupation { get; set; }
    }
    public class MemberProfile
    {
        public string TmpID { get; set; }
        public string UserInfoID { get; set; }
        public string TempRegID { get; set; }
        public string UserCategoryID { get; set; }
        public string VUNAID { get; set; }
        public string UniqueID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string DOB { get; set; }
        public string EnrolmentDate { get; set; }
        public string MaritalID { get; set; }
        public string KRAPIN { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string OccupationID { get; set; }
        public string OtherOccupation { get; set; }
        public string ProfileImage { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentPOBox { get; set; }
        public string PermanentPostalCode { get; set; }
        public string PermanentDistrict { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string CorrespondencePOBox { get; set; }
        public string CorrespondencePostalCode { get; set; }
        public string District { get; set; }
        public string PermanentLocation { get; set; }
        public string PermanentDivision { get; set; }
        public string CorrespondenceLocation { get; set; }
        public string Division { get; set; }
        public string Country { get; set; }
        public string Constituency { get; set; }
        public string ContributionAmount { get; set; }
        public string LastExpensePremimumAmount { get; set; }
        public string Servicecharge { get; set; }
        public string CreatedBy { get; set; }
        public string CheckBoxCorrespondenceAddress { get; set; }
        public string CorrespondenceCountyID { get; set; }
        public string CorrespondenceSubCountyID { get; set; }
        public string CorrespondenceProvince { get; set; }
        public string ProductName { get; set; }

    }
    public class ManageListFilter
    {
        public string UserCategory { get; set; }
        public string PageSize { get; set; }
        public string PageNo { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

    }

    public class Management
    {
        public string TotalCount { get; set; }
        public List<Managementuser> Managementuserlist = new List<Managementuser>();
    }
    public class Managementuser
    {
        public string UserInfoID { get; set; }
        public string IncID { get; set; }
        public string AdminUser { get; set; } 
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DELETED { get; set; }

    }
    public class UserAdminManage
    {

        public string UserInfoID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string RightGroup { get; set; }
        public string EmployeeCode { get; set; }
        
    }
    public class ActiveInactiveAgencySearch
    {
        public string AgencyID { get; set; }
        public string UserInfoID { get; set; }
        public string ActiveInactive { get; set; }
        public string CreatedBy { get; set; }
    }
    #endregion
    #region ReminderMatrix
    public class ExcalationHelpGroup
    {
        public List<HelpGroup> HelpGroupList { get; set; }
        public List<HelpProduct> HelpProductList { get; set; }
    }
    public class HelpGroup
    {
        public string GroupID { get; set; }
        public string GroupName { get; set; }
    }
    public class HelpProduct
    {
        public string id { get; set; }
        public string Product { get; set; }
    }
    public class ReminderMatrix
    {
        public string RMID { get; set; }
        public string ProductType { get; set; }
        public string BeforeDueDate { get; set; }
        public string Days { get; set; }
        public string ReminderText { get; set; }
        public string UserID { get; set; }
        public string TemplateName { get; set; }
        public string TotalCount { get; set; }
        public List<ReminderMatrixList> objReminderMatrixList { get; set; }
    }
    public class ReminderMatrixList
    {
        public string RMID { get; set; }
        public string ProductType { get; set; }
        public string ProductTypeID { get; set; }
        public string BeforeDueDate { get; set; }
        public string Days { get; set; }
        public string ReminderText { get; set; }
        public string TemplateName { get; set; }

    }
    #endregion 
    #region AgentRegistractionStep1
    public class AgencyRegistraction
    {
        public string AgencyID { get; set; }
        public string AgencyTypeID { get; set; }
        public string AgencyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string stateID { get; set; }
        public string CityID { get; set; }
        public string Pincode { get; set; }
        public string ProfileImage { get; set; }
        public string ImageName { get; set; }
        public string CreatedBy { get; set; }

      
    }
    public class AgencyServiceChargesContainer
    {
        public string AgencyId { get; set; }
        public List<AgencyServiceCharges> AgencyServiceChargesList { get; set; }
    }
    public class AgencyServiceCharges
    {
        public string SAChkID { get; set; }
        public string TaskID { get; set; }
        public string Task { get; set; }
        public string TaskParentID { get; set; }
        public string TaskOrder { get; set; }
        public string VarValue { get; set; }
        public string variableOption { get; set; }
    }
    public class AgencyWithAdmin
    {
        public string AgServiceID { get; set; }
        public string TmpAgencyID { get; set; }
        public string CreatedBy { get; set; }
        public List<TBLTasks> TBLTasksList { get; set; }
    }
    public class TBLTasks
    {
        public string TaskID { get; set; }
        public string Amount { get; set; }

    }
    #endregion 
    #region AgentRegistractionStep3

    public class AgencyAdminInfoContainer
    {
        public string AgencyID { get; set; }
        public string AgencyBrachId { get; set; }
        public string UserCategoryID { get; set; }
        public List<AgencyAdminInfo> agentadminlist { get; set; }

    }
    public class AgencyAdminInfo
    {
        public string TmpAgencyID { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmployeeCode { get; set; }
        public string MobileNo { get; set; }
        public string CreatedBy { get; set; } 
    }
    #endregion AgentRegistractionStep3
    #region agency existing
    public class AgencyExisting
    {

        public string Agency { get; set; }
        public string Mobileno { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string Email { get; set; }
        public string PageSize { get; set; }
        public string PageNo { get; set; }
        public string TotalRows { get; set; }
        public List<AgencyExistingList> AgencyExistingList { get; set; }
    }
    public class AgencyExistingList
    {
        public string IncID { get; set; }
        public string AgencyID { get; set; }
        public string AgencyName { get; set; }
        public string mobileno { get; set; }
        public string EmailAddress { get; set; }
        public string statename { get; set; }
        public string cityname { get; set; }
        public string KeyPersonName { get; set; }
        public string Inactive { get; set; }
    }
    #endregion
    #region AgencyBankAcoountDetails
    public class AgencyBankAccount
    {
        public string TmpAgencyID { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string BankId { get; set; }
        public string IFSCCode { get; set; }
        public string Branch { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    #endregion
  
}