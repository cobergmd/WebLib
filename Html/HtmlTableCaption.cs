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
    public class HtmlTableCaption : HtmlElement
    {
        private string _Align;
        private IHtmlElement _Element;

        public HtmlTableCaption()
        { }

        public HtmlTableCaption(IHtmlElement element)
        {
            _Element = element;
        }

        public IHtmlElement Element
        {
            get { return _Element; }
            set
            {
                IHtmlElement old = _Element;
                _Element = value;
                //this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Element.ToString()));
            }
        }

        public string Align
        {
            get { return _Align; }
            set
            {
                string old = _Align;
                _Align = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Align));
            }
        }

        public override string ToString()
        {
            StringBuilder tag = new StringBuilder("<caption");
            if (_Align != null)
            {
                tag.Append(" align=\"");
                tag.Append(_Align);
                tag.Append("\"");
            }
            tag.Append(GetAttributeString());                   
            tag.Append(">");
            tag.Append(_Element.ToString());
            tag.Append("</caption>\n");

            return tag.ToString();
        }
    }
}
