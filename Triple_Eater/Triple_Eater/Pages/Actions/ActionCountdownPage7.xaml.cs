using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Triple_Eater.DataModels;
using Triple_Eater.Pages.EndGame;
using Triple_Eater.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages.Actions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActionCountdownPage7 : ContentPage
	{
        private Player _currentPlayer;
        private int _secondsElapsed = 0;

        public int SecondsElapsed
        {
            get { return _secondsElapsed; }
            set { _secondsElapsed = value; }
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

        public ActionCountdownPage7(Player currentPlayer)
        {
            InitializeComponent();
            BindingContext = this;
            CurrentPlayer = currentPlayer;
        }

        protected async override void OnAppearing()
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (SecondsElapsed < 10)
                {
                    //update the count down timer with 1 second here 
                    Clock.Text = SecondsElapsed.ToString();
                    SecondsElapsed++;
                    return true;
                }
                return false;
            });

            NavigationPage nextPage;
            var players = await App.Database.TryGetAllPlayersAsync();
            int remainingPlayers = players.Where((x) => !x.WasProcessed).Count();
            if (remainingPlayers != 0)
            {
                nextPage = new NavigationPage(new FinalDiscussionPage8());
            }
            else
            {
                nextPage = new NavigationPage(new ActionPhaseInfoPage4());
            }
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);

            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
	    {
	        DependencyService.Get<IMinimizeAppService>().Minimize();
	        return true;
	    }
    }
}