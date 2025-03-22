using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Markup.Xaml;
using System.Text;
using System;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.IO;
using System.Collections.Generic;

namespace CalcpadGenerator
{
    public partial class MainWindow : Window
    {
        private readonly TextBox _outputTextBox;
        private readonly TextBox _ExcelRange;
        private readonly TextBox _ClassName;
        private readonly TextBox _DropdownName;
        private readonly Button _ExcelFileOpener;

        public MainWindow()
        {
            InitializeComponent();
            #if DEBUG
            this.AttachDevTools();
            #endif
            _outputTextBox = this.FindControl<TextBox>("OutputTextBox") ?? throw new Exception("Control not found");
            _ExcelRange = this.FindControl<TextBox>("ExcelRange") ?? throw new Exception("Control not found");
            _ClassName = this.FindControl<TextBox>("ClassName") ?? throw new Exception("Control not found");
            _DropdownName = this.FindControl<TextBox>("DropdownName") ?? throw new Exception("Control not found");
            _ExcelFileOpener = this.FindControl<Button>("ExcelFileOpener") ?? throw new Exception("Control not found");
            _ExcelFileOpener.Click += OpenExcelFileButton_Clicked;
        }

        // UI Events

        private async void OpenExcelFileButton_Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                string cellRange = _ExcelRange.Text ?? throw new Exception("Please insert Excel range");
                string filepath = await UIHelper.FilePicker(this, "Open Excel File", UIHelper.ExcelFileType);
                if (filepath == "" || cellRange == null)
                {
                    throw new Exception("Missing Range Text");
                }
                else
                {
                    List<List<object>> data = ExcelFunctions.GetLookupDataFromRange(filepath, cellRange);
                    _outputTextBox.Text = CodeBuilder.LookupDropdown(data);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowErrorBox($"Error: {ex.Message}");
            }
        }
    }
}