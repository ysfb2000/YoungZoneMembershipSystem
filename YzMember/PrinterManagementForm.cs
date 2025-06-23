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
    public partial class PrinterManagementForm : Form
    {
        public PrinterManagementForm()
        {
            InitializeComponent();
        }

        private void PrinterManagementForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add(YzMemberClass1.TAdministrator.PrintName);
            checkBox1.Checked = YzMemberClass1.TAdministrator.BoolPrintPoint;
            checkBox2.Checked = YzMemberClass1.TAdministrator.BoolPrintExchange;

            PrintDocument pd = new PrintDocument();

            foreach (string PrintName in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(PrintName);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要保存以上打印设置吗?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
                OleDbCommand cmd = new OleDbCommand("",conn);

                YzMemberClass1.TAdministrator.BoolPrintPoint = checkBox1.Checked;
                YzMemberClass1.TAdministrator.BoolPrintExchange = checkBox2.Checked;
                YzMemberClass1.TAdministrator.PrintName = comboBox1.Text;

                int i=0, j=0;

                if (checkBox1.Checked == true) i = 1; else i = 0;
                if (checkBox2.Checked == true) j = 1; else j = 0;

                string strSql = "update yz_Mac set PrinterName = ? ,PointPrint = " + i.ToString() + ",GoodsPrint = " + j.ToString() + " where MAC_ID = '" + YzMemberClass1.TAdministrator.MAC +"'";
                cmd.CommandText = strSql;
                OleDbParameter p = new OleDbParameter("@PrintName",comboBox1.Text);
                cmd.Parameters.Add(p);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "7", YzMemberClass1.TAdministrator.Shop.ToString());
                MessageBox.Show("打印设置保存成功!");
            }
        }
    }
}
