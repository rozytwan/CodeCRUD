using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myinnovation.Models;

namespace myinnovation.Controllers
{
    public class StudentsController : Controller
    {
        TestEntities2 db = new TestEntities2();
        Student1 objStud = new Student1();
        // GET: Students
        public ActionResult Index()
        {
            List<StudentTeacherSubject> model = new List<StudentTeacherSubject>();
            var data = (from S in db.Student1
                        join t in db.Teachers on S.Teacher_Id equals t.Id
                        join s in db.Subjects on S.Subject_Id equals s.Id
                        select new { S.Id, S.StudentName, t.TeacherName, s.SubjectName });
            foreach (var item in data)
            {
                model.Add(new StudentTeacherSubject()
                {
                    Id = item.Id,
                    StudentName = item.StudentName,
                    Teacher = item.TeacherName,
                    Subject = item.SubjectName
                });

            }
            return View(model);
        }
        public ActionResult AddStudent()
        {
            ViewBag.Teacher = new SelectList(db.Teachers, "Id", "Teacher", objStud.Teacher);
            //ViewBag.Subject = new SelectList(db.Subjects, "Id", "Subject", objStud.Subject);
 return View();

        } 
        public ActionResult Create(Student1 stud)
        {
            objStud.StudentName = stud.StudentName;
            objStud.Teacher = stud.Teacher;
            objStud.Subject = stud.Subject;
            db.Student1.Add(objStud);
            db.SaveChanges();
            objStud.StudList = db.Student1.ToList();
            return View("Index", objStud.StudList);

        }
        public ActionResult EditStudent(Student1 student,int id)
        {
            objStud = db.Student1.Find(id);
            ViewBag.student = new SelectList(db.Teachers,"Id", "Teacher", objStud.Teacher);
            //ViewBag.student = new SelectList(db.Subjects , "Id", "Subject", objStud.Subject);
            return View();

        }
        
        public ActionResult Update(Student1 stud,int id)
        {
            objStud = db.Student1.Find(id);
            objStud.StudentName = stud.StudentName;
            objStud.Teacher = stud.Teacher;
            objStud.Subject = stud.Subject;
            db.Entry(stud);
            db.SaveChanges();
            return View();
        }    
        public ActionResult Delete(int id)
        {
            objStud = db.Student1.Find(id);
            db.Student1.Remove(objStud);
            db.SaveChanges();
            objStud.StudList = db.Student1.ToList();
            return View("Index", objStud.StudList);
        }
        
    }
}