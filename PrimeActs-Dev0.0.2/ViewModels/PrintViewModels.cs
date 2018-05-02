using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels
{
    public class PrintDocumentModel
    {
        public int? PageWidth { get; set; }
        public bool? RawPrinter { get; set; }
        public List<SectionModel> Sections { get; set; }
        public bool? Condensed { get; set; }
        public int? PageLength { get; set; }
        //public SectionModel Header { get; set; }
        //public SectionModel Body { get; set; }
        //public SectionModel Footer { get; set; }
    }

    public class SectionModel
    {
        public string Name { get; set; }
        public List<LineModel> Lines { get; set; }
    }

    public class LineModel : ICloneable
    {
        public List<LineItemModel> LineItems { get; set; }

        public int GetLineHeight(Graphics g, int maxWidth, string defaultFontName, int defaultFontSize, FontStyle defaultFontStyle)
        {
            int height = 0;
            foreach (var lineItem in LineItems)
            {
                var lineItemHeight = lineItem.GetLineItemHeight(g, maxWidth, defaultFontName, defaultFontSize, defaultFontStyle);
                if (lineItemHeight > height)
                    height = lineItemHeight;
            }
            return height;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class LineItemModel : ICloneable
    {
        public byte[] ImageData { get; set; }
        public string Text { get; set; }
        public int? Width { get; set; }
        public int? X { get; set; }

        public string FontName { get; set; }
        public int? FontSize { get; set; }
        public FontStyle? FontStyle { get; set; }

        public StringAlignment? Align { get;set; }

        private int? _lineItemHeight = null;
        public int GetLineItemHeight(Graphics g, int maxWidth, string defaultFontName, int defaultFontSize, FontStyle defaultFontStyle)
        {
            if (_lineItemHeight == null)
            {
                if (Image == null)
                {
                    int height = 0;
                    var font = new Font(FontName ?? defaultFontName, FontSize ?? defaultFontSize, FontStyle ?? defaultFontStyle);
                    if (font.Height > height)
                        height = font.Height;

                    // adjust height if need to wrap text
                    if (!string.IsNullOrEmpty(Text))
                    {
                        var heightForText = height;
                        var size = g.MeasureString(Text.Trim(), font);
                        var availableWidth = Width ?? maxWidth;
                        while (size.Width > availableWidth)
                        {
                            heightForText += font.Height;
                            size.Width -= availableWidth;
                        }

                        if (heightForText > height)
                            height = heightForText;
                    }

                    // assign calculated height to class variable
                    _lineItemHeight = height;
                }
                else
                {
                    _lineItemHeight = Image.Height;
                }
            }
            return _lineItemHeight.Value;
        }
        public int? Reverse { get; set; }
        public int? Invisible { get; set; }
        private Image _image = null;
        public Image Image
        {
            get
            {
                if (_image == null && ImageData != null)
                {
                    using (var ms = new MemoryStream(ImageData))
                    {
                        _image = Image.FromStream(ms);
                    }
                }
                return _image;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
