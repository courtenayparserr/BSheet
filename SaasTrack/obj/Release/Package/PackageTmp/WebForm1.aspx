<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SaasTrack.WebForm1" %>

<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <title>SUM</title>
  <link rel="stylesheet" type="text/css" href="style.css">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
</head>
<body>
  <div class="container">
    <div class="logo">Sum</div>
    <h2>Sum is a simple web app that gives you access to all your bank accounts</h2>
    <div class="dual-button">
      <div>
        <span class="radio-button radio-left" id="sandboxLinkButton">Sandbox Mode</span>
      </div>
      <div>
        <span class="radio-button radio-right" id="liveLinkButton">Regular Mode</span>
      </div>
    </div>
    <script src="https://cdn.plaid.com/link/stable/link-initialize.js"></script>
    <script>
    // This demo uses two custom initializers so that we can provide
    // both a sandbox and live demo on the same page.
    // See: https://github.com/plaid/link/blob/master/README.md#step-2-custom-integration
    var sandboxHandler = Plaid.create({
      clientName: 'SUM',
      env: 'tartan',
      product: 'auth',
      key: 'test_key',
      onSuccess: function (public_token, metadata)
      {
          debugger;
          $.get('/api/get/' + public_token, function (data, status) {
              alert("Data: " + data + "\nStatus: " + status);
          });

          //window.location = '/api/get/' + public_token;
      },
    });
    var liveHandler = Plaid.create({
      clientName: 'SUM',
      env: 'tartan',
      product: 'auth',
      key: 'ca9eeb4133f494b49bd2cb72f902ab',
      onSuccess: function (public_token, metadata)
      {
          debugger;
          $.get('/api/bankaccount/' + public_token, function (data, status) {
              alert("Data: " + data + "\nStatus: " + status);
          });
      },
    });
    // Open the "Institution Select" view using the sandbox Link handler.
    document.getElementById('sandboxLinkButton').onclick = function() {
      sandboxHandler.open();
    };
    // Open the "Institution Select" view using the live Link handler.
    document.getElementById('liveLinkButton').onclick = function() {
      liveHandler.open();
    };
    </script>
  </div>
</body>
</html>

