using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kompanija.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int EmployeeNo { get; set; }

        [StringLength(50)]
        public string EmployeeName { get; set; }
        public  int salary { get; set; }
        public  DateTime lastModifyDate { get; set; }
        public int departmentId { get; set; }
        public  virtual Department Department { get; set; }
    }
}