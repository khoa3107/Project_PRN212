using System;
using System.Windows;
using System.Windows.Controls;

namespace Project_PRN212.Views
{
    public partial class Header : UserControl
    {
        public event EventHandler? OnLogoutRequested;

        public Header()
        {
            InitializeComponent();
        }

        public void SetUserInfo(string username, string role)
        {
            txtUserInfo.Text = $"Xin chào, {username} ({role})";
        }

        public void SetTitle(string title)
        {
            txtTitle.Text = title;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            OnLogoutRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}