using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Entity
{
    public class Department
    {
        public Guid Id { get; set; }  // Id UNIQUEIDENTIFITI.
        public String? Name { get; set; }  // Name NWARCHAR(50).
    }
}