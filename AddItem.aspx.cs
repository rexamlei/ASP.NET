using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_AddItem : System.Web.UI.Page
{
    SqlHelper sh = new SqlHelper();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLevel3();
            BindTM();
            ViewState["dt"] = dt;
            BindLX();
            DisplayColumnInfo();
        }
    }

    private void DisplayColumnInfo()
    {
        string sql2 = String.Format("select Column_Name,Data_Type from information_schema.Columns where Table_Name='{0}' and Column_Name not in ('IID','DTID')",ddlJDLX.SelectedValue.ToString());
        GridView2.DataSource = sh.QueryOperation(sql2);
        GridView2.DataBind();
    }

    private void BindTM()
    {
        dt.Columns.Clear();
        dt.Columns.Add(new DataColumn("ColumnID"));
        dt.Columns.Add(new DataColumn("ColumnName"));
        dt.Columns.Add(new DataColumn("ColumnType"));
        DataRow dr = dt.NewRow();
        dr["ColumnID"] = Guid.NewGuid().ToString();
        dt.Rows.Add(dr);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        Panel1.Visible = false;
    }

    private void BindLevel3()
    {
        DropDownList2.DataSource = sh.QueryOperation("select * from T_Level3");
        DropDownList2.DataTextField = "Level3_Name";
        DropDownList2.DataValueField = "Level3_ID";
        DropDownList2.DataBind();
    }
    //添加一行数据表
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        dt = (DataTable)ViewState["dt"];
        DataRow dr = dt.NewRow();
        dr["ColumnID"] = Guid.NewGuid().ToString();
        dt.Rows.Add(dr);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        dt = (DataTable)ViewState["dt"];
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("chkID");
            if (ck.Checked)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    //根据我们填写的内容，创建一个新的表！
    protected void BtnSaveInfo_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtTableName.Text) && !string.IsNullOrEmpty(TxtTypeName.Text))
        {
            sh.ExeNonQuery(string.Format("insert into T_DetailsType values('{0}','{1}','{2}')", TxtTableName.Text, TxtTypeName.Text,""));
            //创建详细类型表
            string createTableStr = "Create Table  " + TxtTableName.Text + "(IID int primary key identity(1,1),";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox tb = (TextBox)GridView1.Rows[i].Cells[2].FindControl("TextBox5");
                if (tb.Text!="")
                {
                    DropDownList ddl = (DropDownList)GridView1.Rows[i].Cells[3].FindControl("DropDownList4");
                    string StrType = tb.Text + " " + ddl.SelectedValue.ToString();
                    StrType = StrType + ",";
                    createTableStr = createTableStr + StrType;
                }
            }
            createTableStr = createTableStr + "DTID int)";
            sh.ExeNonQuery(createTableStr);
            string fk = string.Format("alter table {0} add constraint {1} foreign key(DTID) references T_DetailsType(DTID)", TxtTableName.Text, "fk" + Guid.NewGuid().ToString().Substring(0, 7));
            sh.ExeNonQuery(fk);
            BindLX();
        }
        else
        {
            Response.Write("<script>alert('对不起，请填写完整！')</script>");
        }
    }

    private void BindLX()
    {
        ddlJDLX.DataSource = sh.QueryOperation("select * from T_DetailsType");
        ddlJDLX.DataTextField = "DTName";
        ddlJDLX.DataBind();
    }
    //改变该下拉类表的内容时激发该事件
    protected void ddlJDLX_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayColumnInfo();
    }
    //显示新建表的内容
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Panel1.Visible = true;
    }
    //图片上传
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string fileExt = FileUpload1.FileName.ToString();
            if (fileExt.Contains(".jpg") || fileExt.Contains(".gif"))
            {
                string phyPath = Server.MapPath("~/TP");
                FileUpload1.SaveAs(phyPath + "//" + fileExt);
                Image1.ImageUrl = "~/TP/" + fileExt;
            }
            else
            {
                Label1.Text = "对不起，只能上传jpg和gif格式图片。";
            }
        }
    }
    //把所有的商品信息添加到数据库
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        int DTID = -1;
        int IID = -1;
        SqlTransaction trans;
        SqlConnection con = sh.returnConn();
        con.Open();
        trans = con.BeginTransaction();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = trans;
            cmd.Connection = con;
            string sql1 = string.Format("select DTID from T_DetailsType where DTName='{0}'", ddlJDLX.SelectedValue.ToString());
            cmd.CommandText = sql1;
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                DTID = sdr.GetInt32(0);
            }
            sdr.Close();
            string sql2 = string.Format("insert into {0} values(", ddlJDLX.SelectedValue.ToString());
            int count = GridView2.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                TextBox tb = (TextBox)GridView2.Rows[i].Cells[1].FindControl("TextBox6");
                string columnsType = GridView2.Rows[i].Cells[2].Text;
                if (columnsType.Equals("nvarchar"))
                {
                    sql2 = sql2 + "'" + tb.Text + "'";
                }
                else
                {
                    sql2 = sql2 + tb.Text;
                }
                sql2 = sql2 + ",";
            }
            sql2 = sql2 + DTID + ");select cast (scope_identity() as int )";
            cmd.CommandText = sql2;
            IID = (int)cmd.ExecuteScalar();
            string wNumber = TextBox1.Text;
            string wName = TextBox2.Text;
            string wWeight = DropDownList1.SelectedItem.ToString();
            string wStock = TextBox3.Text;
            string wLevel3 = DropDownList2.SelectedValue.ToString();
            string wPrice = TextBox4.Text;
            string extend_ID = IID.ToString();
            string wImage = Image1.ImageUrl;
            int DetailsType_ID = DTID;
            string sql3 = string.Format("insert into T_Ware values('{0}','{1}','{2}',{3},{4},{5},{6},'{7}',{8})", wNumber, wName, wWeight, wStock, wLevel3, wPrice, extend_ID, wImage, DetailsType_ID);
            cmd.CommandText = sql3;
            cmd.ExecuteNonQuery();
            trans.Commit();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('恭喜您添加成功！');window.location.href='AddItem.aspx';</script>");
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw;
        }
        finally
        {
            con.Close();
        }
    }
}
