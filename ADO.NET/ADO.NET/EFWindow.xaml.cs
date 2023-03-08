using ADO.NET.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ADO.NET
{
    public partial class EFWindow : Window
    {
        private EFContext _ef_context;
        private ICollectionView _I_departments_list_view;

        public EFWindow()
        {
            InitializeComponent();

            _ef_context = new EFContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ef_context.Departments.Load();

            DepartmentsList.ItemsSource = _ef_context.Departments.Local.ToObservableCollection();
            _I_departments_list_view = CollectionViewSource.GetDefaultView(DepartmentsList.ItemsSource);
            _I_departments_list_view.Filter = obj => (obj as Department)?.DeleteDt == null;

            UpdateMonitor();
        }

        private void UpdateMonitor()
        {
            TextBlock_MonitorBlock.Text = $"Departments: {_ef_context.Departments.Count()}";
            TextBlock_MonitorBlock.Text += $"\nProducts: {_ef_context.Products.Count()}";
            TextBlock_MonitorBlock.Text += $"\nManagers: {_ef_context.Managers.Count()}";
            TextBlock_MonitorBlock.Text += $"\nSales: {_ef_context.Sales.Count()}";
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
                            departments.DeleteDt = DateTime.Now;
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

        private bool HideDeletedDepartmentsFilter(object item)
        {
            if (item is Department department)
                return department.DeleteDt is null;
            return false;
        }

        private void Checkbox_showDeletedDepartment_Checked(object sender, RoutedEventArgs e)
        {
            _I_departments_list_view.Filter = null;
            ((GridView)DepartmentsList.View).Columns[2].Width = Double.NaN;
        }

        private void Checkbox_showDeletedDepartment_Unchecked(object sender, RoutedEventArgs e)
        {
            _I_departments_list_view.Filter = HideDeletedDepartmentsFilter;
            ((GridView)DepartmentsList.View).Columns[2].Width = 0;
        }
    }
}