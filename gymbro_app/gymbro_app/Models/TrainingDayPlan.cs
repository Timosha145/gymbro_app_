using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace gymbro_app.Models
{
    public class TrainingDayPlan
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ExerciceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Day { get; set; }
        public string ImagePath { get; set; }
    }
}