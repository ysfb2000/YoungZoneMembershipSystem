using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace YzMember
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            YzMemberClass1.TAdministrator.MAC = GetMAC();
            //OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
            OleDbConnection conn2 = new OleDbConnection(YzMemberClass1.strAuthenticateConnection);
            OleDbCommand cmd2 = new OleDbCommand("", conn2);
            OleDbCommand cmd3 = new OleDbCommand("", conn2);

            cmd3.CommandText = "select mac_ID from yz_mac_Authenticate where mac_state = 1 and mac_ID = '" + YzMemberClass1.TAdministrator.MAC + "'";
            conn2.Open();
            if (cmd3.ExecuteScalar() == null)
            {
                conn2.Close();
                MessageBox.Show("品牌不允许的软件运行地点!");
                Application.Exit();
            }
            conn2.Close();

            cmd2.CommandText = "select Brands_Name,Brands_ID from yz_Brands where Brands_State = 1 order by Brands_Sort asc";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd2);
            DataSet ds = new DataSet();
            da.Fill(ds, "a1");
            comboBox1.DataSource = ds.Tables["a1"].DefaultView;
            comboBox1.DisplayMember = "Brands_Name";
            comboBox1.ValueMember = "Brands_ID";




        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (YzMember.YzMemberClass1.IsAdministraotName(textBox1.Text) != true)
            {
                label3.Text = "用户名中包含非法字符!";
                return;
            }

            new YzMemberClass1.TBrands(comboBox1.SelectedValue.ToString());


            YzMemberClass1.strConnection = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;User ID=youngzone;Initial Catalog=YoungZone;Data Source=XJL-WorkPad\SQLEXPRESS";
            OleDbConnection conn = new OleDbConnection(YzMemberClass1.strConnection);
            OleDbCommand cmd = new OleDbCommand("", conn);
            string strSql = "select Shop_ID,PrinterName,PointPrint,GoodsPrint from yz_Mac where Mac_ID = '" + YzMemberClass1.TAdministrator.MAC + "'";
            cmd.CommandText = strSql;
            OleDbDataReader reader;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                YzMemberClass1.TAdministrator.Shop = int.Parse(reader.GetValue(0).ToString());
                YzMemberClass1.TAdministrator.PrintName = reader.GetValue(1).ToString();
                if (reader.GetValue(2).ToString() == "")
                {
                    YzMemberClass1.TAdministrator.BoolPrintPoint = false;
                }
                else
                {
                    YzMemberClass1.TAdministrator.BoolPrintPoint = reader.GetBoolean(2);
                }

                if (reader.GetValue(3).ToString() == "")
                {
                    YzMemberClass1.TAdministrator.BoolPrintExchange = false;
                }
                else
                {
                    YzMemberClass1.TAdministrator.BoolPrintExchange = reader.GetBoolean(3);
                }
                

                reader.Close();


            }
            else
            {
                MessageBox.Show("不合法的软件运行地点!");
                Application.Exit();
            }

            conn.Close();


            
            if (YzMember.YzMemberClass1.Administrator_Login(this.textBox1.Text.Trim(), this.textBox2.Text))
            {
                YzMemberClass1.WriteAdminLog(YzMemberClass1.TAdministrator.ID.ToString(), "1", YzMemberClass1.TAdministrator.Shop.ToString());
                this.DialogResult = DialogResult.OK;
                
                this.Close();
            }

            this.label3.Text = "用户名或密码错误!";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.label3.Text = "";

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.label3.Text = "";
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
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }

        private static string GetMAC()
        {
            
            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"])
                        return mo["MacAddress"].ToString();
                }
            }

            return null;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
