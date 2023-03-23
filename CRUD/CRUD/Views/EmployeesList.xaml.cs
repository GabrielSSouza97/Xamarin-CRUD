using System;
using System.Linq;
using Xamarin.Forms;
using CRUD.Models;

namespace CRUD.Views
{
    public partial class EmployeesList : ContentPage
    {
        public EmployeesList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the employees from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.EmployeeDatabase.GetEmployeesAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the EmployeeEntry, without passing any data.
            await Shell.Current.GoToAsync(nameof(EmployeeEntry));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the EmployeeEntry, passing the ID as a query parameter.
                Employee employee = (Employee)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(EmployeeDetails)}?{nameof(EmployeeDetails.ItemId)}={employee.ID.ToString()}");
            }
        }
    }
}