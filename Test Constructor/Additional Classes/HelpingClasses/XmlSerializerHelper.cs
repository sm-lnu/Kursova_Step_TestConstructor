using System;
using System.IO;
using System.Xml.Serialization;

namespace Test_Constructor.SerializationDeserializationQuestion
{
    public class XmlSerializerHelper<T>
    {
        public string SerializeToXml(T obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, obj);
                    return writer.ToString();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Serialization error: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
                throw;
            }
        }

        public T DeserializeFromXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
