using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUD.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EnterpriseDetails : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadEnterprise(value);
            }
        }
        public EnterpriseDetails()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Enterprise.
            BindingContext = new Enterprise();
        }

        async void LoadEnterprise(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the enterprise and set it as the BindingContext of the page.
                Enterprise enterprise = await App.EnterpriseDatabase.GetEnterpriseAsync(id);
                BindingContext = enterprise;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var enterprise = (Enterprise)BindingContext;

            await Shell.Current.GoToAsync($"{nameof(EnterpriseEntry)}?{nameof(EnterpriseEntry.ItemId)}={enterprise.ID.ToString()}");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var enterprise = (Enterprise)BindingContext;

            await App.EnterpriseDatabase.DeleteEnterpriseAsync(enterprise);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}