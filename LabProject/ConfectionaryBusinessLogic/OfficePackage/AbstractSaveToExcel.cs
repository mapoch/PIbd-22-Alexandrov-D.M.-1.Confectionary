﻿using System;
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
        public void CreateReport(ExcelInfo info)
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
                    Text = pc.ComponentName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;

                foreach (var pastry in pc.Pastries)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = pastry.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBorder
                    });

                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = pastry.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBorder
                    });

                    rowIndex++;
                }

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = pc.TotalCount.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });
            }

            SaveExcel(info);
        }

        protected abstract void CreateExcel(ExcelInfo info);

        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        protected abstract void SaveExcel(ExcelInfo info);
    }
}
