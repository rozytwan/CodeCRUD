using myinnovation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myinnovation.Controllers
{
    public class PersonController : Controller
    {
        TestEntities2 db = new TestEntities2();
        Person objPerson = new Person();
        // GET: Person
        public ActionResult Index(string Search)
        {
            if (!String.IsNullOrEmpty(Search))
            {
               var v = from p in db.People
                        where (p.Name.Contains(Search)) ||
                              (p.Address.Contains(Search)) ||
                              (p.Contact.ToString().Contains(Search))
                            
                        select p;
                return View("Index",v.ToList());
            }
            objPerson.personList = db.People.ToList();

            return View("Index", objPerson.personList);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult AddPerson(Person obj)
        {
            objPerson.Name = obj.Name;
            objPerson.Address = obj.Address;
            objPerson.Contact = obj.Contact;
            db.People.Add(objPerson);
            db.SaveChanges();

            objPerson.personList = db.People.ToList();
            return View("Index",objPerson.personList);
        }
        public ActionResult Edit(int? id)
        {
            Person person = db.People.Find(id);
            return View(person);

        }
        public ActionResult Update(Person person,int? id)
        {
            if (id != null)
            {
                objPerson = db.People.Find(id);
                objPerson.Name = person.Name;
                objPerson.Address = person.Address;
                objPerson.Contact = person.Contact;
                db.Entry(person);
                db.SaveChanges();
                objPerson.personList = db.People.ToList();
                return View("Index", objPerson.personList);
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        
        }
       
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                objPerson = db.People.Find(id);
                db.People.Remove(objPerson);
                db.SaveChanges();
                objPerson.personList = db.People.ToList();
                return View("Index", objPerson.personList);
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }

        }
       
    }
}