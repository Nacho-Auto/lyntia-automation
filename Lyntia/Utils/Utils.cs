using System;
using Lyntia.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Lyntia
{

    public class Utils
    {
        IWebDriver driver;
        Lyntia.Utils.ObjectRepositoryUtils objRep;
        public IWebDriver Instanciador()
        {
            Lyntia.Utils.ObjectRepositoryUtils objRep = new Lyntia.Utils.ObjectRepositoryUtils();
            IWebDriver driver = new ChromeDriver(@"C:\chromedriver");
            driver.Navigate().GoToUrl("https://ufinetprep2.crm4.dynamics.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return driver;
        }

        public bool EncontrarElemento(By by, out IWebElement element, IWebDriver driver)
        {
            try
            {
                element = driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                element = null;
                return false;
            }
            return true;
        }
    }

    public class Navegacion
    {
        public void Login(IWebDriver driver)
        {
            // Login (acceso a PRE lyntia) 365 dinamic
            driver.FindElement(By.Id("i0116")).SendKeys("rgomezs.ext@lyntia.com"); //usuario de lyntia
            driver.FindElement(By.Id("idSIButton9")).Click();
            driver.FindElement(By.Id("userNameInput")).Clear();
            driver.FindElement(By.Id("userNameInput")).SendKeys("rgomezs@lyntia.com"); //usuario de entorno lyntia
            driver.FindElement(By.Id("passwordInput")).SendKeys("W1nter20$"); //pass de entorno lyntia
            driver.FindElement(By.Id("submitButton")).Click();
            driver.FindElement(By.Id("idBtn_Back")).Click(); //Desea mantener la sesion iniciada NO

        }

        public void ScrollHaciaElemento(IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("scrollBy(0, 2500)");
        }
    }

    public class GridUtils
    {
        public int NumeroRegistrosEnGrid(By by, IWebDriver driver)
        {       
            return Int16.Parse(driver.FindElement(by).GetAttribute("data-row-count"));
        }
    }
}
