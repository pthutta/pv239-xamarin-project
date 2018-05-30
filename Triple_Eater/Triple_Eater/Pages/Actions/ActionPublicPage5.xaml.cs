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
	public partial class ActionPublicPage5 : ContentPage
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

        public ActionPublicPage5 ()
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
	        Title = CurrentPlayer.Name;
	    }

        public async void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            CurrentPlayer.WasProcessed = true;
            await App.Database.TryUpdatePlayerAsync(CurrentPlayer);
            NavigationPage nextPage; 

            switch (CurrentPlayer.Operation)
            {
                case Operation.Confession:
                    nextPage = new NavigationPage(new ActionPrivateResultPage6(CurrentPlayer));
                    break;

                case Operation.SecretIntel:
                    nextPage = new NavigationPage(new ActionPrivateResultPage6(CurrentPlayer));
                    break;

                case Operation.AnonymousTip:
                    nextPage = new NavigationPage(new ActionPrivateResultPage6(CurrentPlayer));
                    break;

                case Operation.NeighborhoodGossip:
                    nextPage = new NavigationPage(new ActionPrivateResultPage6(CurrentPlayer));
                    break;

                case Operation.NightPhotographs:
                    nextPage = new NavigationPage(new ActionPrivateResultPage6(CurrentPlayer));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(CurrentPlayer.Operation), "Unsupported operation");
            }
            
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