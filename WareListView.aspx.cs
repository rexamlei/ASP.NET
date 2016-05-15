using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shop_WareListView : System.Web.UI.Page
{
    static int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getData(1);
        }
    }

    /// <summary>
    /// 获取数据通过SqlDataAdapter对象填充到DataTable中
    /// </summary>
    /// <returns>DataTable</returns>
    private DataTable getData_stu()
    {
        string sql = "select * from T_Ware";
        SqlConnection conn;
        SqlCommand cmd;
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SmartConnectionString"].ToString());
        cmd = new SqlCommand(sql, conn);
        conn.Open();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        conn.Close();
        return dt;
    }

    /// <summary>
    /// 利用PagedDataSource进行分页
    /// </summary>
    /// <param name="pageIndex">当前第几页</param>
    private void getData(int pageIndex)
    {
        PagedDataSource pd = new PagedDataSource();       //声明PagedDataSource对象
        pd.AllowPaging = true;                            //允许分页
        pd.PageSize = 10;                                 //每一页显示10条记录
        pd.CurrentPageIndex = pageIndex - 1;              //获取当前页索引，如果第1页，索引就等与0
        pd.DataSource = getData_stu().DefaultView;        //获取当前数据表
        DataList1.DataSource = pd;                        //绑定该数据到DataList1控件
        DataList1.DataBind();
        lblCurrent.Text = Convert.ToString(pd.CurrentPageIndex + 1);//显示当前在第几页
        lblCount.Text = "/ ";
        lblCount.Text += pd.PageCount.ToString();                   //显示总页数
        i = pd.PageCount;                                         //获取总页数，放在变量i中
        if (pd.IsFirstPage)                                       //如果目前在第一页
        {
            lbtnFirst.Visible = false;
            lbtnPrev.Visible = false;
        }
        else
        {
            lbtnFirst.Visible = true;
            lbtnPrev.Visible = true;
        }
        if (pd.IsLastPage)                                        //如果目前在最后一页
        {
            lbtnLast.Visible = false;
            lbtnNext.Visible = false;
        }
        else
        {
            lbtnLast.Visible = true;
            lbtnNext.Visible = true;
        }

    }




    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        getData(1);
    }

    protected void lbtnPrev_Click(object sender, EventArgs e)
    {
        getData(Convert.ToInt32(lblCurrent.Text) - 1);
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        getData(Convert.ToInt32(lblCurrent.Text) + 1);
    }

    protected void lbtnLast_Click(object sender, EventArgs e)
    {
        getData(i);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;//获取按钮对象
        string Ware_id = btn.CommandArgument; //把该按钮的CommandArgument对应的值赋给Ware_id变量
        DataListItem item = (DataListItem)btn.NamingContainer;  //找到该按钮所在的容器即得到DataListItem对象
        int index = item.ItemIndex;                             //获取DataListItem对象的索引
        Label lblNum = (Label)DataList1.Items[index].FindControl("Label1");//获得DataList1.Items的Label1对象
        string addr = "~/Shop/shoppingCart2.aspx?wid={0}&wnum={1}";
        addr = string.Format(addr, Ware_id, lblNum.Text);       //把商品id和编码通过url传递到目标地址
        Response.Redirect(addr);

    }
}
