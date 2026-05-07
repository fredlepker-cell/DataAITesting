<%@ Page Language="VB" AutoEventWireup="false" CodeFile="index1.aspx.vb" Inherits="Index1" %>

<!DOCTYPE html>
<html lang="en">
  <head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>OUReports</title>
    <link
      href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css"
      rel="stylesheet"
      <%--integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD"
      crossorigin="anonymous"--%>
    />
    <script
      src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"
      <%--integrity="sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN"
      crossorigin="anonymous"--%>
    ></script>

    <script>
        // Initialize Bootstrap tooltips
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    </script>

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script
      async
      src="https://www.googletagmanager.com/gtag/js?id=G-K4GE50T22P"
    ></script>
    <script>
      window.dataLayer = window.dataLayer || [];
      function gtag() {
        dataLayer.push(arguments);
      }
      gtag("js", new Date());

      gtag("config", "G-K4GE50T22P");
    </script>

    <style>
      body {
        background-color: var(--primary-color);
      }

      table td,
      table th {
        color: var(--text-color);
      }

      .smaller-font {
        font-size: 11px;
      }

      .navbar-nav .nav-link {
        background-color: var(--primary-color);
        color: var(--text-color);
      }

      .nav-link:hover{
        background-color: var(--secondary-color);
          transition: 0s;
      }

      .nav-item:hover .dropdown-menu {
          display: block;
          margin-top: 0; 
      }

      .dropdown-menu {
          background-color: var(--secondary-color);
          border-radius: 0;
      }

      .dropdown-item {
        color: var(--text-color);
      }

      .hovBtn{
          background-color: #0fb600;
          transition: 0.5s;
      }

      .hovBtn:hover{
          background-color: green;
          transform: scale(1.05);
      }

      :root {
        --primary-color: #fff;
        --secondary-color: lightgray;
        --tertiary: darkslategray;
        --fourth: #f4f4f4;
        --text-color: black;
      }

      .dark-theme {
        --primary-color: black;
        --secondary-color: darkslategray;
        --tertiary: limegreen;
        --fourth: black;
        --text-color: white;
      }

      #icon {
        width: 30px;
        cursor: pointer;
      }

      nav ul {
        flex: 1;
        text-align: right;
      }

      @media only screen and (max-width: 1000px) {
          .col-3 {
              width: 90%;
              margin: 0;
          }

          .col-6 {
              width: 90%;
          }
          
          .navbar {
              display: block;
              justify-content: flex-end;
              font-size: 45%;
          }

          .navbar .me-5{
              width: 0;
          }

          .table{
              margin: -10rem -3rem;
              transform: scale(75%);
          }
      }

        @media only screen and (max-width: 768px) {

            .hidden {
                display: none;
            }

            .btn {
                font-size: 16px;
                padding: 10px;
            }
            .pic {
                display: none;
            }
            .youtube {
                display: flex;
                height: auto;
                width: 90%;
            }

            .right{
                width: 90%;
            }

            .rightBtn {
                --bs-gutter-x: -1.5rem;
            }
        }

        #tooltip{
            position:relative;
            cursor:pointer;
            border-bottom:2px dotted;
            padding:7px;
            font-size:25px;
            font-weight:bold;
            background-color:#000
            color:#fff;
            white-space:nowrap;
            padding:10px 15px;
            border-radius:7px;
            visibility:hidden;
            opacity:0;
            transition:opacity 0.5s ease;
        }
        #tooltipText{
            position:absolute;
            left:50%;
            top:auto;
            transform:translateX(-50%);
            border:15px solid;
            border-color:#000 #0000 #0000 #0000;
        }
        #tooltip:hover #tooltipText{
            top: -130%;
            visibility: visible;
            opacity:1
        }

    </style>
  </head>
  <body>
    <div class="container-fluid">
      <!-- Navbar -->
      <nav class="navbar navbar-expand ms-5">
        <!-- Title -->
        <a class="hidden navbar-brand ms-5">
               <span id="siteseal"><script async type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=SYhDKXg2IT7QXHzPEW7z7tmavANkr8vMDCiRmZvbKczmKBJ5Wj8eKl1EX00B"></script></span>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </a>
    
        <!-- Navbar links -->
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item dropdown">
              <a
                class="nav-link me-5"
                href="#"
                id="brandDropdown"
                data-bs-toggle="dropdown"
                style="color: var(--text-color); font-family: Times New Roman;"
              >
                OUReports
              </a>
              <div class="dropdown-menu" aria-labelledby="brandDropdown">
                <a
                  class="dropdown-item"
                  href="https://www.oureports.net/OUReports/OUReportsOverview.pdf"
                    Target="_blank"
                  >OUReports Overview</a
                >
                 <a
                  class="dropdown-item"
                  href="https://www.oureports.net/OUReports/comparison.aspx"
                    Target="_blank"
                  >Comparison</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/AboutUs.aspx" Target="_blank"
                  >About Us</a
                >
              
              </div>
            </li>
            <li class="nav-item dropdown">
              <a 
                class="nav-link dropdown-toggle"
                href="#"
                id="productsDropdown"
                role="button"
                data-bs-toggle="dropdown"
                style="color: var(--text-color)"
              >
                Products
              </a>
              <div class="dropdown-menu" aria-labelledby="productsDropdown">
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/ShowTestingSiteProposal.aspx" Target="_blank"
                  >OUReports Testing Site</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Index3.aspx"
                  >OUReports Services</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/IndexSoftware.aspx"
                  >OUReports Software</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/HelpDesk/Default.aspx"
                  >OUReports Project Manager - Free</a
                >
              </div>
            </li>
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                id="customersDropdown"
                role="button"
                data-bs-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
                style="color: var(--text-color)"
              >
                Customers
              </a>
              <div class="dropdown-menu" aria-labelledby="customersDropdown">
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Registration.aspx"
                  >Individual</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/UnitRegistration.aspx?org=company"
                  >Company</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/RunReport/OUReportsAgents.aspx"
                  >Sales Agent</a
                >
              </div>
            </li>
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                id="useCasesDropdown"
                role="button"
                data-bs-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
                style="color: var(--text-color)"
              >
                Use Cases
              </a>
              <div class="dropdown-menu" aria-labelledby="useCasesDropdown">
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/default.aspx?srd=30&dash=yes&lgn=d720202024346P906" Target="_blank"
                  >Covid 2020 Dashboard

                </a>
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/DashboardHelp.pdf" Target="_blank"
                  >Health Care</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/UseCasePublic.aspx" Target="_blank"
                  >Public Data</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/ExploreData.pdf" Target="_blank"
                  >Explore Data</a
                >
                <a
                  class="dropdown-item"
                  href="https://www.oureports.net/OUReports/MapDefinitionDocumentation.pdf" Target="_blank"
                  >Google Earth and Map Generator</a
                >
               
                <a
                  class="dropdown-item"
                  href="https://www.oureports.net/OUReports/Tornadoes.pdf" Target="_blank"
                  >Google Earth and Maps: Tornadoes</a
                >
                  <a
                  class="dropdown-item"
                  href="https://www.oureports.net/OUReports/ExploreDataAndDataAnalytics.pdf" Target="_blank"
                  >Explore Data and Data Analytics</a
                > 
                  <a
                  class="dropdown-item"
                  href="https://www.oureports.net/OUReports/MatrixBalancingYouthTobaccoUsage.pdf" Target="_blank"
                  >Matrix Balancing - Youth Tobacco Usage</a
                > 
                  <a
                  class="dropdown-item"
                  href="https://www.oureports.net/OUReports/WorldHappinessScoresIn2019.pdf" Target="_blank"
                  >World_Happiness_Scores_In_2019</a
                > 





              </div>
            </li>
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                id="docsAndVid"
                role="button"
                data-bs-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
                style="color: var(--text-color)"
              >
                Docs and Videos
              </a>
              <div class="dropdown-menu" aria-labelledby="docsAndVidn">
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/OnlineUserReporting.pdf" Target="_blank"
                  >PDF: General Documentation</a
                >
                 <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/AdvancedReportDesigner.pdf#page=3" Target="_blank"
                  >PDF: Advanced Report Designer </a
                >
                 <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/GoogleChartsAndDashboards.pdf" Target="_blank"
                  >PDF: Charts and Dashboards</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/MatrixBalancing.pdf" Target="_blank"
                  >PDF: Matrix Balancing</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/MatrixBalancingSamples.pdf" Target="_blank"
                  >PDF: More Matrix Balancing SamplesPublic Data</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/MapDefinitionDocumentation.pdf" Target="_blank"
                  >PDF: Documentation on making Google Maps and Google Earth
                  reports</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/DataImport.mp4" Target="_blank"
                  >Video: DataAI - Data Analytics and Instant Reporting</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/zoom_2.mp4" Target="_blank"
                  >Video: Charts, maps, and Dashboards</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/QuickStart.mp4" Target="_blank"
                  >Video: Quick Start(only email needed)</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/UserRegistrationVideo.mp4" Target="_blank"
                  >Video: Individual Registration, user database</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/RegOurDb.mp4" Target="_blank"
                  >Video: Individual Registration, use our database</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/UnitRegistrationVideo.mp4" Target="_blank"
                  >Video: Company Registration</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/InputFromAccess.mp4" Target="_blank"
                  >Video: Input from Access</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/MatrixBalance.mp4" Target="_blank"
                  >Video: Matrix Balancing</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/MatrixBalance1a1b.mp4" Target="_blank"
                  >Video: Matrix Balancing Scenarios 1a, 1b</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/MatrixBalance2a3a.mp4" Target="_blank"
                  >Video: Matrix Balancing Scenarios 2a, 3a</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/MatrixBalance2b2c.mp4" Target="_blank"
                  >Video: Matrix Balancing Scenarios 2b, 2c</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/MatrixBalance3b3c.mp4" Target="_blank"
                  >Video: Matrix Balancing Scenarios 3b, 3c</a
                >
                <a
                  class="dropdown-item"
                  href="https://oureports.net/OUReports/Videos/MatrixBalance4a4b4c.mp4" Target="_blank"
                  >Video: Matrix Balancing Scenarios 4a, 4b, 4c</a
                >
              </div>
            </li>
            <li class="nav-item">
              <a
                class="nav-link"
                href="https://oureports.net/OUReports/OUReportsTraining.pdf"
                style="color: var(--text-color)"
                   Target="_blank"
                >Self Training</a
              >
            </li>
            <li class="nav-item">
              <a
                class="nav-link"
                href="https://oureports.net/OUReports/ContactUs.aspx"
                style="color: var(--text-color)"
                >Contact Us</a
              >
            </li>
            <li class="nav-item">
              <a
                class="nav-link"
                href="https://oureports.net/OUReports/LandingPage.pdf"
                style="color: var(--text-color)"
                   Target="_blank"
                >                      Guide to this page</a
              >
            </li>
              <!-- Light/Dark mode toggle -->
          </ul>
          <form class="d-flex">
            <div class="form-check form-switch mt-2 ms-5" id="icon">
              <input
                class="form-check-input"
                type="checkbox"
                id="flexSwitchCheckDefault"
                data-bs-toggle="tooltip"
                data-bs-placement="top"
                title="Toggle Light/Dark Mode"
              />
              <label
                class="form-check-label"
                for="flexSwitchCheckDefault"
              ></label>
            </div>
          </form>
        </div>
      </nav>

      <div class="row" style="background-color: var(--fourth)">
        <div class="col-1"></div>
        <div class="col-6">
          <div class="row mt-5">
            <h1 style="color: var(--tertiary); font-size: 300%">
              <asp:Label ID="LabelPageTtl" runat="server" Text="Online User Reporting"></asp:Label>
            </h1>
          </div>
          <div class="row my-0">
            <h2 style="color: var(--tertiary); font-size: 250%"></h2>
          </div>
          <div class="row mb-5">
            <p style="color: var(--tertiary); font-size: 100%">
              Automatically analyzes data in your existing database or your
              file, and generates reports, charts, maps, dashboards, provides
              interface for ad hoc reports and statistical research, matrix
              balancing, makes the creation and processing of reports
              convenient, simple, and accessible for end users and
              administrators alike.
            </p>
          </div>
        </div>
        <div class="hidden col m-5">
          <img src="graph.png" alt="" style="width: 370px; height: 220px" />
        </div>       
      </div>

      <!-- Big Buttons -->
      <div class="row mt-4">
        <div class="col"></div>
        <div class="col-3 m-4 me-5">
          <a
            href="https://oureports.net/OUReports/QuickStart.aspx"
            class="btn hovBtn"
            role="button"
          >
            <div class="row mt-4">
              <h2 style="color: var(--primary-color)">Quick Start</h2>
            </div>
            <div class="row">
              <img src="Flag.png" alt="" style="width: 200px; height: 150px" />
            </div>
          </a>
        </div>
        <div class="col-3 m-4 me-5">
          <a
            href="https://oureports.net/OUReports/index3.aspx"
            class="btn hovBtn"
            role="button"
          >
            <div class="row mt-4">
              <h2 style="color: var(--primary-color)">Registration</h2>
            </div>
            <div class="row" style="text-align: center">
              <img
                src="Envelope.png"
                alt=""
                style="width: 200px; height: 150px"
              />
            </div>
          </a>
        </div>
        <div class="col-3 m-4">
          <a
            href="https://oureports.net/OUReports/Default.aspx"
            class="btn hovBtn"
            role="button"
          >
            <div class="row mt-4">
              <h2 style="color: var(--primary-color)">Sign In</h2>
            </div>
            <div class="row">
              <img
                src="SignIn.png"
                alt=""
                style="width: 200px; height: 150px"
              />
            </div>
          </a>
        </div>
      </div>

      <!--Second Page -->
   <form runat="server">
      <div class="row mt-4" style="background-color: var(--primary-color)">
        <div class="hidden col-1"></div>
         
          <div class="right col-3 py-5" style="background-color: var(--primary-color)">
          <%--<div class="row m-4">--%>
              <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://oureports.net/oureportspg/ExploreData.pdf" Target="_blank" Font-Bold="True" Font-Italic="True" Font-Size="Medium">How to play in Sandbox</asp:HyperLink>
