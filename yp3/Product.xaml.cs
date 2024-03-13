using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using yp3.yp3yDataSetTableAdapters;

namespace yp3
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Window
    {

        ProductTableAdapter products = new ProductTableAdapter();
        CategoryTableAdapter category = new CategoryTableAdapter();
        public Product()
        {
            InitializeComponent();
            ProductDataGrid.ItemsSource = products.GetData();
            TextBox_Copy.ItemsSource = category.GetData();
            TextBox_Copy.DisplayMemberPath = "Category";
            ApplyRoleRestrictions();
        }
        private void ApplyRoleRestrictions()
        {
            // Предполагается, что у вас есть переменная CurrentUser.Role, содержащая роль текущего пользователя
            if (CurrentUser.Role != null)
            {
                // Если роль - 11, скрываем кнопку "Удалить"
                if (CurrentUser.Role == 11)
                {
                    Delete.Visibility = Visibility.Collapsed;
                }
                // Если роль - 12, скрываем кнопки "Удалить" и "Добавить"
                else if (CurrentUser.Role == 12)
                {
                    Delete.Visibility = Visibility.Collapsed;
                    Add.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];
                products.InsertQuery(TextBox.Text, Convert.ToInt32(TextBox_Copy1.Text), TextBox_Copy2.Text, Convert.ToInt32(m));
                ProductDataGrid.ItemsSource = products.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении записи: " + ex.Message);
            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductDataGrid.SelectedItem != null)
                {
                    object id = (ProductDataGrid.SelectedItem as DataRowView).Row[0];
                    object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];
                    products.UpdateQuery(TextBox_Copy1.Text, Convert.ToInt32(TextBox_Copy2), TextBox_Copy2.Text, Convert.ToInt32(m), Convert.ToInt32(id));
                    ProductDataGrid.ItemsSource = products.GetData();
                }
                else
                {
                    MessageBox.Show("Выберите запись для обновления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении записи: " + ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductDataGrid.SelectedItem != null)
                {
                    object id = (ProductDataGrid.SelectedItem as DataRowView).Row[0];
                    products.DeleteQuery(Convert.ToInt32(id));
                    ProductDataGrid.ItemsSource = products.GetData();
                }
                else
                {
                    MessageBox.Show("Выберите запись для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении записи: " + ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Navigation navigationWindow = new Navigation();
            navigationWindow.Show();
            this.Close();
        }
    }
}
