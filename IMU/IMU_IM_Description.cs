using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace IMU
{
    internal class IMU_IM_Description
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
                ExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"
            };
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://imu-test.hprime.com.au/login.aspx");




            //Login IMU(Test)
            var username = "Jamweb.admin.imu";
            var password = "JAMWeb@605";
            

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
            var formName1 = "S9 IM Optional PT Insertion of Dialysis Catheter";
            await page.GetByRole(AriaRole.Tab, new() { Name = "Create" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForSelectorAsync("text=" + formName1, new PageWaitForSelectorOptions { Timeout = 25000 });



            //Select Form 1
            await page.GetByRole(AriaRole.Heading, new() { Name = formName1 }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForURLAsync("https://imu-test-hp3.hprime.com.au/jam-medical/dynamicform/**");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=" + formName1, new PageWaitForSelectorOptions { Timeout = 25000 });
            var submitButton = page.Locator("#btnSubmitWithSignature");
            await submitButton.WaitForAsync(new() { State = WaitForSelectorState.Visible });


            //Verify Form Descriptor 1

            var isVisible = await page.Locator("text=Topic").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Dialysis catheter insertion").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("b:text('Task')").IsVisibleAsync(); // try this version
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Observe or assist in the procedure. Students able to describe and interpret:").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Equipment required").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Indications and contraindications for the procedures").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Preparation and conducting the task").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Important complications").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Post procedural care").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Setting").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=This task must be completed during the internal medicine rotation semester 9.").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Level of competency").IsVisibleAsync();
            Assert.IsTrue(isVisible);

            isVisible = await page.Locator("text=Level 2").IsVisibleAsync();
            Assert.IsTrue(isVisible);


            await page.GetByRole(AriaRole.Button, new() { Name = "Back to Assessments" }).ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });


            //Select Create Tab
            var formName2 = "S9 IM Optional PT Insertion of Dialysis Catheter";
            await page.GetByRole(AriaRole.Tab, new() { Name = "Create" }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForSelectorAsync("text=" + formName2, new PageWaitForSelectorOptions { Timeout = 25000 });



            //Select Form 2
            await page.GetByRole(AriaRole.Heading, new() { Name = formName2 }).ClickAsync();

            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });

            await page.WaitForURLAsync("https://imu-test-hp3.hprime.com.au/jam-medical/dynamicform/**");
            await page.WaitForLoadStateAsync(LoadState.Load);

            await page.WaitForSelectorAsync("text=" + formName2, new PageWaitForSelectorOptions { Timeout = 25000 });
            await submitButton.WaitForAsync(new() { State = WaitForSelectorState.Visible });


            //Verify Form Descriptor 2

            //var isVisible = await page.Locator("text=Topic").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Dialysis catheter insertion").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            //await page.Locator("text=Task").IsVisibleAsync();
            //Assert.IsTrue(isVisible);
            await page.Locator("text=Observe or assist in the procedure. Students able to describe and interpret:").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Equipment required").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Indications and contraindications for the procedures").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Preparation and conducting the task").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Important complications").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Post procedural care").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Setting").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=This task must be completed during the internal medicine rotation semester 9.").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Level of competency").IsVisibleAsync();
            Assert.IsTrue(isVisible);
            await page.Locator("text=Level 2").IsVisibleAsync();
            Assert.IsTrue(isVisible);


            await page.GetByRole(AriaRole.Button, new() { Name = "Back to Assessments" }).ClickAsync();
            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

            {
                State = WaitForSelectorState.Hidden,
                Timeout = 30000
            });








        }
    }
}