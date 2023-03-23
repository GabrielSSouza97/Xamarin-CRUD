using System;
using System.IO;
using CRUD.Models;
using Xamarin.Forms;

namespace CRUD.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EmployeeDetails : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadEmployee(value);
            }
        }

        public EmployeeDetails()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Employee.
            BindingContext = new Employee();
        }

        async void LoadEmployee(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Employee employee = await App.EmployeeDatabase.GetEmployeeAsync(id);
                BindingContext = employee;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }
        async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var employee = (Employee)BindingContext;

            await Shell.Current.GoToAsync($"{nameof(EmployeeEntry)}?{nameof(EmployeeEntry.ItemId)}={employee.ID.ToString()}");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var employee = (Employee)BindingContext;

            await App.EmployeeDatabase.DeleteEmployeeAsync(employee);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}