using System;
using System.Windows;
using yp3.yp3yDataSetTableAdapters;
using System.Data;

namespace yp3
{
    public partial class Supply : Window
    {
        SupplyTableAdapter supply = new SupplyTableAdapter();
        SupplierTableAdapter supplier = new SupplierTableAdapter();
        public Supply()
        {
            InitializeComponent();
            SupplyDataGrid.ItemsSource = supply.GetData();
            TextBox_Copy.ItemsSource = supplier.GetData();
            TextBox_Copy.DisplayMemberPath = "Name";
            ApplyRoleRestrictions();
        }
        private void ApplyRoleRestrictions()
        {
            // Предполагается, что у вас есть переменная CurrentUser.Role, содержащая роль текущего пользователя
            if (CurrentUser.Role != null)
            {
                // Если роль - 11, скрываем кнопку "Удалить"
                if (CurrentUser.Role == 12)
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
                supply.InsertQuery(TextBox_Copy1.Text, Convert.ToInt32(TextBox_Copy2.Text), Convert.ToInt32(m));
                SupplyDataGrid.ItemsSource = supplier.GetData();
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
                if (SupplyDataGrid.SelectedItem != null)
                {
                    object id = (SupplyDataGrid.SelectedItem as DataRowView).Row[0];
                    object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];
                    supply.UpdateQuery(TextBox_Copy1.Text, Convert.ToInt32(TextBox_Copy2), Convert.ToInt32(m), Convert.ToInt32(id));
                    SupplyDataGrid.ItemsSource = supply.GetData();
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
                if (SupplyDataGrid.SelectedItem != null)
                {
                    object id = (SupplyDataGrid.SelectedItem as DataRowView).Row[0];
                    supply.DeleteQuery(Convert.ToInt32(id));
                    SupplyDataGrid.ItemsSource = supply.GetData();
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
