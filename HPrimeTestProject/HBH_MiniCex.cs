using Microsoft.Playwright;

namespace HPrimeTestProject
{
    internal class HBH_MiniCex
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
            await page.GotoAsync("https://hospital-beta.hprime.com.au/Login.aspx?ReturnUrl=%2fGeneral%2fDashboard.aspx");




            var username = "**********";
            var password = "**********";

            var loaderSelector = "#loading-hprime"; //Normal circle loader
            var loaderSelector2 = "#rlpddlUsers"; //Impersonation users data loader
            var loaderSelector3 = "#loadingSpinnerId"; //HP3 loader
            var loaderSelector4 = "#rlpcphMain_pnlSearchResults"; //view req loader, on search using text filter

            await page.ClickAsync("#HBH_LoginDiv");
            await page.FillAsync("#txtUserName", username);
            await page.FillAsync("#txtPassword", password);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });


            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");

            DateTime currentDateTime = DateTime.Now;

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
            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_ddlForms_0").SelectOptionAsync(new[] { "474" }); //MiniCex Adult Medicine Selection

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden, 
                Timeout = 30000 
            });
            Console.WriteLine("MiniCex Selected");

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
            await page.GetByText("401, User").ClickAsync();
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


            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "2" });
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "1980" });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForURLAsync("**/home");
            await page.WaitForLoadStateAsync(LoadState.Load);

            
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);


            string targetText = "QA " + formattedDate;

            async Task<bool> IsTextPresentAsync()
            {
                await page.ReloadAsync();
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                var textFound = await page.EvaluateAsync<bool>(@$"() => document.body.textContent.includes('{targetText}')");

                return textFound;
            }



            int maxAttempts = 20;
            int attempt = 0;

            while (attempt < maxAttempts)
            {
                var textFound = await IsTextPresentAsync();

                if (textFound)
                {
                    Console.WriteLine($"Found the text: {targetText}");
                    break;
                }
                else
                {
                    Console.WriteLine("Requirement not found, refreshing...");
                    await Task.Delay(5000); 
                }

                attempt++;
            }

            if (attempt == maxAttempts)
            {
                Console.WriteLine("Requirement is not published.");
                await browser.CloseAsync();
            }

            await page.GetByText("" + formattedDate).First.ClickAsync();
            await page.Locator("span").Filter(new() { HasText = "Please select assessor" }).First.ClickAsync();
            await page.GetByText("402, User").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);

            
            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            Thread.Sleep(500);
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            await page.Locator(".ng-select-container").First.ClickAsync();
            await page.GetByRole(AriaRole.Option, new() { Name = "Speech Pathologist" }).ClickAsync();
            await page.Locator("input[name=\"typename\"]").First.ClickAsync();
            await page.Locator("input[name=\"typename\"]").First.FillAsync("QA Assessor Position");
            await page.GetByText("History", new() { Exact = true }).ClickAsync();
            await page.GetByText("Physical examination", new() { Exact = true }).ClickAsync();
            await page.GetByText("Management/Counselling").ClickAsync();
            await page.GetByText("Clinical Judgement", new() { Exact = true }).ClickAsync();
            await page.GetByText("Communication Skills", new() { Exact = true }).ClickAsync();
            await page.GetByText("Working in a team").ClickAsync();
            await page.GetByText("Professionalism", new() { Exact = true }).ClickAsync();
            await page.GetByText("Cultural competence", new() { Exact = true }).ClickAsync();
            await page.GetByText("Patient safety and quality of care", new() { Exact = true }).ClickAsync();
            await page.GetByPlaceholder("Enter Age").ClickAsync();
            await page.GetByPlaceholder("Enter Age").FillAsync("99");
            await page.Locator(".age-dropdown > .ng-select-container").ClickAsync();
            await page.GetByRole(AriaRole.Option, new() { Name = "year(s)" }).ClickAsync();
            await page.Locator("div:nth-child(10) > .m-t-20 > div > .select-control > .ng-select > .ng-select-container").ClickAsync();
            await page.GetByRole(AriaRole.Option, new() { Name = "Female" }).ClickAsync();
            await page.Locator("input[name=\"typename\"]").Nth(1).ClickAsync();
            await page.Locator("input[name=\"typename\"]").Nth(1).FillAsync("QA" + formattedDate);
            await page.Locator("textarea").First.FillAsync("QA" + currentDate);

            await page.Locator("#mat-radio-14").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-21").GetByText("BELOW EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-28").GetByText("AT EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-35").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-43").GetByText("ABOVE EXPECTED LEVEL").ClickAsync();
            await page.Locator("#mat-radio-51").GetByText("NOT OBSERVED").ClickAsync();
            await page.Locator("#mat-radio-53").GetByText("BELOW EXPECTED LEVEL").ClickAsync();

            await page.GetByText("Not competent", new() { Exact = true }).ClickAsync();
            await page.Locator("form-field").Filter(new() { HasText = "0/500 characters" }).GetByRole(AriaRole.Textbox).ClickAsync();
            await page.Locator("form-field").Filter(new() { HasText = "0/500 characters" }).GetByRole(AriaRole.Textbox).FillAsync("QA Assessor's comments" + currentDate);
            await page.Locator("input[name=\"typename\"]").Nth(2).FillAsync("03 hrs");
            await page.Locator("input[name=\"typename\"]").Nth(3).FillAsync("04 hrs");
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit With Assessor Signature" }).ClickAsync();

            
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


            
            await page.GetByPlaceholder("Enter Emp No.").FillAsync("12345");

            
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);

            
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);


            
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "1" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "1282" });
            Thread.Sleep(1500);
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Thread.Sleep(1000);

            
            var idElement = page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl03_ctl01_PageSizeComboBox_Arrow");
            await idElement.ScrollIntoViewIfNeededAsync();
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
            await page.WaitForLoadStateAsync(LoadState.Load);
            
            await page.WaitForSelectorAsync("text=Mini-CEX", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            Thread.Sleep(500);
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();

            
            await page.GetByRole(AriaRole.Button, new() { Name = "Approve" }).ClickAsync();
            
            await page.WaitForURLAsync("**/General/Dashboard.aspx?v3SignOut=true");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });

            
            await page.GotoAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);


            
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "1" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "1286" });
            Thread.Sleep(1500);
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Thread.Sleep(1000);


            
            await idElement.ScrollIntoViewIfNeededAsync();
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
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.GetByRole(AriaRole.Button, new() { Name = "close", Exact = true }).ClickAsync();
            Thread.Sleep(500);
            await page.GetByRole(AriaRole.Button, new() { Name = "close", Exact = true }).ClickAsync();

            await page.GetByPlaceholder("Sign Off Authority Comments").ClickAsync();
            await page.GetByPlaceholder("Sign Off Authority Comments").FillAsync("QA " + currentDate);


            
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


