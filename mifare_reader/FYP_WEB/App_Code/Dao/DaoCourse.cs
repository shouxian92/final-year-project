using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_WEB.App_Code
{
    public partial class DaoCourse
    {
        public ModelDataContext db = new ModelDataContext();

        public List<Course> getCoursesAllocatedToHall(int intHallId)
        {
            return (from ha in db.HallAllocations
                    where ha.HallId == intHallId
                    select ha.Course).ToList();
        }
    }
}