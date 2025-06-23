using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace YzMember
{

    class YzMemberClass1
    {
        //数据库连接字符串
        public static string strAuthenticateConnection = @"Provider=SQLOLEDB.1;Password=******;Persist Security Info=True;User ID=******;Initial Catalog=yz_Authenticate;Data Source=";
        public static string strConnection = @"Provider=SQLOLEDB.1;Password=******;Persist Security Info=True;User ID=******;Initial Catalog=yz;Data Source=";
        //public static string strConnection = @"Provider=SQLOLEDB.1;Password=******;Persist Security Info=True;User ID=******;Initial Catalog=yz;Data Source=young-zone.net";

        public class TBrands
        {
            public static string ID;
            public static string Brands_Name = string.Empty;
            public static string Brands_SQL = string.Empty;
            public static int Brands_CardNoLength = 0;

            public TBrands(string strID)
            {
                OleDbConnection conn = new OleDbConnection(YzMemberClass1.strAuthenticateConnection);
                OleDbCommand cmd = new OleDbCommand("", conn);
                cmd.CommandText = "select Brands_ID,Brands_Name,Brands_SQL,Brands_CardNO_Length from yz_Brands where Brands_State = 1 and Brands_ID = ?";
                cmd.Parameters.AddWithValue("@Brands_ID", strID);
                OleDbDataReader reader;
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TBrands.ID = strID;
                    TBrands.Brands_Name = reader.GetValue(1).ToString();
                    TBrands.Brands_SQL = reader.GetValue(2).ToString();
                    TBrands.Brands_CardNoLength = int.Parse(reader.GetValue(3).ToString());
                }
                conn.Close();
            }
        }
        
        //存储登录管理员的变量
        public class TAdministrator
        {
            public static int ID = 0;
            public static string Name = "";
            public static int Level = 0;
            public static int Shop = 0;
            public static string PrintName = "";  //打印机名
            public static bool BoolPrintPoint = true; //积分票据是否打印
            public static bool BoolPrintExchange = true;
            public const string DefaultPassword = "888888";
            public static DateTime dtime = DateTime.Parse("1900-01-01");
            public const double MaxPoint = 999999.99;
            public static string MAC = "";
            public static string strOperationID = "";
            public const string Version = "0.70";
        }

        public class GoodsType
        {
            public const int AddPoint = 0;   //一般的消费增加积分
            public const int PointToGift = 1; //用积分兑换的礼品,有数量限制
            public const int PointToGiftUnlimited = 2; //用积分兑换的礼品,无数量限制
            public const int FreePoint = 3;  //获赠的积分
        }


        //管理员登录验证
        public static bool Administrator_Login(string strUserName,string strPassword)
        {

            string strSql = "select Administrator_ID,Administrator_Name,AdministratorType_ID from yz_administrator where administrator_State = 1 and administrator_LoginName = ? and administrator_Password = ?";

            OleDbDataReader reader;
            OleDbConnection conn = new OleDbConnection(strConnection);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);
            OleDbParameter p = new OleDbParameter("@Name", OleDbType.VarChar, 32);
            p.Value = strUserName;
            OleDbParameter p2 = new OleDbParameter("@Password", OleDbType.VarChar, 32);
            p2.Value = strPassword;
            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p2);
            

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    TAdministrator.ID = (int)reader.GetInt32(0);
                    TAdministrator.Name = reader.GetValue(1).ToString();
                    TAdministrator.Level = (int)reader.GetInt32(2);
                    reader.Close();
                    strSql = "update yz_Administrator set Administrator_LastLoginTime = getdate(),Administrator_LoginCount = Administrator_LoginCount + 1 where Administrator_ID = " + TAdministrator.ID.ToString();
                    cmd.CommandText = strSql;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                else
                {

                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return false;
            }

 
        }

        //执行SQL语句,返回查询获得的行数
        public static int ExecSql(string strSql)
        {
            
            int i=0;
            OleDbConnection conn = new OleDbConnection(strConnection);
            OleDbCommand cmd = new OleDbCommand(strSql,conn);


            try
            {
                conn.Open();
                i = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


            return i;
        }

        //执行SQL语句,返回首字段的值
        public static string ExecSqlReturn(string strSql)
        {
            string s="";
            OleDbDataReader reader;
            OleDbConnection conn = new OleDbConnection(strConnection);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read()) s = reader.GetValue(0).ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }



            return s;
        }



        public static string filter(string InputString)
        {
            InputString = InputString.Replace("'", "''");
            InputString = InputString.Replace(@"""", @"""""");
            InputString = InputString.Replace(";", " ");
            InputString = InputString.Replace(",", ",,");
            InputString = InputString.Replace("\\", "\\\\");
            InputString = InputString.Replace("#", " ");
            InputString = InputString.Replace("--", " ");
            InputString = InputString.Replace("&", " ");
            InputString = InputString.Replace("$", " ");
            InputString = InputString.Replace("?", " ");
            InputString = InputString.Replace("%", " ");
            InputString = InputString.Replace("#", " ");

            return InputString;
        }


        //检验mdi子窗口是否重复,有重复窗口则显示窗口
        public static bool ChildFormSingleShow(Yz_Member_Main form1,string ChildName)
        {
            foreach (System.Windows.Forms.Form ChildForm in form1.MdiChildren)
            {
                if (ChildForm.Name == ChildName)
                {
                    ChildForm.Show();
                    ChildForm.Activate();
                    return true;
                }
            }
            return false;
        }

        //判别字符串是不是卡号
        public static bool IsNumber(string strNumber)
        {
            string strValidRealPattern = @"^[0-9]\d*$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsCardNumber(string strNumber)
        {
            string strValidRealPattern = @"^[0-9]{" + TBrands.Brands_CardNoLength.ToString() + @"}$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);
        }


        public static bool IsNoZeroNumber(string strNumber)
        {
            string strValidRealPattern = @"^[1-9]\d*$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);

        }


        public static bool IsScanNumber(string strNumber)
        {
            string strValidRealPattern = @"^;\d{" + TBrands.Brands_CardNoLength.ToString() + @"}\?$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsScanNumberForLogin(string strNumber)
        {
            string strValidRealPattern = @"^;\d*\?$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);
        }


        public static bool IsFloatNumber(string strNumber)
        {
            string strValidRealPattern = @"^-?\d+\.?\d{0,2}$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsAdministraotName(string strNumber)
        {
            string strValidRealPattern = @"^[^!@#$%^&;:'"",/\\|`]{1,32}$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsPassword(string strNumber)
        {
            string strValidRealPattern = @"^[^!@#$%^&*-+:'"",./\\|`]{1,32}$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);
        }


        //判断卡号的状态
        public static string GetCardState(string strCardNo)
        {
            return YzMemberClass1.ExecSqlReturn("select Card_State from yz_Card where Card_No = '" + strCardNo + "'");
        }

        //判断卡号所属ID
        public static string GetCardID(string strCardNo)
        {
            return YzMemberClass1.ExecSqlReturn("select Card_ID from yz_Card where Card_No = '" + strCardNo + "'");
        }

        //根据状态号给出中文状态
        public static string GetCardStateByStateNo(string s)
        {
            switch (s)
            {
                case "0":
                    return "未开通";
                case "1":
                    return "正常";
                case "2":
                    return "已挂失";
                case "3":
                    return "注销";
                default:
                    return "未知";

            }
        }


        //给会员卡加上积分
        public static string AddPoint(string strCardNo,float point,string Time,string Notice)
        {
            string strSql = "update yz_Card set Card_Point = Card_Point + " + point.ToString() + " where Card_NO = '" + strCardNo + "'" ;
            ExecSql(strSql);

            YzMemberClass1.WriteOperateLog(point.ToString(), "1", point.ToString(), "2", strCardNo, YzMemberClass1.TAdministrator.Shop.ToString(), YzMemberClass1.TAdministrator.ID.ToString(),Notice,Time);

            strSql = "select Card_Point from yz_Card where Card_NO = '" + strCardNo + "'";
            return ExecSqlReturn(strSql);
 
        }

        //根据卡类型给出卡内初始值
        public static string GetIntValueByCardTypeID(string CardTypeID)
        {
            return ExecSqlReturn("select CardType_IntValue from yz_CardType where CardType_ID = " + CardTypeID);
 
        }


        //根据shop_ID给出门店名
        public static string GetShopNameByID(int intID)
        {
            return YzMemberClass1.ExecSqlReturn("select Shop_Name from yz_Shop where Shop_ID = " + intID.ToString());
        }


        public static string GetAdministratorTypeByID(int intID)
        {
            return YzMemberClass1.ExecSqlReturn("select AdministratorType_Name from yz_AdministratorType where AdministratorType_ID = " + intID.ToString());
        }


        //兑换货品的操作
        public static void ExchangeGoods(string Goods_ID,string Goods_Type,string Goods_Value,string ExchangeNum,string Card_NO,string Shop_ID)
        {
            string strSql = "update yz_Card set Card_Point = Card_Point + " + Goods_Value + "*" + ExchangeNum + " where Card_NO =" + Card_NO;

            OleDbConnection conn = new OleDbConnection(strConnection);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                switch (Goods_Type)
                {
                    case "1":
                        strSql = "insert into yz_GoodsInOut(GoodsINOut_Var,Shop_ID,Goods_ID,Administrator_ID,Events_ID) values(-" + ExchangeNum + "," + Shop_ID + "," + Goods_ID + "," + TAdministrator.ID.ToString() + ",4)";
                        cmd.CommandText = strSql;
                        cmd.ExecuteNonQuery();
                        strSql = "update yz_Store set Store_Amount = Store_Amount - " + ExchangeNum + " where Shop_ID = " + Shop_ID + " and Goods_ID = " + Goods_ID;
                        cmd.CommandText = strSql;
                        cmd.ExecuteNonQuery();
                        break;
                    case "2":
                        strSql = "insert into yz_GoodsInOut(GoodsINOut_Var,Shop_ID,Goods_ID,Administrator_ID,Events_ID) values(-" + ExchangeNum + "," + Shop_ID + "," + Goods_ID + "," + TAdministrator.ID.ToString() + ",4)";
                        cmd.CommandText = strSql;
                        cmd.ExecuteNonQuery();
                        break;
                    case "3":
                        strSql = "insert into yz_GoodsInOut(GoodsINOut_Var,Shop_ID,Goods_ID,Administrator_ID,Events_ID) values(-" + ExchangeNum + "," + Shop_ID + "," + Goods_ID + "," + TAdministrator.ID.ToString() + ",4)";
                        cmd.CommandText = strSql;
                        cmd.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
 
        }


        //新建商品种类时,建立门店商品对应数据
        public static void BuildGoodsInShop(string GoodsID)
        {
            string strSql = "select Shop_ID from yz_Shop";

            OleDbDataReader reader;
            OleDbConnection conn = new OleDbConnection(strConnection);
            OleDbCommand cmd = new OleDbCommand(strSql, conn);
            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                string s = "";
                if (GetGoodsTypeByGoodsID(GoodsID) == "2") s = "99999.99"; else s = "0";

                while (reader.Read())
                {

                    strSql = "insert into yz_Store(Shop_ID,Goods_ID,Store_Amount) values(" + reader.GetValue(0).ToString() + "," + GoodsID + "," + s + ")";
                    cmd2.CommandText = strSql;
                    cmd2.ExecuteNonQuery();


                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        //进,退货
        public static void AddGoods(string ShopID,string GoodsNum,string GoodsID,string AdministratorID,string EventsID,string Notice)
        {
            try
            {
                string strSql = "update yz_Store set Store_Amount = Store_Amount + " + GoodsNum + " where Shop_ID = " + ShopID + " and Goods_ID = " + GoodsID;
                ExecSql(strSql);

                OleDbConnection conn = new OleDbConnection(strConnection);
                OleDbCommand cmd = new OleDbCommand("", conn);
                strSql = "insert into yz_GoodsInOut(GoodsInOut_Var,Goods_ID,Shop_ID,Administrator_ID,Events_ID,GoodsInOut_Notice) values(" + GoodsNum + "," + GoodsID + "," + ShopID + "," + AdministratorID + "," + EventsID + ",?)";
                cmd.CommandText = strSql;
                OleDbParameter p = new OleDbParameter("@Notice", OleDbType.VarChar, 64);
                p.Value = Notice;
                cmd.Parameters.Add(p);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
 
        }


        //根据Goods_ID给出Goods_Type
        public static string GetGoodsTypeByGoodsID(string Goods_ID)
        {
            string strSql = "select Goods_Type from yz_Goods where Goods_ID = " + Goods_ID;
            return ExecSqlReturn(strSql);
        }


        //写管理员操作日志
        public static void WriteAdminLog(string AdministratorID,string AdminTypeID,string ShopID)
        {
            string strSql = "insert into yz_AdminLog(AdminType_ID,Administrator_ID,Shop_ID) values(" + AdminTypeID + "," + AdministratorID + "," + ShopID + ")";
            ExecSql(strSql);
 
        }


        //写会员卡操作日志
        public static void WriteOperateLog(string ChangeVar,string GoodsID,string GoodsNum,string OperationID,string CardNo,string ShopID,string AdministratorID,string Notice)
        {
            try
            {
                OleDbDataReader reader;
                OleDbConnection conn = new OleDbConnection(strConnection);
                OleDbCommand cmd = new OleDbCommand("", conn);
                string strSql = "insert into yz_OperateLog(OperateLog_ChangVar,OperateLog_Notice,Card_No,Shop_ID,OperateLog_GoodsAmount,Administrator_ID,Goods_ID,Operation_ID,OperateLog_Date) values(" + ChangeVar + ",?,'" + CardNo + "'," + ShopID + "," + GoodsNum + "," + AdministratorID + "," + GoodsID + "," + OperationID + ",'" + DateTime.Now.ToString() + "');select SCOPE_IDENTITY()";
                cmd.CommandText = strSql;
                OleDbParameter p = new OleDbParameter("@Notice", OleDbType.VarChar, 64);
                p.Value = Notice;
                cmd.Parameters.Add(p);
                conn.Open();
                reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    YzMemberClass1.TAdministrator.strOperationID = reader.GetValue(0).ToString();
                }

                conn.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        public static void WriteOperateLog(string ChangeVar, string GoodsID, string GoodsNum, string OperationID, string CardNo, string ShopID, string AdministratorID, string Notice,string Time)
        {
            try
            {
                OleDbDataReader reader;
                OleDbConnection conn = new OleDbConnection(strConnection);
                OleDbCommand cmd = new OleDbCommand("", conn);
                string strSql = "insert into yz_OperateLog(OperateLog_ChangVar,OperateLog_Notice,Card_No,Shop_ID,OperateLog_GoodsAmount,Administrator_ID,Goods_ID,Operation_ID,OperateLog_Date) values(" + ChangeVar + ",?,'" + CardNo + "'," + ShopID + "," + GoodsNum + "," + AdministratorID + "," + GoodsID + "," + OperationID + ",'" + Time + "');select SCOPE_IDENTITY()";
                cmd.CommandText = strSql;
                OleDbParameter p = new OleDbParameter("@Notice", OleDbType.VarChar, 64);
                p.Value = Notice;
                cmd.Parameters.Add(p);
                conn.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    YzMemberClass1.TAdministrator.strOperationID = reader.GetValue(0).ToString();
                }

                conn.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是数据库不能连接,程序即将退出.\r\n\r\n系统错误信息:" + ex.Message, "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }


        //解析卡号为数字
        public static Int32  CardNoToInt(string CardNo)
        {
            int i = 0;
            while (CardNo.Substring(i, 1) == "0") i++;
            string s = CardNo.Substring(i);
            return Int32.Parse(s);
        }

        //解析布尔值为性别
        public static string BoolToSex(bool b)
        {
            if (b)
                return "男";
            else
                return "女";
        }


    

    }



}
