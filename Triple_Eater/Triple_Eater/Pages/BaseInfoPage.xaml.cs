using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.Pages.RoleChoice;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseInfoPage : ContentPage
	{
		public BaseInfoPage ()
		{
			InitializeComponent ();
        }

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new RoleInfoInitPage31());
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }
    }
}