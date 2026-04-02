using Project_PRN212.Models;
using System;
using System.Linq;
using System.Windows;

namespace Project_PRN212.Views.Products
{
    public partial class ProductFormWindow : Window
    {
        private int _dishId;

        public ProductFormWindow(int dishId)
        {
            InitializeComponent();
            _dishId = dishId;
            LoadProductData();
        }

        private void LoadProductData()
        {
            using var db = new CentralKitchenManagementContext();
            var dish = db.FinishedDishes.FirstOrDefault(d => d.DishId == _dishId);
            if (dish != null)
            {
                txtDishName.Text = dish.DishName;
                txtQty.Text = dish.ProducedQuantity.ToString();
                txtUnit.Text = dish.Unit;
                if (dish.ExpiryDate.HasValue)
                {
                    txtExpiry.Text = dish.ExpiryDate.Value.ToString("yyyy-MM-dd");
                }
                foreach (System.Windows.Controls.ComboBoxItem item in cmbStatus.Items)
                {
                    if (item.Content.ToString() == dish.Status)
                    {
                        cmbStatus.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtDishName.Text.Trim();
            string qtyStr = txtQty.Text.Trim();
            string unit = txtUnit.Text.Trim();
            string expiryStr = txtExpiry.Text.Trim();
            string status = (cmbStatus.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString() ?? "Available";

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(qtyStr) || string.IsNullOrEmpty(unit))
            {
                txtError.Text = "Vui lòng nhập đầy đủ Tên, Số lượng và Đơn vị.";
                return;
            }

            if (!int.TryParse(qtyStr, out int qty) || qty < 0)
            {
                txtError.Text = "Số lượng phải là số nguyên >= 0.";
                return;
            }

            DateOnly? expiry = null;
            if (!string.IsNullOrEmpty(expiryStr))
            {
                if (!DateOnly.TryParseExact(expiryStr, "yyyy-MM-dd", out DateOnly d))
                {
                    txtError.Text = "HSD phải theo định dạng yyyy-MM-dd.";
                    return;
                }
                expiry = d;
            }

            using var db = new CentralKitchenManagementContext();
            var dish = db.FinishedDishes.FirstOrDefault(d => d.DishId == _dishId);
            if (dish != null)
            {
                dish.DishName = name;
                dish.ProducedQuantity = qty;
                dish.Unit = unit;
                dish.ExpiryDate = expiry;
                dish.Status = status;

                db.SaveChanges();
            }

            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}