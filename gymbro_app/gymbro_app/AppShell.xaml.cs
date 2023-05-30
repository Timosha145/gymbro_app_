using gymbro_app.ViewModels;
using gymbro_app.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace gymbro_app
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
