using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Lyntia.Utilities;
using NUnit.Framework;

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

                    TestContext.WriteLine("Se accede correctamente a la sección " + seccion);

                }
                catch (Exception e)
                {
                    CommonActions.CapturadorExcepcion(e, "AccesoSeccionOfertas.png", "No se pudo acceder a la sección " + seccion + ".");
                    throw e;
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

                TestContext.WriteLine("Nueva Oferta creada correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AccesoNuevaOferta.png", "No se pudo crear una nueva Oferta.");
                throw e;
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

                TestContext.WriteLine("Se accede a la primera Oferta del grid.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "PrimeraOfertaGrid.png", "No se pudo acceder a la primera Oferta del grid.");
                throw e;
            }
        }

        /// <summary>
        /// Método para acceder a la pestaña de Fechas de Oferta
        /// </summary>
        public void AccesoFechasOferta()
        {
            try
            {
                Utils.SearchWebElement("Oferta.datesSection").Click();

                TestContext.WriteLine("Se accede correctamente a la pestaña Fechas de la Oferta.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AccesoFechas.png", "No se pudo acceder a la pestaña Fechas de la OFerta.");
                throw e;
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

                TestContext.WriteLine("La Oferta se guarda correctamente.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "GuardarOferta.png", "No se pudo guardar la Oferta.");
                throw e;
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

                TestContext.WriteLine("La Oferta se guarda y cierra correctamente.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "GuardarCerrarOferta.png", "No se pudo guardar y cerrar la Oferta.");
                throw e;
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
                if (!nombre.Equals(""))
                {
                    Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                    Utils.SearchWebElement("Oferta.inputNameOferta").Clear();
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);

                    // Rellenar Nombre de Oferta
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

                    if (!nombre.Equals("") && Utils.SearchWebElement("Oferta.inputNameOferta").Text.Contains("---"))
                    {
                        Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                        Utils.SearchWebElement("Oferta.inputNameOferta").Clear();
                        Thread.Sleep(2000);
                        Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                        Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);

                        // Rellenar Cliente de Oferta
                        Thread.Sleep(2000);
                        Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(nombre);
                        Thread.Sleep(1000);
                    }
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

                TestContext.WriteLine("Se han introducido correctamente los campos de la Oferta: " + nombre + ", " + cliente + ", " + tipoOferta + ", " + kam + ".");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AddDatosOferta.png", "No se introducen los datos de la Oferta: " + nombre + ", " + cliente + ", " + tipoOferta + ", " + kam + ".");
                throw e;
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
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Oferta.deleteButtonOferta"))));
            if (utils.EncontrarElemento(By.XPath(Utils.GetIdentifier("Oferta.deleteButtonOferta"))))
            {
                try
                {
                    Utils.SearchWebElement("Oferta.deleteButtonOferta").Click();

                    // Confirmar Borrado
                    if (opcion.Equals("Eliminar"))
                    {
                        Utils.SearchWebElement("Oferta.confirmDeleteOferta").Click();
                        Thread.Sleep(4000);
                        TestContext.WriteLine("Se elimina la Oferta correctamente.");
                    }
                    else
                    {
                        Utils.SearchWebElement("Oferta.cancelDeleteOferta").Click();
                        Thread.Sleep(4000);
                        TestContext.WriteLine("Se cancela el proceso de eliminación.");
                    }
                }
                catch (Exception e)
                {
                    CommonActions.CapturadorExcepcion(e, "EliminarOferta.png", "No se puede completar el proceso de eliminación (" + opcion + ").");
                    throw e;
                }
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
                TestContext.WriteLine("Se busca la Oferta por parámetro: " + parametroBusqueda + ".");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "BuscaOferta.png", "El proceso de búsqueda de Oferta por parámetro " + parametroBusqueda + " ha fallado.");
                throw e;
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
                TestContext.WriteLine("Se abre la Oferta " + nombreOferta + " se abre correctamente.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AbrirOferta.png", "No se abre la Oferta " + nombreOferta + ".");
                throw e;
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
                TestContext.WriteLine("Se selecciona la Oferta del grid.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "SeleccionOferta.png", "Se selecciona la Oferta del grid.");
                throw e;
            }
        }

        /// <summary>
        /// Método para inserción de datos de una Oferta
        /// </summary>
        public void IntroduccirDatos()
        {
            try
            {
                Utils.SearchWebElement("Oferta.inputContactId").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Oferta.inputContactId").SendKeys("ddd ddd");
                Thread.Sleep(1000);

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
                TestContext.WriteLine("Se modifican los datos de la prueba CRM-EOF0003.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ModificacionOfertaEOF0003.png", "No se modifican los datos de la prueba CRM-EOF0003.");
                throw e;
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
                TestContext.WriteLine("Se modifica el tipo de Oferta de la prueba CRM-EOF0004: Cambio de capacidad (Upgrade/Downgrade).");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "TipoOfertaCambioCapacidadEOF0004.png", "No se modifica el tipo de Oferta de la prueba CRM-EOF0004: Cambio de capacidad (Upgrade/Downgrade).");
                throw e;
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
                TestContext.WriteLine("Se modifica el tipo de Oferta de la prueba CRM-EOF0004: Cambio de precio/Renovación.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "TipoOfertaCambioPrecioEOF0004.png", "No se modifica el tipo de Oferta de la prueba CRM-EOF0004: Cambio de precio/Renovación");
                throw e;
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
                TestContext.WriteLine("Se modifica el tipo de Oferta de la prueba CRM-EOF0004: Cambio de solución técnica (Tecnología).");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "TipoOfertaCambioSolucionEOF0004.png", "No se modifica el tipo de Oferta de la prueba CRM-EOF0004: Cambio de solución técnica (Tecnología).");
                throw e;
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
                TestContext.WriteLine("Se modifica el tipo de Oferta de la prueba CRM-EOF0004: Cambio de dirección (Migración).");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "TipoOfertaCambioPrecioEOF0004.png", "No se modifica el tipo de Oferta de la prueba CRM-EOF0004: Cambio de dirección (Migración).");
                throw e;
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
                Utils.SearchWebElement("Oferta.saveOferta").Click();
                //Utils.SearchWebElement("Oferta.ButtonGuardarYcerrar").Click(); //Guarda y cierra
                TestContext.WriteLine("Se reestablecen los datos originales de la prueba CRM-EOF0004.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ReestablecimientoEOF0003.png", "No se reestablecen los datos originales de la prueba CRM-EOF0004.");
                throw e;
            }
        }

        /// <summary>
        /// Método para Eliminar datos de la Oferta.
        /// </summary>
        public void Eliminar_campos_obligatorios()
        {
            try
            {
               
                Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);
                Thread.Sleep(3000);

                //hay que añadir divisa y lista de precios kam y cliente
                
                Utils.SearchWebElement("Oferta.kamResponsable").Click();
                Thread.Sleep(1000);

                Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(Keys.Delete);

                TestContext.WriteLine("Se eliminan los campos obligatorios de la prueba.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "EliminarCampos.png", "No se eliminan los campos obligatorios de la prueba.");
                throw e;
            }

        }

        /// <summary>
        /// Método para modificar datos de la Oferta.
        /// </summary>
        public void Modificar_campos_obligatorios(String Modcampo)
        {
            try
            {

                Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);
                Thread.Sleep(3000);

                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Modcampo);

                //hay que añadir divisa y lista de precios kam y cliente

                TestContext.WriteLine("Se modifican los campos obligatorios de la prueba.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "EliminarCampos.png", "No se modifican los campos obligatorios de la prueba.");
                throw e;
            }

        }

        /// <summary>
        /// Método Cerrar Oferta
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="razonOferta"></param>
        /// <param name="motivoCierre"></param>
        public void CerrarOfertaActual(string opcion, string razonOferta, string motivoCierre, String fechaCierre)
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

                if (!fechaCierre.Equals(""))
                {
                    Utils.SearchWebElement("Oferta.inputFechaCierre").SendKeys(fechaCierre);
                }

                if (opcion.Equals("Aceptar"))
                {
                    // Cierre realizado
                    Utils.SearchWebElement("Oferta.buttonConfirmarCierre").Click();
                    Thread.Sleep(3000);
                    TestContext.WriteLine("Se completa correctamente el proceso de cierre de Oferta.");
                }
                else
                {
                    // Cierre anulado
                    Utils.SearchWebElement("Oferta.buttonCancelarCierre").Click();
                    Thread.Sleep(3000);
                    TestContext.WriteLine("Se cancela correctamente el proceso de cierre de Oferta.");
                }
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "CierreOferta.png", "No se completa el proceso de cierre de Oferta (" + opcion + ").");
                throw e;
            }
        }

        /// <summary>
        /// Método para acceder a Oferta Adjudicada
        /// </summary>
        public void AccederOfertaestado_Adjudicada()
        {
            try
            {
                BuscarOfertaEnVista("test");
                driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Click();//seleccionamos una oferta ganada y pulsamos sobre el ckeck
                Thread.Sleep(3000);
                TestContext.WriteLine("Se accede a una Oferta Adjudicada.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AccesoOfertaAdjudicada.png", "No se accede a una Oferta Adjudicada.");
                throw e;
            }
        }

        /// <summary>
        /// Método para filtrar Oferta por ID de Revisión
        /// </summary>
        /// <param name="idRevision"></param>
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
                TestContext.WriteLine("Se filtra correctamente la Oferta por ID de revisión: " + idRevision + ".");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "FiltradoIdRevisionOferta.png", "No se filtra la Oferta por ID de revisión: " + idRevision + ".");
                throw e;
            }
        }

        /// <summary>
        /// Método para seleccionar todas las Ofertas en vista del grid
        /// </summary>
        public void SeleccionarTodasOfertaGrid()
        {
            try
            {
                Utils.SearchWebElement("Oferta.gridSelectAll").Click();
                Thread.Sleep(2000);
                TestContext.WriteLine("Se seleccionan todas las ofertas en vista del grid.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "SeleccionTodasOfertas.png", "No se seleccionan todas las ofertas en vista del grid.");
                throw e;
            }
        }

        /// <summary>
        /// Método para pulsar Eliminar en la barra de Ofertas
        /// </summary>
        public void Eliminar_BarraMenu()
        {
            try
            {
                Utils.SearchWebElement("Oferta.buttonEliminar").Click(); //pulsamos sobre eliminar de la barra superior del menu
                TestContext.WriteLine("Se pulsa correctamente Eliminar.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "EliminarBarraMenuOfertas.png", "No se pulsa Eliminar.");
                throw e;
            }
        }

        /// <summary>
        /// Método para cancelar popup de eliminación
        /// </summary>
        public void Cancelar()
        {
            try
            {
                Utils.SearchWebElement("Producto.buttonCancelar").Click(); //cancelar del pop up
                TestContext.WriteLine("Se cancela el popup de error al intentar eliminar.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "PopupEliminarOfertas.png", "No se cancelar el popup de eliminar Oferta.");
                throw e;
            }
        }

        /// <summary>
        /// Método para acceder a Oferta Adjudicada
        /// </summary>
        public void Seleccionofertarazonadjudicada()
        {
            try
            {
                BuscarOfertaEnVista("test");
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.labelOfertaestadoGanada").Click(); //seleccionamos una oferta ganada y pulsamos sobre el ckeck
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.buttonEditar").Click(); //pulsamos sobre editar
                Thread.Sleep(3000);
                TestContext.WriteLine("Se accede a Oferta adjudicada.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "EditarOfertaAdjudicada.png", "No se accede a Oferta adjudicada.");
                throw e;
            }
        }

        /// <summary>
        /// Método para confirmar eliminación desde popup
        /// </summary>
        public void Eliminar_Popup()
        {
            try
            {
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.confirmDeleteOferta").Click();
                TestContext.WriteLine("Se confirma el borrado desde el popup.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ConfirmarPopupEliminarOferta.png", "No se confirma el borrado desde el popup.");
                throw e;
            }
        }

        /// <summary>
        /// Método para presentar Oferta
        /// </summary>
        public void PresentarOferta()
        {
            try
            {
                Utils.SearchWebElement("Oferta.buttonPresentOferta").Click();
                TestContext.WriteLine("Se accede a la ventana de Presentar Oferta.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "PresentarOferta.png", "No se accede a la ventana de Presentar Oferta.");
                throw e;
            }
        }

        /// <summary>
        /// Método para buscar por un filtro de búsqueda
        /// </summary>
        /// <param name="busqueda"></param>
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

                TestContext.WriteLine("Se ha filtrado correctamente la oferta por: " + busqueda + ".");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "FiltroBuscarEnestaVista.png", "No se ha podido filtrar correctamente la oferta por: " + busqueda + ".");
                throw e;
            }
        }

        /// <summary>
        /// Reestablecimiento de datos de la prueba CRM-EOF0004
        /// </summary>
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
                TestContext.WriteLine("Se reestablecen los datos de la prueba EOF0004.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ReestablecimientoEOF0004.png", "No se reestablecen los datos de la prueba EOF0004.");
                throw e;
            }
        }   
        
        /// <summary>
        /// Método para presentar Oferta
        /// </summary>
        public void Adjudicar_Oferta()
        {
            try
            {
                Utils.SearchWebElement("Oferta.buttonAdjudicarOferta").Click();
                TestContext.WriteLine("Se accede a la ventana de Adjudicar Oferta.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AdjudicarOferta.png", "No se accede a la ventana de Adjudicar Oferta.");
                throw e;
            }
        }
        /// <summary>
        /// Método para controlar fecha de la venta crear pedido de una oferta adjudicada
        /// </summary>
        public void VentanaCrearPedido(String Fecha)
        {
            try
            {
                Utils.SearchWebElement("Oferta.inputFechaVentanaCrearpedido").Click();
                Utils.SearchWebElement("Oferta.inputFechaVentanaCrearpedido").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputFechaVentanaCrearpedido").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Oferta.inputFechaVentanaCrearpedido").SendKeys(Fecha);

                Utils.SearchWebElement("Oferta.inputFechaVentanaCrearpedido").SendKeys("Oferta.gridFilterBuscarPorAceptarButton");
                Utils.SearchWebElement("Oferta.gridFilterBuscarPorAceptarButton").Click();
                TestContext.WriteLine("se continua con el pedido correctamente.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "VentanaCrearPedido.png", "No se continua con el pedido correctamente.");
                throw e;
            }
        }
        /// <summary>
        /// Método para actualizar desde la barra de menu superior con acciones guardar y descartar
        /// </summary>
        public void Actualizar(String accion)
        {
            try
            {
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.buttonActualizar").Click();

                if (accion.Equals("Guardar"))
                {
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Producto.buttonGuardaryContinuar").Click();
                    Thread.Sleep(4000);
                }
                else
                {
                    //cambios no guardados
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Oferta.ButtonDescartarcambios").Click();
                }
                    TestContext.WriteLine("La oferta se actualiza correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Actualizar.png", "La oferta no se ha actualizado correctamente");
                throw e;
            }
            
        }

        /// <summary>
        /// Método para actualizar desde la barra de menu superior
        /// </summary>
        public void ActualizarBarramenu()
        {
            try
            {
                
                Utils.SearchWebElement("Oferta.buttonActualizar").Click();
                Thread.Sleep(3000);
                TestContext.WriteLine("La oferta se actualiza correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ActualizarBarraMenu.png", "La oferta no se ha actualizado correctamente");
                throw e;
            }

            

        }
    }
}