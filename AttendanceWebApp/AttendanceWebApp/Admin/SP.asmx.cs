using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AttendanceWebApp.Admin
{
    /// <summary>
    /// Summary description for SP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SP : System.Web.Services.WebService
    {

        [WebMethod]
        public static string HelloWorld(string imageData)
        {
            try
            {
                
                //byte[] data = Convert.FromBase64String(photo);
                string filename = "test";
                string fileNameWitPath = @"D:\Git\VisioIngenii_GIT\VRSOHA-AttendanceSystem\AttendanceWebApp\AttendanceWebApp\Assets\" + filename + ".png";
                using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(imageData);
                        bw.Write(data);
                        bw.Close();
                    }

                }
                Boolean confidenceGood = checkFaceConfidence(fileNameWitPath);
                if (confidenceGood == true)
                    return "Hello World T";
                else
                    return "Hello World F";
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            return "Hello World O";

        }

        
        public static Boolean checkFaceConfidence(string imagePath)
        {
            //D:\Official\VisioIngenii\Samples\UASFaceDetection\UASFaceDetection\UASFaceDetection.exe

            string strCommand = @"D:\Official\VisioIngenii\Samples\UASFaceDetection\UASFaceDetection\UASFaceDetection.exe";
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
            int ch;

            while ((ch = p.StandardOutput.Read()) >= 0)
            {
                output = output + ((char)ch).ToString();
                if (((char)ch).ToString() == "]")
                    p.Kill();
            }
            output = output + "}";
            JObject json = JObject.Parse(output);
            //while (!p.StandardOutput.EndOfStream)
            //{
            //    string line = p.StandardOutput.ReadLine();
            //    // do something with line
            //}

            return true;
        }
    }
}
