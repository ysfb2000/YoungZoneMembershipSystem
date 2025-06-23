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
    public partial class AddAdministratorForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();
        BindingSource bs = new BindingSource();
        int i = 0;
        string strID = "";
        

        public AddAdministratorForm()
        {
            InitializeComponent();
        }

        private void AddAdministratorForm_Load(object sender, EventArgs e)
        {
            string strSql = "SELECT dbo.yz_Administrator.Administrator_ID, dbo.yz_Administrator.Administrator_Name AS 管理员名, CASE dbo.yz_Administrator.Administrator_State WHEN 1 THEN '正常' WHEN 0 THEN '注销' ELSE NULL END AS 状态, dbo.yz_AdministratorType.AdministratorType_Name AS 角色, dbo.yz_Administrator.Administrator_LoginName, dbo.yz_Administrator.Administrator_LastLoginTime, dbo.yz_Administrator.Administrator_LoginCount, dbo.yz_Administrator.AdministratorType_ID, dbo.yz_Administrator.Administrator_State FROM dbo.yz_Administrator INNER JOIN dbo.yz_AdministratorType ON dbo.yz_Administrator.AdministratorType_ID = dbo.yz_AdministratorType.AdministratorType_ID";
            cmd = new OleDbCommand(strSql,conn);
            da.SelectCommand = cmd;

            try
            {
                da.Fill(dataSet1, "a1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            bs.DataSource = dataSet1.Tables["a1"];

            dataGridView1.DataSource = bs;
            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 30;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;


            strSql = "select AdministratorType_ID,AdministratorType_Name from yz_AdministratorType";
            cmd.CommandText = strSql;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(dataSet1, "a2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            comboBox1.DisplayMember = "AdministratorType_Name";
            comboBox1.ValueMember = "AdministratorType_ID";
            comboBox1.DataSource = dataSet1.Tables["a2"];

            button2.Enabled = false;

        }


        private void dataGridView1_Click(object sender, EventArgs e)
        {
            SetAdministratorInfo();
            button2.Enabled = false;
            button1.Enabled = true;
            label11.Text = "";
            i = 0;

            textBox1.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            textBox2.Enabled = true;

        }




        private void SetAdministratorInfo()
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            label7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            label9.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            strID = dataGridView1.CurrentRow.Cells[0].Value.ToString();

 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ( label9.Text == "")
            {
                MessageBox.Show("请选择要修改的管理员!");
                return;

            }

            if (!YzMemberClass1.IsAdministraotName(textBox1.Text))
            {
                MessageBox.Show("用户名不能为空或包含非法字符!");
                return;
            }

            if (!YzMemberClass1.IsAdministraotName(textBox2.Text))
            {
                MessageBox.Show("请正确填写登录名!");
                return;
            }

            if (YzMemberClass1.ExecSql("select * from yz_Administrator where Administrator_LoginName = '" + textBox2.Text.Trim() + "' and Administrator_ID <> " + strID) != 0)
            {
                MessageBox.Show("登录名重复!");
                return;

            }


            string s1 = "";
            if (comboBox2.Text == "正常") s1 = "1"; else s1 = "0";

            string strSql = "update yz_Administrator set Administrator_Name = '" + textBox1.Text + "',AdministratorType_ID = " + comboBox1.SelectedValue.ToString() + ",Administrator_State = " + s1 + ",Administrator_LoginName = '" + textBox2.Text.Trim() + "'"; 
            if (i == 1) strSql += ",Administrator_Password = '" + YzMemberClass1.TAdministrator.DefaultPassword + "'";
            strSql += "  where Administrator_ID = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (MessageBox.Show("是否保存修改?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).ToString() == "OK")
            {
                YzMemberClass1.ExecSql(strSql);
                MessageBox.Show("保存成功");
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "6", YzMemberClass1.TAdministrator.Shop.ToString());
                DataGridDataBind();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你真的要重置管理员:" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "的密码吗?", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation).ToString() == "Yes")
            {
                button2.Enabled = true;
                label11.Text = "密码重置为:" + YzMemberClass1.TAdministrator.DefaultPassword;
                i = 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NewAdministratorForm nform = new NewAdministratorForm();

            if (nform.ShowDialog() == DialogResult.OK)
            {
                DataGridDataBind();
            }
        }

        private void DataGridDataBind()
        {
            string strSql = "SELECT dbo.yz_Administrator.Administrator_ID, dbo.yz_Administrator.Administrator_Name AS 管理员名, CASE dbo.yz_Administrator.Administrator_State WHEN 1 THEN '正常' WHEN 0 THEN '注销' ELSE NULL END AS 状态, dbo.yz_AdministratorType.AdministratorType_Name AS 角色, dbo.yz_Administrator.Administrator_LoginName, dbo.yz_Administrator.Administrator_LastLoginTime, dbo.yz_Administrator.Administrator_LoginCount, dbo.yz_Administrator.AdministratorType_ID, dbo.yz_Administrator.Administrator_State FROM dbo.yz_Administrator INNER JOIN dbo.yz_AdministratorType ON dbo.yz_Administrator.AdministratorType_ID = dbo.yz_AdministratorType.AdministratorType_ID";
            cmd = new OleDbCommand(strSql, conn);
            da.SelectCommand = cmd;
            dataSet1.Tables["a1"].Clear();

            try
            {
                da.Fill(dataSet1, "a1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            textBox1.Text = "";
            textBox2.Text = "";
            label7.Text = "";
            label9.Text = "";
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 1;

            button2.Enabled = false;
            textBox2.Enabled = false;
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            button1.Enabled = false;

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.IndexOf(";") == 0 && textBox1.Text.LastIndexOf("?") == textBox1.Text.Length - 1) textBox1.Text = textBox1.Text.Substring(1, textBox1.Text.Length - 2);
                textBox2.SelectAll();
                textBox2.Focus();

            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text.IndexOf(";") == 0 && textBox2.Text.LastIndexOf("?") == textBox2.Text.Length - 1) textBox2.Text = textBox2.Text.Substring(1, textBox2.Text.Length - 2);

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }
    }
}
