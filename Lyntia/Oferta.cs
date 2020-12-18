using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;
using static Lyntia.OfertaActions;

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

        [TestInitialize]
        public void Instanciador()
        {
            driver = utils.Instanciador();
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
        public void CRM_COF0001_accesoOferta()
        {
            // Paso 1
            actions.AccesoGestionCliente(driver);//Acceso al modulo de Gestion de Cliente(Apliaciones)
            condition.AccedeGestionCliente(driver);//Acceso correcto

            //Paso 2
            actions.AccesoOferta(driver);//seleccionamos una oferta de la lista
            condition.AccedeOferta(driver);//comprobamos el acceso
        }

        //CRM-EOF0003
        [TestMethod]
        public void CRM_EOF0003_Editar_Oferta()
        {
            // Paso 1
            actions.AccesoGestionCliente(driver);//Acceso al modulo de Gestion de Cliente(Apliaciones)
            condition.AccedeGestionCliente(driver);//Acceso correcto

            //Paso 2
            actions.AccesoOferta(driver);//Oferta menu
            condition.AccedeOferta(driver);//comprobamos el acceso

            //Paso 3
            actions.SeleccionOferta(driver);//hacemos click en una oferta del listado
            condition.AccederSeleccionOferta(driver);//accede a la oferta

            //Paso 4
            actions.IntroduccirDatos(driver);//introduccir campos de la oferta
            condition.IntroduccionDatos(driver);//los datos se introduccen correctamente

        }

    }

    public class OfertaActions
    {

        public void AccesoGestionCliente(IWebDriver driver)
        {
            // Acceso al modulo Gestion del cliente(Aplicaciones)
            Thread.Sleep(10000);
            driver.SwitchTo().Frame(driver.FindElement(By.Id("AppLandingPage"))); // Cambiar al frame de Apps
            driver.FindElement(By.XPath("//a[contains(@aria-label, 'Cliente')]")).Click(); //modulo gestion de clientes
            Assert.AreEqual(driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div/div[1]/a/span")).Text, "Gestión del Cliente");

        }

        public void AccesoOfertasLyntia(IWebDriver driver)//Desplegable tipos de ofertas(mis ofertas lyntia, ofertas lyntia y AQS)
        {

            driver.FindElement(By.Id("sitemap-entity-oferta")).Click();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Mis Ofertas lyntia')]")).Click(); //Expandimos la opción de Mis Ofertas lyntia

            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//Opción no recomentada con cursores del teclado
            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();

            //driver.FindElements(By.XPath("//*[starts-with(@id, 'ViewSelector') and contains(@id, 'list')]"))[3].Click();//Opción escalable
            driver.FindElement(By.XPath("//*[contains(@title, 'Ofertas lyntia')")).Click();//Opción escalable
        }

        public void SeleccionOferta(IWebDriver driver)//Seleccion de una oferta del listado
        {
            driver.FindElement(By.Id("sitemap-entity-oferta")).Click();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Mis Ofertas lyntia')]")).Click(); //Expandimos la opción de Mis Ofertas lyntia
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//Opción no recomentada con cursores del teclado
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
            driver.FindElement(By.XPath("//div[contains(@title, 'aaa')]")).Click();//click en la oferta

        }

        public void CrearOferta(IWebDriver driver)
        {

            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateLauncher')]")).Click();
            Thread.Sleep(2000);
            driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[4].Click();
            driver.FindElement(By.XPath("//*[@id='quickCreateSaveAndNewBtn']")).Click();
            //driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[0].Click();

        }

        public void AccesoOferta(IWebDriver driver)//menu de ofertas
        {
            // Click en Ofertas, barra izquierda del CRM
            driver.FindElement(By.XPath("//li[@title='Ofertas']")).Click();
            Thread.Sleep(10000);

        }

        public void IntroduccirDatos(IWebDriver driver)//Cumplimentar datos de la oferta(campos contacto, fecha...)
        {
            // campos de la oferta
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Buscar registros para el campo Contacto de oferta, Búsqueda')]")).Click();//contacto oferta
            Thread.Sleep(2000);
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//selecciona el primer cliente de la lista
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Nombre oferta')]")).SendKeys("Prueba_auto_NO_borrar_MODIFICADA");
            driver.FindElement(By.XPath("//a[contains(@aria-label, 'Es permuta: No')]")).Click();//Toggle Switch
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Fecha de Fecha de presentación')]")).Click();//Calendario
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'diciembre 16, 2020')]")).Click();//seleccionamos fecha del calendario
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Código GVAL')]")).SendKeys("prue123456");//Escribimos prueba en codigo gval
            driver.FindElement(By.XPath("//textarea[contains(@aria-label, 'Descripción')]")).SendKeys("Prueba campo descripcion");//Escribimos prueba en detalle de oferta
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar

            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Fecha de Fecha de presentación')]")).Click();//Calendario
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'diciembre 14, 2020')]")).Click();//seleccionamos fecha del calendario
            driver.FindElement(By.XPath("//li[contains(@title, 'Fechas')]")).Click();//Pestaña fechas
            
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();//Guarda y cierra






        }



        // Para continuar trabajando
        public class OfertaConditions
        {

            public void AccedeGestionCliente(IWebDriver driver)
            {
                Assert.AreEqual(true, driver.FindElement(By.LinkText("Gestión del Cliente")).Enabled);
                Assert.AreEqual("Gestión del Cliente", driver.FindElement(By.LinkText("Gestión del Cliente")).Text);
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("AccedeGestionCliente.png");
            }


            public void AccedeOferta(IWebDriver driver)
            {
                Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@id, 'Todos_listItem')]")).Enabled);//el componente Todos esta activo
                Assert.AreEqual("Todos", driver.FindElement(By.XPath("//li[contains(@id, 'Todos_listItem')]")).Text);
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(" AccedeOferta.png");
            }

            public void AccederSeleccionOferta(IWebDriver driver)
            {
                Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Enabled);//la pestaña general esta activa
                Assert.AreEqual("General", driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Text);
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("AccederSeleccionOferta.png");
            }

            public void IntroduccionDatos(IWebDriver driver)//Comprobamos que todos los campos se introducen correctamente
            {
                Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Enabled);
               
                String Modulocliente = driver.FindElement(By.LinkText("aaaPrueba_auto_NO_borrar_MODIFICADA")).Text;
                Console.WriteLine(Modulocliente);
            }





        }
    }
}