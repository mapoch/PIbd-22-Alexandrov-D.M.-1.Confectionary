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
        public void CreateDoc(WordInfo info)
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

        protected abstract void CreateWord(WordInfo info);
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void SaveWord(WordInfo info);
    }
}
