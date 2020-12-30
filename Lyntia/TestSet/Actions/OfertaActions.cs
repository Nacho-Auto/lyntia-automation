using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Lyntia.Utilities;
using Lyntia.TestSet.Conditions;

namespace Lyntia.TestSet.Actions
{
    /// <summary>
    /// OfertaActions Class
    /// </summary>
    public class OfertaActions
    {
        readonly Utils utils = new Utils();

        private static IWebDriver driver;
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;

        /// <summary>
        /// OfertaActions
        /// </summary>
        public OfertaActions()
        {
            driver = Utils.driver;
            ofertaCondition = Utils.getOfertaConditions();
            productoActions = Utils.getProductoActions();
            productoCondition = Utils.getProductoConditions();
            commonActions = Utils.getCommonActions();
            commonCondition = Utils.getCommonConditions();
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
        }

        /// <summary>
        /// Método usado seleccionar vista de Ofertas (Mis Ofertas Lyntia, Todas las ofertas ...)
        /// </summary>
        /// <param name="seccion"></param>
        public void AccesoOfertasLyntia(String seccion)
        {
            if (utils.EncontrarElemento(By.Id(Utils.getIdentifier("Oferta.ofertaSection"))))
            {
                Utils.searchWebElement("Oferta.ofertaSection").Click();
                Thread.Sleep(2000);
                Utils.searchWebElement("Oferta.ofertaTitleSelector").Click();
                driver.FindElement(By.XPath("//span[contains(text(), '" + seccion + "')]")).Click(); //Opción escalable
                Thread.Sleep(2000);
            }

        }

