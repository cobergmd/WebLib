/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Collections.Specialized;
using System.IO;

namespace CJO.Web.Http
{
    public class HttpDataSource
    {
        private NameValueCollection _Attributes;
        private string _ContentType;
        private byte[] _Data;
        private string _Name;

        public NameValueCollection Attributes
        {
            get
            {
                return ((_Attributes == null) ? (_Attributes = new NameValueCollection()) : _Attributes);
            }
        }

        public string ContentType
        {
            get
            {
                return _ContentType;
            }
            set
            {
                _ContentType = value;
            }
        }

        public byte[] Data
        {
            get
            {
                return _Data;
            }
        }

        public int Length
        {
            get
            {
                return ((this.Data != null) ? _Data.Length : -1);
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public HttpDataSource(string name, byte[] data)
        {
            _ContentType = "unknown";
            _Name = name;
            _Data = data;
        }

        public HttpDataSource(string name, byte[] data, string contentType)
            : this(name, data)
        {
            _ContentType = contentType;
        }

        public HttpDataSource(string name, FileInfo file, string contentType)
        {
            _ContentType = "unknown";
            using (MemoryStream memStream = new MemoryStream())
            {
                byte[] buffer = new byte[0x100];
                using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                {
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }
                memStream.Flush();
                _Data = memStream.ToArray();
                _ContentType = contentType;
                this.Attributes.Add("name", name);
                this.Attributes.Add("filename", Path.GetFileName(file.FullName));
            }
        }
    }
}


