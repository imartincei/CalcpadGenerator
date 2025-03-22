using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcpadGenerator
{
    internal class UIHelper
    {
        async internal static Task<string> FilePicker(Avalonia.Visual mainWindow, string title, FilePickerFileType fileType, bool multiple=false)
        {
            try
            {
                // Get top level from the current control. Alternatively, you can use Window reference instead.
                var topLevel = TopLevel.GetTopLevel(mainWindow) ?? throw new Exception();

                // Start async operation to open the dialog.
                var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = title,
                    AllowMultiple = multiple,
                    FileTypeFilter = [fileType]
                });

                if (files.Count >= 1)
                {
                    return files[0].TryGetLocalPath() ?? throw new Exception();
                }
                else
                {
                    return "";
                }                
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowErrorBox($"Error: {ex.Message}");
                return "";
            }
        }
        internal static FilePickerFileType ExcelFileType { get; } = new("Excel Files")
        {
            Patterns = ["*.xlsx", "*.xlsm"]
        };
    }
}
