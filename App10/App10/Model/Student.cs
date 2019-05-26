using System;
using System.Collections.Generic;
using System.Text;

namespace App10.Model
{
    public class Student : ISqliteModel
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Grade { get; set; }
        public int TeacherId { get; set; }
    }
}
