using System;
using SalesReportsSystem.Reports;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Data.SqlClient;

namespace SalesReportsSystem
{
    public partial class ViewReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() != "MANAGER")
            {
                Response.Redirect("Login.aspx");
            }

            StaffDb.SelectCommand = "Select MyStaffID,MyStaffID+'-'+StaffName As StaffInfo from ManageSalesStaffs join UserMaster On ManageSalesStaffs.MyStaffID= UserMaster.LoginStaffID where MyLoginID='" + Session["UserId"].ToString() + "'";
            YrDb.SelectCommand = "Select Distinct FORMAT(SalesDate, 'MM/yyyy')As Year from SalesTrans join ManageSalesStaffs on SalesTrans.LoginStaffID=ManageSalesStaffs.MyStaffID where ManageSalesStaffs.MyLoginID='" + Session["UserId"].ToString() + "' and SalesTrans.LoginStaffID=(Select Top 1 MyStaffID from ManageSalesStaffs join UserMaster On ManageSalesStaffs.MyStaffID= UserMaster.LoginStaffID where MyLoginID = '" + Session["UserId"].ToString() + "')";
        }
        protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            YrDb.SelectCommand = "Select Distinct FORMAT(SalesDate, 'MM/yyyy')As Year from SalesTrans join ManageSalesStaffs on SalesTrans.LoginStaffID=ManageSalesStaffs.MyStaffID where ManageSalesStaffs.MyLoginID='" + Session["UserId"].ToString() + "' and SalesTrans.LoginStaffID='" + ddlStaff.SelectedValue + "'";
        }
        protected void btnLoadRpt_Click(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/MyAgtMonthlyRpt.rdlc");
            ReportParameter parameter = new ReportParameter("Year", ddlYr.SelectedValue);
            ReportViewer1.LocalReport.SetParameters(parameter);
            DataSetSales Dtsales = getSalesData();
            ReportDataSource dataSource = new ReportDataSource("DataSetRcds", Dtsales.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(dataSource);
        }

        private DataSetSales getSalesData()
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
            string query = "Select * from V_SalesRecords WHERE MthYr =@Year and RptManagerID = @UsrID and LoginStaffID=@MyStaffID";
            SqlCommand cmd = new SqlCommand(query);
            string a = ddlYr.SelectedValue;
            string b = ddlStaff.SelectedValue;
            cmd.Parameters.AddWithValue("@Year", ddlYr.SelectedValue);
            cmd.Parameters.AddWithValue("@UsrID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@MyStaffID", ddlStaff.SelectedValue);

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                using (DataSetSales sales = new DataSetSales())
                {
                    da.Fill(sales, "SalesRecords");
                    return sales;
                }
            }
        }
    }
}