using App1.ViewModels;
using gymbro_app.Models;
using gymbro_app.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace gymbro_app.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {

        public  DetailsViewModel()
        {
            Title = "Details";
        }

    }
}
