using Project_PRN212.Views.Ingredients;
using System.Windows;

namespace Project_PRN212
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckRole();
        }

        private void CheckRole()
        {
            if (App.CurrentUser != null)
            {
                txtUserInfo.Text = $"Xin chào, {App.CurrentUser.Username} ({App.CurrentUser.Role})";

                string role = App.CurrentUser.Role;

                if (role == "admin")
                {
                    btnManageUser.Visibility = Visibility.Visible;
                    btnManageIngredient.Visibility = Visibility.Visible;
                    btnExport.Visibility = Visibility.Visible;
                    btnReceive.Visibility = Visibility.Visible;
                    btnProduction.Visibility = Visibility.Visible;
                    btnProduct.Visibility = Visibility.Visible;
                }
                else if (role == "kitchen")
                {
                    btnManageIngredient.Visibility = Visibility.Visible;
                    btnExport.Visibility = Visibility.Visible;
                    btnProduction.Visibility = Visibility.Visible;
                    btnProduct.Visibility = Visibility.Visible;
                }
                else if (role == "store")
                {
                    btnReceive.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tính năng Quản lý User/Cửa hàng",
                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnKitchen_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;
            if (btn?.Name == "btnManageIngredient")
                new IngredientWindow { Owner = this }.ShowDialog();
            else if (btn?.Name == "btnExport")
                //4. Xuất hàng (shipments)
                MessageBox.Show("Chức năng xuất hàng sẽ tích hợp sau.");
        }

        private void BtnProduction_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;
            if (btn?.Name == "btnProduction")
            {
                new Views.Productions.ProductionWindow { Owner = this }.ShowDialog();
            }
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;
            if (btn?.Name == "btnProduct")
            {
                new Views.Products.ProductWindow { Owner = this }.ShowDialog();
            }
        }

        private void BtnStore_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng cửa hàng/xuất hàng sẽ tích hợp sau.");
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}

