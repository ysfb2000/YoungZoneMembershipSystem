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
    public partial class NewAdministratorForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();



        public NewAdministratorForm()
        {
            InitializeComponent();
        }

        private void AddAdministratorForm_Load(object sender, EventArgs e)
        {
            label11.Text = YzMemberClass1.TAdministrator.DefaultPassword;

            cmd = new OleDbCommand("",conn);
            string strSql = "select AdministratorType_ID,AdministratorType_Name from yz_AdministratorType";
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

        }




        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!YzMemberClass1.IsAdministraotName(textBox1.Text))
            {
                MessageBox.Show("用户名不能为空或包含非法字符!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            if (!YzMemberClass1.IsAdministraotName(textBox2.Text))
            {
                MessageBox.Show("登录名不能为空或包含非法字符!");
                textBox2.SelectAll();
                textBox2.Focus();
                return;
            }

            if (YzMemberClass1.ExecSql("select 1 from yz_administrator where Administrator_LoginName = '" + textBox2.Text + "'") == -1)
            {
                
                MessageBox.Show("登录名已被注册!");
                textBox2.SelectAll();
                textBox2.Focus();
                return;
            }

            string strSql = "insert into yz_administrator(Administrator_Name,Administrator_LoginName,Administrator_Password,AdministratorType_ID,Administrator_LoginCount,Administrator_State) values('" + textBox1.Text + "','" + textBox2.Text + "','" + YzMemberClass1.TAdministrator.DefaultPassword + "'," + comboBox1.SelectedValue.ToString() + ",0,1)"; 


            if (MessageBox.Show("是否新增管理员?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).ToString() == "OK")
            {
                YzMemberClass1.ExecSql(strSql);
                MessageBox.Show("新增成功");
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "5", YzMemberClass1.TAdministrator.Shop.ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.IndexOf(";") == 0 && textBox1.Text.LastIndexOf("?") == textBox1.Text.Length - 1) textBox1.Text = textBox1.Text.Substring(1, textBox1.Text.Length - 2);
                textBox2.Focus();
            }


        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text.IndexOf(";") == 0 && textBox2.Text.LastIndexOf("?") == textBox2.Text.Length - 1) textBox2.Text = textBox2.Text.Substring(1, textBox2.Text.Length - 2);
                comboBox1.Focus();

            }
        }


    }
}
