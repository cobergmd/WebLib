/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CJO.Web.HTML
{
    public class HtmlHyperlink : HtmlElement
    {
        private string _BookmarkName;
        private string _Link;
        // Properties associated with the hyperlink. (ie. parameters for URL rewriting)
        private Dictionary<string, string> _Properties = new Dictionary<string,string>();
        private string _Target;                
        private string _Text;                  
        private string _Title;                 
        // The bookmark location (ie - #location) for the link resource.  
        private string _Location;

        public HtmlHyperlink()
        {
        }

        public HtmlHyperlink(string link)
        {
            if (link == null)
                throw new ArgumentException("link");
            _Link = link;      
        }

        public HtmlHyperlink(string link, string text) 
            : this(link)
        {
            if (text == null)
                throw new ArgumentException("text");
            _Text = text;
        }

        public HtmlHyperlink(string link, string text, string target)
            : this(link, text)
        {
            if (target == null)
                throw new ArgumentException("target");
            _Target = target;
        }

        public string BookmarkName
        {
            get { return _BookmarkName; }
            set
            {
                string old = _BookmarkName;
                _BookmarkName = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _BookmarkName));
            }
        } 

        public string Link
        {
            get { return _Link; }
            set
            {
                string old = _Link;
                _Link = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Link));
            }
        } 

        public string Target
        {
            get { return _Target; }
            set
            {
                string old = _Target;
                _Target = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Target));
            }
        } 

        public string Text
        {
            get { return _Text; }
            set
            {
                string old = _Text;
                _Text = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Text));
            }
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

        public string Location
        {
            get { return _Location; }
            set
            {
                string old = _Location;
                _Location = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Location));
            }
        } 

        public void AddProperty(string name, string value)
        {
            _Properties.Add(name, value);
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();

            buffer.Append("<a href=\"");
            buffer.Append(_Link);

            string parmStart = "?";
            foreach (string key in _Properties.Keys)
            {
                buffer.Append(parmStart);
                buffer.Append(HttpUtility.UrlEncode(key));
                buffer.Append("=");
                buffer.Append(HttpUtility.UrlEncode(_Properties[key]));
                parmStart = "&";
            }

            if (_Location != null)                         
            {
                buffer.Append("#");
                buffer.Append(_Location);                  
            }

            buffer.Append("\"");

            if (_BookmarkName != null)
            {
                buffer.Append(" name=\"");
                buffer.Append(_BookmarkName);
                buffer.Append("\"");
            }

            if (_Title != null)
            {
                buffer.Append(" title=\"");
                buffer.Append(_Title);
                buffer.Append("\"");
            }

            if (_Target != null)
            {
                buffer.Append(" target=\"");
                buffer.Append(_Target);
                buffer.Append("\"");
            }

            buffer.Append(GetAttributeString());                                              

            buffer.Append(">");
            buffer.Append(_Text);
            buffer.Append("</a>");

            return buffer.ToString();
        }
    }
}
