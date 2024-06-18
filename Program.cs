using PdfSharp.Drawing;
using QuotationExport.Pdf.Table;
using QuotationExport.utils;



PdfSharpUtilities pdf = new PdfSharpUtilities("test.pdf", marginTop: 1, marginBottom: 1, marginLeft: 1, marginRight: 1, argAddMarginGuides: true);

pdf.drawSquare(new DPoint(0, 0), 3, 2, XBrushes.Purple);

pdf.addText("Username", new DPoint(0, 4.5), 16);

pdf.addText("Invoice", new DPoint(12.15, 1.5), 14);

pdf.addText("Account: 69696969", new DPoint(0, 6));

pdf.addText("Period: 2022-11", new DPoint(0, 7));

pdf.addText("E-mail: mail@gmail.com", new DPoint(0, 8));

pdf.addText("Inventory:", new DPoint(0, 10));

//Example table: to fill with example data leave contents = null
pdf.drawTable(3, 11, 20, 2, XBrushes.LightGray, null);

var cell = new Th("Test", new DPoint(pdf.CM * 3, pdf.CM * 3));
cell.Render(pdf.Graphics);

pdf.saveAndShow();