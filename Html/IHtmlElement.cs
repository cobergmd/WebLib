using System;

namespace CJO.Web.HTML
{
    public enum HorizontalAlignment
    {
        NotSet,
        Left,
        Right,
        Center,
        Justify
    }

    public enum VerticalAlignment
    {
        NotSet,
        Top,
        Middle,
        Bottom,
        Baseline
    }

    public enum ImageAlignment
    {
        Left,
        Right,
        Top,
        TextTop,
        Middle,
        AbsMiddle,
        Bottom,
        AbsBottom,
        Baseline
    }

    public interface IHtmlElement
    {
        void AddAttribute(string name, string value);
        string GetAttributeString();
        string ToString();
    }
}


