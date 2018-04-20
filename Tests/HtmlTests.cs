using System;
using Xunit;

using CJO.Web.HTML;

namespace Tests
{
    public class HtmlTests
    {
        [Fact]
        public void CreateEmptyDocument()
        {
            HtmlDocument doc = new HtmlDocument();
            string tag = doc.ToString();
            Assert.Equal("<html>\n<body>\n</body>\n</html>\n", tag);
        }
    }
}
