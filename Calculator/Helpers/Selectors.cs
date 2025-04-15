using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Helpers
{
    public static class Selectors
    {
        public static string link = "https://www.mathster.com/10secondsmaths/";

        public static readonly By QuestionNumber = By.Id("question");
        public static readonly By AnswerField = By.Id("question-answer");
    }
}
