using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using UnitTestProject1;

namespace CrearOfertaLyntia {
    [TestClass]
    public class Oferta {

        IWebDriver driver;

        OfertaActions actions = new OfertaActions();
        OfertaConditions condition = new OfertaConditions();
        ClienteActions clientActions = new ClienteActions();
        Utilidades utils = new Utilidades();

        [TestInitialize]
        public void instanciador() {

            driver = new ChromeDriver("C:\\chromedriver");
            driver.Navigate().GoToUrl("https://ufinetprep2.crm4.dynamics.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            // Realizar login
            utils.login(driver);
        }

        [TestCleanup]
        public void cierre() {
            driver.Quit();
        }

        //CRM-COF0001
        [TestMethod]
        public void accesoOfertas()
        {
            // Paso 1
            actions.accesoGestionCliente(driver);
            condition.accedeGestionCliente(driver);

            // Comprobar si existe cliente o crear
            clientActions.creacionRapidaCliente(driver);

        }

        [TestMethod]
        public void creandoOferta()
        {
            // Paso 1
            actions.accesoGestionCliente(driver);
            condition.accedeGestionCliente(driver);
            // Paso 2
            actions.crearOferta(driver);
            condition.creaOferta(driver);
           
        }
    }

    public class Navegacion
    {
        public void login(IWebDriver driver)
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

    

    public class OfertaActions
    {
       
        public void accesoGestionCliente(IWebDriver driver)
        {
            // Acceso al modulo Gestion del cliente
            Thread.Sleep(10000);
            driver.SwitchTo().Frame(driver.FindElement(By.Id("AppLandingPage"))); // Cambiar al frame de Apps
            driver.FindElement(By.XPath("//a[contains(@aria-label, 'Cliente')]")).Click(); //modulo gestion de clientes
            Assert.AreEqual(driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div/div[1]/a/span")).Text, "Gestión del Cliente");

        }

        public void accesoOfertasLyntia(IWebDriver driver)
        {

            driver.FindElement(By.Id("sitemap-entity-oferta")).Click();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Mis Ofertas lyntia')]")).Click(); //Expandimos la opción de Mis Ofertas lyntia

            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//Opción no recomentada con cursores del teclado
            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();

            //driver.FindElements(By.XPath("//*[starts-with(@id, 'ViewSelector') and contains(@id, 'list')]"))[3].Click();//Opción escalable
            driver.FindElement(By.XPath("//*[contains(@title, 'Ofertas lyntia')")).Click();//Opción escalable
        }

        public void crearOferta(IWebDriver driver)
        {

            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateLauncher')]")).Click();
            Thread.Sleep(2000);
            driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[4].Click();
            driver.FindElement(By.XPath("//*[@id='quickCreateSaveAndNewBtn']")).Click();
            //driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[0].Click();

        }

        public void accesoOferta(IWebDriver driver)
        {
            // Click en Ofertas, barra izquierda del CRM
            driver.FindElement(By.XPath("//li[@title='Ofertas']")).Click();
            Thread.Sleep(10000);

        }
    }

        // Para continuar trabajando
        public class OfertaConditions
        {

            public void accedeGestionCliente(IWebDriver driver)
            {

            }

            public void creaOferta(IWebDriver driver)
            {

            }

        }
}