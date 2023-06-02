using gymbro_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gymbro_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanPage : ContentPage
    {
        private List<TrainingPlan> _trainingPlans = new List<TrainingPlan>() { new TrainingPlan(1, "Monday", "gainweight.jpg"), new TrainingPlan(2, "Tuesday", "loseweight.png"), 
            new TrainingPlan(3, "Wendsay", "gainweight.jpg"), new TrainingPlan(4, "Thursday", "loseweight.png") };
        private List<TrainingDayPlan> _trainingDays = new List<TrainingDayPlan>();
        public PlanPage()
        {
            InitializeComponent();
            GetDayPlans();

            ScrollView scrollView = new ScrollView { Content = stackLayout };

            Content = scrollView;

        }

        private async void GetDayPlans()
        {
            foreach (var item in await App.Database.GetTrainingWeekPlanAsync())
            {
                await DisplayAlert("Alert", item.Name, "❌");
            }

            List<TrainingWeekPlan> allTrainingWeekPlans = await App.Database.GetByNameTrainingWeekPlanAsync(Title);

            foreach (var trainingWeekPlan in allTrainingWeekPlans)
            {
                await DisplayAlert("Alert", trainingWeekPlan.TrainingDayId.ToString()+" > "+trainingWeekPlan.Name, "❌");
                _trainingDays.Add(await App.Database.GetByIdTrainingDayPlanAsync(trainingWeekPlan.TrainingDayId));
            }

            collectionView.ItemsSource = _trainingDays;
        }

        private async void NewDay(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddTrainingDayPlanPage(Title));
        }
    }
}