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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Contacts_Window.xaml
    /// </summary>
    public partial class Contacts_Window : Window
    {
        string ConnectionString = "Data Source=stud-mssql.sttec.yar.ru, 38325; user id = user54_db; password =user54; MultipleActiveResultSets = True; App = EntityFramework providerName=System.Data.SqlClient";
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public Contacts_Window()
        {
            InitializeComponent();
            LoadData();
        }
        // Метод для загрузки данных из таблицы в DataGrid
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Запрос на выборку всех данных из таблицы
                    string query = "SELECT Number AS Номер, Imya AS Имя, Familiya AS Фамилия FROM UserContacts Where UserID =" + Global.userID + " ";
                    dataAdapter = new SqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Привязка данных к DataGrid
                    DataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }
    }
}
