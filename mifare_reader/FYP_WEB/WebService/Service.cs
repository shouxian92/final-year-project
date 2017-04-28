using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FYP_WEB.App_Code;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FYP_WEB.WebService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IService
    {
        public bool authenticate(string strUsername, string strPassword, string strDeviceImei)
        {
            DaoStaff daoStaff = new DaoStaff();
            DaoDevice daoDevice = new DaoDevice();
            bool blnAccountIsValid = daoStaff.authenticate(strUsername, strPassword);
            bool blnDeviceIsValid = daoDevice.getDeviceByImeiNumber(strDeviceImei) != null;
            return blnAccountIsValid && blnDeviceIsValid;
        }

        public int getSeatNumber(int intHallId, string strMatricNumber)
        {
            DaoEnrolment daoEnrolment = new DaoEnrolment();
            return daoEnrolment.getSeatNumberByHallIdAndMatricNumber(intHallId, strMatricNumber.ToUpper());
        }

        public List<Hall> getAllHalls()
        {
            DaoHall daoHall = new DaoHall();
            return daoHall.getAllHalls();
        }

        public List<Course> getCoursesAllocatedToHall(int intHallId, bool blnAuthenticated)
        {
            DaoCourse daoCourse = new DaoCourse();
            return daoCourse.getCoursesAllocatedToHall(intHallId);
        }

        public List<ObjStudentAttendance> getStudentsWithAttendanceInCourse(int intCourseId)
        {
            DaoEnrolment daoEnrolment = new DaoEnrolment();
            return daoEnrolment.getStudentsWithAttendanceInCourse(intCourseId);
        }

        public ObjStudentAttendance getStudentWithAttendanceInCourse(int intCourseId, string strSearch, bool blnSearchByMatric)
        {
            DaoEnrolment daoEnrolment = new DaoEnrolment();
            return daoEnrolment.getStudentWithAttendanceInCourse(intCourseId, strSearch, blnSearchByMatric);
        }

        public bool markAttendance(int intCourseId, int intStudentId)
        {
            DaoEnrolment daoEnrolment = new DaoEnrolment();
            return daoEnrolment.markAttendance(intCourseId, intStudentId);
        }

        public string getImage(int intCourseId, string strSearch, bool blnSearchByMatric)
        {
            DaoEnrolment daoEnrolment = new DaoEnrolment();
            if (!blnSearchByMatric)
            {
                Enrolment objEnrolment = daoEnrolment.findEnrolmentBySeatNumber(intCourseId, strSearch);
                strSearch = objEnrolment.Student.MatricNumber;
            }
            Image image = Image.FromFile(HostingEnvironment.ApplicationPhysicalPath + String.Format(@"\files\student_images\{0}.jpg", strSearch));
            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            return Convert.ToBase64String(ms.ToArray());
        }
        
        //Web Service Example
        public Student getStudent(int studentId)
        {
            DaoStudent daoStudent = new DaoStudent();
            return daoStudent.getStudent(studentId);
        }
    }
}