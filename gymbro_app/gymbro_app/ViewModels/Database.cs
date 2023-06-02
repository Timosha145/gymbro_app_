using gymbro_app.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
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

        //TrainingWeekPlan Table

        public Task<List<TrainingWeekPlan>> GetShowableTrainingWeekPlanAsync()
        {
            return  _database.Table<TrainingWeekPlan>().Where(p => p.ImagePath != null).ToListAsync();
        }

        public Task<List<TrainingWeekPlan>> GetByNameTrainingWeekPlanAsync(string name)
        {
            return _database.Table<TrainingWeekPlan>().Where(p => p.Name == name).ToListAsync();
        }

        public Task<TrainingWeekPlan> GetByIdTrainingWeekPlanAsync(int id)
        {
            return _database.Table<TrainingWeekPlan>().FirstOrDefaultAsync(p => p.Id == id);
        }

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

        public Task<TrainingDayPlan> GetByIdTrainingDayPlanAsync(int id)
        {
            return _database.Table<TrainingDayPlan>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<TrainingDayPlan> GetLastTrainingDayPlanAsync()
        {
            List<TrainingDayPlan> trainingDayPlans = await GetTrainingDayPlanAsync();

            return trainingDayPlans.LastOrDefault();
        }

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
