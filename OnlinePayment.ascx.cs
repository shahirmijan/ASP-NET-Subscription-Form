using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public partial class CMSWebParts_Custom_RegistrationForm : System.Web.UI.UserControl
{
    Discount GlobalDiscount = new Discount();
    List<MediaFileInfoIssue> AllIssues = new List<MediaFileInfoIssue>();
    List<SubscriptionPostage> SubscriptionPostages = new List<SubscriptionPostage>();
    List<SubscriptionTypeForm> SubscriptionTypes = new List<SubscriptionTypeForm>();


    MediaFileInfoIssue CurrentIssue = null;
    decimal Price;
    string id;
    private CurrentUserInfo CurrentUser = null;
    public class Global
    {
        public static string RegisterForm = "Subscription";
        public static string DiscountForm = "DiscountCodes";
        public static string SiteName = "WorldCoffeePortal";

    }

    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CMS.Membership.AuthenticationHelper.IsAuthenticated())
        {
            CurrentUser = CMS.Membership.MembershipContext.AuthenticatedUser;
            if (this.CurrentUser != null)
            {
                /* if (CMS.Membership.MembershipContext.AuthenticatedUser.IsInRole("yourRoleName", "siteName")){}*/

                //EmailAddressValidation.InnerText = string.Format("your logged in as {0} ({1})", this.CurrentUser.Email, this.CurrentUser.UserID);
            }
            else
            {
                //EmailAddressValidation.InnerText = string.Format("your not logged ", "");
            }
        }






        this.loadIssueAndFilter(6);
        this.loadSubscriptionData();

        Description.Visible = true;
        Part1.Visible = true;
        Part2.Visible = false;
        // CorporateRegion.Visible = false;
        //MagazineCover.InnerHtml = "<img style='height: 285px; width: auto; border: solid 1px #ccc;' src='/Files/WCP/FifthWaveMagazines/MagazineCover/" + StartWith.Text + ".jpg'>";
        ShippingAddressZone.Visible = false;
        OrderSummarySection.Visible = false;
        BillingAddressZone.Visible = false;
        PayNowZone.Visible = false;
        if (!IsPostBack)
        {
            /*  this.Region.Items.Add(new ListItem("UK (FREE delivery)"));
              this.Region.Items.Add(new ListItem("Europe (+ £15.00 delivery)"));
              this.Region.Items.Add(new ListItem("Rest of the World (+ £20.00 delivery)"));
              this.CorporateRegion.Items.Add(new ListItem("UK (FREE delivery)"));
              this.CorporateRegion.Items.Add(new ListItem("Europe (+ £36.00 delivery)"));
              this.CorporateRegion.Items.Add(new ListItem("Rest of the World (+ £56.00 delivery)"));*/

            foreach (var postage in this.SubscriptionPostages)
            {
                this.Region.Items.Add(new ListItem(postage.Name, postage.Subscription_PostageID.ToString()));
            }

            foreach (var subType in this.SubscriptionTypes)
            {
                this.SubscriptionType.Items.Add(new ListItem(subType.Name, subType.Subscription_TypesID.ToString()));
            }
        }


        if (!Page.IsPostBack)
        {
            /* ListItem liIssue1 = new ListItem("Issue 1 (Sold out)", "Issue1");
             ListItem liIssue2 = new ListItem("Issue 2 (+ £35)", "Issue2");
             ListItem liIssue3 = new ListItem("Issue 3 (+ £35)", "Issue3");
             ListItem liIssue4 = new ListItem("Issue 4 (+ £35)", "Issue4");



             liIssue1.Enabled = false;


             cblBackIssues.Items.Add(liIssue1);
             cblBackIssues.Items.Add(liIssue2);
             cblBackIssues.Items.Add(liIssue3);
             cblBackIssues.Items.Add(liIssue4);*/


            /* var allMediaFiles = MediaFileInfoProvider.GetMediaFiles()
                 .Where("FileLibraryID", QueryOperator.Equals, "6")
                 .ToList();*/
            //var filtered = allMediaFiles.OrderBy("FileTitle").ToList();


            //var filtered = allMediaFiles.OrderByDescending(q => q.FileID);
            //var filtered = allMediaFiles.OrderBy("FileReleaseDate ").ski.ToList();
            //.OrderBy("FormInserted DESC")

            //var files = GetIssues(6);

            /*var sb = new StringBuilder();
            sb.Append("<h2>All</h2>");
            foreach (var mediaFile in this.AllIssues)
            {
                sb.Append("<div>" + mediaFile.FileTitle + "</div>");
            }

            sb.Append("<h2>Current</h2><div>" + this.CurrentIssue.FileTitle + "</div>");

            this.Response.Write(sb.ToString());*/



            if (this.AllIssues.Any())
            {
                foreach (var mediaFile in this.AllIssues)
                {
                    ListItem liIssue1 = new ListItem(mediaFile.FileTitle, mediaFile.FileID.ToString());
                    cblBackIssues.Items.Add(liIssue1);
                }


                ListItem liIssue1Disabled = cblBackIssues.Items[0];
                if (liIssue1Disabled != null)
                {
                    liIssue1Disabled.Enabled = false;
                    liIssue1Disabled.Text = liIssue1Disabled.Text + " (Sold out)";
                }
            }
        }

        getPrice();
    }

    public void UpdateMagazineCover(object sender, EventArgs e)
    {
        // MagazineCover.InnerHtml = "<img style='height: 270px; margin-top: 5px; border: solid 1px #ccc;' src='/Files/WCP/FifthWaveMagazines/MagazineCover/" + StartWith.Text + ".jpg'>";
    }



    //SubscriptionPostages
    public void loadSubscriptionData()
    {

        var subscriptionTypes = new List<SubscriptionTypeForm>();
        var typesData = DbHelper.ExecuteSelectQuery("SELECT * FROM [KenticoCMSDBWCP].[dbo].[Form_WorldCoffeePortal_Subscription_Types] order by [Order]");
        if (typesData != null)
        {
            foreach (DataRow dataRow in typesData.Rows)
            {
                subscriptionTypes.Add(new SubscriptionTypeForm
                {
                    Subscription_TypesID = DbHelper.ChangeType<int>(dataRow["Subscription_TypesID"]),
                    Name = DbHelper.ChangeTypeToString(dataRow["Name"]),
                    Price = DbHelper.ChangeType<decimal>(dataRow["Price"]),
                    Order = DbHelper.ChangeType<int>(dataRow["Order"]),
                });
            }

            if (subscriptionTypes.Any())
            {
                this.SubscriptionTypes = subscriptionTypes.OrderBy(q => q.Order).ToList();
            }
        }



        var subscriptionPostages = new List<SubscriptionPostage>();
        var postageData = DbHelper.ExecuteSelectQuery("SELECT * FROM [KenticoCMSDBWCP].[dbo].[Form_WorldCoffeePortal_Subscription_Postage] order by [Order]");
        if (postageData != null)
        {
            foreach (DataRow dataRow in postageData.Rows)
            {
                subscriptionPostages.Add(new SubscriptionPostage
                {
                    Subscription_PostageID = DbHelper.ChangeType<int>(dataRow["Subscription_PostageID"]),
                    Name = DbHelper.ChangeTypeToString(dataRow["Name"]),
                    Price = DbHelper.ChangeType<decimal>(dataRow["Price"]),
                    Order = DbHelper.ChangeType<int>(dataRow["Order"]),
                });
            }

            if (subscriptionPostages.Any())
            {
                this.SubscriptionPostages = subscriptionPostages.OrderBy(q => q.Order).ToList();
            }
        }

    }


    public void loadIssueAndFilter(int fileLibraryID)
    {

        var issues = new List<MediaFileInfoIssue>();

        var sqlParameters = new List<SqlParameter> { new SqlParameter("FileLibraryID", fileLibraryID) };
        //var dataTable = DbHelper.ExecuteSelectQuery("SELECT *  FROM [Media_File] where @FileLibraryID=FileLibraryID and fileid NOT in (SELECT TOP (1) fileid  FROM [Media_File] where FileLibraryID = 6 order by FileReleaseDate DESC) order by FileReleaseDate ", sqlParameters);
        var dataTable = DbHelper.ExecuteSelectQuery("SELECT *  FROM [Media_File] where @FileLibraryID=FileLibraryID AND FileReleaseDate > '0001-01-01' ORDER BY FileReleaseDate ", sqlParameters);
        if (dataTable != null)
        {
            /*
            FileID 
            FileName    
            FileTitle 
            FileDescription 
            FileExtension 
            FileMimeType    
            FilePath 
            FileSize    
            FileImageWidth 
            FileImageHeight 
            FileGUID 
            FileLibraryID   
            FileSiteID 
            FileCreatedByUserID 
            FileCreatedWhen 
            FileModifiedByUserID    
            FileModifiedWhen 
            FileCustomData  
            FileReleaseDate*/

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var order = new MediaFileInfoIssue
                {
                    FileID = DbHelper.ChangeType<int>(dataRow["FileID"]),
                    FileName = DbHelper.ChangeTypeToString(dataRow["FileName"]),
                    FileTitle = DbHelper.ChangeTypeToString(dataRow["FileTitle"]),
                    //FileGUID = DbHelper.ChangeTypeToString(dataRow["FileGUID"]),
                    FilePath = DbHelper.ChangeTypeToString(dataRow["FilePath"]),
                    FileDescription = DbHelper.ChangeTypeToString(dataRow["FileDescription"]),
                    FileReleaseDate = DbHelper.ChangeTypeToDateTime(dataRow["FileReleaseDate"])
                };

                issues.Add(order);
            }
        }

        if (issues.Any())
        {
            this.CurrentIssue = issues.OrderByDescending(q => q.FileReleaseDate).FirstOrDefault();
            this.AllIssues = issues.Where(q => q.FileID != this.CurrentIssue.FileID).OrderBy(q => q.FileReleaseDate).ToList();
        }
    }

    public int GetCurrentIssue(int fileLibraryID)
    {
        var FifthWaveStartingIssue = 0;
        var sqlParameters = new List<SqlParameter> { new SqlParameter("FileLibraryID", fileLibraryID) };
        var dataTable = DbHelper.ExecuteSelectQuery("SELECT TOP(1) fileid FROM[Media_File] where FileLibraryID = @FileLibraryID order by FileReleaseDate DESC", sqlParameters);
        if (dataTable != null)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                FifthWaveStartingIssue = DbHelper.ChangeType<int>(dataRow["FifthWaveStartingIssue"]);
            }
        }

        return FifthWaveStartingIssue;
    }


    public List<string> GetBackIssuesText
    {
        get
        {
            var issues = new List<string>();
            foreach (ListItem item in cblBackIssues.Items)
            {
                if (item.Selected && !item.Value.Equals("Issue1"))
                {
                    issues.Add(item.Text);
                }
            }

            return issues;
        }
    }

    public List<string> GetBackIssuesValues
    {
        get
        {
            var issues = new List<string>();
            foreach (ListItem item in cblBackIssues.Items)
            {
                if (item.Selected && !item.Value.Equals("Issue1"))
                {
                    issues.Add(item.Value);
                }
            }

            return issues;
        }
    }


    protected void OnCheckBox_Changed(object sender, EventArgs e)
    {
        /*var issues =  GetBackIssuesText;
        var message = "";        
        foreach (var item in issues)
        {
            message += "item: " + item;                
            message += "<br />";  
            message += " count: " + GetBackIssuesText.Count + "<br />";           
        }
    
        Response.Write(message );    */

        getPrice();
    }


    public void Return_To_Issue(object sender, EventArgs e)
    {
        Response.Redirect("https://www.worldcoffeeportal.com/5THWAVE");
    }

    public void GoToPartOne(object sender, EventArgs e)
    {
        Part1.Visible = true;
        MagazineCover.Visible = true;
        Part2.Visible = false;
        OrderSummarySection.Visible = false;
    }

    public void GoToPartTwo(object sender, EventArgs e)
    {
        //if (StartWith.Text.Equals("Issue1"))
        // {
        // StartWithValidation.InnerText = "Issue 1 has been sold out.";
        //}
        //else
        //{
        Description.Visible = false;
        Part1.Visible = false;
        MagazineCover.Visible = false;
        Part2.Visible = true;
        OrderSummarySection.Visible = false;
        ShippingAddressZone.Visible = true;
        this.SubPageTitle.Visible = false;
        this.SubPageTitle.InnerText = "Magazine subscription";
        //}
    }

    public void PriceUpdate(object sender, EventArgs e)
    {
        getPrice();
    }

    public void getPrice()
    {
        var postagePrice = this.SubscriptionPostages.Where(q => q.Subscription_PostageID == int.Parse(Region.SelectedValue.ToString())).Select(q => q.Price).FirstOrDefault();
        var subscriptionType = this.SubscriptionTypes.Where(q => q.Subscription_TypesID == int.Parse(SubscriptionType.SelectedValue.ToString())).Select(q => q.Price).FirstOrDefault();




        Price = postagePrice + subscriptionType + (this.GetBackIssuesText.Count * 35);

        TotalPriceField.InnerText = "£" + (Price.ToString("n"));


        //Response.Write(string.Format("<br />Price {2} - {1} - {0}", postagePrice, subscriptionType, Price));
        //Response.Write(string.Format("<br />Price n {0}", Price.ToString("n")));
    }

    protected void CheckoutButton_Click(object sender, EventArgs e)
    {
        if (!ConfirmationCheckbox.Checked)
        {
            ReviewOrder();
            Part1.Visible = false;
            Description.Visible = false;
            BillingAddressZone.Visible = true;
            AgreementError.InnerHtml = "you must read and agree to the Terms & Conditions of service and Privacy Policy prior to completing your purchasing.";
        }
        else
        {
            Checkout();
        }
    }

    public void ReviewOrderTrigger(object sender, EventArgs e)
    {
        Part1.Visible = false;
        Description.Visible = false;
        BillingAddressZone.Visible = true;

        this.SubPageTitle.Visible = false;
        this.SubPageTitle.InnerText = "Order summary";
        ReviewOrder();
    }

    public void ReviewOrder()
    {
        if (this.IsFormValid())
        {

            PopulateOrderSummary();
            OrderSummarySection.Visible = true;
            PayNowZone.Visible = true;
            Part2.Visible = false;


        }
        else
        {


            OrderSummarySection.Visible = false;
            ShippingAddressZone.Visible = true;
            BillingAddressZone.Visible = false;
            Part2.Visible = true;
        }
    }

    public void PopulateOrderSummary()
    {

        FirstnameSummary.InnerHtml = FirstName.Value;
        LastnameSummary.InnerText = LastName.Value;

        AddressLine1Summary.InnerText = CompanyAddress1.Value;
        AddressLine2Summary.InnerText = CompanyAddress2.Value;
        TownCitySummary.InnerText = CompanyCity.Value;
        PostcodeSummary.InnerText = CompanyPostcode.Value;
        CountrySummary.InnerText = Country.Value;


        ShippingFirstnameSummary.InnerHtml = ShippingAddressFirstName.Value;
        ShippingLastnameSummary.InnerText = ShippingAddressLastName.Value;

        ShippingLine1Summary.InnerText = ShippingAddressLine1.Value;
        ShippingLine2Summary.InnerText = ShippingAddressLine2.Value;
        ShippingCitySummary.InnerText = ShippingAddressCity.Value;
        ShippingPostcodeSummary.InnerText = ShippingAddressPostcode.Value;
        ShippingCountrySummary.InnerText = ShippingAddressCountry.Value;


        var postagePrice = this.SubscriptionPostages.Where(q => q.Subscription_PostageID == int.Parse(Region.SelectedValue.ToString())).Select(q => q.Price).FirstOrDefault();
        var subscriptionType = this.SubscriptionTypes.Where(q => q.Subscription_TypesID == int.Parse(SubscriptionType.SelectedValue.ToString())).Select(q => q.Price).FirstOrDefault();


        string[] DiscountValue = Apply_Discount(DiscountCode.Value);
        annualSubTypeLabel.InnerText = SubscriptionType.SelectedItem.Text;

        AnnualSubscription.InnerText = string.Format("£{0}", subscriptionType);
        Shipping.InnerText = string.Format("£{0}", postagePrice);
        ShippingOrderSummaryLabel.InnerText = string.Format("Shipping {0}", Region.SelectedItem.Text);


        // HtmlTableRow regionRow = new HtmlTableRow();
        //HtmlTableCell regionCell1 = new HtmlTableCell();   
        //HtmlTableCell regionCell2 = new HtmlTableCell(); 

        //regionCell1.InnerHtml = "<label class=\"OrderSummaryLabel\"> Region</label>";
        //regionCell2.InnerHtml = "<label class=\"OrderSummaryValue\">"+ regionSummary +"</label>";

        // regionRow.Controls.Add(regionCell1);
        //regionRow.Controls.Add(regionCell2);
        //OrderSummaryTable.Rows.Add(regionRow);


        /*HtmlTableRow tRow = new HtmlTableRow();
        var issues = GetBackIssuesText;         
        foreach (var issue in issues)
        {   
           
            HtmlTableCell cell1 = new HtmlTableCell();   
            HtmlTableCell cell2 = new HtmlTableCell(); 

            cell1.InnerHtml = "<label class=\"OrderSummaryLabel\"> Back Issue</label>";
            cell2.InnerHtml = "<label class=\"OrderSummaryValue\">UK</label>";
            tRow.Controls.Add(cell1);
            tRow.Controls.Add(cell2);
            
        }*/

        // if(issues.Any()){


        // StartWithSummary.InnerHtml  = string.Join( " <br />", GetBackIssuesText);
        //}

        //OrderSummaryTable.Rows.Add(tRow);

        //OrderSummaryTable.Controls.Add(new Literal { Text = html.ToString() });







        if (string.IsNullOrWhiteSpace(DiscountCode.Value))
        {
            DiscountField.Visible = false;
        }

        if (string.IsNullOrEmpty(ShippingAddressLine1.Value) && string.IsNullOrEmpty(ShippingAddressLine2.Value) && string.IsNullOrEmpty(ShippingAddressCountry.Value) && string.IsNullOrEmpty(ShippingAddressPostcode.Value))
        {
            NoShippingAddress.Visible = true;
            shippingAddressElement.Visible = false;
            ShippingAddressSummery.Visible =false;
            BillingAddressTitle.InnerText = "Billing & shipping address";
        }
        else
        {
            NoShippingAddress.Visible = false;
            ShippingAddressSummery.Visible = true;
            shippingAddressElement.Visible = true;
            BillingAddressTitle.InnerText = "Billing address";
        }

        if (string.IsNullOrEmpty(CompanyAddress2.Value))
        {
            AddressLine2.Visible = false;
        }

        if (!string.IsNullOrEmpty(DiscountCode.Value))
        {
            //getPrice();
            Price = CalculateTotalPrice();
            Total.InnerText = "£" + Price.ToString("n");
        }
        else
        {
            Total.InnerText = "£" + Price.ToString("n");
        }

        var backissues = GetBackIssuesText;

        // selectedBackIssues.InnerText = "Back Issues × " +(backissues.Count);

        selectedBackIssues.InnerHtml = string.Format("Back Issues x {1} <br /> {0}", string.Join(" <br />", GetBackIssuesText), backissues.Count);

        if (backissues.Count > 0)
        {
            selectedBackIssuesContainerTr.Visible = true;
        }
        else
        {
            selectedBackIssuesContainerTr.Visible = false;
        }

        backIssuesTotal.InnerText = "£" + (backissues.Count * 35).ToString("n");
    }

    protected void DicountButton_Click(object sender, EventArgs e)
    {
        decimal TotalPrice = Price;
        HtmlGenericControl response = new HtmlGenericControl("p");
        string[] applyDiscountCode = Apply_Discount(DiscountCode.Value);
        response.InnerText = applyDiscountCode[1]; //The text
        response.Attributes["class"] = applyDiscountCode[0]; //The css
        discountValidation.Controls.Add(response);
        CalculateTotalPrice();
    }
    #endregion

    /// <summary>
    /// Converts string to Hash value
    /// </summary>
    public static string GetSHA1Hash(string plaintext)
    {
        return BitConverter.ToString(new SHA1CryptoServiceProvider()
          .ComputeHash(Encoding.Default.GetBytes(plaintext)))
          .Replace("-", String.Empty);
    }

    public bool IsFormValid()
    {
        FirstnameValidation.InnerText = string.Empty;
        LastnameValidation.InnerText = string.Empty;
        CompanyAddress1Validation.InnerText = string.Empty;
        CompanyAddress1Validation.InnerText = string.Empty;
        CompanyCityValidation.InnerText = string.Empty;
        CompanyPostcodeValidation.InnerText = string.Empty;
        EmailAddressValidation.InnerText = string.Empty;
        CompanyCountryValidation.InnerText = string.Empty;
        CompanyValidation.InnerText = string.Empty;
        FirstnameValidation.InnerText = string.Empty;
        EmailAddressConfirmValidation.InnerText = string.Empty;


        bool valid = true;
        var regexItem = new Regex("^[0-9]*$");
        if (string.IsNullOrEmpty(FirstName.Value))
        {
            FirstnameValidation.InnerText = "Mandatory field";
            valid = false;
        }
        else if (FirstName.Value.Length < 2)
        {
            FirstnameValidation.InnerText = "Your Firstname should be longer than 2 charecters long";
            valid = false;
        }
        else if (regexItem.IsMatch(FirstName.Value))
        {
            FirstnameValidation.InnerText = "Your Firstname should not contain any numbers or special characters";
            valid = false;
        }

        /*  **** Lastname Validation ****  */
        if (string.IsNullOrEmpty(LastName.Value))
        {
            LastnameValidation.InnerText = "Mandatory field";
            valid = false;
        }
        else if (LastName.Value.Length < 3)
        {
            LastnameValidation.InnerText = "Your Lastname should be longer than 2 charecters long";
            valid = false;
        }
        else if (regexItem.IsMatch(LastName.Value))
        {
            LastnameValidation.InnerText = "Your Lastname should not contain any numbers or special charecters";
            valid = false;
        }

        /* **** Business Name **** */
        if (string.IsNullOrEmpty(CompanyAddress1.Value))
        {
            CompanyAddress1Validation.InnerText = "Mandatory field";
            valid = false;
        }

        if (string.IsNullOrEmpty(CompanyCity.Value))
        {
            CompanyCityValidation.InnerText = "Mandatory field";
            valid = false;
        }

        if (string.IsNullOrEmpty(Country.Value))
        {
            CompanyValidation.InnerText = "Mandatory field";
            valid = false;
        }

        if (string.IsNullOrEmpty(CompanyPostcode.Value))
        {
            CompanyPostcodeValidation.InnerText = "Mandatory field";
            valid = false;
        }
        /* **** Email and Confirmation Email Validation **** */
        //Email Regex
        //Regex emailRegex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        if (string.IsNullOrEmpty(EmailAddress.Value) || string.IsNullOrEmpty(EmailAddressConfirm.Value))
        {
            if (string.IsNullOrEmpty(EmailAddress.Value))
            {
                EmailAddressValidation.InnerText = "Mandatory field";
                valid = false;
            }

            if (string.IsNullOrEmpty(EmailAddressConfirm.Value))
            {
                EmailAddressConfirmValidation.InnerText = "Mandatory field";
                valid = false;
            }
        }
        else if (!String.Equals(EmailAddress.Value, EmailAddressConfirm.Value))
        {
            EmailAddressConfirmValidation.InnerText = "Email address does not match";
            valid = false;
        }
        /*else if (!emailRegex.IsMatch(EmailAddress.Value))
        {
           
        }*/
        else
        {
            try
            {
                var test = new MailAddress(EmailAddress.Value).Address;
            }
            catch (FormatException)
            {
                EmailAddressValidation.InnerText = "The email you have entered is not valid";
                valid = false;
            }

            if (valid)
            {
                bool emailExist = DbHelper.UserNameExists(EmailAddress.Value);
                if (emailExist)
                {
                    EmailAddressConfirmValidation.InnerHtml = "This email address is already in use. Please use an alternative to set-up your 5THWAVE subscription account. <br /><br /> For account enquiries, please contact info@worldcoffeeportal.com <br />";
                    valid = false;
                }
            }




        }

        Regex telephone_regex = new Regex(@"/^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$/i");
        return valid;
    }

    //Add information to the Register database on Kentico into a formmal called "Subscription"
    protected void AddToRegisterForm(User user, Company company, decimal TotalPrice)
    {

        BizFormInfo formObject = BizFormInfoProvider.GetBizFormInfo(Global.RegisterForm, Global.SiteName);
        if (formObject != null)
        {
            DataClassInfo formClass = DataClassInfoProvider.GetDataClassInfo(formObject.FormClassID);
            string formClassName = formClass.ClassName;

            BizFormItem newFormItem = BizFormItem.New(formClassName);
            DateTime TimeDate_Now = DateTime.Now;
            DateTime ExpiryDate = TimeDate_Now.AddYears(1);

            newFormItem.SetValue("FormInserted", TimeDate_Now);
            newFormItem.SetValue("FormUpdated", TimeDate_Now);
            newFormItem.SetValue("SubscriptionType", SubscriptionType.SelectedItem.Text);
            newFormItem.SetValue("SubscriptionTypeRef", SubscriptionType.SelectedItem.Value);
            newFormItem.SetValue("ExpiryDate", ExpiryDate.ToString());
            //newFormItem.SetValue("PaymentTotal", Price.ToString("n"));
            newFormItem.SetValue("PaymentTotal", Price);
            newFormItem.SetValue("Region", HttpUtility.HtmlEncode(Region.SelectedItem.Text));
            newFormItem.SetValue("RegionRef", HttpUtility.HtmlEncode(Region.SelectedItem.Value));

            newFormItem.SetValue("StartWith", HttpUtility.HtmlEncode(user.StartWith));
            newFormItem.SetValue("FifthWaveIssues", HttpUtility.HtmlEncode(user.FifthWaveIssues));
            newFormItem.SetValue("DiscountCode", HttpUtility.HtmlEncode(DiscountCode.Value));

            newFormItem.SetValue("Firstname", HttpUtility.HtmlEncode(user.Firstname));
            newFormItem.SetValue("Lastname", HttpUtility.HtmlEncode(user.Lastname));
            newFormItem.SetValue("JobTitle", HttpUtility.HtmlEncode(user.Position));
            newFormItem.SetValue("VatNumber", HttpUtility.HtmlEncode(user.VatNumber));

            newFormItem.SetValue("InvoiceDate", HttpUtility.HtmlEncode(DateTime.Now));
            newFormItem.SetValue("Company", HttpUtility.HtmlEncode(company.Name));
            newFormItem.SetValue("emailinput", HttpUtility.HtmlEncode(user.Email));
            newFormItem.SetValue("Address1", HttpUtility.HtmlEncode(company.Address1));
            newFormItem.SetValue("Address2", HttpUtility.HtmlEncode(company.Address2));
            newFormItem.SetValue("City", HttpUtility.HtmlEncode(company.City));
            newFormItem.SetValue("Postcode", HttpUtility.HtmlEncode(company.Postcode));
            newFormItem.SetValue("Country", HttpUtility.HtmlEncode(company.Country));


            newFormItem.SetValue("ShippingAddressFirstName", HttpUtility.HtmlEncode(ShippingAddressFirstName.Value));
            newFormItem.SetValue("ShippingAddressLastName", HttpUtility.HtmlEncode(ShippingAddressLastName.Value));
            newFormItem.SetValue("ShippingAddressLine1", HttpUtility.HtmlEncode(ShippingAddressLine1.Value));
            newFormItem.SetValue("ShippingAddressLine2", HttpUtility.HtmlEncode(ShippingAddressLine2.Value));
            newFormItem.SetValue("ShippingAddressCity", HttpUtility.HtmlEncode(ShippingAddressCity.Value));
            newFormItem.SetValue("ShippingAddressPostcode", HttpUtility.HtmlEncode(ShippingAddressPostcode.Value));
            newFormItem.SetValue("ShippingAddressCountry", HttpUtility.HtmlEncode(ShippingAddressCountry.Value));

            if (this.CurrentIssue != null)
            {
                //set FifthWaveStartingIssue which will be the current issue 
                newFormItem.SetValue("FifthWaveStartingIssue", this.CurrentIssue.FileID);
            }


            if (TotalPrice == 0.0m) //if the total price is free
            {
                newFormItem.SetValue("PaymentStatus", "Free");
                newFormItem.SetValue("Payment_Received", "Free");
            }
            else
            {
                newFormItem.SetValue("PaymentStatus", "Payment Waiting");
                newFormItem.SetValue("Payment_Received", "Payment Waiting");
            }


            newFormItem.Insert();
        }
    }

    protected string OrderSummary(List<Store> stores)
    {
        string OrderSummary = "<div class='UKCWRegisterOrderSummary'>";
        foreach (var store in stores)
        {
            if (!string.IsNullOrWhiteSpace(store.Name) || !string.IsNullOrWhiteSpace(store.Address1) || !string.IsNullOrWhiteSpace(store.Address2))
                OrderSummary = OrderSummary + "<strong><u>Additional store</u></strong><br /><strong>Company name:</strong> " + HttpUtility.HtmlEncode(store.Name) + "<br /><strong>Marketing pack:</strong> " + HttpUtility.HtmlEncode(store.Marketing) + "<br /><br />";
        }
        //make the string
        OrderSummary = OrderSummary + "</div>";
        return OrderSummary;
    }
    //Get the Id of the Submitted Row
    protected string GetRegisterID()
    {
        string id = null;
        // Gets the form object representing the 'ContactUs' form on the current site
        BizFormInfo formObject = BizFormInfoProvider.GetBizFormInfo(Global.RegisterForm, Global.SiteName);
        if (formObject != null)
        {
            // Gets the class name of the 'ContactUs' form
            DataClassInfo formClass = DataClassInfoProvider.GetDataClassInfo(formObject.FormClassID);
            string formClassName = formClass.ClassName;
            // Loads the form's data and find the user based on the latest email inserted into the database. 
            ObjectQuery<BizFormItem> data = BizFormItemProvider.GetItems(formClassName)
                                                                                    .Where("emailinput='" + HttpUtility.HtmlEncode(EmailAddress.Value) + "'")
                                                                                    .OrderBy("FormInserted DESC")
                                                                                    .TopN(1);

            // Loops through the form's data records
            foreach (BizFormItem item in data)
            {
                id = item.GetStringValue("SubscriptionID", "");
            }
        }
        return id;
    }
    //SetupDictionary
    protected Dictionary<string, object> Macro_dictionary()
    {
        Dictionary<string, object> macro_dictionary = new Dictionary<string, object>();
        //Get the information of the person
        // Gets the form object representing the 'ContactUs' form on the current site
        BizFormInfo formObject = BizFormInfoProvider.GetBizFormInfo(Global.RegisterForm, Global.SiteName);
        if (formObject != null)
        {
            // Gets the class name of the 'ContactUs' form
            DataClassInfo formClass = DataClassInfoProvider.GetDataClassInfo(formObject.FormClassID);
            string formClassName = formClass.ClassName;

            // Loads the form's data
            ObjectQuery<BizFormItem> data = BizFormItemProvider.GetItems(formClassName)
                                                                                .Where("SubscriptionID=" + HttpUtility.HtmlEncode(Convert.ToInt32(GetRegisterID())));

            // Loops through the form's data records
            foreach (BizFormItem item in data)
            {
                // Gets the values of the 'UserEmail' and 'UserMessage' text fields for the given data record
                macro_dictionary.Add("payment", "0  - Free Registration");
                macro_dictionary.Add("reference_number", "WCP-FW" + GetRegisterID());
                macro_dictionary.Add("card_brand", "N/A");
                macro_dictionary.Add("card_number", "N/A");
                macro_dictionary.Add("card_expiredate", "N/A");
                macro_dictionary.Add("card_transcationdate", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                macro_dictionary.Add("Firstname", FirstName.Value);
                macro_dictionary.Add("subscription_type", SubscriptionType.SelectedItem.Text);
                macro_dictionary.Add("order_summary", item.GetStringValue("order_summary", ""));
            }
        }
        return macro_dictionary;
    }

    protected void SendEmail(string emailTo)
    {
        DateTime TimeDate_Now = DateTime.Now;
        DateTime ExpiryDate = TimeDate_Now.AddYears(1);
        string confirmTemplate = "WCP_5THWAVE_Subscription_PaymentReceived";
        EmailMessage emailMessage = new CMS.EmailEngine.EmailMessage();
        EmailTemplateInfo eti = EmailTemplateProvider.GetEmailTemplate(confirmTemplate, Global.SiteName);
        MacroResolver mcr = new MacroResolver();
        mcr.SetNamedSourceData("reference_number", "WCP-FW" + GetRegisterID());
        mcr.SetNamedSourceData("card_transcationdate", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
        mcr.SetNamedSourceData("expiry_date", ExpiryDate.ToString("MM/dd/yyyy h:mm tt"));
        mcr.SetNamedSourceData("Firstname", FirstName.Value);
        mcr.SetNamedSourceData("subscription_type", SubscriptionType.SelectedItem.Text);

        mcr.SetNamedSourceData("paymentAmount", Price.ToString("n"));
        foreach (var macro in Macro_dictionary())
        {
            mcr.SetNamedSourceData(macro.Key, macro.Value.ToString());
        }
        emailMessage.EmailFormat = EmailFormatEnum.Both;
        emailMessage.From = eti.TemplateFrom; //make sure this is specified in the template settings
        emailMessage.Subject = eti.TemplateSubject;
        emailMessage.Recipients = emailTo;
        EmailSender.SendEmailWithTemplateText(Global.SiteName, emailMessage, eti, mcr, true);
    }

    public decimal CalculateTotalPrice()
    {
        getPrice();
        decimal TotalPrice = CalculateDiscount();
        return (TotalPrice);
    }

    public decimal CalculateDiscount()
    {
        decimal OriginalPrice = Price;
        if (GlobalDiscount.Value != 0)
        {
            decimal discountPercentage = 0;
            decimal discountValue = GlobalDiscount.Value;
            if (GlobalDiscount.Type.Equals("Value (%)"))
            {
                discountPercentage = 100m - discountValue;
                discountPercentage /= 100;
                Price *= discountPercentage;
                OriginalPrice = OriginalPrice - Price;
                DiscountAmount.InnerText = "- £" + OriginalPrice.ToString("n");
            }
            else
            {
                if (GlobalDiscount.Type.Equals("Value (£)"))
                {
                    Price = Price - GlobalDiscount.Value;
                    DiscountAmount.InnerText = "- £" + GlobalDiscount.Value.ToString("n");
                }
            }
            TotalPriceField.InnerText = "£" + Price.ToString("n");
            Total.InnerText = "£" + Price.ToString("n");
        }
        return (Price);
    }

    private void Checkout()
    {
        //process payment 
    }

    //Search discount code
    protected Discount DiscountCodeSearch(string discountEntered)
    {
        Discount discount = new Discount(); //Initialise the discount class to create a new object
        BizFormInfo formObject = BizFormInfoProvider.GetBizFormInfo(Global.DiscountForm, Global.SiteName); //Gets the form object representing the 'ContactUs' form on the current site
        if (formObject != null)
        {
            // Gets the class name of the 'ContactUs' form
            DataClassInfo formClass = DataClassInfoProvider.GetDataClassInfo(formObject.FormClassID);
            string formClassName = formClass.ClassName;

            // Loads the form's data
            ObjectQuery<BizFormItem> data = BizFormItemProvider.GetItems(formClassName)
                                            .Where("LOWER(Discount_Code)=Lower('" +
                                            HttpUtility.HtmlEncode(discountEntered.Length > 7 ?
                                            discountEntered.Substring(0, 7) : discountEntered) + "')");

            // Loops through the form's data records
            foreach (BizFormItem item in data)
            {
                discount.Code = discountEntered;
                discount.DiscountStart = item.GetStringValue("Discount_Start", "");
                discount.DiscountEnd = item.GetStringValue("Discount_End", "");
                discount.Limit = Convert.ToInt32(string.IsNullOrWhiteSpace(item.GetStringValue("Discount_Usage", "")) ? "0" : item.GetStringValue("Discount_Usage", ""));
                discount.Type = item.GetStringValue("Discount_Type", "");
                discount.SubscriptionType = item.GetStringValue("Subscription_Type", "");
                discount.Value = Convert.ToDecimal(item.GetStringValue("Discount_Value", ""));
                discount.ErrorMessage = item.GetStringValue("Discount_ErrorMessage", "");
                discount.SuccessMessage = item.GetStringValue("Discount_SuccessMessage", "");
                discount.FreeTicket = Convert.ToBoolean(item.GetStringValue("Discount_FreeTicket", ""));
                return discount;
            }
        }
        GlobalDiscount = discount;
        return discount;
    }

    protected string[] Apply_Discount(string discountEntered)
    {


        string[] discountArray = new string[2];
        //Create a key value pair which will hold the css style of the discount and also the return string
        Discount discount = DiscountCodeSearch(DiscountCode.Value);
        //Create the function variables used here
        string cssDanger = "text-danger";
        string cssSuccess = "text-success";
        string successMessage = !string.IsNullOrWhiteSpace(discount.SuccessMessage) ? discount.SuccessMessage : "Discount successfully applied";
        string expiredMessage = !string.IsNullOrWhiteSpace(discount.ErrorMessage) ? discount.ErrorMessage : "The discount code you have entered has expired";
        string incorrectMessage = !string.IsNullOrWhiteSpace(discount.IncorrectMessage) ? discount.IncorrectMessage : "The discount code you have entered is not valid for your subscription type";
        string notValidYet = !string.IsNullOrWhiteSpace(discount.ErrorMessage) ? discount.ErrorMessage : "The discount code you have entered has expired";

        //Check if a discount code has been found
        if (!string.IsNullOrWhiteSpace(discount.Code))
        {
            //A code has been found, get Current date time
            DateTime currentDateTime = DateTime.ParseExact(Convert.ToString(DateTime.Now), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime? discountStart = null;
            DateTime? discountEnd = null;
            if (!string.IsNullOrWhiteSpace(discount.DiscountStart))
            {
                discountStart = DateTime.ParseExact(discount.DiscountStart, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(discount.DiscountEnd))
            {
                discountEnd = DateTime.ParseExact(discount.DiscountEnd, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }

            //Check if there is any time limit on the discount code
            if (discountStart.HasValue && discountEnd.HasValue)
            {
                //Both the fields are present
                //Check to see if the current time is after the start time (or equal to) 
                if (currentDateTime < discountStart)
                {
                    discountArray[0] = cssDanger;
                    discountArray[1] = notValidYet;
                    return discountArray;
                }
                else if (currentDateTime > discountEnd)
                {
                    discountArray[0] = cssDanger;
                    discountArray[1] = expiredMessage;
                }
            }
            else if (discount.SubscriptionType.Equals("Annual") && !SubscriptionType.SelectedItem.Text.Equals("Annual"))
            {
                discountArray[0] = cssDanger;
                discountArray[1] = incorrectMessage;
                return discountArray;
            }
            else if (discount.SubscriptionType.Equals("Rolling") && !SubscriptionType.SelectedItem.Text.Equals("Rolling"))
            {
                discountArray[0] = cssDanger;
                discountArray[1] = incorrectMessage;
                return discountArray;
            }
            else if (discount.SubscriptionType.Equals("Corporate") && !SubscriptionType.SelectedItem.Text.Equals("Corporate"))
            {
                discountArray[0] = cssDanger;
                discountArray[1] = incorrectMessage;
                return discountArray;
            }
            else if (discountStart.HasValue)
            {
                //Start is present
                //Check to see if the current time is after the start time for the discount code
                if (currentDateTime < discountStart)
                {
                    discountArray[0] = cssDanger;
                    discountArray[1] = notValidYet;
                    return discountArray;
                }
            }
            else if (discountEnd.HasValue)
            {
                //End is present
                //Check to see if the current time is before or equal to the end time
                if (currentDateTime > discountEnd)
                {
                    discountArray[0] = cssDanger;
                    discountArray[1] = expiredMessage;
                    return discountArray;
                }
            }
            //Now we need to get the discount value deducted, deduction type, any custom success message
            //If the discount code has free ticket then create a session with "free" string otherwise create a session with discount type and value
            Session["Discount"] = discount.FreeTicket ? "Free" : discount.Type + "|" + discount.Value.ToString();
            Price = CalculateTotalPrice();
            discountArray[0] = cssSuccess;
            discountArray[1] = successMessage;
        }
        else
        {
            //A code has not been found
            discountArray[0] = cssDanger;
            discountArray[1] = "The code you have entered is not valid";
        }
        GlobalDiscount = discount;
        return discountArray;
    }

    //Create a user class
    public class User
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string VatNumber { get; set; }
        public string Email { get; set; }
        public string StartWith { get; set; }
        public string FifthWaveIssues { get; set; }
    }

    public User Person()
    {
        User user = new User
        {
            Firstname = FirstName.Value,
            Lastname = LastName.Value,
            Position = Position.Value,
            VatNumber = VatNumber.Value,
            Email = EmailAddress.Value,
            //StartWith = StartWith.Text, 
            StartWith =  string.Join(",", GetBackIssuesText),
            FifthWaveIssues =  string.Join("|", GetBackIssuesValues)
        };
        return user;
    }

    //Create a Company class
    public class Company
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }
        public string Type { get; set; }
        public string Outlet { get; set; }
        public string Marketing { get; set; }
    }

    public Company Comp()
    {
        Company company = new Company
        {
            Name = CompanyName.Value,
            Address1 = CompanyAddress1.Value,
            Address2 = CompanyAddress2.Value,
            City = CompanyCity.Value,
            Postcode = CompanyPostcode.Value,
            Region = Region.SelectedValue,
            Country = Country.Value,
        };
        return company;
    }

    //Creare an additional Store Class
    public class Store
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Marketing { get; set; }
    }

    public class Discount
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string SubscriptionType { get; set; }
        public decimal Value { get; set; }
        public bool FreeTicket { get; set; }
        public int Limit { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string IncorrectMessage { get; set; }
        public string DiscountStart { get; set; }
        public string DiscountEnd { get; set; }
    }





    #region DB Helper Classes

    public class MediaFileInfoIssue : MediaFileInfo
    {
        public DateTime? FileReleaseDate { get; set; }
    }


    public class SubscriptionTypeForm
    {
        public int Subscription_TypesID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Order { get; set; }

    }


    public class SubscriptionPostage
    {
        public int Subscription_PostageID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Order { get; set; }

    }

    public class SubscriptionOrder
    {
        public string SubscriptionID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PaymentTotal { get; set; }
        public string StartWith { get; set; }
        public List<int> IssuesList { get; set; }
        public string FifthWaveIssues { get; set; }
        public DateTime? FormInserted { get; set; }
    }

    public static class DbHelper
    {
        private static readonly string Connectionstring = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword";
        public static string ChangeTypeToString(object value)
        {
            Type conversionType = typeof(string);
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value is System.DBNull)
                {
                    return default(string);
                }

                conversionType = Nullable.GetUnderlyingType(conversionType);
            }

            if (value is System.DBNull)
            {
                return default(string);
            }

            return HttpUtility.HtmlDecode((string)Convert.ChangeType(value, conversionType));
        }
        public static T ChangeType<T>(object value)
        {
            Type conversionType = typeof(T);
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value is System.DBNull)
                {
                    return default(T);
                }

                conversionType = Nullable.GetUnderlyingType(conversionType);
            }

            if (value is System.DBNull)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(value, conversionType);
        }
        public static System.DateTime? ChangeTypeToDateTime(object value)
        {
            if (value == null || value is System.DBNull)
            {
                return null;
            }
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }
            return System.Convert.ToDateTime(value);
        }
        public static int ExecuteUpdateQuery(string commandText, List<SqlParameter> sqlParameters, CommandType commandType = CommandType.Text)
        {
            int rowEffected;
            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                try
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = commandText;

                        if (sqlParameters.Any())
                        {
                            command.Parameters.AddRange(sqlParameters.ToArray());
                        }

                        command.Connection = connection;
                        command.CommandType = commandType;
                        connection.Open();
                        rowEffected = command.ExecuteNonQuery();
                        //connection.Close();
                    }
                }
                catch (SqlException e)
                {
                    Console.Write(string.Format("Error - Connection.executeUpdateQuery - Query: {0} \nException: {1}", commandText, e.StackTrace.ToString()));
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
            return rowEffected;
        }
        public static DataTable ExecuteSelectQuery(string commandText, List<SqlParameter> sqlParameters = null, CommandType commandType = CommandType.Text)
        {
            DataTable dataTable = null;
            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = commandText;
                        if (sqlParameters != null && sqlParameters.Any())
                        {
                            command.Parameters.AddRange(sqlParameters.ToArray());
                        }


                        command.Connection = connection;
                        command.CommandType = commandType;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return null;
                            }
                            dataTable = new DataTable("dataTable");
                            dataTable.Load(reader);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.Write(string.Format("Error - Connection.executeSelectQuery - Query: {0} \nException: {1}", commandText, e.StackTrace.ToString()));
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            return dataTable;
        }
        public static bool UserNameExists(string _email)
        {

            var sqlParameters = new List<SqlParameter> { new SqlParameter("Email", _email.ToLower().Trim()) };
            var dataTable = DbHelper.ExecuteSelectQuery("SELECT UserName FROM [CMS_User] WHERE lower(Email) = @Email OR  lower(UserName) = @Email ", sqlParameters);
            if (dataTable != null)
                return true;
            else
                return false;

        }
    }

    #endregion

}