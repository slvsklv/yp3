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
    /// Логика взаимодействия для Category.xaml
    /// </summary>
    public partial class Category : Window

    {
        CategoryTableAdapter category = new CategoryTableAdapter();
        SupplyTableAdapter supply = new SupplyTableAdapter();
        public Category()
        {
            InitializeComponent();
            CategoryDataGrid.ItemsSource = category.GetData();
            CategoryTextBox_Copy.ItemsSource = supply.GetData();
            CategoryTextBox_Copy.DisplayMemberPath = "Product";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object m = (CategoryTextBox_Copy.SelectedItem as DataRowView).Row[0];
                category.InsertQuery(CategoryTextBox.Text, Convert.ToInt32(m));
                CategoryDataGrid.ItemsSource = category.GetData();

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
                if (CategoryDataGrid.SelectedItem != null)
                {
                    object id = (CategoryDataGrid.SelectedItem as DataRowView).Row[0];
                    object m = (CategoryTextBox_Copy.SelectedItem as DataRowView).Row[0];
                    category.UpdateQuery(CategoryTextBox.Text, Convert.ToInt32(m), Convert.ToInt32(id));
                    CategoryDataGrid.ItemsSource = category.GetData();
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
                if (CategoryDataGrid.SelectedItem != null)
                {
                    object id = (CategoryDataGrid.SelectedItem as DataRowView).Row[0];
                    category.DeleteQuery(Convert.ToInt32(id));
                    CategoryDataGrid.ItemsSource = category.GetData();
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
