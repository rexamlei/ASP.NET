using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class DataListPaging : System.Web.UI.Page
{
    static int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) //如果是第一次打开页面
        {
            getData(1);  //绑定第一页数据
        }
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
        lblCurrent.Text = Convert.ToString(pd.CurrentPageIndex+1);//显示当前在第几页
        lblCount.Text = "/ ";
        lblCount.Text+=pd.PageCount.ToString();                   //显示总页数
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
    /// <summary>
    /// 获取数据通过SqlDataAdapter对象填充到DataTable中
    /// </summary>
    /// <returns>DataTable</returns>
    private DataTable getData_stu() {
        string sql = "select * from stu";
        SqlConnection conn;
        SqlCommand cmd;
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SmartConnetionString"].ToString());
        cmd = new SqlCommand(sql, conn);
        conn.Open();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        conn.Close();
        return dt;
    }

    protected void lbtnFirst_Click(object sender, EventArgs e)//按第一页按钮
    {
        getData(1);
    }

    protected void lbtnPrev_Click(object sender, EventArgs e)//按上一页按钮
    {
        getData(Convert.ToInt32(lblCurrent.Text)-1);
    }

    protected void lbtnNext_Click(object sender, EventArgs e)//按下一页按钮
    {
        getData(Convert.ToInt32(lblCurrent.Text) + 1);
    }

    protected void lbtnLast_Click(object sender, EventArgs e)//按最后一页按钮
    {
        getData(i);
    }
}