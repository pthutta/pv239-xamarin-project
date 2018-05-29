using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.Pages.RoleChoice;
using Triple_Eater.DataModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseInfoPage : ContentPage
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

        public BaseInfoPage ()
		{
			InitializeComponent ();
		    BindingContext = this;
        }

	    protected async override void OnAppearing()
	    {
	        Players = new ObservableCollection<Player>(
	                await App.Database.TryGetAllPlayersAsync()
	        );
            base.OnAppearing();
	    }

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new RoleInfoInitPage31());
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }
    }
}