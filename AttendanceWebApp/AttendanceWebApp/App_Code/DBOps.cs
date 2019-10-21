using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace AttendanceWebApp.App_Code
{
//Host : 206.71.52.87
//DB Name : atharep_biom
//DB Username : atharep_bomu
//DB Password: ukHG=CJ_.%T)
    public class DBOps
    {
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString);
        
        /// <summary>
        /// A common function to execute the query that returns no rows
        /// </summary>
        /// <param name="cmd">The command in the form of query/stored procedure along with the parameters</param>
        private void executeNonQuery(MySqlCommand cmd)
        {
            try
            {
                //open the connection
                conn.Open();
                //send the sql query to insert the data to our Users table
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// A common function to execute the sql query/procedure to fetch the data
        /// </summary>
        /// <param name="cmd">The command in the form of query/stored procedure along with the parameters</param>
        /// <returns></returns>
        private DataSet getDataSet(MySqlCommand cmd)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter();

            try
            {
                da.SelectCommand = cmd;
                conn.Open();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// SQL Method to check the validation for LOGIN
        /// </summary>
        /// <returns>Returns true or false based on user verification</returns>
        public Boolean checkLogin(string username, string password)
        {
            if(username == "Admin" && password == "face$$Attendance2017")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// A Sql procedure to get unique classes from the DB
        /// </summary>
        /// <returns></returns>
        public DataSet getClasses()
        {
            //SQL command to get uniques class numbers
            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT class FROM osso_profile ORDER BY CAST(class AS UNSIGNED)", conn);
            cmd.CommandType = CommandType.Text;
            DataSet ds = getDataSet(cmd);
            return ds;
        }

        /// <summary>
        /// A Sql procedure to get unique classes from the DB
        /// </summary>
        /// <returns></returns>
        public DataSet getSections(string classNumber)
        {
            //SQL command to get uniques class numbers
            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT section FROM osso_profile WHERE class = '" + classNumber  + "' ORDER BY section", conn);
            cmd.CommandType = CommandType.Text;
            DataSet ds = getDataSet(cmd);
            return ds;
        }

        /// <summary>
        /// A Sql procedure to get unique classes from the DB
        /// </summary>
        /// <returns></returns>
        public DataSet getRollNos(string classNumber, string section)
        {
            //SQL command to get uniques class numbers
            MySqlCommand cmd = new MySqlCommand("SELECT roll_no FROM osso_profile WHERE class = '" + classNumber + "' AND  section = '" + section + "' ORDER BY CAST(roll_no AS UNSIGNED)", conn);
            cmd.CommandType = CommandType.Text;
            DataSet ds = getDataSet(cmd);
            return ds;
        }

        /// <summary>
        /// A Sql procedure to get unique classes from the DB
        /// </summary>
        /// <returns></returns>
        public string getName(string classNumber, string section, string rollNo)
        {
            //SQL command to get uniques class numbers
            MySqlCommand cmd = new MySqlCommand("SELECT id, name FROM osso_profile WHERE class = '" + classNumber + "' AND  section = '" + section + "' AND roll_no = '" + rollNo + "'", conn);
            cmd.CommandType = CommandType.Text;
            DataSet ds = getDataSet(cmd);
            string name = "";
            string id = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                name = row["name"].ToString();
                id = row["id"].ToString();
            }
            return name + "$" + id;
        }

        /// <summary>
        /// A method to insert the student details to the DB. Details such as class, section, roll_number, filename and ID
        /// </summary>
        /// <param name="fileName">multipart string containing class, section, roll_number, filename and ID seperated by _ </param>
        public void registerStudent(string fileName)
        {
            try
            {
                string[] getFields = fileName.Split('_');
                string studentID = getFields[0];
                string studentClass = getFields[1];
                string studentSection = getFields[2];
                string studentRollnumber = getFields[3];
                string fullFileName = fileName + ".png";

                //SQL command to get uniques class numbers
                MySqlCommand cmd = new MySqlCommand("INSERT INTO face_registration (filename,class,section,roll_no, id)  VALUES ('" + fullFileName + "','" + studentClass + "','" + studentSection + "','" + studentRollnumber + "'," + studentID + ")", conn);
                cmd.CommandType = CommandType.Text;
                executeNonQuery(cmd);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// A method to check for registered student
        /// </summary>
        /// <param name="id">ID of the student registering</param>
        /// <returns>Returns true if student already exists</returns>
        public Boolean checkForRegisteredStudent(string id)
        {
            //SQL command to get uniques class numbers
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM face_registration WHERE id = " + id, conn);
            cmd.CommandType = CommandType.Text;
            DataSet ds = getDataSet(cmd);
            Boolean hasRow = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                hasRow = true;
            }
            return hasRow;
        }

        /// <summary>
        /// Get the list of registered students for the selected class and section
        /// </summary>
        /// <param name="selectedClass">Selected class</param>
        /// <param name="selectedSection">Selected section</param>
        /// <returns>Returns Dataset consistingt of List of Registered Students for the selected class and section</returns>
        public DataSet getStudentsList(string selectedClass, string selectedSection)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM atharep_biom.osso_profile WHERE id IN (SELECT id FROM atharep_biom.face_registration WHERE class = '" + selectedClass + "' AND section = '" + selectedSection + "')", conn);
            cmd.CommandType = CommandType.Text;
            DataSet ds = getDataSet(cmd);
            return ds;
        }

        /// <summary>
        /// A method to delete the registered student
        /// </summary>
        /// <param name="studentID">ID of the student to be deleted</param>
        public void deleteStudentDetails(string studentID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM face_registration WHERE id = '" + studentID + "' ", conn);
                cmd.CommandType = CommandType.Text;
                DataSet ds = getDataSet(cmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string studentFileName = row["filename"].ToString();

                    //File Path and Filename
                    string fileNameWithPath = ConfigurationManager.AppSettings["RegisteredFacesPath"] + studentFileName;
                    
                    //If unsuccessful delete the saved image 
                    if (File.Exists(fileNameWithPath))
                    {
                        File.Delete(fileNameWithPath);
                    }
                }

                //SQL command to get uniques class numbers
                cmd = new MySqlCommand("DELETE FROM face_registration WHERE id = '" + studentID + "'", conn);
                cmd.CommandType = CommandType.Text;
                executeNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// Get the list of non-registered students for the selected class and section
        /// </summary>
        /// <param name="selectedClass">Selected class</param>
        /// <param name="selectedSection">Selected section</param>
        /// <returns>Returns Dataset consisting of List of Non-Registered Students for the selected class and section</returns>
        public DataSet getNonRegisteredStudentsList(string selectedClass, string selectedSection)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM atharep_biom.osso_profile WHERE class = '" + selectedClass + "' AND section = '" + selectedSection + "' AND id NOT IN (SELECT id FROM atharep_biom.face_registration WHERE class = '" + selectedClass + "' AND section = '" + selectedSection + "')", conn);
            cmd.CommandType = CommandType.Text;
            DataSet ds = getDataSet(cmd);
            return ds;
        }
    }



}