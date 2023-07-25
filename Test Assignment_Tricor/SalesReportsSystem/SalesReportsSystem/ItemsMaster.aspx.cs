using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SalesReportsSystem
{
    public partial class ItemsMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() != "ADMIN")
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.ItemsMaster (ItemName,UpdDate,UserID) Values (@ItemName,GETDATE(),@UserId)", conn);
                cmd.Parameters.AddWithValue("ItemName", txtitem.Text);
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