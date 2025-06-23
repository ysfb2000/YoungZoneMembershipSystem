using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YzMember
{
    public partial class ExchangeRePrintForm : Form
    {
        string TempCardNo, TempYPoint, TempNowPoint, TempGoods, TempGoodsNum, TempVpoint, TempName,TempTime;

        public ExchangeRePrintForm(string strmessage,string CardNo,string YPoint,string NowPoint,string Goods,string GoodsNum,string Vpoint,string Name,string Time)
        {
            InitializeComponent();
            textBox1.Text = strmessage;
            TempCardNo = CardNo;
            TempYPoint = YPoint;
            TempGoods = Goods;
            TempGoodsNum = GoodsNum;
            TempVpoint = Vpoint;
            TempNowPoint = NowPoint;
            TempName = Name;
            TempTime = Time;
        }


        private void ExchangeRePrintForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (YzMemberClass1.TAdministrator.BoolPrintExchange)
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
            else
            {
                MessageBox.Show("打印功能已被关闭!请到系统菜单里设置打印!");
            }

        }


        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);

            string s = "";
            s = "洋葱餐厅会员卡积分兑换明细单\r\n";
            s += "单据号:" + YzMemberClass1.TAdministrator.strOperationID + "\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + TempCardNo + "\r\n";
            s += "会员姓名:" + TempName + "\r\n";
            s += "原有积分:" + TempYPoint + "\r\n";
            s += "兑换货品:" + TempGoods + "\r\n";
            s += "兑换数量:" + TempGoodsNum + "\r\n";
            s += "消费积分:" + TempVpoint + "\r\n";
            s += "现有积分:" + TempNowPoint + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + YzMemberClass1.TAdministrator.Name + "    门店:" + YzMemberClass1.GetShopNameByID(YzMemberClass1.TAdministrator.Shop) + "\r\n";
            s += "操作时间:" + TempTime + "\r\n";
            s += "打印时间:" + DateTime.Now.ToString();
            s += "\r\n\r\n_";
            e.Graphics.DrawString(s, f, b, 0, 0);

        }

        private void pd_PrintPage2(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);

            string s = "";
            s = "洋葱餐厅会员卡积分兑换确认单\r\n";
            s += "单据号:" + YzMemberClass1.TAdministrator.strOperationID + "\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + TempCardNo + "\r\n";
            s += "会员姓名:" + TempName + "\r\n";
            s += "原有积分:" + TempYPoint + "\r\n";
            s += "兑换货品:" + TempGoods + "\r\n";
            s += "兑换数量:" + TempGoodsNum + "\r\n";
            s += "消费积分:" + TempVpoint + "\r\n";
            s += "现有积分:" + TempNowPoint + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + YzMemberClass1.TAdministrator.Name + "    门店:" + YzMemberClass1.GetShopNameByID(YzMemberClass1.TAdministrator.Shop) + "\r\n";
            s += "操作时间:" + TempTime + "\r\n";
            s += "打印时间:" + DateTime.Now.ToString();
            s += "\r\n\r\n--------------------------\r\n\r\n会员签名:\r\n\r\n--------------------------\r\n\r\n_";
            e.Graphics.DrawString(s, f, b, 0, 0);

        }




    }
}
