using Microsoft.Playwright;
using NUnit.Framework.Internal;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using Codeuctivity.ImageSharpCompare;
using Image = SixLabors.ImageSharp.Image;
//using SixLabors.ImageSharp;

namespace PlaywrightDemo
{
    public class Tests
    {
        //public static async Task Main()
        //{
        //    using var playwright = await Playwright.CreateAsync();
        //    await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
        //    var page = await browser.NewPageAsync();
        //    await page.GotoAsync("https://playwright.dev/dotnet");
        //    await page.ScreenshotAsync(new() { Path = "screenshot.png" });
        //}

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1Async()
        {;
            var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://playwright.dev/dotnet");
            //var screenshot = await page.ScreenshotAsync(new() { Path = "screenshot.png" });

            var LocalScreenshotActual = @"C:\\Users\\DDimov\\source\\repos\\denislavdimov\\LVMasterAutomationDemo\\PlaywrightDemo\\bin\\Debug\\net6.0\\screenshot.png";
            var LocalScreenshotExpected = @"C:\\Users\\DDimov\\source\\repos\\denislavdimov\\LVMasterAutomationDemo\\PlaywrightDemo\\bin\\Debug\\net6.0\\testpic.png";
            var actual = Image.Load(LocalScreenshotActual);
            var expected = Image.Load(LocalScreenshotExpected);
            Assert.IsTrue(ImageSharpCompare.ImagesAreEqual(actual, expected));
        }
    }
}