        /// <summary>
        /// Método para hacer click en "+ Nuevo", barra de herramientas
        /// </summary>
        public void AccesoNuevaOferta()
        {
            Utils.searchWebElement("Oferta.newOferta").Click();
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Método para abrir la primera Oferta del grid
        /// </summary>
        public void abrirOferta()
        {
            Utils.searchWebElement("Oferta.firstFromGrid").Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Método para acceder a la pestaña de Fechas de Oferta
        /// </summary>
        public void AccesoFechasOferta()
        {
            // Click en pestaña Fechas
            Utils.searchWebElement("Oferta.datesSection").Click();
            Thread.Sleep(6000);

        }

        /// <summary>
        /// Método para hacer Click en Guardar Oferta en la barra de herramientas
        /// </summary>
        public void GuardarOferta()
        {
            Utils.searchWebElement("Oferta.saveOferta").Click();
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Método para hacer click en Guardar y cerrar en la barra de herramientas
        /// </summary>
        public void GuardarYCerrarOferta()
        {
            Utils.searchWebElement("Oferta.saveAndCloseOferta").Click();
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Métdodo empleado para completar los campos necesarios para crear una nueva Oferta.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="cliente"></param>
        /// <param name="tipoOferta"></param>
        /// <param name="kam"></param>
        public void RellenarCamposOferta(String nombre, String cliente, String tipoOferta, String kam)
        {

            Utils.searchWebElement("Oferta.inputNameOferta").Click();
            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);

            if (!nombre.Equals(""))
            {
                // Rellenar Cliente de Oferta
                Utils.searchWebElement("Oferta.inputNameOferta").Click();
                Thread.Sleep(1000);
                Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(nombre);
                Thread.Sleep(1000);

            }

            if (!cliente.Equals(""))
            {
                // Rellenar Cliente de Oferta
                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();
                Thread.Sleep(3000);

                Utils.searchWebElement("Oferta.inputCustomerId").Click();
                Thread.Sleep(1000);
                Utils.searchWebElement("Oferta.inputCustomerId").SendKeys(cliente);
                Thread.Sleep(1000);
                // Seleccionar cliente del desplegable
                driver.FindElement(By.XPath("//span[contains(text(), '" + cliente + "')]")).Click();
                Thread.Sleep(2000);

            }

            if (!tipoOferta.Equals(""))
            {
                // Rellenar Tipo de Oferta
                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();

                SelectElement drop = new SelectElement(Utils.searchWebElement("Oferta.selectOfertaType"));

                drop.SelectByText(tipoOferta);

                Utils.searchWebElement("Oferta.inputReferenceOferta").SendKeys(Keys.PageDown);

            }

            if (!kam.Equals(""))
            {
                // Rellenar Tipo de Oferta
                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();

                Utils.searchWebElement("Oferta.kamResponsable").Click();
                Thread.Sleep(1000);

                Utils.searchWebElement("Oferta.kamResponsable").SendKeys(Keys.Control + "a");
                Utils.searchWebElement("Oferta.kamResponsable").SendKeys(Keys.Delete);

                Utils.searchWebElement("Oferta.kamResponsable").SendKeys(kam);
                Thread.Sleep(1000);

                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();

                driver.FindElement(By.XPath("//span[contains(text(), '" + kam + "')]")).Click();
                Thread.Sleep(2000);

            }
        }

        /// <summary>
        /// Método para eliminar Oferta seleccionada o abierta. 
        /// El campo opción permite cancelar el borrado o realizarlo por completo.
        /// </summary>
        /// <param name="opcion"></param>
        public void EliminarOfertaActual(String opcion)
        {
            // Click en Eliminar
            Utils.searchWebElement("Oferta.deleteButtonOferta").Click();

            // Confirmar Borrado
            if (opcion.Equals("Eliminar"))
            {
                Utils.searchWebElement("Oferta.confirmDeleteOferta").Click();
                Thread.Sleep(4000);

            }
            else
            {
                Utils.searchWebElement("Oferta.cancelDeleteOferta").Click();
                Thread.Sleep(4000);

            }
        }

        /// <summary>
        /// Método para buscar Ofertas en el grid dado un parámetro de búsqueda.
        /// </summary>
        /// <param name="parametroBusqueda"></param>
        internal void BuscarOfertaEnVista(string parametroBusqueda)
        {
            Utils.searchWebElement("Oferta.inputQuickFindOferta").SendKeys(parametroBusqueda);
            Utils.searchWebElement("Oferta.buttonQuickFindOferta").Click();
            Thread.Sleep(2000);

        }

        /// <summary>
        /// Método empleado para abrir una Oferta sabiendo su nombre.
        /// </summary>
        /// <param name="nombreOferta"></param>
        internal void AbrirOfertaEnVista(string nombreOferta)
        {
            driver.FindElement(By.XPath("//a[@title='" + nombreOferta + "']")).Click();
            Thread.Sleep(2000);

        }

        /// <summary>
        /// Método empleado para una vez buscada una Oferta en el grid, abrirla
        /// </summary>
        internal void SeleccionarOfertaGrid()
        {
            Utils.searchWebElement("Oferta.selectOfertaGrid").Click();
            Thread.Sleep(2000);

        }

        /// <summary>
        /// Método para inserción de datos de una Oferta
        /// </summary>
        public void IntroduccirDatos()//Cumplimentar datos de la oferta(campos contacto, fecha...)
        {
            // campos de la oferta
            // TODO: agregar screenshots a Allure
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("IntroduccirDatosOrinales.png");

            Utils.searchWebElement("Oferta.buttonSearchContact").Click(); //contacto oferta
            Thread.Sleep(2000);

            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform(); //selecciona el primer cliente de la lista
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.Enter).Perform(); //y lo pulsa 

            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys("Prueba_auto_NO_borrar_MODIFICADA");

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

        /// <summary>
        /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de capacidad.
        /// </summary>
        public void Tipo_de_oferta_Cambiodecapacidad()
        {
            driver.FindElement(By.LinkText("Prueba-Auto_NO_borrarCRM-EOF0004")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Nuevo servicio')]")).Click();
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();//selecciona la opcion inferior
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de precio.
        /// </summary>
        public void Tipo_de_oferta_Cambiodeprecio()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de capacidad (Upgrade/Downgrade)')]")).Click();
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la opcion inferior
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de solución.
        /// </summary>
        public void Tipo_de_oferta_Cambiodesolucion()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de precio/Renovación')]")).Click();
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la opcion inferior
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de dirección.
        /// </summary>
        public void Tipo_de_oferta_Cambiodedireccion()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de solución técnica (Tecnología)')]")).Click();
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();////selecciona la opcion inferior
            accionesSelenium.SendKeys(OpenQA.Selenium.Keys.Enter).Perform();//y lo pulsa 
        }

        /// <summary>
        /// Método para reestablecer datos de la Oferta de la prueba CRM_COF0003 de Editar Oferta
        /// </summary>
        public void Restablecimiento_CRM_COF0003()
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

        /// <summary>
        /// Método para seleccionar la Oferta Prueba_AUTO_CRM-APR
        /// </summary>
        public void SeleccionOfertaAPR0001()
        {
            driver.FindElement(By.LinkText("Prueba_AUTO_CRM-APR")).Click();

        }

        /// <summary>
        /// Método para Agregar producto
        /// </summary>
        public void Editar_añadir_producto()
        {
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Click();//pulsamos sobre agregar producto
            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateSaveAndCloseBtn')]")).Click();//guardamos
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateSaveAndCloseBtn')]")).Click();//guardamos y cerramos
        }

        internal void CerrarOfertaActual(string opcion, string razonOferta, string motivoCierre)
        {
            // Click en Cerrar Oferta
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@title='Cerrar Oferta']")).Click();
            Thread.Sleep(2000);

            // Cambiar al frame de Cierre de Ofertas
            driver.SwitchTo().Frame(driver.FindElement(By.Id("FullPageWebResource")));

            if (!razonOferta.Equals(""))
            {
                SelectElement drop = new SelectElement(driver.FindElement(By.XPath("//select[@id='razonestado']")));

                drop.SelectByText(razonOferta);
            }

            if (!motivoCierre.Equals(""))
            {
                SelectElement drop = new SelectElement(driver.FindElement(By.XPath("//select[@id='motivocierre']")));

                drop.SelectByText(motivoCierre);
            }

            if (opcion.Equals("Aceptar"))
            {
                // Cancelación realizada
                driver.FindElement(By.XPath("//button[@id='btnOK']")).Click();
                Thread.Sleep(3000);
            }
            else
            {
                // Cancelación anulada
                driver.FindElement(By.XPath("//button[@id='cmdDialogCancel']")).Click();
                Thread.Sleep(3000);
            }
        }

        public void SeleccionOferta()//Seleccion de una oferta del listado
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

        public void SeleccionarOfertaGridEliminar()//selecciona una oferta y la eliminamos(realizamos comprobaciones tipo cancelar)
        {
            driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Click();//seleccionamos una oferta ganada y pulsamos sobre el ckeck
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Eliminar')]")).Click();//pulsamos sobre eliminar de la barra superior del menu
            driver.FindElement(By.XPath("//hi[contains(@aria-label, 'Confirmar eliminación')]"));//encuentra el elemento pop up
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Cancelar')]")).Click();//cancelar del pop up
            driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Click();//seleccionamos una oferta ganada y pulsamos sobre el ckeck
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Eliminar')]")).Click();//pulsamos sobre eliminar de la barra superior del menu
            driver.FindElement(By.XPath("//hi[contains(@aria-label, 'Confirmar eliminación')]"));//encuentra el elemento pop up
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Eliminar')]")).Click();
        }
        public void AccederOfertaestado_Adjudicada()
        {
            driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Click();//seleccionamos una oferta ganada y pulsamos sobre el ckeck
            Thread.Sleep(3000);
        }

        public void FiltrarPorIDRevision(string idRevision)
        {
            Utils.searchWebElement("Oferta.gridFilterIdRevision").Click();
            Utils.searchWebElement("Oferta.gridFilterBuscarPor").Click();
            Utils.searchWebElement("Oferta.gridFilterBuscarPorInput").Clear();
            Utils.searchWebElement("Oferta.gridFilterBuscarPorInput").SendKeys(idRevision);
            Thread.Sleep(2000);
            Utils.searchWebElement("Oferta.gridFilterBuscarPorAceptarButton").Click();
            Thread.Sleep(2000);

        }

        public void SeleccionarTodasOfertaGrid()
        {
            Utils.searchWebElement("Oferta.gridSelectAll").Click();   
        }

        public void Eliminar_BarraMenu()
        {
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Eliminar')]")).Click();//pulsamos sobre eliminar de la barra superior del menu
        }
        public void Cancelar()
        {
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Cancelar')]")).Click();//cancelar del pop up
        }
        public void Seleccionofertarazonadjudicada()
        {
            driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Click();//seleccionamos una oferta ganada y pulsamos sobre el ckeck
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Editar')]")).Click();//seleccionamos una oferta ganada y pulsamos sobre editar
        }
        public void Eliminar_Popup()//pulsamos sobre el eliminar del popup
        {
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='confirmButtonText']")).Click();

        }

        public void PresentarOferta()
        {
            driver.FindElement(By.XPath("//button[@title='Presentar Oferta']")).Click();
        }
    }
}