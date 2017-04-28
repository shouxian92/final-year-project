using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FYP_WEB.App_Code
{

    public partial class DaoHall
    {
        public ModelDataContext db = new ModelDataContext();

        public List<Hall> getAllHalls()
        {
            return (from hall in db.Halls
                    select hall).ToList();
        }
    }
}
