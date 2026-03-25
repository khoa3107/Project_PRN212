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
                }
                else if (role == "kitchen")
                {
                    btnManageIngredient.Visibility = Visibility.Visible;
                    btnExport.Visibility = Visibility.Visible;
                }
                else if (role == "store")
                {
                    btnReceive.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "Hệ thống Quản lý Người dùng và Cửa hàng - Dành cho Admin.";
        }

        private void BtnKitchen_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "Quản lý Nguyên Liệu và Sản Xuất - Dành cho Bếp Trung Tâm.";
        }

        private void BtnStore_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "Nhận hàng và Kho Cửa Hàng - Dành cho Store Manager.";
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