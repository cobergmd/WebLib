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
    public class FormUrlEncoder : HttpBodyEncoder
    {
        private const string _ContentType = "application/x-www-form-urlencoded";

        public sealed override void WriteRequestBody(Stream stream, HttpClient client, Encoding characterEncoding)
        {
            HttpParameterCollection parameters = client.Parameters;
            if ((parameters != null) && (parameters.Count > 0))
            {
                byte[] newlineBytes = characterEncoding.GetBytes("\r\n");
                HttpParameter parameter = null;
                for (int i = 0; i < parameters.Count; i++)
                {
                    parameter = parameters[i];
                    string parameterString = string.Format("{0}={1}{2}", HttpUtility.UrlPathEncode(parameter.Name), HttpUtility.UrlPathEncode(parameter.Value), (i < (parameters.Count - 1)) ? "&" : string.Empty);
                    byte[] parameterBytes = characterEncoding.GetBytes(parameterString);
                    stream.Write(parameterBytes, 0, parameterBytes.Length);
                }
                stream.Write(newlineBytes, 0, newlineBytes.Length);
            }
        }

        public sealed override string ContentType
        {
            get
            {
                return _ContentType;
            }
        }
    }
}


