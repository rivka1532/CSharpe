using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal static class Config
{
    private static string fileName = "../xml/data-config.xml";
    public static int NextSaleCode
    {
        get
        {
            XElement xml = XElement.Load(fileName);
            int code = (int)xml.Element("NextSaleCode");
            xml.Element("NextSaleCode").SetValue((code+1).ToString());
            xml.Save(fileName);
            return code;
        }
    }
    public static int NextProductCode 
    {
        get
        {
            XElement xml = XElement.Load(fileName);
            int code = (int)xml.Element("NextProductCode");
            xml.Element("NextProductCode").SetValue((code + 1).ToString());
            xml.Save(fileName);
            return code;
        }
    }
}
