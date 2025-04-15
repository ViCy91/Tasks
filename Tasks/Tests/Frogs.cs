using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Frogs
{
    public class FrogTest
    {
        IWebDriver driver = new ChromeDriver();
        private FrogsTask frogsTask;

        [OneTimeSetUp]
        public void Setup()
        {
            frogsTask = new FrogsTask();//
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        [Test]
        public void FrogTestOne()
        {
            frogsTask.GoToPage(driver);
            frogsTask.CalculateFrogs(driver);
            frogsTask.ClickNewGame(driver);
            frogsTask.LogicOfFrogs(driver);
        }
    }
}
