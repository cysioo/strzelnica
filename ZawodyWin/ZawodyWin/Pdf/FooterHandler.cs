using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace ZawodyWin.Pdf
{
    public class FooterHandler : IEventHandler
    {
        protected PdfFormXObject placeholder;
        protected float side = 20;
        protected float x = 300;
        protected float y = 25;
        protected float space = 4.5f;
        protected float descent = 3;

        public FooterHandler()
        {
            placeholder = new PdfFormXObject(new Rectangle(0, 0, side, side));
        }

        public virtual void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdf = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();
            int pageNumber = pdf.GetPageNumber(page);
            Rectangle pageSize = page.GetPageSize();

            // Creates drawing canvas
            PdfCanvas pdfCanvas = new PdfCanvas(page);
            Canvas canvas = new Canvas(pdfCanvas, pageSize);

            Paragraph p = new Paragraph()
                .Add("Page ")
                .Add(pageNumber.ToString())
                .Add(" of");

            canvas.ShowTextAligned(p, x, y, TextAlignment.RIGHT);
            canvas.Close();

            // Create placeholder object to write number of pages
            pdfCanvas.AddXObjectAt(placeholder, x + space, y - descent);
            pdfCanvas.Release();
        }

        public void WriteTotal(PdfDocument pdf)
        {
            Canvas canvas = new Canvas(placeholder, pdf);
            canvas.ShowTextAligned(pdf.GetNumberOfPages().ToString(),
                0, descent, TextAlignment.LEFT);
            canvas.Close();
        }
    }
}
