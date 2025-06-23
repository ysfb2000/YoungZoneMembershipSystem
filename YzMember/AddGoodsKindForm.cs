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
    public partial class AddGoodsKindForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbDataAdapter da2 = new OleDbDataAdapter();
        OleDbCommand cmd;

        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();
        string strTemp = "";

        public AddGoodsKindForm()
        {
            InitializeComponent();
        }

        private void AddGoodsKindForm_Load(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "select Goods_ID as ID,Goods_Name as 货品名,Goods_Value as 积分,GoodsType_Name as 类型,Goods_Level as 等级,Goods_State as 状态 from  dbo.yz_Goods INNER JOIN  dbo.yz_GoodsType ON dbo.yz_Goods.Goods_Type = dbo.yz_GoodsType.GoodsType_ID where Goods_Type<>0";
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
                dataSet1.Tables["a1"].PrimaryKey = new DataColumn[] { dataSet1.Tables["a1"].Columns["ID"] };

                dataGridView1.DataSource = dataSet1.Tables["a1"];
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.RowHeadersWidth = 20;
                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 160;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 60;

                cmd.CommandText = "select GoodsType_ID,GoodsType_Name from yz_GoodsType";
                da2.SelectCommand = cmd;
                da2.Fill(dataSet2, "a1");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            comboBox1.DataSource = dataSet2.Tables["a1"];
            comboBox1.DisplayMember = "GoodsType_Name";
            comboBox1.ValueMember = "GoodsType_ID";

            comboBox2.Items.Insert(0, "0");
            comboBox2.Items.Insert(1, "1");
            comboBox2.Items.Insert(2, "2");
            comboBox2.Items.Insert(3, "3");
            comboBox2.Items.Insert(4, "4");

            button2.Enabled = false;

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            strTemp = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells["货品名"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["积分"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["类型"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["等级"].Value.ToString();

            if (dataGridView1.CurrentRow.Cells["状态"].Value.ToString() == "1") 
                radioButton1.Select();
            else
                radioButton2.Select();

            EnableInput();
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewGoodsKindForm form1 = new NewGoodsKindForm();
            if (form1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    cmd.CommandText = "select Goods_ID as ID,Goods_Name as 货品名,Goods_Value as 积分,GoodsType_Name as 类型,Goods_Level as 等级,Goods_State as 状态 from  dbo.yz_Goods INNER JOIN  dbo.yz_GoodsType ON dbo.yz_Goods.Goods_Type = dbo.yz_GoodsType.GoodsType_ID where Goods_Type<>0";
                    da.SelectCommand = cmd;
                    da.Fill(dataSet1, "a1");
                    ClearInput();
                    DisableInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void ClearInput()
        {
            textBox1.Text = "";
            textBox2.Text = "";

            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            strTemp = "";
        }

        private void EnableInput()
        {
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;

            textBox1.Enabled = true; ;
            textBox2.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            button4.Enabled = true;
        }

        private void DisableInput()
        {
            textBox1.BackColor = SystemColors.ButtonFace;
            textBox2.BackColor = SystemColors.ButtonFace;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("货品名必须输入!");
                return;
            }

            if (!YzMemberClass1.IsAdministraotName(textBox1.Text))
            {
                MessageBox.Show("货品名中有非法字符!");
                return;
            }

            if (!YzMemberClass1.IsFloatNumber(textBox2.Text))
            {
                MessageBox.Show("积分错误!");
                return;
            }

            string s2 = textBox2.Text;
            if (comboBox1.SelectedValue.ToString() == "3" && s2.Substring(0, 1) == "-")
            {
                MessageBox.Show("赠送积分类商品积分不能为负数!");
                textBox2.SelectAll();
                textBox2.Focus();
                return;
            };

            if (comboBox1.SelectedValue.ToString() != "3" && s2.Substring(0, 1) != "-")
            {
                MessageBox.Show("兑换类商品积分应该为负数!");
                textBox2.SelectAll();
                textBox2.Focus();
                return;
            }; 

            string s="";
            if (radioButton1.Checked == true)
                s = "1";
            else
                s="0";

            try
            {
                cmd.CommandText = "update yz_Goods set Goods_Name = ?,Goods_Value = ?,Goods_Type = ?,Goods_Level = ?,Goods_State = ? where Goods_ID = " + strTemp;
                OleDbParameter[] p = {new OleDbParameter("@Goods_Name",textBox1.Text),new OleDbParameter("@Goods_value",textBox2.Text),new OleDbParameter("@Goods_Type",comboBox1.SelectedValue.ToString()),new OleDbParameter("@Goods_Level",comboBox2.Text),new OleDbParameter("@Goods_State",s)};
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(p);
                
                if (MessageBox.Show("是否确定要保存修改货品信息?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "10", YzMemberClass1.TAdministrator.Shop.ToString());

                    cmd.CommandText = "select Goods_ID as ID,Goods_Name as 货品名,Goods_Value as 积分,GoodsType_Name as 类型,Goods_Level as 等级,Goods_State as 状态 from  dbo.yz_Goods INNER JOIN  dbo.yz_GoodsType ON dbo.yz_Goods.Goods_Type = dbo.yz_GoodsType.GoodsType_ID where Goods_Type<>0";
                    da.SelectCommand = cmd;
                    dataSet1.Tables["a1"].Clear();
                    da.Fill(dataSet1, "a1");
 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            MessageBox.Show("保存修改成功!");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ClearInput();
            DisableInput();
        }

        

    }
}
