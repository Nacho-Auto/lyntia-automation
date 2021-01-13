using Lyntia.TestSet.Actions;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Lyntia.TestSet.Conditions
{
	public class CommonConditions
	{
        
        private static IWebDriver driver;
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;

        public CommonConditions()
        {
            driver = Utils.driver;
            ofertaCondition = Utils.getOfertaConditions();
            productoActions = Utils.getProductoActions();
            productoCondition = Utils.getProductoConditions();
            commonActions = Utils.getCommonActions();
            commonCondition = Utils.getCommonConditions();
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
        }

        public void AccedeGestionCliente()
        {
            Assert.AreEqual(true, driver.FindElement(By.LinkText("Gestión del Cliente")).Enabled);
            Assert.AreEqual("Gestión del Cliente", driver.FindElement(By.LinkText("Gestión del Cliente")).Text);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("AccedeGestionCliente.png");
        }

        public void AccedeOferta()
        {
            //Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@id, 'Todos_listItem')]")).Enabled);//el componente Todos esta activo
            //Assert.AreEqual("Todos", driver.FindElement(By.XPath("//li[contains(@id, 'Todos_listItem')]")).Text);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(" AccedeOferta.png");
        }
    }
}
