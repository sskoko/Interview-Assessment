using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SalesReportsSystem
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        public string myrole = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Userid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            try
            {
                if (Convert.ToBoolean(Session["IsAuth"]))
                {
                    switch (Session["Role"].ToString())
                    {
                        case "SALES":
                            myrole = "<ul class='nav navbar-nav' id='mSales'><li><a href='Home.aspx'>Home</a></li><li><a href='SalesMaster.aspx'>Sales Records</a></li><li><a href='ViewMyReports.aspx'>View Reports</a></li></ul>";
                            break;
                        case "MANAGER":
                            myrole = "<ul class='nav navbar-nav' id='mManager'><li><a href='Home.aspx'>Home</a></li><li><a href='StaffsMaster.aspx'>Staffs Master</a></li><li><a href='ViewReports.aspx'>View Agents Reports</a></li><li><a href='ViewMyReports.aspx'>View Annual Reports</a></li></ul>";
                            break;
                        case "ADMIN":
                            myrole = "<ul class='nav navbar-nav' id='mAdmin'><li><a href='Home.aspx'>Home</a></li><li><a href='UserMaster.aspx'>Users Master</a></li><li><a href='ItemsMaster.aspx'>Items Master</a></li></li><li><a href='ChgPwd.aspx'>Change Password</a></li></ul>";
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception EX)
            {

                Response.Redirect("Login.aspx");
            }
        }
    }
}