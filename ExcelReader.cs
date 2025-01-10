using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcpadGenerator
{
    internal class ExcelReader
    {
        public static double[,] GetDataFromRange(string filePath, string sheetName, string startCell, string endCell)
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(sheetName);
            var range = worksheet.Range(startCell + ":" + endCell);
            int rowCount = range.RowCount();
            int columnCount = range.ColumnCount();
            double[,] data = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var cellValue = range.Cell(i + 1, j + 1).GetValue<double>();
                    data[i, j] = cellValue;
                }
            }

            return data;
        }
    }
}
