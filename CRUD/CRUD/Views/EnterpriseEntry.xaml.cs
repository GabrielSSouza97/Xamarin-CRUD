using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CRUD.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUD.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EnterpriseEntry : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadEnterprise(value);
            }
        }
        public EnterpriseEntry()
        {
            InitializeComponent();

            BindingContext = new Enterprise();
        }

        async void LoadEnterprise(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                Enterprise enterprise = await App.EnterpriseDatabase.GetEnterpriseAsync(id);
                BindingContext = enterprise;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load employee.");
            }
        }

        void OnNameEntryChanged(object sender, EventArgs e)
        {

            if (nameEntry.Text.Length >= 50)
            {
                nameEntry.TextColor = Color.Red;
                saveButton.IsEnabled = false;
                nameHint.HeightRequest = 15;
                nameHint.Text = "Tamanho máximo 50 caracteres";
            }
            else
            {
                nameEntry.TextColor = Color.Black;
                saveButton.IsEnabled = true;
                nameHint.HeightRequest = 0;
                nameHint.Text = string.Empty;
            }
        }

        void OnCNPJEntryTextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]+$");

            if (!regex.IsMatch(cnpjEntry.Text) && cnpjEntry.Text.Length > 0)
            {
                cnpjEntry.TextColor = Color.Red;
                saveButton.IsEnabled = false;
                cnpjHint.HeightRequest = 15;
                cnpjHint.Text = "Digite apenas números";
            }
            else if (cnpjEntry.Text.Length < 14 && cnpjEntry.Text.Length > 0)
            {
                saveButton.IsEnabled = false;
                cnpjHint.HeightRequest = 15;
                cnpjHint.Text = "CNPJ precisa conter 14 dígitos";
            }
            else
            {
                cnpjEntry.TextColor = Color.Black;
                saveButton.IsEnabled = true;
                cnpjHint.Text = string.Empty;
                cnpjHint.HeightRequest = 0;
            }
        }

        void OnNewEmployeeButtonClicked(object sender, EventArgs e)
        {
            Entry newEmployeeEntry = new Entry
            {
                Placeholder = "Enter employee"
            };

            InformationsStack.Children.Add(newEmployeeEntry);
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var enterprise = (Enterprise)BindingContext;

            enterprise.Date = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(cnpjEntry.Text) && !string.IsNullOrWhiteSpace(addressEntry.Text) && !string.IsNullOrWhiteSpace(employeeEntry.Text))
            {
                await App.EnterpriseDatabase.SaveEnterpriseAsync(enterprise);

                // Navigate backwards
                await Shell.Current.GoToAsync("..");
            }
            else
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