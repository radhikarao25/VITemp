<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Home.Master" AutoEventWireup="true" CodeBehind="RegisteredStudents.aspx.cs" Inherits="AttendanceWebApp.Admin.RegisteredStudents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <!--BEGIN TITLE & BREADCRUMB PAGE-->
        <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
            <div class="page-header pull-left">
                <div class="page-title">
                    Registered Students
                </div>
            </div>
            <ol class="breadcrumb page-breadcrumb pull-right">
                <li><i class="fa fa-home"></i>&nbsp;<a href="Registration.aspx">Home</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
                <li class="hidden"><a href="RegisteredStudents.aspx">Registered Students</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
                <li class="active">Registered Students</li>
            </ol>
            <div class="clearfix">
            </div>
        </div>
        <!--END TITLE & BREADCRUMB PAGE-->
        <!--BEGIN CONTENT-->
        <div class="page-content">
            <div id="tab-general">
                <div>
                    <div class="col-sm-6 col-md-6">
                        <div class="panel panel-red">
                            <div class="panel-heading">Search Panel</div>
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
                                
                            </div>
                        </div>
                        
                    </div>
                    
                    <div class="col-sm-12 col-md-12">
                    <div class="panel panel-yellow">
                            <div class="panel-heading">List of Registered Students</div>
                            <asp:Label ID="defaultLabel" runat="server" Text="Please select a class and section"></asp:Label>
                            <div class="panel-body">
                                <asp:ListView ID="registerStudentsList" runat="server" ItemPlaceholderID="placeHolder1" OnItemDeleting="registerStudentsList_ItemDeleting">
                                    <ItemTemplate>
                                        <tr>
                                            <td><asp:HiddenField ID="studentID" runat="server" Value='<%#Eval("id") %>' /> <%# Container.DataItemIndex + 1 %></td>
                                            <td><asp:Label runat="server" Text='<%#Eval("name") %>'></asp:Label></td>
                                            <td><asp:Label runat="server" Text='<%#Eval("class") %>'></asp:Label></td>
                                            <td><asp:Label runat="server" Text='<%#Eval("section") %>'></asp:Label></td>
                                            <td><asp:Label runat="server" Text='<%#Eval("roll_no") %>'></asp:Label></td>
                                            <td><asp:Label runat="server" Text='<%#Eval("gender") %>'></asp:Label></td>
                                            <td><asp:Button CssClass="btn btn-danger btn-sm" ID="DeleteButton" runat="server" CommandName="Delete"
                                                Text="Delete" />
</td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        No students to show
                                    </EmptyDataTemplate>
                                    <EmptyItemTemplate>
                                        No Students to show
                                    </EmptyItemTemplate>
                                    <LayoutTemplate>
                                        <table class="table table-hover">
                                    <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Name</th>
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Roll Number</th>
                                        <th>Gender</th>
                                        <th>Action</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="placeHolder1" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                            </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                                
                            </div>
                        </div>
                        </div>
                </div>
            </div>
        </div>
        <!--END CONTENT-->
        <!--BEGIN FOOTER-->
        <script src="https://code.jquery.com/jquery-1.10.2.js" integrity="sha256-it5nQKHTz+34HijZJQkpNBIHsjpV8b6QzMJs9tmOBSo="  crossorigin="anonymous"></script>
        
        <div id="footer">
            <div class="copyright">
                <a href="http://www.visioingenii.com">2017 © Visio Ingenii Ltd. Powered by Visio Ingenii Ltd</a>
            </div>
        </div>
        <!--END FOOTER-->
    </div>
</asp:Content>

