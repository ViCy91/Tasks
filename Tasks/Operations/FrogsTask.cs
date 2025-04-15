using Frogs.Helpers;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Frogs
{
    class FrogsTask
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
        public (int, int, int) CalculateFrogs(IWebDriver driver)
        {
            // Find all td elements within the specified XPath
            var tdElements = driver.FindElements(Selectors.tdElementsXPath);

            // Filter td elements that contain an img with the specified src attribute
            var frog0Elements = tdElements.Where(td =>
                td.FindElements(Selectors.frog0XPath).Count > 0);

            var frog1Elements = tdElements.Where(td =>
                td.FindElements(Selectors.frog1XPath).Count > 0);

            var frog2Elements = tdElements.Where(td =>
                td.FindElements(Selectors.frog2XPath).Count > 0);

            // Count the number of such elements
            int frog0Count = frog0Elements.Count();
            int frog1Count = frog1Elements.Count();
            int frog2Count = frog2Elements.Count();

            return (frog0Count, frog1Count, frog2Count);

        }

        public void ClickNewGame(IWebDriver driver)
        {
            var (frog0Count, frog1Count, frog2Count) = CalculateFrogs(driver);

            while (frog1Count != 2 || frog2Count != 2)
            {
                // Find the button element with the specified XPath
                var buttonElement = driver.FindElement(Selectors.buttonElementXpath);
                // Perform an action on the button element, e.g., click it
                buttonElement.Click();
                // Keep the browser open for a while to see the result
                Thread.Sleep(2000);
                (frog0Count, frog1Count, frog2Count) = CalculateFrogs(driver);
            }
        }

        public void LogicOfFrogs(IWebDriver driver)
        {
            var (frog0Count, frog1Count, frog2Count) = CalculateFrogs(driver);

            // Initialize lists to store onmousedown values and positions
            List<(IWebElement element, int position)> frog0Elements = new List<(IWebElement, int)>();
            List<(IWebElement element, int position)> frog1Elements = new List<(IWebElement, int)>();
            List<(IWebElement element, int position)> frog2Elements = new List<(IWebElement, int)>();


            if (frog1Count == 2 && frog2Count == 2)
            {
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> tdElements;
                ExtractFrogPositions(driver, out frog0Elements, out frog1Elements, out frog2Elements, out tdElements);
                SortFrogs(ref frog0Elements, ref frog1Elements, ref frog2Elements);

                // Move frogs
                while (frog1Elements.Any(f => f.position < 4) || frog2Elements.Any(f => f.position > 4))
                {
                    var alert = false;
                    // Move frog1 to the right
                    foreach (var frog1 in frog1Elements)
                    {
                        var nextPosition = frog1.position + 1;
                        var jumpPosition = frog1.position + 2;

                        if (frog0Elements.Any(f => f.position == nextPosition))
                        {
                            // Move to the next position
                            frog1.element.Click();
                            Thread.Sleep(500);
                            alert = CheckForAlert(driver);
                            break;
                        }
                        else if (frog0Elements.Any(f => f.position == jumpPosition))
                        {
                            // Jump to the next position
                            frog1.element.Click();
                            Thread.Sleep(500);
                            alert = CheckForAlert(driver);
                            break;
                        }
                    }

                    if (alert) break;

                    // Move frog2 to the left
                    foreach (var frog2 in frog2Elements)
                    {
                        var nextPosition = frog2.position - 1;
                        var jumpPosition = frog2.position - 2;

                        if (frog0Elements.Any(f => f.position == nextPosition))
                        {
                            // Move to the next position
                            frog2.element.Click();
                            Thread.Sleep(500);
                            alert = CheckForAlert(driver);
                            break;
                        }
                        else if (frog0Elements.Any(f => f.position == jumpPosition))
                        {
                            // Jump to the next position
                            frog2.element.Click();
                            Thread.Sleep(500);
                            alert = CheckForAlert(driver);
                            break;
                        }
                    }

                    if (alert) break;

                    // Recalculate positions
                    (frog0Count, frog1Count, frog2Count) = CalculateFrogs(driver);
                    ExtractFrogPositions(driver, out frog0Elements, out frog1Elements, out frog2Elements, out tdElements);

                    // Sort elements by position
                    SortFrogs(ref frog0Elements, ref frog1Elements, ref frog2Elements);

                }
            }
            else
            {
                ClickNewGame(driver);
            }
        }

        private static void ExtractFrogPositions(IWebDriver driver, out List<(IWebElement element, int position)> frog0Elements, out List<(IWebElement element, int position)> frog1Elements, out List<(IWebElement element, int position)> frog2Elements, out System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> tdElements)
        {
            // Find all td elements within the specified XPath
            tdElements = driver.FindElements(Selectors.tdElementsXPath);

            // Extract positions from onmousedown attribute
            frog0Elements = tdElements
                .Where(td => td.FindElements(Selectors.frog0XPath).Count > 0)
                .Select(td => (td.FindElement(Selectors.frog0XPath), int.Parse(td.FindElement(Selectors.frog0XPath).GetAttribute("onmousedown").Split('(')[1].Split(')')[0])))
                .ToList();

            frog1Elements = tdElements
                .Where(td => td.FindElements(Selectors.frog1XPath).Count > 0)
                .Select(td => (td.FindElement(Selectors.frog1XPath), int.Parse(td.FindElement(Selectors.frog1XPath).GetAttribute("onmousedown").Split('(')[1].Split(')')[0])))
                .ToList();

            frog2Elements = tdElements
                .Where(td => td.FindElements(Selectors.frog2XPath).Count > 0)
                .Select(td => (td.FindElement(Selectors.frog2XPath), int.Parse(td.FindElement(Selectors.frog2XPath).GetAttribute("onmousedown").Split('(')[1].Split(')')[0])))
                .ToList();
        }

        private static void SortFrogs(ref List<(IWebElement element, int position)> frog0Elements, ref List<(IWebElement element, int position)> frog1Elements, ref List<(IWebElement element, int position)> frog2Elements)
        {
            // Sort elements by position
            frog0Elements = frog0Elements.OrderBy(f => f.position).ToList();
            // Sort frog1 in descending order
            frog1Elements = frog1Elements.OrderByDescending(f => f.position).ToList();
            // Sort frog2 in ascending order
            frog2Elements = frog2Elements.OrderBy(f => f.position).ToList();
        }

        private static bool CheckForAlert(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
    }
}
