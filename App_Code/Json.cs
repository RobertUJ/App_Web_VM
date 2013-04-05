using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Json
/// </summary>
public static class Json
{
    public static string Parse_Value(string json, string Name)
    {
        string result = String.Empty;
        int index = json.IndexOf("\"" + Name + "\"");
        if (index > 0)
        {
            index = index + Name.Length + 4;
            int last_position = json.IndexOf("\"", index);
            result = json.Substring(index, last_position - index);
        }

        return result;
    }

    public static string Parse(string json, string Name, string default_value)
    {
        string result = default_value;
        int index = json.IndexOf("\"" + Name + "\"");
        if (index > 0)
        {
            index = index + Name.Length + 4;
            int last_position = json.IndexOf("\"", index);
            result = json.Substring(index, last_position - index);
        }

        return result;
    }

    public static string Builder(string json, string name, string value)
    {
        if (json.Length == 0)
        {
            json = "{\"" + name + "\":\"" + value + "\"}";
        }
        else
        {
            json = json.Substring(0, json.Length - 1);
            json = json + ",\"" + name + "\":\"" + value + "\"}";
        }
        return json;
    }
}