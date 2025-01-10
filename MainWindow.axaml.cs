using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Markup.Xaml;
using System.Text;
using System;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using DocumentFormat.OpenXml;
using System.IO;

namespace CalcpadGenerator
{
    public partial class MainWindow : Window
    {
        private readonly TextBox _outputTextBox;
        private readonly Button _ExcelFileOpener;
        private string? _filePath;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _outputTextBox = this.FindControl<TextBox>("OutputTextBox") ?? throw new Exception("Control not found");
            _ExcelFileOpener = this.FindControl<Button>("ExcelFileOpener") ?? throw new Exception("Control not found");
            _ExcelFileOpener.Click += OpenExcelFileButton_Clicked;

            string filePath = "C:\\Users\\IsaiahMartin\\source\\repos\\CalcpadGenerator\\Test.xlsx";
            string sheetName = "Sheet1";
            string startCell = "A16";
            string endCell = "C18";

            double[,] data = ExcelReader.GetDataFromRange(filePath, sheetName, startCell, endCell);
            DisplayDataInTextBox(data);
        }

        private async void OpenExcelFileButton_Clicked(object sender, RoutedEventArgs args)
        {
            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(this) ?? throw new Exception();

            // Start async operation to open the dialog.
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Excel File",
                AllowMultiple = false,
                FileTypeFilter = [ExcelFileType]
            });

            if (files.Count >= 1)
            {
                _filePath = files[0].Name;
            }
            LoadAndDisplayData();
        }

        public static FilePickerFileType ExcelFileType { get; } = new("Excel Files")
        {
            Patterns = [ "*.xlsx", "*.xls" ]
        };

        private void LoadAndDisplayData()
        {
            if (string.IsNullOrEmpty(_filePath)) return;

            string sheetName = "Sheet1";
            string startCell = "A16";
            string endCell = "C18";

            double[,] data = ExcelReader.GetDataFromRange(_filePath, sheetName, startCell, endCell);
            DisplayDataInTextBox(data);
        }

        private void DisplayDataInTextBox(double[,] data)
        {
            StringBuilder sb = new();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    sb.Append(data[i, j] + " ");
                }
                sb.AppendLine();
            }
            _outputTextBox.Text = sb.ToString();
        }
    }
}