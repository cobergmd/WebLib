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
    public class HtmlHeading : HtmlElement
    {
        private int _Level;
        private string _Text;
        private HorizontalAlignment _Align;

        public HtmlHeading()
        {
            _Level = 1;
        }

        public HtmlHeading(int level)
        {
            _Level = level;
        }

        public HtmlHeading(int level, string text)
        {
            _Level = level;
            _Text = text;
        }

        public HtmlHeading(int level, string text, HorizontalAlignment align)
        {
            _Level = level;
            _Text = text;
            _Align = align;
        }

        public int Level
        {
            get { return _Level; }
            set 
            {
                string old = _Level.ToString();
                _Level = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Level.ToString()));
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

        public HorizontalAlignment Align
        {
            get { return _Align; }
            set 
            {
                string old = _Align.ToString();
                _Align = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Align.ToString()));
            }
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder("<h" + _Level.ToString());

            switch (_Align)
            {
                case HorizontalAlignment.Left:
                    s.Append(" align=\"left\"");
                    break;
                case HorizontalAlignment.Right:
                    s.Append(" align=\"right\"");
                    break;
                case HorizontalAlignment.Center:
                    s.Append(" align=\"center\"");
                    break;
                default:
                    break;
            }
            s.Append(GetAttributeString());           

            s.Append(">" + _Text + "</h");

            if (_Level > 0)
                s.Append(_Level.ToString());

            s.Append(">");

            return s.ToString();
        }
    }
}
