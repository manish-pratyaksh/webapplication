using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBoxIndiaAPIV1._0.Models
{
    public class AgencyBranch
    {
    }
    #region NewAgent
    //public class Agent
    //{
    //    public List<NewAgentADD> NewAgentADDlist { get; set; }
    //}
    public class NewAgentADD
    {
        public string UserInfoID { get; set; }
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

    #endregion
    #region Existing User
    public class ExistingUserSearch
    {
        public string UserInfoID { get; set; }
        public string AgencyID { get; set; }
        public string AgencyBranchID { get; set; }
        public string AgentName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string UserCategoryID { get; set; }
        public string PageSize { get; set; }
        public string PageNo { get; set; }
        public string TotalCount { get; set; }
        public List<ExistingUser> ExistingUserList { get; set; }
    }

    public class ExistingUser
    {
        public string IncID { get; set; }
        public string UserInfoID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string EmployeeCode { get; set; }
        public string Inactive { get; set; }

    }
    public class AgenctActiveInactive
    {
        public string UserInfoID { get; set; }
        public string ActiveInactive { get; set; }
        public string CreatedBy { get; set; }
    }

    #endregion Existing User
}