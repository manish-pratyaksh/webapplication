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
    public class AgencyDAL
    {
        #region CreateBranchStep1
        public StringBuilder SetNewBranchStep1(CreateNewBranchStep1 _objCreateNewBranchStep1A)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { 
                    new SqlParameter("@AgencyID",_objCreateNewBranchStep1A.AgencyID ),   
                    new SqlParameter("@BranchName",String.IsNullOrEmpty(_objCreateNewBranchStep1A.BranchName)?DBNull.Value:(object)_objCreateNewBranchStep1A.BranchName ),   
                    new SqlParameter("@Address1",String.IsNullOrEmpty(_objCreateNewBranchStep1A.AddressLine1)?DBNull.Value:(object)_objCreateNewBranchStep1A.AddressLine1 ),   
                    new SqlParameter("@Address2",String.IsNullOrEmpty(_objCreateNewBranchStep1A.AddressLine2)?DBNull.Value:(object)_objCreateNewBranchStep1A.AddressLine2 ),   
                    new SqlParameter("@StateID",String.IsNullOrEmpty(_objCreateNewBranchStep1A.State)?DBNull.Value:(object)_objCreateNewBranchStep1A.State ),   
                    new SqlParameter("@CityID",String.IsNullOrEmpty(_objCreateNewBranchStep1A.City)?DBNull.Value:(object)_objCreateNewBranchStep1A.City ), 
                    new SqlParameter("@POBox",String.IsNullOrEmpty(_objCreateNewBranchStep1A.POBoxNo)?DBNull.Value:(object)_objCreateNewBranchStep1A.POBoxNo ),                                       
                    new SqlParameter("@CreatedBy",String.IsNullOrEmpty(_objCreateNewBranchStep1A.CreatedBy)?DBNull.Value:(object)_objCreateNewBranchStep1A.CreatedBy ),   
                   };

                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetTmpAgencyBranch, P);
                return jsonResult;
            }
            catch { throw; }
        }
        public DataSet GetNewBranchStep1(CreateNewBranchStep1 _objCreateNewBranchStep1A)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                   new SqlParameter("@TmpAgencyBranchID",_objCreateNewBranchStep1A.AgencyBranchID),   
                };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetTmpAgencyBranch, P);
                return dtResult;
            }
            catch { throw; }
        }
        #endregion
        #region Branch All Work Flow
        public StringBuilder SetNewBranchStep2(CreateNewBranchStep2 _objCreateNewBranchStep2)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{
                    new SqlParameter("@AgencyID",_objCreateNewBranchStep2.AgencyID),
                    new SqlParameter("@TmpAGBrID",_objCreateNewBranchStep2.AgencyBranchID),
                    new SqlParameter("@FirstName",_objCreateNewBranchStep2.FirstName),
                    new SqlParameter("@LastName",_objCreateNewBranchStep2.LastName),
                    new SqlParameter("@Mobile",_objCreateNewBranchStep2.Mobile),
                    new SqlParameter("@Email",_objCreateNewBranchStep2.Email),
                    new SqlParameter("@EmpCode",_objCreateNewBranchStep2.EmpCode),
                    new SqlParameter("@CreatedBy",_objCreateNewBranchStep2.CreatedBy),                 
                   
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAgencyBrachDetails, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public DataSet ViewExistingBranchesList(AgencyBranchFilter _objAgencyBranchFilterA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                    
                    new SqlParameter("@AgencyID", _objAgencyBranchFilterA.AgencyID), 
                    new SqlParameter("@BranchName", _objAgencyBranchFilterA.BranchName), 
                    new SqlParameter("@StateID", _objAgencyBranchFilterA.StateID), 
                    new SqlParameter("@CityID", _objAgencyBranchFilterA.CityID), 
                    new SqlParameter("@Email", _objAgencyBranchFilterA.Email),
                    new SqlParameter("@MobileNo", _objAgencyBranchFilterA.MobileNo),
                    new SqlParameter("@PageNo", _objAgencyBranchFilterA.PageNo),                 
                    new SqlParameter("@PageSize", _objAgencyBranchFilterA.PageSize)
                };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.SetConnectionString, SPKeys.SPGetAgencyBranchSearch, P);
                return dsResult;
            }
            catch { throw; }
        }
        public DataSet ViewExistingagencyuserList(AgencyExectingUser _objAgencyExectingUser)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                    
                    new SqlParameter("@AgencyID", _objAgencyExectingUser.AgencyID), 
                    new SqlParameter("@AgentName", _objAgencyExectingUser.AgentName), 
                    new SqlParameter("@MobileNo", _objAgencyExectingUser.MobileNo), 
                    new SqlParameter("@Email", _objAgencyExectingUser.Email), 
                    new SqlParameter("@cityid", _objAgencyExectingUser.cityid),
                    new SqlParameter("@stateid", _objAgencyExectingUser.stateid),
                    new SqlParameter("@BranchID", _objAgencyExectingUser.BranchID),                 
                    new SqlParameter("@PageSize", _objAgencyExectingUser.PageSize),
                    new SqlParameter("@PageNo", _objAgencyExectingUser.PageNo)
                };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.SetConnectionString, SPKeys.SPGetAgencyExistingUser, P);
                return dsResult;
            }
            catch { throw; }
        }
        public DataSet GetAgencyBranchUpdate(CreateNewBranchStep1 _objGetBranchUpdateA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                   new SqlParameter("@AgencyBranchID",_objGetBranchUpdateA.AgencyBranchID),   
                };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetAgencyBranchUpdate, P);
                return dtResult;
            }
            catch { throw; }
        }
        public StringBuilder SetUpdateBranchDetails(CreateNewBranchStep1 _objBranchUpdateA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { 
                    new SqlParameter("@AgBrID",_objBranchUpdateA.AgencyBranchID ),   
                    new SqlParameter("@BranchName",String.IsNullOrEmpty(_objBranchUpdateA.BranchName)?DBNull.Value:(object)_objBranchUpdateA.BranchName ),   
                    new SqlParameter("@Address1",String.IsNullOrEmpty(_objBranchUpdateA.AddressLine1)?DBNull.Value:(object)_objBranchUpdateA.AddressLine1 ),   
                    new SqlParameter("@Address2",String.IsNullOrEmpty(_objBranchUpdateA.AddressLine2)?DBNull.Value:(object)_objBranchUpdateA.AddressLine2 ),   
                    new SqlParameter("@StateID",String.IsNullOrEmpty(_objBranchUpdateA.State)?DBNull.Value:(object)_objBranchUpdateA.State ),   
                    new SqlParameter("@CityID",String.IsNullOrEmpty(_objBranchUpdateA.City)?DBNull.Value:(object)_objBranchUpdateA.City ), 
                    new SqlParameter("@POBox",String.IsNullOrEmpty(_objBranchUpdateA.POBoxNo)?DBNull.Value:(object)_objBranchUpdateA.POBoxNo ),
                    new SqlParameter("@UpdatedBy",String.IsNullOrEmpty(_objBranchUpdateA.CreatedBy)?DBNull.Value:(object)_objBranchUpdateA.CreatedBy ),   
                   };

                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAgencyBranchUpdate, P);
                return jsonResult;
            }
            catch { throw; }
        }
        public StringBuilder ChangeStatusBranch(ChangeBranchStatus _objChangeBranchStatusA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { 
                    new SqlParameter("@AgencyBranchID",_objChangeBranchStatusA.AgencyBranchID ),   
                    new SqlParameter("@ActiveInactive",String.IsNullOrEmpty(_objChangeBranchStatusA.ActiveInactive)?DBNull.Value:(object)_objChangeBranchStatusA.ActiveInactive ),   
                    new SqlParameter("@CreatedBy",String.IsNullOrEmpty(_objChangeBranchStatusA.CreatedBy)?DBNull.Value:(object)_objChangeBranchStatusA.CreatedBy ),   
                   };

                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAgencyBranchActiveInactive, P);
                return jsonResult;
            }
            catch { throw; }
        }
        public StringBuilder SetMemberProfile(AgencyAdminInfo objAgentStepA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { 
                    new SqlParameter("@AgencyBranchID",objAgentStepA.TmpAgencyID ),   
                    new SqlParameter("@FirstName",objAgentStepA.FirstName ),   
                    new SqlParameter("@LastName", String.IsNullOrEmpty(objAgentStepA.LastName)?DBNull.Value:(object)objAgentStepA.LastName ), 
                    new SqlParameter("@Mobile",String.IsNullOrEmpty(objAgentStepA.MobileNo)?DBNull.Value:(object)objAgentStepA.MobileNo ),   
                    new SqlParameter("@Email",String.IsNullOrEmpty(objAgentStepA.Email)?DBNull.Value:(object)objAgentStepA.Email ),   
                    new SqlParameter("@employeeCode",String.IsNullOrEmpty(objAgentStepA.EmployeeCode)?DBNull.Value:(object)objAgentStepA.EmployeeCode ),   
                    new SqlParameter("@CreatedBy",String.IsNullOrEmpty(objAgentStepA.CreatedBy)?DBNull.Value:(object)objAgentStepA.CreatedBy ),                     
                    };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetNewAgencyBranchAdmin, P);
                return jsonResult;
            }
            catch { throw; }
        }
        #endregion
        #region Activate/Deactivate Agent
        public StringBuilder PostChangeStatusManageAgent(ChangeBranchStatus _objChangeBranchStatusA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { 
                    new SqlParameter("@AUIID",_objChangeBranchStatusA.UserInfoID ),   
                    new SqlParameter("@ActiveInactive",String.IsNullOrEmpty(_objChangeBranchStatusA.ActiveInactive)?DBNull.Value:(object)_objChangeBranchStatusA.ActiveInactive ),   
                    new SqlParameter("@CreatedBy",String.IsNullOrEmpty(_objChangeBranchStatusA.CreatedBy)?DBNull.Value:(object)_objChangeBranchStatusA.CreatedBy ),   
                   };

                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAgentActiveInactive, P);
                return jsonResult;
            }
            catch { throw; }
        }
        #endregion

        #region Agency Branch dropdown list
        public DataSet GetAgencyBranchDdlDAL(mstAgencyBranch objmstAgencyBranch)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{
                 new SqlParameter("@agencyID",objmstAgencyBranch.agencyID) ,
                 new SqlParameter("@StateID",objmstAgencyBranch.StateID) ,
                 new SqlParameter("@CityID",objmstAgencyBranch.CityID) 
               };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.SetConnectionString, SPKeys.SPGetManageAgentBranch, P);
                return dtResult;

            }
            catch { throw; }
        }
        #endregion
    }
}