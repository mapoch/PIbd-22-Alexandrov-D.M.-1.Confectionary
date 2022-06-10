using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryBusinessLogic.OfficePackage.HelperEnums;
using ConfectionaryBusinessLogic.OfficePackage.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace ConfectionaryBusinessLogic.OfficePackage.Implements
{
    public class SaveToWord : AbstractSaveToWord
    {
        private WordprocessingDocument wordDocument;
        private Body docBody;

        private static JustificationValues GetJustificationValues(WordJustificationType type)
        {
            return type switch
            {
                WordJustificationType.Both => JustificationValues.Both,
                WordJustificationType.Center => JustificationValues.Center,
                _ => JustificationValues.Left,
            };
        }

        private static SectionProperties CreateSectionProperties()
        {
            var properties = new SectionProperties();
            var pageSize = new PageSize { Orient = PageOrientationValues.Portrait };
            properties.AppendChild(pageSize);

            return properties;
        }

        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                var properties = new ParagraphProperties();
                properties.AppendChild(new Justification() 
                    { Val = GetJustificationValues(paragraphProperties.JustificationType) });
                properties.AppendChild(new SpacingBetweenLines { LineRule = LineSpacingRuleValues.Auto });
                properties.AppendChild(new Indentation());

                var paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
                }
                properties.AppendChild(paragraphMarkRunProperties);

                return properties;
            }
            return null;
        }

        protected override void CreateWord(WordInfoAbstract info)
        {
            wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document);
            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            docBody = mainPart.Document.AppendChild(new Body());
        }

        protected override void CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                var docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));

                foreach (var run in paragraph.Texts)
                {
                    var docRun = new Run();
                    var properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);

                    docRun.AppendChild(new Text 
                        { Text = run.Item1, Space = SpaceProcessingModeValues.Preserve });
                    docParagraph.AppendChild(docRun);
                }
                docBody.AppendChild(docParagraph);
            }
        }

        protected override void CreateTable(WordTable table)
        {
            if (table != null)
            {
                Table docTable = new Table();

                var tableProps = new TableProperties();
                tableProps.AppendChild(new TableLayout { Type = TableLayoutValues.Fixed });
                tableProps.AppendChild(new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }));
                tableProps.AppendChild(new TableWidth { Type = TableWidthUnitValues.Auto });
                docTable.AppendChild(tableProps);

                TableGrid tableGrid = new TableGrid();
                for (int j = 0; j < table.Columns.Count; ++j)
                {
                    tableGrid.AppendChild(new GridColumn() { Width = "3413" });
                }
                docTable.AppendChild(tableGrid);

                TableRow clmns = new TableRow();
                for (int j = 0; j < table.Columns.Count; ++j)
                {
                    var docParagraph = new Paragraph();

                    var parProps = new ParagraphProperties();
                    parProps.AppendChild(new Justification() { Val = JustificationValues.Center });
                    parProps.AppendChild(new SpacingBetweenLines { Before = "120", After = "0" });
                    docParagraph.AppendChild(parProps);

                    var docRun = new Run();

                    var runProps = new RunProperties();
                    runProps.AppendChild(new RunFonts() { Ascii = "Times New Roman", ComplexScript = "Times New Roman", HighAnsi = "Times New Roman" });
                    runProps.AppendChild(new FontSize { Val = "22" });
                    runProps.AppendChild(new Bold());

                    docRun.AppendChild(runProps);
                    docRun.AppendChild(new Text { Text = table.Columns[j].ToString(), Space = SpaceProcessingModeValues.Preserve });
                    docParagraph.AppendChild(docRun);

                    TableCell docCell = new TableCell();
                    docCell.AppendChild(docParagraph);
                    clmns.AppendChild(docCell);
                }
                docTable.AppendChild(clmns);

                for (int i = 0; i < table.Texts.Count; ++i)
                {
                    TableRow docRow = new TableRow();

                    for (int j = 0; j < table.Columns.Count; ++j)
                    {
                        var docParagraph = new Paragraph();

                        var parProps = new ParagraphProperties();
                        parProps.AppendChild(new Justification() { Val = JustificationValues.Center });
                        parProps.AppendChild(new SpacingBetweenLines { Before = "120", After = "0" });
                        docParagraph.AppendChild(parProps);

                        var docRun = new Run();

                        var runProps = new RunProperties();
                        runProps.AppendChild(new RunFonts() { Ascii = "Times New Roman", ComplexScript = "Times New Roman", HighAnsi = "Times New Roman" });
                        runProps.AppendChild(new FontSize { Val = "22" });

                        docRun.AppendChild(runProps);
                        docRun.AppendChild(new Text { Text = table.Texts[i].GetValue(j).ToString(), Space = SpaceProcessingModeValues.Preserve });
                        docParagraph.AppendChild(docRun);

                        TableCell docCell = new TableCell();
                        docCell.AppendChild(docParagraph);
                        docRow.AppendChild(docCell);
                    }
                    docTable.AppendChild(docRow);
                }

                docBody.AppendChild(docTable);
            }
        }

        protected override void SaveWord(WordInfoAbstract info)
        {
            docBody.AppendChild(CreateSectionProperties());
            wordDocument.MainDocumentPart.Document.Save();
            wordDocument.Close();
        }
    }
}
