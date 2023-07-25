using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SalesReportsSystem
{
    public partial class StaffsMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() != "MANAGER")
            {
                Response.Redirect("Login.aspx");
            }

            StaffsDb.SelectCommand = "Select LoginStaffID As StaffID,LoginStaffID +':'+StaffName As StaffInfo from UserMaster where Role='SALES'";
            LibraryDb.SelectCommand = "Select Row_Number() Over (Order By MyStaffID) As No,ManageSalesStaffs.ID,MyLoginID,MyStaffID,MyStaffID+'-'+StaffName As StaffInfo,ManageSalesStaffs.UpdDate,ManageSalesStaffs.UserID from ManageSalesStaffs join UserMaster On ManageSalesStaffs.MyStaffID= UserMaster.LoginStaffID where MyLoginID='" + Session["UserId"].ToString() + "'";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.ManageSalesStaffs (MyLoginID,MyStaffID,UpdDate,UserID) Values (@MyLoginID,@MyStaffID,GETDATE(),@UserId)", conn);
                cmd.Parameters.AddWithValue("MyLoginID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("MyStaffID", ddlStaff.SelectedValue);
                cmd.Parameters.AddWithValue("UserId", Session["UserId"].ToString());

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }


            }
            catch (Exception ex)
            {

            }
        }
    }
}