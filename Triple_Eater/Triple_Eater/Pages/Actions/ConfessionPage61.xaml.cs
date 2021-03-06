﻿using System;
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
	public partial class ConfessionPage61 : ContentPage
	{
	    private Player _currentPlayer;

	    public Player CurrentPlayer
	    {
	        get => _currentPlayer;
	        set
	        {
	            _currentPlayer = value;
	            RoleImage.Source = _currentPlayer?.OriginalRole == Role.Glutton ? "Glutton.jpg" : "Flatmates.jpg";
                OnPropertyChanged();
	        }
	    }

	    public ConfessionPage61(Player currentPlayer)
	    {
	        InitializeComponent();
	        BindingContext = this;
	        CurrentPlayer = currentPlayer;
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