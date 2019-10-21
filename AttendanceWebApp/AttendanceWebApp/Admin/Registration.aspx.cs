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
    public partial class Registration : System.Web.UI.Page
    {
        //Initialise DB Operations class
        DBOps dbops = new DBOps();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    if (Session["username"] != null)
                    {
                        //do something
                    }
                    else
                        Response.Redirect("../Login.aspx");
                }
                catch(Exception ex)
                {
                    Response.Redirect("../Login.aspx");
                }
                //Make the current menu item active if it is the current page being loaded
                HtmlGenericControl menu = (HtmlGenericControl)this.Master.FindControl("registrationLink");
                menu.Attributes.Add("class", "active");

                //Disable the Webcam modules until the student information is fetched
                cameraModule.Enabled = false;
                btnSave1.Visible = false;
                videoModule.Visible = false;

                //Should be visible only if the student being registered is already registered
                nextRegistration.Visible = false;

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

            ddlRollNumber.DataSource = null;
            ddlRollNumber.DataBind();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Populated list of Roll numbers based on the Class and Section selected
            ddlRollNumber.DataSource = dbops.getRollNos(ddlClasses.SelectedValue,ddlSection.SelectedValue);
            ddlRollNumber.DataTextField = "roll_no";
            ddlRollNumber.DataValueField = "roll_no";
            ddlRollNumber.DataBind();
            ddlRollNumber.Items.Insert(0, new ListItem("--Select Roll Number--", "0"));
        }

        protected void ddlRollNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get student name and ID based on the Selected Class, Section and Roll Sumber
            string getNameAndID = dbops.getName(ddlClasses.SelectedValue, ddlSection.SelectedValue, ddlRollNumber.SelectedValue);
            string[] studentInfo = getNameAndID.Split('$');
            studentName.Text = studentInfo[0];
            studentID.Value = studentInfo[1];
            filenameField.Value = studentID.Value + "_" + ddlClasses.SelectedValue + "_" + ddlSection.SelectedValue + "_" + ddlRollNumber.SelectedValue + "_face";

            //Check if student is already registered
            Boolean studentAlreadyExists = dbops.checkForRegisteredStudent(studentID.Value.Trim());
            if (studentAlreadyExists)
            {
                alreadyExistsLabel.Text = "Student already registered!";
                nextRegistration.Visible = true;
                cameraModule.Enabled = false;
                btnSave1.Visible = false;
                videoModule.Visible = false;
            }
            else
            {
                alreadyExistsLabel.Text = "";
                cameraModule.Enabled = true;
                nextRegistration.Visible = false;
                btnSave1.Visible = true;
                videoModule.Visible = true;
            }
        }

        protected void btnCompleteRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        
    }
}