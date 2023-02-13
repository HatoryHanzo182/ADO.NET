using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.IO;
using ADO.NET.Entity;
using System.Data;

namespace ADO.NET
{
    public partial class OrmWindow : Window
    {
        private SqlConnection _connection;

        public ObservableCollection<Entity.Department> Departments { get; set; }
        public ObservableCollection<Entity.Manager> Manager { get; set; }
        public ObservableCollection<Entity.Product> Product { get; set; }

        public OrmWindow()
        {
            InitializeComponent();

            Departments = new ObservableCollection<Entity.Department>();
            Manager = new ObservableCollection<Entity.Manager>();
            Product = new ObservableCollection<Entity.Product>();
            DataContext = this;  // {Binding Departments}
            _connection = new SqlConnection(App._connection_string);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand() { Connection = _connection };

                #region Load Department.
                cmd.CommandText = "SELECT Id, Name FROM Departments";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                    Departments.Add(new Entity.Department() { Id = reader.GetGuid(0), Name = reader.GetString(1) });

                reader.Close();
                #endregion
                #region Load Manager.
                cmd.CommandText = "SELECT M.Id, M.Surname, M.Name, M.Secname, M.Id_main_Dep, M.Id_sec_dep, M.Id_chief FROM Managers as M";
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Manager.Add(new Entity.Manager()
                    {
                        Id = reader.GetGuid(0),
                        Surname = reader.GetString(1),
                        Name = reader.GetString(2),
                        Secname = reader.GetString(3),
                        IdMainDep = reader.GetGuid(4),
                        IdSecDep = reader.GetValue(5) == DBNull.Value ? null : reader.GetGuid(5),
                        IdChief = reader.IsDBNull(6) ? null : reader.GetGuid(6)
                    }); 
                }

                reader.Close();
                #endregion
                #region Load Product.
                cmd.CommandText = "SELECT P.Id, P.Name, P.Price FROM Products as P";
                reader= cmd.ExecuteReader();
                
                while (reader.Read())
                    Product.Add(new Entity.Product() { Id = reader.GetGuid(0), Name = reader.GetString(1), Price = reader.GetDouble(2) });
                
                reader.Close();
                #endregion
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Window will be cloded", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        #region Events click.
        private void DepartmentsListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (sender is ListViewItem item)
            //{
            //    if (item.Content is Entity.Department departments)
            //    {
            //        CRUDWindow dialog = new CRUDWindow(departments);
            //
            //        if (dialog.ShowDialog() == true)
            //        {
            //            if (dialog.EditDepartment is null)  // delete.
            //            {
            //                Departments.Remove(departments);
            //                MessageBox.Show($"Deleting {departments.Name}");
            //
            //            }
            //            else
            //            {
            //                int index = Departments.IndexOf(departments);
            //
            //                Departments.Remove(departments);
            //                Departments.Insert(index, departments);
            //                MessageBox.Show($"Update {departments.Name}");
            //            }
            //        }
            //        else  // Cancel.
            //            MessageBox.Show("Asked");
            //    }
            //
            //}
        }


        private void ManagerListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                if (item.Content is Entity.Manager managers)
                    MessageBox.Show(managers.Surname);
            }
        }

        private void ProductListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                if (item.Content is Entity.Product product)
                    MessageBox.Show($"{product.Name}   {product.Price}$");
            }
        }
        #endregion

        private void Add_Department_Click(object sender, RoutedEventArgs e)
        {
            //CRUDWindow dialog = new CRUDWindow(null!);
        }
    }
}
