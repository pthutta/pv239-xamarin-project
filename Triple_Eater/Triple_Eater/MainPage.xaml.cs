using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.DataModels;
using Xamarin.Forms;

namespace Triple_Eater
{
    public partial class MainPage : ContentPage
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

        public MainPage()
		{
			InitializeComponent();
            BindingContext = this;

            Players.Add(new Player()
            {
                Name = "neco",
            });

		}
	}
}
