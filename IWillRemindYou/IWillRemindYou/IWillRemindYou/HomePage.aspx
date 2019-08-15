<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="IWillRemindYou.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
        
        <nav class="nav nav-pills" role="navigation">
                <div class="container">
                    <!-- Brand and toggle get grouped for better mobile display -->
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                            <span class="fa fa-bars fa-lg"></span>
                        </button>
                        <a class="navbar-brand" href="#">
                            <b>IWillRemindYou</b>
                        </a>
                    </div>
                    <div align="right">
                        <input type="button" class="btn btn-danger"  value="Sign Up" /> &nbsp;&nbsp;
                         <input type="button" class="btn btn-primary"  value="Log In" />
                    </div>
                </div>
                <!-- /.container-->
               <!-- Collect the nav links, forms, and other content for toggling -->
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">

                        <ul class="nav navbar-nav navbar-right">
                             <li><a href="#Home">Home</a>
                            </li>
                            <li><a href="#about">about</a>
                            </li>
                            <li><a href="#features">features</a>
                            </li>
                            <li><a href="#reviews">reviews</a>
                            </li>
                        
                        </ul>
                    </div>
                    <!-- /.navbar-collapse -->
        </nav>
            </header>


        <div class="row">
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-6">
                        <img src="Images/pay-bills-20875243.jpg" height="270%" width="110%" />
                    </div>
                    <div class="col-md-6">
                        Some text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text Here
                    </div>
                </div>

            </div>
            <div class="col-md-4">
                
                <div class="row">
                    <div class="col-md-6">
                        <img src="Images/couple-celebrating-anniversary.jpg" height="270%" width="110%" />
                    </div>
                    <div class="col-md-6">
                        Some text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text Here
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                    <div class="row">
                    <div class="col-md-6">
                        <img src="Images/happy_birthday_by_babsdraws-d61xnoe.png" height="270%" width="110%" />
                    </div>
                    <div class="col-md-6">
                        Some text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text HereSome text Here
                    </div>
                </div>
            </div>
          </div>


         <footer>
            <div align="Center" class="container">
                <a href="#" class="scrollpoint sp-effect3">
                    <img src="http://chickasawjournal.com/wp-content/blogs.dir/36/files/2014/04/calendar-events-logo.jpg" height="50px" width="50px" alt="Logo" class="logo">
                </a>
                <div align="Center">
                    <p>Copyright &copy; 2015</p>
                    <p>Template by <a href="#" target="_blank">IWillRemindU</a></p>
                </div>
            </div>
        </footer>

    </form>
</body>
</html>
