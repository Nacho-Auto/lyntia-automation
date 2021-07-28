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
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(Utils.GetIdentifier("Oferta.ofertaSection"))));
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
                Thread.Sleep(7000);

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
                    Utils.SearchWebElement("Oferta.inputCustomerId").SendKeys(cliente);                                                           
                    driver.FindElement(By.XPath("//div[@aria-label = 'General']")).SendKeys(Keys.PageUp);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), '" + cliente + "')]")));                    
                    
                    
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
                    //accionesSelenium.SendKeys(Keys.PageDown);
                    //accionesSelenium.Build().Perform();

                    //Thread.Sleep(1000);
                    //Utils.SearchWebElement("Oferta.kamResponsable").Click();
                    //Thread.Sleep(1000);

                    //Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(Keys.Control + "a");
                    //Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(Keys.Delete);

                    //Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(kam);
                    //Thread.Sleep(1000);

                    //accionesSelenium.SendKeys(Keys.PageDown);
                    //accionesSelenium.Build().Perform();

                    //driver.FindElement(By.XPath("//span[contains(text(), '" + kam + "')]")).Click();
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
                    Thread.Sleep(3000);
                    Utils.SearchWebElement("Oferta.deleteButtonOferta").Click();
                    Thread.Sleep(3000);
                    // Confirmar Borrado
                    if (opcion.Equals("Eliminar"))
                    {
                        Utils.SearchWebElement("Oferta.confirmDeleteOferta").Click();
                        Thread.Sleep(4000);
                        TestContext.WriteLine("Se elimina la Oferta correctamente.");
                        Thread.Sleep(2000);
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
                Thread.Sleep(7000);
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
        /// Método empleado para una vez buscada una Oferta en el grid, abrirla(seleccion con un ckeck)
        /// </summary>
        public void SeleccionarOfertaGrid()
        {
            try
            {
                Utils.SearchWebElement("Oferta.selectOfertaGrid").Click();
                Thread.Sleep(8000);
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
                Thread.Sleep(7000);
                //driver.FindElement(By.XPath("//button[contains(@aria-label,'julio 31, 2021')]")).Click();
                Utils.SearchWebElement("Oferta.calendarDateRandomDay").Click();//seleccionamos fecha del calendario
                Thread.Sleep(2000);
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
        public void Eliminar_campos_obligatorios(int vez)
        {
            try
            {
                // Nombre de la oferta
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                Thread.Sleep(3000);

                // Cliente
                if(vez == 1)
                {
                    accionesSelenium.SendKeys(Keys.Tab).Perform();
                    accionesSelenium.SendKeys(Keys.Tab).Perform();
                }
                else
                {
                    accionesSelenium.SendKeys(Keys.Tab).Perform();
                }

                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//button[contains(@data-id,'customerid_selected_tag_delete')]")).Click();
                Thread.Sleep(2000);

                // KAM
                //driver.FindElement(By.XPath("//input[contains(@aria-label, 'KAM que oferta, Búsqueda')]")).Click();
                //driver.FindElement(By.XPath("//div[contains(@data-id,'new_kamresponsableid.fieldControl-LookupResultsDropdown_new_kamresponsableid_selected_tag')]")).Click();

                //driver.FindElement(By.XPath("//ul[contains(@data-id,'new_kamresponsableid_SelectedRecordList')]")).Click();
                //driver.FindElement(By.XPath("//button[contains(@data-id,'kamresponsableid_selected_tag_delete')]")).Click();
                Thread.Sleep(3000);

                // Divisa
                driver.FindElement(By.XPath("//ul[contains(@data-id,'transactioncurrencyid_SelectedRecordList')]")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//span[contains(@data-id, 'transactioncurrencyid_microsoftIcon_cancelButton')]")).Click();

                //TODO: capturar boton aceptar
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.buttonAceptarVentanaEmergente").Click();

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
        public void Modificar_campos_obligatorios(String Modcampo ,String name, String tipoOferta, String KAM, String divisa, String listadeprecios)
        {
            try
            {
                // Modificar el nombre de la oferta
                Utils.SearchWebElement("Oferta.inputNameOferta").Click();
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Keys.Delete);
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.inputNameOferta").SendKeys(Modcampo);
                Thread.Sleep(3000);

                // Modificar el cliente
                Utils.SearchWebElement("Oferta.inputCustomerId").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Oferta.inputCustomerId").SendKeys(name);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//span[contains(text(), '" + name + "')]")).Click();
                Thread.Sleep(2000);

                // Modificar tipo de oferta
                Utils.SearchWebElement("Oferta.selectOfertaType").Click();
                accionesSelenium.SendKeys(Keys.PageDown);
                accionesSelenium.Build().Perform();
                Thread.Sleep(2000);

                // Modificar Kan
                //Utils.SearchWebElement("Oferta.kamResponsable").Click();
                //Thread.Sleep(1000);
                //Utils.SearchWebElement("Oferta.kamResponsable").SendKeys(KAM);
                //Thread.Sleep(1000);
                //driver.FindElement(By.XPath("//span[contains(text(), '" + KAM + "')]")).Click();
                //Thread.Sleep(2000);

                // Modificar divisa
                Utils.SearchWebElement("Oferta.inputDivisa").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Oferta.inputDivisa").SendKeys(divisa);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//span[contains(text(), '" + divisa + "')]")).Click();
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.buttonAceptarVentanaEmergente").Click();
                Thread.Sleep(2000);

                // Modificar la lista de precios
                Utils.SearchWebElement("Oferta.inputListadeprecios").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Oferta.inputListadeprecios").SendKeys(listadeprecios);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//span[contains(text(), '" + listadeprecios + "')]")).Click();
                Thread.Sleep(2000);




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
                Thread.Sleep(4000);
                Utils.SearchWebElement("Oferta.buttonCerrar").Click();
                Thread.Sleep(4000);

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
                    if (fechaCierre.Equals("dd/mm/aaaa"))
                    {
                        Utils.SearchWebElement("Oferta.inputFechaCierre").SendKeys("22/09/2021");
                        Utils.SearchWebElement("Oferta.inputFechaCierre").SendKeys(Keys.Control + "a");
                        Utils.SearchWebElement("Oferta.inputFechaCierre").SendKeys(Keys.Delete);
                        Utils.SearchWebElement("Oferta.inputFechaCierre").SendKeys(fechaCierre);
                    }
                    else
                    {
                        Utils.SearchWebElement("Oferta.inputFechaCierre").SendKeys(fechaCierre);
                    } 
                }

                if (opcion.Equals("Aceptar"))
                {
                    // Cierre realizado
                    Utils.SearchWebElement("Oferta.buttonConfirmarCierre").Click();
                    Thread.Sleep(10000);
                    TestContext.WriteLine("Se completa correctamente el proceso de cierre de Oferta.");
                }
                else
                {
                    // Cierre anulado
                    Utils.SearchWebElement("Oferta.buttonCancelarCierre").Click();
                    Thread.Sleep(8000);
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
                driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Click();//seleccionamos una oferta ganada y pulsamos sobre el check
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

        public void PresentarOferta(string fechaPresentacion)
        {
            try
            {
                Utils.SearchWebElement("Oferta.fechaPresentacion").Click();
                Utils.SearchWebElement("Oferta.fechaPresentacion").Click();                
                Utils.SearchWebElement("Oferta.fechaPresentacion").SendKeys(fechaPresentacion);
                

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

                
                Utils.SearchWebElement("Oferta.buttonAceptarVentanaEmergente").Click();
                TestContext.WriteLine("se continua con el pedido correctamente.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "VentanaCrearPedido.png", "No se continua con el pedido correctamente.");
                throw e;
            }
        }

        /// <summary>
        /// Método para indicar en la fecha el dia siguiente al actual
        /// </summary>
        public void VentanaCrearPedidofechaposterior()
        {
            try
            {
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.inputFechaVentanaCrearpedido").Click();
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.ButtonNextday").Click();
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.buttonAceptarVentanaEmergente").Click();
                Thread.Sleep(28000);

                
                Utils.SearchWebElement("Oferta.saveOferta").Click();

                TestContext.WriteLine("se continua con el pedido correctamente.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "VentanaCrearPedido.png", "No se continua con el pedido correctamente.");
                throw e;
            }
        }

        /// <summary>
        /// Método para acceder a proyectos del Grid
        /// </summary>
        public void AccesoServiciosGrid()
        {
            try
            {
                Utils.SearchWebElement("Oferta.labelServicioscontratadosGrid").Click();
                Thread.Sleep(2000);

                TestContext.WriteLine("Se accede correctamente a la opcion de Servicios contratados");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AccesoProyectosGrid.png", "No se accede correctamente a la opcion de Servicios contratados");
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

        /// <summary>
        /// Método para acceder a servicios contratados
        /// </summary>
        public void Acceso_Servicios_contratados()
        {
            try
            {
                Utils.SearchWebElement("Oferta.GridServiciosContratados").Click();
                Thread.Sleep(3000);

                TestContext.WriteLine("Se accede correctamente a Servicios contratados");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Acceso servicios contratados.png", "No se accede a servicios contratados");
                throw e;
            }
        }

        /// <summary>
        /// Método para acceder a la primera linea de los productos
        /// </summary>
        public void Acceder_line1()
        {
            //Pulsar sobre el primer registro y editar 

            Thread.Sleep(7000);
            Utils.SearchWebElement("Producto.SelectLine1").Click();
            Utils.SearchWebElement("Oferta.buttonEditOferta").Click();
            Thread.Sleep(4000);
        }

        /// <summary>
        /// Método para acceder a la segunda linea de los productos
        /// </summary>
        public void Acceder_line2()
        {
            //Pulsar sobre el segundo registro y editar 

            Thread.Sleep(10000);
            Utils.SearchWebElement("Producto.SelectLine2").Click();
            Utils.SearchWebElement("Oferta.buttonEditOferta").Click();
            Thread.Sleep(4000);
        }

        /// <summary>
        /// Método para acceder a servicios contratados
        /// </summary>
        public void CamposObligatoriosProductoCC(String ProductoExistente2)

        {

            // General
            
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingNOMBREANEXO").SendKeys("prueba anexo");
            Thread.Sleep(2000);

            /// <summary>
            /// Método para cumplimentar datos obligatorios
            /// </summary>

            ////Pestaña Caracteristicas (datos obligatorios)

            Utils.SearchWebElement("Producto.PestañaCaracteristicas").Click();
            Thread.Sleep(2000);

            // Seleccion de Red
            Utils.SearchWebElement("Producto.PestañaCaracteristicasRED").Click();//swich para ON/OFF red

            // Seleccion de Operador ultima milla
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.PestañaCaracteristicasOPERADOR"))));
            Thread.Sleep(3000);
            Utils.SearchWebElement("Producto.PestañaCaracteristicasOPERADOR").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaCaracteristicasOPERADOR").SendKeys("BRODYNT");
            Utils.SearchWebElement("Producto.PestañaCaracteristicasOPERADOR").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaCaracteristicasOPERADOR").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaCaracteristicasOPERADOR").SendKeys("BRODYNT");
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//span[contains(text(), 'BRODYNT')]")).Click();
            Thread.Sleep(3000);
            
            // Seleccion de Ambito
            SelectElement drop = new SelectElement(Utils.SearchWebElement("Producto.PestañaCaracteristicasAMBITO"));
            drop.SelectByText("Urbano");
            Thread.Sleep(2000);

            //Pestaña Direcciones y Coordenadas (datos obligatorios)
            /// </summary>
            /// Sitio origen, destino, Direccion origen, destino
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadas").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIORIGEN").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINO").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONORIGEN").SendKeys("Galicia");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONDESTINO").SendKeys("Galicia");
            Thread.Sleep(2000);


            //Pestaña Jira (datos obligatorios)
            /// </summary>
            /// Capex fibra, equipo, contacto, fecha compromiso y detalle
            Utils.SearchWebElement("Producto.PestañaJira").Click();
            Thread.Sleep(2000);

            // Capex fibra y equipo
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys("456");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys("458");        
            Thread.Sleep(2000);

            // Detalle de servicio y fecha de compromiso
            Utils.SearchWebElement("Producto.PestañaJiraDETALLESERVICIO").SendKeys("Pruebas");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaJiraFECHACOMPROMISO").SendKeys("28/02/2022");
            Thread.Sleep(2000);

            // Contacto
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").Click();
            //Thread.Sleep(1000);
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys("Jose Antonio Garcia Mendez");
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys(Keys.Control + "a");
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys(Keys.Delete);
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys("Jose Antonio Garcia Mendez");
            //Thread.Sleep(2000);
            //driver.FindElement(By.XPath("//span[contains(text(), 'Jose Antonio Garcia Mendez')]")).Click();
            //Thread.Sleep(3000);

            //Pestaña Billing (datos obligatorios)
            /// </summary>
            /// Contrato marco, Actualizacion precio, periodicidad, UTPRX, codigo tarea, sociedad de facturacion, limite ipc
            Utils.SearchWebElement("Producto.PestañaContratosYbilling").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaContratosYbillingUTPRX").SendKeys("45");
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys("prueba");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys("prueba");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(), 'prueba')]")).Click();
            Thread.Sleep(3000);
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTAREA").SendKeys("3");
            Thread.Sleep(2000);

            // Actualizacion de precio
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys("IPC");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys("IPC");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(), 'IPC')]")).Click();
            Thread.Sleep(3000);

            //Limite IPC
            Utils.SearchWebElement("Producto.PestañaContratosYbillingLIMITEIPC").SendKeys("10");
            

            // Periodicidad
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys("Anual");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys("Anual");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(), 'Anual')]")).Click();
            Thread.Sleep(3000);

            //Sociedad facturacion
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").Click();
            //Thread.Sleep(1000);
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys("España");
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys(Keys.Control + "a");
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys(Keys.Delete);
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys("España");
            //Thread.Sleep(2000);
            //driver.FindElement(By.XPath("//span[contains(text(), 'España')]")).Click();
            //Thread.Sleep(3000);

            Utils.SearchWebElement("Oferta.saveAndCloseOferta").Click();
            Thread.Sleep(17000);
        }

        /// <summary>
        /// Método para acceder a servicios contratados
        /// </summary>
        public void CamposObligatoriosProductoFIBRA(String opcionModalidad)
        {

            // General

            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingNOMBREANEXO").SendKeys("prueba anexo tipo fibra");
            Thread.Sleep(2000);

            // Seleccion IRU/LEASE
            if (opcionModalidad.Equals("IRU"))
            {
                // Seleccion de IRU
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.PestañanaGeneralIRU"))));
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys("BRODYNT");
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys("Normal");
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//span[contains(text(), 'Normal')]")).Click();
                Thread.Sleep(3000);
            }
            else
            {
                Utils.SearchWebElement("Producto.PestañaCaracteristicas");
            }

           

            /// <summary>
            /// Método para cumplimentar datos obligatorios
            /// </summary>

            ////Pestaña Caracteristicas (datos obligatorios)

            Utils.SearchWebElement("Producto.PestañaCaracteristicas").Click();
            Thread.Sleep(2000);

            // Seleccion de Red
            Utils.SearchWebElement("Producto.PestañaCaracteristicasRED").Click();//swich para ON/OFF red
            Thread.Sleep(2000);
            

            // Seleccion de Ambito
            SelectElement drop = new SelectElement(Utils.SearchWebElement("Producto.PestañaCaracteristicasAMBITO"));
            drop.SelectByText("Urbano");
            Thread.Sleep(2000);

            //Pestaña Direcciones y Coordenadas (datos obligatorios)
            /// </summary>
            /// Sitio origen, destino, Direccion origen, destino
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadas").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIORIGEN").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINO").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONORIGEN").SendKeys("Galicia");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONDESTINO").SendKeys("Galicia");
            Thread.Sleep(2000);


            //Pestaña Jira (datos obligatorios)
            /// </summary>
            /// Capex fibra, equipo, contacto, fecha compromiso y detalle
            Utils.SearchWebElement("Producto.PestañaJira").Click();
            Thread.Sleep(3000);

            // Capex fibra y equipo
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys("456");
            Thread.Sleep(3000);
            

            // Detalle de servicio y fecha de compromiso
            Utils.SearchWebElement("Producto.PestañaJiraDETALLESERVICIO").SendKeys("Pruebas");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaJiraFECHACOMPROMISO").SendKeys("28/02/2022");
            Thread.Sleep(2000);

            // Contacto
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").Click();
            //Thread.Sleep(1000);
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys("Jose Antonio Garcia Mendez");
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys(Keys.Control + "a");
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys(Keys.Delete);
            //Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys("Jose Antonio Garcia Mendez");
            //Thread.Sleep(2000);
            //driver.FindElement(By.XPath("//span[contains(text(), 'Jose Antonio Garcia Mendez')]")).Click();
            //Thread.Sleep(3000);

            //Pestaña Billing (datos obligatorios)
            /// </summary>
            /// Contrato marco, Actualizacion precio, periodicidad, UTPRX, codigo tarea, sociedad de facturacion, limite ipc
            Utils.SearchWebElement("Producto.PestañaContratosYbilling").Click();
            Thread.Sleep(2000);

            // UTPRX
            Utils.SearchWebElement("Producto.PestañaContratosYbillingUTPRX").SendKeys("45");
            Thread.Sleep(2000);

           // Tarea
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTAREA").SendKeys("3");
            Thread.Sleep(2000);

            // Pais
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").Click();
            //Thread.Sleep(1000);
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys("España");
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys(Keys.Control + "a");
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys(Keys.Delete);
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys("España");
            //Thread.Sleep(2000);
            //driver.FindElement(By.XPath("//span[contains(text(), 'España')]")).Click();
            //Thread.Sleep(3000);



            Utils.SearchWebElement("Oferta.saveAndCloseOferta").Click();
            Thread.Sleep(17000);
        }

        public void CamposObligatoriosProductoFIBRABilling(String opcionModalidad)
        {

            // General

            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingNOMBREANEXO").SendKeys("prueba anexo tipo fibra");
            Thread.Sleep(2000);

            // Seleccion IRU/LEASE
            if (opcionModalidad.Equals("IRU"))
            {
                // Seleccion de IRU
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.PestañanaGeneralIRU"))));
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys("BRODYNT");
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys("Normal");
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//span[contains(text(), 'Normal')]")).Click();
                Thread.Sleep(3000);
            }
            else
            {
                Utils.SearchWebElement("Producto.PestañaCaracteristicas");
            }

            /// <summary>
            /// Método para cumplimentar datos obligatorios
            /// </summary>

            ////Pestaña Caracteristicas (datos obligatorios)

            Utils.SearchWebElement("Producto.PestañaCaracteristicas").Click();
            Thread.Sleep(2000);

            // Seleccion de Red
            Utils.SearchWebElement("Producto.PestañaCaracteristicasRED").Click();//swich para ON/OFF red
            Thread.Sleep(2000);


            // Seleccion de Ambito
            SelectElement drop = new SelectElement(Utils.SearchWebElement("Producto.PestañaCaracteristicasAMBITO"));
            drop.SelectByText("Urbano");
            Thread.Sleep(2000);

            //Pestaña Direcciones y Coordenadas (datos obligatorios)
            /// </summary>
            /// Sitio origen, destino, Direccion origen, destino
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadas").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIORIGEN").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINO").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONORIGEN").SendKeys("Galicia");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONDESTINO").SendKeys("Galicia");
            Thread.Sleep(2000);


            //Pestaña Jira (datos obligatorios)
            /// </summary>
            /// Capex fibra, equipo, contacto, fecha compromiso y detalle
            Utils.SearchWebElement("Producto.PestañaJira").Click();
            Thread.Sleep(3000);

            // Capex fibra y equipo
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys("456");
            Thread.Sleep(3000);


            // Detalle de servicio y fecha de compromiso
            Utils.SearchWebElement("Producto.PestañaJiraDETALLESERVICIO").SendKeys("Pruebas");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaJiraFECHACOMPROMISO").SendKeys("28/02/2022");
            Thread.Sleep(2000);


            //Pestaña Billing (datos obligatorios)
            /// </summary>
            /// Contrato marco, Actualizacion precio, periodicidad, UTPRX, codigo tarea, sociedad de facturacion, limite ipc
            Utils.SearchWebElement("Producto.PestañaContratosYbilling").Click();
            Thread.Sleep(2000);

            // UTPRX
            Utils.SearchWebElement("Producto.PestañaContratosYbillingUTPRX").SendKeys("45");
            Thread.Sleep(2000);

            // Tarea
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTAREA").SendKeys("3");
            Thread.Sleep(2000);

            //Pestaña Fechas
            Utils.SearchWebElement("Producto.PestañaFechas").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaFechas.inputFechaInicioFacturacion").SendKeys(Utils.getFechaActual());
            Utils.SearchWebElement("Producto.PestañaFechas.inputFechaInicioFacturacion").SendKeys(Keys.Enter);
            //Alert
            Utils.SearchWebElement("Producto.botonConfirmar").Click();
            Utils.SearchWebElement("Producto.botonOk").Click();
            
            Utils.SearchWebElement("Oferta.saveOferta").Click();
            Thread.Sleep(17000);
        }

        public void CamposObligatoriosProductoFIBRABilling(String opcionModalidad, string fechaPresentacionOferta, string tipoPeriodicidad)
        {

            // General

            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingNOMBREANEXO").SendKeys("prueba anexo tipo fibra");
            Thread.Sleep(2000);

            // Seleccion IRU/LEASE
            if (opcionModalidad.Equals("IRU"))
            {
                // Seleccion de IRU
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.PestañanaGeneralIRU"))));
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys("BRODYNT");
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.PestañanaGeneralIRU").SendKeys("Normal");
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//span[contains(text(), 'Normal')]")).Click();
                Thread.Sleep(3000);
            }
            else
            {
                Utils.SearchWebElement("Producto.PestañaCaracteristicas");
            }

            /// <summary>
            /// Método para cumplimentar datos obligatorios
            /// </summary>

            ////Pestaña Caracteristicas (datos obligatorios)

            Utils.SearchWebElement("Producto.PestañaCaracteristicas").Click();
            Thread.Sleep(2000);

            // Seleccion de Red
            Utils.SearchWebElement("Producto.PestañaCaracteristicasRED").Click();//swich para ON/OFF red
            Thread.Sleep(2000);


            // Seleccion de Ambito
            SelectElement drop = new SelectElement(Utils.SearchWebElement("Producto.PestañaCaracteristicasAMBITO"));
            drop.SelectByText("Urbano");
            Thread.Sleep(2000);

            //Pestaña Direcciones y Coordenadas (datos obligatorios)
            /// </summary>
            /// Sitio origen, destino, Direccion origen, destino
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadas").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIORIGEN").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINO").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONORIGEN").SendKeys("Galicia");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONDESTINO").SendKeys("Galicia");
            Thread.Sleep(2000);


            //Pestaña Jira (datos obligatorios)
            /// </summary>
            /// Capex fibra, equipo, contacto, fecha compromiso y detalle
            Utils.SearchWebElement("Producto.PestañaJira").Click();
            Thread.Sleep(3000);

            // Capex fibra y equipo
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").Click();
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys("456");
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys(Keys.Enter);
            Thread.Sleep(3000);


            // Detalle de servicio y fecha de compromiso
            Utils.SearchWebElement("Producto.PestañaJiraDETALLESERVICIO").SendKeys("Pruebas");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaJiraFECHACOMPROMISO").SendKeys("28/02/2022");
            Thread.Sleep(2000);


            //Pestaña Billing (datos obligatorios)
            /// </summary>
            /// Contrato marco, Actualizacion precio, periodicidad, UTPRX, codigo tarea, sociedad de facturacion, limite ipc
            Utils.SearchWebElement("Producto.PestañaContratosYbilling").Click();
            Thread.Sleep(2000);

            // UTPRX
            Utils.SearchWebElement("Producto.PestañaContratosYbillingUTPRX").SendKeys("45");
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Tipo de periodicidad')]")).Click() ;
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(., '" + tipoPeriodicidad + "')]")));
            // Seleccionar cliente del desplegable
            driver.FindElement(By.XPath("//span[contains(., '" + tipoPeriodicidad + "')]")).Click();
            Thread.Sleep(2000);

            // Tarea
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTAREA").SendKeys("3");
            Thread.Sleep(2000);

            //Pestaña Fechas
            Utils.SearchWebElement("Producto.PestañaFechas").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaFechas.inputFechaInicioFacturacion").SendKeys(fechaPresentacionOferta);
            Utils.SearchWebElement("Producto.PestañaFechas.inputFechaInicioFacturacion").SendKeys(Keys.Enter);
            //Alert
            Utils.SearchWebElement("Producto.botonConfirmar").Click();
            Utils.SearchWebElement("Producto.botonOk").Click();

            Utils.SearchWebElement("Oferta.saveOferta").Click();
            Thread.Sleep(17000);
        }
        /// <summary>
        /// Método para acceder a servicios contratados
        /// </summary>
        public void CamposObligatoriosProductoUbiredPRO(String Contacto)

        {

            // General

            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingNOMBREANEXO").SendKeys("prueba anexo UBI PRO");
            Thread.Sleep(2000);

            //Pestaña Direcciones y Coordenadas (datos obligatorios)
            /// </summary>
            /// Sitio origen, destino, Direccion origen, destino
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadas").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIORIGEN").SendKeys("Madrid");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINOEXISTENTE").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINOEXISTENTE").SendKeys("Jose Antonio Garcia Mendez");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINOEXISTENTE").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINOEXISTENTE").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINOEXISTENTE").SendKeys("Colt Madrid. Santa Leonor 55");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(), 'Colt Madrid. Santa Leonor 55')]")).Click();
            Thread.Sleep(3000);





            //Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINO").SendKeys("Madrid");

            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONORIGEN").SendKeys("Galicia");
            //Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONDESTINO").SendKeys("Galicia");
            Thread.Sleep(4000);


            //Pestaña Jira (datos obligatorios)
            /// </summary>
            /// Capex fibra, equipo, contacto, fecha compromiso y detalle
            Utils.SearchWebElement("Producto.PestañaJira").Click();
            Thread.Sleep(2000);

            // Capex fibra y equipo
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys("456");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys("458");
            Thread.Sleep(2000);

            // Detalle de servicio y fecha de compromiso
            Utils.SearchWebElement("Producto.PestañaJiraDETALLESERVICIO").SendKeys("Pruebas");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaJiraFECHACOMPROMISO").SendKeys("28/02/2022");
            Thread.Sleep(2000);

            // Contacto
            if (Contacto.Equals("contacto"))
            {
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys("Jose Antonio Garcia Mendez");
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys("Jose Antonio Garcia Mendez");
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//span[contains(text(), 'Jose Antonio Garcia Mendez')]")).Click();
                Thread.Sleep(3000);
            }
            else
            {
                Thread.Sleep(3000);
            }

            //Pestaña Billing (datos obligatorios)
            /// </summary>
            /// Contrato marco, Actualizacion precio, periodicidad, UTPRX, codigo tarea, sociedad de facturacion, 
            Utils.SearchWebElement("Producto.PestañaContratosYbilling").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaContratosYbillingUTPRX").SendKeys("45");
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys("prueba");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys("prueba");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(), 'prueba')]")).Click();
            Thread.Sleep(4000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTAREA").SendKeys("3");
            Thread.Sleep(2000);

            // Actualizacion de precio
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys("IPC");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys("IPC");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//span[contains(text(), 'IPC')]")).Click();
            Thread.Sleep(4000);

            //Limite IPC
            Utils.SearchWebElement("Producto.PestañaContratosYbillingLIMITEIPC").SendKeys("10");
            Thread.Sleep(3000);

            // Periodicidad
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys("Anual");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys("Anual");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//span[contains(text(), 'Anual')]")).Click();
            Thread.Sleep(3000);

            //Sociedad facturacion
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").Click();
            //Thread.Sleep(1000);
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys("España");
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys(Keys.Control + "a");
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys(Keys.Delete);
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys("España");
            //Thread.Sleep(2000);
            //driver.FindElement(By.XPath("//span[contains(text(), 'España')]")).Click();
            //Thread.Sleep(3000);

            Utils.SearchWebElement("Oferta.saveAndCloseOferta").Click();
            Thread.Sleep(17000);

        }

        /// <summary>
        /// Método para acceder a servicios contratados
        /// </summary>
        public void CamposObligatoriosProductoRACK(String contacto)

        {

            // General

            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingNOMBREANEXO").SendKeys("prueba anexo RACK");
            Thread.Sleep(2000);

            //Pestaña Direcciones y Coordenadas (datos obligatorios)
            /// </summary>
            /// Sitio origen, destino, Direccion origen, destino
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadas").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIORIGEN").SendKeys("Madrid");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasSITIODESTINO").SendKeys("Madrid");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONORIGEN").SendKeys("Galicia");
            Utils.SearchWebElement("Producto.PestañaDireccionesYcoordenadasDIRECCIONDESTINO").SendKeys("Galicia");
            Thread.Sleep(2000);


            //Pestaña Jira (datos obligatorios)
            /// </summary>
            /// Capex fibra, equipo, contacto, fecha compromiso y detalle
            Utils.SearchWebElement("Producto.PestañaJira").Click();
            Thread.Sleep(2000);

            // Capex fibra y equipo
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXFIBRA").SendKeys("456");
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaJiraKAPEXEQUIPOS").SendKeys("458");
            Thread.Sleep(4000);

            // Detalle de servicio y fecha de compromiso
            Utils.SearchWebElement("Producto.PestañaJiraDETALLESERVICIO").SendKeys("Pruebas");
            Thread.Sleep(4000);
            Utils.SearchWebElement("Producto.PestañaJiraFECHACOMPROMISO").SendKeys("28/02/2022");
            Thread.Sleep(2000);

            // Contacto 
            if (contacto.Equals("contacto"))
            {
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys("Jose Antonio Garcia Mendez");
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.PestañaJiraCONTACTO").SendKeys("Jose Antonio Garcia Mendez");
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//span[contains(text(), 'Jose Antonio Garcia Mendez')]")).Click();
                Thread.Sleep(3000);
            }
            else
            {
                Thread.Sleep(3000);
            }

            //Pestaña Billing (datos obligatorios)
            /// </summary>
            /// Contrato marco, Actualizacion precio, periodicidad, UTPRX, codigo tarea, sociedad de facturacion, 
            Utils.SearchWebElement("Producto.PestañaContratosYbilling").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaContratosYbillingUTPRX").SendKeys("45");
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys("prueba");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingCONTRATOMARCO").SendKeys("prueba");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//span[contains(text(), 'prueba')]")).Click();
            Thread.Sleep(4000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTAREA").SendKeys("3");
            Thread.Sleep(2000);

            // Actualizacion de precio
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys("IPC");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingACTUALIZACIONPRECIO").SendKeys("IPC");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//span[contains(text(), 'IPC')]")).Click();
            Thread.Sleep(4000);

            //Limite IPC
            Utils.SearchWebElement("Producto.PestañaContratosYbillingLIMITEIPC").SendKeys("10");
            Thread.Sleep(3000);

            // Periodicidad
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys("Anual");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaContratosYbillingTIPOPERIODICIDAD").SendKeys("Anual");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//span[contains(text(), 'Anual')]")).Click();
            Thread.Sleep(3000);

            //Sociedad facturacion
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").Click();
            //Thread.Sleep(1000);
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys("España");
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys(Keys.Control + "a");
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys(Keys.Delete);
            //Utils.SearchWebElement("Producto.PestañaContratosYbillingPAIS").SendKeys("España");
            //Thread.Sleep(2000);
            //driver.FindElement(By.XPath("//span[contains(text(), 'España')]")).Click();
            //Thread.Sleep(3000);

            Utils.SearchWebElement("Oferta.saveAndCloseOferta").Click();
            Thread.Sleep(17000);

        }

         /// <summary>
         /// Método para Enviar a Jira los productos
         /// </summary>
         public void Enviar_A_Jira()
         {
            Utils.SearchWebElement("Producto.EnviarAJira").Click();
            Thread.Sleep(10000);
            
        }

        /// <summary>
        /// Método para cancelar producto enviados a Jira
        /// </summary>
        public void Enviar_A_Jira_cancelar()
        {

            Utils.SearchWebElement("Oferta.buttonAceptarVentanaEmergente").Click();
            Thread.Sleep(4000);

            //Pulsar sobre el primer registro y editar 

            Thread.Sleep(7000);
            Utils.SearchWebElement("Producto.SelectLine1").Click();
            Utils.SearchWebElement("Oferta.buttonEditOferta").Click();
            Thread.Sleep(4000);

            Utils.SearchWebElement("Oferta.LabelFechaspestaña").Click();
            Thread.Sleep(3000);

            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Oferta.ButtonNextday").Click();
            Thread.Sleep(2000);

            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion_Motivo").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion_Motivo").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion_Motivo").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion_Motivo").SendKeys("El Cliente lo entregará con su propia red");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(), 'El Cliente lo entregará con su propia red')]")).Click();
            Thread.Sleep(3000);
            Utils.SearchWebElement("Oferta.saveAndCloseOferta").Click();
            Thread.Sleep(7000);

            //Pulsar sobre el segundo registro y editar 

            Thread.Sleep(7000);
            Utils.SearchWebElement("Producto.SelectLine2").Click();
            Utils.SearchWebElement("Oferta.buttonEditOferta").Click();
            Thread.Sleep(4000);

            Utils.SearchWebElement("Oferta.LabelFechaspestaña").Click();
            Thread.Sleep(3000);

            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Oferta.ButtonNextday").Click();
            Thread.Sleep(4000);

            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion_Motivo").Click();
            Thread.Sleep(1000);
            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion_Motivo").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion_Motivo").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.PestañaFechas_fechacancelacion_Motivo").SendKeys("El Cliente lo entregará con su propia red");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(text(), 'El Cliente lo entregará con su propia red')]")).Click();
            Thread.Sleep(3000);
            Utils.SearchWebElement("Oferta.saveAndCloseOferta").Click();
            Thread.Sleep(7000);
        }
        
        public void rellenarCamposNuevoGeneradorFacturacion(string cliente, string nombreServicio)
        {
            //Rellenar Cliente
            wait.Until(ExpectedConditions.ElementToBeClickable(Utils.getByElement("Producto.GeneradorFacturacion.inputCliente")));
            Utils.SearchWebElement("Producto.GeneradorFacturacion.inputCliente").Click();
            driver.FindElement(By.XPath("//div[@aria-label = 'General']")).SendKeys(Keys.PageUp);
            Utils.SearchWebElement("Producto.GeneradorFacturacion.inputCliente").SendKeys(cliente);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), '" + cliente + "')]")));
                // Seleccionar cliente del desplegable
            driver.FindElement(By.XPath("//span[contains(text(), '" + cliente + "')]")).Click();
            Thread.Sleep(2000);

            //Rellenar sociedad
            Utils.SearchWebElement("Producto.GeneradorFacturacion.inputSociedad").Click();
            Utils.SearchWebElement("Producto.GeneradorFacturacion.inputSociedad").SendKeys("España");            
            driver.FindElement(By.XPath("//div[@aria-label = 'General']")).SendKeys(Keys.PageUp);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'España')]")));
            driver.FindElement(By.XPath("//span[contains(text(), 'España')]")).Click();

            //Rellenar servicio contratado
            Utils.SearchWebElement("Producto.GeneradorFacturacion.inputServicioContratado").Click();
            Utils.SearchWebElement("Producto.GeneradorFacturacion.inputServicioContratado").SendKeys(nombreServicio);            
            driver.FindElement(By.XPath("//div[@aria-label = 'General']")).SendKeys(Keys.PageUp);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), '" + nombreServicio + "')]")));
            driver.FindElement(By.XPath("//span[contains(text(), '" + nombreServicio + "')]")).Click();
            Utils.SearchWebElement("Oferta.saveOferta").Click();

        }


    }
}