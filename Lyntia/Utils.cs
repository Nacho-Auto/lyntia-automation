using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Lyntia
{

    class Utils
    {

        IWebDriver driver;

        public IWebDriver Instanciador()
        {
            driver = new ChromeDriver ("C:/Chromedriver");
            driver.Navigate().GoToUrl("https://ufinetprep2.crm4.dynamics.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return driver;
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

    }

}
