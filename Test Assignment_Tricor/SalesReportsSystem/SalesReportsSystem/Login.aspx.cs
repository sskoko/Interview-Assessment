using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encrypt_Decrypt;

namespace SalesReportsSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Request.Cookies.Remove("user");
                Session.RemoveAll();
            }
            catch (Exception)
            {


            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginDetails log = ValidateUser(inputLogin.Value, inputPassword.Value);
            
            if (log.IsAuthUser)
            {
                Session["userid"] = log.UserId;
                Session["username"] = log.UserName;
                Session["IsAuth"] = log.IsAuthUser;
                Session["role"] = log.Role;
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        private LoginDetails ValidateUser(string username, string password)
        {
            clsEncrypt_Decrypt EncryptDecrypt = new clsEncrypt_Decrypt();
            LoginDetails obj = new LoginDetails();
            obj.IsAuthUser = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
                SqlDataAdapter da;
                DataSet ds = new DataSet();
                string query = "select * from UserMaster where LoginStaffID='" + username.Trim() + "' and Pwd='" + EncryptDecrypt.EncryptData(password.Trim()) + "'";
                da = new SqlDataAdapter(query, con);
                con.Open();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    obj.IsAuthUser = true;
                    obj.UserName = ds.Tables[0].Rows[0]["StaffName"].ToString();
                    obj.UserId = ds.Tables[0].Rows[0]["LoginStaffID"].ToString();
                    obj.Role = ds.Tables[0].Rows[0]["Role"].ToString();
                }
            }
            catch (Exception ex)
            {
                obj.IsAuthUser = false;
            }
            return obj;

        }

        private struct LoginDetails
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Role { get; set; }
            public bool IsAuthUser { get; set; }
        }
    }
}