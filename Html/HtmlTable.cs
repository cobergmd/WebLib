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
    public class HtmlTable : HtmlElement
    {
        private List<HtmlTableRow> _Rows = new List<HtmlTableRow>();                           
        private HtmlTableCaption _Caption = null;
        private List<HtmlTableHeader> _Headers = new List<HtmlTableHeader>();

        private HorizontalAlignment _Alignment = HorizontalAlignment.NotSet;                      
        private int _BorderWidth = 0;                   
        private int _CellPadding = -1;                  
        private int _CellSpacing = -1;                  
        private int _Width = 0;                         

        private bool _HeaderInUse = false;            
        private bool _WidthPercent = false;  

        public HtmlTable()
        {
        }

        public HtmlTable(HtmlTableRow[] rows)
        {
            foreach (HtmlTableRow row in rows)
            {
                _Rows.Add(row);
            }
        }

        public void AddRow(HtmlTableRow row)
        {
            _Rows.Add(row);
        }

        public void AddRows(HtmlTableRow[] rows)
        {
            foreach (HtmlTableRow row in rows)
            {
                _Rows.Add(row);
            }
        }

        public void AddColumns(HtmlTableCell[] columns)
        {
            if (_Rows.Count == 0)
            {
                foreach (HtmlTableCell column in columns)
                {
                    HtmlTableRow row = new HtmlTableRow();
                    row.AddColumn(column);
                    _Rows.Add(row);
                }
            }
            else
            {
                for (int i = 0; i < _Rows.Count; i++)
                {
                    HtmlTableRow row = _Rows[i];
                    row.AddColumn(columns[i]);
                }
            }
        }

        public void AddColumnHeader(string header)
        {
            AddColumnHeader(new HtmlTableHeader(new HtmlText(header)));
        }

        public void AddColumnHeader(HtmlTableHeader header)
        {
            _Headers.Add(header);
            _HeaderInUse = true;
        }

        public override string ToString()
        {
            StringBuilder tag = new StringBuilder(GetStartTableTag());

            // Add the column headers.
            if (_HeaderInUse)
            {
                if (_Rows.Count > 0)
                {
                    int hdrSize = _Headers.Count;

                    foreach (HtmlTableRow row in _Rows)
                    {
                        // Verify that the table header size greater or equal to the number of columns in a row. 
                        if (hdrSize < row.ColumnCount) 
                        {
                            throw new Exception("header or row length not valid");
                        }
                    }
                }
                tag.Append(GetHeaderTag());
            }

            // Add the rows.
            foreach (HtmlTableRow row in _Rows)
            {
                tag.Append(row.ToString());
            }
            tag.Append(GetEndTableTag());

            return tag.ToString();
        }

        private string GetStartTableTag()
        {
            StringBuilder tag = new StringBuilder();       
            tag.Append("<table");

            if (_Alignment != HorizontalAlignment.NotSet)
            {
                tag.Append(" align=\"");
                tag.Append(_Alignment);
                tag.Append("\"");
            }
            if (_BorderWidth > 0)
            {
                tag.Append(" border=\"");
                tag.Append(_BorderWidth);
                tag.Append("\"");
            }
            if (_CellPadding >= 0)              
            {
                tag.Append(" cellpadding=\"");
                tag.Append(_CellPadding);
                tag.Append("\"");
            }
            if (_CellSpacing >= 0)              
            {
                tag.Append(" cellspacing=\"");
                tag.Append(_CellSpacing);
                tag.Append("\"");
            }
            if (_Width > 0)
            {
                tag.Append(" width=\"");
                tag.Append(_Width);

                if (_WidthPercent)
                    tag.Append("%");
                tag.Append("\"");
            }

            tag.Append(GetAttributeString());            

            tag.Append(">\n");

            if (_Caption != null)
                tag.Append(_Caption.ToString());

            return tag.ToString();    
        }

        private string GetEndTableTag()
        {
            return "</table>\n";
        }

        private string GetHeaderTag()
        {
            if (_Headers.Count == 0)
                return "";
            else
            {
                StringBuilder tag = new StringBuilder();
                tag.Append("<tr>\n");

                foreach (HtmlTableHeader header in _Headers)
                {
                    tag.Append(header.ToString());
                }

                tag.Append("</tr>\n");
                return tag.ToString();                  
            }
        }
    }
}
