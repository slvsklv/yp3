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
    /// Логика взаимодействия для Supplier.xaml
    /// </summary>
    public partial class Supplier : Window
    {
        SupplierTableAdapter supplier = new SupplierTableAdapter();
        UsersTableAdapter users = new UsersTableAdapter();
        public Supplier()
        {
            InitializeComponent();
            SupplierDataGrid.ItemsSource = supplier.GetData();
            TextBox_Copy.ItemsSource = users.GetData();
            TextBox_Copy.DisplayMemberPath = "Login";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];
            supplier.InsertQuery(TextBox_Copy2.Text, Convert.ToInt32(m));
            SupplierDataGrid.ItemsSource = supplier.GetData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SupplierDataGrid.SelectedItem != null)
                {
                    object id = (SupplierDataGrid.SelectedItem as DataRowView).Row[0];
                    object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];
                    supplier.UpdateQuery(TextBox_Copy2.Text, Convert.ToInt32(m),Convert.ToInt32(id));
                    SupplierDataGrid.ItemsSource = supplier.GetData();
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
                if (SupplierDataGrid.SelectedItem != null)
                {
                    object id = (SupplierDataGrid.SelectedItem as DataRowView).Row[0];
                    supplier.DeleteQuery(Convert.ToInt32(id));
                    SupplierDataGrid.ItemsSource = supplier.GetData();
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

