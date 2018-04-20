/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Collections.Generic;

namespace CJO.Web.Http
{
    public class HttpParameterCollection : List<HttpParameter>
    {
        public void Add(HttpParameterCollection parameters)
        {
            foreach (HttpParameter parameter in parameters)
            {
                base.Add(parameter);
            }
        }

        public void Add(string name, string value)
        {
            base.Add(new HttpParameter(name, value));
        }
    }
}


