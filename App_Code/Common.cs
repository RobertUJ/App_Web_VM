using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Common
/// </summary>
public static class Common
{
    public static int BAR_CODE_LENGTH = 12;

    public static void Session_State()
    {

        if (System.Web.HttpContext.Current.Session["Store_ID"] == null)
        {
            System.Web.HttpContext.Current.Response.Write("<script language=\"javascript\">alert(\"Sesión Expirada\");</script>");
            System.Web.HttpContext.Current.Response.Redirect("/default.aspx");
        }

        wsConnect.wsConnect proxy = new wsConnect.wsConnect();

        string page = "";
        if (HttpContext.Current.Session["Page"] != null)
        {
            page = HttpContext.Current.Session["Page"].ToString();
        }
        else
        {
            System.Web.HttpContext.Current.Response.Redirect("/Access_Denied.aspx");
        }

        if (page != "/Main.aspx" && page != string.Empty)
        {
            String json = Json.Builder(String.Empty, "Store_ID", HttpContext.Current.Session["Store_ID"].ToString());
            json = Json.Builder(json, "Branch_ID", HttpContext.Current.Session["Branch_ID"].ToString());
            json = Json.Builder(json, "User_ID", HttpContext.Current.Session["User_ID"].ToString());
            json = Json.Builder(json, "Page", page);
            json = Json.Builder(json, "P", "s");
            json = Json.Builder(json, "Commit_User_ID", HttpContext.Current.Session["User_ID"].ToString());
            json = Json.Builder(json, "Commit_Location", HttpContext.Current.Session["Mac_Address"].ToString());

            if (proxy.Allow_Access(json) == "0")
            {
                Redirect("/Access_Denied.aspx");
            }

        }

    }

    public static void Alert(string message)
    {
        Telerik.Web.UI.RadAjaxManager ram = new Telerik.Web.UI.RadAjaxManager();
        string script_text = "radakert('" + message + "', 250, 150);";
        ram.ResponseScripts.Add(@script_text);
    }

    public static void Redirect(string URL)
    {
        System.Web.HttpContext.Current.Response.Redirect(URL);
    }

    public static string Pad(Int64 number, int length)
    {
        string result = number.ToString();
        for (int i = result.Length; i < length; i++)
        {
            result = "0" + result;
        }

        return result;
    }

    public static System.Boolean IsNumeric(System.Object Expression)
    {
        Boolean result = false;
        if (Expression == null || Expression is DateTime)
            return false;

        if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
            return true;

        try
        {
            if (Expression is string)
                Double.Parse(Expression as string);
            else
                Double.Parse(Expression.ToString());
            result = true;
        }
        catch (Exception e)
        {
            return false;
        }

        return result;
    }

    public static DataSet XML2DataSet(String xml)
    {
        StringReader sr = new StringReader(xml);
        DataSet ds = new DataSet();
        ds.ReadXml(sr);
        return ds;
    }

    public static DataTable XML2DataTable(String xml)
    {
        DataTable dt = new DataTable();
        StringReader sr = new StringReader(xml);
        DataSet ds = new DataSet();
        ds.ReadXml(sr);
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }
        return dt;
    }

    public static bool IsAdministrator()
    {
        return (System.Web.HttpContext.Current.Session["Group_Name"].ToString().Equals("Administradores"));
    }
}