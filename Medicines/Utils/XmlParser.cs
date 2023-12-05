using System.Text;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;



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

    public static string SerializeXml<T>(this T obj, string rootName, bool omitXmlDeclaration = false)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj), "Object to serialize cannot be null.");

        if (string.IsNullOrEmpty(rootName))
            throw new ArgumentNullException(nameof(rootName), "Root name cannot be null or empty.");

        try
        {
            XmlRootAttribute xmlRoot = new(rootName);
            XmlSerializer xmlSerializer = new(typeof(T), xmlRoot);

            XmlSerializerNamespaces namespaces = new();
            namespaces.Add(string.Empty, string.Empty);

            XmlWriterSettings settings = new()
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\r\n",

            };

            StringBuilder sb = new();
            using var stringWriter = new StringWriter(sb);
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);

            xmlSerializer.Serialize(xmlWriter, obj, namespaces);
            return sb.ToString().TrimEnd();
        }
        catch (InvalidOperationException ex)
        {
            Debug.WriteLine($"Serialization error: {ex.Message}");
            throw new InvalidOperationException($"Serializing {typeof(T)} failed.", ex);
        }
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
