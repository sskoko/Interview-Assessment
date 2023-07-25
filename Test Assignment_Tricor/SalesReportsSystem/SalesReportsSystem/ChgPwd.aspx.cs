using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Encrypt_Decrypt;

namespace SalesReportsSystem
{
    public partial class ChgPwd : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() != "ADMIN")
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void UpdatePwd(object sender, GridViewUpdateEventArgs e)
        {
            clsEncrypt_Decrypt EncryptDecrypt = new clsEncrypt_Decrypt();
            int KeyID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            string pwd = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPwd")).Text;

            LibraryDb.UpdateCommand = "UPDATE [UserMaster] SET [Pwd] ='"+ EncryptDecrypt.EncryptData(pwd) + "',[UpdDate]=GETDATE(),UserId='" + Session["UserId"].ToString() + "' WHERE [Id] =" + KeyID;
        }
    }
}