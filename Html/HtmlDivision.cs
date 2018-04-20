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
    // HTML DIV Element
    // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/div
    public class HtmlDivision : HtmlElement
    {
        private HorizontalAlignment _HorizontalAlign = HorizontalAlignment.NotSet;
        private List<HtmlElement> _Elements = new List<HtmlElement>();

        public HorizontalAlignment HorizontalAlignment
        {
            get { return _HorizontalAlign; }
            set
            {
                string old = _HorizontalAlign.ToString();
                _HorizontalAlign = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _HorizontalAlign.ToString()));
            }
        }

        public HtmlDivision()
        { }

        public HtmlDivision(HtmlElement element)
        {
            _Elements.Add(element);
        }

        public HtmlDivision(HtmlElement element, HorizontalAlignment alignment)
        {
            _Elements.Add(element);
            _HorizontalAlign = alignment;
        }

        public void AddItem(HtmlElement element)
        {
            _Elements.Add(element);
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            if (_HorizontalAlign == HTML.HorizontalAlignment.Left)
                s.Append("<div align=\"left\"");
            else if (_HorizontalAlign == HTML.HorizontalAlignment.Right)
                s.Append("<div align=\"right\"");
            else if (_HorizontalAlign == HTML.HorizontalAlignment.Center)
                s.Append("<div align=\"center\"");
            else
                s.Append("<div ");

            s.Append(GetAttributeString());                                             

            s.Append(">\n");

            foreach (HtmlElement element in _Elements)
            {
                s.Append(element.ToString());
            }

            s.Append("\n</div>\n");

            return s.ToString();
        }
    }
}
