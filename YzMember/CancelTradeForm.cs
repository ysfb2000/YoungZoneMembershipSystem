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
    public partial class RePrintForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataReader reader;
        string strOperateID = "";

        public RePrintForm()
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
            string strSql = "SELECT OperateLog_ChangVar, Card_No, OperateLog_GoodsAmount, Goods_ID, Operation_ID, OperateLog_State, Shop_ID FROM dbo.yz_OperateLog where OperateLog_ID = " + strOperateID;
            OleDbCommand cmd;
            cmd = new OleDbCommand(strSql, conn);
            
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetValue(5).ToString() == "True")
                {
                    string ChangVar = reader.GetValue(0).ToString();
                    string CardNo = reader.GetValue(1).ToString();
                    string GoodsAmount = reader.GetValue(2).ToString();
                    string GoodsID = reader.GetValue(3).ToString();
                    string OperateID = reader.GetValue(4).ToString();
                    string ShopID = reader.GetValue(6).ToString();

                    reader.Close();
                    strSql = "update yz_OperateLog set OperateLog_State = 0 where OperateLog_ID = " + strOperateID;
                    cmd.CommandText = strSql;
                    cmd.ExecuteNonQuery();

                    switch(OperateID)
                    {
                        case "2":
                            YzMemberClass1.WriteOperateLog("-" + ChangVar, GoodsID, GoodsAmount, "10", CardNo, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), textBox3.Text);
                            cmd.CommandText = "update yz_Card set Card_Point = Card_Point - " + ChangVar +" where Card_No = " + CardNo;
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("单据撤销成功!");
                            break;
                        case "3":
                            YzMemberClass1.WriteOperateLog("abs(" + ChangVar + ")", GoodsID, GoodsAmount, "11", CardNo, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), textBox3.Text);
                            cmd.CommandText = "update yz_Card set Card_Point = Card_Point + abs(" + ChangVar + ") where Card_No = " + CardNo;
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "update yz_Store set Store_Amount = Store_Amount + " + GoodsAmount + " where Shop_ID = " + ShopID + " and Goods_ID = " + GoodsID;
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("单据撤销成功!");
                            break;
                        default:
                            MessageBox.Show("此单据不能撤销!");
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
    }
}
