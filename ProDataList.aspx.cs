using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ProDataList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SmartConnetionString"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("getdetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter spr;
            spr = cmd.Parameters.Add("@id", SqlDbType.Int);
            spr = cmd.Parameters.Add("@title", SqlDbType.NVarChar, 50);
            spr.Direction = ParameterDirection.Output;     //设置@title参数为输出参数
            spr = cmd.Parameters.Add("@count", SqlDbType.Int);
            spr.Direction = ParameterDirection.ReturnValue;//设置@count参数为返回值
            cmd.Parameters["@id"].Value = 6;               //为 @id 参数赋值为6           
            cmd.ExecuteNonQuery();
            Label1.Text = cmd.Parameters["@title"].Value.ToString();
            Label2.Text = cmd.Parameters["@count"].Value.ToString();
            con.Close();
        }
    }
}