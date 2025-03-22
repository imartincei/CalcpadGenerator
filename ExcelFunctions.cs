#nullable enable

using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcpadGenerator
{
    internal partial class ExcelFunctions
    {
        internal static Tuple<string, string> GetCellRange(string range)
        {
            string sheetName;
            string rangeName;
            // Regex for multi-cell range with a sheet name
            if (ExcelRangeWithSheetName().Match(range).Success)
            {
                sheetName = range.Split("!")[0];
                rangeName = range.Split("!").Last();
            }
            // Regex for multi-cell range without a sheet name, uses the first sheet
            else if (ExcelRangeWithoutSheetName().Match(range).Success)
            {
                sheetName = "";
                rangeName = range;
            }
            else
            {
                throw new ArgumentException("Invalid range format");
            }
            return new Tuple<string, string>(sheetName, rangeName);
        }

        public static List<List<object>> GetLookupDataFromRange(string filepath, string cellRange)
        {
            using var workbook = new XLWorkbook(filepath);
            Tuple<string, string> rangeObject = GetCellRange(cellRange);
            string sheetName = rangeObject.Item1;
            string rangeName = rangeObject.Item2;
            // Uses first sheet if not specified
            if (sheetName == "")
            {
                sheetName = workbook.Worksheets.First().Name;
            }
            var worksheet = workbook.Worksheet(sheetName);
            var range = worksheet.Range(rangeName);
            int rowCount = range.RowCount();
            int columnCount = range.ColumnCount();
            List<List<object>> data = [];

            for (int i = 0; i < rowCount; i++)
            {
                List<object> row = [];
                var cellValue = new object();
                for (int j = 0; j < columnCount; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        cellValue = range.Cell(i + 1, j + 1).GetValue<string>();
                    }
                    else
                    {
                        cellValue = range.Cell(i + 1, j + 1).GetValue<double>();
                    }
                    row.Add(cellValue);
                }
                data.Add(row);
            }
            return data;
        }

        [System.Text.RegularExpressions.GeneratedRegex(@"([^'!]+)!([A-Za-z]+[0-9]+):([A-Za-z]+[0-9]+)")]
        private static partial System.Text.RegularExpressions.Regex ExcelRangeWithSheetName();
        [System.Text.RegularExpressions.GeneratedRegex(@"([A-Z]+)(\d+):([A-Z]+)(\d+)")]
        private static partial System.Text.RegularExpressions.Regex ExcelRangeWithoutSheetName();
    }
}