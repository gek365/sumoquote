using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BestDealWebApp.Services.Service3
{
    public class Api3Mapper : IXmlMapper<Api3Request, Api3Response>
    {
        public string ToString(Api3Request request)
        { 
            var requestSerializer = new XmlSerializer(typeof(Api3Request));
            
            var memoryStream = new MemoryStream();
            
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8)
            {
                Formatting = Formatting.Indented
            };
            
            requestSerializer.Serialize(xmlTextWriter, request);
            
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        public Api3Response FromStream(Stream stream)
        {
            var responseSerializer =
                new XmlSerializer(typeof(Api3Response));
            
            return responseSerializer.Deserialize(stream) as Api3Response;
        }
    }
}
