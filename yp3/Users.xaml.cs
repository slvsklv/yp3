using System;
using System.Windows;
using yp3.yp3yDataSetTableAdapters;
using System.Data;
using System.Windows.Controls;

namespace yp3
{
    public partial class Users : Window
    {

        UsersTableAdapter users = new UsersTableAdapter();
        RoleTableAdapter role = new RoleTableAdapter();


        public Users()
        {
            InitializeComponent();
            UsersDataGrid.ItemsSource = users.GetData();
            TextBox_Copy.ItemsSource = role.GetData();
            TextBox_Copy.DisplayMemberPath = "Name";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];
            users.InsertQuery(TextBox_Copy1.Text, TextBox_Copy2.Text, Convert.ToInt32(m));
            UsersDataGrid.ItemsSource = users.GetData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersDataGrid.SelectedItem != null)
                {
                    object id = (UsersDataGrid.SelectedItem as DataRowView).Row[0];
                    object m = (TextBox_Copy.SelectedItem as DataRowView).Row[0];
                    users.UpdateQuery(TextBox_Copy1.Text, TextBox_Copy2.Text, Convert.ToInt32(m), Convert.ToInt32(id));
                    UsersDataGrid.ItemsSource = users.GetData();
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
                if (UsersDataGrid.SelectedItem != null)
                {
                    object id = (UsersDataGrid.SelectedItem as DataRowView).Row[0];
                    users.DeleteQuery(Convert.ToInt32(id));
                    UsersDataGrid.ItemsSource = users.GetData();
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
