using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    public class Agency
    {
    }
    public class AgencyDLL
    {
        public List<CountyDDL> CountyDDL { get; set; }
        public List<ConstituencyDDL> ConstituencyDDL { get; set; }
        public List<BankDDL> BankDDL { get; set; }
        public List<BranchDDL> BranchDDL { get; set; }
        public List<AgencyAgentDDL> AgencyAgentDDL { get; set; }
    }
    public class CountyDDL
    {
        public string CountyID { get; set; }
        public string County { get; set; }
    }
    public class ConstituencyDDL
    {
        public string ConstituencyID { get; set; }
        public string Constituency { get; set; }
    }
    public class BankDDL
    {
        public string BankID { get; set; }
        public string BankName { get; set; }
    }
    public class BranchDDL
    {
        public string AgencyID { get; set; }
        public string AgencyBranchID { get; set; }
        public string BranchName { get; set; }
    }
    public class AgencyAgentDDL
    {
        public string AgencyID { get; set; }
        public string AgencyBranchID { get; set; }
        public string UserInfoID { get; set; }
        public string FullName { get; set; }
    }
    public class CreateNewBranchStep1
    {
        public string AgencyBranchID { get; set; }
        public string IncID { get; set; }
        public string AgencyID { get; set; }
        public string BranchName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string POBoxNo { get; set; }
        public string PostalCode { get; set; }
        public string ImageName { get; set; }
        public string ProfileImage { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateBankDetailsStep2
    {
        public string AgencyID { get; set; }
        public string AGBRID { get; set; }
        public string TmpAgencyBranchID { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string BankId { get; set; }
        public string Branch { get; set; }
        public string CreatedBy { get; set; }
    }
    public class CreateNewBranchStep3
    {
        public string TmpAgencyID { get; set; }
        public string TmpAGBrID { get; set; }
        public string IDType { get; set; }
        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        //public string Date_of_Birth { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public string Occupation { get; set; }
        public string Other_Name { get; set; }
        public string Place_of_Live { get; set; }
        public string PIN { get; set; }
        public string Photo { get; set; }
        public string CreatedBy { get; set; }
    }

    #region View Existing Branches
    public class AgencyBranchFilter
    {
        public string AgencyID { get; set; }
        public string AgencyBranchID { get; set; }
        public string UserInfoID { get; set; }
        public string AgentName { get; set; }
        public string AgecyAgentID { get; set; }
        public string SubscriberName { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string StateID { get; set; }
        public string CityID { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string VUNAID { get; set; }
        public string PageSize { get; set; }
        public string PageNo { get; set; }

        public string TotalCount { get; set; }
        public List<ExistingBranches> ExistingBranchesList { get; set; }
        public List<ExistingAgency> ExistingAgencyList { get; set; }
        public List<AgencySubscriber> AgencySubscriberList { get; set; }
    }

    public class ExistingBranches
    {
        public string AgencyID { get; set; }
        public string AgencyBranchID { get; set; }
        public string AgencyName { get; set; }
        public string IncID { get; set; }
        public string BranchName { get; set; }
        public string FirstName { get; set; }
        public string UniqueID { get; set; }
        public string AgencyAdminInfoID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Inactive { get; set; }
    }
    public class ExistingAgency
    {
        public string AgencyID { get; set; }
        public string UserInfoID { get; set; }
        public string IncID { get; set; }
        public string BranchName { get; set; }
        public string FullName { get; set; }
        public string UniqueID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string County { get; set; }
        public string Constituency { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string Inactive { get; set; }
    }
    public class AgencySubscriber
    {
        public string AgencyID { get; set; }
        public string UserInfoID { get; set; }
        public string VUNAID { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string EnrolmentBranch { get; set; }
        public string EnrollmentDate { get; set; }
        public string EnrolmentAgent { get; set; }
        public string LastContributionDate { get; set; }
        public string Inactive { get; set; }
    }
    public class ChangeBranchStatus
    {
        public string UserInfoID { get; set; }
        public string AgencyBranchID { get; set; }
        public string ActiveInactive { get; set; }
        public string CreatedBy { get; set; }
    }
    public class CityDLL
    {
        public List<SubCityDDL> SubCityDL { get; set; }
    }
    public class SubCityDDL
    {
        public string MSCID { get; set; }
        public string SubCounty { get; set; }
    }

    public class agencysubCity
    {
        public string UserInfoID { get; set; }
    }

    public class CreateNewBranchStep2
    {
        public string TmpAgencyID { get; set; }
        public string CreatedBy { get; set; }
        public string AgencyID { get; set; }
        public string AgencyBranchID { get; set; }
        public string UserCategory { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmpCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
    #endregion View Existing Branches
    #region Agency user existing list
    public class AgencyExectingUser
    {
        public string AgencyID { get; set; }
        public string AgentName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string cityid { get; set; }
        public string stateid { get; set; }
        public string BranchID { get; set; }
        public string PageSize { get; set; }
        public string PageNo { get; set; }
    }
    
    public class AgencyExistinglist
    {
        public string TotalRows { get; set; }
        public List<AgencyUsers> agencyuser { get; set; }
       
    }
    public class AgencyUsers
    {
        public string AUIID { get; set; }
        public string Inactive { get; set; }
        public string AgentName { get; set; }
        public string mobileno { get; set; }
        public string EmailAddress { get; set; }
        public string AgencyID { get; set; }
        public string AgencyBranchID { get; set; }
        public string statename { get; set; }
        public string cityname { get; set; }
    }
#endregion

#region Agency Branch ddl list 
    public class mstAgencyBranch
    {
        public string agencyID { get; set; }
        public string StateID { get; set; }
        public string CityID { get; set; }
        public List<AgBranchDDL> AgencyBranchList { get; set; }

    }
    public class AgBranchDDL
    {
        public string AgencyBranchID { get; set; }
        public string BranchName { get; set; }

    }
#endregion 
}