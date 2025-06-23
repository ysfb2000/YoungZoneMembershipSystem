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
    public partial class RePrintForm2 : Form
    {
        string ChangVar;
        string CardNo;
        string GoodsAmount;
        string GoodsID;
        string OperateID;
        string ShopID;
        string MemberName;
        string AdministratorName;
        string OperateLogDate;
        string OperateLogID;
        string GoodsName;

        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataReader reader;
        string strOperateID = "";

        public RePrintForm2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            strOperateID = textBox1.Text.Trim();
            string strSql = "";

            if (YzMemberClass1.IsNumber(strOperateID))
            {
                OleDbCommand cmd;
                strSql = "SELECT dbo.yz_OperateLog.OperateLog_ID as ID,dbo.yz_OperateLog.Card_No as 卡号, dbo.yz_Operation.Operation_Name as 类型, dbo.yz_OperateLog.OperateLog_Date as 日期,  dbo.yz_Goods.Goods_Name 货品名,dbo.yz_OperateLog.OperateLog_GoodsAmount as 数量, dbo.yz_OperateLog.OperateLog_ChangVar as 积分, dbo.yz_Administrator.Administrator_Name as 操作员, dbo.yz_Shop.Shop_Name as 门店, dbo.yz_OperateLog.OperateLog_Notice FROM dbo.yz_Operation INNER JOIN dbo.yz_OperateLog ON dbo.yz_Operation.Operation_ID = dbo.yz_OperateLog.Operation_ID INNER JOIN dbo.yz_Shop ON dbo.yz_OperateLog.Shop_ID = dbo.yz_Shop.Shop_ID INNER JOIN dbo.yz_Administrator ON dbo.yz_OperateLog.Administrator_ID = dbo.yz_Administrator.Administrator_ID INNER JOIN dbo.yz_Goods ON dbo.yz_OperateLog.Goods_ID = dbo.yz_Goods.Goods_ID where yz_OperateLog.OperateLog_ID = " + strOperateID;
                cmd = new OleDbCommand(strSql, conn);
                conn.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox2.Text = reader.GetValue(9).ToString();
                    label12.Text = reader.GetValue(3).ToString();
                    label13.Text = reader.GetValue(6).ToString();
                    label14.Text = reader.GetValue(5).ToString();
                    label15.Text = reader.GetValue(4).ToString();
                    label16.Text = reader.GetValue(2).ToString();
                    label17.Text = reader.GetValue(7).ToString();
                    label18.Text = reader.GetValue(8).ToString();
                    label19.Text = reader.GetValue(1).ToString();
                }
                else
                {
                    MessageBox.Show("无此单据号记录!");
                    ClearInfo();
                    textBox1.SelectAll();
                    textBox1.Focus();
                }


               
            }
            else
            {
                MessageBox.Show("请正确输入单据号!");
                textBox1.SelectAll();
                textBox1.Focus();
            }
            conn.Close();

        }

        private void ClearInfo()
        {
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
 
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT OperateLog_ChangVar, yz_OperateLog.Card_No, OperateLog_GoodsAmount, yz_OperateLog.Goods_ID, Operation_ID, OperateLog_State, yz_Shop.Shop_ID, Member_Name,Administrator_Name,OperateLog_Date,OperateLog_ID,yz_Goods.Goods_Name FROM dbo.yz_OperateLog INNER JOIN dbo.yz_Card ON dbo.yz_OperateLog.Card_No = dbo.yz_Card.Card_NO INNER JOIN dbo.yz_Member ON dbo.yz_Card.Member_ID = dbo.yz_Member.Member_ID INNER JOIN dbo.yz_Administrator ON dbo.yz_OperateLog.Administrator_ID = dbo.yz_Administrator.Administrator_ID  INNER JOIN dbo.yz_Shop ON dbo.yz_OperateLog.Shop_ID = dbo.yz_Shop.Shop_ID INNER JOIN dbo.yz_Goods ON dbo.yz_OperateLog.Goods_ID = dbo.yz_Goods.Goods_ID where OperateLog_ID = " + strOperateID;
            OleDbCommand cmd;
            cmd = new OleDbCommand(strSql, conn);
            
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetValue(5).ToString() == "True")
                {
                    ChangVar = reader.GetValue(0).ToString();
                    CardNo = reader.GetValue(1).ToString();
                    GoodsAmount = reader.GetValue(2).ToString();
                    GoodsID = reader.GetValue(3).ToString();
                    OperateID = reader.GetValue(4).ToString();
                    ShopID = reader.GetValue(6).ToString();
                    MemberName = reader.GetValue(7).ToString();
                    AdministratorName = reader.GetValue(8).ToString();
                    OperateLogDate = reader.GetValue(9).ToString();
                    OperateLogID = reader.GetValue(10).ToString();
                    GoodsName = reader.GetValue(11).ToString();
                    
                    reader.Close();


                    switch(OperateID)
                    {
                        case "2":

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
                                    pd.Dispose();
                                    YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "11", YzMemberClass1.TAdministrator.Shop.ToString());
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
                                }
                            }
                            else
                            {
                                MessageBox.Show("打印设置中已关闭积分打印功能.");
                            }

                            break;
                        case "3":

                            if (YzMemberClass1.TAdministrator.BoolPrintExchange)
                            {
                                PrintDocument pd = new PrintDocument();
                                Margins mg = new Margins(10, 10, 10, 10);
                                pd.DefaultPageSettings.Margins = mg;
                                pd.PrinterSettings.PrinterName = YzMemberClass1.TAdministrator.PrintName;
                                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage2);

                                try
                                {
                                    pd.Print();
                                    pd.Dispose();
                                    YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "12", YzMemberClass1.TAdministrator.Shop.ToString());
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
                                }


                            }
                            else
                            {
                                MessageBox.Show("");

                            }

                            break;
                        default:
                            MessageBox.Show("打印设置中已关闭兑换打印功能.");
                            break;
                    }

                    ClearInfo();
                    textBox1.SelectAll();
                    textBox1.Focus();
                   
                }
                else
                {
                    MessageBox.Show("此单据已无效!");
                    conn.Close();
                    return;
                }
                
            
            }
            else
            {
                MessageBox.Show("无此单据号!");
                return;
            }
            
            
            


            conn.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            strOperateID = "";
        }


        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);

            string s = "";
            s = "洋葱餐厅会员卡积分单(重印)\r\n";
            s += "单据号:" + OperateLogID + "\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + CardNo + "\r\n";
            s += "会员姓名:" + MemberName + "\r\n";
            s += "增加积分:" + ChangVar + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + AdministratorName + "  门店:" + YzMemberClass1.GetShopNameByID(int.Parse(ShopID)) + "\r\n";
            s += "操作时间" + OperateLogDate + "\r\n";
            s += "打印时间" + DateTime.Now.ToString() + "\r\n";
            s += "\r\n_";


            e.Graphics.DrawString(s, f, b, 0, 0);

        }

        private void pd_PrintPage2(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);

            string s = "";
            s = "洋葱餐厅会员卡积分兑换单(重印)\r\n";
            s += "单据号:" + OperateLogID + "\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + CardNo + "\r\n";
            s += "会员姓名:" + MemberName + "\r\n";
            s += "兑换货品:" + GoodsName + "\r\n";
            s += "兑换数量:" + GoodsAmount + "\r\n";
            s += "消费积分:" + ChangVar + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + AdministratorName + "    门店:" + YzMemberClass1.GetShopNameByID(int.Parse(ShopID)) + "\r\n";
            s += "操作时间:" + OperateLogDate + "\r\n";
            s += "打印时间:" + DateTime.Now.ToString();
            s += "\r\n\r\n_";
            e.Graphics.DrawString(s, f, b, 0, 0);

        }

        private void RePrintForm2_Load(object sender, EventArgs e)
        {

        }
    }
}
