using ADO.NET.Entity;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace ADO.NET
{
    public partial class CrudDepartment : Window
    {
        private SqlConnection _connection;

        public Entity.Department EditedDepartment { get; private set; }

        public CrudDepartment(Entity.Department department)
        {
            InitializeComponent();

            _connection = new SqlConnection() { ConnectionString = App._connection_string };
            this.EditedDepartment = department;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _connection.Open();

            if (EditedDepartment is null)
                EditedDepartment = new Department() { Id = Guid.NewGuid() };
            else
                Textbox_Name.Text = EditedDepartment.Name;

            Textbox_Id.Text = EditedDepartment.Id.ToString();
        }

        private void Click_Button_Save(object sender, RoutedEventArgs e)
        {
            EditedDepartment.Name = Textbox_Name.Text;
            this.DialogResult = true;
        }

        private void Click_Button_Delete(object sender, RoutedEventArgs e)
        {
            EditedDepartment = null!;
            this.DialogResult = true;
        }

        private void Click_Button_Cancel(object sender, RoutedEventArgs e) => this.DialogResult = false;

        private void Window_Closed(object sender, EventArgs e) => _connection.Close();
    }
}
