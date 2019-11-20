using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kompanija.Models
{
    public class Company11Context:DbContext
    {
        public System.Data.Entity.DbSet<Kompanija.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Kompanija.Models.Department> Departments { get; set; }
    }
}