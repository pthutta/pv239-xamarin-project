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
	public partial class AnonymousTipPage63 : ContentPage
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

	    private Player _otherPlayer;

	    public Player OtherPlayer
        {
	        get => _otherPlayer;
	        set
	        {
	            _otherPlayer = value;
	            RoleImage.Source = _otherPlayer?.OriginalRole == Role.Glutton ? "Glutton.jpg" : "Flatmates.jpg";
                OnPropertyChanged();
	        }
	    }

        public AnonymousTipPage63(Player currentPlayer)
	    {
	        InitializeComponent();
	        BindingContext = this;
            CurrentPlayer = currentPlayer;
            
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        var otherPlayers = (await App.Database.TryGetAllPlayersAsync()).Where(x => x.Guid != CurrentPlayer.Guid).ToList();
	        var random = new Random();
	        OtherPlayer = otherPlayers[random.Next(otherPlayers.Count)];
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