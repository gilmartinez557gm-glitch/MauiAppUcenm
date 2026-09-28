using SQLite;
using MauiAppUcenm.Models;

namespace MauiAppUcenm.Data
{
    public class SQLiteDatabase
    {
        private SQLiteAsyncConnection _database;

        public SQLiteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Personas>().Wait();
        }

        // Métodos CRUD para la tabla Personas

        // Create
        public async Task<int> AddPersonaAsync(Personas persona)
        {
            return await _database.InsertAsync(persona);
        }

        // Read
        public async Task<List<Personas>> GetPersonasAsync()
        {
            return await _database.Table<Personas>().ToListAsync();
        }

        // Read by Id
        public async Task<Personas> GetPersonaByIdAsync(int id)
        {
            return await _database
                .Table<Personas>()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        // Update
        public async Task<int> UpdatePersonaAsync(Personas persona)
        {
            return await _database.UpdateAsync(persona);
        }

        // Delete
        public async Task<int> DeletePersonaAsync(Personas persona)
        {
            return await _database.DeleteAsync(persona);
        }
    }
}