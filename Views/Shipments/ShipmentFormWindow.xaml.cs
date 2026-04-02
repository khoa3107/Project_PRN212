using Project_PRN212.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Project_PRN212.Views.Shipments
{
    public partial class ShipmentFormWindow : Window
    {
        // Class tạm để hiển thị trên DataGrid
        public class CartItem
        {
            public int DishId { get; set; }
            public string DishName { get; set; } = "";
            public int Quantity { get; set; }
        }

        private List<CartItem> _cart = new List<CartItem>();

        public ShipmentFormWindow()
        {
            InitializeComponent();
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            using var db = new CentralKitchenManagementContext();
            cboStore.ItemsSource = db.Stores.ToList();
            // Chỉ hiển thị các món ăn đã sản xuất và còn tồn kho > 0
            cboDish.ItemsSource = db.FinishedDishes.Where(d => d.ProducedQuantity > 0).ToList();
        }

        private void BtnAddDish_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";
            if (cboDish.SelectedItem is not FinishedDish selectedDish) return;

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                txtError.Text = "Số lượng phải là số nguyên dương.";
                return;
            }

            // Kiểm tra tồn kho thực tế
            using var db = new CentralKitchenManagementContext();
            var dbDish = db.FinishedDishes.Find(selectedDish.DishId);
            if (dbDish == null || dbDish.ProducedQuantity < qty)
            {
                txtError.Text = $"Trong kho chỉ còn {dbDish?.ProducedQuantity} phần.";
                return;
            }

            // Kiểm tra xem món đã có trong giỏ chưa, nếu có thì cộng dồn
            var existing = _cart.FirstOrDefault(c => c.DishId == selectedDish.DishId);
            if (existing != null)
                existing.Quantity += qty;
            else
                _cart.Add(new CartItem { DishId = selectedDish.DishId, DishName = selectedDish.DishName, Quantity = qty });

            // Refresh UI
            dgDetails.ItemsSource = null;
            dgDetails.ItemsSource = _cart;
            txtQuantity.Text = "";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cboStore.SelectedValue == null)
            {
                txtError.Text = "Vui lòng chọn cửa hàng nhận.";
                return;
            }
            if (!_cart.Any())
            {
                txtError.Text = "Vui lòng thêm ít nhất 1 món ăn vào phiếu xuất.";
                return;
            }

            using var db = new CentralKitchenManagementContext();
            using var transaction = db.Database.BeginTransaction(); // Sử dụng Transaction để đảm bảo tính toàn vẹn dữ liệu
            try
            {
                // 1. Tạo Shipment
                var shipment = new Shipment
                {
                    StoreId = (int)cboStore.SelectedValue,
                    ShipmentDate = DateOnly.FromDateTime(DateTime.Today),
                    CreatedBy = App.CurrentUser!.UserId,
                    Status = "Shipped", // Gán trạng thái đã gửi
                    Note = txtNote.Text
                };
                db.Shipments.Add(shipment);
                db.SaveChanges(); // Lưu để lấy ShipmentId

                // 2. Tạo các ShipmentDetail & Trừ kho FinishedDish
                foreach (var item in _cart)
                {
                    var detail = new ShipmentDetail
                    {
                        ShipmentId = shipment.ShipmentId,
                        DishId = item.DishId,
                        Quantity = item.Quantity
                    };
                    db.ShipmentDetails.Add(detail);

                    // Trừ số lượng kho
                    var dishInDb = db.FinishedDishes.Find(item.DishId);
                    if (dishInDb != null)
                    {
                        dishInDb.ProducedQuantity -= item.Quantity;
                    }
                }

                db.SaveChanges();
                transaction.Commit();

                MessageBox.Show("Tạo phiếu xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                txtError.Text = $"Lỗi khi lưu: {ex.Message}";
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}