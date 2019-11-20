using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kompanija.Models
{
    public class Department
    {
        public int Id { get; set; }
        public int departmentNo { get; set; }
       
        [StringLength(20)]
        public string departmentName { get; set; }
       
        [StringLength(20)]
        public string departmentLocation { get; set; }
        public  virtual List<Employee>Employees { get; set; }
        public Department()
        {
            Employees = new List<Employee>();
        }
    }
}