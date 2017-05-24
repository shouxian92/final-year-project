using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_WEB.App_Code
{
    public partial class DaoStudent
    {
        public ModelDataContext db = new ModelDataContext();

        //Web Service Example
        public Student getStudent(int intStudentId)
        {
            return (from s in db.Students
                    where s.StudentId == intStudentId
                    select s).SingleOrDefault();
        }
    }
}