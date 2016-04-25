using System;
using FreshMvvm;
using ReactiveTouch.PageModels;
using Xamarin.Forms;

namespace ReactiveTouch
{
	public class App : Application
	{
		public App()
		{
			LoadBasicNav();
		}

        private void LoadBasicNav()
        {
            var tweetSearchPage = FreshPageModelResolver.ResolvePageModel<DrawLinesPageModel>();
            var navContainer = new FreshNavigationContainer(tweetSearchPage);
            MainPage = navContainer;
        }

        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

