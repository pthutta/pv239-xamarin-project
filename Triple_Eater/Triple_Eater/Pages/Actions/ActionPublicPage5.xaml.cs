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
	            UpdateTexts(_currentPlayer);
                OnPropertyChanged();
	        }
	    }

	    private void UpdateTexts(Player player)
	    {
	        switch (player.Operation)
	        {
	            case Operation.Confession:
	                OperationLabel.Text = CurrentPlayer.Name + " could't hold the pressure anymore and must show one " +
	                                      "(and only one) other friend which group they belong to.";
                    break;

	            case Operation.ForgetfulPartner:
	                OperationLabel.Text = CurrentPlayer.Name + " must choose two friends. Their forgetful partner " +
	                                      "will reveal if either of them is a glutton.";
                    break;

	            case Operation.AnonymousTip:
	                OperationLabel.Text = CurrentPlayer.Name + "'s source knows the affiliation of one of their friends " +
	                                      "and reveals it to them.";
                    break;

	            case Operation.NeighborhoodGossip:
	                OperationLabel.Text = CurrentPlayer.Name + " hears gossips about two names: one is a glutton " +
	                                      "and the other is not.";
                    break;

	            case Operation.NightPhotographs:
	                OperationLabel.Text = CurrentPlayer.Name + " found evidence that shows them the names of two " +
	                                      "friends that belonged to the same group at the start.";
                    break;

	            default:
	                throw new ArgumentOutOfRangeException();
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
	    }

        public async void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            CurrentPlayer.WasProcessed = true;
            await App.Database.TryUpdatePlayerAsync(CurrentPlayer);
            NavigationPage nextPage; 

            switch (CurrentPlayer.Operation)
            {
                case Operation.Confession:
                    nextPage = new NavigationPage(new ConfessionPage61(CurrentPlayer));
                    break;

                case Operation.ForgetfulPartner:
                    nextPage = new NavigationPage(new SecretIntelPage62(CurrentPlayer));
                    break;

                case Operation.AnonymousTip:
                    nextPage = new NavigationPage(new AnonymousTipPage63(CurrentPlayer));
                    break;

                case Operation.NeighborhoodGossip:
                    nextPage = new NavigationPage(new NeighborhoodGossipPage64(CurrentPlayer));
                    break;

                case Operation.NightPhotographs:
                    nextPage = new NavigationPage(new NightPhotographsPage65(CurrentPlayer));
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