using Calculator.Operations;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Calculator
{
    public class CalculatorTest
    {
        IWebDriver driver = new ChromeDriver();
        private CalculatorTask calculatorTask;

        [OneTimeSetUp]
        public void Setup()
        {
            calculatorTask = new CalculatorTask();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            //driver.Quit();
        }

        [Test]
        public void CalculatorOne()
        {
            calculatorTask.GoToPage(driver);
            calculatorTask.CalculateAll(driver);
        }
    }
}
