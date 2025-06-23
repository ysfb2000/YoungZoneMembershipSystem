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
    public partial class ReportLostForm : Form
    {
        public ReportLostForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CancelCardForm_Resize(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Length == YzMemberClass1.TBrands.Brands_CardNoLength && YzMemberClass1.IsNumber(textBox1.Text))
                {
                    //检验卡号是否可用
                    switch (YzMemberClass1.GetCardState(textBox1.Text))
                    {
                        case "0":
                            MessageBox.Show("此卡还未被开通!");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        case "1":
                            setMemberInfo(textBox1.Text);
                            textBox1.Enabled = false;
                            button3.Enabled = true;
                            break;
                        case "2":
                            MessageBox.Show("此卡已挂失!");
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

                if (textBox1.Text.Length == (YzMemberClass1.TBrands.Brands_CardNoLength + 2 ))
                {
                    //检验卡号是否可用
                    if (YzMemberClass1.IsScanNumber(textBox1.Text))
                    {
                        string s = textBox1.Text.Substring(1, YzMemberClass1.TBrands.Brands_CardNoLength);
                        textBox1.Text = s;
                        switch (YzMemberClass1.GetCardState(s))
                        {
                            case "0":
                                MessageBox.Show("此卡还未被开通!");
                                textBox1.SelectAll();
                                textBox1.Focus();
                                break;
                            case "1":
                                setMemberInfo(textBox1.Text);
                                textBox1.Enabled = false;
                                button3.Enabled = true;
                                break;
                            case "2":
                                MessageBox.Show("此卡已挂失!");
                                textBox1.SelectAll();
                                textBox1.Focus();
                                break;
                            case "3":
                                MessageBox.Show("此卡已注销!");
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
                    else
                    {
                        MessageBox.Show("卡号格式错误!");
                    }
                }

                if (textBox1.Text.Length != YzMemberClass1.TBrands.Brands_CardNoLength && textBox1.Text.Length != (YzMemberClass1.TBrands.Brands_CardNoLength+2))
                {
                    MessageBox.Show("卡号位数错误!");
                    textBox1.SelectAll();
                    textBox1.Focus();
                    return;
                }
            }



        }

        void setMemberInfo(string strCardNo)
        {
            string strSql = "SELECT yz_Member.Member_Name,yz_Member.Member_Sex, yz_Member.Member_Phone,yz_Member.Member_RegDate, yz_Card.Card_Point,yz_CardType.CardType_Name,yz_Card.Card_State";
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
                    label13.Text = YzMemberClass1.GetCardStateByStateNo(reader.GetValue(6).ToString());
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
            label25.Text = "";
            label11.Text = "";
            label10.Text = "";
            label9.Text = "";
            label8.Text = "";
            label7.Text = "";
            label13.Text = "";

            textBox1.Text = "";
            textBox1.Enabled = true;
            button3.Enabled = false;
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Enabled == true)
            {
                MessageBox.Show("请正确输入卡号!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            if (MessageBox.Show("卡号:" + textBox1.Text + "是否开始挂失?", "请确认", MessageBoxButtons.OKCancel).ToString() == "OK")
            {
                string strSql = "update yz_Card set Card_State = 2 where Card_NO = '" + textBox1.Text + "'";
                YzMemberClass1.ExecSql(strSql);
                YzMemberClass1.WriteOperateLog("0", "1", "0", "5", textBox1.Text, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), textBox2.Text);
                MessageBox.Show("卡号:" + textBox1.Text + ",已挂失成功!");
                button3_Click(sender, e);
                textBox1.Focus();
            }
        }

        private void CancelCardForm_Load(object sender, EventArgs e)
        {

        }





    }
}
