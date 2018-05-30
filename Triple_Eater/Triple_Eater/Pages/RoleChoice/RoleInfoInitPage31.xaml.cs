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
        private Player _currentPlayer;

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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var players = await App.Database.TryGetAllPlayersAsync();
            
            var remainingPlayers = players.Where((x) => !x.WasProcessed).ToList();
            var random = new Random();
            CurrentPlayer = remainingPlayers[random.Next(remainingPlayers.Count)];
        }

        public async Task NextPageButton_OnClickedAsync(object sender, EventArgs e)
        {
            CurrentPlayer.WasProcessed = true;
            await App.Database.TryUpdatePlayerAsync(CurrentPlayer);

            NavigationPage nextPage = new NavigationPage(new RoleInfoPage32(CurrentPlayer));
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