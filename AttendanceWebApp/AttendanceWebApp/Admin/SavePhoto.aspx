<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SavePhoto.aspx.cs" Inherits="AttendanceWebApp.Admin.SavePhoto" EnableEventValidation="false" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String source = Request.Form["photo"];
            // remove extra chars at the beginning
            source = source.Substring(source.IndexOf(",") + 1);
            byte[] data = Convert.FromBase64String(source);
            string filename = Request.Form["title"];
            string imagePath = @"D:\Git\VisioIngenii_GIT\VRSOHA-AttendanceSystem\AttendanceWebApp\AttendanceWebApp\Assets\" + filename + ".jpg";
            var fs = new BinaryWriter(new FileStream(@"D:\\Git\\VisioIngenii_GIT\\VRSOHA-AttendanceSystem\\AttendanceWebApp\\AttendanceWebApp\\Assets\\" + filename + ".jpg", FileMode.Append, FileAccess.Write));
            fs.Write(data);
            fs.Close();
            Boolean confidenceGood = checkFaceConfidence(imagePath);
            Response.Write(confidenceGood);
        }
        catch (Exception ex)
        {
            Trace.Write(ex.Message);
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
