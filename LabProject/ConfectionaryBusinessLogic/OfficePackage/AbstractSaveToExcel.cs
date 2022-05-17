using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryBusinessLogic.OfficePackage.HelperEnums;
using ConfectionaryBusinessLogic.OfficePackage.HelperModels;

namespace ConfectionaryBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        public void CreateReport(ExcelInfoPastryComponent info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;
            foreach (var pc in info.PastryComponents)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = pc.PastryName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;

                foreach (var component in pc.Components)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = component.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBorder
                    });

                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = component.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBorder
                    });

                    rowIndex++;
                }

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = "Итого:",
                    StyleInfo = ExcelStyleInfoType.Text
                });

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = pc.TotalCount.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;
            }

            SaveExcel(info);
        }

        public void CreateReport(ExcelInfoWarehouseComponent info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;
            foreach (var pc in info.WarehouseComponents)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = pc.WarehouseName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;

                foreach (var component in pc.Components)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = component.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBorder
                    });

                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = component.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBorder
                    });

                    rowIndex++;
                }

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = "Итого:",
                    StyleInfo = ExcelStyleInfoType.Text
                });

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = pc.TotalCount.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;
            }

            SaveExcel(info);
        }

        protected abstract void CreateExcel(ExcelInfoAbstract info);

        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        protected abstract void SaveExcel(ExcelInfoAbstract info);
    }
}
