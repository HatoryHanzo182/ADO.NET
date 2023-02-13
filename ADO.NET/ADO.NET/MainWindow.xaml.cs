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
using System.Data.SqlClient;  // NuGet(System.Data.SqlClient).
using System.IO;

namespace ADO.NET
{
    public partial class MainWindow : Window
    {
        private SqlConnection _connection;  // Connection object (The basis ADO).

        public MainWindow()
        {
            InitializeComponent();

            _connection = new SqlConnection() { ConnectionString = App._connection_string };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _connection.Open();

                Label_StatusConnection.Content = "connected";
                Label_StatusConnection.Foreground = Brushes.Green;
            }
            catch (SqlException ex)
            {
                Label_StatusConnection.Content = "disconnected";
                Label_StatusConnection.Foreground = Brushes.Red;

                MessageBox.Show(ex.Message, "DB Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            ShowMonitor();
            Showviews();
        }

        #region Query and return results.
        private void Click_CreateDepartments(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand()  // Passing (SQL) request to DBMS.
            {
                Connection = _connection,
                CommandText = File.ReadAllText("RequestsSQL/RequestNewDepartmentTable.sql")
            };

            try
            {
                cmd.ExecuteNonQuery();  // NoQuery - not return result.
                MessageBox.Show("Create OK", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }

            cmd.Dispose();  // Releasing a resource.
        }

        private void Click_FillDepartments(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = _connection,
                CommandText = File.ReadAllText("RequestsSQL/RequestImplementationDepartmentStandardData.sql")
            };

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fill OK", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }

            cmd.Dispose();  // Releasing a resource.
        }

        private void Click_CreateProducts(object sender, RoutedEventArgs e)
        {
            String sql = File.ReadAllText("RequestsSQL/RequestNewProductsTable.sql");

            using SqlCommand cmd = new SqlCommand(sql, _connection);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Create products ok", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Click_FillProducts(object sender, RoutedEventArgs e)
        {
            String sql = File.ReadAllText("RequestsSQL/RequestImplementationProductsStandardData.sql");

            using SqlCommand cmd = new SqlCommand(sql, _connection);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fill products ok", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Click_CreateManagers(object sender, RoutedEventArgs e)
        {
            String sql = File.ReadAllText("RequestsSQL/RequestNewManagersTable.sql");

            using SqlCommand cmd = new SqlCommand(sql, _connection);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fill products ok", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Click_FillManagers(object sender, RoutedEventArgs e)
        {
            String sql = File.ReadAllText("RequestsSQL/RequestImplementationManagersStandardData.sql");

            using SqlCommand cmd = new SqlCommand(sql, _connection);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fill products ok", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion
        #region Queries with a Single Scalar Result.
        private void ShowMonitor()
        {
            ShowMonitorDepartments();
            ShowMonitorProducts();
            ShowMonitorManagers();
        }

        private void ShowMonitorDepartments()
        {
            using SqlCommand cmd = new SqlCommand(File.ReadAllText("RequestsSQL/RequestCountDeportations.sql"), _connection);

            try
            {
                object res = cmd.ExecuteScalar();
                int cnt = Convert.ToInt32(res);

                Label_StatusDepartments.Content = cnt.ToString();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL error", MessageBoxButton.OK, MessageBoxImage.Error);

                Label_StatusDepartments.Content = "----";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Casr error", MessageBoxButton.OK, MessageBoxImage.Error);

                Label_StatusDepartments.Content = "----";
            }
        }

        private void ShowMonitorProducts()
        {
            using SqlCommand cmd = new SqlCommand(File.ReadAllText("RequestsSQL/RequestCountDeportations.sql"), _connection);

            try
            {
                object res = cmd.ExecuteScalar();
                int cnt = Convert.ToInt32(res);

                Label_StatusProducts.Content = cnt.ToString();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL error", MessageBoxButton.OK, MessageBoxImage.Error);

                Label_StatusProducts.Content = "----";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Casr error", MessageBoxButton.OK, MessageBoxImage.Error);

                Label_StatusProducts.Content = "----";
            }
        }

        private void ShowMonitorManagers()
        {
            using SqlCommand cmd = new SqlCommand(File.ReadAllText("RequestsSQL/RequestCountManager.sql"), _connection);

            try
            {
                object res = cmd.ExecuteScalar();
                int cnt = Convert.ToInt32(res);

                Label_StatusManagers.Content = cnt.ToString();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL error", MessageBoxButton.OK, MessageBoxImage.Error);

                Label_StatusManagers.Content = "----";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Casr error", MessageBoxButton.OK, MessageBoxImage.Error);

                Label_StatusManagers.Content = "----";
            }
        }
        #endregion
        #region Request from tabular results
        private void Showviews()
        {
            ShowDepartmentsView();
            ShowProductsView();
            ShowManagersView();
        }

        private void ShowDepartmentsView()
        {
            using SqlCommand cmd = new SqlCommand(File.ReadAllText("RequestsSQL/DepartmentPullRequest.sql"), _connection);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                String str = String.Empty;

                while (reader.Read())
                    str += $"{reader.GetString(0)}  |  {reader[1]}\n";
                reader.Close();

                ViewDepartments.Text = str;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Output error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ShowProductsView()
        {
            using SqlCommand cmd = new SqlCommand(File.ReadAllText("RequestsSQL/ProductPullRequest.sql"), _connection);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                String str = String.Empty;

                while (reader.Read())
                    str += $"{reader.GetString(0)}  |  {reader[1]} \n";
                reader.Close();

                ViewProducts.Text = str;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Output error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ShowManagersView()
        {
            using SqlCommand cmd = new SqlCommand(File.ReadAllText("RequestsSQL/ManagersPullRequest.sql"), _connection);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                String str = String.Empty;

                while (reader.Read())
                    str += $"{reader.GetString(0)}  |  {reader[1]}  |  {reader[2]}\n";
                reader.Close();

                ViewManagers.Text = str;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Output error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_connection?.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}
