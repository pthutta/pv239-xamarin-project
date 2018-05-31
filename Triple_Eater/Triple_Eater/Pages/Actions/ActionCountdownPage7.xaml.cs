using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Triple_Eater.Converters;
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
        private int _secondsElapsed = 0;
        private int _maxSeconds = 0;
        private int _remainingPlayers;
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

        public int SecondsElapsed
        {
            get { return _secondsElapsed; }
            set { _secondsElapsed = value; }
        }

        public ActionCountdownPage7()
        {
            InitializeComponent();
            BindingContext = this;
            SkipButton.Text = "S" + Environment.NewLine + "k" + Environment.NewLine + "i" + Environment.NewLine + "p";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Players = new ObservableCollection<Player>(
                    await App.Database.TryGetAllPlayersAsync()
            );
            _remainingPlayers = Players.Where((x) => !x.WasProcessed).Count();
            _maxSeconds = 10;

            // Set next page
            NavigationPage nextPage;
            if (_remainingPlayers == 0)
            {
                _maxSeconds = 120;

                //Todo: poresit pismo
                ActionInfoLabel.FontAttributes = FontAttributes.Bold;

                ActionInfoLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));

                ActionOverviewLabel.Text = "Discussion phase";
                ActionInfoLabel.Text = "";
                var converter = new OperationToStringConverter();
                foreach (var player in Players)
                {
                    ActionInfoLabel.Text +=
                        player.Name + " - " + converter.Convert(player.Operation, null, null, null) + Environment.NewLine;
                }
                ActionInfoLabel.HorizontalTextAlignment = TextAlignment.Start;
                ActionInfoLabel.Margin = new Thickness(35, 5, 25, 5);
                nextPage = new NavigationPage(new AccusationPublicPage9());

                await ResetWasProcessedFlag();
            }
            else
            {
                nextPage = new NavigationPage(new ActionPublicPage5());
            }
            NavigationPage.SetHasNavigationBar(nextPage, false);

            // Timer
            Clock.Text = GetClockFormat(_maxSeconds, SecondsElapsed);
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (SecondsElapsed < _maxSeconds)
                {
                    //update the count down timer with 1 second here 
                    SecondsElapsed++;
                    Clock.Text = GetClockFormat(_maxSeconds, SecondsElapsed);
                    return true;
                }

                Application.Current.MainPage?.Navigation.PushAsync(nextPage);
                return false;
            });
        }

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            SecondsElapsed = _maxSeconds;
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

        private string GetClockFormat(int maxSeconds, int elapsedSeconds)
        {
            int remainingSeconds = maxSeconds - elapsedSeconds;
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            return seconds > 9 ? 
                '0' + minutes.ToString() + ':' + seconds.ToString()
                : '0' + minutes.ToString() + ":0" + seconds.ToString();
        }
    }
}