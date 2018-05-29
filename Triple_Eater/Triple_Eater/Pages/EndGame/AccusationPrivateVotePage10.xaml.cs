using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages.EndGame
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccusationPrivateVotePage10 : ContentPage
	{
		public AccusationPrivateVotePage10 ()
		{
			InitializeComponent ();
		}

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new AccusationResultsPage11());
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }
    }
}