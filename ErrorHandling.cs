using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcpadGenerator
{
    internal class ErrorHandling
    {
        async public static void ShowErrorBox(string error)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", error, ButtonEnum.Ok);
            await box.ShowAsync();
        }
    }
}
