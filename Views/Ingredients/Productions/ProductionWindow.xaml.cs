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
    }
}
