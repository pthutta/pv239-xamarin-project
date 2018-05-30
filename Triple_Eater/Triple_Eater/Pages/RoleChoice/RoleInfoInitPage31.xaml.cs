using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.Services;
using Triple_Eater.DataModels;
using Triple_Eater.Pages.Actions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages.RoleChoice
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoleInfoInitPage31 : ContentPage
    {

        private ObservableCollection<Player> _players = new ObservableCollection<Player>();
        private Player _currentPlayer;

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

        public RoleInfoInitPage31()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            Players = new ObservableCollection<Player>(
                    await App.Database.TryGetAllPlayersAsync()
            );
            base.OnAppearing();

            var remainingPlayers = Players.Where((x) => !x.WasProcessed).ToList();
            var random = new Random();
            CurrentPlayer = remainingPlayers[random.Next(remainingPlayers.Count())];
        }

        public async Task NextPageButton_OnClickedAsync(object sender, EventArgs e)
        {
            _currentPlayer.WasProcessed = true;
            await App.Database.TryUpdatePlayerAsync(_currentPlayer);

            NavigationPage nextPage = new NavigationPage(new RoleInfoPage32(_currentPlayer));
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