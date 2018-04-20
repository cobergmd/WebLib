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
    public class ZipBodyEncoder : HttpBodyEncoder
    {
        public override string ContentType => "application/zip";

        public override void WriteRequestBody(Stream stream, HttpClient client, Encoding characterEncoding)
        {
            throw new NotImplementedException();
        }
    }
}