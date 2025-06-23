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
    public partial class GoodsInOutForm : Form
    {

        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();

        public GoodsInOutForm()
        {
            InitializeComponent();
        }

        private void GoodsInOutForm_Load(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "SELECT yz_Goods.Goods_ID as ID,yz_Goods.Goods_Name as 货品名, yz_Store.Store_Amount as 库存 FROM yz_Goods INNER JOIN yz_Store ON yz_Goods.Goods_ID = yz_Store.Goods_ID where yz_Goods.Goods_State = 1 and yz_Goods.Goods_Type = 1 and Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString();
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
                //dataSet1.Tables["a1"].PrimaryKey = new DataColumn[] { dataSet1.Tables["a1"].Columns["ID"] };

                dataGridView1.DataSource = dataSet1.Tables["a1"];
                dataGridView1.ReadOnly = true;

                dataGridView1.RowHeadersWidth = 20;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 150;

                

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
                dateTimePicker2.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT dbo.yz_GoodsInOut.GoodsInOut_ID as ID, dbo.yz_Goods.Goods_Name as 货品名, dbo.yz_GoodsInOut.GoodsInOut_Var as 进出数量,dbo.yz_GoodsInOut.GoodsInOut_Date as 操作日期,dbo.yz_Shop.Shop_Name as 门店名, dbo.yz_Administrator.Administrator_Name as 操作员, dbo.yz_Events.Events_Name as 类型 FROM dbo.yz_GoodsInOut INNER JOIN dbo.yz_Goods ON dbo.yz_GoodsInOut.Goods_ID = dbo.yz_Goods.Goods_ID INNER JOIN dbo.yz_Shop ON dbo.yz_GoodsInOut.Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString() + " INNER JOIN dbo.yz_Administrator ON dbo.yz_GoodsInOut.Administrator_ID = dbo.yz_Administrator.Administrator_ID AND yz_Shop.Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString() + " INNER JOIN dbo.yz_AdministratorType ON  dbo.yz_Administrator.AdministratorType_ID = dbo.yz_AdministratorType.AdministratorType_ID INNER JOIN dbo.yz_Events ON dbo.yz_GoodsInOut.Events_ID = dbo.yz_Events.Events_ID";

            strSql += " where (yz_GoodsInOut.Events_ID = 1 or yz_GoodsInOut.Events_ID = 3) and (GoodsInOut_Date >= '" + dateTimePicker1.Value.Date.ToString() + "' and GoodsInOut_Date < '" + dateTimePicker2.Value.AddDays(1).ToString() + "')";

            int i = dataGridView1.SelectedRows.Count;

            if (i != 0)
            {
                strSql += "and (";
            }

            for (int j = 0; j < i; j++)
            {
                if (j > 0) strSql += " or ";
                strSql += "(yz_Goods.Goods_ID = " + dataGridView1.SelectedRows[j].Cells[0].Value.ToString() + ")";
                
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

                if (dataSet2.Tables["a1"].Rows.Count == 0)
                {
                    MessageBox.Show("没有发现符合条件的记录!");
                }
                else
                {
                    MessageBox.Show("查询完毕,发现" + dataSet2.Tables["a1"].Rows.Count.ToString() + "条符合条件的记录!");
                }

                dataGridView2.DataSource = dataSet2.Tables["a1"];
                dataGridView2.ReadOnly = true;

                dataGridView2.RowHeadersWidth = 20;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Width = 150;
                dataGridView2.Columns[3].Width = 110;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count != 0)
            {
                textBox1.Text = YzMemberClass1.ExecSqlReturn("select GoodsInOut_Notice from yz_GoodsInOut where GoodsInOut_ID = " + dataGridView2.CurrentRow.Cells[0].Value.ToString());
            }
        }
    }
}
