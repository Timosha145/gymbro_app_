using gymbro_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ScrollView = Xamarin.Forms.ScrollView;

namespace gymbro_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainingPlanPage : ContentPage
    {
        private bool _isTapped = false;
        private List<TrainingWeekPlan> _trainingWeekPlans;

        public TrainingPlanPage()
        {
            InitializeComponent();
            LoadPage();

            ScrollView scrollView = new ScrollView { Content = stackLayout };
            Content = scrollView;
        }
        private async void LoadPage()
        {
            try
            {
                _trainingWeekPlans = await App.Database.GetTrainingWeekPlanAsync();
                collectionView.ItemsSource = _trainingWeekPlans;
            }
            catch (Exception)
            {

            }

        }

        private async void OnImageTapped(object sender, EventArgs args)
        {
            if (!_isTapped)
            {
                StackLayout stackLayoutSender = (StackLayout)sender;
                PlanPage newPage = new PlanPage();
                _isTapped = true;

                while (stackLayoutSender.TranslationY > 0)
                {
                    stackLayoutSender.TranslationY -= 10;
                    await Task.Delay(4);
                }

                foreach (TrainingWeekPlan trainingWeekPlan in _trainingWeekPlans)
                {
                    if (trainingWeekPlan.Id == int.Parse(stackLayoutSender.AutomationId))
                    {
                        newPage.Title = trainingWeekPlan.Name;
                        break;
                    }
                }

                await Navigation.PopAsync();
                await Navigation.PushAsync(newPage);

                stackLayoutSender.TranslationY = 165;
                _isTapped = false;
            }
        }

        private async void NewTraingPlan(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new AddTrainingPlanPage());
        }
    }
}

public class TrainingPlan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageSource { get; set; }

    public TrainingPlan(int id ,string name, string imageSource)
    {
        Id = id;
        Name = name;
        ImageSource = imageSource;
    }
}