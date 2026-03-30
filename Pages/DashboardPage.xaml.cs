using System.Windows.Controls;

namespace Project_PRN212.Pages
{
    public partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            InitializeComponent();
            if (App.CurrentUser != null)
            {
                txtWelcome.Text = $"Bạn đang đăng nhập với quyền: {App.CurrentUser.Role}. Chúc bạn một ngày làm việc hiệu quả!";
            }
        }
    }
}