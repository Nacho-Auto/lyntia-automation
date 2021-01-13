﻿using System;
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
        private static WebDriverWait wait;

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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        }

        /// <summary>
        /// Método usado seleccionar vista de Ofertas (Mis Ofertas Lyntia, Todas las ofertas ...)
        /// </summary>
        /// <param name="seccion"></param>
        public void AccesoOfertasLyntia(String seccion)
        {
            if (utils.EncontrarElemento(By.Id(Utils.getIdentifier("Oferta.ofertaSection"))))
            {
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(Utils.getIdentifier("Oferta.ofertaSection"))));
                    Thread.Sleep(6000);
                    Utils.searchWebElement("Oferta.ofertaSection").Click();
                    Thread.Sleep(2000);
                    Utils.searchWebElement("Oferta.ofertaTitleSelector").Click();
                    driver.FindElement(By.XPath("//span[contains(text(), '" + seccion + "')]")).Click(); //Opción escalable
                    Thread.Sleep(2000);

                }catch(NoSuchElementException e)
                {
                    CommonActions.CapturadorExcepcion(e, "AccesoSeccionOfertas.png", "No se pudo acceder a la sección " + seccion);
                }
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
                Utils.searchWebElement("Oferta.inputNameOferta").Click();
                Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);

                // Rellenar Cliente de Oferta
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

                Thread.Sleep(1000);
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
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.getIdentifier("Oferta.deleteButtonOferta"))));
            if (utils.EncontrarElemento(By.XPath(Utils.getIdentifier("Oferta.deleteButtonOferta"))))
            {
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
        }

        /// <summary>
        /// Método para buscar Ofertas en el grid dado un parámetro de búsqueda.
        /// </summary>
        /// <param name="parametroBusqueda"></param>
        public void BuscarOfertaEnVista(string parametroBusqueda)
        {
            Utils.searchWebElement("Oferta.inputQuickFindOferta").SendKeys(parametroBusqueda);
            Utils.searchWebElement("Oferta.buttonQuickFindOferta").Click();
            Thread.Sleep(2000);

        }

        /// <summary>
        /// Método empleado para abrir una Oferta sabiendo su nombre.
        /// </summary>
        /// <param name="nombreOferta"></param>
        public void AbrirOfertaEnVista(string nombreOferta)
        {
            driver.FindElement(By.XPath("//a[@title='" + nombreOferta + "']")).Click();
            Thread.Sleep(2000);

        }

        /// <summary>
        /// Método empleado para una vez buscada una Oferta en el grid, abrirla
        /// </summary>
        public void SeleccionarOfertaGrid()
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
            Utils.searchWebElement("Oferta.buttonSearchContact").Click(); //contacto oferta
            Thread.Sleep(2000);

            Utils.searchWebElement("Oferta.inputContactId").Click();
            Thread.Sleep(1000);
            Utils.searchWebElement("Oferta.inputContactId").SendKeys("ddd ddd");
            Thread.Sleep(1000);
            // Seleccionar contacto
            driver.FindElement(By.XPath("//span[contains(text(), 'ddd ddd')]")).Click();
            Thread.Sleep(2000);

            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys("Prueba_auto_NO_borrar_MODIFICADA");

            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);
            Thread.Sleep(2000);

            Utils.searchWebElement("Oferta.labelPermutaDefault").Click(); //Toggle Switch
            Utils.searchWebElement("Oferta.inputCalendar").Click();
            Utils.searchWebElement("Oferta.calendarDateRandomDay").Click(); //seleccionamos fecha del calendario
            Utils.searchWebElement("Oferta.inputReferenceOferta").SendKeys(Keys.PageDown);
            Thread.Sleep(3000);

            Utils.searchWebElement("Oferta.inputGVAL").SendKeys("prue123456");//Escribimos prue123456 en codigo gval
            Utils.searchWebElement("Oferta.inputCampoDescripcion").SendKeys("Prueba campo descripcion");//Escribimos Prueba campo descripcion en detalle de oferta
            Thread.Sleep(3000);

            Utils.searchWebElement("Oferta.buttonGuardar").Click();//Guardar
            Thread.Sleep(3000);
            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);
            Thread.Sleep(2000);
            Utils.searchWebElement("Oferta.inputCalendar").Click();//Calendario
            Utils.searchWebElement("Oferta.calendarDateRandomDay").Click();//seleccionamos fecha del calendario
            Utils.searchWebElement("Oferta.LabelFechaspestaña").Click();//Pestaña fechas
            Utils.searchWebElement("Oferta.ButtonGuardarYcerrar").Click();//Guarda y cierra
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de capacidad.
        /// </summary>
        public void Tipo_de_oferta_Cambiodecapacidad()
        {
            BuscarOfertaEnVista("Prueba-Auto_NO_borrarCRM-EOF0004");
            AbrirOfertaEnVista("Prueba-Auto_NO_borrarCRM-EOF0004");
            Thread.Sleep(3000);

            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);

            SelectElement drop = new SelectElement(Utils.searchWebElement("Oferta.selectOfertaType"));

            drop.SelectByText("Cambio de capacidad (Upgrade/Downgrade)");
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de precio.
        /// </summary>
        public void Tipo_de_oferta_Cambiodeprecio()
        {
            Thread.Sleep(3000);

            SelectElement drop = new SelectElement(Utils.searchWebElement("Oferta.selectOfertaType"));

            drop.SelectByText("Cambio de precio/Renovación");
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de solución.
        /// </summary>
        public void Tipo_de_oferta_Cambiodesolucion()
        {
            Thread.Sleep(3000);

            SelectElement drop = new SelectElement(Utils.searchWebElement("Oferta.selectOfertaType"));

            drop.SelectByText("Cambio de solución técnica (Tecnología)");
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de dirección.
        /// </summary>
        public void Tipo_de_oferta_Cambiodedireccion()
        {
            Thread.Sleep(3000);

            SelectElement drop = new SelectElement(Utils.searchWebElement("Oferta.selectOfertaType"));

            drop.SelectByText("Cambio de dirección (Migración)");
        }

        /// <summary>
        /// Método para reestablecer datos de la Oferta de la prueba CRM_COF0003 de Editar Oferta
        /// </summary>
        public void Restablecimiento_CRM_COF0003()
        {
            AbrirOfertaEnVista("Automatica_MODPrueba_auto_NO_borrar_MODIFICADA");
            Utils.searchWebElement("Oferta.inputNameOferta").Click();
            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);
            Thread.Sleep(3000);

            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys("Automatica_MOD");
            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);

            Utils.searchWebElement("Oferta.calendarFechadepresentacion").SendKeys(Keys.Control + "a");
            Utils.searchWebElement("Oferta.calendarFechadepresentacion").SendKeys(Keys.Delete)

            Utils.searchWebElement("Oferta.inputReferenceOferta").SendKeys(Keys.PageDown);
            Thread.Sleep(3000);
            Utils.searchWebElement("Oferta.labelPermutaDefaultReset").Click(); //Toggle Switch

            Utils.searchWebElement("Oferta.inputGVAL").Click();
            Utils.searchWebElement("Oferta.inputGVAL").SendKeys(Keys.Control + "a");
            Utils.searchWebElement("Oferta.inputGVAL").SendKeys(Keys.Delete);
            Utils.searchWebElement("Oferta.inputCampoDescripcion").Click();
            Utils.searchWebElement("Oferta.inputCampoDescripcion").SendKeys(Keys.Control + "a");
            Utils.searchWebElement("Oferta.inputCampoDescripcion").SendKeys(Keys.Delete);
            Utils.searchWebElement("Oferta.inputGVAL").Clear();
            Thread.Sleep(3000);

            Thread.Sleep(3000);
            Utils.searchWebElement("Oferta.ButtonGuardarYcerrar").Click();//Guarda y cierra
        }

        /// <summary>
        /// Método para seleccionar la Oferta Prueba_AUTO_CRM-APR
        /// </summary>
        public void SeleccionOfertaAPR0001()
        {
            BuscarOfertaEnVista("Prueba_AUTO_CRM-APR");
            AbrirOfertaEnVista("Prueba_AUTO_CRM-APR");
        }

        /// <summary>
        /// Cerrar la Oferta actualmente abierta pasando los parámetros:
        /// </summary>
        public void Editar_añadir_producto()
        {
            Utils.searchWebElement("Oferta.buttonAgregarProducto").Click();//pulsamos sobre agregar producto
            if (utils.EncontrarElemento(By.XPath("//button[contains(@data-id, 'quickCreateSaveAndCloseBtn')]")))
            {
                Thread.Sleep(2000);
                Utils.searchWebElement("Producto.ButtonGuardar").Click();//guardamos
                Thread.Sleep(3000);
                Utils.searchWebElement("Oferta.buttonGuardarYcerrarProd").Click();//guardamos y cerramos
            }

        }
      
        /// <param name="opcion"></param>
        /// <param name="razonOferta"></param>
        /// <param name="motivoCierre"></param>
        public void CerrarOfertaActual(string opcion, string razonOferta, string motivoCierre)
        {
            // Click en Cerrar Oferta
            Thread.Sleep(2000);
            Utils.searchWebElement("Oferta.buttonCerrar").Click();
            Thread.Sleep(2000);

            // Cambiar al frame de Cierre de Ofertas
            driver.SwitchTo().Frame(Utils.searchWebElement("Oferta.frameCerrarOferta"));

            if (!razonOferta.Equals(""))
            {
                SelectElement drop = new SelectElement(Utils.searchWebElement("Oferta.selectRazonEstado"));
                drop.SelectByText(razonOferta);
            }

            if (!motivoCierre.Equals(""))
            {
                SelectElement drop = new SelectElement(Utils.searchWebElement("Oferta.selectMotivoCierre"));
                drop.SelectByText(motivoCierre);
            }

            if (opcion.Equals("Aceptar"))
            {
                // Cancelación realizada
                Utils.searchWebElement("Oferta.buttonConfirmarCierre").Click();
                Thread.Sleep(3000);
            }
            else
            {
                // Cancelación anulada
                Utils.searchWebElement("Oferta.buttonCancelarCierre").Click();
                Thread.Sleep(3000);
            }
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
            Utils.searchWebElement("Oferta.gridFilterBuscarPorInput").SendKeys(Keys.Control + "a");
            Utils.searchWebElement("Oferta.gridFilterBuscarPorInput").SendKeys(Keys.Delete);

            Utils.searchWebElement("Oferta.gridFilterBuscarPorInput").SendKeys(idRevision);
            Thread.Sleep(2000);

            Utils.searchWebElement("Oferta.gridFilterBuscarPorAceptarButton").Click();
            Thread.Sleep(2000);
        }

        public void SeleccionarTodasOfertaGrid()
        {
            Utils.searchWebElement("Oferta.gridSelectAll").Click();
            Thread.Sleep(2000);
        }

        public void Eliminar_BarraMenu()
        {
            Utils.searchWebElement("Oferta.buttonEliminar").Click();//pulsamos sobre eliminar de la barra superior del menu
        }

        public void Cancelar()
        {
            Utils.searchWebElement("Producto.buttonCancelar").Click();//cancelar del pop up
        }

        public void Seleccionofertarazonadjudicada()
        {
            Thread.Sleep(3000);
            Utils.searchWebElement("Oferta.labelOfertaestadoGanada").Click();//seleccionamos una oferta ganada y pulsamos sobre el ckeck
            Thread.Sleep(2000);
            Utils.searchWebElement("Oferta.buttonEditar").Click();//pulsamos sobre editar
            Thread.Sleep(3000);
        }

        public void Eliminar_Popup()//pulsamos sobre el eliminar del popup
        {
            Thread.Sleep(2000);
            Utils.searchWebElement("Producto.buttonEliminarProductodeOfertaConfirm").Click();

        }

        public void PresentarOferta()
        {
            Utils.searchWebElement("Oferta.buttonPresentOferta").Click();
        }

        public void Filtro_buscarEnestaVista(String busqueda)
        {
            try
            {
                Utils.searchWebElement("Oferta.inputFilter").SendKeys(busqueda);//buscamos una oferta en el filtro
                Thread.Sleep(1000);
                Utils.searchWebElement("Oferta.buttonLupaFiltro").Click();
                Thread.Sleep(3000);
                driver.FindElement(By.LinkText(busqueda)).Click();//click en la oferta
                Thread.Sleep(3000);

                Console.WriteLine("Se ha filtrado correctamente la oferta");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Filtro_buscarEnestaVista.png", "No se ha podido filtrar correctamente la oferta");
            }
        }

        public void ReestablecerDatosCRM_EOF0004()
        {
            //reestablece datos CRM-EOF0004
            driver.FindElement(By.LinkText("Prueba-Auto_NO_borrarCRM-EOF0004")).Click();
            Utils.searchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);

            SelectElement drop = new SelectElement(Utils.searchWebElement("Oferta.selectOfertaType"));

            drop.SelectByText("Nuevo servicio");

            GuardarYCerrarOferta();
        }
    }
}