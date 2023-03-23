using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUD.Views
{
    public partial class EnterprisesList : ContentPage
    {
        public EnterprisesList()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the emterprises from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.EnterpriseDatabase.GetEnterprisesAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the EnterpriseEntry, without passing any data.
            await Shell.Current.GoToAsync(nameof(EnterpriseEntry));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the EnterpriseEntry, passing the ID as a query parameter.
                Enterprise enterprise = (Enterprise)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(EnterpriseDetails)}?{nameof(EnterpriseDetails.ItemId)}={enterprise.ID.ToString()}");
            }
        }
    }
}