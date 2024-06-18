using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationExport.utils
{
    public class PdfSharpUtilities
    {
        private double topMargin = 0;
        private double leftMargin = 0;
        private double rightMargin = 0;
        private double bottomMargin = 0;
        private double cm;

        private PdfDocument document;
        private PdfPage page;
        private XGraphics gfx;
        private XFont font;
        private XPen pen;
        private String outputPath;

        public double CM => cm;
        public XGraphics Graphics => gfx;

        public PdfSharpUtilities(String argOutputpath, double marginTop = 2.5, double marginBottom = 2.5, double marginRight = 3.0, double marginLeft = 3.0, Boolean argAddMarginGuides = false)
        {
            this.outputPath = argOutputpath;
            this.document = new PdfDocument();

            this.page = document.AddPage();
            this.page.Size = PageSize.Letter;

            this.cm = new Interpolation().linearInterpolation(0, 0, 27.9, page.Height, 1);

            this.gfx = XGraphics.FromPdfPage(page);

            this.font = new XFont("Arial", 12, XFontStyleEx.Bold);
            this.pen = new XPen(XColors.Black, 0.5);

            topMargin = marginTop * cm;
            leftMargin = marginLeft * cm;
            bottomMargin = page.Height - (marginBottom * cm);
            rightMargin = page.Width - (marginRight * cm);

            if (argAddMarginGuides)
            {
                gfx.DrawString("+", font, XBrushes.Black, rightMargin, topMargin);
                gfx.DrawString("+", font, XBrushes.Black, leftMargin, topMargin);
                gfx.DrawString("+", font, XBrushes.Black, rightMargin, bottomMargin);
                gfx.DrawString("+", font, XBrushes.Black, leftMargin, bottomMargin);
            }
        }

        public void drawTable(double initialPosX, double initialPosY, double width, double height, XBrush xbrush, List<String[]> contents = null)
        {
            drawSquare(new DPoint(initialPosX, initialPosY), width, height, xbrush);

            if (contents == null)
            {
                contents = new List<String[]>();

                contents.Add(new string[] { "Type", "Size", "Weight", "Stock", "Tax", "Price" });
                contents.Add(new string[] { "Obo", "1", "45", "56", "16.00", "6.50" });
                contents.Add(new string[] { "Crotolamo\nTest", "2", "72", "63", "16.00", "19.00" });
            }

            int columns = contents[0].Length;
            int rows = contents.Count;

            double distanceBetweenRows = height / rows;
            double distanceBetweenColumns = width / columns;

            /*******************************************************************/
            // Draw the row lines
            /*******************************************************************/

            DPoint pointA = new DPoint(initialPosX, initialPosY);
            DPoint pointB = new DPoint(initialPosX + width, initialPosY);

            for (int i = 0; i <= rows; i++)
            {
                drawLine(pointA, pointB);

                pointA.y = pointA.y + distanceBetweenRows;
                pointB.y = pointB.y + distanceBetweenRows;
            }

            /*******************************************************************/
            // Draw the column lines
            /*******************************************************************/

            pointA = new DPoint(initialPosX, initialPosY);
            pointB = new DPoint(initialPosX, initialPosY + height);

            for (int i = 0; i <= columns; i++)
            {
                drawLine(pointA, pointB);

                pointA.x = pointA.x + distanceBetweenColumns;
                pointB.x = pointB.x + distanceBetweenColumns;
            }

            /*******************************************************************/
            // Insert text corresponding to each cell
            /*******************************************************************/

            pointA = new DPoint(initialPosX, initialPosY);

            foreach (String[] rowDataArray in contents)
            {
                foreach (String cellText in rowDataArray)
                {                    
                    this.gfx.DrawString(cellText, this.font, XBrushes.Black, new XRect(leftMargin + (pointA.x * cm), topMargin + (pointA.y * cm), distanceBetweenColumns * cm, distanceBetweenRows * cm), XStringFormats.Center);
                    pointA.x = pointA.x + distanceBetweenColumns;
                }

                pointA.x = initialPosX;
                pointA.y = pointA.y + distanceBetweenRows;
            }
        }

        public void addText(String text, DPoint xyStartingPosition, int size = 12)
        {
            var font = new XFont("Arial", size, XFontStyleEx.Regular);
            this.gfx.DrawString(text, font, XBrushes.Black, leftMargin + (xyStartingPosition.x * cm), topMargin + (xyStartingPosition.y * cm));
        }

        public void drawSquare(DPoint xyStartingPosition, double width, double height, XBrush xbrush)
        {
            //this.gfx.DrawRectangle(xbrush, new XRect(leftMargin + (xyStartingPosition.x * cm), topMargin + (xyStartingPosition.y * cm), (width * cm), (height * cm)));

            XPoint[] points = [
                new(leftMargin + ((xyStartingPosition.x) * cm), topMargin + (xyStartingPosition.y * cm)),
                new(leftMargin + ((xyStartingPosition.x + width / 2) * cm), topMargin + ((xyStartingPosition.y + height / 2) * cm)),
                new(leftMargin + ((xyStartingPosition.x + width) * cm), topMargin + ((xyStartingPosition.y) * cm)),
            ];

            this.gfx.DrawPolygon(xbrush, points, XFillMode.Winding);
        }

        public void drawLine(DPoint fromXyPosition, DPoint toXyPosition)
        {
            this.gfx.DrawLine(this.pen, leftMargin + (fromXyPosition.x * cm), topMargin + (fromXyPosition.y * cm), leftMargin + (toXyPosition.x * cm), topMargin + (toXyPosition.y * cm));
        }

        public void saveAndShow(Boolean argShowAfterSaving = true)
        {
            document.Save(this.outputPath);

            if (argShowAfterSaving)
            {
                PdfFileUtility.ShowDocument(this.outputPath);
            }
        }
    }
}
