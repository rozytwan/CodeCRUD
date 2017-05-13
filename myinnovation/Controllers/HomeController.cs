using myinnovation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myinnovation.Controllers
{
    public class HomeController : Controller
    {
        TestEntities2 db = new TestEntities2();
        Teacher objTeacher = new Teacher();

        public ActionResult Index()
        {
            List<TeacherSubject> model = new List<TeacherSubject>();
            var data = (from t in db.Teachers
                        join s in db.Subjects on t.Subject_Id equals s.Id

                        select new { t.Id, s.SubjectName, t.TeacherName });

            foreach (var item in data)
            {
                model.Add(new TeacherSubject()
                {
                    Id = item.Id,
                    TeacherName = item.TeacherName,
                    Subject = item.SubjectName

                });
            }

            return View(model);
        }
        public ActionResult AddTeacher()
        {

            ViewBag.Subject = new SelectList(db.Subjects, "Id", "SubjectName", objTeacher.Subject);
            //Pass model here
            return View(objTeacher);

        }
        

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            try
            {
                objTeacher.Subject = teacher.Subject;

                db.Teachers.Add(objTeacher);
                db.SaveChanges();

                ViewBag.IsSaved = true;
            }
            catch (Exception)
            {
            }
            ViewBag.Teacher = new SelectList(db.Subjects, "Id", "SubjectName", objTeacher.Subject);
            objTeacher.TeacherList = db.Teachers.ToList();
            return View("Index", objTeacher.TeacherList);
        }
        public ActionResult EditTeacher(Teacher teacher, int id)
        {
            Teacher objTeacher = db.Teachers.Find(id);
            ViewBag.teacher = new SelectList(db.Subjects, "Id", "SubjectName", teacher.Subject);
            return View("Index", objTeacher);
        }

        public ActionResult Update(Teacher teacher, int id)
        {
            Teacher objTeacher = db.Teachers.Find(id);
            objTeacher.TeacherName = teacher.TeacherName;
            objTeacher.Subject = teacher.Subject;
            db.Entry(teacher);
            db.SaveChanges();
            objTeacher.TeacherList = db.Teachers.ToList();
            return View("Index", objTeacher.TeacherList);
        }

        public ActionResult DeleteTeacher(int id)
        {

            if (objTeacher == null)
            {
                return HttpNotFound();
            }
            else
            {
                objTeacher = db.Teachers.Find(id);
                db.Teachers.Remove(objTeacher);
                db.SaveChanges();
            }


            objTeacher.TeacherList = db.Teachers.ToList();
            return View("Index", objTeacher.TeacherList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Products()
        {
            ViewBag.Message = "free home delivery";
            return View();
        }
    }
}