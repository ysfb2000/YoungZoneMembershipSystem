using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YzMember
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!YzMemberClass1.IsPassword(textBox1.Text))
            {
                MessageBox.Show("原密码不能为空或者含有非法字符!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            if (!YzMemberClass1.IsPassword(textBox2.Text))
            {
                MessageBox.Show("原密码不能为空或者含有非法字符!");
                textBox2.SelectAll();
                textBox2.Focus();
                return;
            }

            if (!YzMemberClass1.IsPassword(textBox3.Text))
            {
                MessageBox.Show("原密码不能为空或者含有非法字符!");
                textBox3.SelectAll();
                textBox3.Focus();
                return;
            }

            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("重复新密码不正确!");
                textBox3.SelectAll();
                textBox3.Focus();
                return;
            }

            if (YzMemberClass1.ExecSql("select 1 from yz_Administrator where administrator_ID = " + YzMemberClass1.TAdministrator.ID.ToString() + " and administrator_Password = '" + textBox1.Text + "'") == 0)
            {
                MessageBox.Show("原密码不正确!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            if (MessageBox.Show("你确定是要修改管理员" + YzMemberClass1.TAdministrator.Name.ToString() + "的密码?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).ToString() == "OK")
            {
                string strSql = "update yz_Administrator set Administrator_Password = '" + textBox2.Text + "' where Administrator_ID = " + YzMemberClass1.TAdministrator.ID.ToString();
                YzMemberClass1.ExecSql(strSql);
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "2", YzMemberClass1.TAdministrator.Shop.ToString());
                MessageBox.Show("密码修改成功!");
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.SelectAll();
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.SelectAll();
                textBox3.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
