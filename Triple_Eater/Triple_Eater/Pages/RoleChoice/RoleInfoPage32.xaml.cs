﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.DataModels;
using Triple_Eater.Pages.Actions;
using Triple_Eater.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages.RoleChoice
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoleInfoPage32 : ContentPage
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
                RoleLabel.Text = "You play as a " + value?.OriginalRole;
                RoleImage.Source = value?.OriginalRole == Role.Glutton ? "Glutton.jpg" : "Flatmates.jpg";
                OnPropertyChanged();
            }
        }

		public RoleInfoPage32(Player currentPlayer)
        {
            InitializeComponent();
            BindingContext = this;
            CurrentPlayer = currentPlayer;
		}

        protected override async void OnAppearing()
        {
            Players = new ObservableCollection<Player>(
                    await App.Database.TryGetAllPlayersAsync()
            );

            if(CurrentPlayer.CurrentRole == Role.Flatmate)
            {
                RoleInfoLabel.Text = "There are currently " 
                    + Players.Count(x => x.CurrentRole == Role.Glutton)
                    + " gluttons.";
            }
            else
            {
                var currentPlayerPartner = Players
                    .Where(x => x.CurrentRole == CurrentPlayer.CurrentRole)
                    .FirstOrDefault(x => x.Guid != CurrentPlayer.Guid);
                RoleInfoLabel.Text = "Your partner in crime is " + currentPlayerPartner.Name;
            }

            base.OnAppearing();
        }

        public async void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            NavigationPage nextPage;
            int remainingPlayers = Players.Count(x => !x.WasProcessed);
            if (remainingPlayers != 0)
            {
                nextPage = new NavigationPage(new RoleInfoInitPage31());
            }
            else
            {
                nextPage = new NavigationPage(new ActionPhaseInfoPage4());
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