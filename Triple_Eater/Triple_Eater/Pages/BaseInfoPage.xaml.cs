﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple_Eater.DataModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseInfoPage : BasePage<BaseInfoPage>
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
    }
}