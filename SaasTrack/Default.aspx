<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SaasTrack._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- HOME -->
  <section class="home bg-img-1">
    <div class="bg-overlay"></div>
    <div class="container">
      <div class="row">
        <div class="col-sm-12">
          <div class="home-wrapper text-center">
            <h1 class="animated fadeInDown wow" data-wow-delay=".1s">Shine on a light on your company subscriptions</h1>
            <p class="animated fadeInDown wow" data-wow-delay=".2s">Take control of your subscription spend by identifying, tracking, and cancelling unused subscriptions</p>
            <a href="account/register" class="btn btn-primary btn-rounded w-lg animated fadeInDown wow" data-wow-delay=".4s">Try it now</a>
            <div class="clearfix"></div>
            
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- END HOME -->


  <!-- SERVICES -->
  <section class="section" id="how-it-work">
    <div class="container">

      <div class="row">
        <div class="col-sm-12 text-center">
          <h2 class="title zoomIn animated wow" data-wow-delay=".1s">How It Works ?</h2>
          <p class="sub-title zoomIn animated wow" data-wow-delay=".2s">With thousands of on-demand services, apps, and online businesses, it's easy to lose sight of where your money is being spent. We make it easy to know what you’re paying for and how much you’re spending.</p>
        </div> 
      </div>

      <div class="row">
        <div class="col-sm-4">
          <div class="service-item animated fadeInLeft wow" data-wow-delay=".1s">
            <i class=" ti-desktop"></i>
            <div class="service-detail">
              <h4>Find & Monitor subscriptions</h4>
              <p>Understand exactly what subscription charges you’re receiving every month. Find ones you didn't know you had.</p>
            </div> <!-- /service-detail -->
          </div> <!-- /service-item -->
        </div> <!-- /col -->

        <div class="col-sm-4">
          <div class="service-item animated fadeInDown wow" data-wow-delay=".3s">
            <i class=" ti-shield"></i>
            <div class="service-detail">
              <h4>Cancel Unused Subscriptions</h4>
              <p>Request Beam to cancel any unwanted subscriptions. We’ll manage the process for you.</p>
            </div> <!-- /service-detail -->
          </div> <!-- /service-item -->
        </div> <!-- /col -->

        <div class="col-sm-4">
          <div class="service-item animated fadeInRight wow" data-wow-delay=".5s">
            <i class=" ti-server"></i>
            <div class="service-detail">
              <h4>Get alerts for new subscriptions</h4>
              <p>We'll send you an alert when you have a new subscription. No more unwanted spending surprises.</p>
            </div> <!-- /service-detail -->
          </div> <!-- /service-item -->
        </div> <!-- /col -->       
      </div> <!--end row -->


      


    </div>
  </section>
  

</asp:Content>
