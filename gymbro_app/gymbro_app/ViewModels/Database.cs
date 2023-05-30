using gymbro_app.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gymbro_app.ViewModels
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database= new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>();
            _database.CreateTableAsync<TrainingWeekPlan>();
            _database.CreateTableAsync<TrainingDayPlan>();

        }

        //Person Table

        public Task<List<Person>> GetPersonAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }

        public Task<int> ChangePersonAsync(Person person) 
        {
            return _database.UpdateAsync(person);
        }

        public Task<int> DeletePersonAllDataAsync()
        {
            return _database.DeleteAllAsync<Person>();
        }

        //TrainingPlan Table

        public Task<List<TrainingWeekPlan>> GetTrainingWeekPlanAsync()
        {
            return _database.Table<TrainingWeekPlan>().ToListAsync();
        }

        public Task<int> SaveTrainingWeekPlanAsync(TrainingWeekPlan trainingWeekPlan)
        {
            return _database.InsertAsync(trainingWeekPlan);
        }

        public Task<int> ChangeTrainingWeekPlanAsync(TrainingWeekPlan trainingWeekPlan)
        {
            return _database.UpdateAsync(trainingWeekPlan);
        }

        public Task<int> DeleteTrainingWeekPlanAllDataAsync()
        {
            return _database.DeleteAllAsync<TrainingWeekPlan>();
        }

        //TrainingDayPlan Table

        public Task<List<TrainingDayPlan>> GetTrainingDayPlanAsync()
        {
            return _database.Table<TrainingDayPlan>().ToListAsync();
        }

        public Task<int> SaveTrainingDayPlanAsync(TrainingDayPlan trainingDayPlan)
        {
            return _database.InsertAsync(trainingDayPlan);
        }

        public Task<int> ChangeTrainingDayPlanAsync(TrainingDayPlan trainingDayPlan)
        {
            return _database.UpdateAsync(trainingDayPlan);
        }

        public Task<int> DeleteTrainingDayPlanAllDataAsync()
        {
            return _database.DeleteAllAsync<TrainingDayPlan>();
        }
    }
}
