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

namespace Triple_Eater.Pages.EndGame
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameResultsPage12 : ContentPage
    {
        private ObservableCollection<Player> _flatmates = new ObservableCollection<Player>();
        private ObservableCollection<Player> _gluttons = new ObservableCollection<Player>();

        public GameResultsPage12 ()
		{
			InitializeComponent ();
            BindingContext = this;
        }
        public ObservableCollection<Player> Flatmates
        {
            get => _flatmates;
            set
            {
                _flatmates = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Player> Gluttons
        {
            get => _gluttons;
            set
            {
                _gluttons = value;
                OnPropertyChanged();
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var players = await App.Database.TryGetAllPlayersAsync();
            Gluttons = new ObservableCollection<Player>((players.Where(x => x.CurrentRole == Role.Glutton)));
            Flatmates = new ObservableCollection<Player>((players.Where(x => x.CurrentRole == Role.Flatmate)));
        }

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new MainPage());
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