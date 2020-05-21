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
    public class CommonDAL
    {
        #region state and city master
        public DataSet GetCityDDLDal(mstcity objmstcity)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{
                 new SqlParameter("@stateID",objmstcity.stateid)  
               };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.SetConnectionString, SPKeys.SPGetMstcity, P);
                return dtResult;

            }
            catch { throw; }
        }
        public DataSet mstStateDDL()
        {
            try
            {
                SqlParameter[] P = new SqlParameter[] {
                };
                DataSet dtResult = new DataSet();
                dtResult = DataLib.GetStoredProcData(DataLib.Connection.GetConnectionString, SPKeys.SPGetMstStates, P);
                return dtResult;
            }
            catch { throw; }
        }
        #endregion
        #region Right Group Left Menu
        public DataSet GETRightGroupLeftMenu(BindMenuFilter _objBindMenuFilter)
        {
            try
            {
                SqlParameter[] P = new SqlParameter[]{                
                    new SqlParameter("@UserCatID", _objBindMenuFilter.UserCategoryID),
                    new SqlParameter("@RGID", _objBindMenuFilter.RightGroupID),                    
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
        #endregion
    
    }
}