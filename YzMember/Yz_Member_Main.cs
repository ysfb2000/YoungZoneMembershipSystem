using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml;



namespace YzMember
{
    public partial class Yz_Member_Main : Form
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        public Yz_Member_Main()
        {
            InitializeComponent();
        }

        private void Yz_Member_Main_Load(object sender, EventArgs e)
        {
            if (!IsConnected())
            {
                MessageBox.Show("现在不能连接互联网,此程序不能使用!","网络不可用",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }

            //YzMemberClass1.strConnection += GetConnectionString();
            YzMemberClass1.strAuthenticateConnection += GetConnectionString();


            LoginForm LoginForm1 = new LoginForm();
            if (LoginForm1.ShowDialog() != DialogResult.OK) Application.Exit();
            this.toolStripStatusLabel1.Text = "操作员:" + YzMemberClass1.TAdministrator.Name + "         ";
            this.toolStripStatusLabel2.Text = "级别:" + YzMemberClass1.GetAdministratorTypeByID(YzMemberClass1.TAdministrator.Level) + "         ";
            this.toolStripStatusLabel3.Text = "门店:" + YzMemberClass1.GetShopNameByID(YzMemberClass1.TAdministrator.Shop) + "         "; ;
            this.toolStripStatusLabel4.Text = "品牌:" + YzMemberClass1.TBrands.Brands_Name;

            SetAdministratorRights(YzMemberClass1.TAdministrator.Level.ToString()); 
            
        }

        public static string GetConnectionString()
        {
            string strConn = "";
            string strPort = "";
            XmlTextReader reader = new XmlTextReader("IP.xml");

            while (reader.Read())
            {
                if (reader.LocalName.Trim() == "DNS")
                {
                    strConn = reader.ReadString();
                }

                if (reader.LocalName == "Port")
                    strPort = reader.ReadString();


                

            }

            if (strPort.Trim() == "")
            {
                return strConn;
            }
            else
            {
                return strConn + "," + strPort;
            }


 
        }

        private bool IsConnected()
        {
            int I = 0;
            bool state = InternetGetConnectedState(out I, 0);
            return state;
        } 


        private void 会员功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form Form1 in this.MdiChildren)
            {
                Form1.Close();
            }

            基本信息维护ToolStripMenuItem.Enabled = false;
            管理员类型ToolStripMenuItem.Enabled = false;
            商品种类管理ToolStripMenuItem.Enabled = false;
            会员卡制卡ToolStripMenuItem.Enabled = false;
            权限管理ToolStripMenuItem.Enabled = false;
            会员卡开卡ToolStripMenuItem.Enabled = false;
            会员卡积分ToolStripMenuItem.Enabled = false;
            会员卡积分兑换ToolStripMenuItem.Enabled = false;
            会员卡维护ToolStripMenuItem.Enabled = false;
            会员卡积分转移ToolStripMenuItem1.Enabled = false;
            会员卡注销ToolStripMenuItem.Enabled = false;
            会员卡挂失ToolStripMenuItem1.Enabled = false;
            会员卡挂失恢复ToolStripMenuItem.Enabled = false;
            会员卡查询ToolStripMenuItem.Enabled = false;
            进货ToolStripMenuItem.Enabled = false;
            退货ToolStripMenuItem.Enabled = false;
            库存清单ToolStripMenuItem.Enabled = false;
            进出货日志ToolStripMenuItem.Enabled = false;
            货品清单ToolStripMenuItem.Enabled = false;
            管理员登录日志ToolStripMenuItem.Enabled = false;
            操作日志ToolStripMenuItem.Enabled = false;
            管理员维护ToolStripMenuItem.Enabled = false;
            修改密码ToolStripMenuItem.Enabled = false;
            会员信息修改ToolStripMenuItem.Enabled = false;
            打印机管理ToolStripMenuItem.Enabled = false;
            会员卡注销恢复ToolStripMenuItem.Enabled = false;
            会员卡交易明细ToolStripMenuItem.Enabled = false;
            报表ToolStripMenuItem.Enabled = false;
            水晶报表ToolStripMenuItem.Enabled = false;
            查询会员卡号ToolStripMenuItem.Enabled = false;
            会员交易撤销ToolStripMenuItem.Enabled = false;
            会员单据重印ToolStripMenuItem.Enabled = false;
            会员报表ToolStripMenuItem.Enabled = false;
            门店维护ToolStripMenuItem.Enabled = false;
            每月报表ToolStripMenuItem.Enabled = false;

