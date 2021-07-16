using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using DemoCamara.ViewModels;
using System.Threading.Tasks;

namespace DemoCamara.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Images>().Wait();
        }

        public Task <int> SaveImagesAsync(Images images)
        {
            if (images.ID == 0)
            {
                return db.InsertAsync(images);
            }
            else
            {
                return null;
            }
        }

        public Task<List<Images>> GetItemsAsync()
        {
            return db.Table<Images>().OrderByDescending(a => a.ID).ToListAsync();
        }

    }
}
