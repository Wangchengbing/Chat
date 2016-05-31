using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;
namespace LoserDAL
{
    public abstract class SqlHelper
    {
        public static readonly string ConnectionString = Config.ConnectionString;
        public static readonly int PageSize = Config.PageSize;
        /// <summary>
        /// 修改默认 SqlCommand Timeout 的值，默认为30s，目前设置为120s
        /// </summary>
        public static readonly int Default_Timeout_Value = 120;
        public static object ExecuteScalar(string cmdText, SqlParameter[] patas)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.Parameters.AddRange(patas);
            cmd.CommandTimeout = Default_Timeout_Value;
            return ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, cmd);
        }
        public static object ExecuteScalar(string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = Default_Timeout_Value;
            return ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, cmd);
        }
        public static object ExecuteScalar(SqlCommand cmd)
        {
            return ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, cmd);
        }
        public static object ExecuteScalar(SqlCommand cmd, CommandType type)
        {
            return ExecuteScalar(SqlHelper.ConnectionString, type, cmd);
        }
        public static object ExecuteScalar(string connectionString, CommandType cmdType, SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = Default_Timeout_Value;
                return cmd.ExecuteScalar();
            }
        }
        public static int ExecuteNonQuery(string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = Default_Timeout_Value;
            return ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, cmd);
        }
        public static int ExecuteNonQuery(SqlCommand cmd)
        {
            return ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, cmd);
        }
        public static int ExecuteNonQuery(string connectionString, SqlCommand cmd)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, cmd);
        }
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = Default_Timeout_Value;
                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return rowsAffected;
            }
        }
        public static int ExecuteNonQuery(SqlCommand cmd, out int sysno)
        {
            return ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, cmd, out sysno);
        }
        public static int ExecuteNonQuery(string connectionString, SqlCommand cmd, out int sysno)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, cmd, out sysno);
        }
        //new
        public static int ExecuteNonQuery(string sql, SqlParameter[] patas, CommandType cmdType)
        {
            SqlCommand cmd = new SqlCommand(sql);
            if (patas != null)
            {
                cmd.Parameters.AddRange(patas);
            }
            cmd.CommandType = cmdType;
            return ExecuteNonQuery(cmd, cmdType);
        }
        public static int ExecuteNonQuery(string sql, SqlParameter[] patas)
        {
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddRange(patas);
            cmd.CommandType = CommandType.Text;
            return ExecuteNonQuery(cmd);
        }
        public static int ExecuteNonQuery(SqlCommand cmd, CommandType cmdType)
        {
            return ExecuteNonQuery(SqlHelper.ConnectionString, cmdType, cmd);
        }
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, SqlCommand cmd, out int sysno)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = Default_Timeout_Value;
                int rowsAffected = cmd.ExecuteNonQuery();
                //必须符合下面的条件
                if (cmd.Parameters.Contains("@SysNo") && cmd.Parameters["@SysNo"].Direction == ParameterDirection.Output)
                    sysno = (int)cmd.Parameters["@SysNo"].Value;
                else if (cmd.Parameters.Contains("@TransactionNumber") && cmd.Parameters["@TransactionNumber"].Direction == ParameterDirection.Output)
                    sysno = (int)cmd.Parameters["@TransactionNumber"].Value;
                else
                    throw new Exception("SqlHelper: Does not contain SysNo or ParameterDirection is Not Output");
                cmd.Parameters.Clear();
                return rowsAffected;
            }
        }
        public static DataSet ExecuteDataSet(string cmdText)
        {
            return ExecuteDataSet(SqlHelper.ConnectionString, cmdText, null);
        }
        public static DataSet ExecuteDataSet(string connectionString, string cmdText)
        {
            return ExecuteDataSet(connectionString, cmdText, null);
        }
        public static DataSet ExecuteDataSet(string cmdText, SqlParameter[] paras)
        {
            return ExecuteDataSet(SqlHelper.ConnectionString, cmdText, paras);
        }
        public static DataSet Query(string cmdText, SqlParameter[] paras)
        {
            return ExecuteDataSet(cmdText, paras);
        }
        public static DataSet Query(string cmdText)
        {
            return ExecuteDataSet(cmdText);
        }
        public static DataSet ExecuteDataSet(string connectionString, string cmdText, SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Default_Timeout_Value;

                if (paras != null && paras.Length > 0)
                    cmd.Parameters.AddRange(paras);

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;////////////
                DataSet dataSet = new DataSet();
                sqlDA.Fill(dataSet);

                return dataSet;
            }
        }
        public static DataSet ExecuteDataSet(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.Connection = conn;
                cmd.CommandTimeout = Default_Timeout_Value;
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                sqlDA.Fill(dataSet ,"Anonymous");
                return dataSet;
            }
        }
        /// <summary>
        /// 判断是否存在记录数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool Exists(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql);
            object obj = ExecuteScalar(cmd);
            return Convert.ToInt32(obj) > 0 ? true : false;

        }
        public static int ExecuteSql(string p, SqlParameter[] parameters)
        {
            return ExecuteNonQuery(p, parameters, CommandType.Text);
        }
        //后增
        public static SqlDataReader ExecuteDataReader(String sql, CommandType cmty, params SqlParameter[] para)
        {
            SqlDataReader reader = null;
            SqlConnection sqlconn = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.Connection = sqlconn;
                sqlconn.Open();
                sqlcomm.CommandText = sql;
                sqlcomm.CommandType = cmty;
                if (para != null)
                {
                    sqlcomm.Parameters.AddRange(para);
                }
                reader = sqlcomm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                sqlconn.Close();
                sqlconn.Dispose();
            }

            return reader;
        }
        public static void ExecuteDataSet(string sql, SqlParameter sysNoParam)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 通过反射 DataTable转换List
        /// </summary>
        /// <typeparam name="T">需转成的泛型实体类</typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> GetList<T>(DataTable table)
        {
            IList<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (value != DBNull.Value)
                            pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
        /// <summary>
        /// 通过反射 DataTable转换List
        /// </summary>
        /// <typeparam name="T">需转成的泛型实体类</typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T GetModel<T>(DataTable table)
        {
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            t = Activator.CreateInstance<T>();
            if (table.Rows.Count == 0) return t;
            DataRow row = table.Rows[0];
            propertypes = t.GetType().GetProperties();
            foreach (PropertyInfo pro in propertypes)
            {
                tempName = pro.Name;
                if (table.Columns.Contains(tempName))
                {
                    object value = row[tempName];
                    if (value != DBNull.Value)
                        pro.SetValue(t, value, null);
                }
            }
            return t;
        }
        /// <summary>
        /// 判断是否有条件相匹配的记录
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="synid"></param>
        /// <returns></returns>
        public static bool IsHasGuid(string tablename, Guid synid)
        {
            string sql = @"select count(0)  from " + tablename + " where  synid='" + synid + "'";
            int retint = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
            return retint > 0;
        }
    }
}
