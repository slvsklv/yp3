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
using System.Windows.Navigation;
using System.Windows.Shapes;
using yp3.yp3yDataSetTableAdapters;

namespace yp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UsersTableAdapter users = new UsersTableAdapter();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {


                // Получаем данные из текстовых полей
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;

             
                // Проверяем, соответствует ли логин ограничениям
                if (!IsValidInput(login, 6, 30, false, false))
                {
                    MessageBox.Show("Логин должен содержать от 6 до 30 символов и состоять из букв.");
                    return;
                }

                // Проверяем, соответствует ли пароль ограничениям
                if (!IsValidInput(password, 6, 30, true, true))
                {
                    MessageBox.Show("Пароль должен содержать от 6 до 30 символов и состоять из букв, цифр и спец. символов.");
                    return;
                }




                // Проверяем, есть ли уже аккаунт с таким логином
                if (IsLoginExists(login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует. Выберите другой логин.");
                    return;
                }
                else
                {
                    // Выполняем вставку нового пользователя в базу данных
                    users.InsertQuery(login, password, 4);     

                    MessageBox.Show("Пользователь зарегистрирован успешно!");
                    Authorization Authorization = new Authorization();

                    // Отображаем окно навигации
                    Authorization.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации пользователя: " + ex.Message);
            }


        }

        private bool IsLoginExists(string login)
        {
            try
            {
                // Получаем данные из базы данных
                var usersData = users.GetData();

                // Создаем массив логинов из данных
                string[] existingLogins = usersData.Select(user => user.Login.Trim()).ToArray();

                // Выводим содержимое массива для отладки
             

                // Проверяем, есть ли в массиве введенный логин
                return existingLogins.Contains(login.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при проверке логина: " + ex.Message);
                return false;
            }
        }
        private bool IsValidInput(string input, int minLength, int maxLength, bool allowSpecialCharacters, bool allowDigits)
        {
            // Создаем регулярное выражение, включая или исключая спецсимволы и цифры
            string pattern = allowSpecialCharacters && allowDigits ? @"^[a-zA-Zа-яА-Я0-9\s\W]+$" :
                             allowSpecialCharacters ? @"^[a-zA-Zа-яА-Я\s\W]+$" :
                             allowDigits ? @"^[a-zA-Zа-яА-Я\s]+$" : @"^[a-zA-Zа-яА-Я\s]+$";

            // Проверяем, соответствует ли ввод ограничениям по длине и символам
            return input.Length >= minLength && input.Length <= maxLength && System.Text.RegularExpressions.Regex.IsMatch(input, pattern);
        }








        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Navigation navigationWindow = new Navigation();

            // Отображаем окно навигации
            navigationWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Authorization Authorization = new Authorization();

            // Отображаем окно навигации
            Authorization.Show();
            this.Close();
        }
    }
}
