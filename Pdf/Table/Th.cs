using PdfSharp.Drawing;
using QuotationExport.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationExport.Pdf.Table
{
    public class Th
    {
        public String Text { get; set; }
        public XFontStyleEx Style { get; set; }
        public XFont Font { get; set; }
        public XBrush Brush { get; set; }
        public XRect Layout { get; set; }
        public XStringFormat Format { get; set; }


        public Th(String text, DPoint loc, int fontSize = 12)
        {
            Text = text;
            Font = new XFont("Arial", fontSize);
            Layout = new XRect(loc.x, loc.y, text.Length * fontSize, fontSize + 6);
            Format = new XStringFormat();
            Format.LineAlignment = XLineAlignment.Near;
            Format.Alignment = XStringAlignment.Near;
            Brush = XBrushes.Black;
        }

        public void Render(XGraphics gfx)
        {
            gfx.DrawString(Text, Font, Brush, Layout, Format);
        }
    }
}
