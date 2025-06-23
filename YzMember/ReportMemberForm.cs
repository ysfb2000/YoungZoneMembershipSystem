using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Reporting.WinForms;

namespace YzMember
{
    public partial class ReportMemberForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        public ReportMemberForm()
        {
            InitializeComponent();
        }

        private void ReportMemberForm_Load(object sender, EventArgs e)
        {
            cmd.Connection = conn;
            cmd.CommandText = "select * from yz_CardState";
            da.SelectCommand = cmd;
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "a1");

            comboBox1.DataSource = ds1.Tables["a1"];
            comboBox1.DisplayMember = "CardState_Name";
            comboBox1.ValueMember = "CardState_ID";

            comboBox1.SelectedIndex = 1;

        }

        private DataTable LoadData(string strCmd)
        {
            cmd.Connection = conn;
            cmd.CommandText = strCmd;
            da.SelectCommand = cmd;
            DataSet dataSet = new DataSet();

            da.Fill(dataSet, "a1");

            return dataSet.Tables["a1"];
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                comboBox1.Enabled = true;
                checkBox4.Checked = false;
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reportViewer1.Reset();
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = "ReportMember.rdlc";
            string s = "SELECT dbo.yz_Card.Card_NO as Card_NO, dbo.yz_Member.Member_Name as Member_Name,CASE dbo.yz_Member.Member_Sex WHEN 1 THEN '男' ELSE '女' END as Member_Sex, dbo.yz_Member.Member_Phone as Member_Phone, dbo.yz_Member.Member_Address as Member_Address, dbo.yz_Member.Member_Postcode as Member_Postcode,CONVERT(nvarchar(10), dbo.yz_Member.Member_Birthday, 120) as Member_Birthday, dbo.yz_Member.Member_Email as Member_Email, dbo.yz_Member.Member_RegDate as Member_RegDate,case dbo.yz_Card.Card_State when 0 then '未激活' when 1 then '正常' when 2 then '冻结' when 3 then '注销' end as Card_State, dbo.yz_Card.Card_Point as Card_Point FROM dbo.yz_Member INNER JOIN dbo.yz_Card ON dbo.yz_Member.Member_ID = dbo.yz_Card.Member_ID where 1 =1";
            
            if (checkBox1.Checked)
            {
                s += " and (Card_Point >= " + numericUpDown1.Value.ToString() + " and Card_Point <= " + numericUpDown2.Value.ToString() + ")";
            }

            if (checkBox2.Checked)
            {
                s += " and (Member_RegDate >= '" + dateTimePicker1.Value.ToShortDateString() + "' and Member_RegDate < '" + dateTimePicker2.Value.AddDays(1).ToShortDateString() + "')";
            }

            if (checkBox3.Checked)
            {
                s += " and (Card_State = " + comboBox1.SelectedValue + ")";
            }

            if (checkBox4.Checked)
            {
                s = "SELECT dbo.yz_Card.Card_NO as Card_NO, dbo.yz_Member.Member_Name as Member_Name,CASE dbo.yz_Member.Member_Sex WHEN 1 THEN '男' ELSE '女' END as Member_Sex, dbo.yz_Member.Member_Phone as Member_Phone, dbo.yz_Member.Member_Address as Member_Address, dbo.yz_Member.Member_Postcode as Member_Postcode,CONVERT(nvarchar(10), dbo.yz_Member.Member_Birthday, 120) as Member_Birthday, dbo.yz_Member.Member_Email as Member_Email, dbo.yz_Member.Member_RegDate as Member_RegDate,case dbo.yz_Card.Card_State when 0 then '未激活' when 1 then '正常' when 2 then '冻结' when 3 then '注销' end as Card_State, dbo.yz_Card.Card_Point as Card_Point,count(*) FROM dbo.yz_Card INNER JOIN dbo.yz_OperateLog ON dbo.yz_Card.Card_NO = dbo.yz_OperateLog.Card_No INNER JOIN dbo.yz_Member ON dbo.yz_Card.Member_ID = dbo.yz_Member.Member_ID  where 1=1 and dbo.yz_OperateLog.Operation_ID=2 and dbo.yz_OperateLog.OperateLog_Date >='" + dateTimePicker3.Value.ToShortDateString() + "' and dbo.yz_OperateLog.OperateLog_Date < '" + dateTimePicker4.Value.AddDays(1).ToShortDateString() + "' group by dbo.yz_Card.Card_NO, Member_Name,Member_Sex, Member_Phone,Member_Address,Member_Postcode,Member_Birthday,Member_Email,Member_RegDate,Card_State,Card_Point";
            }

            if (checkBox5.Checked)
            {
                if (checkBox4.Checked)
                {
                    s = "SELECT dbo.yz_Card.Card_NO as Card_NO, dbo.yz_Member.Member_Name as Member_Name,CASE dbo.yz_Member.Member_Sex WHEN 1 THEN '男' ELSE '女' END as Member_Sex, dbo.yz_Member.Member_Phone as Member_Phone, dbo.yz_Member.Member_Address as Member_Address, dbo.yz_Member.Member_Postcode as Member_Postcode,CONVERT(nvarchar(10), dbo.yz_Member.Member_Birthday, 120) as Member_Birthday, dbo.yz_Member.Member_Email as Member_Email, dbo.yz_Member.Member_RegDate as Member_RegDate,case dbo.yz_Card.Card_State when 0 then '未激活' when 1 then '正常' when 2 then '冻结' when 3 then '注销' end as Card_State, dbo.yz_Card.Card_Point as Card_Point,count(*) FROM dbo.yz_Card INNER JOIN dbo.yz_OperateLog ON dbo.yz_Card.Card_NO = dbo.yz_OperateLog.Card_No INNER JOIN dbo.yz_Member ON dbo.yz_Card.Member_ID = dbo.yz_Member.Member_ID  where 1=1 and dbo.yz_OperateLog.Operation_ID=2 and dbo.yz_OperateLog.OperateLog_Date >='" + dateTimePicker3.Value.ToShortDateString() + "' and dbo.yz_OperateLog.OperateLog_Date < '" + dateTimePicker4.Value.AddDays(1).ToShortDateString() + "' group by dbo.yz_Card.Card_NO, Member_Name,Member_Sex, Member_Phone,Member_Address,Member_Postcode,Member_Birthday,Member_Email,Member_RegDate,Card_State,Card_Point having count(*)>=" + numericUpDown3.Value.ToString() + " and count(*)<=" + numericUpDown4.Value.ToString();
                }
                else
                {
                    s = "SELECT dbo.yz_Card.Card_NO as Card_NO, dbo.yz_Member.Member_Name as Member_Name,CASE dbo.yz_Member.Member_Sex WHEN 1 THEN '男' ELSE '女' END as Member_Sex, dbo.yz_Member.Member_Phone as Member_Phone, dbo.yz_Member.Member_Address as Member_Address, dbo.yz_Member.Member_Postcode as Member_Postcode,CONVERT(nvarchar(10), dbo.yz_Member.Member_Birthday, 120) as Member_Birthday, dbo.yz_Member.Member_Email as Member_Email, dbo.yz_Member.Member_RegDate as Member_RegDate,case dbo.yz_Card.Card_State when 0 then '未激活' when 1 then '正常' when 2 then '冻结' when 3 then '注销' end as Card_State, dbo.yz_Card.Card_Point as Card_Point,count(*) FROM dbo.yz_Card INNER JOIN dbo.yz_OperateLog ON dbo.yz_Card.Card_NO = dbo.yz_OperateLog.Card_No INNER JOIN dbo.yz_Member ON dbo.yz_Card.Member_ID = dbo.yz_Member.Member_ID  where 1=1 and dbo.yz_OperateLog.Operation_ID=2 group by dbo.yz_Card.Card_NO, Member_Name,Member_Sex, Member_Phone,Member_Address,Member_Postcode,Member_Birthday,Member_Email,Member_RegDate,Card_State,Card_Point having count(*)>=" + numericUpDown3.Value.ToString() + " and count(*)<=" + numericUpDown4.Value.ToString();
                }
            }

            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable16", LoadData(s)));

            this.reportViewer1.RefreshReport();
        }

        private void DataTable15BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                dateTimePicker3.Enabled = true;
                dateTimePicker4.Enabled = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
            else
            {
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
            else
            {
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
            }
        }


    }
}
