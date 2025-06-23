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
    public partial class ShopForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();
        BindingSource bs = new BindingSource();
        string strID;

        public ShopForm()
        {
            InitializeComponent();
        }

        private void ShopForm_Load(object sender, EventArgs e)
        {
            string strSql = "SELECT dbo.yz_Shop.Shop_ID, dbo.yz_Shop.Shop_Name as 门店名, dbo.yz_ShopType.ShopType_Name as 门店类型 FROM dbo.yz_Shop INNER JOIN dbo.yz_ShopType ON dbo.yz_Shop.ShopType_ID = dbo.yz_ShopType.ShopType_ID";
            cmd = new OleDbCommand(strSql, conn);
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
            dataGridView1.Columns[1].Width = 120;




            strSql = "SELECT ShopType_ID, ShopType_Name FROM dbo.yz_ShopType";
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

            comboBox1.DisplayMember = "ShopType_Name";
            comboBox1.ValueMember = "ShopType_ID";
            comboBox1.DataSource = dataSet1.Tables["a2"];

            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void SetShopInfo()
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            strID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            SetShopInfo();
            button1.Enabled = false;

            textBox1.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!YzMemberClass1.IsAdministraotName(textBox1.Text))
            {
                MessageBox.Show("门店名不能为空或包含非法字符!");
                return;
            }


            if (YzMemberClass1.ExecSql("select * from yz_Shop where Shop_Name = '" + textBox1.Text.Trim() + "' and Shop_ID <> " + strID) != 0)
            {
                MessageBox.Show("门店名重复!");
                return;

            }


            string strSql = "update yz_Shop set Shop_Name = '" + textBox1.Text + "',ShopType_ID = " + comboBox1.SelectedValue.ToString();
            strSql += "  where Shop_ID = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (MessageBox.Show("是否保存修改?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).ToString() == "OK")
            {
                YzMemberClass1.ExecSql(strSql);
                MessageBox.Show("保存成功");
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "13", YzMemberClass1.TAdministrator.Shop.ToString());
                DataGridDataBind();

            }
        }

        private void DataGridDataBind()
        {
            string strSql = "SELECT dbo.yz_Shop.Shop_ID, dbo.yz_Shop.Shop_Name as 门店名, dbo.yz_ShopType.ShopType_Name as 门店类型 FROM dbo.yz_Shop INNER JOIN dbo.yz_ShopType ON dbo.yz_Shop.ShopType_ID = dbo.yz_ShopType.ShopType_ID";
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
            comboBox1.SelectedIndex = 1;

            button1.Enabled = false;
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddShopForm nform = new AddShopForm();

            if (nform.ShowDialog() == DialogResult.OK)
            {
                DataGridDataBind();
            }
        }


    }
}
