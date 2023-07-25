using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SalesReportsSystem
{
    public partial class SalesMaster : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() != "SALES")
            {
                Response.Redirect("Login.aspx");
            }
            ItemsDb.SelectCommand = "Select ItemName from ItemsMaster";
            LibraryDb.SelectCommand = "Select Row_Number() Over (Order By SalesDate) As No,ID,convert(varchar, SalesDate, 103) As SalesDate,SalesItem,Qty,UnitPrice,cast(round(UnitPrice*Qty,2) as numeric(15,2)) As TotalPrice,UpdDate,UserID from SalesTrans where LoginStaffID='" + Session["UserId"].ToString() + "'";
        }
        protected void OnSelectionChanged_Date(object sender, EventArgs e)
        {
            txtSalesDate.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;
        }
        protected void DateLink_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = true;
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date > DateTime.Today)
            {
                e.Cell.BackColor = System.Drawing.Color.Gray;
                e.Day.IsSelectable = false;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.SalesTrans (LoginStaffID,SalesDate,SalesItem,Qty,UnitPrice,UpdDate,UserID) Values (@LoginStaffID,@SalesDate,@SalesItem,@Qty,@UnitPrice,GETDATE(),@UserId)", conn);
                cmd.Parameters.AddWithValue("LoginStaffID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("SalesDate", Convert.ToDateTime(txtSalesDate.Text));
                cmd.Parameters.AddWithValue("SalesItem", ddlItems.SelectedValue);
                cmd.Parameters.AddWithValue("Qty", txtQty.Text);
                cmd.Parameters.AddWithValue("UnitPrice", txtUnitPrice.Text);
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
        protected void UpdateSales(object sender, GridViewUpdateEventArgs e)
        {

            int KeyID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            //string SalesDt = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEditSalesDate")).Text;
            string selectItm = (GridView1.Rows[e.RowIndex].FindControl("ddlItems") as DropDownList).SelectedItem.Text;
            string Qty = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtQty")).Text;
            string UntPrc = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtUnitPrice")).Text;
            //            LibraryDb.UpdateCommand = "UPDATE [SalesTrans] SET [SalesDate] = '" + SalesDt + "',[SalesItem] = '" + selectItm + "', [Qty] = " + Qty + ", [UnitPrice] = "+ UntPrc + ", [UpdDate] = GETDATE(),UserId='" + Session["UserId"].ToString() + "' WHERE [Id] =" + KeyID;
            LibraryDb.UpdateCommand = "UPDATE [SalesTrans] SET [SalesItem] = '" + selectItm + "', [Qty] = " + Qty + ", [UnitPrice] = "+ UntPrc + ", [UpdDate] = GETDATE(),UserId='" + Session["UserId"].ToString() + "' WHERE [Id] =" + KeyID;

        }
    }
}