using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

public partial class Shop_Order : System.Web.UI.Page
{
    SqlHelper sh = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useraccount"] == null)
        {
            Response.Redirect("~/Shop/Login.aspx");
        }
        else
        {
            lblUser.Text = Session["useraccount"].ToString();
            if (!Page.IsPostBack)
            {
                ((HtmlImage)Master.FindControl("ContentPlaceHolder1").FindControl("imgExtend")).Attributes.Add("onClick", "DisplayAddr()");
                if (Session["useraccount"] != null)
                {
                    DataTable dt = Session["cart"] as DataTable;
                    gvInfo.DataSource = dt;
                    gvInfo.DataBind();
                }
                Label1.Text = Convert.ToString(DateTime.UtcNow.Millisecond);
                string sql = string.Format("select customer_id from t_Customer where Customer_Account='{0}'", lblUser.Text);
                SqlDataReader sdr = sh.QueryOperation(sql);
                int customerid = -1;
                if (sdr.HasRows)
                {
                    sdr.Read();
                    customerid = sdr.GetInt32(0);
                }
                sdr.Close();//关闭SqlDataReader对象
                string sql3 = string.Format("select * from t_CustomerAddress where Customer_id='{0}'", customerid);
                SqlDataReader sdr2 = sh.QueryOperation(sql3);
                ddlAddress.DataSource = sdr2;
                ddlAddress.DataTextField = "Customer_Address";
                ddlAddress.DataValueField = "CustomerAddress_ID";//添加上述两行
                ddlAddress.DataBind();
                sdr2.Close();//关闭SqlDataReader对象
                sh.closeConn();
            }
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(TextBox1.Text.Trim()))
            {
                string sql = string.Format("select customer_id from t_Customer where Customer_Account='{0}'", lblUser.Text);
                SqlDataReader sdr = sh.QueryOperation(sql);
                int customerid = -1;
                if (sdr.HasRows)
                {
                    sdr.Read();
                    customerid = sdr.GetInt32(0);
                }
                sdr.Close();//关闭SqlDataReader对象

                string sql2 = string.Format("insert into t_CustomerAddress values({0},'{1}')", customerid, TextBox1.Text);
                sh.ExeNonQuery(sql2);
                string sql3 = string.Format("select * from t_CustomerAddress where Customer_id='{0}'", customerid);
                SqlDataReader sdr2 = sh.QueryOperation(sql3);
                ddlAddress.DataSource = sdr2;
                ddlAddress.DataBind();
                sdr2.Close();//关闭SqlDataReader对象
                sh.closeConn();
            }
            else
            {
                Response.Write("<script>alert('请填写内容！')</script>");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        SqlConnection con = sh.getConn();
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        try
        {
            string sql = string.Format("select customer_id from t_Customer where Customer_Account='{0}'", lblUser.Text);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Transaction = trans;
            SqlDataReader sdr = cmd.ExecuteReader();
            int customerid = -1;
            if (sdr.HasRows)
            {
                sdr.Read();
                customerid = sdr.GetInt32(0);
            }
            sdr.Close();
            int CustomerAddressID = -1;
            CustomerAddressID = Convert.ToInt32(ddlAddress.SelectedValue.ToString());
            string sql2 = string.Format("insert into t_Order values('{0}',{1},{2},'{3}',{4},{5},'{6}');select cast(scope_identity() as int)", TextBox1.Text, customerid, CustomerAddressID, "货到付款", 0, 0, DateTime.Now);
            cmd.CommandText = sql2;
            int OrderID = (int)cmd.ExecuteScalar();
            for (int i = 0; i < gvInfo.Rows.Count; i++)
            {
                int WareID = Convert.ToInt32(gvInfo.Rows[i].Cells[0].Text);
                float qty = Convert.ToInt32(gvInfo.Rows[i].Cells[4].Text);
                float Wprice = Convert.ToInt32(gvInfo.Rows[i].Cells[3].Text);
                float Wsum = Wprice * qty;
                string sql3 = string.Format("insert into T_ShoppingCart values({0},{1},{2},{3},{4})", customerid, WareID, qty, Wsum, OrderID);
                cmd.CommandText = sql3;
                cmd.ExecuteNonQuery();
            }
            trans.Commit();
        }
        catch (Exception)
        {
            trans.Rollback();
        }
        finally
        {
            con.Close();
            Session["cart"] = null;
            Response.Redirect("~/Shop/WareListView.aspx");
        }
    }
}
