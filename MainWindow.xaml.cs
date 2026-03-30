using System.Windows;
using System.Windows.Controls;
using Project_PRN212.Pages;

namespace Project_PRN212
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppSidebar.OnNavigationRequested += AppSidebar_OnNavigationRequested;
            AppHeader.OnLogoutRequested += AppHeader_OnLogoutRequested;

            // Start by showing Login page, and hide Sidebar/Header
            ShowLoginView();
        }

        public void ShowLoginView()
        {
            App.CurrentUser = null;
            AppSidebar.Visibility = Visibility.Collapsed;
            AppHeader.Visibility = Visibility.Collapsed;

            Grid.SetColumn(MainContentGrid, 0);
            Grid.SetColumnSpan(MainContentGrid, 2);

            MainFrame.Navigate(new LoginPage());
        }

        public void ShowMainApp()
        {
            if (App.CurrentUser == null) return;

            Grid.SetColumn(MainContentGrid, 1);
            Grid.SetColumnSpan(MainContentGrid, 1);

            AppSidebar.Visibility = Visibility.Visible;
            AppHeader.Visibility = Visibility.Visible;

            AppHeader.SetUserInfo(App.CurrentUser.FullName, App.CurrentUser.Role);
            AppSidebar.ApplyRole(App.CurrentUser.Role);

            // Navigate to Dashboard
            AppSidebar_OnNavigationRequested(this, "Dashboard");
        }

        private void AppSidebar_OnNavigationRequested(object? sender, string target)
        {
            AppHeader.SetTitle(target);
            switch (target)
            {
                case "Dashboard":
                    MainFrame.Navigate(new DashboardPage());
                    break;
                case "User":
                    // MainFrame.Navigate(new UserPage());
                    break;
                case "Inventory":
                    // MainFrame.Navigate(new InventoryPage());
                    break;
                case "Production":
                    // MainFrame.Navigate(new ProductionPage());
                    break;
                case "Delivery":
                    // MainFrame.Navigate(new DeliveryPage());
                    break;
            }
        }

        private void AppHeader_OnLogoutRequested(object? sender, System.EventArgs e)
        {
            ShowLoginView();
        }
    }
}