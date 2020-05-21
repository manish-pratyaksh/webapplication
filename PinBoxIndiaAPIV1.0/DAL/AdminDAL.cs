using PinBoxIndiaAPIV1._0.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PinBoxIndiaAPIV1._0.DAL
{
    public class AdminDAL
    {
        #region Rights Group
        public DataSet RightsGroupDDL(RightsGroupDDL RightsGroupDDLA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                     new SqlParameter("@UserCategoryID",RightsGroupDDLA.UserCategoryID),
                };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetCategoryRightGroup, P);
                return dtResult;
            }
            catch { throw; }
        }
        public DataSet GETRightGroup(string UserCategoryID, string RightGroupID)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{                
                    new SqlParameter("@UserCatID", UserCategoryID),
                    new SqlParameter("@RGID", RightGroupID),                    
                };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGETRightGroup, P);
                return dsResult;
            }
            catch
            {
                throw;
            }
        }
        public StringBuilder SetRightGroups(RightGroups _objRightGp)
        {
            try
            {
                List<RightGPDetails> rightGPDetails = _objRightGp.rgGrouplist;
                DataTable dtRightGP = new DataTable();
                dtRightGP.Columns.Add("PageID", typeof(System.String));
                dtRightGP.Columns.Add("PageActionID", typeof(System.String));
                if (rightGPDetails == null)
                {
                    DataRow dtBRow = dtRightGP.NewRow();
                    dtBRow["PageID"] = "0";
                    dtBRow["PageActionID"] = "0";
                    dtRightGP.Rows.Add(dtBRow);
                    dtRightGP.AcceptChanges();
                }
                else
                {
                    foreach (var right in rightGPDetails)
                    {
                        DataRow dtRow = dtRightGP.NewRow();
                        dtRow["PageID"] = right.PageID;
                        dtRow["PageActionID"] = right.PageActionID;
                        dtRightGP.Rows.Add(dtRow);
                        dtRightGP.AcceptChanges();
                    }
                }
                SqlParameter[] P = new SqlParameter[]{
                
                    new SqlParameter("@UserCategory",_objRightGp.UserCategory),
                    new SqlParameter("@RightGroupID",_objRightGp.RightGroupID),
                    new SqlParameter("@RightName",_objRightGp.RightName),
                    new SqlParameter("@CreatedBy",_objRightGp.CreatedBy),
                    new SqlParameter("@PageAction",dtRightGP){SqlDbType=SqlDbType.Structured}
                };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.SPSetRightGroup, P);
                return jsonResult;
            }
            catch
            {
                throw;
            }
        }
        public DataSet GETRightGroupLeftMenu(string UserCategoryID, string RightGroupID)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{                
                    new SqlParameter("@UserCatID", UserCategoryID),
                    new SqlParameter("@RGID", RightGroupID),                    
                };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGETRightGroupMenu, P);
                return dsResult;
            }
            catch
            {
                throw;
            }
        }
        #endregion Rights Group
        #region User Management
        public DataSet GetManagementList(ManageListFilter objManageListFilter)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {               
                    new SqlParameter("@UserName ", objManageListFilter.UserName),
                    new SqlParameter("@MobileNo", objManageListFilter.MobileNo),                 
                    new SqlParameter("@Email", objManageListFilter.Email),
                    new SqlParameter("@UserCategory", objManageListFilter.UserCategory),
                    new SqlParameter("@PageSize", objManageListFilter.PageSize),
                    new SqlParameter("@PageNo", objManageListFilter.PageNo)
                };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetManagementList, P);
                return dsResult;
            }
            catch { throw; }
        }

      
        public DataSet GetUsereditdetails(string UserInfoID)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@UserInfoID", UserInfoID), };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetUserManagementByUserId, P);
                return dtResult;
            }
            catch { throw; }
        }
        public StringBuilder ProgramAdminActiveInactive(ActiveInactiveAgencySearch activeInactiveAgencySearch)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{

                    new SqlParameter("@UserInfoID",activeInactiveAgencySearch.UserInfoID),
                    new SqlParameter("@ActiveInactive",activeInactiveAgencySearch.ActiveInactive),
                    new SqlParameter("@CreatedBy",activeInactiveAgencySearch.CreatedBy),
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.SPSetUserManagementActiveInactive, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public StringBuilder UpdaterManagementDetails(MemberRegistration _objMemberRegistrationA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@UserInfoID",_objMemberRegistrationA.UserInfoID ),     
                    new SqlParameter("@FullName",String.IsNullOrEmpty(_objMemberRegistrationA.FullName)?DBNull.Value:(object)_objMemberRegistrationA.FullName ),   
                    new SqlParameter("@Gender",String.IsNullOrEmpty(_objMemberRegistrationA.Gender)?DBNull.Value:(object)_objMemberRegistrationA.Gender ),   
                    //new SqlParameter("@Marital",String.IsNullOrEmpty(_objMemberRegistrationA.MaritalStatus)?DBNull.Value:(object)_objMemberRegistrationA.MaritalStatus ), 
                    new SqlParameter("@DOB",String.IsNullOrEmpty(_objMemberRegistrationA.Date_of_Birth)?DBNull.Value:(object)_objMemberRegistrationA.Date_of_Birth ),  
                    //new SqlParameter("@DOB",_objMemberRegistrationA.Date_of_Birth),
                    new SqlParameter("@KRAPin",String.IsNullOrEmpty(_objMemberRegistrationA.KRAPin)?DBNull.Value:(object)_objMemberRegistrationA.KRAPin ), 
                    //new SqlParameter("@Mobile",String.IsNullOrEmpty(_objMemberRegistrationA.MobileNumber)?DBNull.Value:(object)_objMemberRegistrationA.MobileNumber ), 
                    new SqlParameter("@AltMobile",String.IsNullOrEmpty(_objMemberRegistrationA.AlternateMobileNo)?DBNull.Value:(object)_objMemberRegistrationA.AlternateMobileNo ),   
                    new SqlParameter("@Email",String.IsNullOrEmpty(_objMemberRegistrationA.Email)?DBNull.Value:(object)_objMemberRegistrationA.Email ),   
                    new SqlParameter("@RightGroup",String.IsNullOrEmpty(_objMemberRegistrationA.GroupID)?DBNull.Value:(object)_objMemberRegistrationA.GroupID ),   
                    new SqlParameter("@CreatedBy",String.IsNullOrEmpty(_objMemberRegistrationA.CreatedBy)?DBNull.Value:(object)_objMemberRegistrationA.CreatedBy ), 
                    new SqlParameter("@ProfileImage",String.IsNullOrEmpty(_objMemberRegistrationA.UserImageName)?DBNull.Value:(object)_objMemberRegistrationA.UserImageName ), 
                    new SqlParameter("@UserCategoryID",String.IsNullOrEmpty(_objMemberRegistrationA.UserCategory)?DBNull.Value:(object)_objMemberRegistrationA.UserCategory ),   
                     new SqlParameter("@DialerID",String.IsNullOrEmpty(_objMemberRegistrationA.DialerID)?DBNull.Value:(object)_objMemberRegistrationA.DialerID ),   
                    
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAdminUserRegistration, P);
                return jsonResult;

            }
            catch { throw; }
        }



        public StringBuilder UserManagementCreateDal(MemberRegistration _objMemberRegstA)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { 
                     new SqlParameter("@UserInfoID",_objMemberRegstA.UserInfoID),
                    new SqlParameter("@UserCategoryID",_objMemberRegstA.UserCategory),   
                     new SqlParameter("@MobileNo",String.IsNullOrEmpty(_objMemberRegstA.MobileNumber)?DBNull.Value:(object)_objMemberRegstA.MobileNumber ), 
                    new SqlParameter("@FirstName",String.IsNullOrEmpty(_objMemberRegstA.First_Name)?DBNull.Value:(object)_objMemberRegstA.First_Name ),   
                    new SqlParameter("@LastName",String.IsNullOrEmpty(_objMemberRegstA.LastName)?DBNull.Value:(object)_objMemberRegstA.LastName ),  
                    new SqlParameter("@EmailId",String.IsNullOrEmpty(_objMemberRegstA.Email)?DBNull.Value:(object)_objMemberRegstA.Email ),        
                    new SqlParameter("@EmployeeCode",String.IsNullOrEmpty(_objMemberRegstA.EmployeeCode)?DBNull.Value:(object)_objMemberRegstA.EmployeeCode ),   
                     new SqlParameter("@RightGroup",_objMemberRegstA.GroupID),   
                    new SqlParameter("@CreatedBy",_objMemberRegstA.CreatedBy),   
                   };

                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAdminUserRegistration, P);
                return jsonResult;
            }
            catch { throw; }
        }
        #endregion
        #region ReminderMatrix
        public DataSet GeProductList()
        {
            try
            {
                SqlParameter[] P = null;
                return DataLib.GetStoredProcData(DataLib.Connection.SetConnectionString, SPKeys.SPHP_GETHelpProduct, P);
            }
            catch { throw; }
        }
        public StringBuilder SetReminderMatrixDal(ReminderMatrix reminderMatrix)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{
                    new SqlParameter("@RMID",reminderMatrix.RMID),
                    new SqlParameter("@ProductType",reminderMatrix.ProductType),
                    new SqlParameter("@BeforeDueDate",reminderMatrix.BeforeDueDate),
                    new SqlParameter("@Days",reminderMatrix.Days),
                    new SqlParameter("@ReminderText",reminderMatrix.ReminderText),
                    new SqlParameter("@UserID",reminderMatrix.UserID),
                    new SqlParameter("@TemplateName",reminderMatrix.TemplateName),
                   
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetReminderMatrix, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public DataSet GetReminderMatrixDal(string RMID, string PageSize, string PageNo)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@RMID", RMID), new SqlParameter("@PageSize", PageSize), new SqlParameter("@PageNo", PageNo), };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetReminderMatrix, P);
                return dtResult;
            }
            catch { throw; }
        }
        public StringBuilder DelReminderMatrixDal(string RMID)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{
                 new SqlParameter("@RMID", RMID),
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPDelReminderMatrix, P);
                return jsonResult;

            }
            catch { throw; }
        }
        #endregion 
        #region Agency Registration
        public StringBuilder SetAgentRegistrationStep1(AgencyRegistraction agentInfo)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{
                 new SqlParameter("@AgencyId",agentInfo.AgencyID),
                    new SqlParameter("@AgencyTypeID",agentInfo.AgencyTypeID),
                    new SqlParameter("@AgencyName",agentInfo.AgencyName),
                    new SqlParameter("@Address1",agentInfo.Address1),
                    new SqlParameter("@Address2",agentInfo.Address2),
                    new SqlParameter("@stateID",agentInfo.stateID),
                    new SqlParameter("@CityID",agentInfo.CityID),
                    new SqlParameter("@Pincode",agentInfo.Pincode),
                     new SqlParameter("@ProfileImage",agentInfo.ProfileImage),
                      new SqlParameter("@CreatedBy",agentInfo.CreatedBy),
                    
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetTmpAgencyRegStep1, P);
                return jsonResult;

            }
            catch { throw; }
        }
        #endregion
        #region AgencyRegistractionStep4
        public DataSet GetAgencyServiceChargesStep4()
        {
            try
            {
                SqlParameter[] P = null;

                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetTask, P);
                return dtResult;
            }
            catch { throw; }
        }

        public StringBuilder SetAgencyServiceChargesStep4Dal(AgencyWithAdmin agencyWithAdmin)
        {
            try
            {
                List<TBLTasks> _objTBLTasks = agencyWithAdmin.TBLTasksList;
                DataTable dtTBLTasksData = new DataTable();
                dtTBLTasksData.Columns.Add("TaskID", typeof(System.String));
                dtTBLTasksData.Columns.Add("Amount", typeof(System.String));
                if (agencyWithAdmin.TBLTasksList == null)
                {
                    DataRow drTBLTasksData = dtTBLTasksData.NewRow();
                    drTBLTasksData["TaskID"] = "";
                    drTBLTasksData["Amount"] = "";

                    dtTBLTasksData.Rows.Add(drTBLTasksData);
                    dtTBLTasksData.AcceptChanges();
                }
                else
                {
                    foreach (var item in _objTBLTasks)
                    {
                        DataRow drTBLTasksData = dtTBLTasksData.NewRow();

                        drTBLTasksData["TaskID"] = String.IsNullOrEmpty(item.TaskID) ? "" : (object)item.TaskID;
                        drTBLTasksData["Amount"] = String.IsNullOrEmpty(item.Amount) ? "" : (object)item.Amount;

                        dtTBLTasksData.Rows.Add(drTBLTasksData);
                        dtTBLTasksData.AcceptChanges();
                    }
                }

                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@TmpAgencyID",Convert.ToInt64(agencyWithAdmin.TmpAgencyID)),
                    new SqlParameter("@Tbl",dtTBLTasksData),
                    new SqlParameter("@CreatedBy",agencyWithAdmin.CreatedBy),          
                 };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAgencyWithAdmin, P);
                return jsonResult;

            }
            catch { throw; }
        }
        #endregion
        #region agency registration step 3 
        public StringBuilder SetAgencyAdminInfoStep3DAL(AgencyAdminInfo agencyAdminInfo)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@TmpAgencyID",Convert.ToInt64(agencyAdminInfo.TmpAgencyID)), 
                    new SqlParameter("@FirstName",agencyAdminInfo.FirstName), 
                     new SqlParameter("@LastName",agencyAdminInfo.LastName), 
                    new SqlParameter("@Email",agencyAdminInfo.Email),
                    new SqlParameter("@EmployeeCode",agencyAdminInfo.EmployeeCode),
                    new SqlParameter("@MobileNo",agencyAdminInfo.MobileNo),
                  
                    
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetTmpAgencyAdminInfo, P);
                return jsonResult;

            }
            catch { throw; }
        }
        #endregion 
        #region Agency Bank Account Details
        public StringBuilder SetAgencyBankAccountStep2(AgencyBankAccount agentBankInfo)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@TmpAgencyID",Convert.ToInt64(agentBankInfo.TmpAgencyID)),
                    new SqlParameter("@AccountName",agentBankInfo.AccountName),
                    new SqlParameter("@AccountNo",agentBankInfo.AccountNo),                    
                    new SqlParameter("@IFSCCode",agentBankInfo.IFSCCode),
                    new SqlParameter("@CreatedBy",Convert.ToInt64(agentBankInfo.CreatedBy)),
                    
                    
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetTmpAgencyBankAccount, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public DataSet GetTempAgencyBankAccountDal(string TmpAgencyID)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                   new SqlParameter("@AgencyID",TmpAgencyID), 
                };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetTempAgencyBankAccount, P);
                return dtResult;
            }
            catch { throw; }
        }
        #endregion
        #region Agency registration step1 get
        public DataSet GetTempAgencyRegstep1Dal(string TmpAgencyID)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                   new SqlParameter("@AgencyID",TmpAgencyID), 
                };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetTempAgencyRegstep1, P);
                return dtResult;
            }
            catch { throw; }
        }
        #endregion
        #region Agency registration step1 Admin Info
        public DataSet GetTmpAgencyAdminInfoDal(string TmpAgencyID)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                   new SqlParameter("@TmpAgencyID",TmpAgencyID), 
                };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetTmpAgencyAdminInfo, P);
                return dtResult;
            }
            catch { throw; }
        }
        #endregion
        #region agency Update
       public StringBuilder SetSetNewAgencyAdminDAL(AgencyAdminInfo agencyInfo)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@AgencyID",agencyInfo.TmpAgencyID),
                    new SqlParameter("@Email",agencyInfo.Email),
                    new SqlParameter("@EmployeeCode",agencyInfo.EmployeeCode),
                    new SqlParameter("@FirstName",agencyInfo.FirstName),
                    new SqlParameter("@LastName",agencyInfo.LastName),
                    new SqlParameter("@Mobile",agencyInfo.MobileNo),
                    new SqlParameter("@CreatedBy",agencyInfo.CreatedBy),
                   
                };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAgencykeypersonUpdate, P);
                return jsonResult;

            }
            catch { throw; }
        }
        #endregion
        #region agency existing
        public DataSet GetAgencyExistingListDAL(AgencyExisting objAgencyExisting)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {               
                     new SqlParameter("@AgencyName ", objAgencyExisting.Agency),
                    new SqlParameter("@MobileNo", objAgencyExisting.Mobileno),                 
                    new SqlParameter("@Email", objAgencyExisting.Email),             
                    new SqlParameter("@cityid", objAgencyExisting.city),
                       new SqlParameter("@stateid", objAgencyExisting.state),
                    new SqlParameter("@PageSize", objAgencyExisting.PageSize),
                    new SqlParameter("@PageNo", objAgencyExisting.PageNo)
                };
                DataSet dsResult = new DataSet();
                dsResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetAgencyExisting, P);
                return dsResult;
            }
            catch { throw; }
        }
        #endregion
        #region Agency Registration edit 
        public DataSet GetAgentdetailsDAL(string agencyid)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] { new SqlParameter("@AgencyID", agencyid), };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetAgencyEdit, P);
                return dtResult;
            }
            catch { throw; }
        }
        public StringBuilder UpdateAgencyDetailsDal(AgencyRegistraction agencyInfo)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@AgencyID",agencyInfo.AgencyID),
                    new SqlParameter("@AgencyTypeID",agencyInfo.AgencyTypeID),
                    new SqlParameter("@AgencyName",agencyInfo.AgencyName),
                    new SqlParameter("@Address1",agencyInfo.Address1),
                    new SqlParameter("@Address2",agencyInfo.Address2),
                    new SqlParameter("@Pincode",agencyInfo.Pincode),
                    new SqlParameter("@StateID",agencyInfo.stateID),
                    new SqlParameter("@cityID",agencyInfo.CityID),
                    new SqlParameter("@ProfileImage",agencyInfo.ProfileImage), 
                };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetAgencyUpdate, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public DataSet GetAgencyBankAccountStep2Dal(string TmpAgencyID)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                   new SqlParameter("@AgencyID",TmpAgencyID), 
                };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetAgencyBankAccount, P);
                return dtResult;
            }
            catch { throw; }
        }
        public StringBuilder UpdateAgentBankDetailsDl(AgencyBankAccount agentInfo)
        {
            

            try
            {
                SqlParameter[] P = new SqlParameter[]{

                    new SqlParameter("@AgencyID",agentInfo.TmpAgencyID),
                     new SqlParameter("@AccountName",agentInfo.AccountName),
                      new SqlParameter("@AccountNumber",agentInfo.AccountNo),  
                      new SqlParameter("@IFSCCode",agentInfo.IFSCCode), 
                    new SqlParameter("@CreatedBy",agentInfo.CreatedBy),
                   
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.SPSetUpdateAgencyBankAccount, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public StringBuilder AgencyActiveInactiveDAL(ActiveInactiveAgencySearch activeInactiveAgencySearch)
        {

            try
            {
                SqlParameter[] P = new SqlParameter[]{

                    new SqlParameter("@AgencyID",activeInactiveAgencySearch.AgencyID),
                     new SqlParameter("@ActiveInactive",activeInactiveAgencySearch.ActiveInactive),
                      new SqlParameter("@CreatedBy",activeInactiveAgencySearch.CreatedBy),
                      
                   
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.GetConnectionString, SPKeys.SPSetAgencyActiveInactive, P);
                return jsonResult;

            }
            catch { throw; }
        }
        public DataSet GetAGServiceChargesByIDDAL(string AgencyId)
        {
            try
            {
                // SqlParameter[] P = null;
                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@AgencyID",AgencyId), 
               };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetAgencyTaskID, P);
                return dtResult;
            }
            catch { throw; }
        }
        public StringBuilder SetUpdateAgencyChargeDal(AgencyWithAdmin agencyWithAdmin)
        {

            try
            {

                List<TBLTasks> _objTBLTasks = agencyWithAdmin.TBLTasksList;

                DataTable dtTBLTasksData = new DataTable();
                dtTBLTasksData.Columns.Add("TaskID", typeof(System.String));
                dtTBLTasksData.Columns.Add("Amount", typeof(System.String));


                if (agencyWithAdmin.TBLTasksList == null)
                {
                    DataRow drTBLTasksData = dtTBLTasksData.NewRow();
                    drTBLTasksData["TaskID"] = "";
                    drTBLTasksData["Amount"] = "";

                    dtTBLTasksData.Rows.Add(drTBLTasksData);
                    dtTBLTasksData.AcceptChanges();
                }
                else
                {
                    foreach (var item in _objTBLTasks)
                    {
                        DataRow drTBLTasksData = dtTBLTasksData.NewRow();

                        drTBLTasksData["TaskID"] = String.IsNullOrEmpty(item.TaskID) ? "" : (object)item.TaskID;
                        drTBLTasksData["Amount"] = String.IsNullOrEmpty(item.Amount) ? "" : (object)item.Amount;

                        dtTBLTasksData.Rows.Add(drTBLTasksData);
                        dtTBLTasksData.AcceptChanges();
                    }
                }

                SqlParameter[] P = new SqlParameter[]{ 
                    new SqlParameter("@AgencyID",Convert.ToInt64(agencyWithAdmin.TmpAgencyID)),
                      new SqlParameter("@CreatedBy",agencyWithAdmin.CreatedBy),
                    new SqlParameter("@Tbl",dtTBLTasksData),
                   
               };
                var jsonResult = new StringBuilder();
                jsonResult = DataLib.JsonStringExecuteReader(DataLib.Connection.SetConnectionString, SPKeys.SPSetUpdateAgencyCharge, P);
                return jsonResult;

            }
            catch { throw; }
        }
        #endregion 
    }
}