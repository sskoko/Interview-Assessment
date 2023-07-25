using System;
using SalesReportsSystem.Reports;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Data.SqlClient;


namespace SalesReportsSystem
{
    public partial class ViewMyReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "SALES")
            {
                YrDb.SelectCommand = "Select Distinct Year(SalesDate)As Year from SalesTrans where LoginStaffID='" + Session["UserId"].ToString() + "'";
            }
            else if (Session["Role"].ToString() == "MANAGER")
            {
                YrDb.SelectCommand = "Select Distinct Year(SalesDate)As Year from SalesTrans join ManageSalesStaffs on SalesTrans.LoginStaffID=ManageSalesStaffs.MyStaffID where ManageSalesStaffs.MyLoginID='" + Session["UserId"].ToString() + "'";
            }
            else
            {
                Response.Redirect("Login.aspx");
            }  
        }

        protected void btnLoadRpt_Click(object sender, EventArgs e)
        {
            string Vrpt = "";
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            if (Session["Role"].ToString() == "SALES")
            {
                Vrpt = "AnnualMyRpt";
            }
            else
            {
                Vrpt = "AnnualAgtRpt";
            }
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/"+ Vrpt + ".rdlc");
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
            string UsrV = "";

            if (Session["Role"].ToString() == "SALES")
            {
                UsrV = "LoginStaffID";
            }
            else
            {
                UsrV = "RptManagerID";
            }

            string query = "Select * from V_SalesRecords WHERE RIGHT(MthYr,4) =@Year and "+ UsrV + "=@UsrID order by MthYr";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Year", ddlYr.SelectedValue);
            cmd.Parameters.AddWithValue("@UsrID", Session["UserId"].ToString());

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