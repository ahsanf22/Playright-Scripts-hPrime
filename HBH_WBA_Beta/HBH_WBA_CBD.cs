using Microsoft.Playwright;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Xamarin.Essentials;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Xml;


namespace HBH_WBA_Beta
{
    public class HBH_WBA_CBD
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


            var username = "Jamweb.admin.hbh"; //HBH-WBA
            var password = "ZYX#123@abc";
            var reqsetup_Assessment = "479"; //CBD
            var reqsetup_Candidate = "401, User";
            var select_Assessor = "402, User";
            var select_Assessor_EmpNo = "12345"; //Employee Number
            var imp_Candidate = "1980"; //401, User
            var imp_Admin1 = "1282"; //Bob
            var imp_Admin2 = "1286"; //Ajith

            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");
            DateTime currentDateTime = DateTime.Now;

            var loaderSelector = "#loading-hprime"; //Normal HP2 circle loader
            var loaderSelector2 = "#rlpddlUsers"; //Impersonation users data loader
            var loaderSelector3 = "#loadingSpinnerId"; //HP3 loader
            var loaderSelector4 = "#rlpcphMain_pnlSearchResults"; //view req loader, on search using text filter
            var loaderSelector5 = "#rlpctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem"; //loader on search filter (hp2 dashboard)

