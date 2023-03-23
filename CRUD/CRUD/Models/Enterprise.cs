using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Models
{
    public class Enterprise
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Address { get; set; }
        public string Employee { get; set; }
        public DateTime Date { get; set; }
    }
}
