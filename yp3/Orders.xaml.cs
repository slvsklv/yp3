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
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using yp3.yp3yDataSetTableAdapters;

namespace yp3
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        OrdersTableAdapter orders = new OrdersTableAdapter();
        ProductTableAdapter products = new ProductTableAdapter();
        public Orders()
        {
            InitializeComponent();
            OrdersDataGrid.ItemsSource = orders.GetData();
            TextBox_Copy.ItemsSource = products.GetData();
            TextBox_Copy.DisplayMemberPath = "Name";
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

            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение текущего времени и форматирование в строку "HHmm"
                string currentTime = DateTime.Now.ToString("HHmm");

                object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];
                orders.InsertQuery(Convert.ToInt32(currentTime), TextBox_Copy1.Text, Convert.ToInt32(TextBox_Copy2.Text), Convert.ToInt32(m));
                OrdersDataGrid.ItemsSource = orders.GetData();
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
                if (OrdersDataGrid.SelectedItem != null)
                {
                    string currentTime = DateTime.Now.ToString("HHmm");

                    object id = (OrdersDataGrid.SelectedItem as DataRowView).Row[0];
                    object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];

                    
                    orders.UpdateQuery(Convert.ToInt32(currentTime), TextBox_Copy1.Text, Convert.ToInt32(TextBox_Copy2.Text), Convert.ToInt32(m), Convert.ToInt32(id));

                    OrdersDataGrid.ItemsSource = orders.GetData();
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
                if (OrdersDataGrid.SelectedItem != null)
                {
                    object id = (OrdersDataGrid.SelectedItem as DataRowView).Row[0];
                    orders.DeleteQuery(Convert.ToInt32(id));
                    OrdersDataGrid.ItemsSource = orders.GetData();
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

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrdersDataGrid.SelectedItem != null)
                {
                    // Получаем выбранную строку
                    var selectedOrder = (DataRowView)OrdersDataGrid.SelectedItem;

                    // Получаем данные из выбранной строки
                    int orderId = Convert.ToInt32(selectedOrder.Row[0]);
                    int Order_Time = Convert.ToInt32(selectedOrder.Row[1]);
                    string Order_Status = selectedOrder.Row[2].ToString();
                    int Total_cost = Convert.ToInt32(selectedOrder.Row[3]);
                    int Products = Convert.ToInt32(selectedOrder.Row[4]);
                    // Добавьте остальные поля в соответствии с вашей структурой данных

                    // Формируем текстовую строку для экспорта
                    string exportData = $"Order ID: {orderId}, Order_Time: {Order_Time}, Order_Status: {Order_Status}, Total_cost: {Total_cost},Products: {Products}";

                    // Сохраняем данные в файл (в данном случае, можно выбрать свой формат и способ сохранения)
                    string filePath = @"C:\Users\klika\Downloads\exported_order.txt";
                    File.WriteAllText(filePath, exportData);


                    MessageBox.Show("Данные успешно экспортированы.");
                }
                else
                {
                    MessageBox.Show("Выберите заказ для экспорта.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при экспорте данных: " + ex.Message);
            }
        }
    }
}
