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
    public class HtmlTableCell : HtmlElement
    {
        private IHtmlElement _Element;       
        private HorizontalAlignment _HorizontalAlign;                 
        private int _ColSpan = 1;              
        private int _Height;                   
        private int _RowSpan = 1;              
        private VerticalAlignment _VerticalAlign;                
        private int _Width;                    
        private bool _IsWrap = true;          
        private bool _IsHeightPercent = false; 
        private bool _IsWidthPercent = false;

        public int ColumnSpan
        {
            get { return _ColSpan; }
            set
            {
                string old = _ColSpan.ToString();
                _ColSpan = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _ColSpan.ToString()));
            }
        }

        public int RowSpan
        {
            get { return _RowSpan; }
            set
            {
                string old = _RowSpan.ToString();
                _RowSpan = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _RowSpan.ToString()));
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

        public VerticalAlignment VerticalAlignment
        {
            get { return _VerticalAlign; }
            set
            {
                string old = _VerticalAlign.ToString();
                _VerticalAlign = value;
                this.OnHtmlChanged(new HtmlChangedEventArgs(this, old, _VerticalAlign.ToString()));
            }
        }

        public HtmlTableCell()
        { }

        public HtmlTableCell(IHtmlElement element)
        {
            _Element = element;
        }

        public override string ToString()
        {
            StringBuilder tag = new StringBuilder(GetStartTag());
            tag.Append(GetAttributeTag());
            tag.Append(_Element.ToString());
            tag.Append(GetEndTag());

            return tag.ToString(); 
        }

        private string GetStartTag()
        {
            return "<td";
        }

        private string GetEndTag()
        {
            return "</td>\n";
        }

        protected string GetAttributeTag()
        {
            StringBuilder tag = new StringBuilder();

            if (_HorizontalAlign != HorizontalAlignment.NotSet)
            {
                tag.Append(" align=\"");
                tag.Append(_HorizontalAlign);
                tag.Append("\"");
            }

            if (_VerticalAlign != VerticalAlignment.NotSet)    
            {
                tag.Append(" valign=\"");
                tag.Append(_VerticalAlign);
                tag.Append("\"");
            }

            if (_RowSpan > 1)  
            {
                tag.Append(" rowspan=\"");
                tag.Append(_RowSpan);
                tag.Append("\"");
            }
            if (_ColSpan > 1)  
            {
                tag.Append(" colspan=\"");
                tag.Append(_ColSpan);
                tag.Append("\"");
            }

            if (_Height > 0)   
            {
                tag.Append(" height=\"");
                tag.Append(_Height);

                if (_IsHeightPercent)
                    tag.Append("%");
                tag.Append("\"");
            }
            if (_Width > 0)    
            {
                tag.Append(" width=\"");
                tag.Append(_Width);

                if (_IsWidthPercent)
                    tag.Append("%");
                tag.Append("\"");
            }

            if (!_IsWrap)         
                tag.Append(" nowrap=\"nowrap\"");

            tag.Append(GetAttributeString());         
            tag.Append(">");

            return tag.ToString();            
        }
    }
}
