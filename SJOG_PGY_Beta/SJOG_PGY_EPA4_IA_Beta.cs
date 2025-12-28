using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace SJOG_PGY_Beta
{
    public class SJOG_PGY_EPA4_IA_Beta
    {

        [Test]
        public async Task Test1()
        {
            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new List<string> { "--start-maximized" },
                SlowMo = 50,
                Timeout = 80000,
                //ExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" // for Edge Webdriver
            };
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            //await using var browser = await playwright.Webkit.LaunchAsync(launchOptions); // For WebKit (Safari engine)
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport // For chrome and edge
                //ViewportSize = null, // For WebKit (Safari engine)
                //ScreenSize = new ScreenSize { Width = 1920, Height = 1080 } //For WebKit (Safari engine)
            });
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://hospital-beta.hprime.com.au/Login.aspx?ReturnUrl=%2fGeneral%2fDashboard.aspx");




            var username = "**********";
            var password = "**********";
            var reqsetup_Candidate = "AutomationUser, 3001";
            var imp_Candidate = "3656"; //AutomationUser, 3001
            var int_Assessor_Firstname = "3002";
            var int_Assessor_Lastname = "AutomationUser";
            var int_Assessor_Email = "user3002@hprime.com.au";
            var int_Assessor_EmpNumber = "user3002";

            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");
            DateTime currentDateTime = DateTime.Now;

            var loaderSelector = "#loading-hprime"; //Normal HP2 circle loader
            var loaderSelector2 = "#rlpddlUsers"; //Impersonation users data loader
            var loaderSelector3 = "#loadingSpinnerId"; //HP3 loader
            var loaderSelector4 = "#rlpcphMain_pnlSearchResults"; //view req loader, on search using text filter
            var loaderSelector5 = "#rlpctl00_cphMain_rgSearchResults"; //loader on search filter (email)

            var submitWithSignature = page.GetByRole(AriaRole.Button, new() { Name = "Submit With Signature" });
            var submit = page.GetByRole(AriaRole.Button, new() { Name = "Submit" });


            //Login GC-PGY (Beta)
            await page.Locator("#LoginSlider div").Filter(new() { HasText = "HBH" }).ClickAsync();
            await page.FillAsync("#txtUserName", username);
            await page.FillAsync("#txtPassword", password);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });

            //Impersonation to Candidate
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "2" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "" + imp_Candidate });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForURLAsync("**/home");
            await page.WaitForLoadStateAsync(LoadState.Load);
            Console.WriteLine("Impersonated to Candidate => " + imp_Candidate);

            //Run Flow (Create Tab)
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.GetByRole(AriaRole.Tab, new() { Name = "Create" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForSelectorAsync("text=EPA 4: Team Communication:  documentation, handover, referrals", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Navigated to Create tab");

            //Run Flow (Select EPA 4)
            await page.GetByRole(AriaRole.Heading, new() { Name = "EPA 4: Team Communication:  documentation, handover, referrals" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/jam-medical/awaiting-assessor-selection/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Assessor Selection", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Assessor Selection Page, EPA 4 Opened");

            //Run Flow (Externl Assessor Details)
            await page.GetByRole(AriaRole.Textbox, new() { Name = "First name" }).FillAsync(int_Assessor_Firstname);
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(int_Assessor_Lastname);
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync(int_Assessor_Email);

            await page.Locator("ng-select[name='ddlPositions_GCWBA'] .ng-select-container").ClickAsync();
            await page.WaitForSelectorAsync(".ng-dropdown-panel .ng-option");
            await page.Locator(".ng-dropdown-panel .ng-option", new() { HasTextString = "Registrar" }).ClickAsync();

            await page.Locator("ng-select[name='ddlContactDetailOther'] .ng-select-container").ClickAsync();
            await page.WaitForSelectorAsync(".ng-dropdown-panel .ng-option");
            await page.Locator(".ng-dropdown-panel .ng-option", new() { HasTextString = "SJOG" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/jam-medical/dynamicform/**");
            Console.WriteLine("External Assessor Selection Form Submitted");

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForLoadStateAsync(LoadState.Load);

            //Run Flow (EPA 4 Form)
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();

            var headingText = await page.Locator("mat-expansion-panel-header .fs-22").InnerTextAsync();

            Console.WriteLine("Heading: " + headingText);

            var match = System.Text.RegularExpressions.Regex.Match(headingText, @"ID:(\d+)");
            string extracted_RequirementId = match.Success ? match.Groups[1].Value : string.Empty;

            if (!string.IsNullOrEmpty(extracted_RequirementId))
            {
                Console.WriteLine($"Extracted ID: {extracted_RequirementId}");
            }
            else
            {
                Console.WriteLine("Could not extract ID.");
            }

            await page.GetByPlaceholder("Number Value Only").Nth(0).FillAsync("11");
            await page.GetByPlaceholder("Number Value Only").Nth(1).FillAsync("22");
            await page.GetByPlaceholder("Number Value Only").Nth(2).FillAsync("33");

            await page.Locator("#mat-checkbox-1").GetByText("Specialist or equivalent (").ClickAsync();
            await page.Locator("#mat-checkbox-2").GetByText("Specialist or equivalent (").ClickAsync();
            await page.Locator("#mat-checkbox-3").GetByText("Registrar").ClickAsync();
            await page.Locator("#mat-checkbox-4").GetByText("Nurse/ nurse practitioner").ClickAsync();
            await page.Locator("#mat-checkbox-5").GetByText("Pharmacist").ClickAsync();
            await page.Locator("#mat-checkbox-6").GetByText("Other").ClickAsync();

            await page.Locator("#mat-checkbox-7").GetByText("Specialist or equivalent (").ClickAsync();
            await page.Locator("#mat-checkbox-8").GetByText("Specialist or equivalent (").ClickAsync();
            await page.Locator("#mat-checkbox-9").GetByText("Registrar").ClickAsync();
            await page.Locator("#mat-checkbox-10").GetByText("Nurse/ nurse practitioner").ClickAsync();
            await page.Locator("#mat-checkbox-11").GetByText("Allied health").ClickAsync();
            await page.Locator("#mat-checkbox-12").GetByText("Pharmacist").ClickAsync();
            await page.Locator("#mat-checkbox-13").GetByText("Patient").ClickAsync();
            await page.Locator("#mat-checkbox-14").GetByText("PGY1/2 peer").ClickAsync();
            await page.Locator("#mat-checkbox-15").GetByText("Other (specify)").ClickAsync();
            await page.Locator("input[name=\"typename\"]").Nth(1).FillAsync("QA " + currentDateTime);

            await page.Locator("#mat-checkbox-16 .mat-checkbox-inner-container").ClickAsync(new() { Force = true });
            await page.Locator("#mat-checkbox-17 .mat-checkbox-inner-container").ClickAsync(new() { Force = true });
            await page.Locator("div:nth-child(16) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.Locator("div:nth-child(17) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.Locator("div:nth-child(18) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.GetByText("High", new() { Exact = true }).ClickAsync();
            await page.GetByText("Adult").ClickAsync();
            await page.Locator("input[name=\"typename\"]").Nth(2).FillAsync("QA " + currentDateTime);
            await page.GetByText("The patient(s) is known to me").ClickAsync();
            await page.GetByText("Requires direct supervision (").ClickAsync();
            await page.GetByText("Yes").ClickAsync();

            await page.Locator("div:nth-child(29) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.Locator("div:nth-child(30) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.Locator("div:nth-child(31) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit With Assessor Signature" }).ClickAsync();

            //Assessor Signature
            var canvases = await page.QuerySelectorAllAsync("//div[@class='sig-background m-t-10']//canvas");
            var canvas = canvases.Last();
            var boundingBox = await canvas.BoundingBoxAsync();

            if (boundingBox != null)
            {
                Console.WriteLine($"{boundingBox.X} {boundingBox.Y} {boundingBox.Width} {boundingBox.Height}");

                await page.Mouse.MoveAsync(boundingBox.X + 10, boundingBox.Y + 10);
                await page.Mouse.DownAsync();
                await page.Mouse.MoveAsync(boundingBox.X + 100, boundingBox.Y + 100);
                await page.Mouse.MoveAsync(boundingBox.X + 200, boundingBox.Y + 100);
                await page.Mouse.MoveAsync(boundingBox.X + 300, boundingBox.Y + 200);
                await page.Mouse.UpAsync();
            }

            //Employee Number
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Enter Emp No." }).FillAsync(int_Assessor_EmpNumber);
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            //Employee Number (assessor username)
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);
            Console.WriteLine("Form Filled and submit by candidate");
            Console.WriteLine("EPA 4 Completed");

            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by assessor");

            //View Req Page
            await page.Locator("#tooltiphelpRequirements").ClickAsync();
            await page.WaitForURLAsync("**/RequirementManagement/ViewRequirement.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=View Requirements", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.Locator("#cphMain_UserTagSearch_pnlCandidatesList").GetByText("Unselect All").ClickAsync();
            await page.GetByText("" + reqsetup_Candidate).ClickAsync();
            await page.Locator("#cphMain_pnlStatusForAdminOnly").GetByText("Unselect All").ClickAsync();
            await page.Locator("#diviFormStatuses").GetByText("Completed").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").ClickAsync();
            await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").FillAsync("" + extracted_RequirementId);
            await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").PressAsync("Enter");

            await page.WaitForSelectorAsync(loaderSelector4, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");


            //Take Screenshot
            string screenshotsFolderPath = @"C:/Users/Ahsan/OneDrive - JAM Web Services Pty Ltd/Pictures/Playwright Screenshots/";
            string screenshotFileName = $"screenshot_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.png";
            string screenshotPath = Path.Combine(screenshotsFolderPath, screenshotFileName);
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = screenshotPath
            });

            Console.WriteLine("Screenshot saved (C:/Users/Ahsan/OneDrive - JAM Web Services Pty Ltd/Pictures/Playwright Screenshots/)");


            Thread.Sleep(5000);


        }
    }

}


