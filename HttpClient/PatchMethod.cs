/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Net;
using System.Text;
using System.Web;

namespace CJO.Web.Http
{
    public class PatchMethod : HttpMethod
    {
        public PatchMethod()
        {
            base.HttpVerb = "PATCH";
        }
    }
}