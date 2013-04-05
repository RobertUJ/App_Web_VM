using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Get_Info : System.Web.UI.Page
{
    protected wsConnect.wsConnect proxy = new wsConnect.wsConnect();

    protected void Page_Load(object sender, EventArgs e)
    {
        string IP_Entry_State = System.Web.HttpContext.Current.Request.Form["IP_Entry_State"].ToString();
        string IP_Country_Plates = System.Web.HttpContext.Current.Request.Form["IP_Country_Plates"].ToString();

        try
        {
            String json = Json.Builder(String.Empty, "Table_Name", "Insurance_Products");
            json = Json.Builder(json, "Top", "DISTINCT");
            json = Json.Builder(json, "Field_ID", "IP_Code + '|' +  dbo.M2S(IP_Payment) + '|' + dbo.M2S(IP_Policy_Right)   AS IP_Code");
            json = Json.Builder(json, "Field_Name", "IP_Validity");
            json = Json.Builder(json, "Where", "WHERE IP_Entry_State = '" + IP_Entry_State + "' AND IP_Country_Plates = '" + IP_Country_Plates + "'");
            String xml = proxy.Get_Field(json);
            DataSet ds = Common.XML2DataSet(xml);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Messages = String.Empty;
                    int i = 0;
                    while (i < ds.Tables[0].Rows.Count)
                    {
                        Messages = Messages + "<option value=\"" + ds.Tables[0].Rows[i]["IP_Code"].ToString() + "\">" + ds.Tables[0].Rows[i]["IP_Validity"].ToString() + "</option>";
                        i++;
                    }

                    System.Web.HttpContext.Current.Response.Write(Messages);
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write(String.Empty);
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write(String.Empty);
            }
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write(ex.Message);
        }
    }
}