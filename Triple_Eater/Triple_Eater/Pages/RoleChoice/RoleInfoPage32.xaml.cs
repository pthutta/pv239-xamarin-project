﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.DataModels;
using Triple_Eater.Pages.Actions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages.RoleChoice
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoleInfoPage32 : ContentPage
	{
        private Player _player;

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged();
            }
                
        }

		public RoleInfoPage32(Player player)
        {
			InitializeComponent ();
            _player = player;
		}

        public void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new ActionPhaseInfoPage4());
            NavigationPage.SetHasNavigationBar(nextPage, false);
            Application.Current.MainPage?.Navigation.PushAsync(nextPage);
        }
    }

}