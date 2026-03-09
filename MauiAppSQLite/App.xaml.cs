using MauiAppSQLite.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace MauiAppSQLite
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _database;

        public static SQLiteDatabaseHelper Database
        {
            get
            {
                if (_database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder
                        .LocalApplicationData), "products.db3");
                    _database = new SQLiteDatabaseHelper(dbPath);
                }
                return _database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}