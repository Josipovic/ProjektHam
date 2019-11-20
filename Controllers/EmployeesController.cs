using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kompanija.Models;

namespace Kompanija.Controllers
{
    [Authorize(Users = "Bill@gmail.com,Jean@gmail.com")]
    public class EmployeesController : Controller
    {
        private Company11Context db = new Company11Context();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }
        public ActionResult Index2(int DepartmentId)
        {
            var employees = db.Employees.Include(e => e.Department).Where(m => m.departmentId == DepartmentId);

            return View("Index", employees.ToList());
        }
        //prikazuje koliko ima zaposlenika u svakoj lokaciji
        //prikaze kao badge

        public ActionResult ShowSecondHighestSalary()
        {   //da mi prikaze drugu najvecu placu

            Company11Context db = new Company11Context();
            //var list = db.Employees.OrderByDescending(e => e.salary).Skip(1).First();

            var list = db.Employees.GroupBy(e => e.salary).OrderByDescending(g => g.Key).Skip(1).First();
            ///ovaj kod koristim jer vise zaposlenika ima istu placu

            return View(list.ToList());
        }
        public ActionResult DevEmployees()
        {  //vraca development zaposlenike

            Company11Context db = new Company11Context();

            var list1 = db.Employees.Where(a => a.Department.departmentName == "Development");

            return View(list1.ToList());
        }
        public void ExportEmployeesListToCSV()  //export tablice
        {

            StringWriter sw = new StringWriter();

            sw.WriteLine("\"employeeNo\",\"employeeName\",\"Salary\",\"departmentNo\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Exported_Users.csv");
            Response.ContentType = "text/csv";
            var lista = db.Employees.ToList();
            foreach (var line in lista)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                                           line.EmployeeNo,
                                           line.EmployeeName,
                                           line.salary,
                                           line.Department.departmentNo));
            }

            Response.Write(sw.ToString());

            Response.End();

        }
        public ActionResult Average()
        {
              //vraca prosjecnu placu dev zaposlenika bez londona
            Company11Context db = new Company11Context();
           
            var average = db.Employees.Where(i => i.Department.departmentName == "Development").
                Where(a => a.Department.departmentLocation != "London").Average(x=>x.salary);
            ViewBag.average = average;


            return View();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.departmentId = new SelectList(db.Departments, "Id", "Id");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeNo,EmployeeName,salary,lastModifyDate,departmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.departmentId = new SelectList(db.Departments, "Id", "Id", employee.departmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.departmentId = new SelectList(db.Departments, "Id", "Id", employee.departmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeNo,EmployeeName,salary,lastModifyDate,departmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.departmentId = new SelectList(db.Departments, "Id", "Id", employee.departmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
