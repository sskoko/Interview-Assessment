using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encrypt_Decrypt;

namespace SalesReportsSystem
{
    public partial class UserMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"].ToString()!= "ADMIN")
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            clsEncrypt_Decrypt EncryptDecrypt = new clsEncrypt_Decrypt();

            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.UserMaster (LoginStaffID,Pwd,StaffName,Role,UpdDate,UserID) Values (@LoginStaffID,@Pwd,@StaffName,@Role,GETDATE(),@UserId)", conn);
                cmd.Parameters.AddWithValue("LoginStaffID", txtLoginID.Text);
                cmd.Parameters.AddWithValue("Pwd", EncryptDecrypt.EncryptData(txtpwd.Text));
                cmd.Parameters.AddWithValue("StaffName", txtName.Text.ToUpper());
                cmd.Parameters.AddWithValue("Role", ddlRole.SelectedValue);
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