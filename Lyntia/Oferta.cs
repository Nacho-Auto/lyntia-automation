using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Lyntia;

namespace Lyntia
{

    [TestClass]
    public class Oferta {

        IWebDriver driver;

        OfertaActions actions = new OfertaActions();
        OfertaConditions condition = new OfertaConditions();
        ProductoAction productoActions = new ProductoAction();
        Utils utils = new Utils();
        Navegacion navegacion = new Navegacion();

        [TestInitialize]
        public void Instanciador()
        {
            driver = utils.Instanciador();
            // Realizar login
            navegacion.Login(driver);
        }

        [TestCleanup]
        public void Cierre() {
            driver.Quit();
        }

        //CRM-COF0001
        [TestMethod]
        public void CRM_COF0001_accesoOfertas()
        {
            // Paso 1
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Comprobar si existe cliente o crear
            productoActions.CreacionRapidaCliente(driver);

        }

        [TestMethod]
        public void CreandoOferta()
        {
            // Paso 1
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);
            // Paso 2
            actions.CrearOferta(driver);
            condition.CreaOferta(driver);
           
        }

    }

    public class OfertaActions
    {
       
        public void AccesoGestionCliente(IWebDriver driver)
        {
            // Acceso al modulo Gestion del cliente
            Thread.Sleep(10000);
            driver.SwitchTo().Frame(driver.FindElement(By.Id("AppLandingPage"))); // Cambiar al frame de Apps
            driver.FindElement(By.XPath("//a[contains(@aria-label, 'Cliente')]")).Click(); //modulo gestion de clientes
            Assert.AreEqual(driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div/div[1]/a/span")).Text, "Gestión del Cliente");

        }

        public void AccesoOfertasLyntia(IWebDriver driver)
        {

            driver.FindElement(By.Id("sitemap-entity-oferta")).Click();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Mis Ofertas lyntia')]")).Click(); //Expandimos la opción de Mis Ofertas lyntia

            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//Opción no recomentada con cursores del teclado
            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();

            //driver.FindElements(By.XPath("//*[starts-with(@id, 'ViewSelector') and contains(@id, 'list')]"))[3].Click();//Opción escalable
            driver.FindElement(By.XPath("//*[contains(@title, 'Ofertas lyntia')")).Click();//Opción escalable
        }

        public void CrearOferta(IWebDriver driver)
        {

            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateLauncher')]")).Click();
            Thread.Sleep(2000);
            driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[4].Click();
            driver.FindElement(By.XPath("//*[@id='quickCreateSaveAndNewBtn']")).Click();
            //driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[0].Click();

        }

        public void AccesoOferta(IWebDriver driver)
        {
            // Click en Ofertas, barra izquierda del CRM
            driver.FindElement(By.XPath("//li[@title='Ofertas']")).Click();
            Thread.Sleep(10000);

        }

    }

        // Para continuar trabajando
        public class OfertaConditions
        {

            public void AccedeGestionCliente(IWebDriver driver)
            {

            }

            public void CreaOferta(IWebDriver driver)
            {

            }

        }
}