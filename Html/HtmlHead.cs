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
    public class HtmlHead : HtmlElement
    {
        private string _Title;
        private List<HtmlMeta> _MetaTags = new List<HtmlMeta>();

        public HtmlHead()
        {}

        public HtmlHead(string title)
        {
            _Title = title;
        }

        public string Title
        {
            get { return _Title; }
            set
            {
                string old = _Title;
                _Title = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Title));
            }
        }

        public void AddMetaInformation(HtmlMeta meta)
        {
            _MetaTags.Add(meta);
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            s.Append("<head");
            s.Append(GetAttributeString());            
            s.Append(">\n");

            foreach (HtmlMeta meta in _MetaTags)
            {
                s.Append(meta.ToString());
            }

            if (_Title != null)
            {
                s.Append("<title");
                s.Append(">");
                s.Append(_Title);
                s.Append("</title>\n");
            }

            s.Append("</head>\n");

            return s.ToString();
        }
    }
}
