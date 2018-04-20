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
    public class HtmlImage : HtmlElement
    {
        private string _Name;
        private string _Source;
        private ImageAlignment _Align = ImageAlignment.Left;
        private string _AlternateText;
        private int _Border = -1;
        private int _Height = 0;
        private int _Hspace = 0;
        private int _Vspace = 0;
        private int _Width = 0;

        public int Width
        {
            get { return _Width; }
            set
            {
                string old = _Width.ToString();
                _Width = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Width.ToString()));
            }
        }

        public int HSpace
        {
            get { return _Hspace; }
            set 
            {
                string old = _Hspace.ToString();
                _Hspace = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Hspace.ToString()));
            }
        }

        public int VSpace
        {
            get { return _Vspace; }
            set 
            {
                string old = _Vspace.ToString();
                _Vspace = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Vspace.ToString()));
            }
        }

        public int Height
        {
            get { return _Height; }
            set 
            {
                string old = _Height.ToString();
                _Height = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Height.ToString()));
            }
        }

        public int Border
        {
            get { return _Border; }
            set 
            {
                string old = _Border.ToString();
                _Border = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Border.ToString()));
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

        public string Source
        {
            get { return _Source; }
            set 
            {
                string old = _Source;
                _Source = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Source));
            }
        }

        public ImageAlignment ImageAlignment
        {
            get { return _Align; }
            set 
            {
                string old = _Align.ToString();
                _Align = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Align.ToString()));
            }
        }

        public string AlternateText
        {
            get { return _AlternateText; }
            set 
            {
                string old = _AlternateText;
                _AlternateText = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _AlternateText));
            }
        }

        public HtmlImage()
        {
        }

        public HtmlImage(string source, string alternate)
        {
            this.Source = source;
            this.AlternateText = alternate;
        }

        public override string ToString()
        {
            if (_Source == null)
            {
                throw new Exception("Attempting to render element before setting image source.");
            }

            StringBuilder buffer = new StringBuilder("<img");

            buffer.Append(" src=\"");
            buffer.Append(_Source);
            buffer.Append("\"");

            buffer.Append(" alt=\"");
            buffer.Append(_AlternateText);
            buffer.Append("\"");

            switch (_Align)
            {
                case HTML.ImageAlignment.Left:
                    buffer.Append(" align=\"left\"");
                    break;
                case HTML.ImageAlignment.Right:
                    buffer.Append(" align=\"right\"");
                    break;
                case HTML.ImageAlignment.Top:
                    buffer.Append(" align=\"top\"");
                    break;
                case HTML.ImageAlignment.TextTop:
                    buffer.Append(" align=\"texttop\"");
                    break;
                case HTML.ImageAlignment.Middle:
                    buffer.Append(" align=\"middle\"");
                    break;
                case HTML.ImageAlignment.AbsMiddle:
                    buffer.Append(" align=\"absmiddle\"");
                    break;
                case HTML.ImageAlignment.Baseline:
                    buffer.Append(" align=\"baseline\"");
                    break;
                case HTML.ImageAlignment.Bottom:
                    buffer.Append(" align=\"bottom\"");
                    break;
                case HTML.ImageAlignment.AbsBottom:
                    buffer.Append(" align=\"absbottom\"");
                    break;
                default:
                    break;
            }

            if (_Name != null)
            {
                buffer.Append(" name=\"");
                buffer.Append(_Name);
                buffer.Append("\"");
            }

            if (_Border > -1)
            {
                buffer.Append(" border=\"");
                buffer.Append(_Border);
                buffer.Append("\"");

            }

            if (_Height > 0)
            {
                buffer.Append(" height=\"");
                buffer.Append(_Height);
                buffer.Append("\"");
            }

            if (_Width > 0)
            {
                buffer.Append(" width=\"");
                buffer.Append(_Width);
                buffer.Append("\"");
            }

            if (_Hspace > 0)
            {
                buffer.Append(" hspace=\"");
                buffer.Append(_Hspace);
                buffer.Append("\"");
            }

            if (_Vspace > 0)
            {
                buffer.Append(" vspace=\"");
                buffer.Append(_Vspace);
                buffer.Append("\"");
            }

            buffer.Append(GetAttributeString());
            buffer.Append(" />");

            return buffer.ToString();
        }
    }
}
