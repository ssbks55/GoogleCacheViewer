<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CacheBrowser.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <style type="text/css">
        .txtBox
        {
            width: 700px;
            padding: 5px;
        }

        .btnBox
        {
            padding: 2px;
            width: 140px;
        }

        .frmBox
        {
            width: 100%;
            height: 100%;
            overflow-x: auto;
        }

        .sfbgg
        {
            background-color: #f1f1f1;
            border-bottom: 1px solid #e5e5e5;
            border-color: #e5e5e5;
            height: 27px;
            padding: 5px;
            margin-top: -5px;
        }



        .kpbb
        {
            background-color: #4d90fe;
            background-image: -moz-linear-gradient(center top, #4d90fe, #4787ed);
            border: 1px solid #3079ed;
            padding: 4px;
            width: 100px;
            color: #fff;
        }

        .logo
        {
            color: cadetblue;
            font-size: 14px;
            font-weight: bolder;
            width: 10px;
        }

        .bar
        {
            float: left;
            margin-top: 5px;
            width: 140px;
        }

        .txtTokenBox
        {
            width: 70px;
            padding: 5px;
        }
    </style>
    <script type="text/javascript">
        function refresh() {
            $("[id$=iframeViewer]")[0].contentWindow.location.reload();
        }

        $("[id$=iframeViewer]").load(function () {
            alert('frame has (re)loaded');
        });

        function remove() {
            debugger;
            if ($("[id$=iframeViewer]").contents().find('#google-cache-hdr').length > 0)
                $("[id$=iframeViewer]").contents().find('#google-cache-hdr')[0].style.display = "none";
            
        }

        //$(document).ready(function () {
        //    $("[id$=chkSecure]").change(function (evnt) {
        //        if (this.checked) {
        //            alert("Currently available for premium user's only.");
        //            event.pr
        //        }
        //    });
        //});

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="sfbgg">
            <div class="bar">
                <span class="logo">Koogle</span>
                <span style="font-size: 12px">alpha</span>
            </div>
         <%--   <asp:CheckBox Text="" ToolTip="Enable secure search" ID="chkSecure" runat="server" />--%>
            <asp:TextBox runat="server" CssClass="txtBox" ID="txtUrl"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtToken" TextMode="Password" MaxLength="3" Visible="false" CssClass="txtTokenBox"></asp:TextBox>
            <asp:Button runat="server" CssClass="kpbb" OnClick="btnSubmit_Click" ID="btnSubmit" Text="Search" />

            <span>IP Address:</span>
            <asp:Label ForeColor="Green" ID="lblIP" runat="server" />
            <span>and Host Name:</span>
            <asp:Label ForeColor="Green" ID="lblHostName" runat="server" />
        </div>
        <div style="height: 575px">
            <asp:Label ForeColor="Red" Visible="false" ID="lblError" Text="" runat="server" />
            <iframe id="iframeViewer" runat="server" class="frmBox" frameborder="0"></iframe>
        </div>
    </form>
</body>
</html>
