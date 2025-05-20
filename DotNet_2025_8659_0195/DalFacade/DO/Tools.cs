using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DO;

public static class Tools

{

    public static List<T> loadDataFromXmlFile<T>(List<T> items, string filePath, XmlSerializer serializer)
    {
        try
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    items = (List<T>)serializer.Deserialize(fs) as List<T>;

                }
                return items;
            }
        }
        catch (Exception)
        {

            throw;
        }
        return new List<T>();
    }

    public static void saveDataToXmlFile<T>(List<T> items, string filePath, XmlSerializer serializer)
    {
        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, items);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

}
