<%@ Control Debug="true" Language="C#" AutoEventWireup="true" CodeFile="OnlinePayment.ascx.cs" Inherits="CMSWebParts_Custom_RegistrationForm" %>

<!--<link href="~/CMSPages/GetResource.ashx?stylesheetname=bootstrap_grid2" type="text/css" rel="stylesheet" />-->
<script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>



<!--
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.8.1/css/bootstrap-select.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.8.1/js/bootstrap-select.js"></script>
-->


<!--<script src="https://cdn.jsdelivr.net/npm/bigpicture@2.5.3/dist/BigPicture.min.js"></script>-->

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/magnific-popup.js/1.1.0/magnific-popup.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/magnific-popup.js/1.1.0/jquery.magnific-popup.min.js"></script>

<script>

    $(document).ready(function () {
        $('.magazine-covers a').magnificPopup({
            type: 'image',
            closeOnContentClick: true,
            closeBtnInside: false,
            //mainClass: 'mfp-no-margins mfp-with-zoom', // class to remove default margin from left and right side
            image: {
                verticalFit: true
            },
            zoom: {
                enabled: true,
                duration: 300 // don't foget to change the duration also in CSS
            }
        });

        /* $('.back-issues').multiselect({
            //includeSelectAllOption: true,
            onChange: function (option, checked, select) {
                console.log('Changed option ' + $(option).val() + '.');
            }
        });*/

    });

</script>




<script>
    function ShowInput(checkbox) {
        console.log(checkbox.checked);
        if (checkbox.checked) {
            $(checkbox).parent().find("input[type=text]").show();
        } else {
            $(checkbox).parent().find("input[type=text]").hide();
        }
    }

    function ShowInputMediaActivity(radio) {
        console.log("radio: " + radio.value);
        if ($(radio).is(':checked')) {
            $("#fundraisingSection").show();
            console.log("radio: " + radio.value);
        } else {
            $("#fundraisingSection").hide();
        }
    }

    jQuery(document).ready(function ($) {
        
            //$('.selectpicker').selectpicker();


        $("#Part2").hide();
        $("#OrderSummary").hide();
        $(".ProceedToPaymentButton").click(function () {
            $("#Part1").hide();
            $("#Part2").show();
            $("#OrderSummary").show();
        });

        $(".MediaActivity [type=radio]").on('change', function () {
            console.log(this.value);
            if (this.value === "Yes") {
                $("#fundraisingSection").show();
            } else {
                $("#fundraisingSection").hide();
            }
        });

        $("#fundraisingSection [type=checkbox]").on('change', function () {
            ShowInput(this);
        });

        $("#fundraisingSection [type=checkbox]").each(function () {
            ShowInput(this);
        });
        $(".MediaActivity [type=radio][value='Yes']").each(function () {
            ShowInputMediaActivity(this);
        });
    });


</script>


<style>


    .mainContent {   
    margin-bottom: 60px;
}
    #OrderSummarySection h1 {
        padding: 24px 0 30px 0;
        margin: 0;
        font-family: droidserifbold,arial;
        font-size: 30px;
        line-height: 34px;
        font-weight: normal;
        margin: 0;
        padding: 0;
        margin-bottom: 30px;
        color: #575757;
        font-size: 16px;
        font-weight: 600;
        line-height: 20px;
        margin-top: 30px;
        font-family: Arial,Helvetica;
    }

    #OrderSummarySection table {
        width: 100%;
    }


    #OrderSummarySection td {
        border: none;
        border-bottom: 1px solid #7e705b21;
        border-top: 1px solid #7e705b21;
        padding: 1px 0;
        font-size: 14px;
        word-spacing: normal;
        letter-spacing: normal;
        line-height: 16px;
    }

    #OrderSummarySection tr:last-child > td:nth-child(1),
    #OrderSummarySection tr:last-child > td:nth-child(2) {
        /*font-size: 20px;
   padding: 12px 0*/
    }


    #OrderSummarySection td:nth-child(1) {
        text-align: left;
    }

    #OrderSummarySection td:nth-child(2) {
        text-align: right;
    }


.magazine-covers .image-link {
    box-shadow: 0.3rem 0.4rem 0.4rem rgb(0 0 0 / 12%);
    display: block;
    position: relative;
    overflow: hidden;
    margin: 10px 0;
}

.magazine-covers .img-responsive {
    display: block;
    max-width: 100%;
    height: auto;
    border: 1px solid #c1c1c1;
    object-fit: cover;
    transition: transform 400ms ease-out;
    image-rendering: -webkit-optimize-contrast;
}

