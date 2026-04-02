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
    }
}