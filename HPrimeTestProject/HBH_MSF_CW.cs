using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace HPrimeTestProject
{
    internal class HBH_MSF_CW
    {
        [Test]
        public async Task Test2()
        {
            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new List<string> { "--start-maximized" },
                SlowMo = 50,
                Timeout = 80000,
            };
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://hospital-beta.hprime.com.au/");




            //Login HBH-WBA(Beta)
            var username = "Jamweb.admin.hbh";
            var password = "JAMWeb@605";

            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");

            DateTime currentDateTime = DateTime.Now;

            var loaderSelector = "#loading-hprime"; //Normal circle loader
            var loaderSelector2 = "#rlpddlUsers"; //Impersonation users data loader
            var loaderSelector3 = "#loadingSpinnerId"; //HP3 loader
            var loaderSelector4 = "#rlpcphMain_pnlSearchResults"; //view req loader, on search using text filter
            var loaderSelector5 = "#rlpcphMain_ucWidgetFormsAndAssessments_divEmailBody";

            var idElement = page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl03_ctl01_PageSizeComboBox_Arrow");

            await page.ClickAsync("#HBH_LoginDiv");
            await page.FillAsync("#txtUserName", username);
            await page.FillAsync("#txtPassword", password);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("JAMWeb Admin Logged in");


            //Requirement Setup

            await page.ClickAsync("#Main-Requirements");
            await page.ClickAsync("#ManageRequirementSetup");
            await page.WaitForSelectorAsync("text=Add New Requirement Setup", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByRole(AriaRole.Button, new() { Name = "Add New Requirement Setup" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions //Wait for the loader to disappear

            {
                State = WaitForSelectorState.Hidden, // Wait until the loader is hidden
                Timeout = 30000 // Timeout after 30 seconds if loader does not disappear
            });
            Console.WriteLine("Req Setup Started");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=Setup Information", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByLabel("Requirement Type").ClickAsync();
            await page.GetByLabel("Requirement Type").FillAsync("Test Requirement " + currentDateTime);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions
            {
                State = WaitForSelectorState.Hidden, // Wait until the loader is hidden
                Timeout = 30000 // Timeout after 30 seconds if loader does not disappear
            });
            Console.WriteLine("Req Name & Type Finished");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=Requirement Details", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_ddlForms_0").SelectOptionAsync(new[] { "481" }); //MSF Co Worker

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden, // Wait until the loader is hidden
                Timeout = 30000 // Timeout after 30 seconds if loader does not disappear
            });
            Console.WriteLine("MSF Co-Worker Selected");

            Thread.Sleep(1000);
            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_txtSuffix_0").FillAsync("QA " + formattedDate);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden, // Wait until the loader is hidden
                Timeout = 30000 // Timeout after 30 seconds if loader does not disappear
            });
            Console.WriteLine("Assessment Selected");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=Selected Candidates", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByText("401, User").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden, // Wait until the loader is hidden
                Timeout = 30000 // Timeout after 30 seconds if loader does not disappear
            });
            Console.WriteLine("Candidate Selected");

            await page.WaitForSelectorAsync("text=Requirement Details Summary", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByRole(AriaRole.Button, new() { Name = "Finish Requirement Setup" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            Console.WriteLine("Requirement setup completed");
            await page.GotoAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);


            //Impersonation to Candidate
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "2" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "1980" });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/home");
            Console.WriteLine("Impersonation with candidate");

            //Run Flow (Select Assessment Page)
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });


            //Wait for Req Publish (5 sec refresh)
            string targetText = "QA " + formattedDate; //Searching text

            async Task<bool> IsTextPresentAsync() //Function to check if the text is present
            {
                await page.ReloadAsync(); //Reload the page
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle); //Wait for the page to load

                var textFound = await page.EvaluateAsync<bool>(@$"() => document.body.textContent.includes('{targetText}')"); //Check if the target text is present on the page

                return textFound;
            }



            int maxAttempts = 20;
            int attempt = 0;

            while (attempt < maxAttempts)
            {
                var textFound = await IsTextPresentAsync();

                if (textFound)
                {
                    Console.WriteLine($"Requirement found: {targetText}");
                    break;
                }
                else
                {
                    Console.WriteLine("Requirement not found, refreshing...");
                    await Task.Delay(5000); //Wait for 5 seconds before the next refresh
                }

                attempt++;
            }

            if (attempt == maxAttempts)
            {
                Console.WriteLine("Requirement is not published.");
                await browser.CloseAsync();
            }


            //Run Flow (Fill Data)
            await page.GetByText("" + formattedDate).First.ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/jam-medical/assign-nominee/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            Console.WriteLine("Assessment Opened");
            await page.GetByPlaceholder("First name").FillAsync("QA");
            await page.GetByPlaceholder("Last name").FillAsync("Test");
            await page.GetByPlaceholder("Email").FillAsync("qatest@gmail.com");
            await page.GetByPlaceholder("Unit no.").FillAsync("QA");
            await page.GetByPlaceholder("Comments/Position").FillAsync("QA");
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            Console.WriteLine("Assessment Submitted");


            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by canddiate");


            //Impersonation to Admin(Leesa)
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "1" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "1283" });
            Thread.Sleep(1500);
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation with admin Leesa");


            //Open Assessment by Leesa
            await idElement.ScrollIntoViewIfNeededAsync();
            Thread.Sleep(1000);
            await page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl03_ctl01_PageSizeComboBox_Input").ClickAsync();
            Thread.Sleep(1000);
            await page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl03_ctl01_PageSizeComboBox_DropDown").GetByText("All").ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.GetByText("QA " + formattedDate).First.ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.GetByRole(AriaRole.Button, new() { Name = "Next", Exact = true }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector5, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.GetByRole(AriaRole.Link, new() { Name = "Click here to open form" }).ClickAsync(new LocatorClickOptions
            {
                Button = MouseButton.Right,
            });
            Thread.Sleep(500);
            await page.Locator("span").Filter(new() { HasText = "Properties..." }).Nth(2).ClickAsync();
            Thread.Sleep(4000);
            string ex_url = await page.FrameLocator("iframe[name=\"Window\"]").GetByLabel("URL").InputValueAsync(); // Extract the URL
            await page.FrameLocator("iframe[name=\"Window\"]").GetByRole(AriaRole.Button, new() { Name = "Cancel" }).ClickAsync();
            Thread.Sleep(1000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Send", Exact = true }).ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Nominee has been approved", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("MSF admin to review form submitted");


            //Sign Out
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Sign out for open external form");


            //var url = ("https://hp3-test.hprime.com.au//externalForm?id=74T1HRhG/2I%3D&cs=zxjHyEUXfpM%3D");
            //Open External Assessor Form
            await page.GotoAsync(ex_url);
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForSelectorAsync("text=Form Descriptor", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByRole(AriaRole.Button, new() { Name = "close", Exact = true }).ClickAsync();
            Console.WriteLine("External assessor form openned");




            await page.Locator(".ng-arrow-wrapper").ClickAsync();
            await page.GetByRole(AriaRole.Option, new() { Name = "Clinical Director" }).ClickAsync();
            await page.GetByText("History").ClickAsync();
            await page.GetByText("Physical Examination").ClickAsync();
            await page.GetByText("Management/Counselling").ClickAsync();
            await page.GetByText("Clinical Judgement").ClickAsync();
            await page.GetByText("Communication Skills").ClickAsync();
            await page.GetByText("Working in a team").ClickAsync();
            await page.GetByText("Professionalism", new() { Exact = true }).ClickAsync();
            await page.GetByText("Cultural competence").ClickAsync();
            await page.GetByText("Patient safety and quality of").ClickAsync();
            await page.Locator("#mat-radio-2").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-9").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-17").GetByText("AT EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-25").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-32").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-34").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-41").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-48").GetByText("AT EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-55").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-63").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-65").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-72").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-79").GetByText("AT EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-87").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-94").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-102").GetByText("NOT OBSERVED").ClickAsync();
            await page.Locator("#mat-radio-104").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-112").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.GetByText("Competent", new() { Exact = true }).ClickAsync();
            await page.Locator("textarea").ClickAsync();
            await page.Locator("textarea").FillAsync("QA " + currentDate);
            await page.Locator("input[name=\"typename\"]").Nth(2).FillAsync("11");
            await page.Locator("input[name=\"typename\"]").Nth(3).FillAsync("22");
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit With Signature" }).ClickAsync();

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
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await page.WaitForSelectorAsync("text=We'd love to hear from you", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("External assessor form submitted");


            //Login Again
            await page.GotoAsync("https://hospital-beta.hprime.com.au/");
            await page.ClickAsync("#HBH_LoginDiv");
            await page.FillAsync("#txtUserName", username);
            await page.FillAsync("#txtPassword", password);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("JAMWeb admin logged in again");


            //Impersonation to Admin (Leesa) for review
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "1" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "1283" });
            Thread.Sleep(1500);
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation with admin Leesa");


            //Open Assessment by Leesa for review
            await idElement.ScrollIntoViewIfNeededAsync();
            Thread.Sleep(1000);
            await page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl03_ctl01_PageSizeComboBox_Input").ClickAsync();
            Thread.Sleep(1000);
            await page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl03_ctl01_PageSizeComboBox_DropDown").GetByText("All").ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/jam-medical/dynamicform/**");
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.GetByRole(AriaRole.Button, new() { Name = "close", Exact = true }).ClickAsync();
            await page.WaitForSelectorAsync("text=MSF", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByPlaceholder("Sign Off Authority Comments").FillAsync("QA" + currentDateTime );


            //Reviewed by Leesa (admin)
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign Off" }).ClickAsync();

            await page.WaitForURLAsync("**/General/Dashboard.aspx?v3SignOut=true");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Review and submitted by Leesa admin");


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


            //Thread.Sleep(10000);
        }
    }
}
