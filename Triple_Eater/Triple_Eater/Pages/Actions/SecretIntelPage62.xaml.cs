using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class SecretIntelPage62 : ContentPage
	{
	    private int _selectedPlayers = 0;

	    public int SelectedPlayers
	    {
	        get => _selectedPlayers;
	        set
	        {
	            _selectedPlayers = value;
	            OnPropertyChanged();
	        }
	    }

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

	    private ObservableCollection<Player> _otherPlayers = new ObservableCollection<Player>();

        public ObservableCollection<Player> OtherPlayers
	    {
	        get => _otherPlayers;
	        set
	        {
	            _otherPlayers = value;
	            OnPropertyChanged();
	        }
	    }

        public SecretIntelPage62(Player currentPlayer)
		{
			InitializeComponent ();
		    BindingContext = this;
            CurrentPlayer = currentPlayer;
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        OtherPlayers = new ObservableCollection<Player>(
                (await App.Database.TryGetAllPlayersAsync()).Where(p => p.Guid != CurrentPlayer.Guid)
            );
        }

	    public void NextPageButton_OnClicked(object sender, EventArgs e)
	    {
	        var nextPage = new NavigationPage(new SecretIntelResultsPage62(CurrentPlayer, OtherPlayers.Where(p => p.WasSelected).ToList()));
	        NavigationPage.SetHasNavigationBar(nextPage, false);
	        Application.Current.MainPage?.Navigation.PushAsync(nextPage);
	    }

	    protected override bool OnBackButtonPressed()
	    {
	        DependencyService.Get<IMinimizeAppService>().Minimize();
	        return true;
	    }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
	    {
	        if (e.Value)
	        {
	            SelectedPlayers++;
	        }
	        else
	        {
	            SelectedPlayers--;
	        }
	    }
	}
}