<%--</div>--%>
                
           <div class="rightBtn hovBtn row my-5 my-5 ms-5" style="border-radius: 8px; width:420px">             
            <asp:Button
            id="ButtonPlayCinema"
            runat="server"
            Text="Sandbox"
            CssClass="btn"
            style="color: var(--primary-color); font-size:200%; text-align: center;">
            </asp:Button>
          </div>
               <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="https://oureports.net/oureportspg/UseCasePublic.aspx" Target="_blank" Font-Bold="True" Font-Italic="True" Font-Size="Medium">How to play with Analytics</asp:HyperLink>
                
              , 
              
                 <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="https://oureports.net/OUReports/GoogleChartsAndDashboards.pdf" Target="_blank" Font-Bold="True" Font-Italic="True" Font-Size="Medium">Charts</asp:HyperLink>
                   
              , 
         <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="https://www.oureports.net/OUReports/MapDefinitionDocumentation.pdf" Target="_blank" Font-Bold="True" Font-Italic="True" Font-Size="Medium">Maps</asp:HyperLink>
         
          <div class="rightBtn hovBtn row my-5 ms-5" style="border-radius: 8px; width:420px">
              <asp:Button
              id="ButtonPlayMaps"
              runat="server"
              Text="Analytics, Charts, and Maps"              
              CssClass="btn"
              class ="hovBtn"
              style="color: var(--primary-color); font-size:200%; text-align: center; border-radius: 8px;">
            </asp:Button>
          </div>
          <!-- Extra Button if want to add later
          <div class="rightBtn hovBtn row my-5 ms-5" style="background-color: #0fb600; border-radius: 8px;">
            <asp:Button
            id="Button1"
            runat="server"
            Text="BUTTON TITLE GOES HERE"
            CssClass="btn"
            style="color:var(--primary-color); font-size: 200%; text-align: center;">
            </asp:Button>
          </div>
          -->
          <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="https://oureports.net/HelpDesk/TaskList.pdf" Target="_blank" Font-Bold="True" Font-Italic="True" Font-Size="Medium">How to use the OUReports Project Manager</asp:HyperLink>
          <br />
              <div class="rightBtn hovBtn row my-5 ms-5" style="border-radius: 8px; width:420px">
            <asp:Button
            id="ButtonProjectManager"
            runat="server"
            Text="Project Manager"
            CssClass="btn"
            class ="hovBtn"
            style="color:var(--primary-color); font-size: 200%; text-align: center;"
            >
            </asp:Button>
          </div>
        </div>

        <!-- Right Half(YouTube) -->
        <div class="col-8">
           <div class="row m-4">
            <a
              style="text-align: center; font-size: 175%;"
              href="https://www.youtube.com/@oureports3989"
              >OUReports Youtube Channel</a>
           
          </div>
            
          <div class="row m-4">
            <div class="col-2"></div><%--<div class="col-2"></div>--%>

            <div class="youtube col-4">
              
              OUReports Video Demonstration  <br />
              <iframe style="border: 10px solid var(--tertiary);"
                width="600"
                height="400"
                src="https://www.youtube.com/embed/RXfEXEDOy2w"
                  <%--src="https://www.youtube.com/channel/UCcsTziBCeV47Njs1qN0RuQg"--%>
              >
                <!-- replace ID after /embed/ to desired YouTube video ID -->
              </iframe>   
                <br />
                <%--https://www.oureports.net/OUReports/--%>
                 <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="https://www.oureports.net/OUReports/Comparison.aspx" Target="_blank" Font-Bold="True" Font-Italic="True" Font-Size="Medium">Comparison of OUReports features</asp:HyperLink>
         
            </div>
              
          </div>
         
          
        </div>
      </div>
     
     
      <!-- Left Half -->
     <%-- <div class="row" style="background-color: var(--primary-color)">
        <div class="hidden col-1" style="background-color: var(--primary-color)"></div>
        <!-- Screenshots -->
        <div class="pic col-4" style="background-color: var(--primary-color)">
          <div class="row m-4">
            <img
              src="Registration.png"
              alt=""
              style="border: solid var(--fourth); width: 425px; height: 300px"
            />
          </div>
          <div class="row m-4"></div>
          <div class="row m-4">
            <img
              src="CovidDash.png"
              alt=""
              style="border: solid var(--fourth); width: 425px; height: 300px"
            />
          </div> 
         
          
     </div>--%>

<!-- Table -->
      <%--  <div class="col-7 my-4" style="color: var(--text-color)">

           <div >
                    <asp:Label ID="Label5" runat="server" BackColor="White" Font-Bold="True" Font-Italic="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="#999999" Height="35px" Text="Comparison of OUReports features: "></asp:Label>
          
                <asp:GridView ID="GridView1" runat="server" AllowSorting="False" BackColor="WhiteSmoke" Font-Names="Arial"  Font-Size="X-Small" AllowPaging="True" PageSize="30" BorderWidth="0">
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <RowStyle BackColor="White" />
                </asp:GridView>

          </div>
 
     </div>--%>
      <!-- End of container -->
</form>
    </div>
 
    <!-- Script for light/dark mode switch -->
    <script>
      var icon = document.getElementById("icon");
      icon.onclick = function () {
        document.body.classList.toggle("dark-theme");
      };
    </script>
  </body>
</html>
