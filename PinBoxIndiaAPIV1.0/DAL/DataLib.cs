using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PinBoxIndiaAPIV1._0.DAL
{
    public class DataLib
    {
        public enum Connection
        {
            GetConnectionString,
            SetConnectionString,
            ReportConnectionString
        }
        public static string GetConnectionString(Connection connType)
        {

            string retValue = "";
            switch (connType)
            {
                case Connection.GetConnectionString:
                    {
                        retValue = ConfigurationManager.AppSettings["GetConnString"].ToString();
                        break;
                    }
                case Connection.SetConnectionString:
                    {
                        retValue = ConfigurationManager.AppSettings["SetConnString"].ToString();
                        break;
                    }
                case Connection.ReportConnectionString:
                    {
                        retValue = ConfigurationManager.AppSettings["ReportConnString"].ToString();
                        break;
                    }
            }
            return retValue;
        }
        public static DataTable GetStoredProcDataTablewithoutP(Connection connType, string strSP)
        {

            string connStr = GetConnectionString(connType);
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                //Prepare the Command
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = strSP;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Add stored procedure parameters to Command

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.SelectCommand = cmd;
                        //DO NOT open the connection, allow the fill command to do this
                        adapter.Fill(dt);
                        adapter.SelectCommand = null;
                    }
                }
            }
            return dt;
        }
        public static DataTable GetStoredProcDataTable(Connection connType, string strSP, SqlParameter[] arrSPParams)
        {

            string connStr = GetConnectionString(connType);
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                //Prepare the Command
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = strSP;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Add stored procedure parameters to Command
                    if (arrSPParams != null)
                    {
                        foreach (SqlParameter param in arrSPParams)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.SelectCommand = cmd;
                        //DO NOT open the connection, allow the fill command to do this
                        adapter.Fill(dt);
                        adapter.SelectCommand = null;
                    }
                }
            }
            return dt;
        }
        public static DataSet GetStoredProcData(Connection connType, string strSP, SqlParameter[] arrSPParams)
        {

            string connStr = GetConnectionString(connType);
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                //Prepare the Command
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = strSP;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Add stored procedure parameters to Command
                    if (arrSPParams != null)
                    {
                        foreach (SqlParameter param in arrSPParams)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.SelectCommand = cmd;
                        //DO NOT open the connection, allow the fill command to do this
                        adapter.Fill(ds);
                        adapter.SelectCommand = null;
                    }
                }
            }
            return ds;
        }
        public static int ExecuteStoredProcData(Connection connType, string strSP, SqlParameter[] arrSPParams)
        {
            string connStr = GetConnectionString(connType);
            int RowsAffected = 0;


            using (SqlConnection con = new SqlConnection(connStr))
            {
                //Prepare the Command
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandTimeout = 10000;
                    cmd.Connection = con;
                    cmd.CommandText = strSP;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Add stored procedure parameters to Command
                    if (arrSPParams != null)
                    {
                        foreach (SqlParameter param in arrSPParams)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }
                    try
                    {
                        if (con.State == ConnectionState.Open) con.Close(); con.Open();
                        RowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                    }

                }
            }
            return RowsAffected;
        }
        public static string GetScalarStringStoredProcData(Connection connType, string strSP, SqlParameter[] arrSPParams)
        {
            string connStr = string.Empty;
            string retString = string.Empty;
            connStr = GetConnectionString(connType);


            using (SqlConnection con = new SqlConnection(connStr))
            {
                //Prepare the Command
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = strSP;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Add stored procedure parameters to Command
                    if (arrSPParams != null)
                    {
                        foreach (SqlParameter param in arrSPParams)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }
                    if (con.State == ConnectionState.Open) con.Close(); con.Open();
                    retString = Convert.ToString(cmd.ExecuteScalar());
                    //retString = arrSPParamsOut[0].Value.ToString();
                    con.Close();
                }
            }
            return retString;
        }
        public static IDataReader ExecuteReader(Connection connType, string strSP, SqlParameter[] arrSPParams)
        {
            string connStr = string.Empty;
            string retString = string.Empty;
            connStr = GetConnectionString(connType);
            string resultOutput = string.Empty;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                //Prepare the Command
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = strSP;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Add stored procedure parameters to Command
                    if (arrSPParams != null)
                    {
                        foreach (SqlParameter param in arrSPParams)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }
                    if (con.State == ConnectionState.Open) con.Close(); con.Open();
                    //using (IDataReader dr = cmd.ExecuteReader())
                    //{
                    //    return dr;
                    //}
                    var jsonResult = new StringBuilder();
                    var reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        jsonResult.Append("[]");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            jsonResult.Append(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            return null;
        }
        public static SqlCommand createCommand(Connection connType, string strSP, SqlParameter[] arrSPParams)
        {
            string connStr = string.Empty;
            string retString = string.Empty;
            connStr = GetConnectionString(connType);
            SqlConnection con = new SqlConnection(connStr);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = strSP;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //Add stored procedure parameters to Command
                if (arrSPParams != null)
                {
                    foreach (SqlParameter param in arrSPParams)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                if (con.State == ConnectionState.Open) con.Close(); con.Open();

                return cmd;
            }
        }
        public static StringBuilder JsonStringExecuteReader(Connection connType, string strSP, SqlParameter[] arrSPParams)
        {
            string connStr = GetConnectionString(connType);
            using (SqlConnection con = new SqlConnection(connStr))
            {
                //Prepare the Command
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = strSP;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Add stored procedure parameters to Command
                    if (arrSPParams != null) { foreach (SqlParameter param in arrSPParams) { cmd.Parameters.Add(param); } }
                    if (con.State == ConnectionState.Open) con.Close(); con.Open();
                    var jsonResult = new StringBuilder();
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows) { jsonResult.Append("[]"); }
                    else { while (reader.Read()) { jsonResult.Append(reader.GetValue(0).ToString()); } }

                    return jsonResult;
                }
            }
            return null;
        }
    }
}