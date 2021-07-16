using DemoCamara.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace DemoCamara
{
    public partial class App : Application
    {
        static SQLiteHelper db;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MenuView());
        }

        public static SQLiteHelper SQLiteDb
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Galery.db3"));
                }
                return db;
            }
        }
    }
}
