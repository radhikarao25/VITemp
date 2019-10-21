using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AttendanceWebApp.Admin
{
    public partial class SavePhoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }




        public Boolean checkFaceConfidence(string imagePath)
        {
            //D:\Official\VisioIngenii\Samples\UASFaceDetection\UASFaceDetection\UASFaceDetection.exe

            string strCommand = @"D:\Official\VisioIngenii\Samples\UASFaceDetection\UASFaceDetection\UASFaceDetection.exe";
            string strArguments = imagePath.Replace(@"\",@"/");
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

            return false;
        }
    }
}
