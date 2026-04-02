using Project_PRN212.Models;
using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Project_PRN212.Views.Productions
{
    /// <summary>
    /// Interaction logic for ProductionWindow.xaml
    /// </summary>
    public partial class ProductionWindow : Window
    {
        public ProductionWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            using var db = new CentralKitchenManagementContext();
            dgProduction.ItemsSource = db.ProductionPlans
                .Include(x => x.CreatedByNavigation)
                .OrderByDescending(x => x.ProductionDate)
                .ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";

            string planName = txtPlanName.Text.Trim();
            string product = txtProduct.Text.Trim();
            string note = txtNote.Text.Trim();

            if (string.IsNullOrEmpty(planName) || string.IsNullOrEmpty(product) || !int.TryParse(txtQty.Text.Trim(), out int qty) || qty <= 0)
            {
                txtError.Text = "Nhập đầy đủ tên kế hoạch, tên món ăn và số lượng (> 0).";
                return;
            }

            if (App.CurrentUser == null)
            {
                txtError.Text = "Không tìm thấy thông tin đăng nhập.";
                return;
            }

            using var db = new CentralKitchenManagementContext();
            db.ProductionPlans.Add(new ProductionPlan
            {
                PlanName = planName,
                DishName = product,
                PlannedQuantity = qty,
                ProductionDate = DateOnly.FromDateTime(DateTime.Today),
                Status = "Planned",
                Note = note,
                CreatedBy = App.CurrentUser.UserId
            });
            db.SaveChanges();

            // Reset form
            txtPlanName.Text = txtProduct.Text = txtQty.Text = txtNote.Text = "";
            LoadData();
        }

        private void BtnComplete_Click(object sender, RoutedEventArgs e)
        {
            int planId = (int)((System.Windows.Controls.Button)sender).Tag;

            using var db = new CentralKitchenManagementContext();
            var plan = db.ProductionPlans.FirstOrDefault(p => p.PlanId == planId);

            if (plan == null) return;

            if (plan.Status == "Completed")
            {
                MessageBox.Show("Kế hoạch này đã hoàn thành!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Xác nhận hoàn thành sản xuất món: {plan.DishName}?\nThành phẩm sẽ được tự động thêm vào kho.",
                "Hoàn thành sản xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                plan.Status = "Completed";

                db.FinishedDishes.Add(new FinishedDish
                {
                    PlanId = plan.PlanId,
                    DishName = plan.DishName,
                    ProducedQuantity = plan.PlannedQuantity,
                    Unit = "Phần", // Default unit
                    ProductionDate = plan.ProductionDate,
                    ExpiryDate = plan.ProductionDate.AddDays(7), // Expiry 1 week for example
                    Status = "Available"
                });

                db.SaveChanges();
                LoadData();
                MessageBox.Show("Đã hoàn thành và chuyển sang Thành Phẩm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnReject_Click(object sender, RoutedEventArgs e)
        {
            int planId = (int)((System.Windows.Controls.Button)sender).Tag;

            using var db = new CentralKitchenManagementContext();
            var plan = db.ProductionPlans.FirstOrDefault(p => p.PlanId == planId);

            if (plan == null) return;

            if (plan.Status == "Completed")
            {
                MessageBox.Show("Kế hoạch này đã hoàn thành, không thể từ chối!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (plan.Status == "Rejected")
            {
                MessageBox.Show("Kế hoạch này đã bị từ chối trước đó!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn TỪ CHỐI kế hoạch sản xuất món: {plan.DishName}?",
                "Từ chối kế hoạch", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                plan.Status = "Rejected";
                db.SaveChanges();
                LoadData();
                MessageBox.Show("Đã từ chối kế hoạch sản xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
