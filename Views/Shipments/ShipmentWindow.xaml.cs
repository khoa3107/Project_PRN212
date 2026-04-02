using Microsoft.EntityFrameworkCore;
using Project_PRN212.Models;
using System.Linq;
using System.Windows;

namespace Project_PRN212.Views.Shipments
{
    public partial class ShipmentWindow : Window
    {
        public ShipmentWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using var db = new CentralKitchenManagementContext();
            dgShipments.ItemsSource = db.Shipments
                .Include(s => s.Store)
                .Include(s => s.CreatedByNavigation)
                .OrderByDescending(s => s.ShipmentDate)
                .ToList();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            var form = new ShipmentFormWindow { Owner = this };
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }
    }
}