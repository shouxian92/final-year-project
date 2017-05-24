using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FYP_WEB.App_Code;

namespace FYP_WEB
{
    public partial class _Attendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateDdlHalls();
            }
        }

        protected void ddlHalls_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateDdlCourses(Convert.ToInt32(ddlHalls.SelectedValue));
        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateAttendance(Convert.ToInt32(ddlCourses.SelectedValue), null);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateAttendance(Convert.ToInt32(ddlCourses.SelectedValue), txtStudentName.Text);
        }

        private void populateDdlHalls()
        {
            DaoHall daoHall = new DaoHall();

            ddlHalls.DataSource = daoHall.getAllHalls();
            ddlHalls.DataTextField = "Code";
            ddlHalls.DataValueField = "HallId";
            ddlHalls.DataBind();

            ddlHalls.Items.Insert(0, new ListItem("Select Hall", "0"));
            ddlHalls.SelectedIndex = 0;

            ddlCourses.Items.Insert(0, new ListItem("Select Hall", "0"));
        }

        private void populateDdlCourses(int intHallId)
        {
            DaoHallAllocation daoHallAllocation = new DaoHallAllocation();

            ddlCourses.Items.Clear();
            if (intHallId == 0)
            {
                ddlCourses.Items.Insert(0, new ListItem("Select Hall", "0"));
            }
            else
            {
                ddlCourses.DataSource = from ha in daoHallAllocation.getCoursesAllocatedToHall(intHallId)
                                        select new
                                        {
                                            ha.CourseId,
                                            Course = ha.Course.Code + " - " + ha.Course.Name
                                        };
                ddlCourses.DataTextField = "Course";
                ddlCourses.DataValueField = "CourseId";
                ddlCourses.DataBind();

                ddlCourses.Items.Insert(0, new ListItem("Select Course", "0"));
            }
            ddlCourses.SelectedIndex = 0;

            gvAttendance.DataSource = null;
            gvAttendance.DataBind();
        }

        private void populateAttendance(int intCourseId, string strSearchTerm)
        {
            DaoEnrolment daoEnrolment = new DaoEnrolment();

            if (intCourseId == 0)
            {
                txtStudentName.Text = "";
                pnlStudentFilter.Visible = false;
                gvAttendance.DataSource = null;
            }
            else
            {
                pnlStudentFilter.Visible = true;
                gvAttendance.DataSource = daoEnrolment.getAttendanceByCourse(intCourseId, strSearchTerm);
            }

            gvAttendance.DataBind();
        }
    }
}
