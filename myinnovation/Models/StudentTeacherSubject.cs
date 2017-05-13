using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myinnovation.Models
{
    public class StudentTeacherSubject
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Teacher { get; set; }
        public string Subject { get; set; }
    }
}