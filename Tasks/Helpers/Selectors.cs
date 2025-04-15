using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogs.Helpers
{
    public static class Selectors
    {
        public static string link = "https://www.lutanho.net/play/frogs.html";

        public static readonly By tdElementsXPath = By.XPath("/html/body/div/table/tbody/tr/td/table/tbody/tr/td");
        public static readonly By frog0XPath = By.XPath(".//img[@src='https://www.lutanho.net/play/frog0.gif']");
        public static readonly By frog1XPath = By.XPath(".//img[@src='https://www.lutanho.net/play/frog1.gif']");
        public static readonly By frog2XPath = By.XPath(".//img[@src='https://www.lutanho.net/play/frog2.gif']");
        public static readonly By buttonElementXpath = By.XPath("//input[@value='NEW GAME']");
    }
}
