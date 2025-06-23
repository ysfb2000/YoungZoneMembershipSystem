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
    public partial class RightsForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd;
        DataSet dataSet1 = new DataSet();
        OleDbDataReader reader;

        public RightsForm()
        {
            InitializeComponent();
        }

        private void RightsForm_Load(object sender, EventArgs e)
        {
            try
            {
                string strSql = "select AdministratorType_ID,AdministratorType_Name from yz_AdministratorType";
                cmd = new OleDbCommand(strSql, conn);
                da.SelectCommand = cmd;
                da.Fill(dataSet1, "a1");
                comboBox1.DataSource = dataSet1.Tables["a1"];
                comboBox1.DisplayMember = "AdministratorType_Name";
                comboBox1.ValueMember = "AdministratorType_ID";

                strSql = "select Administrator_ID as ID, Administrator_Name as 操作员名 from yz_Administrator where AdministratorType_ID = " + comboBox1.SelectedValue.ToString();
                cmd.CommandText = strSql;
                da.SelectCommand = cmd;
                if (!dataSet1.Tables.Contains("a2")) da.Fill(dataSet1, "a2");
                dataGridView1.DataSource = dataSet1.Tables["a2"];

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.RowHeadersWidth = 20;
                dataGridView1.Columns[0].Width = 60;

                cmd.CommandText = "select Authority_Name from yz_Authority";

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    checkedListBox1.Items.Add(reader.GetValue(0).ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            ShowAdministratorTypeRights(comboBox1.SelectedValue.ToString());

        }


        private void ShowAdministratorTypeRights(string AdministratorType_ID)
        {
            string strSql = "select Authority_ID from yz_Authority_AdministratorType where AdministratorType_ID = " + AdministratorType_ID;
            cmd.CommandText = strSql;
            da.SelectCommand = cmd;

            try
            {
                if (dataSet1.Tables.Contains("a3")) dataSet1.Tables["a3"].Clear();
                da.Fill(dataSet1, "a3");

                for (int i = 0; i < dataSet1.Tables["a3"].Rows.Count; i++)
                {

                    checkedListBox1.SetItemChecked(Int32.Parse(dataSet1.Tables["a3"].Rows[i][0].ToString()) - 1, true);

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
 
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dataSet1.Tables.Contains("a2"))
            {
                try
                {
                    string strSql;
                    strSql = "select Administrator_ID as ID, Administrator_Name as 操作员名 from yz_Administrator where AdministratorType_ID = " + comboBox1.SelectedValue.ToString();
                    cmd.CommandText = strSql;
                    da.SelectCommand = cmd;
                    dataSet1.Tables["a2"].Clear();
                    da.Fill(dataSet1, "a2");

                    cmd.CommandText = "select Authority_Name from yz_Authority";

                    conn.Open();
                    reader = cmd.ExecuteReader();
                    checkedListBox1.Items.Clear();

                    while (reader.Read())
                    {
                        checkedListBox1.Items.Add(reader.GetValue(0).ToString());
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

                ShowAdministratorTypeRights(comboBox1.SelectedValue.ToString());
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i = 0;
            string strSql = "";
            string strID = "";

            if (checkedListBox1.SelectedItem != null)
            {
                i = checkedListBox1.Items.IndexOf(checkedListBox1.SelectedItem);
                strID = getAuthorityIDByName(checkedListBox1.Items[i].ToString());

                

                if (!checkedListBox1.GetItemChecked(i))
                {
                    
                    strSql = "insert into yz_Authority_AdministratorType(AdministratorType_ID,Authority_ID) values(" + comboBox1.SelectedValue.ToString()  + "," + strID + ")";
                    YzMemberClass1.ExecSql(strSql);
                }
                else
                {
                    strSql = "delete from yz_Authority_AdministratorType where Authority_ID = " + strID + " and AdministratorType_ID = " + comboBox1.SelectedValue.ToString();
                    YzMemberClass1.ExecSql(strSql);
                }

                
            }
        }

        private string getAuthorityIDByName(string AuthorityName)
        {
            string strSql = "select Authority_ID from yz_Authority where Authority_Name = '" + AuthorityName + "'";
            return YzMemberClass1.ExecSqlReturn(strSql);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        


    }
}
