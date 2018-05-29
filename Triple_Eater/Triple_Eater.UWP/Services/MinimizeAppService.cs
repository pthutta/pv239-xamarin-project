using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;
using Triple_Eater.Services;
using Triple_Eater.UWP.Services;

[assembly: Xamarin.Forms.Dependency(typeof(MinimizeAppService))]

namespace Triple_Eater.UWP.Services
{
    public class MinimizeAppService : IMinimizeAppService
    {
        public async void Minimize()
        {
            var alert = new ContentDialog
            {
                Title = "Minimize",
                Content = "Do you want to minimize this application?",
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await alert.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                Console.WriteLine("Not supported by UWP.");
            }
        }
    }
}
