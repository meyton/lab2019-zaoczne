using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App10.Data
{
    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public LocalDatabase(string filepath)
        {
            database = new SQLiteAsyncConnection(filepath);
        }
    }
}
