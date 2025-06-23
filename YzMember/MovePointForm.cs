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
    public partial class MovePointForm : Form
    {
        public MovePointForm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (textBox1.Text.Length == (YzMemberClass1.TBrands.Brands_CardNoLength + 2) && textBox1.Text.IndexOf(";") == 0 && textBox1.Text.LastIndexOf("?") == (YzMemberClass1.TBrands.Brands_CardNoLength + 1)) textBox1.Text = textBox1.Text.Substring(1, YzMemberClass1.TBrands.Brands_CardNoLength);

                if (textBox1.Text.Length != YzMemberClass1.TBrands.Brands_CardNoLength)
                {
                    MessageBox.Show("卡号位数错误!");
                    textBox1.SelectAll();
                    textBox1.Focus();
                    return;
                }

                if (!YzMemberClass1.IsCardNumber(textBox1.Text))
                {
                    MessageBox.Show("卡号格式错误!");
                    textBox1.SelectAll();
                    textBox1.Focus();
                    return;
                }

                    switch (YzMemberClass1.GetCardState(textBox1.Text))
                    {
                        case "0":
                            MessageBox.Show("此卡还未被开卡!");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        case "1":
                            setMemberInfo(textBox1.Text);
                            textBox1.Enabled = false;
                            button3.Enabled = true;
                            textBox2.Focus();
                            break;
                        case "2":
                            MessageBox.Show("此卡已冻结");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        case "3":
                            MessageBox.Show("此卡已注销");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        default:
                            MessageBox.Show("未知的卡!");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                    }

            }
        }


        void setMemberInfo(string strCardNo)
        {
            string strSql = "SELECT yz_Member.Member_Name,yz_Member.Member_Sex, yz_Member.Member_Phone,yz_Member.Member_RegDate, yz_Card.Card_Point,yz_CardType.CardType_Name";
            strSql += " FROM yz_Member INNER JOIN yz_Card ON yz_Member.Member_ID = yz_Card.Member_ID INNER JOIN yz_CardType ON yz_Card.CardType_ID = yz_CardType.CardType_ID";
            strSql += " Where yz_Card.Card_No = '" + strCardNo + "'";

            OleDbDataReader reader;
            OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label25.Text = reader.GetValue(5).ToString();
                    label11.Text = reader.GetValue(4).ToString();
                    label10.Text = reader.GetValue(0).ToString();
                    label9.Text = YzMemberClass1.BoolToSex(reader.GetBoolean(1));
                    label8.Text = reader.GetValue(2).ToString();
                    label7.Text = reader.GetValue(3).ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            label25.Text = "";
            label11.Text = "";
            label10.Text = "";
            label9.Text = "";
            label8.Text = "";
            label7.Text = "";
            textBox1.Text = "";
            textBox1.Focus();
            button3.Enabled = false;
        }

        void setMemberInfo2(string strCardNo)
        {
            string strSql = "SELECT yz_Member.Member_Name,yz_Member.Member_Sex, yz_Member.Member_Phone,yz_Member.Member_RegDate, yz_Card.Card_Point,yz_CardType.CardType_Name";
            strSql += " FROM yz_Member INNER JOIN yz_Card ON yz_Member.Member_ID = yz_Card.Member_ID INNER JOIN yz_CardType ON yz_Card.CardType_ID = yz_CardType.CardType_ID";
            strSql += " Where yz_Card.Card_No = '" + strCardNo + "'";

            OleDbDataReader reader;
            OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label27.Text = reader.GetValue(5).ToString();
                    label16.Text = reader.GetValue(4).ToString();
                    label15.Text = reader.GetValue(0).ToString();
                    label14.Text = YzMemberClass1.BoolToSex(reader.GetBoolean(1));
                    label13.Text = reader.GetValue(2).ToString();
                    label12.Text = reader.GetValue(3).ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (textBox2.Text.Length == (YzMemberClass1.TBrands.Brands_CardNoLength + 2) && textBox2.Text.IndexOf(";") == 0 && textBox2.Text.LastIndexOf("?") == (YzMemberClass1.TBrands.Brands_CardNoLength + 1)) textBox2.Text = textBox2.Text.Substring(1, YzMemberClass1.TBrands.Brands_CardNoLength);

                if (textBox2.Text.Length != YzMemberClass1.TBrands.Brands_CardNoLength)
                {
                    MessageBox.Show("卡号位数错误!");
                    textBox2.SelectAll();
                    textBox2.Focus();
                    return;
                }

                if (textBox2.Text.Length == YzMemberClass1.TBrands.Brands_CardNoLength && !YzMemberClass1.IsCardNumber(textBox2.Text))
                {
                    MessageBox.Show("卡号格式错误!");
                    textBox2.SelectAll();
                    textBox2.Focus();
                    return;
                }

                    switch (YzMemberClass1.GetCardState(textBox2.Text))
                    {
                        case "0":
                            MessageBox.Show("此卡还未被开通!");
                            textBox2.SelectAll();
                            textBox2.Focus();
                            break;
                        case "1":
                            setMemberInfo2(textBox2.Text);
                            textBox2.Enabled = false;
                            button4.Enabled = true;
                            break;
                        case "2":
                            MessageBox.Show("此卡已冻结");
                            textBox2.SelectAll();
                            textBox2.Focus();
                            break;
                        case "3":
                            MessageBox.Show("此卡已注销");
                            textBox2.SelectAll();
                            textBox2.Focus();
                            break;
                        default:
                            MessageBox.Show("未知的卡!");
                            textBox2.SelectAll();
                            textBox2.Focus();
                            break;
                    }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            label27.Text = "";
            label16.Text = "";
            label15.Text = "";
            label14.Text = "";
            label13.Text = "";
            label12.Text = "";
            textBox2.Text = "";
            textBox2.Focus();
            button4.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MovePointForm_Resize(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Enabled == true)
            {
                MessageBox.Show("请正确输入源卡号!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }


            if (textBox2.Enabled == true)
            {
                MessageBox.Show("请正确输入目标卡号!");
                textBox2.SelectAll();
                textBox2.Focus();
                return;
            }

            if (textBox1.Text == textBox2.Text)
            {
                MessageBox.Show("源卡号和目标卡号不能相同!");
                return;
            }

            if (label11.Text == "0.00")
            {
                MessageBox.Show("无积分可转移!");
                return;
            }

            if ((float.Parse(label11.Text) + float.Parse(label16.Text)) > 9999999)
            {
                MessageBox.Show("积分过大,不能转移!");
                return;
            }

            if (MessageBox.Show("将卡号:" + textBox1.Text + "里的所有积分转移到卡号:"+textBox2.Text,"请确认",MessageBoxButtons.OKCancel).ToString() == "OK")
            {
                string strSql = "update yz_Card set Card_Point = 0 where Card_NO = '" + textBox1.Text + "'; update yz_Card set Card_Point = Card_Point + " + label11.Text + " where Card_NO = '" + textBox2.Text + "'";
                YzMemberClass1.ExecSql(strSql);
                YzMemberClass1.WriteOperateLog("-" + label11.Text, "1", label11.Text, "7", textBox1.Text, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), textBox3.Text);
                YzMemberClass1.WriteOperateLog(label11.Text, "1", label11.Text, "7", textBox2.Text, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), textBox3.Text);
                MessageBox.Show("卡号:" + textBox1.Text + "里的所有积分转移到卡号:"+textBox2.Text + ",转移成功!");

                setMemberInfo(textBox1.Text);
                setMemberInfo2(textBox2.Text);
                textBox3.Text = "";
            }
        }

        private void MovePointForm_Load(object sender, EventArgs e)
        {

        }


    }
}
