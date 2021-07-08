using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Serialization
{
    public static void ToXml<T>( ref XmlDocument parent, T toSerialize )
    {
        string xmlString = ToString<T>( toSerialize );
        parent.InnerXml = xmlString;
    }

    public static void AddXml<T>( ref XmlElement parent, T toSerialize )
    {
        string xmlString = ToString<T>( toSerialize );
        parent.InnerXml += xmlString;
    }
    public static string ToString<T>( T toSerialize )
    {
        XmlSerializer xmlSerializer = new XmlSerializer( toSerialize.GetType() );
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = true;
        var writer = new StringWriter();
        XmlWriter xmlWriter = XmlWriter.Create( writer, settings );
        var emptyNs = new XmlSerializerNamespaces( new[] { XmlQualifiedName.Empty } );

        xmlSerializer.Serialize( xmlWriter, toSerialize, emptyNs );
        return writer.ToString();
    }

    //public static string ToJsonString<T>( T toSerialize )
    //{
    //    //XmlSerializer xmlSerializer = new XmlSerializer( toSerialize.GetType() );
    //    //XmlWriterSettings settings = new XmlWriterSettings();
    //    //settings.OmitXmlDeclaration = true;
    //    //var writer = new StringWriter();
    //    //XmlWriter xmlWriter = XmlWriter.Create( writer, settings );
    //    //var emptyNs = new XmlSerializerNamespaces( new[] { XmlQualifiedName.Empty } );

    //    //xmlSerializer.Serialize( xmlWriter, toSerialize, emptyNs );
    //    //return writer.ToString();
    //    return JsonUtility.ToJson( toSerialize );

    //}
}
