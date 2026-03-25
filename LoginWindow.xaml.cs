using System.Linq;
using System.Windows;
using Project_PRN212.Data;

namespace Project_PRN212
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
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
                txtError.Text = "Nhập đủ thông tin.";
                return;
            }

            using var db = new AppDbContext();
            
            var account = db.Users.FirstOrDefault(x => x.Username == user && x.Password == pass);
            
            if (account != null)
            {
                App.CurrentUser = account;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                txtError.Text = "Sai tài khoản hoặc mật khẩu.";
            }
        }
    }
}