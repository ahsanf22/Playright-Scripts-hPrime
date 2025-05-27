using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;

namespace HPrimeTestProject
{
    internal class HBH_TA
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
            var username = "Jamweb.admin.hbhpgy";
            var password = "JAMWeb@605";
            
            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");

            DateTime currentDateTime = DateTime.Now;

            var loaderSelector = "#loading-hprime"; //Normal circle loader
            var loaderSelector2 = "#rlpddlUsers"; //Impersonation users data loader
            var loaderSelector3 = "#loadingSpinnerId"; //HP3 loader
            var loaderSelector4 = "#rlpcphMain_pnlSearchResults"; //view req loader, on search using text filter

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
            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_ddlForms_0").SelectOptionAsync(new[] { "10338" }); //TA Mid Selection

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden, // Wait until the loader is hidden
                Timeout = 30000 // Timeout after 30 seconds if loader does not disappear
            });
            Console.WriteLine("TA Mid Selected");

            Thread.Sleep(1000);
            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_txtSuffix_0").FillAsync("QA " + formattedDate);
            Thread.Sleep(2000);
            await page.Locator("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_ddlSignOff_Arrow").ClickAsync();
            Thread.Sleep(1000);
            await page.Locator("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_ddlSignOff_Input").FillAsync("403");
            Thread.Sleep(1000);
            await page.Locator("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_ddlSignOff_DropDown").GetByText("User").ClickAsync();
            Thread.Sleep(1000);
            //await page.Locator("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_ddlSignOff_Input").PressAsync("Enter");
            //Thread.Sleep(500);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden, // Wait until the loader is hidden
                Timeout = 30000 // Timeout after 30 seconds if loader does not disappear
            });
            Console.WriteLine("Assessment Selected");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=Selected Candidates", new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.GetByText("401, User").ClickAsync();
            await page.Locator("span").Filter(new() { HasText = "401, User" }).ClickAsync();
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
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "23168" });
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



            //Run Flow (Select Assessor)
            await page.GetByText("" + formattedDate).First.ClickAsync();
            await page.Locator("span").Filter(new() { HasText = "Please select assessor" }).First.ClickAsync();
            await page.GetByText("402, User").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);
            Console.WriteLine("Assessor selected");

            //End Impersonation HP3 (Candidate)
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation end with candidate");

            //Impersonation to Assessor
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "5" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "23169" });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/home");
            Console.WriteLine("Impersonation with assessor");

            //Run Flow(Select Assessment Page)
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });


            //Run Flow(Fill Data)
            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            Console.WriteLine("Assessment Opened by assessor");

            await page.Locator(".ng-select-container").First.ClickAsync();
            await page.Locator("input[type=\"text\"]").Nth(1).PressAsync("ArrowDown");
            await page.Locator("input[type=\"text\"]").Nth(1).PressAsync("Enter");
            await page.Locator("ng-select").Filter(new() { HasText = "--Please Select--" }).GetByRole(AriaRole.Textbox).ClickAsync();
            await page.Locator("ng-select").Filter(new() { HasText = "--Please Select--" }).GetByRole(AriaRole.Textbox).PressAsync("ArrowDown");
            await page.Locator("ng-select").Filter(new() { HasText = "--Please Select--" }).GetByRole(AriaRole.Textbox).PressAsync("Enter");
            await page.GetByText("Registrars").ClickAsync();
            await page.GetByText("Nursing staff").ClickAsync();
            await page.GetByText("Allied health professionals").ClickAsync();
            await page.GetByText("Other specialists").ClickAsync();
            await page.GetByText("Other (please specify)").ClickAsync();
            await page.Locator("input[type=\"undefined\"]").ClickAsync();
            await page.Locator("input[type=\"undefined\"]").FillAsync("QA " + formattedDate);
            await page.GetByText("1.1 Patient safety: aware of").ClickAsync();
            await page.GetByText("1.2 Communication: can").ClickAsync();
            await page.GetByText("1.3 Communication - A&TSI").ClickAsync();
            await page.GetByText("1.4 Patient assessment:").ClickAsync();
            await page.GetByText("1.5 Investigations: can order").ClickAsync();
            await page.GetByText("1.6 Procedures: performs").ClickAsync();
            await page.GetByText("1.7 Patient management:").ClickAsync();
            await page.GetByText("1.8 Prescribing: has safe").ClickAsync();
            await page.GetByText("1.9 Emergency care:").ClickAsync();
            await page.GetByText("1.10 Utilising and adapting").ClickAsync();
            await page.Locator("#mat-radio-8").GetByText("Consistently met").ClickAsync();
            await page.Locator(".cdk-textarea-autosize").First.ClickAsync();
            await page.Locator(".cdk-textarea-autosize").First.FillAsync("QA " + currentDateTime);
            await page.GetByText("2.1 Professionalism: high").ClickAsync();
            await page.GetByText("2.2 Self-management: knows").ClickAsync();
            await page.GetByText("2.3 Self-education: shows a").ClickAsync();
            await page.GetByText("2.4 Clinical responsibility:").ClickAsync();
            await page.GetByText("2.5 Teamwork: able to work in").ClickAsync();
            await page.GetByText("2.6 Safe workplace culture:").ClickAsync();
            await page.GetByText("2.7 Culturally safe practice").ClickAsync();
            await page.GetByText("2.8 Time management: able to").ClickAsync();
            await page.Locator("#mat-radio-15").GetByText("Often exceeded").ClickAsync();
            await page.Locator("div:nth-child(19) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").ClickAsync();
            await page.Locator("div:nth-child(19) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.GetByText("3.1 Population health:").ClickAsync();
            await page.GetByText("3.2 Whole of person care:").ClickAsync();
            await page.GetByText("3.3 Cultural safety for all").ClickAsync();
            await page.GetByText("3.4 Understanding biases:").ClickAsync();
            await page.GetByText("3.5 Understanding impacts of").ClickAsync();
            await page.GetByText("3.6 Integrated healthcare:").ClickAsync();
            await page.Locator("#mat-radio-22").GetByText("Consistently exceeded").ClickAsync();
            await page.Locator("div:nth-child(24) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").ClickAsync();
            await page.Locator("div:nth-child(24) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.GetByText("4.1 Knowledge: has").ClickAsync();
            await page.GetByText("4.2 Evidence-informed").ClickAsync();
            await page.GetByText("4.3 Quality assurance:").ClickAsync();
            await page.GetByText("4.4 Advancing A&TSI Health:").ClickAsync();
            await page.Locator("#mat-radio-28").GetByText("Consistently exceeded").ClickAsync();
            await page.Locator("div:nth-child(29) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").ClickAsync();
            await page.Locator("div:nth-child(29) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.GetByText("Satisfactory ( The prevocational doctor has met or exceeded performance").ClickAsync();
            await page.Locator("div:nth-child(33) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").ClickAsync();
            await page.Locator("div:nth-child(33) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.Locator("div:nth-child(34) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").ClickAsync();
            await page.Locator("div:nth-child(34) > .m-t-20 > div > .example-full-width > .cdk-textarea-autosize").FillAsync("QA " + currentDateTime);
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            Console.WriteLine("assessment Submitted by Assessor");
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/jam-medical/assessments");

            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by assessor");


            //Impersonation to Candidate for JMO to Review
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "2" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "23168" });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/home");
            Console.WriteLine("Impersonation with candidate for jmo to review");

            //Run Flow (Select Assessment Page)
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            //Run Flow(JMO to Review)
            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            Console.WriteLine("Assessment Opened by JMO");

            await page.Locator("form-field").Filter(new() { HasText = "0/500 characters" }).GetByRole(AriaRole.Textbox).ClickAsync();
            await page.Locator("form-field").Filter(new() { HasText = "0/500 characters" }).GetByRole(AriaRole.Textbox).FillAsync("QA " + formattedDate);
            await page.Locator("#mat-checkbox-34").ClickAsync();
            //Thread.Sleep(10000);
            await page.GetByRole(AriaRole.Button, new() { Name = "Agreed" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/jam-medical/assessments");
            Console.WriteLine("assessment agreed by JMO");

            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by JMO");


            //Impersonation to Admin (Kylie)
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "1" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "22779" });
            Thread.Sleep(1500);
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation with admin kylie");


            //Open Assessment by Kylie
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
            await page.WaitForSelectorAsync("text=Mid Term Assessment Form", new PageWaitForSelectorOptions { Timeout = 25000 });

            await page.GetByRole(AriaRole.Button, new() { Name = "close", Exact = true }).ClickAsync();
            Console.WriteLine("Assessment opened by Kylie Admin");

            await page.GetByRole(AriaRole.Button, new() { Name = "Approve" }).ClickAsync();

            await page.WaitForURLAsync("**/General/Dashboard.aspx?v3SignOut=true");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Assessment approved by Kylie Admin");


            //End Impersonation HP2 Kylie
            await page.GotoAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("End impersonation by Kylie HP2");


            //Impersonation to Bob MEO
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "27" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "22777" });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/home");
            Console.WriteLine("Impersonation with MEO");

            //Run Flow(Select Assessment Page by MEO)
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });


            //Run Flow(Fill Data by MEO)
            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            await page.GetByPlaceholder("Sign Off Authority Comments").FillAsync("QA" + formattedDate);
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign Off" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            Console.WriteLine("assessment signed off by MEO");
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForSelectorAsync("text=Assessments", new PageWaitForSelectorOptions { Timeout = 25000 });

            //End Impersonation MEO HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by MEO");


            //Impersonation to DCT
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "19" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "23170" });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/home");
            Console.WriteLine("Impersonation with DCT");

            //Run Flow(Select Assessment Page by DCT)
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });


            //Run Flow(Fill Data by DCT)
            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            await page.Locator("input[name=\"typename\"]").FillAsync("QA" + formattedDate);
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign Off" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            Console.WriteLine("assessment signed off by DCT");
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForSelectorAsync("text=Assessments", new PageWaitForSelectorOptions { Timeout = 25000 });

            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by DCT");


            //Impersonation to DMS
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "22" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "22783" });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/home");
            Console.WriteLine("Impersonation with DMS");

            //Run Flow(Select Assessment Page by DMS)
            await page.GetByRole(AriaRole.Heading, new() { Name = "Assessments" }).ClickAsync();
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });


            //Run Flow(Fill Data by DMS)
            await page.GetByText("QA " + formattedDate).First.ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            await page.GetByPlaceholder("Sign Off Authority Comments").Nth(2).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign Off" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            Console.WriteLine("assessment signed off by DMS");
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForSelectorAsync("text=Assessments", new PageWaitForSelectorOptions { Timeout = 25000 });

            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by DMS");


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
