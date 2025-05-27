using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;

namespace IMU
{
    internal class IMU_S9_AMT_Overall_End_of_Posting
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
            await page.GotoAsync("https://imu-test.hprime.com.au/login.aspx");




            //Login IMU (Test)
            var username = "Jamweb.admin.imu";
            var password = "JAMWeb@605";
            var formName = "S9 AMT Overall End of Posting Assessment";

            DateTime currentDate = DateTime.Today;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");

            DateTime currentDateTime = DateTime.Now;

            var loaderSelector = "#loading-hprime"; //Normal circle loader
            var loaderSelector2 = "#rlpddlUsers"; //Impersonation users data loader
            var loaderSelector3 = "#loadingSpinnerId"; //HP3 loader
            var loaderSelector4 = "#rlpcphMain_pnlSearchResults"; //view req loader, on search using text filter

            var idElement = page.Locator("#ctl00_cphMain_ucWidgetFormsAndAssessments_rgActionItem_ctl00_ctl03_ctl01_PageSizeComboBox_Arrow");

            //await page.ClickAsync("#IMU_LoginDiv");
            await page.FillAsync("#txtUserName", username);
            await page.FillAsync("#txtPassword", password);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://imu-test.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("JAMWeb Admin Logged in");

            //Impersonation to Student
            await page.ClickAsync("#user-menu-hprime-btn");
            await page.Locator("#ddlRoles").SelectOptionAsync(new[] { "2" });
            await page.WaitForSelectorAsync(loaderSelector2, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });
            await page.Locator("#ddlUsers").SelectOptionAsync(new[] { "3987" });
            await page.WaitForURLAsync("https://imu-test-hp3.hprime.com.au/**");
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


            //Select Create Tab
            await page.GetByRole(AriaRole.Tab, new() { Name = "Create" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForSelectorAsync("text=" + formName, new PageWaitForSelectorOptions { Timeout = 25000 });



            //Select Form
            await page.GetByRole(AriaRole.Heading, new() { Name = formName }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForURLAsync("https://imu-test-hp3.hprime.com.au/jam-medical/awaiting-assessor-selection/**");
            await page.GetByText("Please select assessor").ClickAsync();
            await page.GetByText("Ooi Cheow Peng").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });


            //Form Open
            await page.WaitForURLAsync("https://imu-test-hp3.hprime.com.au/jam-medical/dynamicform/**");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=" + formName, new PageWaitForSelectorOptions { Timeout = 25000 });
            var submitButton = page.Locator("#btnSubmitWithSignature");
            await submitButton.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            await page.GetByText("Good (4)").ClickAsync();

            await page.GetByRole(AriaRole.Textbox).First.FillAsync("QA");
            //await page.Locator("form-field").Filter(new() { HasText = "0/500 characters" }).GetByRole(AriaRole.Textbox).ClickAsync();
            await page.Locator("form-field").Filter(new() { HasText = "0/500 characters" }).GetByRole(AriaRole.Textbox).FillAsync("QA");



            await submitButton.ClickAsync();

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

            //Submit Form
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.GetByRole(AriaRole.Button, new() { Name = "Ok" }).ClickAsync();

            await page.WaitForURLAsync("**/jam-medical/assessments");
            await page.WaitForLoadStateAsync(LoadState.Load);



            Thread.Sleep(5000);






        }
    }
}
