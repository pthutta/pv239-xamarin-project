using System;
using System.Collections.Generic;
using System.Text;
using Triple_Eater.Services;
using Xamarin.Forms;

namespace Triple_Eater.Pages
{
    public abstract class BasePage<TNextPage> : ContentPage
        where TNextPage : ContentPage, new()
    {
        protected BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected virtual void NextPageButton_OnClicked(object sender, EventArgs e)
        {
            var nextPage = new NavigationPage(new TNextPage());
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
