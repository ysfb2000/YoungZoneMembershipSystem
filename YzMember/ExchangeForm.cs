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
    public partial class ExchangeForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;

        string GoodsID = "";
        string GoodsName = "";
        string GoodsType = "";
        string GoodsValue = "";
        string NowPoint = "";
        string Time = "";
        string storeNum = "";

        public ExchangeForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
                            MessageBox.Show("此卡未开通!");
                            textBox1.SelectAll();
                            textBox1.Focus();
                            break;
                        case "1":
                            this.EnableInput();
                            setMemberInfo(textBox1.Text);
                            filldbgrid(label6.Text);
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
            string strSql = "SELECT yz_Member.Member_Name, yz_Member.Member_Phone, yz_Card.Card_ID, yz_Card.Card_State, yz_Card.Card_Point,yz_CardType.CardType_Name,Member_Notice";
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
                    label3.Text = reader.GetValue(0).ToString();
                    label5.Text = reader.GetValue(5).ToString();
                    label11.Text = reader.GetValue(1).ToString();
                    label6.Text = reader.GetValue(4).ToString();
                    label8.Text = YzMemberClass1.GetCardStateByStateNo(reader.GetValue(3).ToString());
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


        }


        //使会员信息输入框变成可使用
        private void EnableInput()
        {
            textBox1.Enabled = false;
            textBox1.BackColor = SystemColors.ButtonFace;
            textBox3.Enabled = true;
            textBox3.BackColor = Color.White;


            numericUpDown1.Enabled = true;

            button1.Enabled = true;
            button2.Enabled = true;
        }

        //使会员信息输入框变成不可用
        private void DisableInput()
        {
            textBox1.Enabled = true;
            textBox1.BackColor = Color.White;


            numericUpDown1.Enabled = false;
            textBox3.Enabled = false;
            textBox3.BackColor = SystemColors.ButtonFace;

            button1.Enabled = false;
            button2.Enabled = false;

            textBox1.Focus();
            dataGridView1.DataSource = null;
        }

        private void ClearInput()
        {
            textBox1.Text = "";

            label3.Text = "";
            label5.Text = "";
            label6.Text = "";
            label8.Text = "";
            label11.Text = "";
            label13.Text = "";
            label16.Text = "";
            textBox3.Text = "";
            numericUpDown1.Value = 1;

        }


        private void filldbgrid(string point)
        {
            try
            {
                DataSet dataSet1 = new DataSet();
                cmd = new OleDbCommand("", conn);
                string strSql = "SELECT dbo.yz_Goods.Goods_ID as ID, dbo.yz_Goods.Goods_Name as 货品名, dbo.yz_Goods.Goods_Value as 所需积分, (case dbo.yz_Goods.Goods_Type when 1 then '有限货品' when 2 then '无限货品' when 3 then '获赠积分' end) as 货品类型, dbo.yz_Store.Store_Amount as 库存  FROM dbo.yz_Goods INNER JOIN dbo.yz_Store ON dbo.yz_Goods.Goods_ID = dbo.yz_Store.Goods_ID Where (Goods_State = 1) and (Goods_Type <> 0) and ((abs(Goods_Value) <= " + point + ") or (Goods_Value > 0)) and (yz_Store.Shop_ID = " + YzMemberClass1.TAdministrator.Shop.ToString() + ")";
                cmd.CommandText = strSql;

                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
                this.dataGridView1.DataSource = dataSet1.Tables["a1"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) return;

            GoodsID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            GoodsName = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            GoodsValue = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            GoodsType = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            storeNum = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            label13.Text = GoodsName;
            label16.Text = GoodsValue;
            numericUpDown1.Value = 1;
               
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Time = DateTime.Now.ToString();
            if (label13.Text == "")
            {
                MessageBox.Show("请选择商品!");
                return;
            }

            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("请选择数量!");
                return;
            }
            
            double f = (double.Parse(GoodsValue) * (double)numericUpDown1.Value);
            double f1 = double.Parse(label6.Text);

            if ((numericUpDown1.Value > decimal.Parse(storeNum)) && GoodsType == "1")
            {
                MessageBox.Show("兑换数量超过该商品库存!");
                numericUpDown1.Focus();
                return;
            }


            if ((f < 0) && (Math.Abs(f) > f1))
            {
                    MessageBox.Show("所需积分大于拥有的积分!");
                    return;
            }

            if ((f > 0) && (f+f1 > YzMemberClass1.TAdministrator.MaxPoint))
            {
                MessageBox.Show("卡内积分将超过上限,操作终止!");
                return;
            }



            double i2 = (double)Math.Floor(numericUpDown1.Value);
            double i = double.Parse(GoodsValue) * i2;

            string strI = "";
            if (i > 0.00)
            {
                strI = "+" + i.ToString();
            }
            else
            {
                strI = i.ToString();
            }

            if (MessageBox.Show("卡号:" + textBox1.Text + "兑换" + numericUpDown1.Value.ToString() + "个" + label13.Text + ",积分变化" + strI, "请确认", MessageBoxButtons.OKCancel).ToString() == "OK")
            {

                YzMemberClass1.ExchangeGoods(GoodsID, GoodsType, GoodsValue, numericUpDown1.Value.ToString(), textBox1.Text, YzMemberClass1.TAdministrator.Shop.ToString());
                YzMemberClass1.WriteOperateLog(f.ToString(), GoodsID, numericUpDown1.Value.ToString(), "3", textBox1.Text, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(), textBox3.Text,Time);

                NowPoint = YzMemberClass1.ExecSqlReturn("Select Card_Point from yz_Card where Card_NO = '" + textBox1.Text + "'");


                string s = "卡号:" + textBox1.Text + "兑换" + numericUpDown1.Value.ToString() + "个" + label13.Text + ",积分变化" + strI + ",还剩积分" + NowPoint + "\r\n";
                textBox2.Text += s;

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
                ExchangeRePrintForm form1 = new ExchangeRePrintForm(s, textBox1.Text, label6.Text, NowPoint, GoodsName, numericUpDown1.Value.ToString(), strI, label3.Text,Time);
                form1.ShowDialog();
                
                setMemberInfo(textBox1.Text);


                ClearInput();
                DisableInput();

                GoodsID = "";
                GoodsName = "";
                GoodsType = "";
                GoodsValue = "";


            }
            else
            {
 
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearInput();
            DisableInput();
            
        }

        private void ExchangeForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 20;

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void ExchangeForm_Resize(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);
            double f2 = (double.Parse(GoodsValue) * (double)numericUpDown1.Value);

            string s = "";
            s = "洋葱餐厅会员卡积分兑换单\r\n";
            s += "单据号:" + YzMemberClass1.TAdministrator.strOperationID + "\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + textBox1.Text + "\r\n";
            s += "会员姓名:" + label3.Text + "\r\n";
            s += "原有积分:" + label6.Text + "\r\n";
            s += "兑换货品:" + label13.Text + "\r\n";
            s += "兑换数量:" + numericUpDown1.Value.ToString() + "\r\n";
            s += "消费积分:" + f2.ToString() + "\r\n";
            s += "现有积分:" + NowPoint + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + YzMemberClass1.TAdministrator.Name + "    门店:" + YzMemberClass1.GetShopNameByID(YzMemberClass1.TAdministrator.Shop) + "\r\n";
            s += "操作时间:" + Time + "\r\n";
            s += "打印时间:" + DateTime.Now.ToString();
            s += "\r\n\r\n_";
            e.Graphics.DrawString(s, f, b, 0, 0);

        }

        private void pd_PrintPage2(object sender, PrintPageEventArgs e)
        {
            Font f = new Font("宋体", 12);
            Brush b = new SolidBrush(Color.Black);
            double f2 = (double.Parse(GoodsValue) * (double)numericUpDown1.Value);

            string s = "";
            s = "洋葱餐厅会员卡积分兑换确认单\r\n";
            s += "单据号:" + YzMemberClass1.TAdministrator.strOperationID + "\r\n";
            s += "--------------------------\r\n";
            s += "会员卡号:" + textBox1.Text + "\r\n";
            s += "会员姓名:" + label3.Text + "\r\n";
            s += "原有积分:" + label6.Text + "\r\n";
            s += "兑换货品:" + label13.Text + "\r\n";
            s += "兑换数量:" + numericUpDown1.Value.ToString() + "\r\n";
            s += "消费积分:" + f2.ToString() + "\r\n";
            s += "现有积分:" + NowPoint + "\r\n";
            s += "--------------------------\r\n";
            s += "操作员:" + YzMemberClass1.TAdministrator.Name + "    门店:" + YzMemberClass1.GetShopNameByID(YzMemberClass1.TAdministrator.Shop) + "\r\n";
            s += "操作时间:" + Time + "\r\n";
            s += "打印时间:" + DateTime.Now.ToString();
            s += "\r\n\r\n--------------------------\r\n\r\n会员签名:\r\n\r\n--------------------------\r\n_";
            e.Graphics.DrawString(s, f, b, 0, 0);

        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (label13.Text != "")
            {

                double d =double.Parse(GoodsValue) * double.Parse(numericUpDown1.Value.ToString());
                label16.Text = d.ToString();
            }
        }




    }
}
