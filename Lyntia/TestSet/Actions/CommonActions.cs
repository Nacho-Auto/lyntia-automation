using System;
using System.Threading;
using Lyntia.TestSet.Conditions;
using Lyntia.Utilities;
using OpenQA.Selenium;

namespace Lyntia.TestSet.Actions
{
	public class CommonActions
	{

        private static IWebDriver driver;
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;
        private static ObjectRepositoryUtils objRep = Utils.objRep;
        private static TestDataUtils dataRep = Utils.dataRep;

        public CommonActions()
        {
            driver = Utils.driver;
            ofertaCondition = Utils.getOfertaConditions();
            productoActions = Utils.getProductoActions();
            productoCondition = Utils.getProductoConditions();
            commonActions = Utils.getCommonActions();
            commonCondition = Utils.getCommonConditions();
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
        }

        public void AccesoGestionCliente()
        {
            // Acceso al modulo Gestion del cliente
            Thread.Sleep(10000);
            driver.SwitchTo().Frame(driver.FindElement(By.Id("AppLandingPage"))); // Cambiar al frame de Apps
            driver.FindElement(By.XPath("//a[contains(@aria-label, 'Cliente')]")).Click(); //modulo gestion de clientes
        }

        public void AccesoOferta()
        {
            // Click en Ofertas, barra izquierda del CRM
            driver.FindElement(By.XPath("//li[@title='Ofertas']")).Click();
            Thread.Sleep(10000);

        }

        public void Login()
        {
            // Login (acceso a PRE lyntia) 365 dinamic
            Utils.searchWebElement("Login.firstInput").SendKeys("rgomezs.ext@lyntia.com"); //usuario de lyntia
            Utils.searchWebElement("Login.firstSubmitButton").Click();
            Utils.searchWebElement("Login.secondInput").Clear();
            Utils.searchWebElement("Login.secondInput").SendKeys("rgomezs@lyntia.com"); //usuario de entorno lyntia
            Utils.searchWebElement("Login.thirdInput").SendKeys("W1nter20$"); //pass de entorno lyntia
            Utils.searchWebElement("Login.secondSubmitButton").Click();
            Utils.searchWebElement("Login.notPersistanceButton").Click(); //Desea mantener la sesion iniciada NO

        }
    }
}
