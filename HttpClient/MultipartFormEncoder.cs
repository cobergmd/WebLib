/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.IO;
using System.Text;
using System.Web;

namespace CJO.Web.Http
{
    /// Encoding: "multipart/form-data"
    /// Form-based file uploads
    /// http://www.faqs.org/rfcs/rfc1867.html
    public class MultipartFormEncoder : HttpBodyEncoder
    {
        /// Note: This must not appearin any of the data
        private string _Boundary = ("PostRequestTransport-" + DateTime.Now.ToString("yyyyMMddhHHmmss"));
        private string _ContentType = null;
        private Encoding _Encoding = Encoding.UTF8;

        private void AppendDataItem(Stream stream, HttpDataSource dataItem, string boundary, Encoding encoding)
        {
            StringBuilder dataItemBuilder = new StringBuilder();
            byte[] newlineBytes = encoding.GetBytes("\r\n");
            dataItemBuilder.Append("--" + boundary + "\r\n");
            dataItemBuilder.AppendFormat("Content-Disposition: form-data; {0}\r\n", GetAttributeString(dataItem));
            dataItemBuilder.AppendFormat("Content-Type: {0}\r\n", dataItem.ContentType);
            dataItemBuilder.Append("\r\n");
            stream.Write(encoding.GetBytes(dataItemBuilder.ToString()), 0, encoding.GetByteCount(dataItemBuilder.ToString()));
            if (dataItem.Data != null)
            {
                stream.Write(dataItem.Data, 0, dataItem.Data.Length);
            }
            stream.Write(newlineBytes, 0, newlineBytes.Length);
        }

        private void AppendNameValuePair(StringBuilder buffer, string name, string value, string boundary)
        {
            buffer.Append("--" + boundary + "\r\n");
            buffer.Append("Content-Disposition: form-data; Name=\"" + HttpUtility.UrlEncode(name) + "\"");
            buffer.Append("\r\n\r\n");
            buffer.Append(HttpUtility.UrlEncode(value) + "\r\n");
        }

        private void AppendRestParameter(StringBuilder buffer, HttpParameter parameter, string boundary)
        {
            AppendNameValuePair(buffer, parameter.Name, parameter.Value, boundary);
        }

        /// Creates a single string of name/value pairs from the
        /// Attributes collection of the supplied RestDataItem
        /// pairs are delimited by semi-colons.
        /// Example: name="value"; name-1="value-1"; name-2="value-2"
        private string GetAttributeString(HttpDataSource dataSource)
        {
            if (dataSource.Attributes != null)
            {
                StringBuilder attributeBuilder = new StringBuilder();
                for (int i = 0; i < dataSource.Attributes.Count; i++)
                {
                    attributeBuilder.AppendFormat("{0}=\"{1}\"{2}", 
                        dataSource.Attributes.Keys[i], 
                        dataSource.Attributes[dataSource.Attributes.Keys[i]], 
                        (i < (dataSource.Attributes.Count - 1)) ? "; " : string.Empty);
                }
                return attributeBuilder.ToString();
            }
            return null;
        }

        public sealed override void WriteRequestBody(Stream stream, HttpClient client, Encoding characterEncoding)
        {
            HttpParameterCollection parameters = client.Parameters;
            HttpDataSourceCollection dataItems = client.Data;
            StringBuilder postBodyBuilder = new StringBuilder();
            byte[] footerBytes = characterEncoding.GetBytes("\r\n--" + this.Boundary + "--\r\n");
            if ((parameters != null) && (parameters.Count > 0))
            {
                foreach (HttpParameter parameter in parameters)
                {
                    AppendRestParameter(postBodyBuilder, parameter, this.Boundary);
                }
                byte[] parameterBytes = characterEncoding.GetBytes(postBodyBuilder.ToString());
                stream.Write(parameterBytes, 0, parameterBytes.Length);
            }
            if ((dataItems != null) && (dataItems.Count > 0))
            {
                foreach (HttpDataSource dataItem in dataItems)
                {
                    AppendDataItem(stream, dataItem, this.Boundary, characterEncoding);
                }
            }
            stream.Write(footerBytes, 0, footerBytes.Length);
        }

        public string Boundary
        {
            get
            {
                return _Boundary;
            }
            set
            {
                _Boundary = value;
            }
        }

        public sealed override string ContentType
        {
            get
            {
                return ((_ContentType == null) ? ("multipart/form-data, boundary=" + this.Boundary) : _ContentType);
            }
        }
    }
}


