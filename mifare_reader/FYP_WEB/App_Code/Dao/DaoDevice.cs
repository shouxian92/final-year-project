using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_WEB.App_Code
{
    public partial class DaoDevice
    {
        public ModelDataContext db = new ModelDataContext();
        public List<Device> getAllDevices()
        {
            return (from d in db.Devices
                    select d).ToList();
        }

        public Device getDeviceByImeiNumber(string strImeiNumber)
        {
            return (from d in db.Devices
                    where d.ImeiNumber.Equals(strImeiNumber)
                    select d).SingleOrDefault();
        }

        public void insertOrUpdate(Device device)
        {
            db.Devices.InsertOnSubmit(device);
            db.SubmitChanges();
        }

        public void deleteById(int intDeviceId)
        {
            Device device = (from d in db.Devices
                             where d.DeviceId == intDeviceId
                             select d).Single();

            db.Devices.DeleteOnSubmit(device);
            db.SubmitChanges();
        }
    }
}