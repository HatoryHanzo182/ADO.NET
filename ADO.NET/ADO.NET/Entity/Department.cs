using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Entity
{
    public class Department
    {
        public Department()
        {
            Id = Guid.NewGuid();
            Name = null!;
        }

        public Department(DbDataReader reader)
        {
            Id = reader.GetGuid(0);
            Name = reader.GetString(1);
        }

        public Guid Id { get; set; }  // UNIQUEIDENTIFIER NOT NULL.
        public String? Name { get; set; }  // NVARCHAR(50).
    }
}