            //Login HBH-WBA(Beta)
            await page.Locator("#LoginSlider div").Filter(new() { HasText = "HBH" }).ClickAsync();
            await page.FillAsync("#txtUserName", username);
            await page.FillAsync("#txtPassword", password);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });


            //Requirement Setup

            await page.ClickAsync("#Main-Requirements");
            await page.ClickAsync("#ManageRequirementSetup");
            await page.WaitForSelectorAsync("text=Add New Requirement Setup", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByRole(AriaRole.Button, new() { Name = "Add New Requirement Setup" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            Console.WriteLine("Req Setup Started");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=Setup Information", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByLabel("Requirement Type").ClickAsync();
            await page.GetByLabel("Requirement Type").FillAsync("Test Requirement " + currentDateTime);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions
            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            Console.WriteLine("Req Name & Type Finished");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=Requirement Details", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_ddlForms_0").SelectOptionAsync(new[] { "" + reqsetup_Assessment });

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            Console.WriteLine("CBD Selected");

            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_txtSuffix_0").FillAsync("QA " + formattedDate);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            Console.WriteLine("Assessment Selected");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=Selected Candidates", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.Locator("#divusers").GetByText("" + reqsetup_Candidate).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            Console.WriteLine("Candidate Selected");

            await page.WaitForSelectorAsync("text=Requirement Details Summary", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByRole(AriaRole.Button, new() { Name = "Finish Requirement Setup" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.GotoAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);


            //Impersonation to Candidate
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "2" });
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "" + imp_Candidate });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForURLAsync("**/home");
            await page.WaitForLoadStateAsync(LoadState.Load);


            //Run Flow (Select Assessment Page)
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);


            //Wait for Req Publish (5 sec refresh)
            string targetText = "QA " + formattedDate;
            int maxAttempts = 20;
            int delayInMs = 5000;
            bool found = false;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                await page.ReloadAsync();
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                await page.WaitForTimeoutAsync(1000);

                var bodyText = await page.InnerTextAsync("body");

                if (bodyText.Contains(targetText))
                {
                    Console.WriteLine($"Found the requirement: {targetText}");
                    found = true;
                    break;
                }

                Console.WriteLine($"Attempt {attempt + 1}: '{targetText}' not found. Retrying in {delayInMs / 1000} sec...");
                await Task.Delay(delayInMs);
            }

            if (!found)
            {
                Console.WriteLine("Requirement is not published.");
                await browser.CloseAsync();
            }


            //Run Flow (Select Assessor)
            await page.GetByText("" + formattedDate).First.ClickAsync();
            await page.Locator("span").Filter(new() { HasText = "Please select assessor" }).First.ClickAsync();
            await page.GetByText("" + select_Assessor).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);

            //Run Flow (Fill Data)
            await page.GetByText("" + formattedDate).First.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            Thread.Sleep(500);
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            await page.Locator("input[type=\"text\"]").First.ClickAsync();
            await page.GetByText("Clinical Director").ClickAsync();
            await page.Locator("input[name=\"typename\"]").First.ClickAsync();
            await page.Locator("input[name=\"typename\"]").First.FillAsync("QA Assessor Position");
            await page.GetByText("History").ClickAsync();
            await page.GetByText("Physical examination").ClickAsync();
            await page.GetByText("Management/Counselling").ClickAsync();
            await page.GetByText("Clinical Judgement", new() { Exact = true }).ClickAsync();
            await page.GetByText("Communication Skills").ClickAsync();
            await page.GetByText("Working in a team").ClickAsync();
            await page.GetByText("Professionalism", new() { Exact = true }).ClickAsync();
            await page.GetByText("Cultural competence").ClickAsync();
            await page.GetByText("Patient safety and quality of").ClickAsync();
            await page.GetByPlaceholder("Enter Age").FillAsync("90");
            await page.Locator("input[type=\"text\"]").Nth(2).ClickAsync();
            await page.GetByText("year(s)").ClickAsync();
            await page.Locator("ng-select").Filter(new() { HasText = "×--Please Select--" }).GetByRole(AriaRole.Textbox).ClickAsync();
            await page.GetByRole(AriaRole.Option, new() { Name = "Male", Exact = true }).ClickAsync();
            await page.Locator("input[name=\"typename\"]").Nth(1).FillAsync("QA Settings");
            await page.GetByText("Adult Medicine").ClickAsync();
            await page.GetByText("Y", new() { Exact = true }).ClickAsync();
            await page.Locator("textarea").First.FillAsync("QA Problems");

            await page.Locator("#mat-radio-24").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-31").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-42").GetByText("NOT OBSERVED").ClickAsync();
            await page.Locator("#mat-radio-48").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();

            await page.GetByText("Competent", new() { Exact = true }).ClickAsync();
            await page.Locator("form-field").Filter(new() { HasText = "0/500 characters" }).GetByRole(AriaRole.Textbox).FillAsync("QA ");
            await page.Locator("input[name=\"typename\"]").Nth(2).FillAsync("01 hrs");
            await page.Locator("input[name=\"typename\"]").Nth(3).FillAsync("02 hrs");
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit With Assessor Signature" }).ClickAsync();


            // Signature on Canvas
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


            //Enter Employee Number
            await page.GetByPlaceholder("Enter Emp No.").FillAsync("" + select_Assessor_EmpNo );


            //Submit Form
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();

            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);


            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);


            //Impersonation to Admin (bob)
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "1" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "" + imp_Admin1 });
            Thread.Sleep(1500);
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Thread.Sleep(1000);

            await page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").FillAsync("QA " + formattedDate);
            await page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").PressAsync("Enter");

            await page.WaitForSelectorAsync(loaderSelector5, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/jam-medical/dynamicform/**");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=CBD", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            Thread.Sleep(500);
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();

            //Bob Approve
            await page.GetByRole(AriaRole.Button, new() { Name = "Approve" }).ClickAsync();

            await page.WaitForURLAsync("**/General/Dashboard.aspx?v3SignOut=true");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });


            //End Impersonation HP2
            await page.GotoAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);


            //Impersonation to Admin(Ajith)
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "1" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "" + imp_Admin2 });
            Thread.Sleep(1500);
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Thread.Sleep(1000);

            await page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").FillAsync("QA " + formattedDate);
            await page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").PressAsync("Enter");

            await page.WaitForSelectorAsync(loaderSelector5, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/jam-medical/dynamicform/**");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.GetByRole(AriaRole.Button, new() { Name = "close", Exact = true }).ClickAsync();
            Thread.Sleep(500);
            await page.GetByRole(AriaRole.Button, new() { Name = "close", Exact = true }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByPlaceholder("Sign Off Authority Comments").ClickAsync();
            await page.GetByPlaceholder("Sign Off Authority Comments").FillAsync("QA " + currentDate);


            //Ajith SignOff
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign Off" }).ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx?v3SignOut=true");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });


            //View Req Page
            await page.Locator("#tooltiphelpRequirements").ClickAsync();
            await page.WaitForURLAsync("**/RequirementManagement/ViewRequirement.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=View Requirements", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.Locator("#cphMain_UserTagSearch_pnlCandidatesList").GetByText("Unselect All").ClickAsync();
            await page.GetByText("401, User").ClickAsync();
            await page.Locator("#cphMain_pnlStatusForAdminOnly").GetByText("Unselect All").ClickAsync();
            await page.Locator("#diviFormStatuses").GetByText("Completed").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").ClickAsync();
            await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").FillAsync("" + formattedDate);
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





        }
    }

}


