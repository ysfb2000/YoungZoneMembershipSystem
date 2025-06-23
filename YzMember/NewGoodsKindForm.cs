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
    public partial class NewGoodsKindForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();

        public NewGoodsKindForm()
        {

            InitializeComponent();
        }

        private void NewGoodsKindForm_Load(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("", conn);
            cmd.CommandText = "select GoodsType_ID,GoodsType_Name from yz_GoodsType";
            da.SelectCommand = cmd;
            da.Fill(dataSet1, "a1");

            comboBox1.DataSource = dataSet1.Tables["a1"];
            comboBox1.DisplayMember = "GoodsType_Name";
            comboBox1.ValueMember = "GoodsType_ID";

            comboBox2.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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

            string strSql = "insert into yz_Goods(Goods_Name,Goods_Value,Goods_Type,Goods_Level) values('" + textBox1.Text.Trim() + "'," + s2 + "," + comboBox1.SelectedValue.ToString() + "," + comboBox2.SelectedIndex.ToString() + ");select SCOPE_IDENTITY()";
            if (MessageBox.Show("你确定要新增" + textBox1.Text + "这个货品吗?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string s = YzMemberClass1.ExecSqlReturn(strSql);
                YzMemberClass1.BuildGoodsInShop(s);

                MessageBox.Show("商品种类新增成功!");
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "9", YzMemberClass1.TAdministrator.Shop.ToString());
                DialogResult = DialogResult.OK;
            }
        }
    }
}
