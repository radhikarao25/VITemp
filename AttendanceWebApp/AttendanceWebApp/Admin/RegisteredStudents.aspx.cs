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
    public partial class RegisteredStudents : System.Web.UI.Page
    {
        //Initialise DB Operations class
        DBOps dbops = new DBOps();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HtmlGenericControl menu = (HtmlGenericControl)this.Master.FindControl("registerStudentsLink");
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
            registerStudentsList.DataSource = dbops.getStudentsList(ddlClasses.SelectedValue, ddlSection.SelectedValue);
            registerStudentsList.DataBind();

            defaultLabel.Visible = false;
        }


        protected void registerStudentsList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            string studentID = "";
            HiddenField studentIDField = (registerStudentsList.Items[e.ItemIndex].FindControl("studentID")) as HiddenField;
            if (studentIDField != null)
                studentID = studentIDField.Value.ToString();
            dbops.deleteStudentDetails(studentID);
            registerStudentsList.EditIndex = -1;
            registerStudentsList.DataSource = dbops.getStudentsList(ddlClasses.SelectedValue, ddlSection.SelectedValue);
            registerStudentsList.DataBind();

        }
    }
}