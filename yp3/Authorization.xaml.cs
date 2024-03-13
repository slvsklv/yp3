using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using yp3.yp3yDataSetTableAdapters;

namespace yp3
{
    public partial class Authorization : Window
    {
        UsersTableAdapter users = new UsersTableAdapter();
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем введенные логин и пароль
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;

            // Выполняем авторизацию (замените на вашу логику)
            if (AuthenticateUser(login, password))
            {
                // Успешная авторизация

                Navigation navigationWindow = new Navigation();
                navigationWindow.Show();
                this.Close();
            }
            else
            {
                // Неудачная авторизация
     
            }
        }



        private bool AuthenticateUser(string login, string password)
        {
            try
            {
                // Получаем данные из базы данных
                var usersData = users.GetData();

                // Проверяем, есть ли в базе данных введенный логин
                if (!usersData.Any(user => user.Login.Trim() == login.Trim()))
                {
                    MessageBox.Show("Пользователь с таким логином не найден.");
                    return false;
                }

                // Получаем хранимый пароль для введенного логина
                string storedPassword = usersData
                    .Where(user => user.Login.Trim() == login.Trim())
                    .Select(user => user.Password.Trim()) // Используем Trim() для удаления пробелов из хранимого пароля
                    .FirstOrDefault();

               
                

                // Реализуйте вашу логику проверки пароля (в данном примере сравнение введенного пароля с хранимым паролем)
                if (password == storedPassword)
                {
                    MessageBox.Show("Авторизация успешна!");

                    int role = DatabaseHelper.GetRoleByLogin(login);
                    CurrentUser.Role = role;
                    MessageBox.Show(CurrentUser.Role.ToString());
                    return true;
                }
                else
                {
                    MessageBox.Show("Неверный пароль.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при авторизации: " + ex.Message);
                return false;
            }
        }

        public static class DatabaseHelper
        {
            private static UsersTableAdapter usersTableAdapter = new UsersTableAdapter();

            public static int GetRoleByLogin(string login)
            {
                try
                {
                    // Получаем данные из базы данных
                    var usersData = usersTableAdapter.GetData();

                    // Ищем пользователя по логину
                    var user = usersData.FirstOrDefault(u => u.Login.Trim() == login.Trim());

                    // Если пользователь найден, возвращаем его роль (целочисленное значение)
                    if (user != null)
                    {
                        return user.Role;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                        // Если пользователь не найден, можно вернуть какое-то значение по умолчанию или -1
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при обращении к базе данных
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных: " + ex.Message);
                    return -1;
                }
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Navigation navigationWindow = new Navigation();
            navigationWindow.Show();
            this.Close();
        }
    }
}
