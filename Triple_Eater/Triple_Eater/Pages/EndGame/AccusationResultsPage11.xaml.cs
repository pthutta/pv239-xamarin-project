using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.DataModels;
using Triple_Eater.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages.EndGame
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccusationResultsPage11 : ContentPage
    {
        private Player _imprisonedPlayer;
        public Player ImprisonedPlayer
        {
            get => _imprisonedPlayer;
            set
            {
                _imprisonedPlayer = value;
                OnPropertyChanged();
            }
        }
        public AccusationResultsPage11()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var orderedPlayers = (await App.Database.TryGetAllPlayersAsync()).OrderByDescending(x => x.AccusationCounter);
            ImprisonedPlayer = orderedPlayers.ElementAt(0);
            if (ImprisonedPlayer.AccusationCounter == orderedPlayers.ElementAt(1).AccusationCounter)
            {
                ImprisonedPlayer = null;
                ImprisonedPlayerLabel.Text = "Nobody was imprisoned.";
                WinnerTeamLabel.Text = "Gluttons win!";
            }
            else
            {
                ImprisonedPlayerLabel.Text = ImprisonedPlayer.Name + " was imprisoned";
                if (ImprisonedPlayer.CurrentRole == Role.Glutton)
                {
                    WinnerTeamLabel.Text = "Flatmates win!";
                }
                if (ImprisonedPlayer.CurrentRole == Role.Flatmate)
                {
                    WinnerTeamLabel.Text = "Gluttons win!";
                }
                ImprisonedPlayerLabel.Text += Environment.NewLine + " they were " + ImprisonedPlayer.CurrentRole.ToString().ToLower() + '.';
            }
        }

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new GameResultsPage12());
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }

	    protected override bool OnBackButtonPressed()
	    {
	        DependencyService.Get<IMinimizeAppService>().Minimize();
	        return true;
	    }
    }
}