.magazine-covers .img-responsive:hover {
    transform: scale(1.05);
}

    .hidden {
        display: none;
    }

    .width-30 {
        width: 30%;
    }

    .width-40 {
        width: 40%;
    }

    .width-50 {
        width: 50%;
    }

    .width-60 {
        width: 60%;
    }

    .width-70 {
        width: 70%;
    }

    .width-80 {
        width: 80%;
    }

    .width-90 {
        width: 90%;
    }

    .width-100 {
        width: 100%;
    }


    .f-col.width-40 {
        flex: 0.75;
    }

    .f-col.width-60 {
        flex: 1.25;
    }

    .mt-20 {
        margin-top: 20px;
    }

    .mb-20 {
        margin-bottom: 20px;
    }

    .mr-20 {
        margin-right: 20px;
    }

    .f-no-padding {
        padding: 0 !important;
    }

    .f-center {
        text-align: center;
    }

    .background-blue {
        background: #f2f5f7;
    }


    .f-row {
        display: flex;
        flex-direction: row;
        flex-wrap: wrap;
        width: 100%;
    }


    .padding > .f-col > div:not(.f-row) {
        padding: 20px;
        padding: 30px 30px 10px 30px;
    }


    .f-col {
        color: #575757;
        font-size: 16px;
        font-family: Arial,Helvetica;
        display: flex;
        flex-direction: column;
        flex-basis: 100%;
        flex: 1;
    }

    .shipping-notice {
        padding: 24px 0;
        font-style: italic;
        text-align:center;
    }

    @media ( min-width : 600px ) {
        .f-row {
            display: flex;
        }
    }

    @media ( max-width : 600px ) {
        .f-row {
            flex-direction: column;
        }

        

        .f-mb > .f-col {
            margin: 0 0 25px;
        }

        .f-total {
            text-align: right;
        }

        button.ReturnToIssue {
            width: 100%;
        }
    }


    .f-row ul {
        padding: 0;
        margin: 20px;
        color: #575757;
    }




    .banner {
        background-image: url('/files/wcp/FifthWaveMagazines/thumbnail_5W subs header@2x.png');
        background-image: url('/files/wcp/FifthWaveMagazines/5W SUBS website header1.png');
        background-size: cover;
        height: 400px;
        width: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: flex-start;
        margin-bottom: 20px;
    }

        .banner .banner-title {
            color: white;
            padding: 64px 0 0 32px;
            width: 32%;
            font-size: 32px;
            line-height: 30px;
            margin-bottom: 13px;
            font-family: droidserifbold,arial;
        }

        .banner .lead {
            color: white;
            padding: 4px 0 0 32px;
            width: 32%;
            font-size: 16px;
            font-family: Arial,Helvetica;
            line-height: 20px;
        }


    @media ( max-width : 800px ) {
        .banner .banner-title, .banner .lead {
            width: 57%;
        }
    }

    @media ( max-width : 700px ) {
        .banner .banner-title, .banner .lead {
            width: 65%;
        }
    }

    @media ( max-width : 600px ) {
        .banner .banner-title, .banner .lead {
            width: 54%;
        }

        .banner .banner-title {
            font-size: 24px;
        }

        .banner .lead {
            font-size: 13px;
            padding: 0px 33px;
        }

        .banner {
            height: 340px;
            margin-top: 20px;
        }

         .shipping-notice {
        padding: 5px 0;

             }
    }

    @media ( max-width : 500px ) {
        .banner .banner-title, .banner .lead {
            width: 83%;
        }
    }

    @media ( max-width : 400px ) {

        .banner {
            justify-content: flex-end;
        }
    }
</style>


