/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJO.Web.HTML
{
    public class HtmlDocument : HtmlElement
    {
        private HtmlHead _Head = null;    
        private HtmlBody _Body = null;            

        public HtmlDocument()
        { 
            _Body = new HtmlBody();
        }

        public HtmlDocument(HtmlHead head, HtmlBody body)
        {
            _Head = head;
            _Body = body;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder("<html>\n");
            
            if (_Head != null)
            {
              s.Append(_Head.ToString());
            }

            if (_Body != null)
            {
                s.Append(_Body.ToString());
            }

            s.Append("</html>\n");

            return s.ToString();
        }
    }
}
