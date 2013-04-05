using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

namespace App_VM.App_Code
{
    public static class Utilities
    {
        public static string XML_Builder(string Root_Node, string Sales_Point, string Password, string Carrier, string Date_Start, string Date_End, string Operation_Type)
        {
            XmlDocument document = new XmlDocument();
            XmlElement root_element;
            XmlElement element;
            XmlDeclaration XML_Declaration = document.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
            document.AppendChild(XML_Declaration);

            root_element = document.CreateElement(Root_Node);
            element = document.CreateElement("PuntoVenta");
            element.InnerText = Sales_Point.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Password");
            element.InnerText = Password.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Mac_Address");
            element.InnerText = GetMacAddress();
            root_element.AppendChild(element);

            element = document.CreateElement("Operadora");
            element.InnerText = Carrier.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("FechaInicio");
            element.InnerText = Date_Start.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("FechaFin");
            element.InnerText = Date_End.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Tipo");
            element.InnerText = Operation_Type.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("IDAplicacion");
            element.InnerText = Constants.APPLICATION_ID;
            root_element.AppendChild(element);

            document.AppendChild(root_element);

            string encrypted_text = Encryption.Text_Encryption(document.OuterXml, 1024, Constants.ENCRYPTION_KEY);
            return XML_Application(Sales_Point, encrypted_text);
        }

        public static string XML_Application(string Sales_Point, string Encrypted_Text)
        {            
            return ("<TRANSACCION><PUNTOVENTA>" + Sales_Point + "</PUNTOVENTA><PROCESO><![CDATA[" + Encrypted_Text + "]]></PROCESO></TRANSACCION>");
        }

        public static string Validate_Token(string Sales_Point, string Primary_Code, string Secondary_Code, string Primary_Value, string Secondary_Value)
        {
            XmlDocument document = new XmlDocument();
            XmlElement root_element;
            XmlElement element;
            XmlDeclaration XML_Declaration = document.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
            document.AppendChild(XML_Declaration);

            root_element = document.CreateElement("VALIDATOKEN");
            element = document.CreateElement("PuntoVenta");
            element.InnerText = Sales_Point.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Combinacion1");
            element.InnerText = Primary_Code.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Combinacion2");
            element.InnerText = Secondary_Code.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Valor1");
            element.InnerText = Primary_Value.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Valor2");
            element.InnerText = Secondary_Value.Trim();
            root_element.AppendChild(element);

            document.AppendChild(root_element);

            string encrypted_text = Encryption.Text_Encryption(document.OuterXml, 1024, Constants.ENCRYPTION_KEY);
            return XML_Application(Sales_Point, encrypted_text);
        }

        public static string Sales_Transaction(string Sales_Point, string Password, string Product, string Sales_Date, string Phone_Number, string Folio, string Carrier, string Amount)
        {
            XmlDocument document = new XmlDocument();
            XmlElement root_element;
            XmlElement element;
            XmlDeclaration XML_Declaration = document.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
            document.AppendChild(XML_Declaration);

            root_element = document.CreateElement("SOLICITARTRANSACCION");
            element = document.CreateElement("PuntoVenta");
            element.InnerText = Sales_Point.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Password");
            element.InnerText = Password.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Producto");
            element.InnerText = Product.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("FechaVenta");
            element.InnerText = Sales_Date.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Telefono");
            element.InnerText = Phone_Number.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Folio");
            element.InnerText = Folio.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Operadora");
            element.InnerText = Carrier.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Cantidad");
            element.InnerText = Amount.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("FechaInicio");
            element.InnerText = Phone_Number.Trim();
            root_element.AppendChild(element);

            element = document.CreateElement("Mac_Address");
            element.InnerText = GetMacAddress();
            root_element.AppendChild(element);

            element = document.CreateElement("IDAplicacion");
            element.InnerText = Constants.APPLICATION_ID;
            root_element.AppendChild(element);

            document.AppendChild(root_element);

            string encrypted_text = Encryption.Text_Encryption(document.OuterXml, 1024, Constants.ENCRYPTION_KEY);
            return XML_Application(Sales_Point, encrypted_text);
        }

        public static string Sales_Report(string Sales_Point, string Password, string Sales_Date)
        {
            string XML_Result = XML_Builder("CORTEDELDIA", Sales_Point, Password, String.Empty, Sales_Date, String.Empty, Constants.PRODUCT_TYPE_TAE);
            return XML_Result;
        }

        public static string Get_XML_Value(string XML, string Node)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(XML);
            XmlNodeList Node_List = document.GetElementsByTagName(Node);
            return Node_List[0].InnerText;
        }

        private static string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = "";
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed && !String.IsNullOrEmpty(tempMac) && tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }
            return macAddress;
        }

        public static string Pad(Int64 number, int lenght)
        {
            string result = number.ToString();


            for (int i = result.Length; i < lenght; i++)
            {
                result = "0" + result;
            }

            return result;
        }

    }
}