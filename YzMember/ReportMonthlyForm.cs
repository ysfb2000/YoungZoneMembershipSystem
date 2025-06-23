using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Reflection;

namespace YzMember
{
    public partial class ReportMonthlyForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        public ReportMonthlyForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ReportMonthlyForm_Load(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSql = "select Shop_Name,IsNull(expr1,0) from dbo.yz_Shop left join (select Shop_ID,count(*) as expr1 from yz_OperateLog where (dbo.yz_OperateLog.Operation_ID = 1) AND (DATEDIFF(month, dbo.yz_OperateLog.OperateLog_Date, '" + dateTimePicker1.Text + "/1') = 0) group by Shop_ID) as m on m.Shop_ID = dbo.yz_Shop.Shop_ID";
            DataTable tb1 = LoadData(strSql);
            string s = "当月新增会员:\r\n";
            int intTotal = 0;
            foreach (DataRow item in tb1.Rows)
            {
                s += item[0].ToString() + "   " + item[1].ToString() + "\r\n";
                intTotal += int.Parse(item[1].ToString());
            }

            s += "总计:" + intTotal.ToString() + "\r\n\r\n";


            strSql = "select Shop_Name,IsNull(expr1,0) from dbo.yz_Shop left join (select Shop_ID,count(*) as expr1 from yz_OperateLog where (dbo.yz_OperateLog.Operation_ID = 2) AND (DATEDIFF(month, dbo.yz_OperateLog.OperateLog_Date, '" + dateTimePicker1.Text + "/1') = 0) group by Shop_ID) as m on m.Shop_ID = dbo.yz_Shop.Shop_ID";
            tb1 = LoadData(strSql);

            s += "当月活跃会员:\r\n";
            intTotal = 0;
            foreach (DataRow item in tb1.Rows)
            {
                s += item[0].ToString() + "   " + item[1].ToString() + "\r\n";
                intTotal += int.Parse(item[1].ToString());
            }

            s += "总计:" + intTotal.ToString() + "\r\n\r\n";


            strSql = "select count(*) from yz_Member where Member_State = 1";
            string intTotalMember = YzMemberClass1.ExecSqlReturn(strSql);

            strSql = "select Shop_Name,cast(cast(IsNull(expr1,0) as float)/" + intTotalMember + "*100 as numeric(8,2)) from dbo.yz_Shop left join (select Shop_ID,count(*) as expr1 from yz_OperateLog where (dbo.yz_OperateLog.Operation_ID = 2) AND (DATEDIFF(month, dbo.yz_OperateLog.OperateLog_Date, '" + dateTimePicker1.Text + "/1') = 0) group by Shop_ID) as m on m.Shop_ID = dbo.yz_Shop.Shop_ID";
            tb1 = LoadData(strSql);

            float flTotal = 0;



            s += "当月活跃会员占比:\r\n";
            intTotal = 0;
            foreach (DataRow item in tb1.Rows)
            {
                s += item[0].ToString() + "   " + item[1].ToString() + "%\r\n";
                flTotal += float.Parse(item[1].ToString());
            }

            s += "总计:" + flTotal.ToString() + "%\r\n\r\n";


            textBox1.Text = s;
        }

        private DataTable LoadData(string strCmd)
        {
            cmd.Connection = conn;
            cmd.CommandText = strCmd;
            da.SelectCommand = cmd;
            DataSet dataSet = new DataSet();

            da.Fill(dataSet, "a1");

            return dataSet.Tables["a1"];
        }
    }
}
