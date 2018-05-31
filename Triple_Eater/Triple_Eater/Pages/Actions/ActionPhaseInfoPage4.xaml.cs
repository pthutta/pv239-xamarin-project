 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Triple_Eater.DataModels;
 using Triple_Eater.Services;
 using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages.Actions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActionPhaseInfoPage4 : ContentPage
	{
		public ActionPhaseInfoPage4()
		{
			InitializeComponent();
		}

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            SetPlayerActions();

            var nextPage = new NavigationPage(new ActionPublicPage5());
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }

	    protected override bool OnBackButtonPressed()
	    {
	        DependencyService.Get<IMinimizeAppService>().Minimize();
	        return true;
	    }

	    public async void SetPlayerActions()
	    {
	        Random rng = new Random();
	        var players = await App.Database.TryGetAllPlayersAsync();
            var operations = new List<Operation>
            {
                Operation.Confession,
                Operation.ForgetfulPartner,
                Operation.AnonymousTip,
                Operation.NeighborhoodGossip,
                Operation.NightPhotographs
            };

            foreach (var player in players)
            {
                var operation = operations[rng.Next(operations.Count)];
                player.Operation = operation;
                operations.Remove(operation);
                await App.Database.TryUpdatePlayerAsync(player);
            }
	    }
    }
}