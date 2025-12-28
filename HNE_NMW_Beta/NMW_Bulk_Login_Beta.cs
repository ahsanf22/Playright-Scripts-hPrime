using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HNE_NMW_Beta
{
    internal class NMW_Bulk_Login_Beta
    {
        [Test]
        public async Task Test()
        {
            List<string> Users = new List<string>();
            //Gradstart Users
            Users.Add("56162581");
            Users.Add("50007492");
            Users.Add("56151755");
            Users.Add("60166913");
            Users.Add("60209621");
            Users.Add("60112378");
            Users.Add("60066522");
            Users.Add("56003676");
            Users.Add("53016773");
            Users.Add("50030250");
            Users.Add("40035018");
            Users.Add("56001247");
            Users.Add("50035501");
            Users.Add("52809914");
            Users.Add("60024761");
            Users.Add("60029385");
            Users.Add("60006919");
            Users.Add("50032050");
            Users.Add("40045941");
            Users.Add("60110691");
            Users.Add("60106326");
            Users.Add("50012710");
            Users.Add("56005279");
            Users.Add("60210014");
            Users.Add("60176166");
            Users.Add("60001880");
            Users.Add("50034418");
            Users.Add("60368231");
            Users.Add("60111883");
            Users.Add("60007838");
            Users.Add("50002779");
            Users.Add("60007338");
            Users.Add("56000317");
            Users.Add("56149388");
            Users.Add("60102899");
            Users.Add("50036125");
            Users.Add("50021055");

            string password = "***********";

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


            for (int i = 0; i < Users.Count; i++)
            {
                Console.WriteLine($"Attempt {i + 1}: Logging User.");

                await page.Locator("#LoginSlider div").Filter(new() { HasText = "HBH" }).ClickAsync();
                await page.WaitForLoadStateAsync(LoadState.Load);
                await page.GetByPlaceholder("Username").FillAsync(Users[i]);
                Thread.Sleep(500);
                await page.GetByPlaceholder("Password").FillAsync(password);
                await page.ClickAsync("#btn_Login"); 
                await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/Nursing/ManageInterviews.aspx");
                await page.WaitForLoadStateAsync(LoadState.Load);
                await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
                Thread.Sleep(2000);
                await page.ClickAsync("#user-menu-hprime-btn");
                await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
                await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
                await page.WaitForLoadStateAsync(LoadState.Load);
                await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
                //Thread.Sleep(3000);

                try
                {
                    var errorMessage = await page.WaitForSelectorAsync("text=Manage Interviews", new PageWaitForSelectorOptions { Timeout = 3000 });
                    if (errorMessage != null)
                    {
                        Console.WriteLine("Invalid credentials.");
                    }
                    else
                    {
                        Console.WriteLine("No error message found.");
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("User logged in.");
                    //}

                    //await Task.Delay(1000);
                }
            }
        }
    }
}
