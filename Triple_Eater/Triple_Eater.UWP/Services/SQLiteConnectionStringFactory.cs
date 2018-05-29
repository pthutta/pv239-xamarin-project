using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Triple_Eater.Services;
using Triple_Eater.UWP.Services;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteConnectionStringFactory))]

namespace Triple_Eater.UWP.Services
{
    class SQLiteConnectionStringFactory : ISQLiteConnectionStringFactory
    {
        public string Create(string name) => Path.Combine(ApplicationData.Current.LocalFolder.Path, name);
    }
}
