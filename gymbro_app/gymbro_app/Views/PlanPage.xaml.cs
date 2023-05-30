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
        public PlanPage()
        {
            InitializeComponent();

            ScrollView scrollView = new ScrollView { Content = stackLayout };

            Content = scrollView;
            collectionView.ItemsSource = _trainingPlans;
        }
    }
}