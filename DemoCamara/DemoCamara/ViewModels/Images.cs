using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DemoCamara.ViewModels
{
    public class Images
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NameUser { get; set; }
        public string NameImage { get; set; }
        public string UrlImage { get; set; }
    }
}