<div class="col-sm-122 YourDetails" style="">

    <h1 style="margin: 4px 0 22px 0;" runat="server" id="SubPageTitle" visible="false">Magazine subscription</h1>

    <div id="Description" runat="server">


        <div class="banner">
            <p class="banner-title h1 display-4">Make waves in the business of coffee</p>
            <p class="lead">
                <strong>5THWAVE magazine is dedicated to exploring the business trends, market forces and consumer desires shaping the global coffee shop industry today.</strong>  <br /> <br /> 
             Subscribe to receive each quarterly edition in print and digitally. Each issue is packed with exclusive interviews, thought leadership, industry analysis, the latest World Coffee Portal data and more.
            </p>
        </div>


        <div class="f-row hidden2 padding f-mb f-features-row">
            <div class="f-col width-50 mr-20 background-blue">
                <div>
                    <h2>Annual subscription</h2>
                    <ul>
                        <li><strong>Print copy</strong> of each quarterly issue</li>
                        <li><strong>Digital download</strong> for each issue</li>
                        <li>Limited edition 5THWAVE tote bag, and <a href="ttps://www.allegra-publishing.com/Shop/The-Meaning-of-Coffee">The Meaning of Coffee book</a></li>
                        <li>Free UK delivery</li>
                    </ul>
                </div>
            </div>

            <div class="f-col width-50 background-blue">
                <div>
                    <h2>Corporate annual subscription</h2>
                    <ul>
                       <li>
                            <strong>x5 print copies of each quarterly issue</strong>
                        </li>
                        <li>
                            <strong>Digital download</strong> for each issue
                        </li>
                        <li>
                            <strong>x10 corporate hospitality codes offering 50% annual subscription
                            discount </strong>to share with colleagues, clients and friends
                        </li>                       
                        <li>
                            <strong>Save 33% on each magazine</strong>
                        </li>
                        <li>Limited edition 5THWAVE tote bag, and <a href="ttps://www.allegra-publishing.com/Shop/The-Meaning-of-Coffee">The Meaning of Coffee book</a></li>
                        <li>
                            Free UK delivery
                        </li>
                    </ul>
                </div>
            </div>
        </div>



        <div class="row">
            <div class="col-12 col-md-12">
                <p class="shipping-notice">Worldwide delivery is available (charges apply)</p>
            </div>
        </div>

        <div>
            <asp:ValidationSummary ID="ValSummary" runat="server" ShowSummary="true" ShowValidationErrors="true" HeaderText="The following problems occured when submitting the form:" />
        </div>
    </div>

    <div class="row f-row">
        <div class="Part1 sub-form col-xs-12 col-md-6 f-col width-50 " id="Part1" runat="server">

            <div class="row f-row">
                <div class="col-xs-12 col-md-5 f-col width-40">
                    <label class="SubscriptionLabel">
                        <h3>Subscription type </h3>
                    </label>
                </div>
                <div class="col-xs-12 col-md-7 f-col width-60">
                    <asp:DropDownList runat="server" OnSelectedIndexChanged="PriceUpdate"   class="SubscriptionInput" ID="SubscriptionType" AutoPostBack="True" CssClass="WCP-FT-select selectpicker">

                    </asp:DropDownList>
                </div>
            </div>

            <div class="row f-row" style="margin-top: 10px; margin-bottom: 10px;">
                <div class="col-xs-12 col-md-5 f-col width-40">
                    <label class="SubscriptionLabel">
                        <h3>Add back issue</h3>
                    </label>
                </div>
                <div class="col-xs-12 col-md-7 f-col width-60">

                    <asp:DropDownList Visible="false" runat="server" OnSelectedIndexChanged="UpdateMagazineCover" class="SubscriptionInput" ID="StartWith" AutoPostBack="true" CssClass="WCP-FT-select">
                        <asp:ListItem Value="Issue1">Issue 1 (Sold out)</asp:ListItem>
                        <asp:ListItem Value="Issue2">Issue 2 (+ £35)</asp:ListItem>
                        <asp:ListItem Value="Issue3">Issue 3 (+ £35)</asp:ListItem>
                        <asp:ListItem Value="Issue4">Issue 4 (+ £35)</asp:ListItem>
                    </asp:DropDownList>

                    <asp:CheckBoxList Visible="false" ID="cblBackIssuesBackup" runat="server" DataTextField="language" DataValueField="language" AutoPostBack="false" OnSelectedIndexChanged="OnCheckBox_Changed">
                        <asp:ListItem Value="Issue1"> &nbsp;Issue 1 (Sold out)</asp:ListItem>
                        <asp:ListItem Value="Issue2"> &nbsp;Issue 2 (+ £35)</asp:ListItem>
                        <asp:ListItem Value="Issue3"> &nbsp;Issue 3 (+ £35)</asp:ListItem>
                        <asp:ListItem Value="Issue4"> &nbsp;Issue 4 (+ £35)</asp:ListItem>
                    </asp:CheckBoxList>

                    <asp:CheckBoxList ID="cblBackIssues"   runat="server" DataTextField="language" DataValueField="language" AutoPostBack="true" OnSelectedIndexChanged="OnCheckBox_Changed">
                    </asp:CheckBoxList>


                    <p runat="server" class="text-danger" id="StartWithValidation"></p>
                </div>
            </div>

            <div class="row f-row">
                <div class="col-xs-12 col-md-5 f-col width-40">
                    <label class="SubscriptionLabel">
                        <h3>Shipping region</h3>
                    </label>
                </div>
                <div class="col-xs-12 col-md-7 f-col width-60">
                    <asp:DropDownList runat="server" class="SubscriptionInput" ID="Region" AutoPostBack="True" CssClass="WCP-FT-select"></asp:DropDownList>
                    
                </div>
            </div>

            <div class="row f-row">
                <div class="col-xs-12 col-md-5 f-col width-40">
                    <label>
                        <h3>Discount code </h3>
                    </label>
                </div>
                <div class="col-xs-12 col-md-7 f-col width-60">

                    <div class="row f-row">
                        <div class="col-xs-12 col-md-6 f-col width-50">
                            <input type="text" id="DiscountCode" runat="server" class="discountField" maxlength="200" size="8" />
                        </div>
                        <div class="col-xs-12 col-md-6 f-col width-50">
                            <asp:Button ID="Button2" OnClick="DicountButton_Click" CssClass="discountButton width-100" runat="server" Text="APPLY" />
                        </div>
                    </div>


                </div>

            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <div id="discountValidation" runat="server">
                    </div>
                </div>
            </div>

           


            <style>

                .f-checkout-row {
                margin-top: 3px;
                }
                .f-checkout-row > .f-col:nth-child(2) {
                  flex-basis: 31%;flex-grow: 0; 
            }

                .f-total-row {
                padding: 6px 0 0 0;
                }

                 .f-total-row .f-col > label:first-child {
                     color: #ff5800;
                    font-size: 16px;
                    font-family: Arial,Helvetica;
                    font-weight: bold;
                }

                  .f-total-row .f-col:nth-child(2) > label {

                    font-weight: 500;
                }
              .f-total-row > .f-col:first-child {
                   flex-basis: 55%; flex-grow: 0;
            }


              @media (max-width: 800px){
                    .f-total-row {
                            flex-direction: row !important;
                            padding: 20px 0;
                    }

                     .f-total-row .f-col:nth-child(1) {
                       text-align:left;
                       width: 50%;
                       flex-basis: 50%;
                    }

                     .f-total-row .f-col:nth-child(2){
                        text-align:right;
                         width: 50%;
                       flex-basis: 50%;
                    }

              }
            </style>

            <div class="row f-row f-checkout-row">
               <div class="col-xs-6 col-md-2 f-total f-col width-70">

                    <div class="row f-row f-total-row">
                        <div class="col-xs-6 col-md-2 f-total f-col width-30">
                             <label>                                
                                 Total Payment
                             </label>                           
                        </div>
                        <div class="col-xs-12 col-md-5 f-col width-50">                          
                               <label id="TotalPriceField" runat="server"></label>
                        </div>
                    </div>

                </div>

                <div class="col-xs-6 col-md-5 f-col width-30" style="">
                     <asp:Button runat="server" CssClass="ProceedToPaymentButton" ID="Proceed" OnClick="GoToPartTwo" Text="Checkout" />
                </div>

            </div>

            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <p runat="server" class="text-danger" id="CompanyCountryValidation"></p>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 col-md-12">

                    <hr>
                    <p style="padding: 0 0 20px 0;">
                        For subscription inquiries please contact Hannah Heath, Marketing Manager on <strong><a href="mailto:hheath@allegra.co.uk" style="background-color: rgb(255, 255, 255);">hheath@allegra.co.uk </a></strong>
                    </p>


                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <a href="https://www.worldcoffeeportal.com/5THWAVE" style="text-decoration: none !important;">
                        <button type="button" id="Return" class="ReturnToIssue" runat="server">Return to issue</button></a>
                </div>
            </div>

            <br />
            <br />
            <br />

        </div>
        
        <div id="MagazineCover" runat="server" class="col-xs-12 col-md-6 f-col width-50">
            <div class="magazine-covers">

                <a href="/Files/WCP/FifthWaveMagazines/preview/costa-coca-cola.jpg" class="without-caption image-link" title2="The caption">
                   <img src="/Files/WCP/FifthWaveMagazines/preview/costa-coca-cola-thumb.jpg" class="img-responsive" /> 
                </a>

                 <a href="/Files/WCP/FifthWaveMagazines/preview/euro-latte-Index.jpg" class="without-caption image-link" title2="The caption">
                   <img src="/Files/WCP/FifthWaveMagazines/preview/euro-latte-Index-thumb.jpg" class="img-responsive" /> 
                </a>

            </div>
        </div>
    </div>



    <style>
        .magazine-checkout-form * {
            box-sizing: border-box;
            font-family: Arial,Helvetica;
            font-weight: normal;
            font-size: 16px;
            line-height: 21px;
            font-style: normal;
            line-height: 1.5em;
        }

        .magazine-checkout-form .form-header {
            min-height: 40px;
            margin-top: 10px;
        }


            .magazine-checkout-form .form-header h3 {
                display: block;
                color: #575757;
                font-size: 16px;
                font-weight: 600;
                padding: 10px 0 0 0;
            }

            .magazine-checkout-form .form-header span {
                display: block;
                color: #888;
                font-size: 13px;
                font-weight: normal;
            }

        .magazine-checkout-form .row {
            display: -ms-flexbox; /* IE10 */
            display: flex;
            -ms-flex-wrap: wrap; /* IE10 */
            flex-wrap: wrap;
            margin: 0 -20px 0 -10px;
        }

        .no-margin {
            margin: 0 !important;
        }

        .magazine-checkout-form .text-danger {
            color: #d06b66 !important;
            line-height: 1.5em;
            font-size: 0.75em;
            margin: 4px 0px;
            /*display:none;*/
        }

        .magazine-checkout-form .text-helper {
            color: #6c757d !important;
            display: block;
            margin-top: .5rem;
            font-size: 80%;
            font-weight: 400;
            /
        }

        .magazine-checkout-form .row.inner {
            margin: 0 -6px;
        }


        .magazine-checkout-form .col-25 {
            -ms-flex: 25%;
            flex: 25%;
        }

        .magazine-checkout-form .col-30 {
            -ms-flex: 30%;
            flex: 30%;
        }

        .magazine-checkout-form .col-40 {
            -ms-flex: 40%;
            flex: 40%;
        }

        .magazine-checkout-form .col-50 {
            -ms-flex: 50%;
            flex: 50%;
        }

        .magazine-checkout-form .col-60 {
            -ms-flex: 60%;
            flex: 60%;
        }

        .magazine-checkout-form .col-70 {
            -ms-flex: 70%;
            flex: 70%;
        }

        .magazine-checkout-form .col-75 {
            -ms-flex: 75%;
            flex: 75%;
        }

        .magazine-checkout-form .col-100 {
            -ms-flex: 100%;
            flex: 100%;
        }

        .magazine-checkout-form .col-25,
        .magazine-checkout-form .col-30,
        .magazine-checkout-form .col-40,
        .magazine-checkout-form .col-50,
        .magazine-checkout-form .col-60,
        .magazine-checkout-form .col-70,
        .magazine-checkout-form .col-75 {
            padding: 0 6px;
            flex-grow: initial;
        }

        .container.magazine-checkout-form {
            background-color: #fff;
            padding: 0 15px;
        }

        .magazine-checkout-form .form-group {
            width: 100%;
            margin-bottom: 8px;
        }


        .magazine-checkout-form input[type=text] {
            width: 100%;
            padding: 6px 10px;
            border: 1px solid #ccc;
            background-color: #fff;
            border-radius: 0;
            outline: none;
            /*font-size: 1em;*/
        }

            .magazine-checkout-form input[type=text]:focus {
                border: solid 1px #bec7ce;
                background-color: #e8edf1;
                outline: none;
            }


            .magazine-checkout-form input[type=text]::placeholder {
                font-size: 0.95em;
            }

        .magazine-checkout-form .form-group > label {
            margin-bottom: 10px;
            display: none;
        }


        .magazine-checkout-form .spacer-t1 {
            margin-top: 10px;
        }

        .magazine-checkout-form .spacer-t2 {
            margin-top: 20px;
        }

        .magazine-checkout-form .spacer-t3 {
            margin-top: 30px;
        }

        .magazine-checkout-form .spacer-t4 {
            margin-top: 40px;
        }

        .magazine-checkout-form .spacer-t5 {
            margin-top: 50px;
        }

        .magazine-checkout-form .spacer-t6 {
            margin-top: 60px;
        }

        .magazine-checkout-form .spacer-t7 {
            margin-top: 70px;
        }

        .magazine-checkout-form .spacer-t8 {
            margin-top: 80px;
        }


        .padding-0 {
            padding: 0 !important;
        }

        .row.inner :nth-child(1) {
            order: 1;
        }

        .row.inner :nth-child(2) {
            order: 2;
        }

        .row.inner :nth-child(3) {
            order: 3;
        }



        .magazine-checkout-form .btn {
            background-color: #a5b8c9;
            color: white;
            padding: 6px 12px;
            margin: 10px 0;
            border: none;
            width: 100%;
            /* border-radius: 3px; */
            cursor: pointer;
            font-size: 12px;
            text-transform: uppercase;
            font-weight: bold;
            outline: none;
            display: block;
            text-align: center;
            text-decoration: none;
            border: 2px solid #a5b8c9;
        }


            .magazine-checkout-form .btn span {
                display: none;
            }

            .magazine-checkout-form .btn svg {
                height: 1.5rem !important;
                width: 1.5rem !important;
                overflow: hidden;
                vertical-align: middle;
                margin-top: -1px;
            }

            .magazine-checkout-form .btn.primary svg rect,
            .magazine-checkout-form .btn.primary svg path {
                fill: #fff;
            }

            .magazine-checkout-form .btn.secondary svg rect,
            .magazine-checkout-form .btn.secondary svg path {
                fill: #a5b8c9;
            }

            .magazine-checkout-form .btn.primary {
                /* background-color: #ff5800;
    color: white;        
    border: 2px solid #ff5800;*/
            }


                .magazine-checkout-form .btn.primary:hover {
                    background-color: #fff;
                    color: #7fa4c7;
                }



                    .magazine-checkout-form .btn.primary:hover svg rect,
                    .magazine-checkout-form .btn.primary:hover svg path {
                        fill: #ff5800;
                    }

            .magazine-checkout-form .btn.secondary:hover svg rect,
            .magazine-checkout-form .btn.secondary:hover svg path {
                fill: #fff;
            }

            .magazine-checkout-form .btn.secondary {
                background-color: #fff;
                color: #a5b8c9;
                border: 2px solid #a5b8c9 !important;
            }

                .magazine-checkout-form .btn.secondary:hover {
                    background-color: #a5b8c9;
                    color: #fff;
                }

            .magazine-checkout-form .btn:hover {
                background: #7fa4c7;
            }

        .magazine-checkout-form a {
            color: #2196F3;
        }

        .magazine-checkout-form hr {
            border: 1px solid lightgrey;
        }


        .magazine-checkout-form h3 {
            padding: 5px 0;
            font-size: 16px;
            font-weight: 600;
        }


        .BillingAddressZone ul, li {
            margin: 0;
            padding: 0;
            list-style: none;
        }

        .f-features-row ul li {
            list-style: disc;
        }

        .f-features-row  {
            line-height: 24px;
        }



        .sub-form label h3 {
            padding: 10px 0;
            font-size: 16px;
            font-family: Arial,Helvetica;
            line-height: 26px;
            font-weight: bold;
        }


        .row.submit {
            justify-content: space-between;
            margin: 0 -15px 0 -10px;
            flex: auto;
            width: 380px;
            flex: auto;
            margin: 40px auto 40px auto;
            margin-top: 40px;
        }

        .magazine-covers {
        width: 75%; margin: auto;
        }

        @media (max-width: 800px) {
            .row:not(.submit) {
                flex-direction: column;
            }
            .f-col {
            margin-left: 0px;
            margin-right: 0px;

            width: 100%;
        }



            .discountField {
                width: 100%;
            }

            .row.submit {
                width: 100%;
            }

            .col-25 {
                margin-bottom: 20px;
            }

            .magazine-checkout-form .spacer-t8 {
                margin-top: 0px;
            }

            .sub-form label h3 {
            padding: 16px 0 0 0;
            }

           .magazine-covers {
        width: 100%;
        }
        }

        @media ( max-width : 500px ) {
            .container.magazine-checkout-form {
                padding: 0;
            }

            .row.submit {
                margin: 0px auto 20px auto;
            }
        }
    </style>



    <div class="col-sm-6" id="Part2" runat="server" style="margin-left: -10px;">


        <div class="container magazine-checkout-form">
            <div class="row">

                <div class="col-50 col-personal-info">

                    <div class="form-header">
                        <h3>Personal information</h3>
                    </div>

                    <div class="row inner">
                        <div class="col-50">
                            <div class="form-group">
                                <label for="FirstName">First name</label>
                                <input type="text" runat="server" class="SubscriptionInput" placeholder="First name" id="FirstName" maxlength="200" />
                                <asp:RequiredFieldValidator ID="rfvFirstName" Enabled="false" Display="Dynamic" runat="server" ControlToValidate="FirstName" CssClass="text-danger" ErrorMessage="Your Firstname should not be empty"></asp:RequiredFieldValidator>
                                <p runat="server" class="text-danger" id="FirstnameValidation"></p>
                            </div>
                        </div>
                        <div class="col-50">
                            <div class="form-group">
                                <label for="LastName">Last Name</label>
                                <input type="text" runat="server" class="SubscriptionInput" placeholder="Last name" id="LastName" maxlength="200" />
                                <asp:RequiredFieldValidator ID="rfvLastName" Enabled="false" Display="Dynamic" runat="server" ControlToValidate="LastName" CssClass="text-danger" ErrorMessage="Your LastName should not be empty"></asp:RequiredFieldValidator>
                                <p runat="server" class="text-danger" id="LastnameValidation"></p>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="Position">Position</label>
                        <input type="text" runat="server" class="SubscriptionInput" placeholder="Position" id="Position" maxlength="200" />
                        <asp:RequiredFieldValidator ID="rfvPosition" Enabled="false" Display="Dynamic" runat="server" ControlToValidate="Position" CssClass="text-danger" ErrorMessage="Your Position should not be empty"></asp:RequiredFieldValidator>
                        <p runat="server" class="text-danger" id="PositionValidation"></p>
                    </div>
                    <div class="form-group">
                        <label for="CompanyName">Company</label>
                        <input type="text" class="SubscriptionInput" id="CompanyName" placeholder="Company" runat="server" maxlength="200" />
                        <p runat="server" class="text-danger" id="CompanyNameValidation"></p>
                    </div>
                    

                    <div class="form-group">
                        <label for="Position">VAT Number</label>
                        <input type="text" runat="server" class="SubscriptionInput" placeholder="VAT Number" id="VatNumber" maxlength="200" />                                                
                    </div>



                </div>

                <div class="col-50">
                    <div class="form-header">
                        <h3>Billing address</h3>
                    </div>

                    <div class="form-group">
                        <label for="CompanyAddress1">Address line 1</label>
                        <input type="text" class="SubscriptionInput" placeholder="Address line 1" id="CompanyAddress1" runat="server" maxlength="200" />
                        <p runat="server" class="text-danger" id="CompanyAddress1Validation"></p>
                    </div>

                    <div class="form-group">
                        <label for="CompanyAddress2">Address line 2</label>
                        <input type="text" class="SubscriptionInput" placeholder="Address line 2" id="CompanyAddress2" runat="server" maxlength="200" />
                        <p runat="server" class="text-danger" id="CompanyAddress2Validation"></p>
                    </div>


                    <div class="row inner">                     
                        <div class="col-30">
                            <div class="form-group">
                                <label for="CompanyCity">City</label>
                                <input type="text" class="SubscriptionInput" placeholder="City" id="CompanyCity" runat="server" maxlength="200" />
                                <p runat="server" class="text-danger" id="CompanyCityValidation"></p>
                            </div>
                        </div>                                                                    
                          <div class="col-30">
                            <div class="form-group">
                                <label for="CompanyPostcode">Post Code</label>
                                <input type="text" class="SubscriptionInput" placeholder="Postcode" id="CompanyPostcode" runat="server" maxlength="200" />
                                <p runat="server" class="text-danger" id="CompanyPostcodeValidation"></p>
                            </div>
                        </div>
                         <div class="col-40">
                            <div class="form-group">
                                <label for="Country">Country </label>
                                <input type="text" class="SubscriptionInput" placeholder="Country" id="Country" runat="server" maxlength="200" />
                                <p runat="server" class="text-danger" id="CompanyValidation"></p>
                            </div>
                        </div>

                    </div>


                </div>


            </div>

            <div class="row">
                <div class="col-50">
                    <div class="form-header" style="margin-top: 23px;">
                        <h3>Account</h3>
                    </div>

                    <div class="form-group">
                        <label for="EmailAddress">Email</label>
                        <input type="text" runat="server" class="SubscriptionInput" placeholder="Email" id="EmailAddress" maxlength="200" />
                        <p runat="server" class="text-danger" id="EmailAddressValidation"></p>
                    </div>

                    <div class="form-group">
                        <label for="EmailAddressConfirm">Confirm Email</label>
                        <input type="text" runat="server" class="SubscriptionInput" placeholder="Confirm email" id="EmailAddressConfirm" maxlength="200" />                        
                       <p runat="server" class="text-danger" id="EmailAddressConfirmValidation"></p>
                       <p class="text-helper">Your email address will be your account username for 5THWAVE subscriptions and / or World Coffee Portal downloads</p>
                    </div>
                </div>
                <div class="col-50 padding-0">
                    <div class="col-50" id="ShippingAddressZone" runat="server">

                        <div class="form-header">
                            <h3>Shipping Address</h3>
                            <span>(If different to billing address)</span>
                        </div>

                        <div class="row inner">
                        <div class="col-50">
                            <div class="form-group">
                                <label for="ShippingAddressFirstName">First name</label>
                                <input type="text" runat="server" class="SubscriptionInput" placeholder="First name" id="ShippingAddressFirstName" maxlength="200" />                               
                                <p runat="server" class="text-danger" id="ShippingAddressFirstNameValidation"></p>
                            </div>
                        </div>
                        <div class="col-50">
                            <div class="form-group">
                                <label for="ShippingAddressLastName">Last Name</label>
                                <input type="text" runat="server" class="SubscriptionInput" placeholder="Last name" id="ShippingAddressLastName" maxlength="200" />                                
                                <p runat="server" class="text-danger" id="ShippingAddressLastNameValidation"></p>
                            </div>
                        </div>
                    </div>

                        <div class="form-group">
                            <label for="ShippingAddressLine1">Address line 1</label>
                            <input type="text" class="SubscriptionInput" placeholder="Shipping line 1" id="ShippingAddressLine1" runat="server" maxlength="200" /><br>
                            <p runat="server" class="text-danger" id="ShippingAddressLine1Validation"></p>
                        </div>

                        <div class="form-group">
                            <label for="ShippingAddressLine2">Address line 2</label>
                            <input type="text" class="SubscriptionInput" placeholder="Shipping line 2" id="ShippingAddressLine2" runat="server" maxlength="200" /><br>
                            <p runat="server" class="text-danger" id="ShippingAddressLine2Validation"></p>
                        </div>

                        
                  <div class="row inner">
                       <div class="col-30">
                            <div class="form-group">
                                <label for="ShippingAddressCity">City</label>
                                <input type="text" class="SubscriptionInput" placeholder="City" id="ShippingAddressCity" runat="server" maxlength="200" />
                                <p runat="server" class="text-danger" id="ShippingAddressCityValidation"></p>
                            </div>
                        </div>                                              
                        <div class="col-30">
                            <div class="form-group">
                                <label for="ShippingAddressPostcode">Post Code</label>
                                <input type="text" class="SubscriptionInput" placeholder="Postcode" id="ShippingAddressPostcode" runat="server" maxlength="200" /><br>
                                <p runat="server" class="text-danger" id="ShippingAddressPostcodeValidation"></p>
                            </div>
                        </div>
                       <div class="col-40">
                             <div class="form-group">
                                <label for="ShippingAddressCountry">Country</label>
                                <input type="text" class="SubscriptionInput" placeholder="Country" id="ShippingAddressCountry" runat="server" maxlength="200" /><br>
                                <p runat="server" class="text-danger" id="ShippingCountryValidation"></p>
                            </div>
                        </div>

                    </div>

                        <div class="row inner">
                            <div class="col-50">
                              
                            </div>
                            <div class="col-50">
                 
                            </div>
                        </div>


                    </div>
                </div>
            </div>

            <div class="spacer-t2"></div>

            <div class="row  submit">


                <div class="col-50">
                    <a href="https://www.worldcoffeeportal.com/5THWAVE">
                        <asp:Button ID="Button3" runat="server" OnClick="GoToPartOne" CssClass="ReturnToIssue2 btn secondary" Text="Previous" Visible="false" /></a>

                    <asp:LinkButton runat="server" ID="LinkButton1" CssClass="ProceedToPaymentButton2 btn secondary " OnClick="GoToPartOne">
                
                  <span class="svg-icon">		
					<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
					<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
						<polygon points="0 0 24 0 24 24 0 24"></polygon>
						<rect fill="#000000" opacity="0.3" transform="translate(15.000000, 12.000000) scale(-1, 1) rotate(-90.000000) translate(-15.000000, -12.000000)" x="14" y="7" width="2" height="10" rx="1"></rect>
						<path d="M3.7071045,15.7071045 C3.3165802,16.0976288 2.68341522,16.0976288 2.29289093,15.7071045 C1.90236664,15.3165802 1.90236664,14.6834152 2.29289093,14.2928909 L8.29289093,8.29289093 C8.67146987,7.914312 9.28105631,7.90106637 9.67572234,8.26284357 L15.6757223,13.7628436 C16.0828413,14.136036 16.1103443,14.7686034 15.7371519,15.1757223 C15.3639594,15.5828413 14.7313921,15.6103443 14.3242731,15.2371519 L9.03007346,10.3841355 L3.7071045,15.7071045 Z" fill="#000000" fill-rule="nonzero" transform="translate(9.000001, 11.999997) scale(-1, -1) rotate(90.000000) translate(-9.000001, -11.999997)"></path>
					</g>
				</svg>								
				</span>
               Previous
                    </asp:LinkButton>


                </div>

                <div class="col-50">
                    <asp:Button runat="server" CssClass="ProceedToPaymentButton2 btn primary" ID="ReviewOrderButton" OnClick="ReviewOrderTrigger" Text="Continue" Visible="false" />
                    <asp:LinkButton runat="server" ID="btnSubmit" CssClass="ProceedToPaymentButton2 btn primary " OnClick="ReviewOrderTrigger">
                  Continue 
                  <span class="svg-icon">		
					<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
						<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
							<polygon points="0 0 24 0 24 24 0 24"></polygon>
							<rect fill="#000000" opacity="0.3" transform="translate(8.500000, 12.000000) rotate(-90.000000) translate(-8.500000, -12.000000)" x="7.5" y="7.5" width="2" height="9" rx="1"></rect>
							<path d="M9.70710318,15.7071045 C9.31657888,16.0976288 8.68341391,16.0976288 8.29288961,15.7071045 C7.90236532,15.3165802 7.90236532,14.6834152 8.29288961,14.2928909 L14.2928896,8.29289093 C14.6714686,7.914312 15.281055,7.90106637 15.675721,8.26284357 L21.675721,13.7628436 C22.08284,14.136036 22.1103429,14.7686034 21.7371505,15.1757223 C21.3639581,15.5828413 20.7313908,15.6103443 20.3242718,15.2371519 L15.0300721,10.3841355 L9.70710318,15.7071045 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.999999, 11.999997) scale(1, -1) rotate(90.000000) translate(-14.999999, -11.999997)"></path>
						</g>
					</svg>										
				</span>
                    </asp:LinkButton>
                </div>

            </div>

            <div class="spacer-t8"></div>
        </div>

    </div>




















    <div id="OrderSummarySection" runat="server" class="col-sm-6 magazine-checkout-form" clientidmode="Static">

        <h1>Order summary</h1>

        <div class="row no-margin">
            <div class="col-100">
                <table class="OrderSummaryTable">
                    <tr>
                        <td>
                            <label class="OrderSummaryLabel" id="annualSubTypeLabel" runat="server">Annual subscription &nbsp</label>
                        </td>
                        <td>
                            <label class="OrderSummaryValue" id="AnnualSubscription" runat="server"></label>
                        </td>
                    </tr>
                    <tr runat="server" id="selectedBackIssuesContainerTr">
                        <td>
                            <label class="OrderSummaryLabel">
                                <p runat="server" id="selectedBackIssues">Back Issues</p>
                            </label>
                        </td>
                        <td>
                            <label class="OrderSummaryValue" id="backIssuesTotal" runat="server"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="OrderSummaryLabel" id="ShippingOrderSummaryLabel" runat="server">Shipping &nbsp</label>
                        </td>
                        <td>
                            <label class="OrderSummaryValue" id="Shipping" runat="server"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="OrderSummaryLabel">VAT &nbsp</label>
                        </td>
                        <td>
                            <label class="OrderSummaryValue" id="VAT" runat="server">£0.00</label>
                        </td>
                    </tr>
                    <div id="DiscountField" runat="server">
                        <tr>
                            <td>
                                <label class="OrderSummaryLabel">Discount &nbsp</label>
                            </td>
                            <td>
                                <label class="OrderSummaryValue" id="DiscountAmount" runat="server"></label>
                            </td>
                        </tr>
                    </div>
                    <tr>
                        <td>
                            <label class="OrderSummaryLabel">Total &nbsp</label>
                        </td>
                        <td>
                            <label class="OrderSummaryValue" id="Total" runat="server"></label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>



        <br />
    </div>

    <div class="magazine-checkout-form BillingAddressZone" id="BillingAddressZone" runat="server">
        <div class="spacer-t1"></div>
        <div class="row" style="margin: 0 -20px 0 0px; justify-content: space-around; max-width: 600px; margin: auto;">
            <div class="col-40 address-billing">
                <h3 class="orderTitle" id="BillingAddressTitle" runat="server">Billing address</h3>
                <address>
                    <ul>
                        <li>
                            <label class="BillingDetails" id="FirstnameSummary" runat="server"></label>
                            <label class="BillingDetails" id="LastnameSummary" runat="server"></label>
                        </li>
                        <li>
                            <label class="BillingDetails" id="AddressLine1Summary" runat="server"></label>
                        </li>
                        <li id="AddressLine2" runat="server">
                            <label class="BillingDetails" id="AddressLine2Summary" runat="server"></label>
                        </li>
                        <li>
                            <label class="BillingDetails" id="TownCitySummary" runat="server"></label>
                        </li>                        
                        <li>
                            <label class="BillingDetails" id="PostcodeSummary" runat="server"></label>
                        </li>
                        <li>
                            <label class="BillingDetails" id="CountrySummary" runat="server"></label>
                        </li>
                    </ul>
                </address>
            </div>
            <div class="col-40 address-billing" id="ShippingAddressSummery" runat="server">
                <h3 class="orderTitle" id="shippingTitle" runat="server">Shipping address</h3>
                <address id="shippingAddressElement" runat="server">
                    <ul>
                        <li>
                            <label class="BillingDetails" id="ShippingFirstnameSummary" runat="server"></label>
                            <label class="BillingDetails" id="ShippingLastnameSummary" runat="server"></label>
                        </li>
                        <li>
                            <label class="BillingDetails" id="ShippingLine1Summary" runat="server"></label>
                        </li>
                        <li>
                            <label class="BillingDetails" id="ShippingLine2Summary" runat="server"></label>
                        </li>
                         <li>
                            <label class="BillingDetails" id="ShippingCitySummary" runat="server"></label>
                        </li>
                        <li>
                            <label class="BillingDetails" id="ShippingPostcodeSummary" runat="server"></label>
                        </li>
                        <li>
                            <label class="BillingDetails" id="ShippingCountrySummary" runat="server"></label>
                        </li>
                    </ul>
                </address>
                <p id="NoShippingAddress" runat="server">Same as billing address</p>
            </div>
        </div>
        <div class="spacer-t4"></div>
    </div>

    <div id="PayNowZone" runat="server" class="magazine-checkout-form">
        <div class="row2">
            <div style="text-align: center;">
                <p style="margin: auto; margin-bottom: 5px;">
                    <asp:CheckBox ID="ConfirmationCheckbox" runat="server" /><span>I have read and agree to the&nbsp;</span><strong><a href="https://www.worldcoffeeportal.com/TermsConditions/Impressum" target="_blank">Terms & Conditions &nbsp;</a></strong><span> of service and&nbsp;</span><strong><a href="https://www.worldcoffeeportal.com/PrivacyPolicy" target="_blank"> Privacy Policy</a></strong></p>

                <p style="margin: auto;" runat="server" class="text-danger" id="AgreementError"></p>
            </div>
        </div>
        <div class="row  submit" style="margin-top: 10px;">


            <div class="col-50">
                <asp:LinkButton runat="server" ID="LinkButton2" CssClass="ProceedToPaymentButton2 btn secondary " OnClick="GoToPartTwo">
                
                  <span class="svg-icon">		
					<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
					<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
						<polygon points="0 0 24 0 24 24 0 24"></polygon>
						<rect fill="#000000" opacity="0.3" transform="translate(15.000000, 12.000000) scale(-1, 1) rotate(-90.000000) translate(-15.000000, -12.000000)" x="14" y="7" width="2" height="10" rx="1"></rect>
						<path d="M3.7071045,15.7071045 C3.3165802,16.0976288 2.68341522,16.0976288 2.29289093,15.7071045 C1.90236664,15.3165802 1.90236664,14.6834152 2.29289093,14.2928909 L8.29289093,8.29289093 C8.67146987,7.914312 9.28105631,7.90106637 9.67572234,8.26284357 L15.6757223,13.7628436 C16.0828413,14.136036 16.1103443,14.7686034 15.7371519,15.1757223 C15.3639594,15.5828413 14.7313921,15.6103443 14.3242731,15.2371519 L9.03007346,10.3841355 L3.7071045,15.7071045 Z" fill="#000000" fill-rule="nonzero" transform="translate(9.000001, 11.999997) scale(-1, -1) rotate(90.000000) translate(-9.000001, -11.999997)"></path>
					</g>
				</svg>								
				</span>
              Previous
                </asp:LinkButton>


            </div>

            <div class="col-50">
                <asp:LinkButton runat="server" ID="LinkButton3" CssClass="ProceedToPaymentButton2 btn primary " OnClick="CheckoutButton_Click">
                  Pay Now 
                  <span class="svg-icon">		
					<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
						<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
							<polygon points="0 0 24 0 24 24 0 24"></polygon>
							<rect fill="#000000" opacity="0.3" transform="translate(8.500000, 12.000000) rotate(-90.000000) translate(-8.500000, -12.000000)" x="7.5" y="7.5" width="2" height="9" rx="1"></rect>
							<path d="M9.70710318,15.7071045 C9.31657888,16.0976288 8.68341391,16.0976288 8.29288961,15.7071045 C7.90236532,15.3165802 7.90236532,14.6834152 8.29288961,14.2928909 L14.2928896,8.29289093 C14.6714686,7.914312 15.281055,7.90106637 15.675721,8.26284357 L21.675721,13.7628436 C22.08284,14.136036 22.1103429,14.7686034 21.7371505,15.1757223 C21.3639581,15.5828413 20.7313908,15.6103443 20.3242718,15.2371519 L15.0300721,10.3841355 L9.70710318,15.7071045 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.999999, 11.999997) scale(1, -1) rotate(90.000000) translate(-14.999999, -11.999997)"></path>
						</g>
					</svg>										
				</span>
                </asp:LinkButton>
            </div>

        </div>


        <div class="row" style="display: none;">
            <div class="col-50">
                <div class="row inner">
                    <div class="col-50">
                        <asp:Button runat="server" CssClass="ReturnToIssue2 btn secondary" ID="GoBackToPart2" OnClick="GoToPartTwo" Text="Edit Order" />
                    </div>

                    <div class="col-50">
                        <asp:Button runat="server" CssClass="ProceedToPaymentButton2 btn primary" ID="purchase" OnClick="CheckoutButton_Click" Text="Pay Now" />
                    </div>

                </div>
            </div>
        </div>
        <div class="spacer-t8"></div>



    </div>



</div>
