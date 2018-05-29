using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Triple_Eater.Droid.Services;
using Triple_Eater.Services;

[assembly: Xamarin.Forms.Dependency(typeof(MinimizeAppService))]

namespace Triple_Eater.Droid.Services
{
    public class MinimizeAppService : IMinimizeAppService
    {
        public void Minimize()
        {
            var alert = new AlertDialog.Builder((Activity)Application.Context);
            alert.SetTitle("Minimize");
            alert.SetMessage("Do you want to minimize this application?");
            alert.SetPositiveButton("Yes", (sender, args) =>
            {
                Intent main = new Intent(Intent.ActionMain);
                main.AddCategory(Intent.CategoryHome);
                main.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(main);
            });
            alert.SetNegativeButton("No", (sender, args) => { });

            alert.Create().Show();
        }
    }
}