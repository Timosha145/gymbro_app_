using gymbro_app.Models;
using Microcharts;
using SkiaSharp;
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
    public partial class StatisticsPage : ContentPage
    {
        public StatisticsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private readonly List<ChartEntry> _entries = new List<ChartEntry>();

        public async void LoadData()
        {
            try
            {
                List<Person> personList = await App.Database.GetPersonAsync();
                for (int i = 0; i < personList.Count; i++)
                {
                    _entries.Add(new ChartEntry((int)personList[i].Weight)
                    {
                        Label = personList[i].DateAdded.ToString("MM/dd/yyyy"),
                        ValueLabel = ((int)personList[i].Weight).ToString(),
                        Color = SKColor.Parse("#F77F00")
                    }
                    );
                }

                chartViewBar.Chart = new LineChart { Entries = _entries, ValueLabelOrientation = Orientation.Horizontal, LabelTextSize = 60 };
            }
            catch (Exception)
            {

            }
        }
    }
}