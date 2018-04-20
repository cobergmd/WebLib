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
    public class HtmlTableRow : HtmlElement
    {
        private List<HtmlTableCell> _Cells = new List<HtmlTableCell>();
        private HorizontalAlignment _HorizontalAlign;
        private VerticalAlignment _VerticalAlign;

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

        public HtmlTableCell this[int index]
        {
            get { return _Cells[index]; }
        }

        public HtmlTableRow()
        { }

        public HtmlTableRow(HtmlTableCell[] cells)
        {
            foreach (HtmlTableCell cell in cells)
            {
                _Cells.Add(cell);
            }
        }

        public int ColumnCount
        {
            get { return _Cells.Count; }
        }

        public void AddColumn(HtmlTableCell cell)
        {
            _Cells.Add(cell);
        }

        public void RemoveAllColumns()
        {
            _Cells.Clear();
        }

        public void RemoveColumn(int index)
        {
            _Cells.RemoveAt(index);
        }

        public override string ToString()
        {
            StringBuilder tag = new StringBuilder("<tr");
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

            tag.Append(GetAttributeString());    
            tag.Append(">\n");

            foreach (HtmlTableCell cell in _Cells)
            {
                tag.Append(cell.ToString());
            }
            tag.Append("</tr>\n");

            return tag.ToString();
        }
    }
}
