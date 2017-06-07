using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace UserData.SharedModels.Helpers
{
    public static class Serializer
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
    }

    public static class Deserializer
    {
        public static T DeserializeObject<T>(this string xmlData)
        {
            return (T)DeserializeObject(xmlData, typeof(T));
        }

        public static object DeserializeObject(this string xmlData, Type type)
        {
            var xmlSerializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(xmlData))
            {
                result = xmlSerializer.Deserialize(reader);
            }

            return result;
        }
    }

}
