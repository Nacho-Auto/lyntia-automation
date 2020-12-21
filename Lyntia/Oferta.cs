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
        public void CRM_COF0001_accesoOferta()
        {


            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia(driver);


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

            // Paso - Reestabñecer datos
            actions.Restablecimiento_CRM_COF0003(driver);
        }


        //CRM-EOF0004
        [TestMethod]
        public void CRM_EOF0004_Editar_Oferta()
        {
            // Paso 1
            actions.AccesoGestionCliente(driver);//Acceso al modulo de Gestion de Cliente(Apliaciones)
            condition.AccedeGestionCliente(driver);//Acceso correcto

            //Paso 2
            actions.AccesoOferta(driver);//Oferta menu
            condition.AccedeOferta(driver);//comprobamos el acceso

            //Paso 3
            actions.Tipo_de_oferta_Cambiodecapacidad(driver);
            condition.Aviso_cambiocapacidad(driver);

            //Paso 4
            actions.Tipo_de_oferta_Cambiodeprecio(driver);
            condition.Aviso_Cambiodeprecio(driver);

            //Paso 5
            actions.Tipo_de_oferta_Cambiodesolucion(driver);
            condition.Aviso_Cambiodesolucion(driver);

            //Paso 6
            actions.Tipo_de_oferta_Cambiodedireccion(driver);
            condition.Aviso_Cambiodedireccion(driver);

            


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
            if (utils.EncontrarElemento(By.XPath("//div[@title='No hay datos disponibles.']"), out element, driver))
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
            // Acceso al modulo Gestion del cliente(Aplicaciones)
            Thread.Sleep(10000);
            driver.SwitchTo().Frame(driver.FindElement(By.Id("AppLandingPage"))); // Cambiar al frame de Apps
            driver.FindElement(By.XPath("//a[contains(@aria-label, 'Cliente')]")).Click(); //modulo gestion de clientes
        }

        public void AccesoOfertasLyntia(IWebDriver driver)//Desplegable tipos de ofertas(mis ofertas lyntia, ofertas lyntia y AQS)
        {

            driver.FindElement(By.Id("sitemap-entity-oferta")).Click();
            //driver.FindElement(By.XPath("//span[contains(//span[contains(@aria-label, 'Mis Ofertas lyntia')]")).Click(); //Expandimos la opción de Mis Ofertas lyntia

            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//Opción no recomentada con cursores del teclado
            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();

            //driver.FindElements(By.XPath("//*[starts-with(@id, 'ViewSelector') and contains(@id, 'list')]"))[3].Click();//Opción escalable
            //driver.FindElement(By.XPath("//span[contains(text(), 'Ofertas lyntia')]")).Click();//Opción escalable
        }

        public void SeleccionOferta(IWebDriver driver)//Seleccion de una oferta del listado
        {
            driver.FindElement(By.Id("sitemap-entity-oferta")).Click();
            //driver.FindElement(By.XPath("//span[contains(@aria-label, 'Ofertas lyntia')]")).Click(); //Expandimos la opción de Mis Ofertas lyntia
            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowUp).Perform();//Opción no recomentada con cursores del teclado
            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
            //Thread.Sleep(2000);
            //driver.FindElement(By.LinkText("CLIENTE INTEGRACION")).Click();//click en la oferta
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[contains(@title, 'Automatica_MOD')]")).Click();//click en la oferta

        }

        public void CrearOferta(IWebDriver driver)
        {

            driver.FindElement(By.XPath("//utton[contains(@data-id, 'quickCreateLauncher')]")).Click();
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
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("IntroduccirDatosOrinales.png");
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Buscar registros para el campo Contacto de oferta, Búsqueda')]")).Click();//contacto oferta
            Thread.Sleep(2000);
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//selecciona el primer cliente de la lista
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Nombre oferta')]")).SendKeys("Prueba_auto_NO_borrar_MODIFICADA");
            driver.FindElement(By.XPath("//a[contains(@aria-label, 'Es permuta: No')]")).Click();//Toggle Switch
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Fecha de Fecha de presentación')]")).Click();//Calendario
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'diciembre 16, 2020')]")).Click();//seleccionamos fecha del calendario
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Código GVAL')]")).SendKeys("prue123456");//Escribimos prue123456 en codigo gval
            driver.FindElement(By.XPath("//textarea[contains(@aria-label, 'Descripción')]")).SendKeys("Prueba campo descripcion");//Escribimos Prueba campo descripcion en detalle de oferta


            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar

            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Fecha de Fecha de presentación')]")).Click();//Calendario
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'diciembre 14, 2020')]")).Click();//seleccionamos fecha del calendario
            driver.FindElement(By.XPath("//li[contains(@title, 'Fechas')]")).Click();//Pestaña fechas
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("IntroduccirDatos.png");
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();//Guarda y cierra
            Thread.Sleep(2000);
        }    
            
       

        public void Tipo_de_oferta_Cambiodecapacidad (IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Prueba-Auto_NO_borrarCRM-EOF0004")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Nuevo servicio')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//selecciona la oppcion inferior
        }
        public void Tipo_de_oferta_Cambiodeprecio (IWebDriver driver)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de capacidad (Upgrade/Downgrade)')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la oppcion inferior
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }
        public void Tipo_de_oferta_Cambiodesolucion (IWebDriver driver)
        {
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de precio/Renovación')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la oppcion inferior
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }
        public void Tipo_de_oferta_Cambiodedireccion (IWebDriver driver)
        {

            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de solución técnica (Tecnología)')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la oppcion inferior
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }


        public void Restablecimiento_CRM_COF0003(IWebDriver driver)//reestablecemos datos
        {
            driver.FindElement(By.LinkText("Automatica_MODPrueba_auto_NO_borrar_MODIFICADA")).Click();
            driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).Click();
            driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).SendKeys(Keys.Delete);
            
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Nombre oferta')]")).SendKeys("Automatica_MOD");
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Fecha de Fecha de presentación')]")).Click();
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'diciembre 10, 2020')]")).Click();
            driver.FindElement(By.XPath("//input[@aria-label='Código GVAL']")).Click();
            driver.FindElement(By.XPath("//input[@aria-label='Código GVAL']")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.XPath("//input[@aria-label='Código GVAL']")).SendKeys(Keys.Delete);
            driver.FindElement(By.XPath("//textarea[contains(@aria-label, 'Descripción')]")).Click();
            driver.FindElement(By.XPath("//textarea[contains(@aria-label, 'Descripción')]")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.XPath("//textarea[contains(@aria-label, 'Descripción')]")).SendKeys(Keys.Delete);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Código GVAL')]")).Clear();
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
            driver.FindElement(By.XPath("/html/body/div[2]/div/div[4]/div[2]/div/div/div/div/div/div[1]/div[1]/div[2]/div/div/div/section[1]/section[1]/div/div/div/div[9]/div/div/div[2]/div/div[3]/div[2]/div/div[2]/div[1]/div[2]/div/div[3]/div/div")).Click();//Toggle Switch
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar


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

            String OfertaMOD = driver.FindElement(By.LinkText("Automatica_MODPrueba_auto_NO_borrar_MODIFICADA")).Text;//muestra por consola la nueva oferta modificada
            Console.WriteLine(OfertaMOD);


        }

        public void IntroduccionDatos2(IWebDriver driver)//comprobamos todas las modificaciones realizadas
        {

            Assert.AreEqual("La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            Assert.AreEqual("La oferta de tipo “Cambio de precio” no requiere envío a construcción ni cambiar el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            Assert.AreEqual("La oferta de tipo “Cambio de tecnología” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            Assert.AreEqual(" La oferta de tipo “Migración” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
           
        }

        public void Aviso_cambiocapacidad (IWebDriver driver)//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodeprecio (IWebDriver driver)//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de precio” no requiere envío a construcción ni cambiar el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodesolucion (IWebDriver driver)//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de tecnología” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodedireccion (IWebDriver driver)//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Migración” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();//Guarda y cierra

            //reestablece datos CRM-EOF0004

            driver.FindElement(By.LinkText("Prueba-Auto_NO_borrarCRM-EOF0004")).Click();
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de dirección (Migración)')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowUp).Perform();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
            
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de solución técnica (Tecnología)')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowUp).Perform();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
            
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de precio/Renovación')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowUp).Perform();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
            
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de capacidad (Upgrade/Downgrade)')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowUp).Perform();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
            Thread.Sleep(7000);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();










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
