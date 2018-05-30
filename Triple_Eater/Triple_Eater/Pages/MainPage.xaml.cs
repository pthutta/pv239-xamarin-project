using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.DataModels;
using Triple_Eater.Pages;
using Triple_Eater.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages
{
    public partial class MainPage : ContentPage
    {
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

        public MainPage()
		{
			InitializeComponent();
            BindingContext = this;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Players = new ObservableCollection<Player>(
                await App.Database.TryGetAllPlayersAsync()
            );

            if (Players.Count == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Players.Add(new Player()
                    {
                        Name = "Player" + (i+1),
                    });
                }

                foreach (var player in Players)
                {
                    await App.Database.TryAddPlayerAsync(player);
                }
            }

            foreach (var player in Players)
            {
                player.AccusationCounter = 0;
                player.WasProcessed = false;
            }
        }

        protected async void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            foreach (var player in Players)
            {
                await App.Database.TryUpdatePlayerAsync(player);
            }

            var nextPage = new NavigationPage(new BaseInfoPage());
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
