using System.Text;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;



namespace Medicines.Utils;

public static class XmlParser
{
    public static string SerializeToXml<T>(this T obj, string rootName)
    {
        var xmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));
        var ns = new XmlSerializerNamespaces();
        ns.Add(string.Empty, string.Empty);

        string result = string.Empty;

        using MemoryStream stream = new MemoryStream();
        xmlSerializer.Serialize(stream, obj, ns);
        result = Encoding.UTF8.GetString(stream.ToArray());

        return result;
    }

    public static T DeserializeFromXml<T>(this string xml, string rootName)
    {
        var xmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));
        T result = default;

        using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
        result = (T)xmlSerializer.Deserialize(stream);


        return result;

    }
}
