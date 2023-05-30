using System;
using SQLite;

namespace gymbro_app.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
