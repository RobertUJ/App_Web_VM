<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Ventamovil</title>
     <link href="css/default.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="js/default.js" type="text/javascript" language="javascript"></script> 
   
              
</head>
<body background="Images/sales.jpg">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" /> 
      

    <asp:Panel ID="pnlLogin" runat="server" BackImageUrl="~/Images/login.jpg" Width="375" Height="320"   >
    <table border="0" cellpadding="4" cellspacing="0" width="85%" align="center">
     <tr>
        <td colspan="2" align="center">
            <img src="images/transparent.png" width="1" height="120" />
        </td> 
    </tr>
    <tr>
        <td colspan="2"><b>Acceso:</b></td>
    </tr>
    <tr>
        <td>Usuario:</td>
        <td><asp:TextBox ID="txtUser" runat="server" MaxLength="15"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Contraseña:</td>
        <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="15"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2" align="center">
          <asp:Button ID="btnSubmit" Text="Accesar" runat="server" onclick="btnSubmit_Click" /> 
          <asp:Button ID="btnClear" runat="server" Text="Limpiar" onclick="btnClear_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <img src="images/transparent.png" width="1" height="15" />
        </td> 
    </tr>
    <tr>
       <td colspan="2">
           <asp:Label ID="lblMessage" runat="server"></asp:Label>
       </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
                <h3>&nbsp;</h3>
        </td> 
    </tr>

    </table>
    </asp:Panel>
    <asp:Panel ID="pnlToken" runat="server" BackImageUrl="~/Images/login.jpg" Width="375" Height="320" >

    <table border="0" cellpadding="4" cellspacing="0" width="85%" align="center">
    <tr>
        <td colspan="2" align="center">
            <img src="images/transparent.png" width="1" height="120" />
        </td> 
    </tr>
    <tr>
        <td colspan="2" align="center" valign="bottom">
               <b>Bienvenid@:  </b><asp:Label ID="lblNameToken" runat="server"></asp:Label>
        </td> 
    </tr>
    <tr>
        <td><asp:Label ID="lblCode1" runat="server"></asp:Label> </td>
        <td><asp:TextBox ID="txtCode1" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td><asp:Label ID="lblCode2" runat="server"></asp:Label></td>
        <td><asp:TextBox ID="txtCode2" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2" align="center">
          <asp:Button ID="btnSubmitToken" Text="Accesar" runat="server" onclick="btnSubmitToken_Click" /> 
          <asp:Button ID="btnClearToken" runat="server" Text="Limpiar" onclick="btnClearToken_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <img src="images/transparent.png" width="1" height="10" />
        </td> 
    </tr>
    <tr>
       <td colspan="2">
           <asp:Label ID="lblMessageToken" runat="server"></asp:Label>
       </td>
    </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlSales" runat="server" BackImageUrl="~/Images/main.jpg" Width="400" Height="480" >
    <table border="0" cellpadding="0" cellspacing="0"  align="center"  >
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="36" /></td>
    </tr>
    <tr>
        <td colspan="5" align="left" valign="bottom" style="color:#FFFFFF;">
               <b>Bienvenid@:  </b><asp:Label ID="lblNameSale" runat="server"></asp:Label>
        </td> 
    </tr>
     <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="14" /></td>
    </tr>
     <tr>
        <td>
             <table border="0" cellpadding="0" cellspacing="0" align="center">
             <tr>
                    <td width="72" style="width: 72px;color:#000000; background-image:url(images/active_pad2.png); height:20px; font-weight:bold;" align="center">Venta</td>
                    <td width="72" style="width: 72px;background-image:url(images/inactive_pad.png);" align="center"><asp:LinkButton ID="lnkSales_Services" runat="server" Text="Servicios" Font-Underline="false" onclick="lnkSales_Services_Click"></asp:LinkButton></td>        
                    <td width="72" style="width: 72px;background-image:url(images/inactive_pad.png);" align="center"><asp:LinkButton ID="lnkSales_Insurances" runat="server" Text="Seguros" Font-Underline="false" onclick="lnkInsurance_Click"></asp:LinkButton></td>        
                    <td width="72" style="width: 72px;background-image:url(images/inactive_pad.png);" align="center"><asp:LinkButton ID="lnkSales_Balance" runat="server" Text="Saldo" Font-Underline="false" onclick="lnkBalance_Click"></asp:LinkButton></td>  
                    <td width="72" style="width: 72px;background-image:url(images/inactive_pad.png);" align="center"><asp:LinkButton ID="lnkSales_Report" runat="server" Text="Reporte" Font-Underline="false" onclick="lnkSales_Report_Click"></asp:LinkButton></td>
                    <td width="18" style="width: 18px;"><asp:ImageButton ID="btnExitSales" runat="server" ImageUrl="~/Images/exit2.png" onclick="btnExitSales_Click" /></td>
             </tr>
             </table>
        </td>
        
    </tr>         
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="10" /></td>
    </tr>    
    </tr>  
     </table>
     <table border="0" cellpadding="2" cellspacing="0" width="300" align="center" style="width:300px;" align="center"> 
             <tr>
                <td  style="color:#000000;">Producto:</td>
                <td ><asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" onselectedindexchanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="color:#000000;">Teléfono: </td>
                <td ><asp:TextBox ID="txtPhone" runat="server" MaxLength="10" Font-Size="Large" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="color:#000000;">Confirmación:</td>
                <td ><asp:TextBox ID="txtConfirmation" runat="server" MaxLength="10" Font-Size="Large"></asp:TextBox></td>
            </tr>
             <tr>
                <td style="color:#000000;">Monto:</td>
                <td ><asp:DropDownList ID="ddlAmounts" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                  <table border="0" cellpadding="2" cellspacing="0">
                   <tr>
                       <td><asp:Button ID="btnSale" Text="Recargar" runat="server" onclick="btnSale_Click" /> </td>
                       <td><asp:Button ID="btmSaleClear" runat="server" Text="Limpiar" onclick="btmSaleClear_Click"/></td>
                       <td><asp:ImageButton ID="ImgTicket" runat="server" ToolTip="Ultimo Ticket" ImageUrl="~/Images/receipt.png" Width="32" Height="32" onclick="ImgTicket_Click" /></td>
                   </tr>
                   </table>
                </td>
            </tr>
            <tr>
                <td colspan="2"><img src="images/transparent.png" width="1" height="10" /></td>
            </tr>
            <tr>
               <td colspan="2"  style="color:#000000;">
                   <asp:Label ID="lblSales_Message" runat="server"></asp:Label>
               </td>
            </tr>
            </table>
    </asp:Panel>
    <asp:Panel ID="pnlServices" runat="server" BackImageUrl="~/Images/main.jpg" Width="400" Height="480" >
    <table border="0" cellpadding="0" cellspacing="0"  align="center">
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="36" /></td>
    </tr>
    <tr>
        <td colspan="5" align="left" valign="bottom" style="color:#FFFFFF;">
               <b>Bienvenid@:  </b><asp:Label ID="lblNameServices" runat="server"></asp:Label>
        </td> 
    </tr>
     <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="14" /></td>
    </tr>
    <tr>
        <td>
             <table border="0" cellpadding="0" cellspacing="0" align="center">
             <tr>
                <td width="72" style="width: 72px; height:20px; background-image:url(images/inactive_pad.png);  " align="center"><asp:LinkButton ID="lnkServices_Sales" runat="server" Text="Venta" Font-Underline="false" onclick="lnkReport_Sales_Click"></asp:LinkButton></td>              
                <td width="72" style="width: 72px; color:#000000; background-image:url(images/active_pad2.png); " align="center">Servicios</td>  
                <td width="72" style="width: 72px; height:20px; background-image:url(images/inactive_pad.png);  " align="center"><asp:LinkButton ID="lnkServices_Insurances" runat="server" Text="Seguros" Font-Underline="false" onclick="lnkInsurance_Click"></asp:LinkButton></td>              
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkServices_Balance" runat="server" Text="Saldo" Font-Underline="false" onclick="lnkBalance_Click"></asp:LinkButton></td>
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkServices_Report" runat="server" Text="Reporte" Font-Underline="false" onclick="lnkSales_Report_Click"></asp:LinkButton></td>
                <td width="18" style="width: 18px;"><asp:ImageButton ID="btnServices_Exit" runat="server" ImageUrl="~/Images/exit2.png" onclick="btnExitSales_Click" /></td>
             </tr>
             </table>
         </td>
    </tr>            
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="10" /></td>
    </tr>   
    <tr>
        <td colspan="5">
            <table border="0" cellpadding="2" cellspacing="0" width="300" align="center" style="width:300px;" align="center">
            <tr>
                <td style="color:#000000;">Servicio:</td>
                <td><asp:DropDownList ID="ddlServices" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="color:#000000;">Referencia: </td>
                <td ><asp:TextBox ID="txtReference" runat="server" MaxLength="40" Font-Size="Large"></asp:TextBox></td>
            </tr>
             <tr>
                <td style="color:#000000;">Monto:</td>
                <td ><asp:TextBox ID="txtServiceAmount" runat="server" MaxLength="15" Font-Size="Large"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                  <table border="0" cellpadding="2" cellspacing="0" align="center">
                  <tr>
                      <td><asp:Button ID="btnService" Text="Pagar" runat="server" onclick="btnService_Click"  /></td>
                      <td><asp:Button ID="btnService_Clear" runat="server" Text="Limpiar" onclick="btnService_Clear_Click"/></td>
                      <td><asp:ImageButton ID="imbService_Ticket" runat="server" ToolTip="Ultimo Ticket" 
                              ImageUrl="~/Images/receipt.png" Width="32" Height="32" 
                              onclick="imbService_Ticket_Click" /></td>
                  </tr>
                  </table>
                </td>
            </tr>
            <tr>
                <td colspan="2"><img src="images/transparent.png" width="1" height="10" /></td>
            </tr>
            <tr>
               <td colspan="2"  style="color:#000000;">
                   <asp:Label ID="lblServiceMessage" runat="server"></asp:Label>
               </td>
            </tr>
            </table>
        </td>
    </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlInsurance" runat="server" BackImageUrl="~/Images/main.jpg" Width="400" Height="480" >
    <table border="0" cellpadding="0" cellspacing="0"  align="center">
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="36" /></td>
    </tr>
    <tr>
        <td colspan="5" align="left" valign="bottom" style="color:#FFFFFF; padding-left:10px;">
               <b>Bienvenid@:  </b><asp:Label ID="lblInsurance_User_Name" runat="server"></asp:Label>
        </td> 
    </tr>
     <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="14" /></td>
    </tr>
    <tr>
        <td style="padding-left:10px;">
             <table border="0" cellpadding="0" cellspacing="0">
             <tr>
                <td width="72" style="width: 72px; height:20px; background-image:url(images/inactive_pad.png);  " align="center"><asp:LinkButton ID="lnkInsurance_Sales" runat="server" Text="Venta" Font-Underline="false" onclick="lnkReport_Sales_Click"></asp:LinkButton></td>              
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkInsurance_Services" runat="server" Text="Servicios" Font-Underline="false" onclick="lnkSales_Services_Click"></asp:LinkButton></td>
                <td width="72" style="width: 72px; color:#000000; background-image:url(images/active_pad2.png); " align="center">Seguros</td>  
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkInsurance_Balance" runat="server" Text="Saldo" Font-Underline="false" onclick="lnkBalance_Click"></asp:LinkButton></td>
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkInsurance_Report" runat="server" Text="Reporte" Font-Underline="false" onclick="lnkSales_Report_Click"></asp:LinkButton></td>
                <td width="18" style="width: 18px;"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/exit2.png" onclick="btnExitSales_Click" /></td>
             </tr>
             </table>
         </td>
    </tr>            
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="10" /></td>
    </tr>   
    <tr>
        <td colspan="5">
            <table border="0" cellpadding="0" cellspacing="1">                            
                            <tr>
                                <td  align="right">Nombre Asegurado:</td>
                                <td align="left"><asp:TextBox ID="txtClient_Name" runat="server"  Width="100"></asp:TextBox></td>                                                            
                                <td  align="right">No Licencia:</td>
                                <td align="left"><asp:TextBox ID="txtDrivers_Licence_Number" runat="server"  Width="100"></asp:TextBox></td>                               
                            </tr>
                            <tr>
                                <td  align="right">Fecha Nacimiento:</td>
                                <td align="left"><telerik:RadDatePicker ID="rdpBirth_Date" runat="server"   Width="100"></telerik:RadDatePicker></td>                                                            
                                <td  align="right">Ocupación:</td>
                                <td align="left"><asp:TextBox ID="txtJob_Position" runat="server"  Width="100"></asp:TextBox></td>                               
                            </tr>
                            <tr>
                                <td  align="right">Nombre Conductor:</td>
                                <td align="left"><asp:TextBox ID="txtDriver_Name" runat="server"  Width="100"></asp:TextBox></td>
                                 <td  align="right">Entrada a EU:</td>
                                <td align="left"> <asp:DropDownList ID="ddlEntrance_State" runat="server" ></asp:DropDownList></td>
                            </tr>
                             <tr>
                                <td  align="right">Calle:</td>
                                <td align="left"><asp:TextBox ID="txtClient_Address_Street" runat="server"  Width="100"></asp:TextBox></td>
                                 <td  align="right">Número:</td>
                                <td align="left"><asp:TextBox ID="txtClient_Address_Street_Number" runat="server"  Width="100"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td  align="right">Colonia:</td>
                                <td align="left"><asp:TextBox ID="txtClient_Address" runat="server"  Width="100"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td  align="right">CP:</td>
                                <td align="left"><asp:TextBox ID="txtClient_Zip" runat="server"  Width="100"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td  align="right">Teléfono:</td>
                                <td align="left"><asp:TextBox ID="txtClient_Phone" runat="server"  Width="100"></asp:TextBox></td>                           
                                <td  align="right">Email:</td>
                                <td align="left"><asp:TextBox ID="txtClient_Email" runat="server"  Width="100"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right"  >Marca:</td>
                                <td align="left"><asp:DropDownList ID="ddlBrand" runat="server"  ></asp:DropDownList></td>                           
                                <td align="right"  >Modelo:</td>
                                <td align="left"><asp:TextBox ID="txtModel" runat="server"  Width="100"></asp:TextBox><asp:DropDownList ID="ddlModel" runat="server"  Visible="false"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right" >Año:</td>
                                <td align="left"><asp:DropDownList ID="ddlYear" runat="server" ></asp:DropDownList></td>
                                <td align="right" >Placas de:</td>
                                <td align="left"><asp:DropDownList ID="ddlCountry_Plates" runat="server" ></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right"  >Número de Serie:</td>
                                <td align="left"><asp:TextBox ID="txtVIN" runat="server" MaxLength="15"  Width="100"></asp:TextBox></td>
                                <td align="right" >Placas:</td>
                                <td align="left"><asp:TextBox ID="txtPlates" runat="server" MaxLength="7"  Width="100"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right" >Fecha Efectiva:</td>
                                <td align="left">
                                    <telerik:RadDateTimePicker  ID="rdpEffective_Date" runat="server" Width="110"  ></telerik:RadDateTimePicker></td>                            
                                <td align="right" >D&iacute;as:</td>
                                <td align="left"><select id="ddlProduct_Days" runat="server"></select>
                                    <telerik:RadDatePicker ID="rdpExpiration_Date" runat="server"   Visible="false"></telerik:RadDatePicker></td>
                            </tr>
                            <tr>
                                <td align="right" >Municipio:</td>
                                <td align="left"><asp:TextBox ID="txtInsurance_City"  runat="server" Text="JUAREZ" Width="100"></asp:TextBox></td>
                                <td align="right" >Estado:</td>
                                <td align="left"><asp:DropDownList ID="ddlInsurance_State" runat="server" ></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="center"><asp:Button ID="btnInsurance_Submit" runat="server" Text="Asegurar" OnClick="btnInsurance_Submit_Click" OnClientClick="return InsuranceValidation();" /></td>
                                <td align="center"><asp:Button ID="btnInsurance_Clear" runat="server" Text="Limpiar" OnClientClick="Clear_Insurance();"  /></td>                            
                                <td colspan="2" align="right" style="padding-right:155px;">
                                <label id="lblTotal_Insurance" runat="server" style="font-size: 12pt; font-weight:bolder;">$0.00</label></td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center"><asp:Label ID="lblInsurance_Status" runat="server"></asp:Label></td>
                            </tr>
                            </table> 
                            <asp:Label ID="lblInsurance_Is_Beta" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblInsurance_User" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblInsurance_Password" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblInsurance_PreFix" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblInsurance_Folio" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="pnlBalance" runat="server" BackImageUrl="~/Images/main.jpg" Width="400" Height="480" >
    <table border="0" cellpadding="0" cellspacing="0" align="center" >
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="36" /></td>
    </tr>
    <tr>
        <td colspan="6" align="left" valign="bottom" style="color:#FFFFFF;">
               <b>Bienvenid@:  </b><asp:Label ID="lblNameBalance" runat="server"></asp:Label>
        </td> 
    </tr>
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="14" /></td>
    </tr>
    <tr>
        <td>
             <table border="0" cellpadding="0" cellspacing="0" align="center">
             <tr>
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkBalance_Sales" runat="server" Text="Venta" Font-Underline="false" onclick="lnkReport_Sales_Click"></asp:LinkButton></td>
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkBalance_Services" runat="server" Text="Servicios" Font-Underline="false" onclick="lnkSales_Services_Click"></asp:LinkButton></td>        
                <td width="72" style="width: 72px; height:20px; background-image:url(images/inactive_pad.png);  " align="center"><asp:LinkButton ID="lnkBalance_Insurances" runat="server" Text="Seguros" Font-Underline="false" onclick="lnkInsurance_Click"></asp:LinkButton></td>              
                <td width="72" style="color:#000000; background-image:url(images/active_pad2.png); width: 72px; height:20px;" align="center">Saldo</td>  
               <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkBalance_Report" runat="server" Text="Reporte" Font-Underline="false" onclick="lnkSales_Report_Click"></asp:LinkButton></td>
                <td><asp:ImageButton ID="btnBalance_Exit" runat="server" ImageUrl="~/Images/exit2.png" onclick="btnExitSales_Click" /></td>
            </tr>
            </table>
        </td>
    </tr>       
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="10" /></td>
    </tr>   
    <tr>       
        <td colspan="5"  style="color:#000000; font-size:12pt;" align="center"><b><asp:Label ID="lblBalance" runat="server"></asp:Label></b></td>
    </tr>   
    </table>
    </asp:Panel>

    <asp:Panel ID="pnlReport" runat="server" BackImageUrl="~/Images/main.jpg" Width="400" Height="480" >
    <table border="0" cellpadding="0" cellspacing="0" width="308" align="center" style="width:308px;">
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="36" /></td>
    </tr>
    <tr>
        <td colspan="6" align="left" valign="bottom" style="color:#FFFFFF;">
               <b>Bienvenid@:  </b><asp:Label ID="lblNameReport" runat="server"></asp:Label>
        </td> 
    </tr>
     <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="14" /></td>
    </tr>
    <tr>
        <td>
             <table border="0" cellpadding="0" cellspacing="0" align="center">
             <tr>
                 <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkReport_Sales" runat="server" Text="Venta" Font-Underline="false" onclick="lnkReport_Sales_Click"></asp:LinkButton></td>
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkReport_Service" runat="server" Text="Servicios" Font-Underline="false" onclick="lnkSales_Services_Click"></asp:LinkButton></td>        
                <td width="72" style="width: 72px; height:20px; background-image:url(images/inactive_pad.png);  " align="center"><asp:LinkButton ID="lnkReport_Insurances" runat="server" Text="Seguros" Font-Underline="false" onclick="lnkInsurance_Click"></asp:LinkButton></td>              
                <td width="72" style="background-image:url(images/inactive_pad.png); width: 72px;" align="center"><asp:LinkButton ID="lnkReport_Balance" runat="server" Text="Saldo" Font-Underline="false" onclick="lnkBalance_Click"></asp:LinkButton></td>
                <td width="72" style="color:#000000; background-image:url(images/active_pad2.png); width: 72px; height:20px;" align="center">Reportes</td>
                <td><asp:ImageButton ID="btnReport_Exit" runat="server" ImageUrl="~/Images/exit2.png" onclick="btnExitSales_Click" /></td>
            </tr>
            </table>
        </td>
    </tr>   
    <tr>
        <td colspan="5"><img src="images/transparent.png" width="1" height="10" /></td>
    </tr>
    <tr>
        <td colspan="5" align="center" valign="bottom">
            <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td><b>Venta:  </b></td>
                <td><asp:DropDownList ID="ddlReport_Dates" runat="server"></asp:DropDownList></td>
                <td><asp:Button ID="btnReport" runat="server" Text="Mostrar" onclick="btnReport_Click" /></td>
            </tr>
            </table>               
        </td> 
    </tr>   
    <tr>
        <td colspan="2"><img src="images/transparent.png" width="1" height="10" /></td>
    </tr>
    </table>
    <table align="center">
    <tr>
        <td align="center" >
            <asp:GridView ID="gvReport" runat="server" 
                          AutoGenerateColumns="false"
                          AlternatingRowStyle-BackColor="#b0c8ed"
                          AlternatingRowStyle-ForeColor="#000000"
                          Font-Size="Large"
                          RowStyle-BackColor="#FFFFFF"
                          RowStyle-ForeColor="#000000"
                          ShowHeader="true"
                          ShowHeaderWhenEmpty="true"
                          ShowFooter="true"
                          OnRowDataBound="gvReport_RowDataBound"
                          BorderColor="#FFFFFF"                          
            >
            <HeaderStyle ForeColor="White" Font-Bold="True" BackColor="#0e278e" />
            <Columns>
                 <asp:BoundField HeaderText="Fecha" DataField="Fecha_Plataforma" ItemStyle-ForeColor="#000000" DataFormatString="{0:HH:mm:ss}" ItemStyle-BorderColor="#CCCCCC"  />
                 <asp:BoundField HeaderText="Telefono" DataField="Telefono" ItemStyle-ForeColor="#000000"  ItemStyle-BorderColor="#CCCCCC" />
                 <asp:BoundField HeaderText="Folio" DataField="Folio_Operadora" ItemStyle-ForeColor="#000000"  ItemStyle-BorderColor="#CCCCCC" />
                 <asp:BoundField HeaderText="Monto" DataField="Valor_Publico" ItemStyle-ForeColor="#000000" DataFormatString="{0:F0}" ItemStyle-HorizontalAlign="Right"  ItemStyle-BorderColor="#CCCCCC" />
                 <asp:BoundField HeaderText="Operadora" DataField="Nombre_Operadora" ItemStyle-ForeColor="#000000"  ItemStyle-BorderColor="#CCCCCC" ItemStyle-HorizontalAlign="Center" />
            </Columns>
             <FooterStyle ForeColor="#FFFFFF" Font-Bold="True" BackColor="#0e278e" />
            </asp:GridView>
        </td>
    </tr>
    </table>
    
    </asp:Panel>
    <asp:Panel ID="pnlTicket" runat="server" Width="100%" Height="100%" BackColor="#FFFFFF">   
   
    <table border="0" cellpadding="2" cellspacing="0" width="140">
    <tr>
        <td colspan="2" style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTiket_Name" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" align="center"><hr style="width:140px; height:1px; background-color:#000000; border: 1 solid #000000;"  /></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;">Producto:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Carrier" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;">Teléfono:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Phone" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;">Monto:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Amount" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;">Folio:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Folio" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;" valign="top">Fecha:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Date" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:ImageButton ID="Img_Close" runat="server" ImageUrl="~/Images/close.png" Width="24" Height="24" onclick="Img_Close_Click" />
        </td>
    </tr>
    </table>     
    </asp:Panel>
    <asp:Panel ID="pnlTicket_Service" runat="server" Width="100%" Height="100%" BackColor="#FFFFFF">    
    
    <table border="0" cellpadding="2" cellspacing="0" width="140">
    <tr>
        <td colspan="2" style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Service_Client" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" align="center"><hr style="width:140px; height:1px; background-color:#000000; border: 1 solid #000000;"  /></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;">Servicio:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Service_Name" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;" colspan="2">Cuenta:</td>
    </tr>
    <tr>
        <td style="color:#000000; font-weight:bold; font-size:8pt;" colspan="2"><asp:Label ID="lblTicket_Service_Reference" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;">Monto:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Service_Amount" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;">Folio:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Service_Folio" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="color:#000000; font-size:8pt;" valign="top">Fecha:</td>
        <td style="color:#000000; font-weight:bold; font-size:8pt;"><asp:Label ID="lblTicket_Service_Date" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:ImageButton ID="imbTicket_Service_Close" runat="server" 
                ImageUrl="~/Images/close.png" Width="24" Height="24" 
                onclick="imbTicket_Service_Close_Click" />
        </td>
    </tr>
    </table>    
    </asp:Panel>



            <font face="verdana"><asp:Label ID="lblTimer" runat="server" Font-Size="XX-Small" ForeColor="#FFFFFF"></asp:Label>&nbsp;</font>
       
    </form>






    
   


</body>
</html>
