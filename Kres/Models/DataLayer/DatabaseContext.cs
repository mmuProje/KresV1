using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Kres.Models.DataLayer
{
    public class DatabaseContext
    {

        public static string ConnectionString { get { return @"Data Source=94.73.170.34;Initial Catalog=u1411774_kresV1;User ID=u1411774_userDb;Password=MustafaMertUfuk123"; } }


        public static DataTable ExecuteReader(CommandType storedProcedure, string commandText, ParameterInfo[] parameterNames, params object[] parameterValues)
        {
            SqlConnection sqlcon = new SqlConnection(ConnectionString);
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(commandText, sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;

            int length = parameterNames.Length;
            SqlParameter[] sqlParams = new SqlParameter[length];

            for (int i = 0; i < length; i++)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + parameterNames[i].Name;
                parameter.Value = parameterValues[i];
                // sqlParams[i] = parameter;
                cmd.Parameters.Add(parameter);
            }

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            sqlcon.Close();
            return dt;

        }
        public static DataTable ExecuteReader(CommandType commandType, string commandText)
        {
            SqlConnection sqlcon = new SqlConnection(ConnectionString);
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(commandText, sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            sqlcon.Close();
            return dt;
        }

        public static bool ExecuteNonQuery(CommandType storedProcedure, string commandText, ParameterInfo[] parameterNames, params object[] parameterValues)
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection(ConnectionString);
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(commandText, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;

                int length = parameterNames.Length;
                SqlParameter[] sqlParams = new SqlParameter[length];

                for (int i = 0; i < length; i++)
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@" + parameterNames[i].Name;
                    parameter.Value = parameterValues[i];
                    // sqlParams[i] = parameter;
                    cmd.Parameters.Add(parameter);
                }
                cmd.ExecuteNonQuery();
                sqlcon.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}