using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_WEB.App_Code
{
    public partial class DaoStaff
    {
        public ModelDataContext db = new ModelDataContext();

        public List<Staff> getAllStaff()
        {
            return (from s in db.Staffs
                    select s).ToList();
        }
        public bool authenticate(string strUsername, string strPassword)
        {
            Staff oStaff = (from s in db.Staffs
                            where s.Username.Equals(strUsername) && s.Password.Equals(strPassword)
                            select s).SingleOrDefault();
            return oStaff != null;
        }
    }
}