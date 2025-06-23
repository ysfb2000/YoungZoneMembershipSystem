using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YzMember
{
    public partial class AdminLogListForm : Form
    {

        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();

        public AdminLogListForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT dbo.yz_AdminLog.AdminLog_DateTime as 发生时间, dbo.yz_Shop.Shop_Name as 门店名,dbo.yz_Administrator.Administrator_Name as 姓名,dbo.yz_AdministratorType.AdministratorType_Name as 身份, dbo.yz_AdminType.AdminType_Name as 操作类型 FROM dbo.yz_AdminLog INNER JOIN dbo.yz_AdminType ON dbo.yz_AdminLog.AdminType_ID = dbo.yz_AdminType.AdminType_ID INNER JOIN dbo.yz_Administrator ON dbo.yz_AdminLog.Administrator_ID = dbo.yz_Administrator.Administrator_ID INNER JOIN dbo.yz_AdministratorType ON dbo.yz_Administrator.AdministratorType_ID = dbo.yz_AdministratorType.AdministratorType_ID INNER JOIN dbo.yz_Shop ON dbo.yz_AdminLog.Shop_ID = dbo.yz_Shop.Shop_ID";

            strSql += " where (AdminLog_DateTime >= '" + dateTimePicker1.Value.Date.ToString() + "' and AdminLog_DateTime < '" + dateTimePicker2.Value.AddDays(1).Date.ToString() + "')";


            int i = dataGridView1.SelectedRows.Count;

            if (i != 0)
            {
                strSql += "and (";
            }

            for (int j = 0; j < i; j++)
            {
                if (j > 0) strSql += " or ";
                strSql += "(yz_Administrator.Administrator_ID = " + dataGridView1.SelectedRows[j].Cells[0].Value.ToString() + ")";
                
            }

            if (i != 0)
            {
                strSql += ")";
            }

            try
            {
                cmd = new OleDbCommand(strSql, conn);
                da.SelectCommand = cmd;
                dataSet2.Clear();
                da.Fill(dataSet2, "a1");

                dataGridView2.DataSource = dataSet2.Tables["a1"];
                dataGridView2.ReadOnly = true;

                dataGridView2.RowHeadersWidth = 20;
                dataGridView2.Columns[0].Width = 100;
                dataGridView2.Columns[1].Width = 120;
                dataGridView2.Columns[2].Width = 120;
                dataGridView2.Columns[3].Width = 100;
                dataGridView2.Columns[4].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }



            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void AdminLogListForm_Load_1(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "SELECT dbo.yz_Administrator.Administrator_ID, dbo.yz_Administrator.Administrator_Name as 姓名,dbo.yz_AdministratorType.AdministratorType_Name as 身份 FROM dbo.yz_Administrator INNER JOIN dbo.yz_AdministratorType ON dbo.yz_Administrator.AdministratorType_ID = dbo.yz_AdministratorType.AdministratorType_ID where yz_Administrator.Administrator_State = 1";
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


            dataGridView1.DataSource = dataSet1.Tables["a1"];
            dataGridView1.ReadOnly = true;

            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Now;
        }
    }
}
