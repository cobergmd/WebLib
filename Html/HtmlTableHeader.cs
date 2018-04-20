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
    public class HtmlTableHeader : HtmlTableCell
    {
        private HtmlElement _Element = null;

        public HtmlTableHeader()
        { }

        public HtmlTableHeader(HtmlElement element)
        {
            _Element = element;
        }

        public HtmlElement Element
        {
            get { return _Element; }
            set
            {
                HtmlElement old = _Element;
                _Element = value;
                //this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Element.ToString()));
            }
        }

        public override string ToString()
        {
            StringBuilder tag = new StringBuilder("<th");
            tag.Append(GetAttributeTag());
            tag.Append(_Element.ToString());
            tag.Append("</th>\n");

            return tag.ToString();
        }
    }
}
