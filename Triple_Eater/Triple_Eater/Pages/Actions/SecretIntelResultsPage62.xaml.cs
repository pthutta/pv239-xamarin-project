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
	public partial class SecretIntelResultsPage62 : ContentPage
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

        public SecretIntelResultsPage62 (Player currentPlayer, List<Player> selected)
        {
            InitializeComponent();
            BindingContext = this;
            CurrentPlayer = currentPlayer;

            if (selected.Any(p => p.CurrentRole == Role.Glutton))
            {
                ResultLabel.Text = "Your partner reveals that at least one of them is a glutton!";
                RoleImage.Source = "Glutton.png";
            }
            else
            {
                ResultLabel.Text = "Your partner reveals that none of them is a glutton.";
                RoleImage.Source = "Flatmates.jpg";
            }
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