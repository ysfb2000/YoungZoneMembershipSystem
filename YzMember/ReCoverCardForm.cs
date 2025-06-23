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
    public partial class ReCoverCardForm : Form
    {
        public ReCoverCardForm()
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
                            MessageBox.Show("此卡还未被开通!");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        case "1":
                            MessageBox.Show("此卡状态正常!");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        case "2":
                            MessageBox.Show("此卡已挂失!");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        case "3":
                            setMemberInfo(textBox1.Text);
                            textBox1.Enabled = false;
                            button3.Enabled = true;
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
            string strSql = "SELECT yz_Member.Member_Name,yz_Member.Member_Sex, yz_Member.Member_Phone,yz_Member.Member_RegDate, yz_Card.Card_Point,yz_CardType.CardType_Name,yz_Card.Card_State";
            strSql += " FROM yz_Member INNER JOIN yz_Card ON yz_Member.Member_ID = yz_Card.Member_ID INNER JOIN yz_CardType ON yz_Card.CardType_ID = yz_CardType.CardType_ID";
            strSql += " Where yz_Card.Card_No = '" + strCardNo + "'";


            try
            {
                OleDbDataReader reader;
                OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
                OleDbCommand cmd = new OleDbCommand(strSql, conn);

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


            if (MessageBox.Show("卡号:" + textBox1.Text + "是否开始恢复注销?", "请确认", MessageBoxButtons.OKCancel).ToString() == "OK")
            {
                string strSql = "update yz_Card set Card_State = 1 where Card_NO = '" + textBox1.Text + "'";
                YzMemberClass1.ExecSql(strSql);
                YzMemberClass1.WriteOperateLog("0", "1", "0", "9", textBox1.Text, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), textBox2.Text);
                MessageBox.Show("卡号:" + textBox1.Text + ",已恢复注销成功!");
                button3_Click(sender, e);
                textBox1.Focus();
            }
        }

        private void CancelCardForm_Load(object sender, EventArgs e)
        {

        }





    }
}
