using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.Pages.RoleChoice;
using Triple_Eater.DataModels;
using Triple_Eater.Services;
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

	    protected override bool OnBackButtonPressed()
	    {
	        DependencyService.Get<IMinimizeAppService>().Minimize();
	        return true;
	    }

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            SetPlayerRoles();

            var nextPage = new NavigationPage(new RoleInfoInitPage31());
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }

	    public async void SetPlayerRoles()
	    {
	        Random rng = new Random();
	        var players = await App.Database.TryGetAllPlayersAsync();

            int playersCount = players.Count;
            for (int i = 0; i < playersCount; i++)
	        {
                int index = rng.Next(players.Count);
                var player = players[index];

	            var role = (i < 2) ? Role.Glutton : Role.Flatmate;
	            player.OriginalRole = role;
	            player.CurrentRole = role;

                players.RemoveAt(index);
	            App.Database.TryUpdatePlayerAsync(player);
	        }
        }
    }
}