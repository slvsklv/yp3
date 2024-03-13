using System;
using System.Collections.Generic;
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
using System.Windows;
using yp3.yp3yDataSetTableAdapters;
using System.Data;

namespace yp3
{
    public partial class Role : Window
    {
        RoleTableAdapter role = new RoleTableAdapter();

        public Role()
        {
            InitializeComponent();
            RoleDataGrid.ItemsSource = role.GetData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            role.InsertQuery(RoleTextBox1.Text, RoleTextBox2.Text);
            RoleDataGrid.ItemsSource = role.GetData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            object id = (RoleDataGrid.SelectedItem as DataRowView).Row[0];
            role.UpdateQuery(RoleTextBox1.Text, RoleTextBox2.Text, Convert.ToInt32(id));
            RoleDataGrid.ItemsSource = role.GetData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoleDataGrid.SelectedItem != null)
                {
                    object id = (RoleDataGrid.SelectedItem as DataRowView).Row[0];
                    role.DeleteQuery(Convert.ToInt32(id));
                    RoleDataGrid.ItemsSource = role.GetData();
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

