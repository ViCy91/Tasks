using Calculator.Helpers;
using OpenQA.Selenium;
using System;
using System.Data;
using System.Threading;

namespace Calculator.Operations
{
    class CalculatorTask
    {
        public void GoToPage(IWebDriver driver)
        {
            // Navigate to the specified URL
            driver.Navigate().GoToUrl(Selectors.link);

            // Maximize the browser window
            driver.Manage().Window.Maximize();

            // Keep the browser open for a while to see the result
            Thread.Sleep(2000);
        }

        public double Calculate(IWebDriver driver)
        {
            var expression = driver.FindElement(Selectors.QuestionNumber).Text;
            // Replace 'x' with '*'
            expression = expression.Replace("×", "*");
            // Replace '÷' with '/'
            expression = expression.Replace("÷", "/");

            var dataTable = new DataTable();
            return Convert.ToDouble(dataTable.Compute(expression, string.Empty));
        }

        public void SendResult(IWebDriver driver, double result)
        {
            // Find the input element by its ID
            var inputElement = driver.FindElement(Selectors.AnswerField);
            // Clear the input field
            inputElement.Clear();
            // Send the result to the input field
            inputElement.SendKeys(result.ToString());
            //click enter
            inputElement.SendKeys(Keys.Enter);
        }
        public void CalculateAll(IWebDriver driver)
        {
            for (int i = 0; i < 50; i++)
            {
                // Calculate the result
                double result = Calculate(driver);
                // Send the result back to the input field
                SendResult(driver, result);
            }
        }
    }
}
