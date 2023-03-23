using CRUD.Views;
using Xamarin.Forms;

namespace CRUD
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EmployeeEntry), typeof(EmployeeEntry));
            Routing.RegisterRoute(nameof(EmployeeDetails), typeof(EmployeeDetails));
            Routing.RegisterRoute(nameof(EnterpriseEntry), typeof(EnterpriseEntry));
            Routing.RegisterRoute(nameof(EnterpriseDetails), typeof(EnterpriseDetails));
        }

    }
}
