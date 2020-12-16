using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    class Cliente
    {

    }

    public class ClienteActions
    {
        public void creacionRapidaCliente(IWebDriver driver)
        {
            // Click en "+"
            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateLauncher')]")).Click();
            Thread.Sleep(4000);
            // Click en Cliente
            driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[1].Click();
            Thread.Sleep(6000);

            // Introduccion de campos de cliente
            driver.FindElement(By.XPath("//input[contains(@data-id, 'nombrecomercial')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@data-id, 'nombrecomercial')]")).SendKeys("TEST");

            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[contains(@data-id, 'nif')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@data-id, 'nif')]")).SendKeys("35127689L");

            Thread.Sleep(1000); Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Razón social')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Razón social')]")).SendKeys("RAZON");
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Razón social')]")).SendKeys(Keys.Tab);

            // Tipo de cliente
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'cliente')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'cliente')]")).SendKeys("grupo");
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'cliente')]")).SendKeys(Keys.Tab);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'cliente')]")).SendKeys(Keys.Tab);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'cliente')]")).SendKeys(Keys.Return);
            Thread.Sleep(6000);

            //driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[0].Click();
            // 35127689L
        }

        public void borradoCliente(IWebDriver driver)
        {

        }
    }
}
