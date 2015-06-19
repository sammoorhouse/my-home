using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace DataProvider.Centaline
{
    public class Gatherer
    {
        private readonly Random _random = new Random();

        public void Start(string url)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMinutes(1));
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            var rent = driver.FindElement(By.Id("ctl00_mnuSTATELYHOME_imgRent"));
            rent.Click();

            NavigateToRandomPropertyFromSearchPage(driver);
            var details = GetPropertyDetails(driver);
        }

        private void NavigateToRandomPropertyFromSearchPage(IWebDriver driver)
        {
            var pageItems = driver.FindElements(By.XPath(@"//div[@style=""position: relative; width: 178px;""]"));
            var targets = pageItems.Select(x => x.FindElement(By.TagName("a"))).ToList();

            var targetToClick = targets[_random.Next(0, targets.Count)];
            targetToClick.Click();
        }

        private Dictionary<string, string> GetPropertyDetails(IWebDriver driver)
        {
            var table = driver.FindElement(By.XPath(@"//table[@style=""width:340px;border-collapse:collapse;""]"));
            var labels = table.FindElements(By.ClassName("yellow1"));
            var items = labels.Select(x => new
            {
                Description = x.Text,
                Detail = x.FindElement(By.XPath(@"../td[@valign=""top""]")).Text
            }).ToDictionary(x => x.Description, x => x.Detail);
            return items;
        }
    }
}
