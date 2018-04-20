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
    public class HtmlBody : HtmlElement
    {
        private List<HtmlElement> _Elements = new List<HtmlElement>();

        public void AddItem(HtmlElement element)
        {
            _Elements.Add(element);
        }

        public override string ToString()
        {
            StringBuilder body = new StringBuilder("<body>\n");

            foreach (HtmlElement element in _Elements)
            {
                body.Append(element.ToString());
                body.Append("\n");
            }

            body.Append("</body>\n");

            return body.ToString();
        }
    }
}