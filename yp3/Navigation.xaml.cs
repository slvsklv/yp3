using System;
using System.Windows;

namespace yp3
{
    public partial class Navigation : Window
    {
        public Navigation()
        {
            InitializeComponent();
            ApplyRoleRestrictions();
        }
       
        private void ApplyRoleRestrictions()
        {

            
            
                // В зависимости от значения роли, вы скрываете/отображаете кнопки
                switch (CurrentUser.Role)
                {
                    case 10:
                        // Доступны все кнопки
                        break;
                    case 11:
                        // Доступны кнопки "Товары" и "Заказы"
                        SuppliersButton.Visibility = Visibility.Collapsed;
                        CategoriesButton.Visibility = Visibility.Collapsed;
                        SuppliesButton.Visibility = Visibility.Collapsed;
                        RolesButton.Visibility = Visibility.Collapsed;
                        UsersButton.Visibility = Visibility.Collapsed;
                        break;
                    case 12:
                        // Доступны кнопки "Поставки" и "Заказы"
                        UsersButton.Visibility = Visibility.Collapsed;
                        ProductsButton.Visibility = Visibility.Collapsed;
                        RolesButton.Visibility = Visibility.Collapsed;
                        SuppliersButton.Visibility = Visibility.Collapsed;
                        CategoriesButton.Visibility = Visibility.Collapsed;
                        SuppliesButton.Visibility = Visibility.Visible;
                        break;
                    default:
                        // Обработка неизвестной роли, например, скрывать все кнопки
                        UsersButton.Visibility = Visibility.Collapsed;
                        ProductsButton.Visibility = Visibility.Collapsed;
                        RolesButton.Visibility = Visibility.Collapsed;
                        SuppliersButton.Visibility = Visibility.Collapsed;
                        CategoriesButton.Visibility = Visibility.Collapsed;
                        SuppliesButton.Visibility = Visibility.Collapsed;
                        OrdersButton.Visibility = Visibility.Collapsed;
                        break;
                }
            
        }
        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            
            Users usersWindow = new Users();
            usersWindow.Show();
            this.Close();
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            
            Product productWindow = new Product();
            productWindow.Show();
            this.Close();
        }

        private void RolesButton_Click(object sender, RoutedEventArgs e)
        {
            
            Role roleWindow = new Role();
            roleWindow.Show();
            this.Close();
        }

        private void SuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            
            Supplier supplierWindow = new Supplier();
            supplierWindow.Show();
            this.Close();
        }

        private void CategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            
            Category categoryWindow = new Category();
            categoryWindow.Show();
            this.Close();
        }

        private void SuppliesButton_Click(object sender, RoutedEventArgs e)
        {
            
            Supply supplyWindow = new Supply();
            supplyWindow.Show();
            this.Close();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            
            Orders orderWindow = new Orders();
            orderWindow.Show();
            this.Close();
        }
    }
}
