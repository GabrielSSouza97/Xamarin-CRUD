using CRUD.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace CRUD
{
    public partial class App : Application
    {
        static EmployeeDatabase employeeDatabase;

        // Create the database connection as a singleton.
        public static EmployeeDatabase EmployeeDatabase
        {
            get
            {
                if (employeeDatabase == null)
                {
                    employeeDatabase = new EmployeeDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Employees.db3"));
                }
                return employeeDatabase;
            }
        }

        static EnterpriseDatabase enterpriseDatabase;
        public static EnterpriseDatabase EnterpriseDatabase
        {
            get
            {
                if (enterpriseDatabase == null)
                {
                    enterpriseDatabase = new EnterpriseDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Enterprises.db3"));
                }
                return enterpriseDatabase;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
