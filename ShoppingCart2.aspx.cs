using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class Shop_ShoppingCart2 : System.Web.UI.Page
{

    int ID = -1;
    string ProductNO = "";
    //整型变量，用于存储总金额
    private Int32 Total = 0;
    
    SqlHelper sh = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ID = Convert.ToInt32(Request.QueryString["wid"].ToString());
                ProductNO = Request.QueryString["wnum"].ToString();
                BindList();
            }
            catch (Exception ee)
            {
                //错误后将要转入指定页面
                //Response.Redirect("index.aspx");
                //Response.Write(ee);
            }
        }
    }

    public void BindList()
    {
        DataTable dt = new DataTable();
        if (Session["cart"] != null)
        {
            dt = (DataTable)Session["cart"];
        }
        else
        {
            dt.Columns.Add(new DataColumn("ID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("ProductNO", typeof(String)));
            dt.Columns.Add(new DataColumn("ProductName", typeof(String)));
            dt.Columns.Add(new DataColumn("BuyPrice", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Amount", typeof(Int32)));
        }
        if (ID != -1)
        {
            bool isExisted = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ProductNO"].ToString().Trim() == ProductNO)
                {
                    isExisted = true;
                    break;
                }
            }

            if (!isExisted)
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter();
                p[0].ParameterName = "@Ware_ID";
                p[0].Value = ID;
                p[0].SqlDbType = SqlDbType.Int;
                DataTable dtproduct = sh.QueryOperationProc("sq_select2_id", p);

                dt.Rows.Add(new object[]{
                ID,
                dtproduct.Rows[0]["Ware_Number"].ToString(),
                dtproduct.Rows[0]["Ware_Name"].ToString(),
                Convert.ToInt32(dtproduct.Rows[0]["Ware_Price"]),
                1
                });
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alertExist", "alert('您选择的商品（编号：" + ProductNO + "）已在购物车！')", true);
            }
        }
        gvCart.DataSource = dt;
        gvCart.DataBind();
        Session.Add("cart", dt);
    }

    protected void gvCart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType==DataControlRowType.DataRow)
        {
            //GridView行的加亮显示功能
            e.Row.Attributes.Add("onmouseover", "b=this.style.backgroundColor;this.style.backgroundColor='#83D2FD'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=b");

            //给+号图片和-号图片添加客户端click事件
            //用JavaScript实现数量的+1和-1
            TextBox tb = (TextBox)e.Row.FindControl("txtAmount");
            ((HtmlImage)e.Row.FindControl("imgReduce")).Attributes.Add("onclick", "Reduce(" + tb.ClientID + ")");
            ((HtmlImage)e.Row.FindControl("imgPlus")).Attributes.Add("onclick", "Plus(" + tb.ClientID + ")");

            //根据商品单价和数量计算购物车中商品的总金额
            DataRowView drv = (DataRowView)e.Row.DataItem;
            Total += Int32.Parse(drv["BuyPrice"].ToString()) * Int32.Parse(tb.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //将总金额显示在金额一列对应的Footer单元格
            e.Row.Cells[1].Text = "金额总计：";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Text = Total.ToString("c2");
            e.Row.Cells[2].ForeColor = Color.Red;
        }
    }
    protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //点击删除时从DataTable中删除对应的数据行
        if (Session["Cart"] != null)
        {
            DataTable dt = (DataTable)Session["Cart"];
            dt.Rows.RemoveAt(e.RowIndex);
            dt.AcceptChanges();
            Session["Cart"] = dt;
            BindList();
        }
    }



    protected void imgbtnTotal_Click(object sender, ImageClickEventArgs e)
    {
        //遍历GridView，根据每行的文本框中的值
        //修改DataTable中对应行中数量一列的值
        if (Session["Cart"] != null)
        {
            DataTable dt = (DataTable)Session["Cart"];
            for (int i = 0; i < gvCart.Rows.Count; i++)
            {
                dt.Rows[i]["Amount"] = ((TextBox)gvCart.Rows[i].FindControl("txtAmount")).Text;
            }
            dt.AcceptChanges();
            Session["Cart"] = dt;
            BindList();
        }
    }
}