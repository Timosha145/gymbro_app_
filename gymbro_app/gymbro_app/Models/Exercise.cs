using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace gymbro_app.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public MucleGroup MuscleGroup { get; set; }
    }
}

public enum MucleGroup
{
    calves,
    hamstrings,
    quadriceps,
    glutes,
    biceps,
    triceps,
    forearms,
    trapezius,
    latissimus
}