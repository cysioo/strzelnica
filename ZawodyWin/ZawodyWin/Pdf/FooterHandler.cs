using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using ZawodyWin.Pdf.DataModels;

namespace ZawodyWin.Pdf
{
    public class FooterHandler : IEventHandler
    {
        protected float x = 40;
        protected float y = 25;

        public FooterHandler(FooterModel model)
        {
            Model = model;
        }

        public FooterModel Model { get; set; }

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
                .Add($"Komunikat klasyfikacyjny {Model.TournamentName} {Model.TournamentDate}\n")
                .Add($"Przewodniczący Komisji RTS: {Model.RefereeName} {Model.RefereeSurname}, sędzia kl. {Model.RefereeClass}");

            canvas.ShowTextAligned(p, x, y, TextAlignment.LEFT);
            canvas.Close();
        }
    }
}
