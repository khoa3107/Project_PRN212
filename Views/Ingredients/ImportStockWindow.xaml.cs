using Project_PRN212.Models;
using System.Windows;

namespace Project_PRN212.Views.Ingredients
{
    public partial class ImportStockWindow : Window
    {
        private readonly Ingredient _ingredient;

        public ImportStockWindow(Ingredient ingredient)
        {
            InitializeComponent();
            _ingredient = ingredient;
            txtIngredientInfo.Text =
                $"Nguyên liệu: {ingredient.IngredientName}\n" +
                $"Tồn kho hiện tại: {ingredient.QuantityInStock:N2} {ingredient.Unit}";
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";

            if (!decimal.TryParse(txtQuantity.Text.Trim(), out decimal qty) || qty <= 0)
            { txtError.Text = "Số lượng nhập phải là số dương."; return; }

            decimal? newPrice = null;
            if (!string.IsNullOrWhiteSpace(txtNewPrice.Text))
            {
                if (!decimal.TryParse(txtNewPrice.Text.Trim(), out decimal p) || p < 0)
                { txtError.Text = "Giá nhập không hợp lệ."; return; }
                newPrice = p;
            }

            try
            {
                using var db = new CentralKitchenManagementContext();
                var ing = db.Ingredients.Find(_ingredient.IngredientId)!;
                ing.QuantityInStock += qty;
                if (newPrice.HasValue) ing.ImportPrice = newPrice;
                ing.LastUpdated = DateTime.Now;
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
