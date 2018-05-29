using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages.RoleChoice
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoleInfoInitPage31 : ContentPage
    {
		public RoleInfoInitPage31 ()
		{
			InitializeComponent ();
        }

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new RoleInfoPage32());
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }
    }
}