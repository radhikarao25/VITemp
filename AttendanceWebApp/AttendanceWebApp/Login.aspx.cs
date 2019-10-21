using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AttendanceWebApp.App_Code;

namespace AttendanceWebApp
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                errorLabel.Text = "";
                Session.Clear();
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            DBOps dops = new DBOps();
            string username = inputName.Text.ToString().Trim();
            string password = inputPassword.Text.ToString().Trim();
            Boolean verified = dops.checkLogin(username, password);
            if (verified)
            {
                Session.Add("username","admin");
                Response.Redirect("Admin/Registration.aspx");

            }
            else
            {
                errorLabel.Text = "Username or Password entered is wrong!";
            }
        }
    }
}