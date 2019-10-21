using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AttendanceWebApp.App_Code;
using System.Configuration;

namespace AttendanceWebApp.Admin
{
    [ScriptService]
    public partial class ImageSave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Save the Image captured from the webcam, check for Image Validity, Face Found and Confidence Score for Training Images registration
        /// </summary>
        /// <param name="imageData">Webcam byte stream as an string to be converted to required format image</param>
        /// <param name="filename">Name of the file for future reference</param>
        /// <returns>returns the result of the image captured - successful or unsuccessful</returns>
        [WebMethod()]
        public static string UploadImage(string imageData, string filename)
        {
            string result = "Unsuccessful";
            try
            {
                //File Path and Filename
                string fileNameWithPath = ConfigurationManager.AppSettings["RegisteredFacesPath"] + filename + ".png";

                //Process the string and convert into a PNG file
                using (FileStream fs = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(imageData);
                        bw.Write(data);
                        bw.Close();
                    }
                }

                //Check for Image validity, face detected and confidence score
                if (checkFaceConfidence(fileNameWithPath))
                {
                    result = "Successful";

                    //If successful write the details of the student onto the DB  
                    DBOps dbops = new DBOps();
                    dbops.registerStudent(filename);
                }
                else
                {
                    //If unsuccessful delete the saved image 
                    if (File.Exists(fileNameWithPath))
                    {
                        File.Delete(fileNameWithPath);
                    }
                }
            }
            catch (Exception ex)
            {
                //Trace.Write(ex.Message); 
                //return ex.Message;
            }
            return result;
        }

        /// <summary>
        /// A method to run UASFaceDetection Exe by passing the image path as an argument to check if the image is valid, face detected and confidence score
        /// </summary>
        /// <param name="imagePath">The path of the image</param>
        /// <returns>Return true if all the conditions are met by the registered image</returns>
        public static Boolean checkFaceConfidence(string imagePath)
        {
            string strCommand = ConfigurationManager.AppSettings["FDModule"];
            string strArguments = imagePath.Replace(@"\", @"/");
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = strCommand;
            p.StartInfo.Arguments = strArguments;
            p.Start();

            string output = "";
            Boolean result = false;
            int ch;
            try
            {
                //wait for the result from the exe
                while ((ch = p.StandardOutput.Read()) >= 0)
                {
                    output = output + ((char)ch).ToString();
                    //Terminate the application UASFaceDetection on recieving the end of the result string
                    if (((char)ch).ToString() == "]")
                        p.Kill();
                }
                

                output = output.Replace(@"\n", "").Replace(@"\r", "").Replace(@"\", "");
                //Convert the result into a JSON Object
                JObject json = JObject.Parse(output);

                //get the values from the returned JSON object
                if (json["result"] != null)
                {
                    JToken jsonData = json["result"];
                    string imageValid = Convert.ToString(jsonData[0][@"image"]);
                    double confidence = Convert.ToDouble(jsonData[0][@"confidence"]);
                    bool faceDetected = Convert.ToBoolean(jsonData[0][@"faceDetected"]);

                    //Check for image validity
                    if (imageValid == "Valid")
                    {
                        //Check if face is detected in the image
                        if (faceDetected)
                        {
                            //Check if the confidence score is more than 0.50
                            if (confidence > 0.50)
                                //If all the conditions are satisfied returm true else false
                                result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}