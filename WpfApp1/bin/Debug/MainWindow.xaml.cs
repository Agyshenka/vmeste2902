using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ConnectionString = "Data Source=stud-mssql.sttec.yar.ru, 38325; user id = user54_db; password =user54; MultipleActiveResultSets = True; App = EntityFramework providerName=System.Data.SqlClient";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Authorization_Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string Login = Login_TextBox.Text;
                string Password = Password_TextBox.Password;

                if (AuthenticateUser(Login, Password))
                {
                    MessageBox.Show("Успешная аутентификация");

                    Contacts_Window frm = new Contacts_Window();
                    frm.Show();
                    this.Close();
                }
                else
                {
                    Error_Label.Content = "Логин/пароль неверны. ";
                }
            }
        }
        private bool AuthenticateUser(string Login, string Password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Login AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Login", Login);
                    command.Parameters.AddWithValue("@Password", Password);

                    int count = (int)command.ExecuteScalar();

                    return count == 1; // Если есть совпадение, возвращаем true
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при аутентификации: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
