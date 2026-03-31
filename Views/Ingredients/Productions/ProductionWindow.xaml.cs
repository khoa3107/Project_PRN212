using Project_PRN212.Data;
using Project_PRN212.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        }
        private void LoadData()
        {
            using var db = new AppDbContext();
            dgProduction.ItemsSource = db.ProductionOrders
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";

            string product = txtProduct.Text.Trim();
            string unit = txtUnit.Text.Trim();
            string note = txtNote.Text.Trim();

            if (string.IsNullOrEmpty(product) || !int.TryParse(txtQty.Text.Trim(), out int qty) || qty <= 0)
            {
                txtError.Text = "Nhập đúng tên sản phẩm và số lượng (> 0).";
                return;
            }

            using var db = new AppDbContext();
            db.ProductionOrders.Add(new ProductionOrder
            {
                ProductName = product,
                Quantity = qty,
                Unit = string.IsNullOrEmpty(unit) ? "cái" : unit,
                Note = note,
                CreatedBy = App.CurrentUser?.Username ?? "unknown"
            });
            db.SaveChanges();

            // Reset form
            txtProduct.Text = txtQty.Text = txtUnit.Text = txtNote.Text = "";
            LoadData();
        }
    }
}
