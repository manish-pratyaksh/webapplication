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
    public class AgencyBranchDAL
    {
        #region Agents All Work Flow
        public StringBuilder AddnewAgent(NewAgentADD objNewAgentADD)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{
                    new SqlParameter("@UserInfoID",objNewAgentADD.UserInfoID),
                    new SqlParameter("@FirstName",objNewAgentADD.FirstName),
                    new SqlParameter("@LastName",objNewAgentADD.LastName),
                    new SqlParameter("@MobileNo",objNewAgentADD.Mobile),
                    new SqlParameter("@EmployeeCode",objNewAgentADD.EmpCode),
                    new SqlParameter("@UserCategoryID",objNewAgentADD.UserCategory),
                   new SqlParameter("@EmailId",objNewAgentADD.Email),
                    new SqlParameter("@CreatedBy",objNewAgentADD.CreatedBy),
                    new SqlParameter("@AgencyID",objNewAgentADD.AgencyID),
                    new SqlParameter("@AgencyBranchID",objNewAgentADD.AgencyBranchID),
                   
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPAddNewAgent, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public DataSet GetAgent(ExistingUserSearch objExistingUserSearch)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{
                      new SqlParameter("@AgencyID",String.IsNullOrEmpty(objExistingUserSearch.AgencyID)?"":(object)objExistingUserSearch.AgencyID),
                    new SqlParameter("@AgencyBranchID",String.IsNullOrEmpty(objExistingUserSearch.AgencyBranchID)?"":(object)objExistingUserSearch.AgencyBranchID),
                    new SqlParameter("@EmailId",String.IsNullOrEmpty(objExistingUserSearch.Email)?"":(object)objExistingUserSearch.Email),
                     new SqlParameter("@UserInfoID",String.IsNullOrEmpty(objExistingUserSearch.UserInfoID)?"":(object)objExistingUserSearch.UserInfoID),
                    new SqlParameter("@AgentName",String.IsNullOrEmpty(objExistingUserSearch.AgentName)?"":(object)objExistingUserSearch.AgentName),
                  //  new SqlParameter("@EmailId",String.IsNullOrEmpty(objExistingUserSearch.Email)?"":(object)objExistingUserSearch.Email),
                    new SqlParameter("@MobileNo",String.IsNullOrEmpty(objExistingUserSearch.MobileNo)?"":(object)objExistingUserSearch.MobileNo),
                    new SqlParameter("@PageNo",objExistingUserSearch.PageNo),
                     new SqlParameter("@UserCategoryID",objExistingUserSearch.UserCategoryID),
                    new SqlParameter("@PageSize",objExistingUserSearch.PageSize),
                      
               };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.SetConnectionString, SPKeys.SPGetAgent, P);
                return dsResult;

            }
            catch { throw; }
        }
        public DataSet GetAgentdetailsByUserinfoID(string UserinfoID)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{
                  new SqlParameter("@UserinfoID",UserinfoID),
                      
               };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.SetConnectionString, SPKeys.SPGetAgenDetailByUserInfoId, P);
                return dsResult;

            }
            catch { throw; }
        }
        public StringBuilder AgentActiveInactiveDAL(AgenctActiveInactive agenctActiveInactive)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{

                    new SqlParameter("@userinfoid",agenctActiveInactive.UserInfoID),
                     new SqlParameter("@ActiveInactive",agenctActiveInactive.ActiveInactive),
                      new SqlParameter("@CreatedBy",agenctActiveInactive.CreatedBy),
                              
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.SPSetAgentuserActiveInactive, P);
                return jsonResult;

            }
            catch { throw; }
        }
        #endregion
      
    }
}


       
 
 