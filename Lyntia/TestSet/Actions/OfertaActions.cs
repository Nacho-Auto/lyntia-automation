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
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;
        private static WebDriverWait wait;

        /// <summary>
        /// OfertaActions
        /// </summary>
        public OfertaActions()
        {
            driver = Utils.driver;
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        }

        /// <summary>
        /// Método usado seleccionar vista de Ofertas (Mis Ofertas Lyntia, Todas las ofertas ...)
        /// </summary>
        /// <param name="seccion"></param>
        public void AccesoOfertasLyntia(String seccion)
        {
            if (utils.EncontrarElemento(By.Id(Utils.GetIdentifier("Oferta.ofertaSection"))))
            {
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(Utils.GetIdentifier("Oferta.ofertaSection"))));
                    Thread.Sleep(6000);
                    Utils.SearchWebElement("Oferta.ofertaSection").Click();
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Oferta.ofertaTitleSelector").Click();
                    driver.FindElement(By.XPath("//span[contains(text(), '" + seccion + "')]")).Click(); //Opción escalable
                    Thread.Sleep(2000);

                    Console.WriteLine("Se accede correctamente a la sección " + seccion);

                }catch(Exception e)
                {
                    CommonActions.CapturadorExcepcion(e, "AccesoSeccionOfertas.png", "No se pudo acceder a la sección " + seccion);
                    driver.Quit();
                }
            }
        }

        /// <summary>
        /// Método para hacer click en "+ Nuevo", barra de herramientas
        /// </summary>
        public void AccesoNuevaOferta()
        {
            try
            {
                Utils.SearchWebElement("Oferta.newOferta").Click();
                Thread.Sleep(3000);

                Console.WriteLine("Nueva Oferta creada correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AccesoNuevaOferta.png", "No se pudo crear una nueva Oferta");
                driver.Quit();
            }
        }

        /// <summary>
        /// Método para abrir la primera Oferta del grid
        /// </summary>
        public void AbrirOferta()
        {
            try
            {
                Utils.SearchWebElement("Oferta.firstFromGrid").Click();
                Thread.Sleep(2000);

                Console.WriteLine("La oferta se abre correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AbrirOferta.png", "La oferta no se abre correctamente");
                driver.Quit();
            }
        }

        /// <summary>
        /// Método para acceder a la pestaña de Fechas de Oferta
        /// </summary>
        public void AccesoFechasOferta()
        {
            try
            {
                // Click en pestaña Fechas
                Utils.SearchWebElement("Oferta.datesSection").Click();
                Console.WriteLine("Se accede correctamente a la pestaña fechas de la oferta");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AccesoFechasOferta.png", "No se accede correctamente a la pestaña fechas de la oferta");
                driver.Quit();
            }
            
        }

        /// <summary>
        /// Método para hacer Click en Guardar Oferta en la barra de herramientas
        /// </summary>
        public void GuardarOferta()
        {
            try
            {
                Utils.SearchWebElement("Oferta.saveOferta").Click();
                Thread.Sleep(3000);

                Console.WriteLine("La Oferta se guarda correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "GuardarOferta.png", "No se pudo guardar la Oferta");
                driver.Quit();
            }  
        }

        /// <summary>
        /// Método para hacer click en Guardar y cerrar en la barra de herramientas
        /// </summary>
        public void GuardarYCerrarOferta()
        {
            try
            {
                Utils.SearchWebElement("Oferta.saveAndCloseOferta").Click();
                Thread.Sleep(3000);
                Console.WriteLine("Se guarda y se cierra correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "GuardarYCerrarOferta.png", "No se guarda y cierra correctamente");
                driver.Quit();
            }
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
            try
            {
                Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);

                if (!nombre.Equals(""))
                {
                    Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                    Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);

                    // Rellenar Cliente de Oferta
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(nombre);
                    Thread.Sleep(1000);
                }

                if (!cliente.Equals(""))
                {
                    // Rellenar Cliente de Oferta
                    accionesSelenium.SendKeys(Keys.PageDown);
                    accionesSelenium.Build().Perform();
                    Thread.Sleep(3000);

                    Utils.SearchWebElement("Oferta.inputCustomerId").Click();
                    Thread.Sleep(1000);
                    Utils.SearchWebElement("Oferta.inputCustomerId").SendKeys(cliente);
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

                    SelectElement drop = new SelectElement(Utils.SearchWebElement("Oferta.selectOfertaType"));

                    drop.SelectByText(tipoOferta);

                    Utils.SearchWebElement("Oferta.inputReferenceOferta").SendKeys(Keys.PageDown);
                }

                if (!kam.Equals(""))
                {
                    // Rellenar Tipo de Oferta
                    accionesSelenium.SendKeys(Keys.PageDown);
                    accionesSelenium.Build().Perform();

                    Thread.Sleep(1000);
                    Utils.SearchWebElement("Oferta.kamResponsable").Click();
                    Thread.Sleep(1000);

                    Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(Keys.Delete);

                    Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(kam);
                    Thread.Sleep(1000);

                    accionesSelenium.SendKeys(Keys.PageDown);
                    accionesSelenium.Build().Perform();

                    driver.FindElement(By.XPath("//span[contains(text(), '" + kam + "')]")).Click();
                    Thread.Sleep(2000);
                }

                Console.WriteLine("Se han introducido correctamente los campos de la Oferta: " + nombre + ", " + cliente + ", " + tipoOferta + ", " + kam);
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AddDatosOferta.png", "No se introducen los datos de la Oferta: " + nombre + ", " + cliente + ", " + tipoOferta + ", " + kam);
                driver.Quit();
            }
        }

        /// <summary>
        /// Método para eliminar Oferta seleccionada o abierta. 
        /// El campo opción permite cancelar el borrado o realizarlo por completo.
        /// </summary>
        /// <param name="opcion"></param>
        public void EliminarOfertaActual(String opcion)
        {
            try
            {
                // Click en Eliminar
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Oferta.deleteButtonOferta"))));
                if (utils.EncontrarElemento(By.XPath(Utils.GetIdentifier("Oferta.deleteButtonOferta"))))
                {
                    Utils.SearchWebElement("Oferta.deleteButtonOferta").Click();

                    // Confirmar Borrado
                    if (opcion.Equals("Eliminar"))
                    {
                        Utils.SearchWebElement("Oferta.confirmDeleteOferta").Click();
                        Thread.Sleep(4000);

                    }
                    else
                    {
                        Utils.SearchWebElement("Oferta.cancelDeleteOferta").Click();
                        Thread.Sleep(4000);

                        Console.WriteLine("La oferta se elimina correctamente");

                    }
                }
               
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "EliminarOfertaActual.png", "La oferta no se elimina correctamente");
                driver.Quit();
            }
            
        }

        /// <summary>
        /// Método para buscar Ofertas en el grid dado un parámetro de búsqueda.
        /// </summary>
        /// <param name="parametroBusqueda"></param>
        public void BuscarOfertaEnVista(string parametroBusqueda)
        {
            try
            {
                Utils.SearchWebElement("Oferta.inputQuickFindOferta").SendKeys(parametroBusqueda);
                Utils.SearchWebElement("Oferta.buttonQuickFindOferta").Click();
                Thread.Sleep(2000);

                Console.WriteLine("La busqueda con el filtro funciona correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "BuscarOfertaEnVista", "La busqueda con el filtro no funciona correctamente");
                driver.Quit();
            }

        }

        /// <summary>
        /// Método empleado para abrir una Oferta sabiendo su nombre.
        /// </summary>
        /// <param name="nombreOferta"></param>
        public void AbrirOfertaEnVista(string nombreOferta)
        {
            try
            {
                driver.FindElement(By.XPath("//a[@title='" + nombreOferta + "']")).Click();
                Thread.Sleep(2000);
                Console.WriteLine("La oferta se abre correctamente desde el filtro");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AbrirOfertaEnVista.png", "La oferta no se abre correctamente desde el filtro");
                driver.Quit();
            }

        }

        /// <summary>
        /// Método empleado para una vez buscada una Oferta en el grid, abrirla
        /// </summary>
        public void SeleccionarOfertaGrid()
        {
            try
            {
                Utils.SearchWebElement("Oferta.selectOfertaGrid").Click();
                Thread.Sleep(2000);
                Console.WriteLine("Se selecciona una oferta del grid correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "SeleecionarOfertaGrid.png", "No se selecciona una oferta del grid correctamente");
                driver.Quit();
            }
        }

        /// <summary>
        /// Método para inserción de datos de una Oferta
        /// </summary>
        public void IntroduccirDatos()//Cumplimentar datos de la oferta(campos contacto, fecha...)
        {
            try
            {
                // campos de la oferta
                //Utils.SearchWebElement("Oferta.buttonSearchContact").Click(); //contacto oferta
                //Thread.Sleep(2000);

                Utils.SearchWebElement("Oferta.inputContactId").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Oferta.inputContactId").SendKeys("ddd ddd");
                Thread.Sleep(1000);

                // Seleccionar contacto
                //driver.FindElement(By.XPath("//span[contains(text(), 'ddd ddd')]")).Click();
                //Thread.Sleep(2000);

                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys("Prueba_auto_NO_borrar_MODIFICADA");
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);
                Thread.Sleep(2000);

                // Seleccionar referencia

                Utils.SearchWebElement("Oferta.inputReferenceOferta").SendKeys("1234");
                Thread.Sleep(1000);

                // Resto de campos
                Utils.SearchWebElement("Oferta.labelPermutaDefault").SendKeys(Keys.Enter); //Toggle Switch
                Utils.SearchWebElement("Oferta.inputCalendar").Click();
                Utils.SearchWebElement("Oferta.calendarDateRandomDay").Click(); //seleccionamos fecha del calendario
                Utils.SearchWebElement("Oferta.inputReferenceOferta").SendKeys(Keys.PageDown);
                Thread.Sleep(3000);

                Utils.SearchWebElement("Oferta.inputGVAL").SendKeys("prue123456");//Escribimos prue123456 en codigo gval
                Utils.SearchWebElement("Oferta.inputCampoDescripcion").SendKeys("Prueba campo descripcion");//Escribimos Prueba campo descripcion en detalle de oferta
                Thread.Sleep(3000);

                Utils.SearchWebElement("Oferta.buttonGuardar").Click();//Guardar
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.inputCalendar").Click();//Calendario
                Utils.SearchWebElement("Oferta.calendarDateRandomDay").Click();//seleccionamos fecha del calendario
                Utils.SearchWebElement("Oferta.LabelFechaspestaña").Click();//Pestaña fechas
                Utils.SearchWebElement("Oferta.ButtonGuardarYcerrar").Click();//Guarda y cierra
                Thread.Sleep(2000);
                Console.WriteLine("La introduccion de datos de la oferta se ha realizado correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "IntroduccirDatos.png", "La introduccion de datos de la oferta no se ha realizado correctamente");
                driver.Quit();
            }
        }

        /// <summary>
        /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de capacidad.
        /// </summary>
        public void Tipo_de_oferta_Cambiodecapacidad()
        {
            try
            {
                BuscarOfertaEnVista("Prueba-Auto_NO_borrarCRM-EOF0004");
                AbrirOfertaEnVista("Prueba-Auto_NO_borrarCRM-EOF0004");
                Thread.Sleep(3000);

                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);

                SelectElement drop = new SelectElement(Utils.SearchWebElement("Oferta.selectOfertaType"));

                drop.SelectByText("Cambio de capacidad (Upgrade/Downgrade)");

                Console.WriteLine("Tipo de oferta cambio de capacidad OK");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Tipo de oferta cambio de capacidad.png", "Tipo de oferta cambio de capacidad KO");
                driver.Quit();
            }
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de precio.
        /// </summary>
        public void Tipo_de_oferta_Cambiodeprecio()
        {
            try
            {
                Thread.Sleep(3000);

                SelectElement drop = new SelectElement(Utils.SearchWebElement("Oferta.selectOfertaType"));

                drop.SelectByText("Cambio de precio/Renovación");

                Console.WriteLine("Tipo de oferta cambio de precio OK");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Tipo_de_oferta_cambiodeprecio.png", "Tipo de oferta cambio de precio KO");
                driver.Quit();
            }
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de solución.
        /// </summary>
        public void Tipo_de_oferta_Cambiodesolucion()
        {
            try
            {
                Thread.Sleep(3000);

                SelectElement drop = new SelectElement(Utils.SearchWebElement("Oferta.selectOfertaType"));

                drop.SelectByText("Cambio de solución técnica (Tecnología)");

                Console.WriteLine("Tipo de oferta cambio de solucion OK");
            }
           catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Tipo de oferta cambio de resolucion.png", "Tipo de oferta cambio de solucion KO");
                driver.Quit();
            }
        }

        /// <summary>
        /// /// Método para abrir una Oferta y cambiar el Tipo de oferta a Cambio de dirección.
        /// </summary>
        public void Tipo_de_oferta_Cambiodedireccion()
        {
            try
            {
                Thread.Sleep(3000);

                SelectElement drop = new SelectElement(Utils.SearchWebElement("Oferta.selectOfertaType"));

                drop.SelectByText("Cambio de dirección (Migración)");

                Console.WriteLine("Tipo de oferta cambio de direccion OK");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Tipo de oferta cambio de direccion.png", "Tipo de oferta cambio de direccion KO");
                driver.Quit();
            }
        }

        /// <summary>
        /// Método para reestablecer datos de la Oferta de la prueba CRM_COF0003 de Editar Oferta
        /// </summary>
        public void Restablecimiento_CRM_COF0003()
        {
            try
            {
                AbrirOfertaEnVista("Automatica_MODPrueba_auto_NO_borrar_MODIFICADA");
                Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);
                Thread.Sleep(3000);

                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys("Automatica_MOD");
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);

                Utils.SearchWebElement("Oferta.calendarFechadepresentacion").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.calendarFechadepresentacion").SendKeys(Keys.Delete);

                Utils.SearchWebElement("Oferta.inputReferenceOferta").SendKeys(Keys.PageDown);
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.labelPermutaDefaultReset").SendKeys(Keys.Enter); //Toggle Switch
                Thread.Sleep(3000);

                // Borrado contacto y referencia

                Utils.SearchWebElement("Oferta.inputContactId").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputContactId").SendKeys(Keys.Delete);
                Thread.Sleep(2000);


                Utils.SearchWebElement("Oferta.inputReferenceOferta").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputReferenceOferta").SendKeys(Keys.Delete);
                Thread.Sleep(2000);



                Utils.SearchWebElement("Oferta.inputGVAL").Click();
                Utils.SearchWebElement("Oferta.inputGVAL").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputGVAL").SendKeys(Keys.Delete);

                Utils.SearchWebElement("Oferta.inputCampoDescripcion").Click();
                Utils.SearchWebElement("Oferta.inputCampoDescripcion").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputCampoDescripcion").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Oferta.inputGVAL").Clear();
                Thread.Sleep(3000);

                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.ButtonGuardarYcerrar").Click();//Guarda y cierra

                Console.WriteLine("El reestablecimiento de la prueba se ha realizado correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Reestablecimiento.png", "El reestablecimiento de la prueba no se ha realizado correctamente");
                driver.Quit();
            }
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
            Utils.SearchWebElement("Oferta.buttonAgregarProducto").Click();//pulsamos sobre agregar producto
            if (utils.EncontrarElemento(By.XPath("//button[contains(@data-id, 'quickCreateSaveAndCloseBtn')]")))
            {
                Thread.Sleep(2000);
                Utils.SearchWebElement("Producto.ButtonGuardar").Click();//guardamos
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.buttonGuardarYcerrarProd").Click();//guardamos y cerramos
            }

        }
      
        /// <param name="opcion"></param>
        /// <param name="razonOferta"></param>
        /// <param name="motivoCierre"></param>
        public void CerrarOfertaActual(string opcion, string razonOferta, string motivoCierre)
        {
            try
            {
                // Click en Cerrar Oferta
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.buttonCerrar").Click();
                Thread.Sleep(2000);

                // Cambiar al frame de Cierre de Ofertas
                driver.SwitchTo().Frame(Utils.SearchWebElement("Oferta.frameCerrarOferta"));

                if (!razonOferta.Equals(""))
                {
                    SelectElement drop = new SelectElement(Utils.SearchWebElement("Oferta.selectRazonEstado"));
                    drop.SelectByText(razonOferta);
                }

                if (!motivoCierre.Equals(""))
                {
                    SelectElement drop = new SelectElement(Utils.SearchWebElement("Oferta.selectMotivoCierre"));
                    drop.SelectByText(motivoCierre);
                }

                if (opcion.Equals("Aceptar"))
                {
                    // Cancelación realizada
                    Utils.SearchWebElement("Oferta.buttonConfirmarCierre").Click();
                    Thread.Sleep(3000);
                }
                else
                {
                    // Cancelación anulada
                    Utils.SearchWebElement("Oferta.buttonCancelarCierre").Click();
                    Thread.Sleep(3000);
                    Console.WriteLine("La oferta actual se cierra correctamente");
                }
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "CerrarOfertaActual.png", "La oferta actual no se cierra correctamente");
                driver.Quit();
            }
            
        }

        public void AccederOfertaestado_Adjudicada()
        {
            try
            {
                driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Click();//seleccionamos una oferta ganada y pulsamos sobre el ckeck
                Thread.Sleep(3000);
                Console.WriteLine("Se accede correctamente a un oferta en estado adjudicada");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Acceder oferta estado adjudicada.png", "No se accede correctamente a una oferta en estado adjudicada");
                driver.Quit();
            }
        }

        public void FiltrarPorIDRevision(string idRevision)
        {
            try
            {
                Utils.SearchWebElement("Oferta.gridFilterIdRevision").Click();

                Utils.SearchWebElement("Oferta.gridFilterBuscarPor").Click();
                Utils.SearchWebElement("Oferta.gridFilterBuscarPorInput").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.gridFilterBuscarPorInput").SendKeys(Keys.Delete);

                Utils.SearchWebElement("Oferta.gridFilterBuscarPorInput").SendKeys(idRevision);
                Thread.Sleep(2000);

                Utils.SearchWebElement("Oferta.gridFilterBuscarPorAceptarButton").Click();
                Thread.Sleep(2000);
                Console.WriteLine("El filtrado por IDRevision se realiza correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "FiltrarPorIDRevision.png", "El filtrado por IDRevision no se realiza correctamente");
                driver.Quit();
            }
        }

        public void SeleccionarTodasOfertaGrid()
        {
            try
            {
                Utils.SearchWebElement("Oferta.gridSelectAll").Click();
                Thread.Sleep(2000);
                Console.WriteLine("Seleccionar todas ofertas del grid funciona correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "SeleccionarTodasOfertaGrid.png", "Seleccionar todas ofertas del grid no funciona correctamente");
                driver.Quit();
            }
        }

        public void Eliminar_BarraMenu()
        {
            try
            {
                Utils.SearchWebElement("Oferta.buttonEliminar").Click();//pulsamos sobre eliminar de la barra superior del menu
                Console.WriteLine("Se pulsa correctamente con la barra de menu");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Eliminar_barramenu.png", "No se pulsa correctamente sobre la opcion eliminar barra de menu");
                driver.Quit();
            }
        }

        public void Cancelar()
        {
            try
            {
                Utils.SearchWebElement("Producto.buttonCancelar").Click();//cancelar del pop up
                Console.WriteLine("Se pulsa cancelar correctamente");
            }
           catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Cancelar.png", "No se pulsa cancelar correctamente");
                driver.Quit();
            }
        }

        public void Seleccionofertarazonadjudicada()
        {
            try
            {
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.labelOfertaestadoGanada").Click();//seleccionamos una oferta ganada y pulsamos sobre el ckeck
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.buttonEditar").Click();//pulsamos sobre editar
                Thread.Sleep(3000);
                Console.WriteLine("se selecciona una oferta del listado y se edita correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Seleccion de oferta razon adjudicada.png", "no se selecciona una oferta del listado y no se edita correctamente");
                driver.Quit();
            }
        }

        public void Eliminar_Popup()//pulsamos sobre el eliminar del popup
        {
            try
            {
                Thread.Sleep(2000);
                Utils.SearchWebElement("Producto.buttonEliminarProductodeOfertaConfirm").Click();
                Console.WriteLine("Se pulsa eliminar del pop up correctamene");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Eliminar PopUp.png", "No se pulsa eliminar del pop up correctamene");
                driver.Quit();
            }

        }

        public void PresentarOferta()
        {
            try
            {
                Utils.SearchWebElement("Oferta.buttonPresentOferta").Click();
                Console.WriteLine("Se pulsa presentar oferta correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "PresentarOferta.png", "No se pulsa presentar oferta correctamente");
                driver.Quit();
            }
        }

        public void Filtro_buscarEnestaVista(String busqueda)
        {
            try
            {
                Utils.SearchWebElement("Oferta.inputFilter").SendKeys(busqueda);//buscamos una oferta en el filtro
                Thread.Sleep(1000);
                Utils.SearchWebElement("Oferta.buttonLupaFiltro").Click();
                Thread.Sleep(3000);
                driver.FindElement(By.LinkText(busqueda)).Click();//click en la oferta
                Thread.Sleep(3000);

                Console.WriteLine("Se ha filtrado correctamente la oferta");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Filtro_buscarEnestaVista.png", "No se ha podido filtrar correctamente la oferta");
                driver.Quit();
            }
        }

        public void ReestablecerDatosCRM_EOF0004()
        {
            try
            {
                //reestablece datos CRM-EOF0004
                driver.FindElement(By.LinkText("Prueba-Auto_NO_borrarCRM-EOF0004")).Click();
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.PageDown);

                SelectElement drop = new SelectElement(Utils.SearchWebElement("Oferta.selectOfertaType"));

                drop.SelectByText("Nuevo servicio");

                GuardarYCerrarOferta();

                Console.WriteLine("reestablecimiento de datos EOF0004 correcto");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Reestablecimiento datos EOF0004.png", "reestablecimiento de datos EOF0004 incorrecto");
                driver.Quit();
            }
        }
    }
}