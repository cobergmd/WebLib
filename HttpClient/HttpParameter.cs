/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Text;

namespace CJO.Web.Http
{
    public class HttpParameter
    {
        private string _Name;
        private string _Value;

        public HttpParameter()
        {
        }

        public HttpParameter(string name, string value)
        {
            _Name = name;
            _Value = value;
        }

        /// Gets the bytes representing the current instance, using UTF8 encoding. 
        /// Designed for use with HTTP methods requiring binary transfer like POST.
        public byte[] GetByteValue()
        {
            return this.GetByteValue(Encoding.UTF8);
        }

        /// Gets the raw bytes representing the current instance. 
        /// Designed for use with HTTP methods requiring binary transfer like POST.
        protected virtual byte[] GetByteValue(Encoding encoding)
        {
            return encoding.GetBytes(this.Value);
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

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
    }
}


