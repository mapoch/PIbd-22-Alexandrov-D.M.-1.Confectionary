using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryBusinessLogic.OfficePackage.HelperEnums;
using ConfectionaryBusinessLogic.OfficePackage.HelperModels;

namespace ConfectionaryBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfoOrders info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph { Text = info.Title, Style = "NormalTitle" });
            CreateParagraph(new PdfParagraph { Text = $"с{ info.DateFrom.ToShortDateString() } " +
                $"по { info.DateTo.ToShortDateString() }", Style = "Normal" });
            CreateTable(new List<string> { "3cm", "6cm", "3cm", "2cm", "3cm" });
            CreateRow(new PdfRowParameters { Texts = new List<string> 
                { "Дата заказа", "Изделие", "Количество", "Сумма", "Статус" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var order in info.Orders)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { order.DateCreate.ToShortDateString(), order.PastryName, 
                        order.Count.ToString(), order.Sum.ToString(), order.Status.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }

        public void CreateDoc(PdfInfoDates info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph { Text = info.Title, Style = "NormalTitle" });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с{ info.DateFrom.ToShortDateString() } " +
                $"по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "3cm", "6cm", "3cm", "2cm", "3cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string>
                { "Дата", "Количество", "Сумма" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var date in info.Dates)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { date.Date.ToShortDateString(), date.Count.ToString(), date.Sum.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);

        }

        protected abstract void CreatePdf(PdfInfoAbstract info);

        protected abstract void CreateParagraph(PdfParagraph paragraph);

        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        protected abstract void SavePdf(PdfInfoAbstract info);
    }
}