            Yz_Member_Main_Load(sender, e);

        }

        private void 管理员类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this,"AdministratorType"))
            {
                AdministratorType frmAdminType = new AdministratorType();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡开卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "RegMemberForm"))
            {
                RegMemberForm frmAdminType = new RegMemberForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 使用文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frmAdminType = new AboutForm();
            frmAdminType.MdiParent = this;
            frmAdminType.Show();
        }

        private void 会员卡积分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "AddPointForm"))
            {
                AddPointForm frmAdminType = new AddPointForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 货品清单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ActivedGoodsForm"))
            {
                ActivedGoodsForm frmAdminType = new ActivedGoodsForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡积分兑换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ExchangeForm"))
            {
                ExchangeForm frmAdminType = new ExchangeForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 商品种类管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "AddGoodsKindForm"))
            {
                AddGoodsKindForm frmAdminType = new AddGoodsKindForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
       
        }

        private void 进货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "AddGoodsForm"))
            {
                AddGoodsForm frmAdminType = new AddGoodsForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 退货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "SubGoodsForm"))
            {
                SubGoodsForm frmAdminType = new SubGoodsForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 库存清单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "StoreListForm"))
            {
                StoreListForm frmAdminType = new StoreListForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 进出货日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "GoodsInOutForm"))
            {
                GoodsInOutForm frmAdminType = new GoodsInOutForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 管理员登录日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "AdminLogListForm"))
            {
                AdminLogListForm frmAdminType = new AdminLogListForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡制卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "MakeCardForm"))
            {
                MakeCardForm frmAdminType = new MakeCardForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 操作日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "OperationListForm"))
            {
                OperationListForm frmAdminType = new OperationListForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡积分转移ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "MovePointForm"))
            {
                MovePointForm frmAdminType = new MovePointForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "CancelCardForm"))
            {
                CancelCardForm frmAdminType = new CancelCardForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡挂失ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ReportLostForm"))
            {
                ReportLostForm frmAdminType = new ReportLostForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡挂失恢复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "CancelLostForm"))
            {
                CancelLostForm frmAdminType = new CancelLostForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "InfoMemberForm"))
            {
                InfoMemberForm frmAdminType = new InfoMemberForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 权限管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "RightsForm"))
            {
                RightsForm frmAdminType = new RightsForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void SetAdministratorRights(string TypeID)
        {
            OleDbCommand cmd;
            OleDbConnection conn;
            OleDbDataReader reader;
            string strSql = "SELECT dbo.yz_Authority.Authority_ID, dbo.yz_Authority.Authority_Control FROM dbo.yz_Authority_AdministratorType INNER JOIN dbo.yz_Authority ON dbo.yz_Authority_AdministratorType.Authority_ID = dbo.yz_Authority.Authority_ID where AdministratorType_ID = " + TypeID;
            conn = new OleDbConnection(YzMemberClass1.strConnection);
            cmd = new OleDbCommand("", conn);
            cmd.CommandText = strSql;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    switch (reader.GetValue(0).ToString())
                    {
                        case "1":
                            基本信息维护ToolStripMenuItem.Enabled = true;
                            break;
                        case "2":
                            管理员类型ToolStripMenuItem.Enabled = true;
                            break;
                        case "3":
                            商品种类管理ToolStripMenuItem.Enabled = true;
                            break;
                        case "4":
                            会员卡制卡ToolStripMenuItem.Enabled = true;
                            break;
                        case "5":
                            权限管理ToolStripMenuItem.Enabled = true;
                            break;
                        case "6":
                            会员卡开卡ToolStripMenuItem.Enabled = true;
                            break;
                        case "7":
                            会员卡积分ToolStripMenuItem.Enabled = true;
                            break;
                        case "8":
                            会员卡积分兑换ToolStripMenuItem.Enabled = true;
                            break;
                        case "9":
                            会员卡维护ToolStripMenuItem.Enabled = true;
                            break;
                        case "10":
                            会员卡积分转移ToolStripMenuItem1.Enabled = true;
                            break;
                        case "11":
                            会员卡注销ToolStripMenuItem.Enabled = true;
                            break;
                        case "12":
                            会员卡挂失ToolStripMenuItem1.Enabled = true;
                            break;
                        case "13":
                            会员卡挂失恢复ToolStripMenuItem.Enabled = true;
                            break;
                        case "14":
                            会员卡查询ToolStripMenuItem.Enabled = true;
                            break;
                        case "15":
                            进货ToolStripMenuItem.Enabled = true;
                            break;
                        case "16":
                            退货ToolStripMenuItem.Enabled = true;
                            break;
                        case "17":
                            库存清单ToolStripMenuItem.Enabled = true;
                            break;
                        case "18":
                            进出货日志ToolStripMenuItem.Enabled = true;
                            break;
                        case "19":
                            货品清单ToolStripMenuItem.Enabled = true;
                            break;
                        case "20":
                            管理员登录日志ToolStripMenuItem.Enabled = true;
                            break;
                        case "21":
                            操作日志ToolStripMenuItem.Enabled = true;
                            break;
                        case "22":
                            修改密码ToolStripMenuItem.Enabled = true;
                            break;
                        case "23":
                            打印机管理ToolStripMenuItem.Enabled = true;
                            break;
                        case "24":
                            会员信息修改ToolStripMenuItem.Enabled = true;
                            break;
                        case "25":
                            管理员维护ToolStripMenuItem.Enabled = true;
                            break;
                        case "26":
                            会员卡注销恢复ToolStripMenuItem.Enabled = true;
                            break;
                        case "27":
                            会员卡交易明细ToolStripMenuItem.Enabled = true;
                            break;
                        case "28":
                            报表ToolStripMenuItem.Enabled = true;
                            break;
                        case "29":
                            水晶报表ToolStripMenuItem.Enabled = true;
                            break;
                        case "30":
                            查询会员卡号ToolStripMenuItem.Enabled = true;
                            break;
                        case "31":
                            会员交易撤销ToolStripMenuItem.Enabled = true;
                            break;
                        case "32":
                            会员单据重印ToolStripMenuItem.Enabled = true;
                            break;
                        case "33":
                            会员报表ToolStripMenuItem.Enabled = true;
                            break;
                        case "34":
                            门店维护ToolStripMenuItem.Enabled = true;
                            break;
                        case "35":
                            每月报表ToolStripMenuItem.Enabled = true;
                            break;
                            
                        default:
                            MessageBox.Show(reader.GetValue(0).ToString());
                            break;
                    }

                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


 
        }

        private void menuStrip1_Resize(object sender, EventArgs e)
        {
            
        }

        private void 管理员维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "AddAdministratorForm"))
            {
                AddAdministratorForm frmAdminType = new AddAdministratorForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ChangePasswordForm"))
            {
                ChangePasswordForm frmAdminType = new ChangePasswordForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员信息修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "EditMemberForm"))
            {
                EditMemberForm frmAdminType = new EditMemberForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }

        }

        private void 打印机管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "PrinterManagementForm"))
            {
                PrinterManagementForm frmAdminType = new PrinterManagementForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡注销恢复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ReCoverCardForm"))
            {
                ReCoverCardForm frmAdminType = new ReCoverCardForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员卡交易明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TradeDetailForm frmAdminType = new TradeDetailForm();
            frmAdminType.MdiParent = this;
            frmAdminType.Show();
        }

        private void 水晶报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ReportForm"))
            {
                ReportForm frmAdminType = new ReportForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查询会员卡号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "SearchForCardNoForm"))
            {
                SearchForCardNoForm frmAdminType = new SearchForCardNoForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员交易撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "RePrintForm"))
            {
                RePrintForm frmAdminType = new RePrintForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员单据重印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "RePrintForm2"))
            {
                RePrintForm2 frmAdminType = new RePrintForm2();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 会员报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ReportMemberForm"))
            {
                ReportMemberForm frmAdminType = new ReportMemberForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void 门店维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ShopForm"))
            {
                ShopForm frmAdminType = new ShopForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }

        private void Yz_Member_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 每月报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!YzMember.YzMemberClass1.ChildFormSingleShow(this, "ReportMonthlyForm"))
            {
                ReportMonthlyForm frmAdminType = new ReportMonthlyForm();
                frmAdminType.MdiParent = this;
                frmAdminType.Show();
            }
        }




    }
}
