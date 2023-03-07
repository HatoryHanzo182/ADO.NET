using ADO.NET.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ADO.NET
{
    public partial class EFWindow : Window
    {
        private EFContext _ef_context;

        public EFWindow()
        {
            InitializeComponent();
            _ef_context = new EFContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ef_context.Departments.Load();
            DepartmentsList.ItemsSource = _ef_context.Departments.Local.ToObservableCollection();
            TextBlock_MonitorBlock.Text = $"Departments: { _ef_context.Departments.Count()}";
        }

        private void DepartmentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                if (item.Content is Department departments)
                {
                    CrudDepartment dialog = new CrudDepartment(new Entity.Department() { Id = departments.Id, Name = departments.Name});

                    if (dialog.ShowDialog() == true)
                    {
                        if (dialog.EditedDepartment is null)  // If delete.
                        {
                            _ef_context.Departments.Remove(departments);
                            _ef_context.SaveChanges();
                            MessageBox.Show($"Deleting {departments.Name}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            
                        }
                        else  // If save.
                        {
                            _ef_context.Departments.Remove(departments);
                            _ef_context.SaveChanges();
                            _ef_context.Departments.Add(new Department() { Id = dialog.EditedDepartment.Id, Name = dialog.EditedDepartment.Name });
                            MessageBox.Show($"Update {departments.Name}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }

        private void Click_Button_AddDepartment(object sender, RoutedEventArgs e)
        {
            CrudDepartment dialog = new CrudDepartment(null!);

            if (dialog.ShowDialog() == true)
            {
                _ef_context.Departments.Add(new Department() { Id = dialog.EditedDepartment.Id, Name = dialog.EditedDepartment.Name });
                _ef_context.SaveChanges();
                TextBlock_MonitorBlock.Text = $"Departments: {_ef_context.Departments.Count()}";
            }
        }
    }
}
