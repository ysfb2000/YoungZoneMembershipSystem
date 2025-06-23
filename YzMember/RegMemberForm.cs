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
    public partial class RegMemberForm : Form
    {
        public RegMemberForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }



        //使会员信息输入框变成可使用
        private void EnableInput()
        {
            textBox2.Enabled = false;
            textBox2.BackColor = SystemColors.ButtonFace;
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;

            textBox1.Enabled = true;
            textBox3.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            dateTimePicker1.Enabled = true;
            textBox9.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;

            textBox1.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            dateTimePicker1.BackColor = Color.White;
            textBox9.BackColor = Color.White;
        }

        //使会员信息输入框变成不可用
        private void DisableInput()
        {
            textBox2.Enabled = true;
            textBox2.BackColor = Color.White;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;

            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            dateTimePicker1.Enabled = false;
            textBox9.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;

            textBox1.BackColor = SystemColors.ButtonFace;
            textBox3.BackColor = SystemColors.ButtonFace;
            textBox5.BackColor = SystemColors.ButtonFace;
            textBox6.BackColor = SystemColors.ButtonFace;
            textBox7.BackColor = SystemColors.ButtonFace;
            dateTimePicker1.BackColor = SystemColors.ButtonFace;
            textBox9.BackColor = SystemColors.ButtonFace;
        }

        private void ClearInput()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            dateTimePicker1.Value = DateTime.Parse("1900-1-1");
            textBox9.Text = "";
            radioButton1.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s, s1="";



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

            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("必须选择性别!");
                return;
            }

            textBox5.Text = YzMemberClass1.filter(textBox5.Text);
            textBox6.Text = YzMemberClass1.filter(textBox6.Text);
            textBox7.Text = YzMemberClass1.filter(textBox7.Text);

            int i=1;
            if (radioButton1.Checked == true) i = 1;
            if (radioButton2.Checked == true) i = 0;

            string strBirthday = "";
            strBirthday = dateTimePicker1.Value.ToShortDateString();
            try
            {
                OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
                OleDbCommand cmd = new OleDbCommand("", conn);
                s = "insert into yz_Member(Member_Name,Member_Sex,Member_Phone,Member_Address,Member_Postcode,Member_Email,Member_Birthday,Member_Notice,Member_State,Member_RegDate) values('" + textBox3.Text.Trim() + "'," + i.ToString() + ",'" + textBox1.Text.Trim() + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + strBirthday + "', ?,1,getdate());select SCOPE_IDENTITY()";
                cmd.CommandText = s;
                OleDbParameter p = new OleDbParameter("@Notice", OleDbType.VarChar, 1000);
                p.Value = textBox9.Text;
                OleDbDataReader reader;
                cmd.Parameters.Add(p);
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    s1 = reader.GetValue(0).ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //string s1 = YzMemberClass1.ExecSqlReturn(s);

            if (textBox2.Text.Trim() != "")
            {
                s = "update yz_Card set Member_ID = " + s1 + ",Card_State = 1 where Card_No = " + textBox2.Text.Trim();

                i = YzMemberClass1.ExecSql(s);
                YzMemberClass1.WriteOperateLog("0", "1", "0", "1", textBox2.Text.Trim(), YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), "");
            }
            

            this.ClearInput();
            this.DisableInput();
            textBox2.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DisableInput();
            this.ClearInput();
            textBox2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.EnableInput();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Enabled == true) this.DisableInput();
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
                            this.EnableInput();
                            textBox1.Focus();
                            break;
                        case "1":
                            MessageBox.Show("有人使用!");
                            textBox2.SelectAll();
                            textBox2.Focus();
                            break;
                        case "2":
                            MessageBox.Show("此卡已挂失!");
                            textBox2.SelectAll();
                            textBox2.Focus();
                            break;
                        case "3":
                            MessageBox.Show("此卡已注销!");
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
            textBox2.MaxLength = YzMemberClass1.TBrands.Brands_CardNoLength + 2;
        }

        private void RegMemberForm_Resize(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

              
    }
}
