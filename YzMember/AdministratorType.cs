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
    public partial class AdministratorType : Form
    {   
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        OleDbCommandBuilder cb;
        
        public AdministratorType()
        {
            InitializeComponent();
        }

        private void AdminType_Load(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("", conn);
                cmd.CommandText = "select AdministratorType_ID as ID,AdministratorType_Name as 管理角色 from yz_AdministratorType";
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");

                this.dataGridView1.DataSource = this.dataSet1.Tables["a1"];
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.RowHeadersWidth = 20;
                dataGridView1.Columns[0].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!YzMemberClass1.IsAdministraotName(textBox1.Text))
            {
                MessageBox.Show("请正确输入角色名!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            if (MessageBox.Show(@"你是否要新增一个名为""" + textBox1.Text + @"""的角色?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string strSql = "insert into yz_AdministratorType(AdministratorType_Name) values('" + textBox1.Text + "')";
                YzMemberClass1.ExecSql(strSql);
                MessageBox.Show("新增角色成功!");
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "8", YzMemberClass1.TAdministrator.Shop.ToString());
            }

            try
            {
                dataSet1.Tables["a1"].Clear();
                cmd.CommandText = "select AdministratorType_ID as ID,AdministratorType_Name as 管理角色 from yz_AdministratorType";
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = this.dataGridView1.CurrentCell.Value.ToString();
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cb = new OleDbCommandBuilder(da);
                da.Update(dataSet1, "a1");
                dataSet1.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            MessageBox.Show("保存修改成功!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
