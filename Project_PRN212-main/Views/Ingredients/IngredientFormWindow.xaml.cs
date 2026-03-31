using Project_PRN212.Data;
using Project_PRN212.Models;
using System.Windows;
using System.Windows.Controls;

namespace Project_PRN212.Views.Ingredients
{
    public partial class IngredientFormWindow : Window
    {
        private readonly Ingredient? _existing;

        /// <summary>Thêm mới</summary>
        public IngredientFormWindow()
        {
            InitializeComponent();
        }

        /// <summary>Chỉnh sửa</summary>
        public IngredientFormWindow(Ingredient ingredient)
        {
            InitializeComponent();
            _existing = ingredient;
            txtTitle.Text = "Chỉnh sửa Nguyên Liệu";
            txtName.Text  = ingredient.IngredientName;
            txtUnit.Text  = ingredient.Unit;
            txtPrice.Text = ingredient.ImportPrice?.ToString() ?? "";

            foreach (ComboBoxItem item in cboStatus.Items)
                if (item.Content.ToString() == ingredient.Status)
                    cboStatus.SelectedItem = item;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";

            string name  = txtName.Text.Trim();
            string unit  = txtUnit.Text.Trim();
            string price = txtPrice.Text.Trim();
            string status = ((ComboBoxItem)cboStatus.SelectedItem).Content.ToString()!;

            if (string.IsNullOrEmpty(name)) { txtError.Text = "Vui lòng nhập tên nguyên liệu."; return; }
            if (string.IsNullOrEmpty(unit)) { txtError.Text = "Vui lòng nhập đơn vị."; return; }

            decimal? importPrice = null;
            if (!string.IsNullOrEmpty(price))
            {
                if (!decimal.TryParse(price, out decimal p) || p < 0)
                { txtError.Text = "Giá nhập không hợp lệ."; return; }
                importPrice = p;
            }

            try
            {
                using var db = new AppDbContext();

                if (_existing == null)
                {
                    // Thêm mới
                    db.Ingredients.Add(new Ingredient
                    {
                        IngredientName  = name,
                        Unit            = unit,
                        ImportPrice     = importPrice,
                        Status          = status,
                        QuantityInStock = 0,
                        LastUpdated     = DateTime.Now
                    });
                }
                else
                {
                    // Cập nhật
                    var ing = db.Ingredients.Find(_existing.IngredientId)!;
                    ing.IngredientName = name;
                    ing.Unit           = unit;
                    ing.ImportPrice    = importPrice;
                    ing.Status         = status;
                    ing.LastUpdated    = DateTime.Now;
                }

                db.SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                txtError.Text = $"Lỗi: {ex.Message}";
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
