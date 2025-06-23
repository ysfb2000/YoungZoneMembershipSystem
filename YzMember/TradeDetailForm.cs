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
    public partial class TradeDetailForm : Form
    {
        string strCardNo = "0";
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd = new OleDbCommand();
        DataSet dataSet1 = new DataSet();

        public TradeDetailForm()
        {
           
            InitializeComponent(); 
            cmd.Connection = conn;
        }

        public TradeDetailForm(string CardNo)
        {
            InitializeComponent();
            cmd.Connection = conn;
            strCardNo = CardNo;
        }


        private void TradeDetailForm_Load(object sender, EventArgs e)
        {
            if (strCardNo != "0")
            {
                textBox1.Enabled = false;
                textBox1.BackColor = SystemColors.ButtonFace;
                textBox1.Text = strCardNo;
                button1.Enabled = true;

                try
                {
                    OleDbDataReader reader;
                    string strSql = "SELECT dbo.yz_Member.Member_Name, dbo.yz_Member.Member_Sex,dbo.yz_Member.Member_Phone, dbo.yz_Member.Member_Notice, dbo.yz_Card.Card_State, dbo.yz_Card.Card_Point, dbo.yz_CardType.CardType_Name FROM dbo.yz_Member INNER JOIN dbo.yz_Card ON dbo.yz_Member.Member_ID = dbo.yz_Card.Member_ID INNER JOIN dbo.yz_CardType ON dbo.yz_Card.CardType_ID = dbo.yz_CardType.CardType_ID";
                    strSql = strSql + " where yz_Card.Card_NO = '" + strCardNo + "'";

                    cmd.CommandText = strSql;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        label3.Text = reader.GetValue(0).ToString();
                        if (reader.GetValue(1).ToString() == "True") label5.Text = "男"; else label5.Text = "女";
                        label7.Text = reader.GetValue(2).ToString();
                        label9.Text = reader.GetValue(5).ToString();
                        textBox2.Text = reader.GetValue(3).ToString();
                        label12.Text = YzMemberClass1.GetCardStateByStateNo(reader.GetValue(4).ToString());
                        label10.Text = reader.GetValue(6).ToString();
                    }
                    conn.Close();


                    strSql = "SELECT dbo.yz_OperateLog.OperateLog_ID as 单据号,yz_OperateLog.Card_NO as 卡号, dbo.yz_OperateLog.OperateLog_ChangVar as 积分变化, dbo.yz_Operation.Operation_Name as 操作, dbo.yz_Goods.Goods_Name as 货品, dbo.yz_OperateLog.OperateLog_GoodsAmount as 货品数量,  dbo.yz_OperateLog.OperateLog_Date as 操作时间, dbo.yz_OperateLog.OperateLog_Notice as 备注, dbo.yz_Administrator.Administrator_Name as 操作员, dbo.yz_AdministratorType.AdministratorType_Name as 角色,dbo.yz_Shop.Shop_Name as 门店 FROM dbo.yz_OperateLog INNER JOIN dbo.yz_Operation ON dbo.yz_OperateLog.Operation_ID = dbo.yz_Operation.Operation_ID INNER JOIN dbo.yz_Shop ON dbo.yz_OperateLog.Shop_ID = dbo.yz_Shop.Shop_ID INNER JOIN dbo.yz_Administrator ON dbo.yz_OperateLog.Administrator_ID = dbo.yz_Administrator.Administrator_ID INNER JOIN dbo.yz_AdministratorType ON dbo.yz_Administrator.AdministratorType_ID = dbo.yz_AdministratorType.AdministratorType_ID INNER JOIN dbo.yz_Goods ON dbo.yz_OperateLog.Goods_ID = dbo.yz_Goods.Goods_ID";
                    strSql = strSql + " where yz_OperateLog.Card_NO = '" + strCardNo + "'";

                    cmd.CommandText = strSql;
                    da.SelectCommand = cmd;
                    da.Fill(dataSet1, "a1");
                    dataGridView1.DataSource = dataSet1.Tables["a1"];

                    dataGridView1.Columns[0].Width = 70;
                    dataGridView1.Columns[1].Width = 60;
                    dataGridView1.Columns[2].Width = 80;
                    dataGridView1.Columns[3].Width = 70;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 80;
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[8].Width = 80;
                    dataGridView1.Columns[9].Width = 80;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

            }
            else
            {
 
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox1.Enabled = true;
            textBox1.BackColor = Color.White;
            textBox1.SelectAll();
            textBox1.Focus();
            dataSet1.Tables["a1"].Clear();
            label3.Text = "";
            label5.Text = "";
            label7.Text = "";
            label9.Text = "";
            label10.Text = "";
            label12.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
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
                        strCardNo = textBox1.Text;
                        this.TradeDetailForm_Load(sender, e);
                        break;
                    case "2":
                        strCardNo = textBox1.Text;
                        this.TradeDetailForm_Load(sender, e);
                        break;
                    case "3":
                        strCardNo = textBox1.Text;
                        this.TradeDetailForm_Load(sender, e);
                        break;
                    default:
                        MessageBox.Show("未知的卡!");
                        textBox1.SelectAll();
                        textBox1.Focus();
                        break;
                }
            }
        }

        private void TradeDetailForm_Resize(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

    }
}
