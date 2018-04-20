/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace CJO.Web.Http
{
    public class XmlBodyEncoder : HttpBodyEncoder
    {
        private const string contentType = "text/xml";

        public sealed override void WriteRequestBody(Stream stream, HttpClient client, Encoding characterEncoding)
        {
            if (client.Object != null)
            {
                byte[] newlineBytes = characterEncoding.GetBytes("\r\n");
                string xml = ToXml(client.Object, client.ObjectType);
                byte[] xmlBytes = characterEncoding.GetBytes(xml);
                stream.Write(xmlBytes, 0, xmlBytes.Length);
                stream.Write(newlineBytes, 0, newlineBytes.Length);
            }
        }

        public sealed override string ContentType
        {
            get { return contentType; }
        }

        private string ToXml(object Obj, System.Type ObjType)
        {
            XmlSerializer ser;
            //ser = new XmlSerializer(ObjType, TargetNamespace);
            ser = new XmlSerializer(ObjType);
            MemoryStream memStream;
            memStream = new MemoryStream();
            XmlTextWriter xmlWriter;
            xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
            xmlWriter.Namespaces = true;
            ser.Serialize(xmlWriter, Obj, GetNamespaces());
            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }

        private XmlSerializerNamespaces GetNamespaces()
        {
            XmlSerializerNamespaces ns;
            ns = new XmlSerializerNamespaces();
            ns.Add("xs", @"http://www.w3.org/2001/XMLSchema");
            ns.Add("xsi", @"http://www.w3.org/2001/XMLSchema-instance");
            return ns;
        }

        private string TargetNamespace
        {
            get
            {
                return @"cjo";
            }
        }
    }
}
