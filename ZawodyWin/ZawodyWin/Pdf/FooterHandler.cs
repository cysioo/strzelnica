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
        protected float x = 40;
        protected float y = 25;

        public virtual void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfPage page = docEvent.GetPage();
            Rectangle pageSize = page.GetPageSize();

            // Creates drawing canvas
            PdfCanvas pdfCanvas = new PdfCanvas(page);
            Canvas canvas = new Canvas(pdfCanvas, pageSize);
            canvas.SetFontSize(8);
            Paragraph p = new Paragraph()
                .Add("Komunikat klasyfikacyjny {tournament.name} {tournament.date}\n")
                .Add("Przewodniczący Komisji RTS: {referee.Name} {referee.Surname}, sędzia kl. {referee.Class}");

            canvas.ShowTextAligned(p, x, y, TextAlignment.LEFT);
            canvas.Close();
        }
    }
}
