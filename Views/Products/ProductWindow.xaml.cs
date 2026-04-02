using Project_PRN212.Models;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Project_PRN212.Views.Products
{
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using var db = new CentralKitchenManagementContext();
            dgProducts.ItemsSource = db.FinishedDishes
                .Include(x => x.Plan)
                .OrderByDescending(x => x.ProductionDate)
                .ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int dishId = (int)((System.Windows.Controls.Button)sender).Tag;
            var dlg = new ProductFormWindow(dishId) { Owner = this };
            if (dlg.ShowDialog() == true)
            {
                LoadData();
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int dishId = (int)((System.Windows.Controls.Button)sender).Tag;

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa món ăn này?\nHành động này không thể hoàn tác.",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                using var db = new CentralKitchenManagementContext();
                var dish = db.FinishedDishes.FirstOrDefault(d => d.DishId == dishId);
                if (dish != null)
                {
                    db.FinishedDishes.Remove(dish);
                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                }
            }
        }
    }
}