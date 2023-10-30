using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Test_Constructor.SerializationDeserializationQuestion
{
    class SerializeDeserializeQuestion<T>
    {
        StringBuilder sbData;
        StringWriter swWriter;
        XmlDocument xDoc;
        XmlNodeReader xNodeReader;
        XmlSerializer xmlSerializer;

        public string SerializeData(T data)
        {
            XmlSerializer questionSerializer = new XmlSerializer(typeof(T));
            swWriter = new StringWriter(sbData);
            questionSerializer.Serialize(swWriter, data);
            return sbData.ToString();
        }
        public T DeserializeData(string dataXML)
        {
            xDoc = new XmlDocument();
            xDoc.LoadXml(dataXML);
            xNodeReader = new XmlNodeReader(xDoc.DocumentElement);
            xmlSerializer = new XmlSerializer(typeof(T));
            var questionData = xmlSerializer.Deserialize(xNodeReader);
            T deserializedQuestion = (T)questionData;
            return deserializedQuestion;
        }
    }
}
