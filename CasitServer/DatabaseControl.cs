using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows;
using MySql.Data.MySqlClient;

namespace CasitServer
{
    class DatabaseControl
    {
        #region mysql read and write
        #region databaseconnect
        private DataSet dsall;
        private MySqlConnection conn;
        private MySqlDataAdapter mdap;
        static public string T_userInfo = "user";
        //static string mysqlConnect = "server=localhost;database = teeair;uid=root;pwd = root;charset = utf8;prot=3306";
        static string mysqlConnect = "server=localhost;database = teeair;uid=root;pwd = root";
        #endregion
        #region user
        public void mysqlconnect()
        {
            conn = new MySqlConnection(mysqlConnect);
            conn.Open();
        }
        public DataSet GetUserInfo()
        {
            string sqltext = "select * from " + T_userInfo;
            using(MySqlConnection sqlcon = new MySqlConnection(mysqlConnect))
            {
                using(MySqlDataAdapter sqldata = new MySqlDataAdapter(sqltext,mysqlConnect))
                {
                    DataSet ds = new DataSet();
                    ds.Clear();
                    sqldata.Fill(ds, T_userInfo);
                    return ds;
                }
            }
        }
        #endregion
        #region publicfun
        public string GetTableNameUserInfomation()
        {
            return T_userInfo;
        }
        public string Login(string Uid, string Password, string TableName)
        {
            using(MySqlConnection sqlcon = new MySqlConnection(mysqlConnect))
            {
                sqlcon.Open();
                string sqltext = "select * from " + TableName + " where id='" + Uid + "'";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sqltext, sqlcon);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    if (TableName == T_userInfo)
                    {
                        if (Password == rdr[14].ToString())
                            return "Success";
                        else
                            return "key error";
                    }
                    //else if (TableName == DataTable_tbAdministrator)
                    //{
                    //    if (Password == rdr[1].ToString())
                    //        return "Success";
                    //    else
                    //        return "key error";
                    //}
                    else
                        return "key error";
                }
                catch
                {
                    return "user error";
                }
            }
        }
        public Boolean CheckSameID(string Keyword, string ColumnName, string tablename)///查看在tablename表中是否存在属性columnname值为keyword的项
        {
            using (MySqlConnection sqlcon = new MySqlConnection(mysqlConnect))
            {
                try
                {
                    string sqltext = "select * from " + tablename + " where " + ColumnName + "='" + Keyword + "'";
                    using (MySqlDataAdapter sqldata = new MySqlDataAdapter(sqltext, sqlcon))
                    {
                        DataSet ds = new DataSet();
                        ds.Clear();
                        sqldata.Fill(ds, tablename);
                        if (ds.Tables[0].Rows.Count < 1)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                catch (Exception exc)
                {
                    return false;
                }
            }
        }
        public bool AddUser(string[] ud)///在数据库中注册新用户
        {
            try
            {
                using (MySqlConnection sqlcon = new MySqlConnection(mysqlConnect))
                {
                    //MessageBox.Show("开始连接");
                    sqlcon.Open();
                    //MessageBox.Show("打开数据库成功");
                    string sqltext = "insert into " + T_userInfo + "(id,name,pwd,reason,tm,tp,registertime) values('" + ud[0] + "','" + ud[1] + "','" + ud[2] + "','" + ud[3] + "','" + 10 + "','" + 10 + "','" + DateTime.Now.ToString() + "')";
                    MySqlCommand sqlcmd = new MySqlCommand(sqltext, sqlcon);
                    sqlcmd.ExecuteNonQuery();
                    //MessageBox.Show("执行成功");
                    return true;
                }
            }
            catch (Exception exc)
            {
                return false;
                //MessageBox.Show(e.Message);
            }
        }
        #endregion
        #endregion
    }
}
