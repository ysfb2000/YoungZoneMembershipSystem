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
    public partial class ActivedGoodsForm : Form
    {

        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;

        public ActivedGoodsForm()
        {
            InitializeComponent();
        }

        private void ActivedGoodsForm_Load(object sender, EventArgs e)
        {
            DataSet dataSet1 = new DataSet();
            cmd = new OleDbCommand("", conn);
            cmd.CommandText = "select Goods_ID as ID,Goods_Name as 货品名,Goods_Value as 相对积分,(case Goods_Type when 1 then '有限货品' when 2 then '无限货品' when 3 then '获赠积分' end) as 类型,Goods_Level as 级别 from yz_Goods Where Goods_State = 1 and Goods_Type <> 0";
            da.SelectCommand = cmd;
            try
            {
                da.Fill(dataSet1, "a1");
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            this.dataGridView1.DataSource = dataSet1.Tables["a1"];

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 160;



        }
    }
}
