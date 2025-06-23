using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YzMember
{
    public partial class EditMemberForm : Form
    {
        string strMemberID = "";

        public EditMemberForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void ClearInput()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            label11.Text = "";
            textBox9.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Text = "";
            strMemberID = "";

        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                            SetMemberInfo(textBox2.Text);
                            EnableInput();
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void RegMemberForm_Load(object sender, EventArgs e)
        {
        }

        private void RegMemberForm_Resize(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void SetMemberInfo(string strCardNo)
        {
            string strSql = "SELECT dbo.yz_Member.Member_Name, dbo.yz_Member.Member_Sex, dbo.yz_Member.Member_Phone, dbo.yz_Member.Member_Address, dbo.yz_Member.Member_Postcode, dbo.yz_Member.Member_Birthday, dbo.yz_Member.Member_Email, dbo.yz_Member.Member_Notice, dbo.yz_Member.Member_RegDate, dbo.yz_Card.Card_NO, dbo.yz_CardType.CardType_Name, dbo.yz_Card.Card_State, dbo.yz_Card.Card_Point, dbo.yz_Card.Member_ID FROM dbo.yz_Member INNER JOIN dbo.yz_Card ON dbo.yz_Member.Member_ID = dbo.yz_Card.Member_ID INNER JOIN dbo.yz_CardType ON dbo.yz_Card.CardType_ID = dbo.yz_CardType.CardType_ID";
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
                    textBox1.Text = reader.GetValue(2).ToString();
                    textBox3.Text = reader.GetValue(0).ToString();
                    if (reader.GetValue(1).ToString() == "True") radioButton1.Checked = true; else radioButton2.Checked = true;
                    textBox5.Text = reader.GetValue(3).ToString();
                    textBox6.Text = reader.GetValue(4).ToString();
                    dateTimePicker1.Value = reader.GetDateTime(5);
                    textBox7.Text = reader.GetValue(6).ToString();
                    textBox9.Text = reader.GetValue(7).ToString();
                    label18.Text = reader.GetValue(8).ToString();
                    label16.Text = reader.GetValue(10).ToString();
                    label15.Text = reader.GetValue(12).ToString();
                    strMemberID = reader.GetValue(13).ToString();
                    label17.Text = YzMemberClass1.GetCardStateByStateNo(reader.GetValue(11).ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void EnableInput()
        {
            textBox2.BackColor = SystemColors.ButtonFace;
            textBox1.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            textBox9.BackColor = Color.White;


            textBox2.Enabled = false;
            textBox1.Enabled = true;
            textBox3.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox9.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            dateTimePicker1.Enabled = true;
        }

        private void DisableInput()
        {
            textBox2.BackColor = Color.White;
            textBox1.BackColor = SystemColors.ButtonFace;
            textBox3.BackColor = SystemColors.ButtonFace;
            textBox5.BackColor = SystemColors.ButtonFace;
            textBox6.BackColor = SystemColors.ButtonFace;
            textBox7.BackColor = SystemColors.ButtonFace;
            textBox9.BackColor = SystemColors.ButtonFace;

            textBox2.Enabled = true; 
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox9.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearInput();
            DisableInput();
            textBox2.SelectAll();
            textBox2.Focus();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string s = "";

            if (!YzMemberClass1.IsAdministraotName(textBox1.Text))
            {
                MessageBox.Show("手机号码请正确输入!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            if (!YzMemberClass1.IsAdministraotName(textBox3.Text))
            {
                MessageBox.Show("姓名请正确填写!");
                textBox3.SelectAll();
                textBox3.Focus();
                return;
            }

            textBox5.Text = YzMemberClass1.filter(textBox5.Text);
            textBox6.Text = YzMemberClass1.filter(textBox6.Text);
            textBox7.Text = YzMemberClass1.filter(textBox7.Text);

            if (MessageBox.Show("你确认修改这个会员的信息吗?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string tempSex = "";
                if (radioButton1.Checked == true) tempSex = "1"; else tempSex = "0";

                try
                {
                    OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
                    OleDbCommand cmd = new OleDbCommand("", conn);
                    s = "update yz_Member set Member_Name = '" + textBox3.Text + "',Member_Sex = " + tempSex + ",Member_Phone = '" + textBox1.Text + "',Member_Address = '" + textBox5.Text + "',Member_PostCode = '" + textBox6.Text + "',Member_Birthday = '" + dateTimePicker1.Value.Date.ToString() + "',Member_Email = '" + textBox7.Text + "',Member_Notice = ? where Member_ID = " + strMemberID;
                    cmd.CommandText = s;
                    OleDbParameter p = new OleDbParameter("@Notice", OleDbType.VarChar, 1000);
                    p.Value = textBox9.Text;
                    cmd.Parameters.Add(p);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    YzMemberClass1.WriteOperateLog("0", "1", "1", "8", textBox2.Text, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), "");
                    MessageBox.Show("会员信息修改成功!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }



                ClearInput();
                DisableInput();
                textBox2.SelectAll();
                textBox2.Focus();
            }
        }



              
    }
}
