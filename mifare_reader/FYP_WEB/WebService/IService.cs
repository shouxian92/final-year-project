using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using FYP_WEB.App_Code;
using System.ServiceModel.Web;
using System.Web.Script.Services;

namespace FYP_WEB.WebService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "authenticate", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json)]
        bool authenticate(string strUsername, string strPassword, string strDeviceImei);

        [OperationContract]
        [WebInvoke(UriTemplate = "seatQuery", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json)]
        int getSeatNumber(int intHallId, string strMatricNumber);

        [OperationContract]
        [WebGet(UriTemplate = "halls")]
        List<Hall> getAllHalls();

        [OperationContract]
        [WebInvoke(UriTemplate = "courses", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json)]
        List<Course> getCoursesAllocatedToHall(int intHallId, bool blnAuthenticated);

        [OperationContract]
        [WebInvoke(UriTemplate = "students", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json)]
        List<ObjStudentAttendance> getStudentsWithAttendanceInCourse(int intCourseId);

        [OperationContract]
        [WebInvoke(UriTemplate = "student", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json)]
        ObjStudentAttendance getStudentWithAttendanceInCourse(int intCourseId, string strSearch, bool blnSearchByMatric);

        [OperationContract]
        [WebInvoke(UriTemplate = "attendance", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json)]
        bool markAttendance(int intCourseId, int intStudentId);

        [OperationContract]
        [WebInvoke(UriTemplate = "image", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json)]
        string getImage(int intCourseId, string strSearch, bool blnSearchByMatric);

        //Web Service Example
        [OperationContract]
        [WebGet(UriTemplate = "getStudent?studentId={studentId}", ResponseFormat = WebMessageFormat.Json)]
        Student getStudent(int studentId);
    }
}