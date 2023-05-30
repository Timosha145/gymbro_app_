using SQLite;

namespace gymbro_app.Models
{
    public class TrainingWeekPlan
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TrainingDayId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
