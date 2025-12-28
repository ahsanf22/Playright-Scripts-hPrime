using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace SJOG_PGY_Beta
{
    public class SJOG_PGY_MidTA_Beta
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
            var imp_Assessor = "3670"; //AutomationUser, 3002
            var imp_DCT = "36"; //Bates, Timothy
            var select_Assessor = "AutomationUser, 3002";

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
            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_ddlForms_0").SelectOptionAsync(new[] { "307" }); //Mid TA Selection

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            Console.WriteLine("Mid TA Selected");

            await page.Locator("#cphMain_ucRequirementDetails_rptIndividualForms_txtSuffix_0").FillAsync("QA " + formattedDate);
            await page.Locator("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_dueDate_dateInput").FillAsync("" + formattedDate);

            await page.ClickAsync("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_ddlSignOff_Input");

            await page.WaitForSelectorAsync("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_ddlSignOff_DropDown .rcbList",
                new PageWaitForSelectorOptions { Timeout = 5000 });

            await page.Locator("#ctl00_cphMain_ucRequirementDetails_rptIndividualForms_ctl01_ddlSignOff_DropDown .rcbItem")
                      .Filter(new() { HasText = "Bates, Timothy" })
                      .ClickAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Save & Next" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            Console.WriteLine("Assessment Selected");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=Selected Candidates", new PageWaitForSelectorOptions { Timeout = 25000 });
            //await page.WaitForSelectorAsync("text=3002, User", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.Locator("label", new() { HasTextString = reqsetup_Candidate }).ClickAsync();
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
            Console.WriteLine("Req Setup Finished");


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
            await page.GetByText(select_Assessor).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            Console.WriteLine("Assessor Selected");
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/jam-medical/dynamicform/**");
            await page.GetByRole(AriaRole.Button, new() { Name = "Close", Exact = true }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            Console.WriteLine("Assessment Opened");

            var registrarLabel = page.Locator("label[for='mat-radio-5-input']");
            var radioInput = page.Locator("#mat-radio-5-input");

            if (await radioInput.IsEnabledAsync())
            {
                await registrarLabel.ClickAsync();
                Console.WriteLine("Level 'Registrar' selected");
            }
            else
            {
                Console.WriteLine("Level disabled, skipping...");
            }

            await page.Locator("#mat-radio-12").ClickAsync();
            await page.Locator("#mat-radio-19").ClickAsync();
            await page.Locator("#mat-radio-26").ClickAsync();
            await page.Locator("#mat-radio-33").ClickAsync();
            await page.Locator("#mat-radio-40").ClickAsync();
            await page.Locator("#mat-radio-47").ClickAsync();
            await page.Locator("#mat-radio-54").ClickAsync();
            await page.Locator("#mat-radio-61").ClickAsync();
            await page.Locator("#mat-radio-68").ClickAsync();
            await page.Locator("#mat-radio-75").ClickAsync();
            await page.Locator("#mat-radio-82").ClickAsync();
            await page.Locator("#mat-radio-89").ClickAsync();
            await page.Locator("#mat-radio-96").ClickAsync();
            await page.Locator("#mat-radio-103").ClickAsync();
            await page.Locator("#mat-radio-110").ClickAsync();
            await page.Locator("#mat-radio-117").ClickAsync();
            await page.Locator("#mat-radio-124").ClickAsync();
            await page.Locator("#mat-radio-131").ClickAsync();
            await page.Locator("#mat-radio-138").ClickAsync();
            await page.Locator("#mat-radio-145").ClickAsync();
            await page.Locator("#mat-radio-152").ClickAsync();
            await page.Locator("#mat-radio-159").ClickAsync();
            await page.Locator("#mat-radio-166").ClickAsync();
            await page.Locator("#mat-radio-173").ClickAsync();
            await page.Locator("#mat-radio-180").ClickAsync();
            await page.Locator("#mat-radio-187").ClickAsync();
            await page.Locator("#mat-radio-194").ClickAsync();
            await page.Locator("#mat-radio-201").ClickAsync();
            await page.Locator("#mat-radio-203").ClickAsync();

            await page.WaitForSelectorAsync("button:has-text('Submit'), button:has-text('Submit With Signature')",
                new PageWaitForSelectorOptions { Timeout = 15000 });

            if (await submitWithSignature.IsVisibleAsync())
            {
                Console.WriteLine("🖊 Detected 'Submit With Signature' flow...");

                await submitWithSignature.ClickAsync();

                var canvases = await page.QuerySelectorAllAsync("//div[@class='sig-background m-t-10']//canvas");
                var canvas = canvases.Last();
                var boundingBox = await canvas.BoundingBoxAsync();

                if (boundingBox != null)
                {
                    await page.Mouse.MoveAsync(boundingBox.X + 10, boundingBox.Y + 10);
                    await page.Mouse.DownAsync();
                    await page.Mouse.MoveAsync(boundingBox.X + 100, boundingBox.Y + 100);
                    await page.Mouse.MoveAsync(boundingBox.X + 200, boundingBox.Y + 100);
                    await page.Mouse.MoveAsync(boundingBox.X + 300, boundingBox.Y + 200);
                    await page.Mouse.UpAsync();
                }

                // Now click Submit
                await submit.ClickAsync();
                Console.WriteLine("Form submitted with signature.");
            }
            else if (await submit.IsVisibleAsync())
            {
                Console.WriteLine("Detected 'Submit' flow...");
                await submit.ClickAsync();
                Console.WriteLine("Form submitted without signature.");
            }
            else
            {
                throw new Exception("Neither 'Submit' nor 'Submit With Signature' button is visible.");
            }


            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);

            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by candidate");

            //Impersonation to Assessor
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "5" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { imp_Assessor });
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

            await page.GetByText("Nursing staff").ClickAsync();
            await page.GetByText("Registrars").ClickAsync();
            await page.GetByText("Allied health professionals").ClickAsync();
            await page.GetByText("Other specialists").ClickAsync();
            await page.GetByText("Other (please specify)").ClickAsync();
            await page.Locator("input[name=\"typename\"]").FillAsync("QA" + formattedDate);
            await page.Locator("#mat-radio-11").ClickAsync();
            await page.Locator("#mat-radio-18").ClickAsync();
            await page.Locator("#mat-radio-25").ClickAsync();
            await page.Locator("#mat-radio-32").ClickAsync();
            await page.Locator("#mat-radio-39").ClickAsync();
            await page.Locator("#mat-radio-46").ClickAsync();
            await page.Locator("#mat-radio-53").ClickAsync();
            await page.Locator("#mat-radio-60").ClickAsync();
            await page.Locator("#mat-radio-67").ClickAsync();
            await page.Locator("#mat-radio-74").ClickAsync();
            await page.Locator("#mat-radio-81 label").ClickAsync();
            await page.Locator("textarea.form-control").Nth(0).FillAsync("QA" + currentDateTime);
            await page.Locator("#mat-radio-87").ClickAsync();
            await page.Locator("#mat-radio-94").ClickAsync();
            await page.Locator("#mat-radio-101").ClickAsync();
            await page.Locator("#mat-radio-108").ClickAsync();
            await page.Locator("#mat-radio-115").ClickAsync();
            await page.Locator("#mat-radio-122").ClickAsync();
            await page.Locator("#mat-radio-136").ClickAsync();
            await page.Locator("#mat-radio-129").ClickAsync();
            await page.Locator("#mat-radio-143 label").ClickAsync();
            await page.Locator("textarea.form-control").Nth(1).FillAsync("QA" + currentDateTime);
            await page.Locator("#mat-radio-149").ClickAsync();
            await page.Locator("#mat-radio-156").ClickAsync();
            await page.Locator("#mat-radio-163").ClickAsync();
            await page.Locator("#mat-radio-170").ClickAsync();
            await page.Locator("#mat-radio-177").ClickAsync();
            await page.Locator("#mat-radio-184").ClickAsync();
            await page.Locator("#mat-radio-191 label").ClickAsync();
            await page.Locator("textarea.form-control").Nth(2).FillAsync("QA" + currentDateTime);
            await page.Locator("#mat-radio-197").ClickAsync();
            await page.Locator("#mat-radio-204").ClickAsync();
            await page.Locator("#mat-radio-211").ClickAsync();
            await page.Locator("#mat-radio-218").ClickAsync();
            await page.Locator("#mat-radio-225 label").ClickAsync();
            await page.Locator("textarea.form-control").Nth(3).FillAsync("QA" + currentDateTime);
            await page.GetByText("Satisfactory ( The prevocational doctor has met or exceeded performance").ClickAsync();
            await page.Locator("textarea.form-control").Nth(4).FillAsync("QA" + currentDateTime);
            await page.Locator("textarea.form-control").Nth(5).FillAsync("QA" + currentDateTime);

            await page.WaitForSelectorAsync("button:has-text('Submit'), button:has-text('Submit With Signature')",
                new PageWaitForSelectorOptions { Timeout = 15000 });

            if (await submitWithSignature.IsVisibleAsync())
            {
                Console.WriteLine("🖊 Detected 'Submit With Signature' flow...");

                await submitWithSignature.ClickAsync();

                var canvases = await page.QuerySelectorAllAsync("//div[@class='sig-background m-t-10']//canvas");
                var canvas = canvases.Last();
                var boundingBox = await canvas.BoundingBoxAsync();

                if (boundingBox != null)
                {
                    await page.Mouse.MoveAsync(boundingBox.X + 10, boundingBox.Y + 10);
                    await page.Mouse.DownAsync();
                    await page.Mouse.MoveAsync(boundingBox.X + 100, boundingBox.Y + 100);
                    await page.Mouse.MoveAsync(boundingBox.X + 200, boundingBox.Y + 100);
                    await page.Mouse.MoveAsync(boundingBox.X + 300, boundingBox.Y + 200);
                    await page.Mouse.UpAsync();
                }

                // Now click Submit
                await submit.ClickAsync();
                Console.WriteLine("Form submitted with signature.");
            }
            else if (await submit.IsVisibleAsync())
            {
                Console.WriteLine("Detected 'Submit' flow...");
                await submit.ClickAsync();
                Console.WriteLine("Form submitted without signature.");
            }
            else
            {
                throw new Exception("Neither 'Submit' nor 'Submit With Signature' button is visible.");
            }


            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);

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
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { imp_Candidate });
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
            Console.WriteLine("Assessment Opened by JMO for review");

            await page.Locator("#mat-checkbox-6 > .mat-checkbox-layout > .mat-checkbox-inner-container").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Agreed" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/jam-medical/assessments");
            Console.WriteLine("Assessment agreed by JMO");

            //End Impersonation HP3
            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
            Thread.Sleep(1500);
            await page.GetByText("End Impersonation").ClickAsync();
            await page.WaitForURLAsync("**/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("Impersonation ended by JMO");

            //Impersonation to DCT for signoff
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "19" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { imp_DCT });
            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.WaitForURLAsync("**/home");
            Console.WriteLine("Impersonation with DCT");

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
            Console.WriteLine("Assessment Opened by DCT");

            await page.GetByPlaceholder("Sign Off Authority Comments").FillAsync("QA" + currentDateTime);
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

            //View Req Page
            await page.Locator("#tooltiphelpRequirements").ClickAsync();
            await page.WaitForURLAsync("**/RequirementManagement/ViewRequirement.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=View Requirements", new PageWaitForSelectorOptions { Timeout = 25000 });
            await page.Locator("#cphMain_UserTagSearch_pnlCandidatesList").GetByText("Unselect All").ClickAsync();
            await page.GetByText(reqsetup_Candidate).ClickAsync();
            await page.Locator("#cphMain_pnlStatusForAdminOnly").GetByText("Unselect All").ClickAsync();
            await page.Locator("#diviFormStatuses").GetByText("Completed").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").ClickAsync();
            await page.Locator("#ctl00_cphMain_rgActionItem_ctl00_ctl02_ctl02_FilterTextBox_TemplateColumn").FillAsync("QA " + formattedDate);
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

            Thread.Sleep(5000);


        }
    }

}


