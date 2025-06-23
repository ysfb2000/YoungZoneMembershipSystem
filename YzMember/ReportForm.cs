using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;


namespace YzMember
{
    public partial class ReportForm : Form
    {
        OleDbConnection conn = new OleDbConnection(YzMember.YzMemberClass1.strConnection);
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {


            //绑定报表
            reportViewer1.LocalReport.ReportPath = "ReportTotal.rdlc";



            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable1", LoadData("SELECT COUNT(*) AS MemberCount FROM yz_Card WHERE (Card_State = 1)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable2", LoadData("SELECT COUNT(*) AS ManMemberCount FROM dbo.yz_Member INNER JOIN dbo.yz_Card ON dbo.yz_Member.Member_ID = dbo.yz_Card.Member_ID WHERE (dbo.yz_Member.Member_Sex = 1) AND (dbo.yz_Card.Card_State = 1)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable3", LoadData("SELECT COUNT(*) AS FelmanMemberCount FROM dbo.yz_Member INNER JOIN dbo.yz_Card ON dbo.yz_Member.Member_ID = dbo.yz_Card.Member_ID WHERE (dbo.yz_Member.Member_Sex = 0) AND (dbo.yz_Card.Card_State = 1)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable4", LoadData("SELECT SUM(Card_Point) AS TotalPoint FROM dbo.yz_Card WHERE (Card_State = 1)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable5", LoadData("SELECT SUM(OperateLog_ChangVar) AS TotalExchangePoint FROM yz_OperateLog WHERE (Operation_ID = 3) OR (Operation_ID = 11)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable6", LoadData("SELECT SUM(OperateLog_ChangVar) AS TotalAllPoint FROM yz_OperateLog WHERE (Operation_ID = 2) OR (Operation_ID = 10)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable7", LoadData("SELECT COUNT(*) AS ReportLost FROM yz_Card WHERE (Card_State = 2)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable8", LoadData("SELECT COUNT(*) AS CancelCard FROM yz_Card WHERE (Card_State = 3)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable9", LoadData("SELECT COUNT(*) AS a1to300 FROM yz_Card WHERE (Card_Point >= 1) AND (Card_Point <= 300)  AND (Card_State = 1)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable10", LoadData("SELECT COUNT(*) AS a301to1000 FROM yz_Card WHERE (Card_Point >= 301) AND (Card_Point <= 1000)  AND (Card_State = 1)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable11", LoadData("SELECT COUNT(*) AS a1001to3000 FROM yz_Card WHERE (Card_Point >= 1001) AND (Card_Point <= 3000)  AND (Card_State = 1)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable12", LoadData("SELECT COUNT(*) AS a3001to99999 FROM yz_Card WHERE (Card_Point >= 3001)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable13", LoadData("SELECT COUNT(*) AS a0to0 FROM yz_Card WHERE (Card_Point = 0) AND (Card_State = 1)")));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("yzDataSet_DataTable14", LoadData("SELECT dbo.yz_Shop.Shop_Name, COUNT(*) AS RegMemberNum FROM dbo.yz_OperateLog INNER JOIN dbo.yz_Shop ON dbo.yz_OperateLog.Shop_ID = dbo.yz_Shop.Shop_ID WHERE (dbo.yz_OperateLog.Operation_ID = 1) GROUP BY dbo.yz_Shop.Shop_Name")));
            this.reportViewer1.RefreshReport();
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

    }
}
