using CRUD.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Data
{
    public class EnterpriseDatabase
    {
        readonly SQLiteAsyncConnection database;

        public EnterpriseDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Enterprise>().Wait();
        }

        public Task<List<Enterprise>> GetEnterprisesAsync()
        {
            //Get all enterprises.
            return database.Table<Enterprise>().ToListAsync();
        }

        public Task<Enterprise> GetEnterpriseAsync(int id)
        {
            // Get a specific enterprise.
            return database.Table<Enterprise>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveEnterpriseAsync(Enterprise enterprise)
        {
            if (enterprise.ID != 0)
            {
                // Update an existing enterprise.
                return database.UpdateAsync(enterprise);
            }
            else
            {
                // Save a new enterprise.
                return database.InsertAsync(enterprise);
            }
        }

        public Task<int> DeleteEnterpriseAsync(Enterprise enterprise)
        {
            // Delete a enterprise.
            return database.DeleteAsync(enterprise);
        }
    }
}
