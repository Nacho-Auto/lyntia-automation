using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Lyntia
{

    [TestFixture]
    [AllureNUnit]
    [AllureSuite("OFERTA")]
    public class Oferta
    {

        IWebDriver driver;

        readonly OfertaActions actions = new OfertaActions();
        readonly OfertaConditions condition = new OfertaConditions();
        readonly ProductoAction productoActions = new ProductoAction();
        readonly Utils utils = new Utils();
        readonly Navegacion navegacion = new Navegacion();
        readonly GridUtils grid = new GridUtils();

        [SetUp]
        public void Instanciador()
        {
            // Instanciador del driver
            driver = utils.Instanciador();

            // Realizar login
            navegacion.Login(driver);
        }

        [TearDown]
        public void Cierre()
        {
            driver.Quit();
        }

        [Test(Description = "CRM-COF0001 Acceso a Ofertas")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0001_accesoOfertas()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

        }

        [Test(Description = "CRM-COF0002 Consultar Oferta")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0002_consultaOferta()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

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

        [Test(Description = "CRM-COF0003 Creación de Oferta")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0003_creacionOferta()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 2 - Crear Nueva Oferta
            actions.AccesoNuevaOferta(driver);
            condition.CreaOferta(driver, navegacion);

            // Paso 3 - Cambiar a la pestaña Fechas
            actions.AccesoFechasOferta(driver);
            condition.FechasSinInformar(driver);

        }

        [Test(Description = "CRM-COF0004 Creación de Oferta sin informar campos obligatorios")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0004_creacionOfertaSinCamposObligatorios()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 2 - Crear Nueva Oferta
            actions.AccesoNuevaOferta(driver);

            // Paso 3 - Guardar sin campos obligatorios
            actions.GuardarOferta(driver);
            condition.ofertaNoCreada(driver);

            // Paso 4 - Repetir el paso anterior pero pulsando Guardar y Cerrar
            actions.GuardarYCerrarOferta(driver);
            condition.ofertaNoCreada(driver);

            // Paso 7 - Guardar informando solo el Cliente
            actions.RellenarCamposOferta("", "CLIENTE INTEGRACION", "", "", driver); ;
            actions.GuardarOferta(driver);
            condition.ofertaNoCreada(driver);

            // Paso 8 - Guardar y cerrar informando solo el Cliente
            actions.GuardarYCerrarOferta(driver);
            condition.ofertaNoCreada(driver);

            driver.Navigate().Refresh();
            driver.SwitchTo().Alert().Accept();

            // Paso 5 - Guardar informando solo el Nombre
            actions.RellenarCamposOferta("TEST", "", "", "", driver); ;
            actions.GuardarOferta(driver);
            condition.ofertaNoCreada(driver);

            // Paso 6 - Guardar y cerrar informando solo el Nombre
            actions.GuardarYCerrarOferta(driver);
            condition.ofertaNoCreada(driver);

        }

        [Test(Description = "CRM-COF0005 Creación de Oferta de tipo Nuevo servicio")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0005_creacionOfertaNuevoServicio()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 2 - Crear Nueva Oferta
            actions.AccesoNuevaOferta(driver);

            // Paso 3 - Rellenar campos y click en Guardar
            actions.RellenarCamposOferta("CRM-COF0005", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA", driver);
            actions.GuardarOferta(driver);

            driver.Navigate().Refresh();

            condition.OfertaGuardadaCorrectamente("CRM-COF0005", "Nuevo servicio", driver);

            actions.AccesoFechasOferta(driver);
            condition.FechasInformadasCorrectamente(driver);

            actions.EliminarOfertaActual("Eliminar", driver);

            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            actions.AccesoNuevaOferta(driver);

            actions.RellenarCamposOferta("CRM-COF0005", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA", driver);
            actions.GuardarYCerrarOferta(driver);

            // Buscar Oferta creada
            actions.BuscarOfertaEnVista("CRM-COF0005", driver);
            condition.OfertaGuardadaCorrectamenteEnGrid(driver);

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            actions.AbrirOfertaEnVista("CRM-COF0005", driver);
            condition.OfertaGuardadaCorrectamente("CRM-COF0005", "Nuevo servicio", driver);

            actions.AccesoFechasOferta(driver);
            condition.FechasInformadasCorrectamente(driver);

            actions.EliminarOfertaActual("Eliminar", driver);

        }

        [Test(Description = "CRM-COF0006 Creación de Oferta de tipo Cambio de capacidad")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0006_creacionOfertaCambioCapacidad()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 2 - Crear Nueva Oferta
            actions.AccesoNuevaOferta(driver);

            // Paso 3 - Rellenar campos y click en Guardar
            actions.RellenarCamposOferta("CRM-COF0006", "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA", driver);
            actions.GuardarOferta(driver);

            driver.Navigate().Refresh();

            condition.OfertaGuardadaCorrectamente("CRM-COF0006", "Cambio de capacidad (Upgrade/Downgrade)", driver);

            actions.AccesoFechasOferta(driver);
            condition.FechasInformadasCorrectamente(driver);

            actions.EliminarOfertaActual("Eliminar", driver);

            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            actions.AccesoNuevaOferta(driver);

            actions.RellenarCamposOferta("CRM-COF0006", "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA", driver);
            actions.GuardarYCerrarOferta(driver);

            // Buscar Oferta creada
            actions.BuscarOfertaEnVista("CRM-COF0006", driver);
            condition.OfertaGuardadaCorrectamenteEnGrid(driver);

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            actions.AbrirOfertaEnVista("CRM-COF0006", driver);
            condition.OfertaGuardadaCorrectamente("CRM-COF0006", "Cambio de capacidad (Upgrade/Downgrade)", driver);

            actions.AccesoFechasOferta(driver);
            condition.FechasInformadasCorrectamente(driver);

            actions.EliminarOfertaActual("Eliminar", driver);

        }

        [Test(Description = "CRM-COF0005 Eliminar Oferta en borrador con producto añadido")]
        [AllureSubSuite("PRO ELIMINAR OFERTA")]
        public void CRM_COF0005_eliminarOfertaProductoAnadido()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            actions.AccesoNuevaOferta(driver);

            actions.RellenarCamposOferta("CRM-COF0005-ELIMINAR", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA", driver);
            actions.GuardarOferta(driver);

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps", driver);

            // Paso 3 y 4 - Pulsar Eliminar en la barra de herramientas y cancelar la eliminacion
            actions.EliminarOfertaActual("Cancelar", driver);

            // Volver al grid
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Buscar Oferta creada
            actions.BuscarOfertaEnVista("CRM-COF0005-ELIMINAR", driver);
            condition.OfertaGuardadaCorrectamenteEnGrid(driver);

            // Abrir la oferta anterior y comprobar datos cumplimentados
            actions.AbrirOfertaEnVista("CRM-COF0005-ELIMINAR", driver);

            // Paso 5 - Eliminar definitivamente
            actions.EliminarOfertaActual("Eliminar", driver);
        }

        [Test(Description = "CRM-COF0006 Eliminar Oferta en borrador con producto añadido desde el grid")]
        [AllureSubSuite("PRO ELIMINAR OFERTA")]
        public void CRM_COF0006_eliminarOfertaProductoAnadidoDesdeGrid()
        {
            // Login y Acceso a Gestión de Cliente
            actions.AccesoGestionCliente(driver);
            condition.AccedeGestionCliente(driver);

            // Paso 1 - Hacer click en Ofertas
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            actions.AccesoNuevaOferta(driver);

            actions.RellenarCamposOferta("CRM-COF0006-ELIMINAR", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA", driver);
            actions.GuardarOferta(driver);

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps", driver);

            // Volver al grid
            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            // Paso 2 Seleccionar la Oferta del grid
            actions.BuscarOfertaEnVista("CRM-COF0006-ELIMINAR", driver);
            condition.OfertaGuardadaCorrectamenteEnGrid(driver);

            // Seleccionar la Oferta del grid
            actions.SeleccionarOfertaGrid(driver);

            // Paso 3 - Pulsar Eliminar en la barra de herramientas y cancelar la eliminacion
            actions.EliminarOfertaActual("Cancelar", driver);

            actions.AccesoOfertasLyntia("Mis Ofertas lyntia", driver);

            actions.BuscarOfertaEnVista("CRM-COF0006-ELIMINAR", driver);
            condition.OfertaGuardadaCorrectamenteEnGrid(driver);

            // Paso 4 - Repetir paso anterior eliminando la oferta
            actions.SeleccionarOfertaGrid(driver);

            actions.EliminarOfertaActual("Eliminar", driver);

        }

        //CRM-EOF0003
        [Test]
        [AllureSubSuite("PRO EDITAR OFERTA")]
        public void CRM_EOF0003_Editar_campos_de_una_oferta()
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
            //actions.Restablecimiento_CRM_COF0003(driver);
        }


        //CRM-EOF0004
        [Test]
        [AllureSubSuite("PRO EDITAR OFERTA")]
        public void CRM_EOF0004_Editar_campo_tipo_de_Oferta()
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

        //CRM-APR0001
        [Test]
        [AllureSubSuite("PRO AÑADIR OFERTA")]
        public void CRM_APR0001_añadir_producto()
        {
            //Paso 1 Login y acceso al modulo gestion de cliente
            actions.AccesoGestionCliente(driver);//Acceso al modulo de Gestion de Cliente(Aplicaciones)
            condition.AccedeGestionCliente(driver);//Acceso correcto

            //Paso 2
            actions.AccesoOferta(driver);//Oferta menu
            condition.AccedeOferta(driver);//comprobamos el acceso

            //Paso 3
            actions.SeleccionOfertaAPR0001(driver);//hacemos click en una oferta del listado para añadir producto
            condition.AccederSeleccionOfertaAPR0001(driver);//accede a la oferta

            //Paso 4
            actions.Editar_añadir_producto(driver);//se pulsa añadir producto en la pestaña general y realizamos unas comprobaciones
            condition.Resultado_Editar_añadir_producto(driver);//se verifican cambios

            //Paso 5
            //actions.Cancelar_y_cerrar(driver);
            //condition.Resultado_Cancelar_y_cerrar(driver);
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

        public void AccesoOfertasLyntia(String seccion, IWebDriver driver)
        {

            driver.FindElement(By.Id("sitemap-entity-oferta")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//h1[@title='Seleccionar vista']")).Click(); //Expandimos la opción de Ofertas lyntia

            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//Opción no recomentada con cursores del teclado
            //new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();

            //driver.FindElements(By.XPath("//*[starts-with(@id, 'ViewSelector') and contains(@id, 'list')]"))[3].Click();//Opción escalable
            driver.FindElement(By.XPath("//span[contains(text(), '" + seccion + "')]")).Click();//Opción escalable
            Thread.Sleep(2000);
        }

        public void CrearOfertaRapida(IWebDriver driver)
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
            driver.FindElement(By.XPath("//div[@data-id='cell-1-4']")).Click();
            Thread.Sleep(2000);
        }

        public void AccesoFechasOferta(IWebDriver driver)
        {
            // Click en pestaña Fechas
            driver.FindElement(By.XPath("//li[@title='Fechas']")).Click();
            Thread.Sleep(6000);

        }

        public void GuardarOferta(IWebDriver driver)
        {
            // Click en Guardar en la barra de herramientas         
            driver.FindElement(By.XPath("//button[@aria-label='Guardar']")).Click();
            Thread.Sleep(3000);
        }

        public void GuardarYCerrarOferta(IWebDriver driver)
        {
            // Click en Guardar y cerrar en la barra de herramientas
            driver.FindElement(By.XPath("//button[@aria-label='Guardar y cerrar']")).Click();
            Thread.Sleep(3000);
        }

        public void RellenarCamposOferta(String nombre, String cliente, String tipoOferta, String kam, IWebDriver driver)
        {
            Actions accionesSelenium = new Actions(driver);

            driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).Click();
            driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).SendKeys(Keys.Delete);

            if (!nombre.Equals(""))
            {
                // Rellenar Cliente de Oferta
                driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).SendKeys(nombre);
                Thread.Sleep(1000);

            }

            if (!cliente.Equals(""))
            {
                // Rellenar Cliente de Oferta
                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();
                Thread.Sleep(3000);

                driver.FindElement(By.XPath("//input[contains(@id,'customerid')]")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//input[contains(@id,'customerid')]")).SendKeys(cliente);
                Thread.Sleep(1000);

                driver.FindElement(By.XPath("//span[contains(text(), '" + cliente + "')]")).Click();
                Thread.Sleep(2000);

            }

            if (!tipoOferta.Equals(""))
            {
                // Rellenar Tipo de Oferta
                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();

                SelectElement drop = new SelectElement(driver.FindElement(By.XPath("//select[@aria-label='Tipo de oferta']")));

                drop.SelectByText(tipoOferta);

                driver.FindElement(By.XPath("//input[contains(@id,'referencia_oferta')]")).SendKeys(Keys.PageDown);

            }

            if (!kam.Equals(""))
            {
                // Rellenar Tipo de Oferta
                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();

                driver.FindElement(By.XPath("//input[contains(@id,'kamresponsable')]")).Click();
                Thread.Sleep(1000);

                driver.FindElement(By.XPath("//input[contains(@id,'kamresponsable')]")).SendKeys(Keys.Control + "a");
                driver.FindElement(By.XPath("//input[contains(@id,'kamresponsable')]")).SendKeys(Keys.Delete);

                driver.FindElement(By.XPath("//input[contains(@id,'kamresponsable')]")).SendKeys(kam);
                Thread.Sleep(1000);

                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();

                driver.FindElement(By.XPath("//span[contains(text(), '" + kam + "')]")).Click();
                Thread.Sleep(2000);

            }
        }

        public void EliminarOfertaActual(String opcion, IWebDriver driver)
        {
            // Click en Eliminar
            driver.FindElement(By.XPath("//button[contains(@aria-label,'Eliminar')]")).Click();

            // Confirmar Borrado
            if (opcion.Equals("Eliminar"))
            {
                driver.FindElement(By.XPath("//button[@id='confirmButton']")).Click();
                Thread.Sleep(4000);

            }
            else
            {
                driver.FindElement(By.XPath("//button[@id='cancelButton']")).Click();
                Thread.Sleep(4000);

            }
        }

        internal void BuscarOfertaEnVista(string nombreOferta, IWebDriver driver)
        {
            driver.FindElement(By.XPath("//input[contains(@id,'quickFind')]")).SendKeys(nombreOferta);
            driver.FindElement(By.XPath("//span[contains(@id,'quickFind_button')]")).Click();
            Thread.Sleep(2000);

        }

        internal void AbrirOfertaEnVista(string nombreOferta, IWebDriver driver)
        {
            driver.FindElement(By.XPath("//a[@title='" + nombreOferta + "']")).Click();
            Thread.Sleep(2000);

        }

        internal void SeleccionarOfertaGrid(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//div[@data-id='cell-0-1']")).Click();
            Thread.Sleep(2000);

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
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Fecha de Fecha de presentación')]")).Click();//Calendario
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'diciembre 14, 2020')]")).Click();//seleccionamos fecha del calendario
            driver.FindElement(By.XPath("//li[contains(@title, 'Fechas')]")).Click();//Pestaña fechas
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("IntroduccirDatos.png");
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();//Guarda y cierra
            Thread.Sleep(2000);
        }



        public void Tipo_de_oferta_Cambiodecapacidad(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Prueba-Auto_NO_borrarCRM-EOF0004")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Nuevo servicio')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//selecciona la opcion inferior
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }
        public void Tipo_de_oferta_Cambiodeprecio(IWebDriver driver)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de capacidad (Upgrade/Downgrade)')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la opcion inferior
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }
        public void Tipo_de_oferta_Cambiodesolucion(IWebDriver driver)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de precio/Renovación')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la opcion inferior
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }
        public void Tipo_de_oferta_Cambiodedireccion(IWebDriver driver)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de solución técnica (Tecnología)')]")).Click();
            new Actions(driver).SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la opcion inferior
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

        public void SeleccionOfertaAPR0001(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Prueba_AUTO_CRM-APR")).Click();

        }
        public void Editar_añadir_producto(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Click();//pulsamos sobre agregar producto
            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateSaveAndCloseBtn')]")).Click();//guardamos
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateSaveAndCloseBtn')]")).Click();//guardamos y cerramos
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

    }

    public class OfertaConditions
    {

        public void AccedeGestionCliente(IWebDriver driver)
        {
            Assert.AreEqual(true, driver.FindElement(By.LinkText("Gestión del Cliente")).Enabled);
            Assert.AreEqual("Gestión del Cliente", driver.FindElement(By.LinkText("Gestión del Cliente")).Text);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("AccedeGestionCliente.png");
        }

        public void CreaOferta(IWebDriver driver, Navegacion navegacion)
        {
            // Assert de título "Nuevo Oferta" del formulario
            Assert.AreEqual("Nuevo Oferta", driver.FindElement(By.XPath("//h1[@data-id='header_title']")).Text);

            // Assert de tab por defecto "General"
            Assert.IsTrue(driver.FindElement(By.XPath("//li[@aria-label='General']")).GetAttribute("aria-selected").Equals("true"));

            // Assert de Razón para el estado de la Oferta "En elaboración" 
            Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//section[@id='quote information']//span[@aria-label='Razón para el estado']//span")).Text);

            // Assert de Tipo de Oferta Nuevo Servicio
            driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).SendKeys(Keys.PageDown);
            Assert.AreEqual(driver.FindElement(By.XPath("//select[contains(@aria-label,'Tipo de oferta')]")).GetAttribute("title"), "Nuevo servicio");

            driver.FindElement(By.XPath("//input[contains(@id,'referencia_oferta')]")).SendKeys(Keys.PageDown);
            driver.FindElement(By.XPath("//input[contains(@id,'referencia_oferta')]")).SendKeys(Keys.PageDown);

            // Assert de Divisa
            Assert.AreEqual("Euro", driver.FindElement(By.XPath("//div[contains(@data-id,'transactioncurrencyid_selected_tag_text')]")).GetAttribute("title"));

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

        public void ofertaNoCreada(IWebDriver driver)
        {
            // Assert de alerta con los campos obligatorios sin informar
            //div[@data-id="notificationWrapper"]
            Assert.IsTrue(driver.FindElements(By.XPath("//div[@data-id='notificationWrapper']")).Count > 0);
            driver.FindElement(By.XPath("//div[@data-id='notificationWrapper']")).Click();
        }

        internal void FechasInformadasCorrectamente(IWebDriver driver)
        {
            // Assert de Fecha de creación vacía
            Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@data-id,'createdon')]")).GetAttribute("value").Equals(""));

            // Assert de Hora de creación vacía
            Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@aria-label,'Time of Fecha de creación')]")).GetAttribute("value").Equals(""));

            // Assert de Fecha de modificación vacía
            Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@data-id,'modifiedon')]")).GetAttribute("value").Equals(""));

            // Assert de Hora de modificación vacía
            Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@aria-label,'Time of Fecha de modificación')]")).GetAttribute("value").Equals(""));
        }

        internal void OfertaGuardadaCorrectamente(String nombreOferta, String tipoOferta, IWebDriver driver)
        {
            Actions accionesSelenium = new Actions(driver);

            // Nombre de Oferta
            Thread.Sleep(6000);
            Assert.AreEqual(nombreOferta, driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).GetAttribute("value"));

            // Cliente 
            Assert.AreEqual("CLIENTE INTEGRACION", driver.FindElement(By.XPath("//div[contains(@data-id,'customerid_selected_tag')]")).Text);

            // Razon para el estado
            Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//span[@aria-label='Razón para el estado']")).Text);

            driver.FindElement(By.XPath("//div[contains(@data-id,'customerid_selected_tag')]")).SendKeys(Keys.PageDown);

            Assert.AreEqual(tipoOferta, driver.FindElement(By.XPath("//select[@aria-label='Tipo de oferta']")).GetAttribute("title"));
            Thread.Sleep(1000);

        }

        internal void OfertaGuardadaCorrectamenteEnGrid(IWebDriver driver)
        {
            // Se encuentra en estado borrador
            Assert.AreEqual("Borrador", driver.FindElement(By.XPath("//div[@data-id='cell-0-7']")).GetAttribute("title"));

            // Se encuentra en Razon para el estado En elaboracion
            Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//div[@data-id='cell-0-8']")).GetAttribute("title"));

        }

        public void AccederSeleccionOfertaAPR0001(IWebDriver driver)
        {
            Assert.AreEqual(true, driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Enabled);//comprobamos que añadir producto esta habilitado
        }

        public void Resultado_Editar_añadir_producto(IWebDriver driver)
        {
            Assert.AreEqual("Creación rápida: Producto de oferta", driver.FindElement(By.XPath("//h1[contains(@data-id, 'quickHeaderTitle')]")).Text);

            Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", driver.FindElement(By.XPath("/html/body/section/div/div/div/div/section/div[1]/div/div/div/span[2]")).Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Producto existente: Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'productid-error-message')]")).Text);//comprobamos advertencia Producto existente
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'lyn_uso-error-message')]")).Text);//comprobamos advertencia Uso linea de negocio 
            Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'uomid-error-message')]")).Text);//comprobamos advertencia Unidad de venta

            //Cancelamos y cerramos con sus comprobaciones

            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Cancelar')]")).Click();//boton cancelar añadir producto
            Assert.AreEqual("¿Desea guardar los cambios antes de salir de esta página?", driver.FindElement(By.XPath("/html/body/section[2]/div/div/div/div/div/div/div[2]/span")).Text);//Mensaje pop up
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Guardar y continuar')]")).Click();//pulsamos en guardar y continuar de se comprueba que siguen faltando los campos
            Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", driver.FindElement(By.XPath("/html/body/section/div/div/div/div/section/div[1]/div/div/div/span[2]")).Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Producto existente: Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'productid-error-message')]")).Text);//comprobamos advertencia Producto existente
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'lyn_uso-error-message')]")).Text);//comprobamos advertencia Uso linea de negocio 
            Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'uomid-error-message')]")).Text);//comprobamos advertencia Unidad de venta
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Cancelar')]")).Click();//boton cancelar añadir producto
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/section[2]/div/div/div/div/div/div/div[1]/div/button/span")).Click();
            Assert.AreEqual(true, driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Enabled);//volvemos a la pagina de añadir producto
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

        public void Aviso_cambiocapacidad(IWebDriver driver)//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodeprecio(IWebDriver driver)//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de precio” no requiere envío a construcción ni cambiar el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodesolucion(IWebDriver driver)//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de tecnología” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodedireccion(IWebDriver driver)//mensaje por el tipo de oferta
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
    }
}