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
            Users.Add("60128773"); //01
            Users.Add("50015582"); //02
            Users.Add("50001761"); //03
            Users.Add("60087360"); //04
            Users.Add("60032575"); //05
            Users.Add("50031750"); //06
            Users.Add("60153506"); //07
            Users.Add("50027122"); //08
            Users.Add("50000575"); //09
            Users.Add("56144289"); //10
            Users.Add("50026576"); //11
            Users.Add("56149819"); //12
            Users.Add("60068295"); //13
            Users.Add("60090440"); //14
            Users.Add("60048411"); //15
            Users.Add("50001284"); //16 E (R)
            Users.Add("60092584"); //17
            Users.Add("56109122"); //18
            Users.Add("60059085"); //19
            Users.Add("56152340"); //20
            Users.Add("50027424"); //21
            Users.Add("60125313"); //22
            Users.Add("25135550"); //23
            Users.Add("50002538"); //24
            Users.Add("60175844"); //25
            Users.Add("50025615"); //26
            Users.Add("53037240"); //27 E (R)
            Users.Add("50009226"); //28
            Users.Add("50000736"); //29
            Users.Add("60110708"); //30
            Users.Add("50009214"); //31
            Users.Add("60065505"); //32 WC (R)
            Users.Add("40027326"); //33
            Users.Add("60007885"); //34
            Users.Add("60010289"); //35
            Users.Add("56009710"); //36
            Users.Add("60010292"); //37
            Users.Add("50001718"); //38 WC (R)
            Users.Add("60010341"); //39
            Users.Add("50011997"); //40
            Users.Add("60066529"); //41
            Users.Add("60064954"); //42
            Users.Add("50026048"); //43
            Users.Add("60011271"); //44
            Users.Add("50016839"); //45
            Users.Add("60089759"); //46
            Users.Add("50025730"); //47
            Users.Add("56140246"); //48
            Users.Add("50019368"); //49
            Users.Add("60127397"); //50
            Users.Add("56162015"); //51 WC (R)
            Users.Add("60165161"); //52
            Users.Add("60142105"); //53
            Users.Add("60043089"); //54
            Users.Add("60060928"); //55
            Users.Add("60065348"); //56
            Users.Add("50000173"); //57
            Users.Add("60091368"); //58
            Users.Add("50017720"); //59
            Users.Add("56160455"); //60 WC (R)
            Users.Add("23906284"); //61
            Users.Add("50000123"); //62
            Users.Add("56001288"); //63
            Users.Add("60012555"); //64
            Users.Add("60144843"); //65
            Users.Add("60111077"); //66
            Users.Add("50015548"); //67 WC (R)
            Users.Add("50000731"); //68
            Users.Add("60006926"); //69
            Users.Add("50022093"); //70
            ////Users.Add("56001094"); Admin 71
            ////Users.Add("60007116"); Admin 72
            ////Users.Add("50025326"); Admin/Panelist 73
            Users.Add("60001365"); //74
            Users.Add("60043422"); //75
            Users.Add("60010293"); //76
            Users.Add("60079872"); //77
            Users.Add("56005393"); //78
            Users.Add("50009223"); //79
            Users.Add("50032610"); //80
            Users.Add("50020831"); //81
            Users.Add("50001648"); //82
            Users.Add("56160603"); //Alex Joseph 83

            string password = "Newcastle#123";

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
                await page.WaitForURLAsync("https://hospital-beta.hprime.com.au/General/Dashboard.aspx");
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
