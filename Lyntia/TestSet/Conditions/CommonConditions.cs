using Lyntia.TestSet.Actions;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Lyntia.TestSet.Conditions
{
	public class CommonConditions
	{
        
        private static IWebDriver driver;
        private static WebDriverWait wait;

        public CommonConditions()
        {
            driver = Utils.driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        }

        public void AccedeGestionCliente()
        {
            Assert.AreEqual(true, driver.FindElement(By.LinkText("Gestión del Cliente")).Enabled);
            Assert.AreEqual("Gestión del Cliente", driver.FindElement(By.LinkText("Gestión del Cliente")).Text);
        }

        public void AccedeOferta()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[text() = 'Ofertas lyntia']")));
            Assert.AreEqual("Ofertas lyntia", driver.FindElement(By.XPath("//span[text() = 'Ofertas lyntia']")).Text);
            /*wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//li[contains(@id, 'Todos_listItem')]")));
            Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@id, 'Todos_listItem')]")).Enabled);//el componente Todos esta activo
            Assert.AreEqual("Todos", driver.FindElement(By.XPath("//li[contains(@id, 'Todos_listItem')]")).Text);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(" AccedeOferta.png");*/
        }
    }
}
