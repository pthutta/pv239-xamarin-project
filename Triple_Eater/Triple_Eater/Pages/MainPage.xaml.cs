using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.DataModels;
using Triple_Eater.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages
{
    public partial class MainPage : BasePage<BaseInfoPage>
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
                Name = "Player1",
            });
		    Players.Add(new Player()
		    {
		        Name = "Player2",
		    });
		    Players.Add(new Player()
		    {
		        Name = "Player3",
		    });
		    Players.Add(new Player()
		    {
		        Name = "Player4",
		    });
		    Players.Add(new Player()
		    {
		        Name = "Player5",
		    });
        }
    }
}
