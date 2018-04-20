/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.IO;
using System.Text;

namespace CJO.Web.Http
{
    public abstract class HttpBodyEncoder
    {
        protected HttpBodyEncoder()
        {
        }

        public static HttpBodyEncoder FormUrlBody
        {
            get
            {
                return new FormUrlEncoder();
            }
        }

        public static HttpBodyEncoder MultipartFormBody
        {
            get
            {
                return new MultipartFormEncoder();
            }
        }

        public byte[] CreateRequestBody(HttpClient client, Encoding characterEncoding)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                this.WriteRequestBody(memStream, client, characterEncoding);
                return memStream.ToArray();
            }
        }

        // The supplied stream should not be closed by the implementation of this method.
        public abstract void WriteRequestBody(Stream stream, HttpClient client, Encoding characterEncoding);

        public abstract string ContentType { get; }
    }
}


