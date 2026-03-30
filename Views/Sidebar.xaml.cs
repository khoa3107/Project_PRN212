using System;
using System.Windows;
using System.Windows.Controls;

namespace Project_PRN212.Views
{
    public partial class Sidebar : UserControl
    {
        public event EventHandler<string>? OnNavigationRequested;

        public Sidebar()
        {
            InitializeComponent();
        }

        public void ApplyRole(string role)
        {
            // Reset visibility
            btnDashboard.Visibility = Visibility.Collapsed;
            btnUser.Visibility = Visibility.Collapsed;
            btnInventory.Visibility = Visibility.Collapsed;
            btnProduction.Visibility = Visibility.Collapsed;
            btnDelivery.Visibility = Visibility.Collapsed;

            // Apply role
            if (role == "admin")
            {
                btnDashboard.Visibility = Visibility.Visible;
                btnUser.Visibility = Visibility.Visible;
                btnInventory.Visibility = Visibility.Visible;
                btnProduction.Visibility = Visibility.Visible;
                btnDelivery.Visibility = Visibility.Visible;
            }
            else if (role == "kitchen")
            {
                btnDashboard.Visibility = Visibility.Visible;
                btnInventory.Visibility = Visibility.Visible;
                btnProduction.Visibility = Visibility.Visible;
                btnDelivery.Visibility = Visibility.Visible;
            }
            else if (role == "store")
            {
                btnDashboard.Visibility = Visibility.Visible;
                btnDelivery.Visibility = Visibility.Visible;
            }
        }

        private void NavDashboard_Click(object sender, RoutedEventArgs e) => OnNavigationRequested?.Invoke(this, "Dashboard");
        private void NavUser_Click(object sender, RoutedEventArgs e) => OnNavigationRequested?.Invoke(this, "User");
        private void NavInventory_Click(object sender, RoutedEventArgs e) => OnNavigationRequested?.Invoke(this, "Inventory");
        private void NavProduction_Click(object sender, RoutedEventArgs e) => OnNavigationRequested?.Invoke(this, "Production");
        private void NavDelivery_Click(object sender, RoutedEventArgs e) => OnNavigationRequested?.Invoke(this, "Delivery");
    }
}