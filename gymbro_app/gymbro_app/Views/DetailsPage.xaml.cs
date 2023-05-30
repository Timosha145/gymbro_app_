using gymbro_app.Models;
using gymbro_app.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Entry = Xamarin.Forms.Entry;
using Picker = Xamarin.Forms.Picker;

namespace gymbro_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DateTime MinAge = DateTime.Now.AddYears(-13);
        public DetailsViewModel ViewModel { get; set; }

        private DateTime _defaultBirthday;
        private string _defaultGender, _defaultWeight, _defaultHeight;

        public DetailsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;

            datePicker.MaximumDate = MinAge;
            LoadPage();
        }

        private async void LoadPage()
        {
            try
            {
                List<Person> personList = await App.Database.GetPersonAsync();
                if (personList.Count > 0)
                {
                    List<Person> freshPersonList = new List<Person>
                    {
                        personList[personList.Count-1]
                    };

                    _defaultBirthday = freshPersonList[0].BirthDay;
                    _defaultGender = freshPersonList[0].Gender;
                    _defaultWeight = freshPersonList[0].Weight.ToString();
                    _defaultHeight = freshPersonList[0].Height.ToString();

                    datePicker.Date = _defaultBirthday;
                    genderPicker.SelectedItem = _defaultGender;
                    weightPicker.Text = _defaultWeight;
                    heightPicker.Text = _defaultHeight;

                    clearButton.IsVisible = true;
                }
                else
                {
                    clearButton.IsVisible = false;
                }
            }
            catch (Exception)
            {
                //await Navigation.PushAsync(new ChangeDetailsPage(new DetailsPage()));
            }
        }

        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOption = (string)((Picker)sender).SelectedItem;
        }

        private void OnEntryNumericChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (double.TryParse(e.NewTextValue, out double value) && value > 999.9)
            {
                ((Entry)sender).Text = e.OldTextValue;
                return;
            }

            string text = "";
            foreach (char c in e.NewTextValue)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    text += c;
                }
            }

            if (text.Contains("."))
            {
                int decimalPlaces = text.Substring(text.IndexOf('.') + 1).Length;
                if (decimalPlaces > 1)
                {
                    text = text.Substring(0, text.Length - 1);
                }
            }

            ((Entry)sender).Text = text;
        }

        async void OnSaveButtonChanged(object sender, EventArgs e)
        {
            if (genderPicker.SelectedItem != null && !string.IsNullOrWhiteSpace(heightPicker.Text) && !string.IsNullOrWhiteSpace(weightPicker.Text) && datePicker.Date != null)
            {
                if (genderPicker.SelectedItem.ToString() != _defaultGender || heightPicker.Text != _defaultHeight || weightPicker.Text != _defaultWeight || datePicker.Date != _defaultBirthday)
                {
                    await App.Database.SavePersonAsync(new Person
                    {
                        BirthDay = datePicker.Date,
                        Gender = genderPicker.SelectedItem.ToString(),
                        Weight = decimal.Parse(weightPicker.Text),
                        Height = decimal.Parse(heightPicker.Text),
                        DateAdded = DateTime.Now
                    });
                }
                else
                {
                    await DisplayAlert("Alert", "No changes were made!", "❌");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Please fill all available fields!", "❌");
            }
        }

        async void OnClearButtonChanged(object sender, EventArgs e)
        {
            bool hasToBeDeleted = await DisplayAlert("Alert", "Are you sure?", "✔️", "❌");

            if (hasToBeDeleted)
            {
                await App.Database.DeletePersonAllDataAsync();
            }
        }
    }
}