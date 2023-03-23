using System;
using SQLite;
using System.Collections.Generic;
namespace CRUD.Models
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Enterprise { get; set; }
        public DateTime Date { get; set; }
    }
}
