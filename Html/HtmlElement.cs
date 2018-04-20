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
    public abstract class HtmlElement : IHtmlElement
    {
        private Dictionary<string, string> _Attributes = new Dictionary<string, string>();
        private List<IHtmlElement> _Elements = new List<IHtmlElement>();

        public event EventHandler<HtmlChangedEventArgs> HtmlChanged;
        public event EventHandler<HtmlChangedEventArgs> HtmlAdded;
        public event EventHandler<HtmlChangedEventArgs> HtmlRemoved;

        public abstract override string ToString();

        public void AddAttribute(string name, string value)
        {
            _Attributes.Add(name, value);
        }

        public void AddChild(IHtmlElement element)
        {
            _Elements.Add(element);
        }

        public string GetAttributeString()
        {
            StringBuilder buffer = new StringBuilder();

            foreach (string attributeName in _Attributes.Keys)
            {
                String attributeValue = _Attributes[attributeName];
                buffer.Append(" ");
                buffer.Append(attributeName);
                buffer.Append("=");
                buffer.Append("\"");
                buffer.Append(attributeValue);
                buffer.Append("\"");
            }

            return buffer.ToString();
        }

        protected virtual void OnHtmlChanged(HtmlChangedEventArgs e)
        {
            if (HtmlChanged != null) HtmlChanged(this, e);
        }
    }
}


