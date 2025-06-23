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
    public partial class AddShopForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();
        BindingSource bs = new BindingSource();

        public AddShopForm()
        {
            InitializeComponent();
        }

        private void AddShopForm_Load(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("", conn);
            string strSql = "select ShopType_ID,ShopType_Name from yz_ShopType";
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!YzMemberClass1.IsAdministraotName(textBox1.Text))
            {
                MessageBox.Show("门店名不能为空或包含非法字符!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }



            if (YzMemberClass1.ExecSql("select 1 from yz_Shop where Shop_Name = '" + textBox1.Text + "'") == -1)
            {

                MessageBox.Show("门店名已存在!");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            string strSql = "insert into yz_Shop(Shop_Name,ShopType_ID,Shop_State) values('" + textBox1.Text + "'," + comboBox1.SelectedValue.ToString() + ",1)";


            if (MessageBox.Show("是否新增门店?", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).ToString() == "OK")
            {
                YzMemberClass1.ExecSql(strSql);
                MessageBox.Show("新增成功");
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "14", YzMemberClass1.TAdministrator.Shop.ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
