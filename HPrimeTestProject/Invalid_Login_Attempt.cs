using Microsoft.Playwright;

namespace HPrimeTestProject
{
    internal class Invalid_Login_Attempt
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

            // Login HBH-WBA(Beta)
            await page.ClickAsync("#HBH_LoginDiv");

            var username = "**********";
            var password = "**********";

            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine($"Attempt {i + 1}: Logging in with incorrect credentials.");

                await page.FillAsync("#txtUserName", username);
                await page.FillAsync("#txtPassword", password);

                await page.ClickAsync("#btn_Login");

                try
                {
                    var errorMessage = await page.WaitForSelectorAsync("text=Please enter a valid username and password", new PageWaitForSelectorOptions { Timeout = 3000 });
                    if (errorMessage != null)
                    {
                        Console.WriteLine("Login failed as expected.");
                    }
                    else
                    {
                        Console.WriteLine("No error message found.");
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Timeout waiting for error message. The login attempt may not have been processed.");
                }

                await Task.Delay(1000);
            }
            await browser.CloseAsync();
        }
    }
}
