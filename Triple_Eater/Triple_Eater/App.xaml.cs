using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Triple_Eater.Pages;
using Triple_Eater.Services;
using Xamarin.Forms;

namespace Triple_Eater
{
	public partial class App : Application
	{
	    public static TripleEaterDatabase Database { get; private set; }
	        = new TripleEaterDatabase("TodoList.sqlite", DependencyService.Get<ISQLiteConnectionStringFactory>());

        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		protected override async void OnStart ()
		{
		    //await Database.DropDatabase();
            await Database.InitDatabase();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
