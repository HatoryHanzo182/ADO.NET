using ADO.NET.Entity;
using ADO.NET.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.DAL
{
    internal class DepartmentAPI
    {
        private readonly SqlConnection _connection;
        private readonly ILogger _logger;

        public DepartmentAPI(SqlConnection connection)
        {
            _connection = connection;
            this._logger = App._logger;
        }


        public List<Entity.Department> GetAll()
        {
            var departments = new List<Entity.Department>();

            try
            {
                using SqlCommand command = new("SELECT D.* FROM Departments D", _connection);
                using var reader = command.ExecuteReader();
                

                while (reader.Read())
                    departments.Add(new Department(reader));
            }
            catch (Exception ex) 
            {
                this._logger.Log(ex.Message, "SERVER", this.GetType().Name, MethodInfo.GetCurrentMethod()?.Name ?? "");
            }
            return departments;
        }
    }
}
