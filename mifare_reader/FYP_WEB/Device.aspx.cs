using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FYP_WEB.App_Code;

namespace FYP_WEB
{
    public partial class _Device : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateDevice();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            pnlCreateForm.Visible = true;
            pnlDevice.Visible = false;

            DaoStaff daoStaff = new DaoStaff();
            ddlStaff.DataSource = daoStaff.getAllStaff();
            ddlStaff.DataTextField = "Username";
            ddlStaff.DataValueField = "StaffId";
            ddlStaff.DataBind();

            ddlStaff.Items.Insert(0, new ListItem("Select Staff", "0"));
            ddlStaff.SelectedIndex = 0;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            pnlCreateForm.Visible = false;
            pnlDevice.Visible = true;

            DaoDevice daoDevice = new DaoDevice();
            Device device = new Device();
            device.ImeiNumber = txtImeiNumber.Text;
            if (ddlStaff.SelectedIndex != 0)
            {
                device.StaffId = Convert.ToInt32(ddlStaff.SelectedItem.Value);
            }
            daoDevice.insertOrUpdate(device);

            populateDevice();
        }

        private void populateDevice()
        {
            DaoDevice daoDevice = new DaoDevice();
            gvDevice.DataSource = daoDevice.getAllDevices();
            gvDevice.DataBind();
        }

        protected void gvDevice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DaoDevice daoDevice = new DaoDevice();
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvDevice.DataKeys[rowIndex].Values[0]);

            if (e.CommandName.Equals("DeleteDevice"))
            {
                daoDevice.deleteById(id);
            }
            populateDevice();
        }
    }
}
