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
    public partial class StoreListForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();

        public StoreListForm()
        {
            InitializeComponent();
        }

        private void StoreListForm_Load(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "SELECT yz_Goods.Goods_ID as ID,yz_Goods.Goods_Name as 货品名, yz_Store.Store_Amount as 库存,yz_Goods.Goods_Value as 对应积分,yz_Goods.Goods_Type as 类型 FROM yz_Goods INNER JOIN yz_Store ON yz_Goods.Goods_ID = yz_Store.Goods_ID where yz_Goods.Goods_State = 1 and Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString();
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
                //dataSet1.Tables["a1"].PrimaryKey = new DataColumn[] { dataSet1.Tables["a1"].Columns["ID"] };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            dataGridView1.DataSource = dataSet1.Tables["a1"];
            dataGridView1.ReadOnly = true;

            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 60; 
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 60;
            
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}
