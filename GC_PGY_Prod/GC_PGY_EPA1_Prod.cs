using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace GC_PGY_Prod
{
    public class GC_PGY_EPA1_Prod
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
            };
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://hospital.hprime.com.au/Login.aspx?ReturnUrl=%2fGeneral%2fDashboard.aspx");


            var username = "**********";
            var password = "**********";
            var reqsetup_Candidate = "JAMWeb, Ahsan";
            var imp_Candidate = "3374"; //JAMWeb, Ahsan
            var imp_MEO = "3200"; //Black, Katina
            var ext_Ass_Firstname = "JAMWeb";
            var ext_Ass_Lastname = "Automation";
            var ext_Ass_Email = "jamweb.automation@hprime.com.au";

            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");
            DateTime currentDateTime = DateTime.Now;

            var loaderSelector = "#loading-hprime"; //Normal HP2 circle loader
            var loaderSelector2 = "#rlpddlUsers"; //Impersonation users data loader
            var loaderSelector3 = "#loadingSpinnerId"; //HP3 loader
            var loaderSelector4 = "#rlpcphMain_pnlSearchResults"; //view req loader, on search using text filter
            var loaderSelector5 = "#rlpctl00_cphMain_rgSearchResults"; //loader on search filter (email)


            //Pre-Test Checklist
            string jsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "confirmChecklist.js");
            string checklistScript = File.ReadAllText(jsPath);

            await page.EvaluateAsync(checklistScript);
            await page.WaitForFunctionAsync("() => window.__confirmationDone === true");

            var result = await page.EvaluateAsync<string>("() => window.__checklistStatus");

            if (result != "success")
            {
                Console.WriteLine("Pre-test checklist not confirmed. Aborting script.");
                throw new Exception("Pre-test checklist was not completed. Test aborted.");
            }


            //Login GC-PGY (Prod)
            await page.Locator("#LoginSlider div").Filter(new() { HasText = "HBH" }).ClickAsync();
            await page.FillAsync("#txtUserName", username);
            await page.FillAsync("#txtPassword", password);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });

            ////Requirement Setup
            //await page.ClickAsync("#Main-Requirements");
            //await page.ClickAsync("#ManageRequirementSetup");
            //await page.WaitForSelectorAsync("text=Add New Requirement Setup", new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.GetByRole(AriaRole.Button, new() { Name = "Add New Requirement Setup" }).ClickAsync();

            //await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions 

            //{
            //    State = WaitForSelectorState.Hidden, 
            //    Timeout = 30000 
            //});
            //Console.WriteLine("Req Setup Started");
            //await page.WaitForLoadStateAsync(LoadState.Load);

            //await page.WaitForSelectorAsync("text=Setup Information", new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.GetByLabel("Requirement Type").ClickAsync();
            //await page.GetByLabel("Requirement Type").FillAsync("Test Requirement " + currentDateTime);
            //await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();

            //await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions
            //{
            //    State = WaitForSelectorState.Hidden, 
            //    Timeout = 30000 
            //});
            //Console.WriteLine("Req Name & Type Finished");
            //await page.WaitForLoadStateAsync(LoadState.Load);

            //await page.WaitForSelectorAsync("text=Requirement Details", new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_ddlForms_0").SelectOptionAsync(new[] { "311" }); //EPA1 Selection

            //await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden, 
            //    Timeout = 30000 
            //});

            //Console.WriteLine("EPA1 Selected");

            //await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_txtSuffix_0").FillAsync("QA " + formattedDate);
            //await page.Locator("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_dueDate_dateInput").FillAsync("" + formattedDate);
            ////Thread.Sleep(5000);
            //await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();

            //await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden, 
            //    Timeout = 30000 
            //});
            //Console.WriteLine("Assessment Selected");
            //await page.WaitForLoadStateAsync(LoadState.Load);

            //await page.WaitForSelectorAsync("text=Selected Candidates", new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.GetByText("" + reqsetup_Candidate).ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();
            //await page.WaitForLoadStateAsync(LoadState.Load);

            //await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden, 
            //    Timeout = 30000 
            //});
            //Console.WriteLine("Candidate Selected");

            //await page.WaitForSelectorAsync("text=Requirement Details Summary", new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.GetByRole(AriaRole.Button, new() { Name = "Finish Requirement Setup" }).ClickAsync();
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.GotoAsync("https://hospital.hprime.com.au/General/Dashboard.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //Console.WriteLine("Req Setup Finished");


            ////Impersonation to Candidate
            //await page.ClickAsync("#user-menu-hprime-btn");
            //await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "2" });
            //await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});
            //await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "" + imp_Candidate });
            //await page.WaitForURLAsync("https://hp3.hprime.com.au/**");
            //await page.WaitForURLAsync("**/home");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //Console.WriteLine("Impersonated to Candidate => " + imp_Candidate);


            ////Run Flow (Select Assessment Page)
            //await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            //await page.WaitForURLAsync("**/jam-medical/assessments");
            //await page.WaitForLoadStateAsync(LoadState.Load);


            ////Wait for Req Publish (5 sec refresh)
            //string targetText = "QA " + formattedDate;
            //int maxAttempts = 20;
            //int delayInMs = 5000;
            //bool found = false;

            //for (int attempt = 0; attempt < maxAttempts; attempt++)
            //{
            //    await page.ReloadAsync();
            //    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            //    await page.WaitForTimeoutAsync(1000); 

            //    var bodyText = await page.InnerTextAsync("body");

            //    if (bodyText.Contains(targetText))
            //    {
            //        Console.WriteLine($"Found the requirement: {targetText}");
            //        found = true;
            //        break;
            //    }

            //    Console.WriteLine($"Attempt {attempt + 1}: '{targetText}' not found. Retrying in {delayInMs / 1000} sec...");
            //    await Task.Delay(delayInMs);
            //}

            //if (!found)
            //{
            //    Console.WriteLine("Requirement is not published.");
            //    await browser.CloseAsync();
            //}



            ////Run Flow (Enter External Assessor Details)
            //await page.GetByText("QA " + formattedDate).First.ClickAsync();

            //await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.GetByRole(AriaRole.Textbox, new() { Name = "First name" }).FillAsync("" + ext_Ass_Firstname);
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync("" + ext_Ass_Lastname);
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("" + ext_Ass_Email);
            //await page.Locator("div").Filter(new() { HasTextRegex = new Regex("^×--Please Select--$") }).Nth(1).ClickAsync();
            //await page.GetByText("Advanced Trainee").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            //Console.WriteLine("External Assessor Details Entered");

            //await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            ////Fill Form by Candidate (Fill Data)
            //await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            //await page.GetByText("1 If clinical assessment has").ClickAsync();
            //await page.GetByText("2 Identify pertinent").ClickAsync();
            //await page.GetByText("3 Obtain consent from the").ClickAsync();
            //await page.GetByText("4 Obtain a history.").ClickAsync();
            //await page.GetByText("5 Examine the patient.").ClickAsync();
            //await page.GetByText("6 Consider and integrate").ClickAsync();
            //await page.GetByText("7 Develop provisional and").ClickAsync();
            //await page.GetByText("8 Produce a management plan.").ClickAsync();
            //await page.GetByText("9 Implement management plan,").ClickAsync();
            //await page.GetByRole(AriaRole.Textbox).Nth(3).FillAsync("QA " + formattedDate);
            //await page.Locator("form-field").Filter(new() { HasText = "0/500 characters" }).GetByRole(AriaRole.Textbox).FillAsync("QA " + formattedDate);

            ////Submit Form
            //await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            //Console.WriteLine("Form Submitted by Candidate (Pending)");

            //await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.WaitForURLAsync("**/jam-medical/assessments");
            //await page.WaitForLoadStateAsync(LoadState.Load);


            ////End Impersonation HP3
            //await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            //Thread.Sleep(1500);
            //await page.GetByText("End Impersonation").ClickAsync();
            //await page.WaitForURLAsync("**/General/Dashboard.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //Console.WriteLine("Impersonation Ended (Candidate)");

            ////Fetch External Assessor Form Link
            //await page.GetByRole(AriaRole.Link, new() { Name = "Communication" }).ClickAsync();
            //await page.GetByRole(AriaRole.Link, new() { Name = "Sent Emails" }).ClickAsync();

            //await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden, 
            //    Timeout = 30000 
            //});

            //await page.WaitForURLAsync("**/Communication/SentEmail.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.WaitForSelectorAsync("text=Sent Email(s)", new PageWaitForSelectorOptions { Timeout = 25000 });
            //Console.WriteLine("Sent Email Page");

            //await page.Locator("#ctl00_cphMain_rdpDateFrom_dateInput").ClickAsync();
            //await page.Locator("#ctl00_cphMain_rdpDateFrom_dateInput").FillAsync("" + formattedDate);

            //await page.Locator("#ctl00_cphMain_rcEmailCategories_Input").ClickAsync();
            //await page.GetByText("Check All").ClickAsync();
            //await page.GetByText("Dynamic Forms/Requirements").ClickAsync();
            //await page.Locator("#ctl00_cphMain_rcEmailCategories_Arrow").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            //await page.WaitForSelectorAsync(loaderSelector4, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.Locator("#ctl00_cphMain_rgSearchResults_ctl00_ctl02_ctl02_FilterTextBox_To").ClickAsync();
            //await page.Locator("#ctl00_cphMain_rgSearchResults_ctl00_ctl02_ctl02_FilterTextBox_To").FillAsync("" + ext_Ass_Email);
            //await page.Locator("#ctl00_cphMain_rgSearchResults_ctl00_ctl02_ctl02_FilterTextBox_To").PressAsync("Enter");

            //await page.WaitForSelectorAsync(loaderSelector5, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.Locator("#ctl00_cphMain_rgSearchResults_ctl00__0").GetByRole(AriaRole.Cell, new() { Name = ext_Ass_Lastname + ", " + ext_Ass_Firstname }).ClickAsync();

            //await page.WaitForSelectorAsync(loaderSelector5, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.WaitForSelectorAsync("text=QA " + formattedDate, new PageWaitForSelectorOptions { Timeout = 25000 });

            //var linkElement = page.Locator("a:has-text('EPA 1: Clinical Assessment')");
            //await linkElement.WaitForAsync();

            //string formUrl = await linkElement.GetAttributeAsync("href");

            //Console.WriteLine($"Extracted URL: {formUrl}");

            //await page.GetByRole(AriaRole.Button, new() { Name = "Cancel" }).ClickAsync();

            ////Admin Sign Out
            //await page.GotoAsync("https://hospita.hprime.com.au/General/Dashboard.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.ClickAsync("#user-menu-hprime-btn");
            //await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });

            //await page.WaitForURLAsync("https://hospital.hprime.com.au/General/Dashboard.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //Console.WriteLine("Sign-out (Admin)");


            //// Navigate to the External Assessor URL
            //if (!string.IsNullOrEmpty(formUrl))
            //{
            //    await page.GotoAsync(formUrl);
            //}

            //await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.WaitForLoadStateAsync(LoadState.Load);
            //Console.WriteLine("External Assessor Form Opened");

            ////Fill Form (External Assessor)
            //await page.WaitForSelectorAsync("text=QA " + formattedDate, new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            //await page.GetByText("Specialist or equivalent (term supervisor)").ClickAsync();
            //await page.GetByText("Specialist or equivalent (other)").ClickAsync();
            //await page.GetByText("Registrar").ClickAsync();
            //await page.GetByText("Nurse/ nurse practitioner").ClickAsync();
            //await page.GetByText("Allied health").ClickAsync();
            //await page.GetByText("Pharmacist").ClickAsync();
            //await page.GetByText("Patient", new() { Exact = true }).ClickAsync();
            //await page.GetByText("PGY1/2 peer").ClickAsync();
            //await page.GetByText("Other (specify)").ClickAsync();
            //await page.Locator("input[type=\"undefined\"]").FillAsync("QA Automation " + formattedDate);
            //await page.GetByText("High").ClickAsync();
            //await page.GetByText("The patient is known to me").ClickAsync();
            //await page.GetByText("Requires DIRECT SUPERVISION").ClickAsync();
            //await page.Locator("form-field").Filter(new() { HasText = "/1000 characters" }).GetByRole(AriaRole.Textbox).FillAsync("QA Automation " + formattedDate);
            //await page.GetByText("Yes").ClickAsync();

            //await page.GetByRole(AriaRole.Button, new() { Name = "Submit With Signature" }).ClickAsync();

            ////External Assessor Signature
            //var canvases = await page.QuerySelectorAllAsync("//div[@class='sig-background m-t-10']//canvas");
            //var canvas = canvases.Last();
            //var boundingBox = await canvas.BoundingBoxAsync();

            //if (boundingBox != null)
            //{
            //    Console.WriteLine($"{boundingBox.X} {boundingBox.Y} {boundingBox.Width} {boundingBox.Height}");

            //    await page.Mouse.MoveAsync(boundingBox.X + 10, boundingBox.Y + 10);
            //    await page.Mouse.DownAsync();
            //    await page.Mouse.MoveAsync(boundingBox.X + 100, boundingBox.Y + 100);
            //    await page.Mouse.MoveAsync(boundingBox.X + 200, boundingBox.Y + 100);
            //    await page.Mouse.MoveAsync(boundingBox.X + 300, boundingBox.Y + 200);
            //    await page.Mouse.UpAsync();
            //}

            //await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            //Console.WriteLine("Form Submitted by External Assessor");


            ////Login Again (Admin)
            //await page.GotoAsync("https://hospital.hprime.com.au/Login.aspx?ReturnUrl=%2fGeneral%2fDashboard.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.Locator("#LoginSlider div").Filter(new() { HasText = "HBH" }).ClickAsync();
            //await page.FillAsync("#txtUserName", username);
            //await page.FillAsync("#txtPassword", password);
            //await page.ClickAsync("#btn_Login");
            //await page.WaitForURLAsync("https://hospital.hprime.com.au/General/Dashboard.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            //Console.WriteLine("Logged-in Again (Admin)");

            ////Impersonated to MEO
            //await page.ClickAsync("#user-menu-hprime-btn");
            //await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "27" });
            //await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});
            //await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "" + imp_MEO });
            //await page.WaitForURLAsync("https://hp3.hprime.com.au/**");
            //await page.WaitForURLAsync("**/home");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //Console.WriteLine("Impersonated to MEO => " + imp_MEO);


            ////Run Flow (MEO Sign Off)
            //await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            //await page.WaitForURLAsync("**/jam-medical/assessments");
            //await page.WaitForLoadStateAsync(LoadState.Load);

            //await page.GetByText("QA " + formattedDate).First.ClickAsync();
            //await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});
            //await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            //Console.WriteLine("Assessment Opened by MEO");

            //await page.GetByText("This form has been reviewed").ClickAsync();
            ////await page.GetByRole(AriaRole.Textbox, new() { Name = "Sign Off Authority Comments" }).ClickAsync();
            //await page.GetByRole(AriaRole.Textbox, new() { Name = "Sign Off Authority Comments" }).FillAsync("QA Automation " + formattedDate);
            //await page.GetByRole(AriaRole.Button, new() { Name = "Sign Off" }).ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            //Console.WriteLine("Form Submitted by MEO (MEO Sign-Off) & Form Completed");

            //await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.WaitForURLAsync("**/jam-medical/assessments");
            //await page.WaitForLoadStateAsync(LoadState.Load);


            ////End Impersonation HP3 (MEO)
            //await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            //Thread.Sleep(1500);
            //await page.GetByText("End Impersonation").ClickAsync();
            //await page.WaitForURLAsync("**/General/Dashboard.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //Console.WriteLine("Impersonation Ended (MEO)");

            ////View Req Page
            //await page.Locator("#tooltiphelpRequirements").ClickAsync();
            //await page.WaitForURLAsync("**/RequirementManagement/ViewRequirement.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.WaitForSelectorAsync("text=View Requirements", new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.Locator("#cphMain_UserTagSearch_pnlCandidatesList").GetByText("Unselect All").ClickAsync();
            //await page.GetByText("" + reqsetup_Candidate).ClickAsync();
            //await page.Locator("#cphMain_pnlStatusForAdminOnly").GetByText("Unselect All").ClickAsync();
            //await page.Locator("#diviFormStatuses").GetByText("Completed").ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            //await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").ClickAsync();
            //await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").FillAsync("" + formattedDate);
            //await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").PressAsync("Enter");

            //await page.WaitForSelectorAsync(loaderSelector4, new PageWaitForSelectorOptions

            //{
            //    State = WaitForSelectorState.Hidden,
            //    Timeout = 30000
            //});

            //await page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");


            ////Take Screenshot
            //string screenshotsFolderPath = @"C:/Users/Ahsan/OneDrive - JAM Web Services Pty Ltd/Pictures/Playwright Screenshots/";
            //string screenshotFileName = $"screenshot_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.png";
            //string screenshotPath = Path.Combine(screenshotsFolderPath, screenshotFileName);
            //await page.ScreenshotAsync(new PageScreenshotOptions
            //{
            //    Path = screenshotPath
            //});

            //Console.WriteLine("Screenshot saved (C:/Users/Ahsan/OneDrive - JAM Web Services Pty Ltd/Pictures/Playwright Screenshots/)");

            Thread.Sleep(5000);


        }
    }

}


