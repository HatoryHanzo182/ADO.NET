using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ADO.NET
{
    public partial class App : Application
    {
                 // Server connection string.
                // To be able to open connections to the database for output, well, or making changes.
        public static readonly String _connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\My resources\Work\WPF\ADO\ADO.NET\ADO.NET\Database.mdf;Integrated Security=True";
    }
}
