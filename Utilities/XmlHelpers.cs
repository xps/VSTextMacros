using System.IO;
using System.Xml.Serialization;

namespace VSTextMacros.Utilities
{
    public static class XmlHelpers
    {
        public static string Serialize<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        public static T Deserialize<T>(string data)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(data))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
