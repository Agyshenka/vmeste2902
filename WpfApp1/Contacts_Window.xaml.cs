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
using System.Xml;

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
                    string query = "SELECT ContactID as 'Номер контакта', Number AS Номер, Imya AS Имя, Familiya AS Фамилия FROM UserContacts Where UserID =" + Global.userID + " ";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string inputData = telephone.Text;

            if (!string.IsNullOrEmpty(inputData))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO UserContacts (UserID,Number,Imya,Familiya) VALUES (@userid,@num,@imya,@familiya)", connection))
                    {
                        command.Parameters.AddWithValue("@userid", Global.userID);
                        command.Parameters.AddWithValue("@num", inputData);
                        command.Parameters.AddWithValue("@imya", Imya.Text);
                        command.Parameters.AddWithValue("@familiya", Familiya.Text);
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно добавлены");
                        LoadData();
                    }
                }

            }
            else
            {
                MessageBox.Show("Введите данные для добавления");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            int id;
            if (!int.TryParse(idizm.Text, out id))
            {
                MessageBox.Show("Введите верный айди");
                return;
            }
            if (telephone.Text != null && telephone.Text != "Номер телефона")
            {
                string updateQuery = "UPDATE UserContacts SET Number = @Column1  WHERE ContactID = @ID";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Column1", telephone.Text);
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка в добавление:" + ex.Message);
                    }
                }
            }
            if (Imya.Text != null && Imya.Text != "Имя")
            {
                string updateQuery = "UPDATE UserContacts SET Imya = @Imya  WHERE ContactID = @ID";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Imya", Imya.Text);
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка в добавление:" + ex.Message);
                    }
                }
            }
            if (Familiya.Text != null && Familiya.Text != "Фамилия")
            {
                string updateQuery = "UPDATE UserContacts SET Familiya = @Familiya  WHERE ContactID = @ID";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Familiya", Familiya.Text);
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка в добавление:" + ex.Message);
                    }
                }
            }
            MessageBox.Show("Данные успешно изменены");
            LoadData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int idToDelete;
            if (int.TryParse(iddel.Text, out idToDelete))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM UserContacts WHERE ContactID = @id";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", idToDelete);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Запись успешно удалена");
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Запись с указанным ID не найдена");
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении записи: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение ID для удаления");
            }
        }
    }
}
