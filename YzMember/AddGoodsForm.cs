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
    public partial class AddGoodsForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();

        string strID="";

        public AddGoodsForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddGoodsForm_Load(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "SELECT yz_Goods.Goods_ID as ID,yz_Goods.Goods_Name as 货品名, yz_Store.Store_Amount as 库存,yz_Goods.Goods_Type as 类型 FROM yz_Goods INNER JOIN yz_Store ON yz_Goods.Goods_ID = yz_Store.Goods_ID where yz_Goods.Goods_State = 1 and yz_Goods.Goods_Type = 1 and Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString();
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
                //dataSet1.Tables["a1"].PrimaryKey = new DataColumn[] { dataSet1.Tables["a1"].Columns["ID"] };

                dataGridView1.DataSource = dataSet1.Tables["a1"];
                dataGridView1.ReadOnly = true;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dataGridView1.RowHeadersWidth = 20;
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 160;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (strID == "")
            {
                MessageBox.Show("请选择要进的货品!");
                return;
            }

            if (!YzMemberClass1.IsNoZeroNumber(textBox1.Text))
            {
                MessageBox.Show("请输入正确的进货数量!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            if (MessageBox.Show("确定" + label4.Text + "进货" + textBox1.Text + "吗?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                YzMemberClass1.AddGoods(YzMemberClass1.TAdministrator.Shop.ToString(), textBox1.Text, strID, YzMemberClass1.TAdministrator.ID.ToString(), "1", textBox3.Text);

                string s = label4.Text + "的库存增加" + textBox1.Text + ".";
                MessageBox.Show(s);
                s = s + "\r\n";
                textBox2.Text += s;


                strID = "";
                label4.Text = "";
                label5.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";

                try
                {
                    cmd = new OleDbCommand("", conn);
                    cmd.CommandText = "SELECT yz_Goods.Goods_ID as ID,yz_Goods.Goods_Name as 货品名, yz_Store.Store_Amount as 库存,yz_Goods.Goods_Type as 类型 FROM yz_Goods INNER JOIN yz_Store ON yz_Goods.Goods_ID = yz_Store.Goods_ID where yz_Goods.Goods_State = 1 and yz_Goods.Goods_Type = 1 and Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString();
                    da.SelectCommand = cmd;
                    dataSet1.Clear();
                    da.Fill(dataSet1, "a1");
                    //dataSet1.Tables["a1"].PrimaryKey = new DataColumn[] { dataSet1.Tables["a1"].Columns["ID"] };

                    dataGridView1.DataSource = dataSet1.Tables["a1"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                label4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                label5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                strID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
        }
    }
}
