using System;
using System.Collections.Generic;
using System.Text;

namespace App10.Model
{
    public class Teacher : ISqliteModel
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
