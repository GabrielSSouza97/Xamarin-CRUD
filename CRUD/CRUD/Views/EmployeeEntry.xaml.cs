using System;
using System.IO;
using System.Text.RegularExpressions;
using CRUD.Models;
using Xamarin.Forms;

namespace CRUD.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EmployeeEntry : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadEmployee(value);
            }
        }

        public EmployeeEntry()
        {
            InitializeComponent();

            BindingContext = new Employee();
        }

        async void LoadEmployee(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                Employee employee = await App.EmployeeDatabase.GetEmployeeAsync(id);
                BindingContext = employee;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load employee.");
            }
        }

        void OnNameEntryChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z ]+$");

            if (!regex.IsMatch(nameEntry.Text))
            {
                nameEntry.TextColor = Color.Red;
                saveButton.IsEnabled = false;
                nameHint.HeightRequest = 15;
                nameHint.Text = "Digite apenas letras";
            }
            else
            {
                nameEntry.TextColor = Color.Black;
                saveButton.IsEnabled = true;
                nameHint.HeightRequest = 0;
                nameHint.Text = string.Empty;
            }
        }

        void OnCPFEntryTextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]+$");

            if (!regex.IsMatch(cpfEntry.Text) && cpfEntry.Text.Length > 0)
            {
                cpfEntry.TextColor = Color.Red;
                saveButton.IsEnabled = false;
                cpfHint.HeightRequest = 15;
                cpfHint.Text = "Digite apenas números";
            } else if (cpfEntry.Text.Length < 11 && cpfEntry.Text.Length > 0) {
                saveButton.IsEnabled = false;
                cpfHint.HeightRequest = 15;
                cpfHint.Text = "CPF precisa conter 11 dígitos";
            } else {
                cpfEntry.TextColor= Color.Black;
                saveButton.IsEnabled = true;
                cpfHint.Text = string.Empty;
                cpfHint.HeightRequest = 0;
            }
        }

        void OnEmailEntryChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            if (!regex.IsMatch(emailEntry.Text))
            {
                emailEntry.TextColor = Color.Red;
                saveButton.IsEnabled = false;
                emailHint.HeightRequest = 15;
                emailHint.Text = "Email deve estar no modelo: nome@email.com";
            } else {
                emailEntry.TextColor = Color.Black;
                saveButton.IsEnabled = true;
                emailHint.HeightRequest = 0;
                emailHint.Text = string.Empty;
            }
        }

        void OnNewEnterpriseButtonClicked(object sender, EventArgs e)
        {
            Entry newEnterpriseEntry = new Entry
            {
                Placeholder = "Enter enterprise"
            };

            InformationsStack.Children.Add(newEnterpriseEntry);
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var employee = (Employee)BindingContext;

            employee.Date = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(cpfEntry.Text) && !string.IsNullOrWhiteSpace(emailEntry.Text) && !string.IsNullOrWhiteSpace(addressEntry.Text) && !string.IsNullOrWhiteSpace(enterpriseEntry.Text))
            {
                await App.EmployeeDatabase.SaveEmployeeAsync(employee);

                // Navigate backwards
                await Shell.Current.GoToAsync("..");
            } else
            {
                await DisplayAlert("Alerta", "Campos vazios, operação não concluída.", "OK");
            }
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}