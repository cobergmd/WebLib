/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Globalization;

namespace CJO.Web.HTML
{
    public class HtmlText : HtmlElement
    {
        private string _Text;                      
        private HorizontalAlignment _HorizontalAlign;               
        private Color _Color = Color.Empty;                     
        private int _Size = 0;                    
        private bool _Bold = false;           
        private bool _Fixed = false;          
        private bool _Italic = false;         
        private bool _Underscore = false;

        public HtmlText()
        { }

        public HtmlText(string text)
        {
            this.Text = text;
        }

        public HtmlText(string text, bool encode)
        {
            if (encode)
            {
                this.Text = HtmlEncode(text);
            }
            else
            {
                this.Text = text;
            }
        }

        public bool Bold
        {
            get { return _Bold; }
            set
            {
                string old = _Bold.ToString();
                _Bold = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Bold.ToString()));
            }
        }

        public bool Fixed
        {
            get { return _Fixed; }
            set
            {
                string old = _Fixed.ToString();
                _Fixed = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Fixed.ToString()));
            }
        }

        public bool Italic 
        {
            get { return _Italic; }
            set
            {
                string old = _Italic.ToString();
                _Italic = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Italic.ToString()));
            }
        }

        public bool Underscore
        {
            get { return _Underscore; }
            set
            {
                string old = _Underscore.ToString();
                _Underscore = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Underscore.ToString()));
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

        public Color Color
        {
            get { return _Color; }
            set
            {
                string old = _Color.ToString();
                _Color = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Color.ToString()));
            }
        }

        public int Size
        {
            get { return _Size; }
            set
            {
                string old = _Size.ToString();
                _Size = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _Size.ToString()));
            }
        }

        public override string ToString()
        {
            StringBuilder tag = new StringBuilder();

            if (_HorizontalAlign != HorizontalAlignment.NotSet)
                tag.Append(GetTextAlignmentTag());

            tag.Append(GetTextFontTag());

            tag.Append(GetTextStyleTag());
            tag.Append(_Text);
            tag.Append(GetEndTextStyleTag());
            tag.Append(GetEndTextFontTag());

            if (_HorizontalAlign != HorizontalAlignment.NotSet)
                tag.Append(GetEndTextAlignmentTag());
            
            return tag.ToString(); 
        }

        private string GetEndTextAlignmentTag()
        {
            if (_HorizontalAlign != HorizontalAlignment.NotSet)
                return ("</div>");
            else
                return "";
        }

        private string GetEndTextFontTag()
        {
            if (_Size != 0 || _Color != Color.Empty)
            {
                return ("</font>");
            }
            else
            {
                return "";
            }
        }

        private string GetEndTextStyleTag()
        {
            StringBuilder tag = new StringBuilder();

            if (_Fixed)
            {
                tag.Append("</tt>");
            }
            if (_Underscore)
            {
                tag.Append("</u>");
            }
            if (_Italic)
            {
                tag.Append("</i>");
            }
            if (_Bold)
            {
                tag.Append("</b>");
            }

            return tag.ToString();                     
        }

        private string GetFontColorAttribute()       
        {
            StringBuilder colorBuffer = new StringBuilder();

            if (!_Color.IsEmpty)
            {
                colorBuffer.Append(" color=\"#");
                String rgb = _Color.ToArgb().ToString("X8");
                colorBuffer.Append(rgb.Substring(2));
                colorBuffer.Append("\"");
            }
            
            return colorBuffer.ToString();
        }

        private string GetFontSizeAttribute()
        {
            StringBuilder tag = new StringBuilder();
            if (_Size != 0)
            {
                tag.Append(" size=\"");
                tag.Append(_Size);
                tag.Append("\"");
            }
            return tag.ToString();                                   
        }

        private string GetTextAlignmentTag()
        {
            if (_HorizontalAlign != HorizontalAlignment.NotSet)
            {
                StringBuilder tag = new StringBuilder();
                tag.Append("<div align=\"");
                tag.Append(_HorizontalAlign.ToString());
                tag.Append("\">");

                return tag.ToString();
            }
            else
            {
                return "";
            }
        }

        private string GetTextFontTag()      
        {
            StringBuilder tag = new StringBuilder();

            string extraAttributes = GetAttributeString();

            if (_Size != 0 || _Color != Color.Empty)     
            {
                tag.Append("<font");
                tag.Append(GetFontSizeAttribute());
                tag.Append(GetFontColorAttribute());
                tag.Append(extraAttributes);                     
                tag.Append(">");
            }
            return tag.ToString();                                  
        }

        private string GetTextStyleTag()
        {
            StringBuilder tag = new StringBuilder();

            if (_Bold)
            {
                tag.Append("<b>");
            }
            if (_Italic)
            {
                tag.Append("<i>");
            }
            if (_Underscore)
            {
                tag.Append("<u>");
            }
            if (_Fixed)
            {
                tag.Append("<tt>");
            }

            return tag.ToString();                              
        }

        /// <summary>
        /// HTML-encodes a string and returns the encoded string.
        /// </summary>
        /// <param name="text">The text string to encode. </param>
        /// <returns>The HTML-encoded text.</returns>
        public static string HtmlEncode(string text)
        {
            if (text == null)
                return null;

            StringBuilder sb = new StringBuilder(text.Length);

            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                switch (text[i])
                {
                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '"':
                        sb.Append("&quot;");
                        break;
                    case '&':
                        sb.Append("&amp;");
                        break;
                    default:
                        if (text[i] > 159)
                        {
                            // decimal numeric entity
                            sb.Append("&#");
                            sb.Append(((int)text[i]).ToString(CultureInfo.InvariantCulture));
                            sb.Append(";");
                        }
                        else
                            sb.Append(text[i]);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
