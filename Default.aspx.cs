using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using App_VM.App_Code;

public partial class _Default : System.Web.UI.Page
{
        protected NationalUnity.idCard NU_Card_ID = new NationalUnity.idCard();
        protected NationalUnity.Poliza NU_Policy = new NationalUnity.Poliza();
        protected NationalUnity.Service NU_Service = new NationalUnity.Service();
        ServiceTAE.Service wsProxy;
        protected wsConnect.wsConnect proxy = new wsConnect.wsConnect();

        double Sales_Amount = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //this.txtPhone.Attributes.Add("onmousedown", "return noCopyMouse(event);");
                //this.txtPhone.Attributes.Add("onkeydown", "return noCopyKey(event);");
                //this.txtConfirmation.Attributes.Add("onmousedown", "return noCopyMouse(event);");
                //this.txtConfirmation.Attributes.Add("onkeydown", "return noCopyKey(event);");
                //this.pnlTicket.Attributes.Add("onload", "return formPrint();");
                //this.pnlTicket_Service.Attributes.Add("onload", "return formPrint();");
                
                HidePannels();        
                Initialize_Report();
                Initialize_Insurance();
            }
        }

        protected void HidePannels()
        {
            this.pnlToken.Visible = false;
            this.pnlSales.Visible = false;
            this.pnlBalance.Visible = false;
            this.pnlReport.Visible = false;
            this.pnlServices.Visible = false;
            this.pnlTicket.Visible = false;
            this.pnlTicket_Service.Visible = false;
            this.pnlInsurance.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            wsProxy = new ServiceTAE.Service();
            string XML_Login = Utilities.XML_Builder("LOGIN", this.txtUser.Text, this.txtPassword.Text, "", "", "", "");           
            DataSet ds = wsProxy.wsLoginUser(XML_Login);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["Password"] = this.txtPassword.Text.Trim();
                    Session["User"] = ds.Tables[0].Rows[0]["Nombre"].ToString();
                    bool has_Token = bool.Parse(ds.Tables[0].Rows[0]["Activar_Token"].ToString());
                    Session["Multi_Brand"] = bool.Parse(ds.Tables[0].Rows[0]["Multimarca"].ToString()); 
                    string Client_Name = Capital_Case(ds.Tables[0].Rows[0]["Nombre"].ToString().ToLower());
                    this.lblNameToken.Text = Client_Name;
                    this.lblNameSale.Text = Client_Name;
                    this.lblNameBalance.Text = Client_Name;
                    this.lblNameReport.Text = Client_Name;
                    this.lblNameServices.Text = Client_Name;
                    this.lblTiket_Name.Text = Client_Name;
                    this.lblTicket_Service_Client.Text = Client_Name;
                    this.lblInsurance_User_Name.Text = Client_Name;
                    this.pnlLogin.Visible = false;
                    if (has_Token)                  
                    {
                        string XML_Codes = wsProxy.wsObtenerCombinacion();
        
                        this.pnlToken.Visible = true;
                        this.lblCode1.Text = Utilities.Get_XML_Value(XML_Codes, "Combinacion1");
                        this.lblCode2.Text = Utilities.Get_XML_Value(XML_Codes, "Combinacion2");  
                    }
                    else
                    {
                        Sales_Initialization();
                        Services_Initialization();
                    }

                    
                }
                else
                {
                    this.lblMessage.Text = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><img src=\"Images/error2.png\" height=\"24\" width=\"24\" alt=\"Favor de intentar de Nuevo!\" /></td><td>Usuario o contraseña inválido!<br />Favor de intentar de Nuevo.</td></tr></table>";
                }
            }
            else
            {
                this.lblMessage.Text = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><img src=\"Images/error2.png\" height=\"24\" width=\"24\" alt=\"Favor de intentar de Nuevo!\" /></td><td>>Usuario o contraseña inválido!<br />Favor de intentar de Nuevo.</td></tr></table>";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.txtUser.Text = String.Empty;
            this.txtPassword.Text = String.Empty;
            this.lblMessage.Text = String.Empty;
        }

        protected void btnSubmitToken_Click(object sender, EventArgs e)
        {
            wsProxy = new ServiceTAE.Service();
            string XML_Validate_Token = Utilities.Validate_Token(this.txtUser.Text, this.lblCode1.Text, this.lblCode2.Text, this.txtCode1.Text, this.txtCode2.Text);
            bool valid_Token = wsProxy.wsValidaToken(XML_Validate_Token);
            if (valid_Token)
            {
                Sales_Initialization();
                Services_Initialization();
            }
            else
            {
                this.lblMessageToken.Text = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><img src=\"Images/error2.png\" height=\"24\" width=\"24\" alt=\"Código Incorrecto! Favor de intentar de Nuevo.\" /></td><td>Favor de intentar de Nuevo!</td></tr></table>";
            }
        }

        protected void btnClearToken_Click(object sender, EventArgs e)
        {
            this.txtCode1.Text = String.Empty;
            this.txtCode2.Text = String.Empty;
        }

        protected void Sales_Initialization()
        {
            wsProxy = new ServiceTAE.Service();
            string Last_Carrier = String.Empty;
            string Carrier_Telcel = "01";    
       
            string XML_Carriers = Utilities.XML_Builder("OPERADORAS", this.txtUser.Text, this.txtPassword.Text, Carrier_Telcel, string.Empty, String.Empty, String.Empty);
            DataSet dsCarriers = wsProxy.wsOperadoras(XML_Carriers);
 
            this.ddlProduct.Items.Clear();
            ListItem liProduct = new ListItem("Seleccionar", "Seleccionar");
            this.ddlProduct.Items.Add(liProduct);

            if (dsCarriers.Tables.Count > 0)
            {
                if (dsCarriers.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    while (i < dsCarriers.Tables[0].Rows.Count)
                    {                        
                        if (dsCarriers.Tables[0].Rows[i]["Grupo"].ToString().Trim().Equals("TAE"))
                        {
                            if (bool.Parse(Session["Multi_Brand"].ToString()))
                            {
                                ListItem li = new ListItem(Capital_Case(dsCarriers.Tables[0].Rows[i]["Nombre"].ToString().Trim()), dsCarriers.Tables[0].Rows[i]["id_Operadora"].ToString().Trim());
                                this.ddlProduct.Items.Add(li);
                            }
                            else
                            {
                                if (!dsCarriers.Tables[0].Rows[i]["id_Operadora"].ToString().Trim().Equals(Carrier_Telcel))
                                {
                                    ListItem li = new ListItem(Capital_Case(dsCarriers.Tables[0].Rows[i]["Nombre"].ToString().Trim()), dsCarriers.Tables[0].Rows[i]["id_Operadora"].ToString().Trim());
                                    this.ddlProduct.Items.Add(li);
                                }
                            }
                        }            
                        
                        i++;
                    }

                }
            }


            string XML_Prices = Utilities.XML_Builder("PRECIOS", this.txtUser.Text, this.txtPassword.Text, Carrier_Telcel, string.Empty, String.Empty, String.Empty);
            DataSet ds = wsProxy.wsOperadorasPrecios(XML_Prices);            

            this.ddlAmounts.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    while (i < ds.Tables[0].Rows.Count)
                    {
                        if (!ds.Tables[0].Rows[i]["MONTO"].ToString().Trim().Equals("0"))
                        {
                            if (!ds.Tables[0].Rows[i]["id_Operadora"].ToString().Trim().Equals(Last_Carrier))
                            {
                                if (bool.Parse(Session["Multi_Brand"].ToString()))
                                {
                                    Last_Carrier = ds.Tables[0].Rows[i]["id_Operadora"].ToString();
                                    ListItem liCarrier = new ListItem(Last_Carrier, Last_Carrier);
                                    DropDownList ddl = new DropDownList();
                                    Session[Last_Carrier] = ddl;
                                }
                                else
                                {
                                    if (!ds.Tables[0].Rows[i]["id_Operadora"].ToString().Trim().Equals(Carrier_Telcel))
                                    {
                                        Last_Carrier = ds.Tables[0].Rows[i]["id_Operadora"].ToString();
                                        ListItem liCarrier = new ListItem(Last_Carrier, Last_Carrier);
                                        DropDownList ddl = new DropDownList();
                                        Session[Last_Carrier] = ddl;
                                    }
                                }
                            }

                            if (bool.Parse(Session["Multi_Brand"].ToString()))
                            {
                                ListItem li = new ListItem(ds.Tables[0].Rows[i]["MONTO"].ToString().Trim(), ds.Tables[0].Rows[i]["MONTO"].ToString().Trim());
                                ((DropDownList)Session[Last_Carrier]).Items.Add(li);
                            }
                            else
                            {
                                if (!ds.Tables[0].Rows[i]["id_Operadora"].ToString().Trim().Equals(Carrier_Telcel))
                                {
                                    ListItem li = new ListItem(ds.Tables[0].Rows[i]["MONTO"].ToString().Trim(), ds.Tables[0].Rows[i]["MONTO"].ToString().Trim());
                                    ((DropDownList)Session[Last_Carrier]).Items.Add(li);
                                }
                            }
                        }
                        i++;
                    }

                }
            }
           
            HidePannels();
            this.pnlSales.Visible = true;
            btmSaleClear_Click(this.btmSaleClear, new EventArgs());
            this.txtPhone.Focus();
        }

        protected void lnkBalance_Click(object sender, EventArgs e)
        {
            Get_Balance();
        }

        protected void Get_Balance()
        {
            HidePannels();
            this.pnlBalance.Visible = true;
            
            double balance = 0;
            wsProxy = new ServiceTAE.Service();
            string XML_Balance = Utilities.XML_Builder("SALDO", this.txtUser.Text, this.txtPassword.Text, String.Empty, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), String.Empty, Constants.PRODUCT_TYPE_TAE);
            DataSet ds = wsProxy.wsSaldoPuntoVenta(XML_Balance);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    balance = double.Parse(ds.Tables[0].Rows[0]["Saldo_Disponible"].ToString()) - double.Parse(ds.Tables[0].Rows[0]["Diferencial"].ToString());
                    this.lblBalance.Text = String.Format("{0:C}", balance);                    
                }
                else
                {
                    this.lblBalance.Text = String.Format("{0:C}", balance);  
                }
            }
            else
            {
                this.lblBalance.Text = String.Format("{0:C}", balance);  
            }
        }

        protected string Capital_Case(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
          
            return char.ToUpper(text[0]) + text.Substring(1); 
        }

        protected void btnSale_Click(object sender, EventArgs e)
        {
            if (this.txtPhone.Text == this.txtConfirmation.Text)
            {
                if (isNumber(this.txtPhone.Text))
                {
                    wsProxy = new ServiceTAE.Service();
                    string XML_Sales = Utilities.Sales_Transaction(this.txtUser.Text, Session["Password"].ToString(), String.Empty, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), this.txtPhone.Text, Constants.REGISTER_FOLIO, ddlProduct.SelectedValue, this.ddlAmounts.SelectedValue.ToString());
                    string XML_Response = wsProxy.wsTransaction(XML_Sales);
                    string Confirmation = Utilities.Get_XML_Value(XML_Response, "Confirmacion");
                    string Response_Folio = Utilities.Get_XML_Value(XML_Response, "Folio");
                    string Description = Utilities.Get_XML_Value(XML_Response, "Descripcion");
                    string Message = Utilities.Get_XML_Value(XML_Response, "Aviso");

                    if (Confirmation != "00")
                    {
                        this.lblSales_Message.Text = "<table border=\"0\" align=\"center\"><tr><td align=\"center\"><img src=\"Images/cellphone.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + this.txtPhone.Text + "</td></tr><td align=\"center\"><img src=\"Images/folio.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Response_Folio + "</td></tr><td><img src=\"Images/error.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#880000;\"><b>" + Confirmation + "</b> " + Description + "</td></tr><tr><td align=\"center\"><img src=\"Images/alert2.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Message + "</td></tr></table>";
                    }
                    else
                    {
                        this.lblSales_Message.Text = "<table border=\"0\" align=\"center\"><tr><td align=\"center\"><img src=\"Images/cellphone.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + this.txtPhone.Text + "</td></tr><td align=\"center\"><img src=\"Images/folio.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Response_Folio + "</td></tr><td><img src=\"Images/success.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#008800;\"><b>" + Confirmation + "</b> " + Description + "</td></tr><tr><td align=\"center\"><img src=\"Images/alert2.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Message + "</td></tr></table>";
                        this.lblTicket_Carrier.Text = ddlProduct.SelectedItem.Text;
                        this.lblTicket_Phone.Text = this.txtPhone.Text;
                        this.lblTicket_Folio.Text = Response_Folio;
                        this.lblTicket_Amount.Text = string.Format("${0:#,#.00}", double.Parse(this.ddlAmounts.SelectedValue.ToString()));
                        this.lblTicket_Date.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        this.pnlSales.Visible = false;
                        this.pnlTicket.Visible = true;
                    }
                }
                else
                {
                    this.lblSales_Message.Text = "<table border=\"0\" align=\"center\"><tr><td><img src=\"Images/error.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#880000;\"> Número incorrecto!</td></tr></table>";
                }
            }
            else
            {
                this.lblSales_Message.Text = "<table border=\"0\" align=\"center\"><tr><td><img src=\"Images/error.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#880000;\"> Números no son idénticos!</td></tr></table>";
            }
            this.txtPhone.Text = String.Empty;
            this.txtConfirmation.Text = String.Empty;
            
        }

        protected void btmSaleClear_Click(object sender, EventArgs e)
        {
            this.txtPhone.Text = String.Empty;
            this.txtConfirmation.Text = String.Empty;
            this.lblSales_Message.Text = String.Empty;
        }

        protected void lnkSales_Report_Click(object sender, EventArgs e)
        {
            HidePannels();
            this.pnlReport.Visible = true;
        }

        protected void lnkInsurance_Click(object sender, EventArgs e)
        {
            HidePannels();
            this.pnlInsurance.Visible = true;
        }

        protected void Sales_Report()
        {           
            wsProxy = new ServiceTAE.Service();
            string XML_Sales_Report = Utilities.Sales_Report(this.txtUser.Text, this.txtPassword.Text, this.ddlReport_Dates.SelectedValue);
            DataSet ds = wsProxy.wsVentadelDia(XML_Sales_Report);
            this.gvReport.DataSource = ds;
            this.gvReport.DataBind();
            
        }

        protected void lnkReport_Sales_Click(object sender, EventArgs e)
        {
            HidePannels();
            this.pnlSales.Visible = true;
            btmSaleClear_Click(this.btmSaleClear, new EventArgs());
            this.txtPhone.Focus();
        }

        protected bool isNumber(string number)
        {
            bool result = true;
            Int64 i = 0;
            try
            {
                i = Int64.Parse(number);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        protected bool isDouble(string number)
        {
            bool result = true;
            Double i = 0;
            try
            {
                i = Double.Parse(number);
            }
            catch
            {
                result = false;
            }
            return result;
        }


        protected void gvReport_RowDataBound(Object sender, GridViewRowEventArgs e)
        {            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Carrier = e.Row.Cells[4].Text.Trim();
                if (!bool.Parse(Session["Multi_Brand"].ToString()))
                {
                    e.Row.Visible = (!Carrier.Equals("TELCEL"));
                    if (!Carrier.Equals("TELCEL"))
                    {
                        Sales_Amount = Sales_Amount + double.Parse(e.Row.Cells[3].Text);
                        e.Row.Cells[4].Text = Capital_Case(e.Row.Cells[4].Text.ToLower());
                    }
                }
                else
                {
                    Sales_Amount = Sales_Amount + double.Parse(e.Row.Cells[3].Text);
                    e.Row.Cells[4].Text = Capital_Case(e.Row.Cells[4].Text.ToLower());
                }

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "<font color=\"#FFFFFF\">Total:</font>";
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = "<font color=\"#FFFFFF\">$" + String.Format("{0:#,#}", Sales_Amount) + "</font>";
            }
        } 

        protected void lnkSales_Services_Click(object sender, EventArgs e)
        {
           
            HidePannels();
            this.pnlServices.Visible = true;

        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedIndex > 0)
            {
                ddlAmounts.Items.Clear();
                foreach (ListItem li in ((DropDownList)Session[ddlProduct.SelectedValue]).Items)
                {
                    ddlAmounts.Items.Add(li);
                }
            }
        }
        protected void Services_Initialization()
        {
            wsProxy = new ServiceTAE.Service();
            string Last_Carrier = String.Empty;
            string XML_Carriers = Utilities.XML_Builder("OPERADORAS", this.txtUser.Text, this.txtPassword.Text, String.Empty, String.Empty, String.Empty, String.Empty);
            DataSet ds = wsProxy.wsOperadoras(XML_Carriers);
            this.ddlServices.Items.Clear();
           
            
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    while (i < ds.Tables[0].Rows.Count)
                    {

                        if (ds.Tables[0].Rows[i]["Grupo"].ToString().Trim().Equals("SERVICIO"))
                        {
                            ListItem li = new ListItem(ds.Tables[0].Rows[i]["Nombre"].ToString().Trim(), ds.Tables[0].Rows[i]["id_Operadora"].ToString().Trim());
                            this.ddlServices.Items.Add(li);
                        }
                        
                        i++;
                    }

                }
            } 
           
            this.btmSaleClear_Click(this.btnService_Clear, new EventArgs());
         
              
        }
        protected void btnService_Clear_Click(object sender, EventArgs e)
        {
            this.txtReference.Text = String.Empty;
            this.txtServiceAmount.Text = String.Empty;
            this.lblServiceMessage.Text = String.Empty;
        }
        protected void btnService_Click(object sender, EventArgs e)
        {
            if (isDouble(this.txtServiceAmount.Text))
            {
                wsProxy = new ServiceTAE.Service();
                string XML_Sales = Utilities.Sales_Transaction(this.txtUser.Text, Session["Password"].ToString(), String.Empty, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), this.txtReference.Text, Constants.REGISTER_FOLIO, this.ddlServices.SelectedValue, this.txtServiceAmount.Text);
                string XML_Response = wsProxy.wsTransaction(XML_Sales);
                string Confirmation = Utilities.Get_XML_Value(XML_Response, "Confirmacion");
                string Folio = Utilities.Get_XML_Value(XML_Response, "Folio");
                string Description = Utilities.Get_XML_Value(XML_Response, "Descripcion");
                string Message = Utilities.Get_XML_Value(XML_Response, "Aviso");
                string Reference = this.txtReference.Text;
                if (Reference.Length > 15)
                {
                    Reference = Reference.Substring(1, 15);
                }

                if (Confirmation != "00")
                {
                    this.lblServiceMessage.Text = "<table border=\"0\" align=\"center\"><tr><td align=\"center\"><img src=\"Images/service.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Reference + "...</td></tr><td align=\"center\"><img src=\"Images/folio.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Folio + "</td></tr><td><img src=\"Images/error.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#880000;\"><b>" + Confirmation + "</b> " + Description + "</td></tr><tr><td align=\"center\"><img src=\"Images/alert2.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Message + "</td></tr></table>";
                }
                else
                {
                    this.lblServiceMessage.Text = "<table border=\"0\" align=\"center\"><tr><td align=\"center\"><img src=\"Images/service.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Reference + "...</td></tr><td align=\"center\"><img src=\"Images/folio.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Folio + "</td></tr><td><img src=\"Images/success.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#008800;\"><b>" + Confirmation + "</b> " + Description + "</td></tr><tr><td align=\"center\"><img src=\"Images/alert2.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#000000;\">" + Message + "</td></tr></table>";
                    this.lblTicket_Service_Name.Text = this.ddlServices.SelectedItem.Text;
                    this.lblTicket_Service_Reference.Text = this.txtReference.Text;
                    this.lblTicket_Service_Folio.Text = Folio;
                    this.lblTicket_Service_Amount.Text = string.Format("${0:#,#.00}", double.Parse(this.txtServiceAmount.Text));
                    this.lblTicket_Service_Date.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    this.pnlServices.Visible = false;
                    this.pnlTicket_Service.Visible = true;
                }
            }
            else
            {
                this.lblServiceMessage.Text = "<table border=\"0\" align=\"center\"><tr><td><img src=\"Images/error.png\" width=\"24\" height=\"24\"/></td><td style=\"color:#880000;\"> Monto incorrecto!</td></tr></table>";
            }
        }

        protected void btnExitSales_Click(object sender, ImageClickEventArgs e)
        {
            Session["Password"] = String.Empty;
            btnClear_Click(this.btnClear, new EventArgs());
            HidePannels();
            this.pnlLogin.Visible = true;
        }

        protected void Img_Close_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlTicket.Visible = false;
            this.pnlSales.Visible = true;
        }
        protected void ImgTicket_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSales.Visible = false;
            this.pnlTicket.Visible = true;
        }
        protected void imbTicket_Service_Close_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlTicket_Service.Visible = false;
            this.pnlServices.Visible = true;
        }
        protected void imbService_Ticket_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlTicket_Service.Visible = true;
            this.pnlServices.Visible = false;
        }

        protected void Initialize_Report()
        {
            this.ddlReport_Dates.Items.Clear();
            for (int i = 0; i > -8; i--)
            {
                string date = DateTime.Now.AddDays(double.Parse(i.ToString())).ToString("dd/MM/yyyy");
                
                ListItem li = new ListItem(date, date);
                this.ddlReport_Dates.Items.Add(li);
            }
            
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Sales_Report();
        }

        protected void UpdateTimer_Tick2(object sender, EventArgs e)
        {
            this.lblTimer.Text = DateTime.Now.ToLongTimeString();
        }

        protected void Initialize_Insurance()
        {
            this.rdpEffective_Date.SelectedDate = DateTime.Now;
            String json = Json.Builder(String.Empty, "Table_Name", "Insurance_States_US");
            json = Json.Builder(json, "Top", "");
            json = Json.Builder(json, "Field_ID", "ISUS_ID");
            json = Json.Builder(json, "Field_Name", "ISUS_Code + '|' + ISUS_Name AS ISUS_Code");
            json = Json.Builder(json, "Where", "ORDER BY ISUS_Code, ISUS_Name");
            String xml = proxy.Get_Field(json);
            this.ddlEntrance_State.DataSource = Common.XML2DataTable(xml);
            this.ddlEntrance_State.DataTextField = "ISUS_Code";
            this.ddlEntrance_State.DataValueField = "ISUS_ID";
            this.ddlEntrance_State.DataBind();
            this.ddlEntrance_State.SelectedValue = "2";

            json = Json.Builder(String.Empty, "Table_Name", "Insurance_Products");
            json = Json.Builder(json, "Top", "DISTINCT");
            json = Json.Builder(json, "Field_ID", "IP_Country_Plates");
            json = Json.Builder(json, "Field_Name", "IP_Country_Plates");
            json = Json.Builder(json, "Where", "");
            xml = proxy.Get_Field(json);
            this.ddlCountry_Plates.DataSource = Common.XML2DataTable(xml);
            this.ddlCountry_Plates.DataTextField = "IP_Country_Plates";
            this.ddlCountry_Plates.DataValueField = "IP_Country_Plates";
            this.ddlCountry_Plates.DataBind();
            this.ddlCountry_Plates.SelectedValue = "MX";

            json = Json.Builder(String.Empty, "Table_Name", "Insurance_States_MX");
            json = Json.Builder(json, "Top", "DISTINCT");
            json = Json.Builder(json, "Field_ID", "ISMX_Code");
            json = Json.Builder(json, "Field_Name", "ISMX_Name");
            json = Json.Builder(json, "Where", "");
            xml = proxy.Get_Field(json);
            this.ddlInsurance_State.DataSource = Common.XML2DataTable(xml);
            this.ddlInsurance_State.DataTextField = "ISMX_Name";
            this.ddlInsurance_State.DataValueField = "ISMX_Code";
            this.ddlInsurance_State.DataBind();
            this.ddlInsurance_State.SelectedValue = "CH";


            json = Json.Builder(String.Empty, "Table_Name", "Insurance_Products");
            json = Json.Builder(json, "Top", "DISTINCT");
            json = Json.Builder(json, "Field_ID", "IP_Code + '|' +  dbo.M2S(IP_Payment) + '|' + dbo.M2S(IP_Policy_Right)   AS IP_Code");
            json = Json.Builder(json, "Field_Name", "IP_Validity");
            json = Json.Builder(json, "Where", "WHERE IP_Entry_State = 'TX' AND IP_Country_Plates = 'MX'");
            xml = proxy.Get_Field(json);
            this.ddlProduct_Days.DataSource = Common.XML2DataSet(xml);
            this.ddlProduct_Days.DataTextField = "IP_Validity";
            this.ddlProduct_Days.DataValueField = "IP_Code";
            this.ddlProduct_Days.DataBind();
            string[] Product_Days = this.ddlProduct_Days.Items[this.ddlProduct_Days.SelectedIndex].Value.Split('|');
            double Insurance_Price = double.Parse(Product_Days[1]) + double.Parse(Product_Days[2]);
            lblTotal_Insurance.InnerText = String.Format("{0:C2}", Insurance_Price);

            xml = proxy.Get_Insurance_Settings();
            DataSet ds = Common.XML2DataSet(xml);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.lblInsurance_Is_Beta.Text = ds.Tables[0].Rows[0]["IS_Beta"].ToString();
                    this.lblInsurance_User.Text = ds.Tables[0].Rows[0]["IS_User"].ToString();
                    this.lblInsurance_Password.Text = ds.Tables[0].Rows[0]["IS_Password"].ToString();
                    this.lblInsurance_PreFix.Text = ds.Tables[0].Rows[0]["IS_PreFix"].ToString();
                    this.lblInsurance_Folio.Text = ds.Tables[0].Rows[0]["IS_Folio"].ToString();
                }
            }

            for (int i = DateTime.Now.Year; i > 1980; i--)
            {
                this.ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            json = Json.Builder(String.Empty, "IB_ID", "null");
            json = Json.Builder(json, "IB_Name", String.Empty);
            json = Json.Builder(json, "P", "s");
            json = Json.Builder(json, "Commit_User_ID", "0");
            json = Json.Builder(json, "Commit_Location", String.Empty);
            xml = proxy.Insurance_Brands(json);
            this.ddlBrand.DataSource = Common.XML2DataTable(xml);
            this.ddlBrand.DataValueField = "IB_ID";
            this.ddlBrand.DataTextField = "IB_Name";
            this.ddlBrand.DataBind();
            //this.ddlBrand.Items.Insert(0, new ListItem("Seleccionar", "0"));

        }

        protected void btnInsurance_Submit_Click(object sender, EventArgs e)
        {
            Process_Insurance();
        }


        protected void Process_Insurance()
        {
            NU_Card_ID.UserName = this.lblInsurance_User.Text;
            NU_Card_ID.Password = this.lblInsurance_Password.Text;
            NU_Card_ID.isBeta = bool.Parse(this.lblInsurance_Is_Beta.Text);
            //Fill only once
            //DataTable dt = NU_Service.wsGetTarifaAuto(NU_Card_ID);//pRDUCTO, tipo emision y vigencia
            //proxy.Fill_Insurance_Products(dt);

            DateTime Entry_Date = DateTime.Now;
            try
            {
                Entry_Date = DateTime.Parse(rdpEffective_Date.SelectedDate.Value.ToShortDateString() + ' ' + rdpEffective_Date.SelectedDate.Value.ToShortTimeString());
            }
            catch (Exception e)
            {

            }

            DateTime Experation_Date = DateTime.Now;
            try
            {
                Experation_Date = Entry_Date.AddDays(double.Parse(this.ddlProduct_Days.Items[this.ddlProduct_Days.SelectedIndex].Text));
            }
            catch (Exception e)
            {
            }
            string[] Insurance_Product = this.ddlProduct_Days.Items[this.ddlProduct_Days.SelectedIndex].Value.Split('|');
            string[] Insurance_State = this.ddlEntrance_State.Items[this.ddlEntrance_State.SelectedIndex].Value.Split('|');
            NU_Policy.Prefijo = this.lblInsurance_PreFix.Text;//Asignar por NU
            

            NU_Policy.MotivoVisita = "1"; //Visit Motives
            NU_Policy.TipoEndoso = "1"; //  Emision de Poliza -> Tipo de Poliza
            NU_Policy.FechaRegistro = DateTime.Now;
            NU_Policy.InicioVigencia = Entry_Date;
            NU_Policy.FinVigencia = Experation_Date;
            NU_Policy.FormaPago = "CONTADO"; //Fijo
            NU_Policy.Moneda = "NACIONAL"; //Fijo
            NU_Policy.NombreAsegurado = this.txtClient_Name.Text;
            NU_Policy.MXCalleNumero = this.txtClient_Address_Street.Text + ' ' + this.txtClient_Address_Street_Number.Text;
            NU_Policy.MXColonia = this.txtClient_Address.Text;
            NU_Policy.MXCodigoPostal = this.txtClient_Zip.Text;
            NU_Policy.MXMunicipio = this.txtInsurance_City.Text; //Necesita catalogo en la forma
            NU_Policy.MXEstadoNom = this.ddlInsurance_State.SelectedItem.Value; //Catalogo en la forma
            NU_Policy.MXLada = this.txtClient_Phone.Text.Substring(0, 3);
            NU_Policy.MXTelefonoNo = this.txtClient_Phone.Text;
            NU_Policy.Anio = this.ddlYear.SelectedValue;
            NU_Policy.Serie = this.txtVIN.Text;
            NU_Policy.Placas = this.txtPlates.Text;
            NU_Policy.Marca = this.ddlBrand.SelectedValue;
            NU_Policy.Modelo = this.txtModel.Text;
            NU_Policy.EstadoPlacas = this.ddlInsurance_State.SelectedValue; //Remplazar por ddl
            NU_Policy.EstadoEntrada = Insurance_State[0]; //States Catalog
            NU_Policy.PuertoEntrada = ddlEntrance_State.SelectedValue; //Catalogo 5
            NU_Policy.Prima = Insurance_Product[1]; //NU mandara 
            NU_Policy.Derecho = Insurance_Product[2]; //NU
            NU_Policy.Asistencia = "0";//NU
            NU_Policy.Extraprima = "3"; //Catalogo 10 // Preguntar como sacar el valor
            NU_Policy.Concepto_Extraprima = "1"; //Catalogo 6
            NU_Policy.FolioCertificado = this.lblInsurance_PreFix.Text + "-" + this.lblInsurance_Folio.Text; //Prefijo + folio Pendiente Nu x mandar
            NU_Policy.NombreConductor = this.txtDriver_Name.Text;
            NU_Policy.NoLicencia = this.txtDrivers_Licence_Number.Text;
            NU_Policy.Ocupacion = this.txtJob_Position.Text;
            NU_Policy.FechaNacimiento =String.Format("{0:M/d/yyyy}",this.rdpBirth_Date.SelectedDate.Value);
            NU_Policy.Email = this.txtClient_Email.Text;


            NU_Policy.Producto = Insurance_Product[0]; //Catalogo 8 
            string Insurance_Date = rdpEffective_Date.SelectedDate.Value.ToShortDateString() + ' ' + rdpEffective_Date.SelectedDate.Value.ToShortTimeString();
            DataSet ds = Process_Insurance_Folio("null", "false", NU_Policy.Producto, NU_Policy.NombreAsegurado, this.txtClient_Address_Street.Text,
                                    this.txtClient_Address_Street_Number.Text, NU_Policy.MXColonia, NU_Policy.MXCodigoPostal, NU_Policy.MXMunicipio,
                                    NU_Policy.MXEstadoNom, NU_Policy.MXLada, NU_Policy.MXTelefonoNo, NU_Policy.Email, NU_Policy.Ocupacion, this.rdpBirth_Date.SelectedDate.ToString(),
                                    NU_Policy.NombreConductor, NU_Policy.NoLicencia, NU_Policy.Serie, NU_Policy.Placas, NU_Policy.Modelo, NU_Policy.Marca,
                                    NU_Policy.EstadoPlacas, NU_Policy.EstadoEntrada, NU_Policy.PuertoEntrada, NU_Policy.Prima, NU_Policy.Derecho,
                                    NU_Policy.Asistencia, NU_Policy.Extraprima, Insurance_Date, this.txtUser.Text, DateTime.Now.ToShortDateString() , "0", "", "i", "1", "");

            string Insurance_Folio = "1";

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Insurance_Folio = ds.Tables[0].Rows[0]["IF_ID"].ToString();
                }
            }


            NU_Policy.Folio = int.Parse(Insurance_Folio); //*Calculo por tienda hasta 10 caracteres punto de venta 

            NationalUnity.AutoResponse NU_Auto_Response = NU_Service.WsData(NU_Card_ID, NU_Policy);
            int folio = NU_Auto_Response.Folio;
            String Status = NU_Auto_Response.Status;

            Process_Insurance_Folio(Insurance_Folio, Status.Equals("Ready").ToString(), NU_Policy.Producto, NU_Policy.NombreAsegurado, this.txtClient_Address_Street.Text,
                                   this.txtClient_Address_Street_Number.Text, NU_Policy.MXColonia, NU_Policy.MXCodigoPostal, NU_Policy.MXMunicipio,
                                   NU_Policy.MXEstadoNom, NU_Policy.MXLada, NU_Policy.MXTelefonoNo, NU_Policy.Email, NU_Policy.Ocupacion, this.rdpBirth_Date.SelectedDate.ToString(),
                                   NU_Policy.NombreConductor, NU_Policy.NoLicencia, NU_Policy.Serie, NU_Policy.Placas, NU_Policy.Modelo, NU_Policy.Marca,
                                   NU_Policy.EstadoPlacas, NU_Policy.EstadoEntrada, NU_Policy.PuertoEntrada, NU_Policy.Prima, NU_Policy.Derecho,
                                   NU_Policy.Asistencia, NU_Policy.Extraprima, Insurance_Date, this.txtUser.Text, DateTime.Now.ToShortDateString(), folio.ToString(), Status,  "u", "1", "");

            


            if (!Status.Equals("Ready"))
            {
                this.lblInsurance_Status.Text = Status;
            }
            else
            {

                //************** INSERTAR EN BASE DE DATOS

                StringBuilder sb = new StringBuilder();

                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"180\">");
                sb.Append("<tr>");
                sb.Append("<th class=\"ticket\">");
                sb.Append("Liability Insurance Identification Card");
                sb.Append("</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th style=\"border-bottom: 1px solid #777777\">");
                sb.Append("Tarjeta de Identificación Vehicular");
                sb.Append("</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<b>National Unity Insurance Company</b><br />");
                sb.Append("One Huebner Park<br />");
                sb.Append("15303 Huebner Road<br />");
                sb.Append("San Antonio, Tx 78248");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<img src=\"images/NationalUnity.png\" width=\"68\" height=\"45\" alt=\"National Unity\" />");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Agent Number:");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(this.txtUser.Text);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">No Agente:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Insured:");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(this.txtClient_Name.Text);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Asegurado:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Policy Number:");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append("<b>" + Common.Pad(folio, 6) + "</b>");//Preguntar cual es: Policy Number
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Número de Póliza:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Effective Date:");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(rdpEffective_Date.SelectedDate.Value.ToShortDateString() + ' ' + rdpEffective_Date.SelectedDate.Value.ToShortTimeString());
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Fecha Efectiva:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Expiration Date:");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(Experation_Date.ToShortDateString() + ' ' + Experation_Date.ToShortTimeString());
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Fecha Expiration:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Phone:");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(this.txtClient_Phone.Text);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Telefono:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Email:");
                sb.Append("</td>");
                sb.Append("<td align=\"center\">");
                sb.Append(this.txtClient_Email.Text);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\" align=\"center\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<th>");
                sb.Append("Vehicle Information");
                sb.Append("</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>Informaci&oacute;n del Veh&iacute;culo</th>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Year:");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(this.ddlYear.SelectedValue);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Año:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Make: </td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(this.ddlBrand.SelectedItem.Text);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Marca: </td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("Model: </td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(this.txtModel.Text);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Modelo:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777;\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("VIN: </td>");
                sb.Append("<td rowspan=\"2\" align=\"center\">");
                sb.Append(this.txtVIN.Text);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-right: 1px dotted #777777;\">Número de serie:</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777\" align=\"center\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<th>");
                sb.Append("Coverage");
                sb.Append("</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>Covertura</th>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px solid #777777;\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\">Body Injury:</td>");
                sb.Append("<td>");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td>$30,000.00 USD per person</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>$60,000.00 USD each accident</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\">Daño Corporal:</td>");
                sb.Append("<td>");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td>$30,000.00 USD por persona</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>$60,000.00 USD cada accidente</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\">Property Damage:</td>");
                sb.Append("<td>");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td>$25,000.00 USD each accident</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\">Daño de la propiedad:</td>");
                sb.Append("<td>");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td>$25,000.00 USD cada accidente</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\">Medical Payments:</td>");
                sb.Append("<td>");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td>$2,000.00 USD per person</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>$10,000.00 USD each accident</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\">Pagos M&eacute;dicos:</td>");
                sb.Append("<td>");
                sb.Append("<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\">");
                sb.Append("<tr>");
                sb.Append("<td>$2,000.00 USD por persona</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>$10,000.00 USD cada accidente</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border-bottom: 1px dotted #777777; border-right: 1px dotted #777777;\">");
                sb.Append("This policy provides Coverage that meets the limits required by law. Please call to verify ");
                sb.Append("if the policy is valid and enforce at 1-800-554-3498. For claims call: 1-800-554-3498  or (210) 479-8886.");
                sb.Append("This card must be carried in the insured vehicle at all time as evidence of insurance.");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td align=\"center\">");
                sb.Append("Esta póliza provee cobertura que satisface los limites requeridos por la ley. Por favor llame al");
                sb.Append("1-800-554-3498 para verificar que sea una póliza valida. Para reclamaciones llamar");
                sb.Append("al 1-800-554-3498. o (210) 479-8886. Esta tarjeta debe ser portada en el vehículo asegurado todo el tiempo");
                sb.Append("como evidencia del seguro.");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td align=\"center\">");
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"2\" width=\"100%\">");
                sb.Append("<tr>");
                sb.Append("<th style=\"border-bottom: 1px solid #777777; height:50px;\" >");
                sb.Append("&nbsp;");
                sb.Append("</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>Signature/Firma</th>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                Session["Insurance_Ticket"] = sb.ToString();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Insurance_Ticket", "window.open('Insurance_Ticket.aspx','','');Clear_Insurance();", true);

            }

           
        }

        protected DataSet Process_Insurance_Folio(string IF_ID, string IF_Successfull, string IF_Product, string IF_Client_Name, string IF_Client_Address_Street, string IF_Client_Address_Number, string IF_Client_Address, string IF_Client_Zip_Code, string IF_Client_City, string IF_Client_State, string IF_Client_TON, string IF_Client_Phone, string IF_Client_Email, string IF_Client_Job_Position, string IF_Client_Birth_Date, string IF_Driver_Name, string IF_Driver_Licence, string IF_Vehicle_VIN, string IF_Vehicle_Plates, string IF_Vehicle_Model, string IF_Vehicle_Brand, string IF_Vehicle_Plates_State, string IF_Entry_State, string IF_Entry_Port, string IF_Payment, string IF_Rights, string IF_Assistance, string IF_Extra_Payment, string IF_Date, string IF_Sales_Point, string IF_Registry_Date, string IF_Confirmation_Folio, string IF_Confirmation_Description, string P, string Commit_User_ID, string Commit_Location)
        {
            String json = Json.Builder(String.Empty, "IF_ID", IF_ID);
            json = Json.Builder(json, "IF_Successfull", IF_Successfull);
            json = Json.Builder(json, "IF_Product", IF_Product);
            json = Json.Builder(json, "IF_Client_Name", IF_Client_Name);
            json = Json.Builder(json, "IF_Client_Address_Street", IF_Client_Address_Street);
            json = Json.Builder(json, "IF_Client_Address_Number", IF_Client_Address_Number);
            json = Json.Builder(json, "IF_Client_Address", IF_Client_Address);
            json = Json.Builder(json, "IF_Client_Zip_Code", IF_Client_Zip_Code);
            json = Json.Builder(json, "IF_Client_City", IF_Client_City);
            json = Json.Builder(json, "IF_Client_State", IF_Client_State);
            json = Json.Builder(json, "IF_Client_TON", IF_Client_TON);
            json = Json.Builder(json, "IF_Client_Phone", IF_Client_Phone);
            json = Json.Builder(json, "IF_Client_Email", IF_Client_Email);
            json = Json.Builder(json, "IF_Client_Job_Position", IF_Client_Job_Position);
            json = Json.Builder(json, "IF_Client_Birth_Date", IF_Client_Birth_Date);
            json = Json.Builder(json, "IF_Driver_Name", IF_Driver_Name);
            json = Json.Builder(json, "IF_Driver_Licence", IF_Driver_Licence);
            json = Json.Builder(json, "IF_Vehicle_VIN", IF_Vehicle_VIN);
            json = Json.Builder(json, "IF_Vehicle_Plates", IF_Vehicle_Plates);
            json = Json.Builder(json, "IF_Vehicle_Model", IF_Vehicle_Model);
            json = Json.Builder(json, "IF_Vehicle_Brand", IF_Vehicle_Brand);
            json = Json.Builder(json, "IF_Vehicle_Plates_State", IF_Vehicle_Plates_State);
            json = Json.Builder(json, "IF_Entry_State", IF_Entry_State);
            json = Json.Builder(json, "IF_Entry_Port", IF_Entry_Port);
            json = Json.Builder(json, "IF_Payment", IF_Payment);
            json = Json.Builder(json, "IF_Rights", IF_Rights);
            json = Json.Builder(json, "IF_Assistance", IF_Assistance);
            json = Json.Builder(json, "IF_Extra_Payment", IF_Extra_Payment);
            json = Json.Builder(json, "IF_Date", IF_Date);
            json = Json.Builder(json, "IF_Sales_Point", IF_Sales_Point);
            json = Json.Builder(json, "IF_Registry_Date", IF_Registry_Date);
            json = Json.Builder(json, "IF_Confirmation_Folio", IF_Confirmation_Folio);
            json = Json.Builder(json, "IF_Confirmation_Description", IF_Confirmation_Description);
            json = Json.Builder(json, "P", P);
            json = Json.Builder(json, "Commit_User_ID", Commit_User_ID);
            json = Json.Builder(json, "Commit_Location", Commit_Location);
            String xml = proxy.Insurance_Folios(json);
            DataSet ds = Common.XML2DataSet(xml);
            return ds;
        }
 

}