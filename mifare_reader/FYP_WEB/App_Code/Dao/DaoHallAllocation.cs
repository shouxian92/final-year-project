using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_WEB.App_Code
{
    public partial class DaoHallAllocation
    {
        public ModelDataContext db = new ModelDataContext();

        public List<HallAllocation> getCoursesAllocatedToHall(int intHallId)
        {
            return (from ha in db.HallAllocations
                    where ha.HallId == intHallId
                    select ha).ToList();
        }
    }
}