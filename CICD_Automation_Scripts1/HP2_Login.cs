using Microsoft.Playwright;
using NUnit.Framework;

namespace CICD_Automation_Scripts1
{
    internal class HP2_Login
    {
        [Test]
        public async Task Test()
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

            var hbhpgy_un = "Jamweb.admin.hbhpgy";
            var hbhpgy_pw = "JAMWeb@605";

            var hbhwba_un = "Jamweb.admin.hbh";
            var hbhwba_pw = "JAMWeb@605";

            var mbhpgy_un = "Jamweb.admin.mbh";
            var mbhpgy_pw = "JAMWeb@605";

            var mbhwba_un = "Jamweb.admin.mbhwba";
            var mbhwba_pw = "JAMWeb@605";

            var sjog_un = "Jamweb.admin.sjog";
            var sjog_pw = "JAMWeb@605";

            var hnepgy_un = "Jamweb.admin.hnepgy";
            var hnepgy_pw = "JAMWeb@605";

            var hnewba_un = "Jamweb.admin.hne";
            var hnewba_pw = "JAMWeb@605";

            var lmh_un = "Jamweb.admin.lmh";
            var lmh_pw = "JAMWeb@605";

            var cclhd_un = "Jamweb.admin.cclhd";
            var cclhd_pw = "JAMWeb@605";

            var swslhd_un = "Jamweb.admin.swslhd";
            var swslhd_pw = "JAMWeb@605";

            //var imu_un = "Jamweb.admin.imu";
            //var imu_pw = "JAMWeb@605";

            var ths_un = "Jamweb.admin.ths";
            var ths_pw = "JAMWeb@605";

            var nbmwba_un = "Jamweb.admin.nbmwba";
            var nbmwba_pw = "JAMWeb@605";

            //HBH-PGY
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", hbhpgy_un);
            await page.FillAsync("#txtPassword", hbhpgy_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.hbhpgy logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.hbhpgy sign out");

            //HBH-WBA
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", hbhwba_un);
            await page.FillAsync("#txtPassword", hbhwba_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.hbhwba logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.hbhwba sign out");

            //MBH-PGY
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", mbhpgy_un);
            await page.FillAsync("#txtPassword", mbhpgy_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.mbh logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.mbh sign out");

            //MBH-WBA
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", mbhwba_un);
            await page.FillAsync("#txtPassword", mbhwba_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.mbhwba logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.mbhwba sign out");

            //HNE-PGY
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", hnepgy_un);
            await page.FillAsync("#txtPassword", hnepgy_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.hnepgy logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.hnepgy sign out");

            //HNE-WBA
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", hnewba_un);
            await page.FillAsync("#txtPassword", hnewba_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.hnewba logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.hnewba sign out");

            //SJOG
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", sjog_un);
            await page.FillAsync("#txtPassword", sjog_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.sjog logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.sjog sign out");

            //LMH
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", lmh_un);
            await page.FillAsync("#txtPassword", lmh_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.lmh logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.lmh sign out");

            //CCLHD
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", cclhd_un);
            await page.FillAsync("#txtPassword", cclhd_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.cclhd logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.cclhd sign out");

            //SWSLHD
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", swslhd_un);
            await page.FillAsync("#txtPassword", swslhd_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.swslhd logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.swslhd sign out");

            ////IMU
            //await page.ClickAsync("#HBH_LoginDiv");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.FillAsync("#txtUserName", imu_un);
            //await page.FillAsync("#txtPassword", imu_pw);
            //await page.ClickAsync("#btn_Login");
            //await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            //Console.WriteLine("jamweb.admin.imu logged in");

            //await page.ClickAsync("#user-menu-hprime-btn");
            //await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            //await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            //await page.WaitForLoadStateAsync(LoadState.Load);
            //await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            //Console.WriteLine("jamweb.admin.imu sign out");

            //THS
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", ths_un);
            await page.FillAsync("#txtPassword", ths_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.ths logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Select your organisation", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.ths sign out");

            //NBM-WBA
            await page.ClickAsync("#HBH_LoginDiv");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.FillAsync("#txtUserName", nbmwba_un);
            await page.FillAsync("#txtPassword", nbmwba_pw);
            await page.ClickAsync("#btn_Login");
            await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=Dashboard", new PageWaitForSelectorOptions { Timeout = 25000 });
            Console.WriteLine("jamweb.admin.nbmwba logged in");

            await page.ClickAsync("#user-menu-hprime-btn");
            await page.GetByText("Sign Out", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            await page.WaitForSelectorAsync("text=User logged out successfully", new PageWaitForSelectorOptions { Timeout = 25000 });
            
            await page.GotoAsync("https://hospital-beta.hprime.com.au/");
            await page.WaitForLoadStateAsync(LoadState.Load);
            Console.WriteLine("jamweb.admin.nbmwba sign out");
        }
    }
}