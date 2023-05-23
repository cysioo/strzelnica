using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZawodyWin.DataModels;

namespace ZawodyWin.Pdf
{
    public class PdfFactory
    {
        //public string CreateTournamentPdf(long tournamentId)
        //{
        //    // Cre
        //}

        //private string CreateTournamentHtml(Tournament tournament)
        //{
        //    // get 
        //}

        public void CreatePdf(string html, string savePath)
        {
            var pdfStream = File.Create(savePath);
            ConverterProperties converterProperties = new ConverterProperties();
            HtmlConverter.ConvertToPdf(html, pdfStream, converterProperties);
        }
    }
}
