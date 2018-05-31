using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class AccusationPrivateVotePage10 : ContentPage
	{
        private Player _currentPlayer;
        private ObservableCollection<Player> _players = new ObservableCollection<Player>();

        public ObservableCollection<Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                OnPropertyChanged();
            }
        }
        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                _currentPlayer = value;
                OnPropertyChanged();
            }
        }
        public AccusationPrivateVotePage10(Player player)
        {
            InitializeComponent();
            BindingContext = this;
            CurrentPlayer = player;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Players = new ObservableCollection<Player>((
                await App.Database.TryGetAllPlayersAsync()).Where(player => player.Name != CurrentPlayer.Name)
            );
        }

        public async void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var selectedPlayer = Players.FirstOrDefault(player => player.Name == ((Button)sender).Text);
            selectedPlayer.AccusationCounter++;
            await App.Database.TryUpdatePlayerAsync(selectedPlayer);

            NavigationPage nextPage;
            int remainingPlayers = Players.Count(x => !x.WasProcessed);
            if (remainingPlayers != 0)
            {
                nextPage = new NavigationPage(new AccusationPublicPage9());
            }
            else
            {
                nextPage = new NavigationPage(new AccusationResultsPage11());
                await ResetWasProcessedFlag();
            }
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }

        protected override bool OnBackButtonPressed()
	    {
	        DependencyService.Get<IMinimizeAppService>().Minimize();
	        return true;
	    }

        private async Task ResetWasProcessedFlag()
        {
            foreach (var player in Players)
            {
                player.WasProcessed = false;
                await App.Database.TryUpdatePlayerAsync(player);
            }
        }
    }
}