using AttendanceWebApp.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AttendanceWebApp.Admin
{
    public partial class NonRegisteredStudents : System.Web.UI.Page
    {
        //Initialise DB Operations class
        DBOps dbops = new DBOps();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HtmlGenericControl menu = (HtmlGenericControl)this.Master.FindControl("nonRegisteredStudentLink");
                menu.Attributes.Add("class", "active");

                defaultLabel.Visible = true;

                //Populate the dropdown with the list of unique classes from the DB
                ddlClasses.DataSource = dbops.getClasses();
                ddlClasses.DataTextField = "class";
                ddlClasses.DataValueField = "class";
                ddlClasses.DataBind();
                ddlClasses.Items.Insert(0, new ListItem("--Select Class--", "0"));
            }
        }

        protected void ddlClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Populate the Sections dropdown for a Class from DB
            ddlSection.DataSource = dbops.getSections(ddlClasses.SelectedValue);
            ddlSection.DataTextField = "section";
            ddlSection.DataValueField = "section";
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, new ListItem("--Select Section--", "0"));
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            nonRegisteredStudentsList.DataSource = dbops.getNonRegisteredStudentsList(ddlClasses.SelectedValue, ddlSection.SelectedValue);
            nonRegisteredStudentsList.DataBind();

            defaultLabel.Visible = false;
        }
    }
}