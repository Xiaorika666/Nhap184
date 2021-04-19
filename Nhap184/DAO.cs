using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;



namespace Nhap184
{
    class DAO
    {
        public static SqlConnection Con;
        public static string conString;
        public static void OpenCon()
        {
            conString = "Data Source=ADMIN;Initial Catalog=Sachnenene;Integrated Security=True";
            Con = new SqlConnection();
            Con.ConnectionString = conString;
            Con.Open();
        }
        public static void CloseCon()
        {
            if(Con.State==ConnectionState.Open)
            {
                Con.Close();
                Con.Dispose();
                Con = null;
            }
        }
        public static DataTable GetDataToTable(string sql)
        {
            OpenCon();

            SqlDataAdapter mydata = new SqlDataAdapter();
            mydata.SelectCommand = new SqlCommand();
            mydata.SelectCommand.Connection = DAO.Con;
            mydata.SelectCommand.CommandText = sql;
            DataTable table = new DataTable(); // cai table nay rong

            mydata.Fill(table);
            CloseCon();
            return table;
        }
        public static void FillCombo (string sql,ComboBox cbo,string ma,string ten)
        {
            OpenCon();
            SqlDataAdapter mydata = new SqlDataAdapter(sql,Con);
            DataTable table = new DataTable();
            mydata.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
            CloseCon();
        }
        public static bool CheckKey(string sql)
        {
            OpenCon();
            SqlDataAdapter mydata = new SqlDataAdapter(sql, DAO.Con);
            DataTable table = new DataTable();
            mydata.Fill(table);
            CloseCon();
            if (table.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public static void RunSql(string sql)
        {
            OpenCon();
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Connection = DAO.Con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
                   
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            cmd.Dispose();
            cmd = null;
            CloseCon();
        }

    }
}
