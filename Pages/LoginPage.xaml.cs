using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Project_PRN212.Models;

namespace Project_PRN212.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";
            string user = txtUsername.Text.Trim();
            string pass = pwdPassword.Password;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                txtError.Text = "Vui lòng nhập đầy đủ thông tin.";
                return;
            }

            try
            {
                using var db = new CentralKitchenManagementContext();
                var account = db.Users.FirstOrDefault(x => x.Username == user && x.Password == pass && x.Status == "Active");

                if (account != null)
                {
                    App.CurrentUser = account;
                    if (Application.Current.MainWindow is MainWindow main)
                    {
                        main.ShowMainApp();
                    }
                }
                else
                {
                    txtError.Text = "Tài khoản hoặc mật khẩu không đúng.";
                }
            }
            catch (System.Exception ex)
            {
                txtError.Text = "Lỗi kết nối CSDL: " + ex.Message;
            }
        }
    }
}