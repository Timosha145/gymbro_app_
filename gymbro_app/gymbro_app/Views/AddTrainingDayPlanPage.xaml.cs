using gymbro_app.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gymbro_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTrainingDayPlanPage : ContentPage
    {
        private bool _isTapped = false;
        private string _selectedImagePath;
        private string _title;

        public AddTrainingDayPlanPage(string title)
        {
            InitializeComponent();

            _title= title;
        }

        private async void OnImageTapped(object sender, EventArgs args)
        {
            if (!_isTapped)
            {
                StackLayout stackLayoutSender = (StackLayout)sender;
                _isTapped = true;

                while (stackLayoutSender.TranslationY > 0)
                {
                    stackLayoutSender.TranslationY -= 10;
                    await Task.Delay(4);
                }

                try
                {
                    var result = await FilePicker.PickAsync(new PickOptions
                    {
                        FileTypes = FilePickerFileType.Images,
                        PickerTitle = "Select an Image"
                    });

                    if (result != null)
                    {
                        _selectedImagePath = result.FullPath;
                        planImage.Source = ImageSource.FromFile(_selectedImagePath);
                        labelForImage.Text = "Change Image";
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Alert", "Error", "❌");
                }

                stackLayoutSender.TranslationY = 165;
                _isTapped = false;
            }
        }

        private async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(_selectedImagePath) && !string.IsNullOrEmpty(nameEntry.Text) && !string.IsNullOrEmpty(descriptionEntry.Text) && dayPicker.SelectedItem != null)
            {
                await App.Database.SaveTrainingDayPlanAsync(new TrainingDayPlan
                {
                    Name = nameEntry.Text,
                    Description = descriptionEntry.Text,
                    Day = dayPicker.SelectedItem.ToString(),
                    ImagePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + Path.GetFileNameWithoutExtension(Path.GetFileName(_selectedImagePath)) + Path.GetExtension(_selectedImagePath).ToString()
                });

                await App.Database.SaveTrainingWeekPlanAsync(new TrainingWeekPlan
                {
                    TrainingDayId = App.Database.GetLastTrainingDayPlanAsync().Id,
                    Name = _title
                });

                var sourcePath = _selectedImagePath;

                DirectoryInfo d = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                File.Copy(sourcePath, d.ToString(), true);
            }
            else
            {
                //planImage.Source = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +"/"+ "filename".ToString();
                await DisplayAlert("Alert", "Please choose another image!", "❌");
            }
        }
    }
}