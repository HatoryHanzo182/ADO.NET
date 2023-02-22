using System.Linq;
using System.Windows;

namespace ADO.NET
{
    public partial class CrudManager : Window
    {
        public Entity.Manager? EditManager;

        public CrudManager(Entity.Manager edit_manager)
        {
            InitializeComponent();
            this.EditManager = edit_manager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Owner;

            if (EditManager is null)
            {
                EditManager = new Entity.Manager();
                Button_Cancel.IsEnabled = false;
            }
            else
            {
                TextBox_Surname.Text = EditManager.Surname;
                TextBox_Name.Text = EditManager.Name;
                TextBox_Secname.Text = EditManager.Secname;

                if (Owner is OrmWindow owner)
                {
                    ComboBox_MainDepartment.SelectedItem = owner.Departments.FirstOrDefault(dep => dep.Id == EditManager.IdMainDep);
                    ComboBox_SecDepartment.SelectedItem = owner.Departments.FirstOrDefault(dep => dep.Id == EditManager.IdSecDep);
                    ComboBox_Chief.SelectedItem = owner.Manager.FirstOrDefault(man => man.Id == EditManager.IdChief);
                }
                else
                {
                    MessageBox.Show("Owner is not OrmWindow");
                    Close();
                }
            }
            TextBox_Id.Text = EditManager.Id.ToString();
        }

        private void Click_Button_Save(object sender, RoutedEventArgs e) => this.DialogResult = true;
        private void Click_Button_Delete(object sender, RoutedEventArgs e) => this.DialogResult = true;
        private void Click_Button_Cancel(object sender, RoutedEventArgs e) => this.DialogResult = false;
    }
}
