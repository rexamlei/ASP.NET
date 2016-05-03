using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// SqlHelper 的摘要说明
/// </summary>
public class SqlHelper
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SmartConnectionString"].ToString());
    SqlDataReader sdr;


    public SqlHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public SqlConnection getConn() { return con; }
    public SqlConnection returnConn() { return con; }
    public SqlDataReader QueryOperation(String StrQueryCommand)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = StrQueryCommand;
        if (sdr != null)
            sdr.Close();
        sdr = cmd.ExecuteReader();
        return sdr;
    }
    public bool ExeNonQuery(String StrCmd)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = StrCmd;
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
            return false;
        }
        return true;
    }
    public void closeConn() {
        if (con.State == ConnectionState.Closed)
            con.Close();
    }
    public bool ExeNonQueryProc(String cmdName,SqlParameter[] ps)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = cmdName;
        cmd.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter p in ps)
        {
            cmd.Parameters.Add(p);
        }
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
            return false;
        }
        return true;
    }

    public DataTable QueryOperationProc(String cmdName, SqlParameter[] ps)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = cmdName;
        cmd.CommandType = CommandType.StoredProcedure;
        if (ps!=null)
        {
            foreach (SqlParameter p in ps)
            {
                cmd.Parameters.Add(p);
            }
        }
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        con.Close();
        return dt;
    }

    public DataTable QueryProc(String cmdStr, SqlParameter[] ps)
    {
        DataTable dt = new DataTable();
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = cmdStr;
        cmd.CommandType = CommandType.StoredProcedure;
        if (ps != null)
        {
            foreach (SqlParameter p in ps)
            {
                cmd.Parameters.Add(p);
            }
        }
       
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        con.Close();
        return dt;
    }
}