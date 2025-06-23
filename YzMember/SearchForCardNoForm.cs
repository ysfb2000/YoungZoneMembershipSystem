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
    public partial class SearchForCardNoForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();

        public SearchForCardNoForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" && textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入查询条件,姓名和手机必须填写其中之一!");
                textBox1.Focus();
                return;
            }

            string strSql = "SELECT dbo.yz_Card.Card_ID, dbo.yz_Card.Card_NO as 卡号, dbo.yz_Card.Card_Point as 积分,case dbo.yz_Card.Card_State when 0 then '未激活' when 1 then '正常' when 2 then '挂失' when 3 then '注销' else '非法状态' end as 卡状态, dbo.yz_Member.Member_Name as 姓名,case dbo.yz_Member.Member_Sex when 1 then '男' when 0 then '女' else '人' end as 性别, dbo.yz_Member.Member_Phone as 手机, dbo.yz_Member.Member_Birthday as 生日, dbo.yz_Member.Member_Email as Email, dbo.yz_Member.Member_RegDate as 注册时间, dbo.yz_Member.Member_Address as 地址,dbo.yz_Member.Member_ID FROM dbo.yz_Member INNER JOIN dbo.yz_Card ON dbo.yz_Member.Member_ID = dbo.yz_Card.Member_ID INNER JOIN dbo.yz_CardType ON dbo.yz_Card.CardType_ID = dbo.yz_CardType.CardType_ID where 1=1";

            if (textBox1.Text.Trim() != "")
            {
                strSql += " and dbo.yz_Member.Member_Name like '%'+?+'%'";
            }

            if (textBox2.Text.Trim() != "")
            {
                strSql += " and dbo.yz_Member.Member_Phone like '%'+?+'%'";
            }


            switch (comboBox1.Text)
            {
                case "女":
                    strSql += " and dbo.yz_Member.Member_Sex = 0";
                    break;
                case "男":
                    strSql += " and dbo.yz_Member.Member_Sex = 1";
                    break;
            }
 


            cmd = new OleDbCommand(strSql, conn);

            if (textBox1.Text.Trim() != "")
            {
                OleDbParameter p = new OleDbParameter("@name", OleDbType.VarChar, 32);
                p.Value = textBox1.Text.Trim();
                cmd.Parameters.Add(p);
            }

            if (textBox2.Text.Trim() != "")
            {
                OleDbParameter p2 = new OleDbParameter("@phone", OleDbType.VarChar, 24);
                p2.Value = textBox2.Text.Trim();
                cmd.Parameters.Add(p2);
            }


            da.SelectCommand = cmd;

            dataSet1.Tables["a1"].Clear();
            da.Fill(dataSet1, "a1");

            dataGridView1.DataSource = dataSet1.Tables["a1"];
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].Width = 40;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 70;
            dataGridView1.Columns[8].Width = 100;



            

        }

        private void SearchForCardNoForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dataSet1.Tables.Add("a1");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                textBox3.Text = YzMemberClass1.ExecSqlReturn("select Member_Notice from yz_Member where Member_ID = " + dataGridView1.CurrentRow.Cells[11].Value.ToString());

            }
        }
    }
}
