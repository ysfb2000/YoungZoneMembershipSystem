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
    public partial class OperationListForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();
        
        

        public OperationListForm()
        {
            InitializeComponent();
        }

        private void OperationListForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Now;

            try
            {
                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "SELECT dbo.yz_Administrator.Administrator_ID, dbo.yz_Administrator.Administrator_Name as 姓名,dbo.yz_AdministratorType.AdministratorType_Name as 身份 FROM dbo.yz_Administrator INNER JOIN dbo.yz_AdministratorType ON dbo.yz_Administrator.AdministratorType_ID = dbo.yz_AdministratorType.AdministratorType_ID where yz_Administrator.Administrator_State = 1";
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");

                cmd.CommandText = "select Shop_ID,Shop_Name from yz_Shop where Shop_State = 1";
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            comboBox1.DataSource = dataSet1.Tables["a2"];
            comboBox1.ValueMember = "Shop_ID";
            comboBox1.DisplayMember = "Shop_Name";
            comboBox1.SelectedValue = YzMemberClass1.TAdministrator.Shop;
            if (YzMemberClass1.TAdministrator.Level == 1 || YzMemberClass1.TAdministrator.Level == 2) comboBox1.Enabled = true;

            dataGridView1.DataSource = dataSet1.Tables["a1"];
            dataGridView1.ReadOnly = true;

            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataSet1.Tables.Add("a3");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT dbo.yz_OperateLog.OperateLog_ID as 单据号,dbo.yz_OperateLog.Card_No as 卡号, dbo.yz_Operation.Operation_Name as 类型, dbo.yz_OperateLog.OperateLog_Date as 日期,  dbo.yz_Goods.Goods_Name 货品名,dbo.yz_OperateLog.OperateLog_GoodsAmount as 数量, dbo.yz_OperateLog.OperateLog_ChangVar as 积分, dbo.yz_Administrator.Administrator_Name as 操作员, dbo.yz_Shop.Shop_Name as 门店 FROM dbo.yz_Operation INNER JOIN dbo.yz_OperateLog ON dbo.yz_Operation.Operation_ID = dbo.yz_OperateLog.Operation_ID INNER JOIN dbo.yz_Shop ON dbo.yz_OperateLog.Shop_ID = dbo.yz_Shop.Shop_ID INNER JOIN dbo.yz_Administrator ON dbo.yz_OperateLog.Administrator_ID = dbo.yz_Administrator.Administrator_ID INNER JOIN dbo.yz_Goods ON dbo.yz_OperateLog.Goods_ID = dbo.yz_Goods.Goods_ID";

            strSql += " where (dbo.yz_OperateLog.OperateLog_Date >= '" + dateTimePicker1.Value.Date.ToString() + "' and dbo.yz_OperateLog.OperateLog_Date < '" + dateTimePicker2.Value.AddDays(1).Date.ToString() + "') and (yz_OperateLog.Shop_ID = " + comboBox1.SelectedValue.ToString() + ")";


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

            cmd = new OleDbCommand(strSql, conn);
            da.SelectCommand = cmd;


            try
            {
                dataSet1.Tables["a3"].Clear();
                da.Fill(dataSet1, "a3");
                if (dataSet1.Tables["a3"].Rows.Count == 0)
                {
                    MessageBox.Show("没有符合条件的记录!");
                }
                else
                {
                    MessageBox.Show("查询完毕,共有" + dataSet1.Tables["a3"].Rows.Count.ToString() + "条符合条件的记录!");
                }
                dataSet1.Tables["a3"].PrimaryKey = new DataColumn[] { dataSet1.Tables["a3"].Columns["ID"] };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            dataGridView2.DataSource = dataSet1.Tables["a3"];
            dataGridView2.ReadOnly = true;

            dataGridView2.RowHeadersWidth = 20;
            dataGridView2.Columns[0].Width = 80;
            dataGridView2.Columns[1].Width = 65;
            dataGridView2.Columns[2].Width = 90;
            dataGridView2.Columns[3].Width = 100;
            dataGridView2.Columns[4].Width = 120;
            dataGridView2.Columns[5].Width = 60;
            dataGridView2.Columns[6].Width = 60;
            dataGridView2.Columns[7].Width = 100;



            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {



        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count != 0)
            {
                textBox1.Text = YzMemberClass1.ExecSqlReturn("select OperateLog_Notice from yz_OperateLog where OperateLog_ID = " + dataGridView2.CurrentRow.Cells[0].Value.ToString());
            }

        }
    }
}
