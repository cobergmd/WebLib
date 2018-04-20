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
    public class HtmlMeta : HtmlElement
    {
        private String _Content;
        private String _Name;
        private String _HttpEquiv;
        private String _Url;

        public HtmlMeta()
        {
        }   

        public HtmlMeta(String httpEquiv, String content)
        {
            _HttpEquiv = httpEquiv;
            _Content = content;
        }

        public HtmlMeta(String httpEquiv, String content, String url)
        {
            _HttpEquiv = httpEquiv;
            _Content = content;
            _Url = url;
        }

        public string Content
        {
            get { return _Content; }
            set 
            {
                string old = _Content;
                _Content = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Content));
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                string old = _Name;
                _Name = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Name));
            }
        }

        public string HttpEquiv
        {
            get { return _HttpEquiv; }
            set
            {
                string old = _HttpEquiv;
                _HttpEquiv = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _HttpEquiv));
            }
        }

        public string Url
        {
            get { return _Url; }
            set
            {
                string old = _Url;
                _Url = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Url));
            }
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder("<meta");

            if (_HttpEquiv != null)
            {
                s.Append(" http-equiv=\"");
                s.Append(_HttpEquiv);
                s.Append("\"");
            }
            else
            {
                s.Append(" name=\"");
                s.Append(_Name);
                s.Append("\"");
            }

            if (_Url != null)
            {
                s.Append(" content=\"");
                s.Append(_Content);
                s.Append("; URL=");
                s.Append(_Url);
                s.Append("\"");
            }
            else
            {
                s.Append(" content=\"");
                s.Append(_Content);
                s.Append("\"");
            }

            s.Append(GetAttributeString());            

            s.Append(" />\n");

            return s.ToString();
        }
    }
}
