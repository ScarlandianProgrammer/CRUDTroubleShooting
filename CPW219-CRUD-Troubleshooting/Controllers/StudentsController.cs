using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(context);
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(student, context);
                TempData["Message"] = $"{student.Name} was added!";
                return RedirectToAction("Index");
            }

            //Show web page with errors
            return View(student);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student student = StudentDb.GetStudent(context, id);

            //show it on web page
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, student);
                TempData["Message"] = "Student Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            Student student = StudentDb.GetStudent(context, id);
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student p = StudentDb.GetStudent(context, id);

            StudentDb.Delete(context, p);
            TempData["Message"] = "Student Removed!";

            return RedirectToAction("Index");
        }
    }
}
