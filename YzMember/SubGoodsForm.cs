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
    public partial class SubGoodsForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();

        string strID="";

        public SubGoodsForm()
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
                cmd.CommandText = "SELECT yz_Goods.Goods_ID as ID,yz_Goods.Goods_Name as 货品名, yz_Store.Store_Amount as 库存 FROM yz_Goods INNER JOIN yz_Store ON yz_Goods.Goods_ID = yz_Store.Goods_ID where yz_Goods.Goods_State = 1 and yz_Goods.Goods_Type = 1 and Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString();
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

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 140;
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
            if (dataGridView1.Rows.Count != 0)
            {
                label4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                label5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                strID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (strID == "")
            {
                MessageBox.Show("请选择要退的货品!");
                return;
            }

            if (!YzMemberClass1.IsNoZeroNumber(textBox1.Text))
            {
                MessageBox.Show("请输入正确的退货数量!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }


            if (int.Parse(textBox1.Text) > int.Parse(label5.Text))
            {
                MessageBox.Show("退货数量不能大于库存!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            YzMemberClass1.AddGoods(YzMemberClass1.TAdministrator.Shop.ToString(),"-"+textBox1.Text,strID,YzMemberClass1.TAdministrator.ID.ToString(),"3",textBox3.Text);

            string s = label4.Text + "的库存减少" + textBox1.Text + ".";
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
                cmd.CommandText = "SELECT yz_Goods.Goods_ID as ID,yz_Goods.Goods_Name as 货品名, yz_Store.Store_Amount as 库存 FROM yz_Goods INNER JOIN yz_Store ON yz_Goods.Goods_ID = yz_Store.Goods_ID where yz_Goods.Goods_State = 1 and yz_Goods.Goods_Type = 1 and Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString();
                da.SelectCommand = cmd;
                dataSet1.Clear();
                da.Fill(dataSet1, "a1");
                //dataSet1.Tables["a1"].PrimaryKey = new DataColumn[] { dataSet1.Tables["a1"].Columns["ID"] };

                dataGridView1.DataSource = dataSet1.Tables["a1"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
