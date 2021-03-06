﻿using System;
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
    public partial class NightPhotographsPage65 : ContentPage
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

        private Player _player1;
        private Player Player1
        {
            get => _player1;
            set
            {
                _player1 = value;
                OnPropertyChanged("Players");
            }
        }

        private Player _player2;
        private Player Player2
        {
            get => _player2;
            set
            {
                _player2 = value;
                OnPropertyChanged("Players");
            }
        }

        public string Players => Player1?.Name + " and " + Player2?.Name;

        public NightPhotographsPage65(Player currentPlayer)
        {
            InitializeComponent();
            BindingContext = this;
            CurrentPlayer = currentPlayer;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var otherPlayers = (await App.Database.TryGetAllPlayersAsync()).Where(p => p.Guid != CurrentPlayer.Guid).ToList();
            if (CurrentPlayer.CurrentRole == Role.Glutton)
            {
                otherPlayers = otherPlayers.Where(p => p.OriginalRole == Role.Flatmate).ToList();
            }
            var random = new Random();
            Player1 = otherPlayers[random.Next(otherPlayers.Count)];

            otherPlayers = otherPlayers.Where(p => p.Guid != Player1.Guid && p.OriginalRole == Player1.OriginalRole).ToList();
            Player2 = otherPlayers[random.Next(otherPlayers.Count)];
        }

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new ActionCountdownPage7());
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