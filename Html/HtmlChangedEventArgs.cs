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
    public class HtmlChangedEventArgs : EventArgs
    {
        public IHtmlElement Element { get; set; }
        private string OriginalValue { get; set; }
        private string NewValue { get; set; }

        public HtmlChangedEventArgs(IHtmlElement element, string originalValue, string newValue)
        {
            Element = element;
            OriginalValue = originalValue;
            NewValue = newValue;
        }
    }
}
