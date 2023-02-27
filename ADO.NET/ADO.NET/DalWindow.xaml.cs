﻿using ADO.NET.DAL;
using ADO.NET.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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

namespace ADO.NET
{
    public partial class DalWindow : Window
    {
        private readonly DataContext _context;
        public ObservableCollection<Entity.Department> DepartmentsList { get; set; }

        public DalWindow()
        {
            InitializeComponent();

            _context = new DataContext();
            DepartmentsList= new ObservableCollection<Entity.Department>(_context.Departments.GetAll());
            this.DataContext= this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MouseDoubleClick_ListView_Departments(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                if (item.Content is Entity.Department department)
                {
                    CrudDepartment dialog = new CrudDepartment(department);

                    if (dialog.ShowDialog() == true)
                    {
                        if (dialog.EditedDepartment is null)
                        {
                            DepartmentsList.Remove(department);
                            MessageBox.Show("Delete: " + department.Name);
                        }
                        else
                        {
                            int index = DepartmentsList.IndexOf(department);

                            DepartmentsList.Remove(department);
                            DepartmentsList.Insert(index, department);
                            MessageBox.Show("Update: " + department.Name);
                        }
                    }
                }
            }
        }


        private void Button_AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            CrudDepartment dialog = new CrudDepartment(null!);
        }
    }
}