using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;//数据库处理使用(SqlConnection)

namespace SQLServerTest_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username=this.textBoxUsername.Text.ToString();
            string userpassword=this.textBoxUserpassword.Text.ToString();
            string str = "null";

            #region SQL Server 2008数据库处理
            string connectionString = @"server=JXL-PC\SQLEXPRESS;database=user;Trusted_Connection=Yes";//数据库连接字符串(Windows身份验证)
            SqlConnection myConnection = new SqlConnection(connectionString);//创建数据库连接对象
            try
            {
                myConnection.Open();//连接数据库
            }
            catch (SqlException ex)//异常处理
            {
                //throw new Exception(ex.Message);
                MessageBox.Show(ex.Message);
            }
            string sql = string.Format("select * from userinfo where username='{0}' and userpassword='{1}'",username,userpassword);//查询sql语句
            SqlDataAdapter adapter = new SqlDataAdapter(sql, myConnection);//创建适配器对象
            DataSet ds = new DataSet();//创建数据集对象
            try
            {
                adapter.Fill(ds);//填充数据集
            }
            catch(SqlException ex)//异常处理
            {
                //throw new Exception(ex.Message);
                MessageBox.Show(ex.Message);
            }
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    str = "succeed";//登陆成功
                }
                else
                {
                    str = "fail";//登陆失败
                }
            }
            catch(IndexOutOfRangeException ex)//异常处理
            {
                //throw new Exception(ex.Message);
                MessageBox.Show(ex.Message);
            }
            myConnection.Close();//断开数据库
            #endregion

            MessageBox.Show(str);
        }
    }
}
