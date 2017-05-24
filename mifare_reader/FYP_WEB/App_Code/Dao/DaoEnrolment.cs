using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_WEB.App_Code
{
    public partial class DaoEnrolment
    {
        public ModelDataContext db = new ModelDataContext();
        public List<Enrolment> getAttendanceByCourse(int intCourseId, string strSearchTerm)
        {
            var query = from e in db.Enrolments
                        where (e.CourseId == intCourseId)
                        select e;

            if (!String.IsNullOrEmpty(strSearchTerm))
            {
                query = from e in query
                        where e.Student.Name.Contains(strSearchTerm)
                        select e;
            }

            return query.OrderBy(e => e.SeatNumber).ToList();
        }

        #region Web Service - Mobile Application
        public Enrolment findEnrolmentBySeatNumber(int intCourseId, string strSearch)
        {
            return (from e in db.Enrolments
                    where e.CourseId == intCourseId && e.SeatNumber == Convert.ToInt32(strSearch)
                    select e).SingleOrDefault();
        }
        public Enrolment findEnrolmentByMatricNumber(int intCourseId, string strSearch)
        {
            return (from e in db.Enrolments
                    where e.CourseId == intCourseId && e.Student.MatricNumber.Equals(strSearch)
                    select e).SingleOrDefault();
        }
        public List<ObjStudentAttendance> getStudentsWithAttendanceInCourse(int intCourseId)
        {
            return (from e in db.Enrolments
                    where e.CourseId == intCourseId
                    orderby e.SeatNumber
                    select new ObjStudentAttendance
                    {
                        oStudent = e.Student,
                        SeatNumber = e.SeatNumber,
                        Attendance = e.Attendance
                    }).ToList();
        }
        public ObjStudentAttendance getStudentWithAttendanceInCourse(int intCourseId, string strSearch, bool blnSearchByMatric)
        {
            strSearch = strSearch.ToUpper();

            // need to add filter of hall allocation datatime also...
            Enrolment oEnrolment = blnSearchByMatric ? findEnrolmentByMatricNumber(intCourseId, strSearch) : findEnrolmentBySeatNumber(intCourseId, strSearch);

            if (oEnrolment != null)
            {
                return new ObjStudentAttendance()
                {
                    oStudent = oEnrolment.Student,
                    SeatNumber = oEnrolment.SeatNumber,
                    Attendance = oEnrolment.Attendance
                };
            }
            else
            {
                // if is search by matric number but not part of enrolment to examination
                // return the student record only
                if (blnSearchByMatric)
                {
                    return (from s in db.Students
                            where s.MatricNumber.Equals(strSearch)
                            select new ObjStudentAttendance
                            {
                                oStudent = s,
                                SeatNumber = -1,
                                Attendance = false
                            }).SingleOrDefault();
                }
                // if enrolment record not found, means seat allocation is not made
                // hence there won't be any referenced student record also
                else
                {
                    return null;
                }
            }
        }
        public bool markAttendance(int intCourseId, int intStudentId)
        {
            Enrolment result = (from e in db.Enrolments
                                where e.CourseId == intCourseId && e.StudentId == intStudentId
                                select e).Single();
            result.Attendance = true;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Web Service - Desktop Application
        public int getSeatNumberByHallIdAndMatricNumber(int intHallId, string strMatricNumber)
        {
            DateTime now = new DateTime(2016, 5, 15, 8, 45, 0); // hardcode for testing

            int intSeatNumber = (from e in db.Enrolments
                                 where e.Course.HallAllocations.Where(
                                         ha => ha.HallId == intHallId
                                             && ha.StartTime >= now
                                             && e.SeatNumber >= ha.FirstSeatNumber
                                             && e.SeatNumber <= ha.LastSeatNumber
                                        ).OrderBy(ha => ha.StartTime).FirstOrDefault() != null
                                 && e.Student.MatricNumber.Equals(strMatricNumber)
                                 select e.SeatNumber).SingleOrDefault();

            // probably this guy is in the wrong hall
            if (intSeatNumber == 0) return -1;

            return intSeatNumber;
        }
        #endregion
    }
}