using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Triple_Eater.Droid.Services;
using Triple_Eater.Services;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteConnectionStringFactory))]

namespace Triple_Eater.Droid.Services
{
    class SQLiteConnectionStringFactory : ISQLiteConnectionStringFactory
    {
        public string Create(string name)
        {
            var directory = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);

            return Path.Combine(directory, name);
        }
    }
}