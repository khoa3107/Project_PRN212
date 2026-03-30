using System.Windows;
using Project_PRN212.Models;

namespace Project_PRN212
{
    public partial class App : Application
    {
        // Lưu trữ user đăng nhập toàn trang
        public static User? CurrentUser { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}