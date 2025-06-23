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
    public partial class MakeCardForm : Form
    {

        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbDataAdapter da2 = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();

        public MakeCardForm()
        {
            InitializeComponent();
        }

        private void MakeCardForm_Load(object sender, EventArgs e)
        {
            try
            {

                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "SELECT CardType_ID,CardType_Name from yz_CardType";
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            comboBox1.DataSource = dataSet1.Tables["a1"];
            comboBox1.ValueMember = "CardType_ID";
            comboBox1.DisplayMember = "CardType_Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            if (!YzMemberClass1.IsCardNumber(textBox1.Text))
            {
                MessageBox.Show("请输入正确卡号!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            if (!YzMemberClass1.IsCardNumber(textBox2.Text))
            {
                MessageBox.Show("请输入正确卡号!");
                textBox2.SelectAll();
                textBox2.Focus();
                return;
            }


            Int64 i = YzMemberClass1.CardNoToInt(textBox1.Text);
            Int64 j = YzMemberClass1.CardNoToInt(textBox2.Text);

            if (i > j)
            {
                MessageBox.Show("起始卡号不能大于结束卡号!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            Int64 k = j - i;
            if (k == 0) k = 1;
            Int64 l = 0;
            Int64 m = 0;

            if (MessageBox.Show("现在开始制卡\r\n\r\n起始卡号:" + textBox1.Text + "\r\n结束卡号:" + textBox2.Text + "\r\n\r\n总共生成" + (j - i + 1).ToString() + "张卡.", "请确认", MessageBoxButtons.OKCancel).ToString() == "OK")
            {
                string s2 = YzMemberClass1.GetIntValueByCardTypeID(comboBox1.SelectedValue.ToString());

                while (i <= j)
                {

                    progressBar1.Value = (int)Math.Round((double)l / (double)k * 100);

                    string s = i.ToString().PadLeft(YzMemberClass1.TBrands.Brands_CardNoLength, '0');
                    if (YzMemberClass1.ExecSqlReturn("select 1 from yz_Card where Card_No = '" + s + "'") == "1")
                    {
                        textBox3.Text += "卡号:" + s + "已经制过卡.\r\n";
                    }
                    else
                    {
                        YzMemberClass1.ExecSql("insert into yz_Card(Card_No,CardType_ID,Card_Point,Card_State) values('" + s + "'," + comboBox1.SelectedValue.ToString() + "," + s2 + ",0)");
                        m++;
                    }

                    l++;
                    i++;
                }

                MessageBox.Show("制卡完成!\r\n\r\n起始卡号:" + textBox1.Text + "\r\n结束卡号:" + textBox2.Text + "\r\n\r\n成功生成" + m.ToString() + "张卡.");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
            else
            {
 
            }
        }

        private void MakeCardForm_Resize(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            progressBar1.Value = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                dataSet2.Tables.Clear();
                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "SELECT Card_NO from yz_Card";
                da2.SelectCommand = cmd;
                da2.Fill(dataSet2, "a1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            DataTable dt = dataSet2.Tables["a1"];

            int i = 0;
            Int64 j = dt.Rows.Count;

            Int64 k = 0;
            Int64 m = 0;
            Int64 n = 0;


            for (i = 0; i < j; i++)
            {
                k = YzMemberClass1.CardNoToInt(dt.Rows[i].ItemArray[0].ToString());

                if (m + 1 == k)
                {
                    m = k;
                    if (m == 1) n = 1;
                }
                else
                {
                    listBox1.Items.Add(n.ToString().PadLeft(YzMemberClass1.TBrands.Brands_CardNoLength, '0') + "-" + m.ToString().PadLeft(YzMemberClass1.TBrands.Brands_CardNoLength, '0'));
                    n = k;
                    m = k;
                }
            }
            listBox1.Items.Add(n.ToString().PadLeft(YzMemberClass1.TBrands.Brands_CardNoLength, '0') + "-" + m.ToString().PadLeft(YzMemberClass1.TBrands.Brands_CardNoLength, '0'));

               
        }
    }
}
