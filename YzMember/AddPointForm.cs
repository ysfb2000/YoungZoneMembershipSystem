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
    public partial class AddPointForm : Form
    {
        string NowPoint = "";
        string Time = "";

        public AddPointForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //使会员信息输入框变成可使用
        private void EnableInput()
        {
            textBox1.Enabled = false;
            textBox1.BackColor = SystemColors.ButtonFace;
            

            textBox2.Enabled = true;
            textBox2.BackColor = Color.White;

            textBox5.Enabled = true;
            textBox5.BackColor = Color.White;

            button1.Enabled = true;
            button3.Enabled = true;
        }

        //使会员信息输入框变成不可用
        private void DisableInput()
        {
            textBox1.Enabled = true;
            textBox1.BackColor = Color.White;


            textBox2.Enabled = false;
            textBox2.BackColor = SystemColors.ButtonFace;

            textBox5.Enabled = false;
            textBox5.BackColor = SystemColors.ButtonFace;

            button1.Enabled = false;
            button3.Enabled = false;

            label3.Text = "";
            label5.Text = "";
            label6.Text = "";
            label8.Text = "";
            label11.Text = "";

            textBox1.Focus();
        }

        private void ClearInput()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";

            label3.Text = "";
            label5.Text = "";
            label6.Text = "";
            label8.Text = "";
            label11.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearInput();
            DisableInput();
        }

        private void AddPointForm_Load(object sender, EventArgs e)
        {
            ClearInput();
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text.Length == (YzMemberClass1.TBrands.Brands_CardNoLength + 2) && textBox2.Text.IndexOf(";") == 0 && textBox2.Text.LastIndexOf("?") == (YzMemberClass1.TBrands.Brands_CardNoLength + 1)) textBox2.Text = textBox2.Text.Substring(1, YzMemberClass1.TBrands.Brands_CardNoLength);

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
                            MessageBox.Show("此卡未开通!");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        case "1":
                            this.EnableInput();
                            setMemberInfo(textBox1.Text);
                            textBox2.Focus();
                            break;
                        case "2":
                            MessageBox.Show("此卡已挂失");
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


        private void setMemberInfo(string strCardNo)
        {
            try
            {
                string strSql = "SELECT yz_Member.Member_Name, yz_Member.Member_Phone, yz_Card.Card_ID, yz_Card.Card_State, yz_Card.Card_Point,yz_CardType.CardType_Name,Member_Notice";
                strSql += " FROM yz_Member INNER JOIN yz_Card ON yz_Member.Member_ID = yz_Card.Member_ID INNER JOIN yz_CardType ON yz_Card.CardType_ID = yz_CardType.CardType_ID";
                strSql += " Where yz_Card.Card_No = '" + strCardNo + "'";

                string strSql2 = "SELECT top 1 dbo.yz_OperateLog.OperateLog_Date, dbo.yz_Operation.Operation_Name,dbo.yz_OperateLog.OperateLog_GoodsAmount FROM dbo.yz_OperateLog INNER JOIN dbo.yz_Operation ON dbo.yz_OperateLog.Operation_ID = dbo.yz_Operation.Operation_ID where yz_OperateLog.Card_No = '" + strCardNo + "' order by dbo.yz_OperateLog.OperateLog_ID desc";
                string strSql3 = "select count(*) from  dbo.yz_OperateLog where yz_OperateLog.Card_No = '" + strCardNo + "' and dbo.yz_OperateLog.OperateLog_State = 2 and datediff(month,getdate(),dbo.yz_OperateLog.OperateLog_Date)=0";
                OleDbDataReader reader;
                OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
                OleDbCommand cmd = new OleDbCommand(strSql, conn);

                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label3.Text = reader.GetValue(0).ToString();
                    label5.Text = reader.GetValue(5).ToString();
                    label11.Text = reader.GetValue(1).ToString();
                    label6.Text = reader.GetValue(4).ToString();
                    label8.Text = YzMemberClass1.GetCardStateByStateNo(reader.GetValue(3).ToString());
                    textBox3.Text = reader.GetValue(6).ToString();
                }

                reader.Close();

                cmd.CommandText = strSql2;

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    label15.Text = reader.GetValue(0).ToString();
                    label17.Text = reader.GetValue(2).ToString();
                    label21.Text = reader.GetValue(1).ToString();
                }

                reader.Close();
                cmd.CommandText = strSql3;
                label19.Text = cmd.ExecuteScalar().ToString();



                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s1= "";
            Time = DateTime.Now.ToString();

            if (YzMemberClass1.IsFloatNumber(textBox2.Text))
            {
                if (double.Parse(textBox2.Text) <= 0)
                {
                    MessageBox.Show("积分不能小于或等于0!");
                    textBox2.Focus();
                    textBox2.SelectAll();
                    return;
                }

                float f = float.Parse(textBox2.Text);
                float f1 = float.Parse(label6.Text);
                f1 += f;
                if (f1 > YzMemberClass1.TAdministrator.MaxPoint)
                {
                    MessageBox.Show("卡内积分将超过上限,操作终止!");
                    return;
                }

                
                s1 = "卡号:" + textBox1.Text +  " 现积分:" + label6.Text + "+" + textBox2.Text;
                if (MessageBox.Show(s1, "请确认", MessageBoxButtons.OKCancel).ToString() == "OK")
                {

                    NowPoint = YzMemberClass1.AddPoint(textBox1.Text, f,Time,textBox5.Text);  //增加积分

                    if (YzMemberClass1.TAdministrator.BoolPrintPoint)
                    {
                        PrintDocument pd = new PrintDocument();
                        Margins mg = new Margins(10, 10, 10, 10);
                        pd.DefaultPageSettings.Margins = mg;
                        pd.PrinterSettings.PrinterName = YzMemberClass1.TAdministrator.PrintName;
                        pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        

                        try
                        {
                            pd.Print();
                            pd.PrintPage -= new PrintPageEventHandler(this.pd_PrintPage);
                            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage2);
                            pd.Print();
                            pd.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
                        }
                    }

                    AddPointRePrintForm form1 = new AddPointRePrintForm(s1 + "=" + NowPoint + "\r\n", textBox1.Text, label6.Text, NowPoint, textBox2.Text,label3.Text,Time);
                    form1.ShowDialog();

                    textBox4.Text += s1 + "=" + NowPoint + "\r\n";

                    ClearInput();
                    DisableInput();
                    textBox1.Focus();
                }
                else
                {
                    textBox2.SelectAll();
                    textBox2.Focus();
                }
            }
            else
            {
                MessageBox.Show("积分错误!");
                textBox2.Focus();
                textBox2.SelectAll();
                return;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }


        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);

            string s = "";
            s = "洋葱餐厅会员卡积分单\r\n";
            s += "单据号:" + YzMemberClass1.TAdministrator.strOperationID + "\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + textBox1.Text + "\r\n";
            s += "会员姓名:" + label3.Text + "\r\n";
            s += "原有积分:" + label6.Text + "\r\n";
            s += "增加积分:" + textBox2.Text + "\r\n";
            s += "现有积分:" + NowPoint + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + YzMemberClass1.TAdministrator.Name + "  门店:" + YzMemberClass1.GetShopNameByID(YzMemberClass1.TAdministrator.Shop) + "\r\n";
            s += "操作时间" + Time + "\r\n";
            s += "打印时间" + DateTime.Now.ToString() + "\r\n";
            s += "\r\n_";


            e.Graphics.DrawString(s, f, b, 0, 0);

        }

        private void pd_PrintPage2(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);

            string s = "";
            s = "洋葱餐厅会员卡积分确认单\r\n";
            s += "单据号:" + YzMemberClass1.TAdministrator.strOperationID + "\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + textBox1.Text + "\r\n";
            s += "会员姓名:" + label3.Text + "\r\n";
            s += "原有积分:" + label6.Text + "\r\n";
            s += "增加积分:" + textBox2.Text + "\r\n";
            s += "现有积分:" + NowPoint + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + YzMemberClass1.TAdministrator.Name + "  门店:" + YzMemberClass1.GetShopNameByID(YzMemberClass1.TAdministrator.Shop) + "\r\n";
            s += "操作时间" + Time + "\r\n";
            s += "打印时间" + DateTime.Now.ToString() + "\r\n";
            s += "\r\n\r\n--------------------------\r\n\r\n会员签名:\r\n\r\n--------------------------";


            e.Graphics.DrawString(s, f, b, 0, 0);

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }



    }
}
