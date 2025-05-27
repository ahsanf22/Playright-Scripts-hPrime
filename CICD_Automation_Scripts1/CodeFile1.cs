using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace HPrimeTestProject
{
    public class Tests
    {
        private static Random random = new Random();

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

            await page.GotoAsync("https://hospital-beta.hprime.com.au/Login.aspx");
            await page.GetByRole(AriaRole.Link, new() { Name = "HBH" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"Attempt: {i + 1}");  // Output the iteration count (1-indexed)

                // Simulate human-like delays
                await RandomDelay(1000, 2000);
                var usernameField = page.GetByPlaceholder("Username");
                await MoveCursorToElement(usernameField);
                await RandomTyping(usernameField, "jamweb.admin");
                await RandomDelay(1000, 2000);

                var passwordField = page.GetByPlaceholder("Password");
                await MoveCursorToElement(passwordField);
                await RandomTyping(passwordField, "JAMWeb@605");
                await RandomDelay(1000, 2000);

                // Attempt to login with a pause before clicking
                await RandomDelay(500, 1000);
                await ClickRandomlyNearElement(page, "#btn_Login");
                await page.WaitForLoadStateAsync(LoadState.Load);
            }

            // Final attempt after loop with correct credentials and backspacing
            var finalUsernameField = page.GetByPlaceholder("Username");
            await MoveCursorToElement(finalUsernameField);
            await RandomTypingWithBackspace(finalUsernameField, "jamweb.admin.hbh"); // Typing with backspaces
            await RandomDelay(1000, 2000);

            var finalPasswordField = page.GetByPlaceholder("Password");
            await MoveCursorToElement(finalPasswordField);
            await RandomTypingWithBackspace(finalPasswordField, "JAMWeb@605"); // Typing with backspaces
            await RandomDelay(1000, 2000);

            await RandomDelay(500, 1000);
            await ClickRandomlyNearElement(page, "#btn_Login");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await RandomDelay(5000, 10000);

            // Close the browser
            await browser.CloseAsync();
        }

        private async Task RandomDelay(int minMilliseconds, int maxMilliseconds)
        {
            int delay = random.Next(minMilliseconds, maxMilliseconds);
            await Task.Delay(delay);
        }

        private async Task RandomTyping(ILocator element, string text)
        {
            foreach (char c in text)
            {
                await element.TypeAsync(c.ToString());
                await RandomDelay(100, 300); // Random delay between keystrokes
            }
        }

        private async Task MoveCursorToElement(ILocator element)
        {
            var box = await element.BoundingBoxAsync();
            if (box != null)
            {
                // Generate a smooth mouse movement path
                int steps = 10;
                for (int step = 0; step < steps; step++)
                {
                    int x = random.Next((int)box.X + 5, (int)(box.X + box.Width - 5));
                    int y = random.Next((int)box.Y + 5, (int)(box.Y + box.Height - 5));
                    await element.Page.Mouse.MoveAsync(x, y);
                    await RandomDelay(50, 100); // Small delay for a more natural movement
                }
                await RandomDelay(500, 1000); // Simulate a small delay after moving the cursor
            }
        }

        private async Task ClickRandomlyNearElement(IPage page, string selector)
        {
            var element = page.Locator(selector);
            var box = await element.BoundingBoxAsync();
            if (box != null)
            {
                // Generate random coordinates near the center of the element
                int x = random.Next((int)(box.X + 5), (int)(box.X + box.Width - 5));
                int y = random.Next((int)(box.Y + 5), (int)(box.Y + box.Height - 5));

                // Move the mouse to the calculated coordinates
                await page.Mouse.MoveAsync(x, y);
                await RandomDelay(500, 1000); // Delay before clicking
                await page.Mouse.ClickAsync(x, y); // Click at the random position
            }
        }

        private async Task RandomTypingWithBackspace(ILocator element, string text)
        {
            foreach (char c in text)
            {
                await element.TypeAsync(c.ToString());
                await RandomDelay(100, 300); // Random delay between keystrokes

                // Occasionally simulate a backspace to mimic human error
                if (random.Next(0, 10) < 3) // 30% chance to backspace
                {
                    await element.PressAsync("Backspace");
                    await RandomDelay(100, 300); // Delay after backspacing
                }
            }
        }
    }
}
