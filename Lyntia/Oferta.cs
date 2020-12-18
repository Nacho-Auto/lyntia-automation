using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace Lyntia
{

    [TestClass]
    public class Oferta 
    {

        IWebDriver driver;

        OfertaActions actions = new OfertaActions();
        OfertaConditions condition = new OfertaConditions();
        ProductoAction productoActions = new ProductoAction();
        Utils utils = new Utils();
        Navegacion navegacion = new Navegacion();
        GridUtils grid = new GridUtils();
        IJavaScriptExecutor js;

        [TestInitialize]
        public void Instanciador()
        {
            // Instanciador del driver
            driver = utils.Instanciador();

            // JavaScriptExecutor, usado por ejemplo para hacer scrolling a los elementos
            js = (IJavaScriptExecutor)driver;

            // Realizar login
            navegacion.Login(driver);
        }

        [TestCleanup]
        public void Cierre() 
        {
            driver.Quit();
        }

        //CRM-COF0001
        [TestMethod]
        public void CRM_COF0001_accesoOfertas()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia(driver);

        }

        //CRM-COF0002
        [TestMethod]
        public void CRM_COF0002_consultaOferta()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia(driver);

            // Paso 2A - Comprobar si hay alguna Oferta para abrir
            IWebElement element = null;
            if(utils.EncontrarElemento(By.XPath("//div[@title='No hay datos disponibles.']"), out element, driver))
            {
                // Paso 2AA - Crear Oferta Nueva
                actions.AccesoNuevaOferta(driver);
            }
            else
            {
                // Paso 2AB - Abrir Oferta existente 
                //div[@data-id='cell-1-4']
                actions.abrirOferta(grid, driver);
            }
 
        }

        //CRM-COF0003
        [TestMethod]
        public void CRM_COF0003_creacionOferta()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia(driver);

            // Paso 2 - Crear Nueva Oferta
            actions.AccesoNuevaOferta(driver);
            condition.CreaOferta(driver, navegacion);

            // Paso 3 - Cambiar a la pestaña Fechas
            actions.AccesoFechasOferta(driver);
            condition.FechasSinInformar(driver);

        }

        //[TestMethod]
        public void CreandoOferta()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // TODO: Cambiar el estilo de creación de oferta
            actions.CrearOferta(driver);
            condition.CreaOferta(driver, navegacion);
           
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
        }

        public void AccesoOfertasLyntia(IWebDriver driver)
        {

            driver.FindElement(By.Id("sitemap-entity-oferta")).Click();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Mis Ofertas lyntia')]")).Click(); //Expandimos la opción de Mis Ofertas lyntia

            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//Opción no recomentada con cursores del teclado
            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();

            //driver.FindElements(By.XPath("//*[starts-with(@id, 'ViewSelector') and contains(@id, 'list')]"))[3].Click();//Opción escalable
            driver.FindElement(By.XPath("//span[contains(text(), 'Ofertas lyntia')]")).Click();//Opción escalable
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

        public void AccesoNuevaOferta(IWebDriver driver)
        {
            // Click en "+ Nuevo", barra de herramientas
            driver.FindElement(By.XPath("//button[@aria-label='Nuevo']")).Click();
            Thread.Sleep(3000);
        }

        public void abrirOferta(GridUtils grid, IWebDriver driver)
        {
            // Cantidad de Ofertas
            int numeroOfertas = grid.NumeroRegistrosEnGrid(By.XPath("//div[@wj-part='cells']"), driver);

            // TODO: Método para clickar en celda de grid          
        }

        public void AccesoFechasOferta(IWebDriver driver)
        {
            // Click en pestaña Fechas
            driver.FindElement(By.XPath("//li[@title='Fechas']")).Click();
            Thread.Sleep(6000);

        }
    }

    public class OfertaConditions{

        public void AccedeGestionCliente(IWebDriver driver)
        {
            // TODO: Asserts de Acceso 
            Assert.AreEqual(driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div/div[1]/a/span")).Text, "Gestión del Cliente");
        }

        public void CreaOferta(IWebDriver driver, Navegacion navegacion)
        {
            // Assert de título "Nuevo Oferta" del formulario
            Assert.AreEqual("Nuevo Oferta", driver.FindElement(By.XPath("//h1[@data-id='header_title']")).Text);

            // Assert de tab por defecto "General"
            Assert.IsTrue(driver.FindElement(By.XPath("//li[@aria-label='General']")).GetAttribute("aria-selected").Equals("true"));

            // Assert de Razón para el estado de la Oferta "En elaboración" 
            Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//section[@id='quote information']//span[@aria-label='Razón para el estado']//span")).Text);

            // TODO : SCROLL A ELEMENTOS NO VISIBLES INICIALMENTE (posiblemente scroll periodico)
            // Assert de Tipo de Oferta por defecto "Nuevo Servicio"
            //Assert.AreEqual(driver.FindElement(By.XPath("//select[contains(@aria-label,'Tipo de oferta')]")).GetAttribute("title"), "Nuevo servicio");

            // Assert de Divisa
            //Assert.AreEqual(driver.FindElement(By.XPath("//span[contains(@title,'Divisa')]")).Text, "Divisa");
        }

        public void FechasSinInformar(IWebDriver driver)
        {
            // Assert de Fecha de creación vacía
            Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@data-id,'createdon')]")).Text.Equals(""));

            // Assert de Hora de creación vacía
            Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@aria-label,'Time of Fecha de creación')]")).Text.Equals(""));

            // Assert de Fecha de modificación vacía
            Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@data-id,'modifiedon')]")).Text.Equals(""));

            // Assert de Hora de modificación vacía
            Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@aria-label,'Time of Fecha de modificación')]")).Text.Equals(""));

        }
    }
}