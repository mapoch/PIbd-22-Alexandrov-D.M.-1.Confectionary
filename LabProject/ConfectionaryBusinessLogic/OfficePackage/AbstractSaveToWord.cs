using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryBusinessLogic.OfficePackage.HelperEnums;
using ConfectionaryBusinessLogic.OfficePackage.HelperModels;

namespace ConfectionaryBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfoPastries info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>()
                {
                    (info.Title, new WordTextProperties {Bold = true, Size = "24",})
                },
                TextProperties = new WordTextProperties
                { Size = "24", JustificationType = WordJustificationType.Center }
            });

            foreach (var pastry in info.Pastries)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)>
                        { (pastry.PastryName, new WordTextProperties {  Bold = true, Size = "24", }),
                            (": цена - " + pastry.Price + "руб.", new WordTextProperties { Size = "24"})},
                    TextProperties = new WordTextProperties
                    { Size = "24", JustificationType = WordJustificationType.Both }
                });
            }

            SaveWord(info);
        }

        public void CreateDoc(WordInfoWarehouses info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>()
                {
                    (info.Title, new WordTextProperties {Bold = true, Size = "24",})
                },
                TextProperties = new WordTextProperties
                { Size = "24", JustificationType = WordJustificationType.Center }
            });

            List<string[]> texts = new List<string[]>();
            foreach (var warehouse in info.Warehouses)
            {
                texts.Add(new string[] { warehouse.Name, warehouse.Manager, warehouse.DateCreate.ToString() });
            }

            CreateTable(new WordTable
            {
                ColumnsProps = new WordTextProperties { Bold = true, Size = "20" },
                Columns = new List<string>()
                {
                    "Название", "Ответственный", "Дата создания"
                },
                Texts = texts
            });

            SaveWord(info);
        }

        protected abstract void CreateWord(WordInfoAbstract info);
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void CreateTable(WordTable table);
        protected abstract void SaveWord(WordInfoAbstract info);
    }
}
