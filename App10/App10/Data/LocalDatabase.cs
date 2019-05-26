using App10.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App10.Data
{
    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public LocalDatabase(string filepath)
        {
            database = new SQLiteAsyncConnection(filepath);
            database.CreateTableAsync<Student>().Wait();
        }

        public async Task<List<Student>> GetStudents()
        {
            return await database.Table<Student>().ToListAsync();
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : class, new()
        {
            return await database.Table<T>().ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await database.Table<Student>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> GetItemByIdAsync<T>(int id) where T : class, ISqliteModel, new()
        {
            return await database.Table<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync<T>(T item) where T: class, ISqliteModel, new()
        {
            var result = await database.UpdateAsync(item);

            if (result == 0)
            {
                result = await database.InsertAsync(item);
            }

            return result;
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : class, ISqliteModel, new()
        {
            return await database.DeleteAsync(item);
        }

    }
}
