using Lyntia.TestSet.Actions;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Lyntia.TestSet.Conditions
{
	public class CommonConditions
	{

        private static IWebDriver driver = Utils.getDriver();
        private static OfertaActions ofertaActions = Utils.getOfertaActions();
        private static OfertaConditions ofertaCondition = Utils.getOfertaConditions();
        private static ProductoActions productoActions = Utils.getProductoActions();
        private static ProductoConditions productoCondition = Utils.getProductoConditions();
        private static CommonActions commonActions = Utils.getCommonActions();
        private static CommonConditions commonCondition = Utils.getCommonConditions();

        public void AccedeGestionCliente()
        {
            Assert.AreEqual(true, driver.FindElement(By.LinkText("Gestión del Cliente")).Enabled);
            Assert.AreEqual("Gestión del Cliente", driver.FindElement(By.LinkText("Gestión del Cliente")).Text);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("AccedeGestionCliente.png");
        }

        public void AccedeOferta()
        {
            Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@id, 'Todos_listItem')]")).Enabled);//el componente Todos esta activo
            Assert.AreEqual("Todos", driver.FindElement(By.XPath("//li[contains(@id, 'Todos_listItem')]")).Text);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(" AccedeOferta.png");
        }
    }
}
