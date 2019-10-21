<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Home.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AttendanceWebApp.Admin.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dashboard | Dashboard</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <!--BEGIN TITLE & BREADCRUMB PAGE-->
        <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
            <div class="page-header pull-left">
                <div class="page-title">
                    Registration
                </div>
            </div>
            <ol class="breadcrumb page-breadcrumb pull-right">
                <li><i class="fa fa-home"></i>&nbsp;<a href="Registration.aspx">Home</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
                <li class="hidden"><a href="Registration.aspx">Registration</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
                <li class="active">Dashboard</li>
            </ol>
            <div class="clearfix">
            </div>
        </div>
        <!--END TITLE & BREADCRUMB PAGE-->
        <!--BEGIN CONTENT-->
        <asp:HiddenField runat="server" id="filenameField"></asp:HiddenField>
        <asp:HiddenField runat="server" id="studentID"></asp:HiddenField>
        <div class="page-content">
            <div id="tab-general">
                <div>
                    <div class="col-sm-4 col-md-4">
                        <div class="panel panel-red">
                            <div class="panel-heading">Enrol Student Details</div>
                            <div class="panel-body form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Class</label>
                                    <div class="col-sm-8 controls">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:DropDownList CssClass="form-control" ID="ddlClasses" runat="server" OnSelectedIndexChanged="ddlClasses_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Section</label>
                                    <div class="col-sm-8 controls">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:DropDownList CssClass="form-control" ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Roll Number</label>
                                    <div class="col-sm-8 controls">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:DropDownList CssClass="form-control" ID="ddlRollNumber" runat="server" OnSelectedIndexChanged="ddlRollNumber_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-yellow">
                            <div class="panel-heading">Student Details</div>
                            <div class="panel-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-12 controls">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:Label CssClass="control-label" ID="studentName" runat="server">
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12 controls">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:Label CssClass="control-label alert-danger" ID="alreadyExistsLabel" runat="server">
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12 controls">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div id="next">
                                                    <asp:Button ID="nextRegistration" CssClass="btn btn-primary btn-block" runat="server" Text="Next Registration" OnClick="btnCompleteRegistration_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4">
                    <div class="panel panel-violet">
                        <div class="panel-heading">Webcam</div>
                            <div class="panel-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-12 controls">
                                        <div class="row">
                                            <div class="col-xs-12 ">
                                                <asp:Panel ID="videoModule" runat="server" style="text-align: center;">
                                                    <video class="col-xs-12" id="video" autoplay></video>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12 controls">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:Button ID="btnSave1" CssClass="btn btn-primary btn-block" runat="server" OnClientClick="takePhoto1(event)" Text="Click Photo" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    <div class="col-sm-4 col-md-4">
                        <div class="panel panel-pink">
                            <div class="panel-heading">Photos taken</div>
                            <div class="panel-body form-horizontal">
                                <asp:Panel ID="cameraModule" runat="server" style="text-align:center;">
                                   <canvas id="canvas1" ></canvas>
                                </asp:Panel>                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--END CONTENT-->
        <!--BEGIN FOOTER-->
        <script src="https://code.jquery.com/jquery-1.10.2.js" integrity="sha256-it5nQKHTz+34HijZJQkpNBIHsjpV8b6QzMJs9tmOBSo="  crossorigin="anonymous"></script>
        <script>
            var canvas1, context1, video, videoObj, img1, verify;

            $(function () {
                canvas1 = document.getElementById("canvas1");
                context1 = canvas1.getContext("2d");
                //img1 = document.getElementById("img1");
                video = document.getElementById("video");
                            
                // different browsers provide getUserMedia() in differnt ways, so we need to consolidate 
                navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;
                if (navigator.getUserMedia) {
                    navigator.getUserMedia(
                        { video: true }, // which media
                        function (stream) {   // success function
                            video.src = window.URL.createObjectURL(stream);
                            video.onloadedmetadata = function (e) {
                                video.play();
                                //video.height = (video.videoHeight/2);
                                //video.width = (video.videoWidth/2);
                                canvas1.height = (video.videoHeight);
                                canvas1.width = (video.videoWidth);
                                             
                            };
                        },
                        function (err) {  // error function 
                            console.log("The following error occured: " + err.name);
                        }
                    );
                }
                else {
                    console.log("getUserMedia not supported");
                }
            });

            function takePhoto1(e) {
                e.preventDefault();
                context1.drawImage(video, 0, 0, video.videoWidth, video.videoHeight);
                var image = canvas1.toDataURL("image/png");
                var filename = document.getElementById('<%=filenameField.ClientID %>').value;
                image = image.replace('data:image/png;base64,', '');

                $.ajax({
                    type: 'POST',
                    url: 'ImageSave.aspx/UploadImage',
                    data: '{ "imageData" : "' + image + '", "filename" : "' + filename + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    error: function (request, status, error) {
                        //alert(request.responseText);
                        alert("Big Image");
                    },
                    success: function (msg) {
                        var message = $.trim(msg.d);
                        //alert(message);
                        if (message === "Successful") {
                            alert("Successful! Please proceed with the next registration.");
                            window.location.href = "Registration.aspx";
                        }
                        else {
                            alert("Unsuccessful! Please take the photo again.");
                        }
                    }
                });
            }
        </script>
        <div id="footer">
            <div class="copyright">
                <a href="http://www.visioingenii.com">2017 © Visio Ingenii Ltd. Powered by Visio Ingenii Ltd</a>
            </div>
        </div>
        <!--END FOOTER-->
    </div>
</asp:Content>
