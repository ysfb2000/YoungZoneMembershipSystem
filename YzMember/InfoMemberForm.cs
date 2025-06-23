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
    public partial class InfoMemberForm : Form
    {

        string SCardNO = "";
        public InfoMemberForm()
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
            label10.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            SCardNO = "";
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
                ClearInput();

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
                            break;
                        case "2":
                            MessageBox.Show("此卡已挂失");
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
            string strSql = "SELECT dbo.yz_Member.Member_Name, dbo.yz_Member.Member_Sex, dbo.yz_Member.Member_Phone, dbo.yz_Member.Member_Address, dbo.yz_Member.Member_Postcode, dbo.yz_Member.Member_Birthday, dbo.yz_Member.Member_Email, dbo.yz_Member.Member_Notice, dbo.yz_Member.Member_RegDate, dbo.yz_Card.Card_NO, dbo.yz_CardType.CardType_Name, dbo.yz_Card.Card_State, dbo.yz_Card.Card_Point FROM dbo.yz_Member INNER JOIN dbo.yz_Card ON dbo.yz_Member.Member_ID = dbo.yz_Card.Member_ID INNER JOIN dbo.yz_CardType ON dbo.yz_Card.CardType_ID = dbo.yz_CardType.CardType_ID";
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
                    SCardNO = strCardNo;
                    textBox1.Text = reader.GetValue(2).ToString();
                    textBox3.Text = reader.GetValue(0).ToString();
                    if (reader.GetValue(1).ToString() == "True") label10.Text = "男"; else label10.Text = "女";
                    textBox5.Text = reader.GetValue(3).ToString();
                    textBox6.Text = reader.GetValue(4).ToString();
                    label11.Text = reader.GetDateTime(5).Date.ToShortDateString();
                    textBox7.Text = reader.GetValue(6).ToString();
                    textBox9.Text = reader.GetValue(7).ToString();
                    label18.Text = reader.GetValue(8).ToString();
                    label16.Text = reader.GetValue(10).ToString();
                    label15.Text = reader.GetValue(12).ToString();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.textBox2_KeyDown(sender,new KeyEventArgs(Keys.Enter));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SCardNO != "" && SCardNO == textBox2.Text)
            {
                PrintDocument pd = new PrintDocument();
                Margins mg = new Margins(10, 10, 10, 10);
                pd.DefaultPageSettings.Margins = mg;
                pd.PrinterSettings.PrinterName = YzMemberClass1.TAdministrator.PrintName;
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

                try
                {
                    pd.Print();
                    pd.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
                }

            }
            else
            {
                MessageBox.Show("请输入正确的会员卡号!");
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);

            string s = "";
            s = "会员卡信息\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + SCardNO + "\r\n";
            s += "会员姓名:" + textBox3.Text + "\r\n";
            s += "    性别:" + label10.Text + "\r\n";
            s += "  卡级别:" + label16.Text + "\r\n";
            s += "    积分:" + label15.Text + "\r\n";
            s += "  卡状态:" + label17.Text + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + YzMemberClass1.TAdministrator.Name + "    门店:" + YzMemberClass1.GetShopNameByID(YzMemberClass1.TAdministrator.Shop) + "\r\n";
            s += "操作时间" + DateTime.Now.ToString() + "\r\n";
            s += "\r\n\r\n\r\n_";


            e.Graphics.DrawString(s, f, b, 0, 0);

        }

        private void button3_Click(object sender, EventArgs e)
        {
        
            TradeDetailForm frmAdminType = new TradeDetailForm(SCardNO);
            frmAdminType.MdiParent = this.ParentForm;
            frmAdminType.Show();
        }


              
    }
}
