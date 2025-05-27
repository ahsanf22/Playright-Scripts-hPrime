//using Microsoft.Playwright;
//using NUnit.Framework;

//namespace CICD_Automation_Scripts1
//{
//    internal class HP3_Login
//    {
//        [Test]
//        public async Task Test()
//        {
//            var launchOptions = new BrowserTypeLaunchOptions
//            {
//                Headless = false,
//                Args = new List<string> { "--start-maximized" },
//                SlowMo = 50,
//                Timeout = 80000,
//            };
//            using var playwright = await Playwright.CreateAsync();
//            await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
//            var context = await browser.NewContextAsync(new BrowserNewContextOptions
//            {
//                ViewportSize = ViewportSize.NoViewport
//            });
//            var page = await context.NewPageAsync();
//            await page.GotoAsync("https://hospital-beta.hprime.com.au/");

//            var loaderSelector3 = "#loadingSpinnerId"; //HP3 loader

//            var hbhpgy_un = "Jamweb.jmo.hbhpgy";
//            var hbhpgy_pw = "JAMWeb@605";

//            var hbhwba_un = "Jamweb.jmo.hbh";
//            var hbhwba_pw = "JAMWeb@605";



//            //HBH-PGY
//            await page.ClickAsync("#HBH_LoginDiv");
//            await page.WaitForLoadStateAsync(LoadState.Load);
//            await page.FillAsync("#txtUserName", hbhpgy_un);
//            await page.FillAsync("#txtPassword", hbhpgy_pw);
//            await page.ClickAsync("#btn_Login");
//            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
//            await page.WaitForURLAsync("**/home");
//            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

//            {
//                State = WaitForSelectorState.Hidden,
//                Timeout = 30000
//            });

//            await page.WaitForLoadStateAsync(LoadState.Load);
//            await page.WaitForSelectorAsync("text=Hervey Bay Hospital", new PageWaitForSelectorOptions { Timeout = 25000 });
//            Console.WriteLine("jamweb.jmo.hbhpgy Logged in");

//            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
//            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Sign Out" }).ClickAsync();
//            await page.GetByRole(AriaRole.Button, new() { Name = "Yes" }).ClickAsync();
//            await page.WaitForLoadStateAsync(LoadState.Load);
//            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
//            Console.WriteLine("jamweb.jmo.hbhpgy sign out");

//            //HBH-WBA
//            await page.ClickAsync("#HBH_LoginDiv");
//            await page.WaitForLoadStateAsync(LoadState.Load);
//            await page.FillAsync("#txtUserName", hbhwba_un);
//            await page.FillAsync("#txtPassword", hbhwba_pw);
//            await page.ClickAsync("#btn_Login");
//            await page.WaitForURLAsync("https://hp3-test.hprime.com.au/**");
//            await page.WaitForURLAsync("**/home");
//            await page.WaitForSelectorAsync(loaderSelector3, new PageWaitForSelectorOptions

//            {
//                State = WaitForSelectorState.Hidden,
//                Timeout = 30000
//            });

//            await page.WaitForLoadStateAsync(LoadState.Load);
//            await page.WaitForSelectorAsync("text=Hervey Bay Hospital", new PageWaitForSelectorOptions { Timeout = 25000 });
//            Console.WriteLine("jamweb.jmo.hbh Logged in");

//            await page.GetByRole(AriaRole.Button, new() { Name = "user" }).ClickAsync();
//            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Sign Out" }).ClickAsync();
//            await page.GetByRole(AriaRole.Button, new() { Name = "Yes" }).ClickAsync();
//            await page.WaitForLoadStateAsync(LoadState.Load);
//            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
//            Console.WriteLine("jamweb.jmo.hbh sign out");
//        }
//    }
//}















using Microsoft.Playwright;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            var username = "Jamweb.admin.hbhpgy";
            var password = "JAMWeb@605";


            // Mocking the reCAPTCHA API call
            await page.RouteAsync("https://www.google.com/recaptcha/api/siteverify", async route =>
            {
                await route.FulfillAsync(new RouteFulfillOptions
                {
                    ContentType = "application/json",
                    Body = "{\"success\": true, \"score\": 0.9}" // Simulating a high human score
                });
            });

            await page.ClickAsync("#HBH_LoginDiv");
            await page.FillAsync("#txtUserName", username);
            await page.FillAsync("#txtPassword", password);
            await page.ClickAsync("#btn_Login");

            // Wait for the dashboard URL and load state
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });

            // Additional checks or actions can go here

            // Close the browser context after the test
            await context.CloseAsync();
        }
    }
}
