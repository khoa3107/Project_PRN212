using Microsoft.EntityFrameworkCore;
using Project_PRN212.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_PRN212.Views.Ingredients
{
    public partial class IngredientWindow : Window
    {
        private List<Ingredient> _allIngredients = new();
        private bool _searchFocused = false;

        public IngredientWindow()
        {
            InitializeComponent();
            LoadIngredients();
        }

        private void LoadIngredients()
        {
            using var db = new CentralKitchenManagementContext();
            _allIngredients = db.Ingredients.OrderBy(i => i.IngredientName).ToList();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var query = _allIngredients.AsEnumerable();

            // Lọc theo tên
            string keyword = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(keyword) && keyword != "Tìm kiếm tên nguyên liệu...")
                query = query.Where(i => i.IngredientName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase));

            // Lọc theo trạng thái
            if (cboStatus.SelectedIndex > 0)
            {
                string status = ((ComboBoxItem)cboStatus.SelectedItem).Content.ToString()!;
                query = query.Where(i => i.Status == status);
            }

            var result = query.ToList();
            dgIngredients.ItemsSource = result;
            txtCount.Text = $"Tổng: {result.Count} nguyên liệu";
        }

        // ── EVENTS ────────────────────────────────────────────────────

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!_searchFocused)
            {
                txtSearch.Text = "";
                txtSearch.Foreground = System.Windows.Media.Brushes.Black;
                _searchFocused = true;
            }
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm tên nguyên liệu...";
                txtSearch.Foreground = System.Windows.Media.Brushes.Gray;
                _searchFocused = false;
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilter();
        private void CboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilter();
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadIngredients();
            txtStatus.Text = "Đã làm mới dữ liệu.";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new IngredientFormWindow();
            if (dlg.ShowDialog() == true)
            {
                LoadIngredients();
                txtStatus.Text = "Đã thêm nguyên liệu mới.";
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            var ingredient = _allIngredients.FirstOrDefault(i => i.IngredientId == id);
            if (ingredient == null) return;

            var dlg = new IngredientFormWindow(ingredient);
            if (dlg.ShowDialog() == true)
            {
                LoadIngredients();
                txtStatus.Text = "Đã cập nhật nguyên liệu.";
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            var ingredient = _allIngredients.FirstOrDefault(i => i.IngredientId == id);
            if (ingredient == null) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa nguyên liệu \"{ingredient.IngredientName}\"?\nHành động này không thể hoàn tác.",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                using var db = new CentralKitchenManagementContext();
                // Kiểm tra đang dùng trong kế hoạch nào không
                bool inUse = db.ProductionPlanDetails.Any(d => d.IngredientId == id);
                if (inUse)
                {
                    MessageBox.Show("Không thể xóa! Nguyên liệu này đang được sử dụng trong kế hoạch sản xuất.",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                db.Ingredients.Remove(db.Ingredients.Find(id)!);
                db.SaveChanges();
                LoadIngredients();
                txtStatus.Text = "Đã xóa nguyên liệu.";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            var ingredient = _allIngredients.FirstOrDefault(i => i.IngredientId == id);
            if (ingredient == null) return;

            var dlg = new ImportStockWindow(ingredient);
            if (dlg.ShowDialog() == true)
            {
                LoadIngredients();
                txtStatus.Text = $"Đã nhập kho cho \"{ingredient.IngredientName}\".";
            }
        }
    }
}
