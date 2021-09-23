using System;
using System.Collections.Generic;
using System.Threading;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Lyntia.TestSet.Conditions;

namespace Lyntia.TestSet.Actions
{
    public class ProductoActions
    {
        readonly Utils utils = new Utils();

        private static IWebDriver driver;
        private static WebDriverWait wait;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;

        private static ProductoConditions productoConditions;

        public ProductoActions()
        {
            driver = Utils.driver;
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            productoConditions = Utils.GetProductoConditions();
        }


        public void CreacionProducto(String productoExistente, String uso, String unidadVenta, String metros, String modalidadContratacion, String nrc, String duracionContrato, String PrecioMensual, String Infraestuctura)
        {
            try
            {
                Utils.SearchWebElement("Oferta.buttonAgregarProducto").Click();

                // Seleccionar Producto existente del desplegable
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.inputProductoExistente"))));
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.inputProductoExistente").Click();
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(productoExistente);
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(Keys.Control + "a");
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(Keys.Delete);
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(productoExistente);
                Thread.Sleep(4000);

                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(), '" + productoExistente + "')]")));
                driver.FindElement(By.XPath("//span[contains(text(), '" + productoExistente + "')]")).Click();
                Thread.Sleep(6000);




                // Seleccionar infrastura
                if (!Infraestuctura.Equals(""))
                {
                    Utils.SearchWebElement("Oferta.Infastructura").Click();
                    Thread.Sleep(1000);

                    Utils.SearchWebElement("Oferta.Infastructura").SendKeys(Infraestuctura);
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Oferta.Infastructura").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Oferta.Infastructura").SendKeys(Keys.Delete);
                    Utils.SearchWebElement("Oferta.Infastructura").SendKeys(Infraestuctura);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//span[contains(text(), '" + Infraestuctura + "')]")).Click();
                    Thread.Sleep(2000);
                }

                // Seleccionar Uso(Línea de negocio)
                if (!uso.Equals(""))
                {
                    SelectElement drop = new SelectElement(Utils.SearchWebElement("Producto.SelectUsoLineaNegocio"));
                    drop.SelectByText(uso);
                }
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AddProducto.png", "No se añaden correctamente los datos del Producto: " + productoExistente + ", " + uso + ", " + unidadVenta);
                throw e;
            }

            if (!unidadVenta.Equals("") && utils.EncontrarElemento(By.XPath(Utils.GetIdentifier("Producto.inputUnidaddeVenta"))))
            {
                // Seleccionar Producto existente del desplegable si esta vacio
                if (utils.EncontrarElemento(By.XPath("//input[contains(@id,'Dropdown_uomid')]")))
                {
                    try
                    {
                        Utils.SearchWebElement("Producto.inputUnidaddeVenta").Click();
                        Thread.Sleep(1000);

                        Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(unidadVenta);
                        Thread.Sleep(2000);
                        Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Control + "a");
                        Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Delete);
                        Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(unidadVenta);
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("//span[contains(text(), '" + unidadVenta + "')]")).Click();
                        Thread.Sleep(2000);
                    }
                    catch (Exception e)
                    {
                        CommonActions.CapturadorExcepcion(e, "AddProducto.png", "No se añaden correctamente los datos del Producto: " + productoExistente + ", " + uso + ", " + unidadVenta);
                        throw e;
                    }
                }
            }

            // Introduccir metros
            if (!metros.Equals(""))
            {
                Utils.SearchWebElement("Producto.inputMetros").Click();
                Thread.Sleep(1000);

                Utils.SearchWebElement("Producto.inputMetros").SendKeys(metros);
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.inputMetros").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.inputMetros").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.inputMetros").SendKeys(metros);
                Thread.Sleep(1000);
            }

            // Introduccir Modalidad de Contratacion
            if (!modalidadContratacion.Equals(""))
            {
                SelectElement drop = new SelectElement(Utils.SearchWebElement("Producto.SelectModalidadContratacion"));
                drop.SelectByText(modalidadContratacion);
            }

            // Introduccir NRC
            if (!nrc.Equals(""))
            {
                Utils.SearchWebElement("Producto.selectNRC").Click();
                Thread.Sleep(1000);

                Utils.SearchWebElement("Producto.selectNRC").SendKeys(nrc);
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.selectNRC").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.selectNRC").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.selectNRC").SendKeys(nrc);
                Thread.Sleep(1000);
            }


            // Introduccir Duración de Contrato
            if (!duracionContrato.Equals(""))
            {
                Utils.SearchWebElement("Producto.inputDuracionContrato").Click();
                Thread.Sleep(1000);

                Utils.SearchWebElement("Producto.inputDuracionContrato").SendKeys(duracionContrato);
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.inputDuracionContrato").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.inputDuracionContrato").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.inputDuracionContrato").SendKeys(duracionContrato);
                Thread.Sleep(3000);
            }

            // Introduccir precio mensual
            if (!PrecioMensual.Equals(""))
            {
                Utils.SearchWebElement("Producto.inputPrecioMensual2").Click();
                Thread.Sleep(2000);

                Utils.SearchWebElement("Producto.inputPrecioMensual2").SendKeys(PrecioMensual);
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.inputPrecioMensual2").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.inputPrecioMensual2").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.inputPrecioMensual2").SendKeys(PrecioMensual);
                Thread.Sleep(1000);
            }

            try
            {
                // Guardar y Cerrar Producto actual
                Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click();
                Thread.Sleep(24000);
                Utils.SearchWebElement("Oferta.saveOferta").Click();
                TestContext.WriteLine("Producto guardado correctamente: " + productoExistente + ", " + uso + ", " + unidadVenta + ", " + metros + ", " + nrc + ", " + modalidadContratacion);
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "GuardarProducto.png", "El producto no fue creado: " + productoExistente + ", " + uso + ", " + unidadVenta + ", " + metros + ", " + nrc + ", " + modalidadContratacion);
                throw e;
            }
        }

        public void Añadirproducto_vistarapida()
        {
            try
            {
                Utils.SearchWebElement("Producto.buttonCrearRegistroNuevo").Click();
                driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[5].Click();
                Thread.Sleep(3000);

                TestContext.WriteLine("Se añade el producto en la vista rapida correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Añadir_producto_vista_rapida.png", "El producto en vista rapida no se ha añadido correctamente");
                throw e;
            }
        }

        public void Añadir_producto_circuito_de_capacidad_sin_campos_oblitatorios()//solo vamoa a rellenar el tipo de producto
        {
            Utils.SearchWebElement("Producto.buttonAgregarProducto").Click();//pulsamos sobre agregar producto
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.inputProductoExistente");
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            Thread.Sleep(2000);
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            Utils.SearchWebElement("Producto.buttonGuardaryCerrar").Click();//Guarda y cierra
        }

        public void Añadir_producto_circuito_de_capacidad_con_campos_oblitatorio()//completamos todos los campos
        {
            Utils.SearchWebElement("Producto.SelectLineaDeNegocio").SendKeys("FT");
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            Utils.SearchWebElement("Producto.SelectUnidadDeVenta").SendKeys("10");
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
        }

        //Metodo en el que agregamos un producto a un servicio tipo cambio de capacidad, seleccionamos un producto heredado con campos obligatorios sin rellenar y se guarda.
        public void Agregar_servicio_heredado_y_guardar()
        {
            try
            {
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.buttonAgregarProducto").Click();//pulsamos sobre agregar producto
                Thread.Sleep(4000);
                Utils.SearchWebElement("Producto.inputServicioexistente").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.inputServicioexistente").SendKeys(Keys.Delete);
                Thread.Sleep(2000);
                Utils.SearchWebElement("Producto.inputServicioexistente").SendKeys("Circuitos de capacidad");
                driver.FindElement(By.XPath("//span[contains(text(), 'Circuitos de capacidad')]")).Click();
                Thread.Sleep(2000);
                Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click();//Guarda y cierra

                TestContext.WriteLine("Se agrega un producto heredado correctamente y se guarda");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Agregar_servicio_heredado_y_guarda.png", "No se agrega un producto heredado correctamente y no se guarda");
                throw e;
            }
        }
        //Metodo para agregar producto heredado
        public void HeredarProducto(String productoHeredado, String precioMensual, String duracionContrato, String nrc, String metros, String precioMetroAnual, String Venta)
        {
            // Click en "+ Agregar producto"
            Utils.SearchWebElement("Producto.buttonAgregarProducto").Click();
            Thread.Sleep(4000);

            try
            {

                if (!productoHeredado.Equals(""))
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.inputServicioHeredado"))));
                    Utils.SearchWebElement("Producto.inputServicioHeredado").Click();
                    Utils.SearchWebElement("Producto.inputServicioHeredado").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Producto.inputServicioHeredado").SendKeys(Keys.Delete);

                    Utils.SearchWebElement("Producto.inputServicioHeredado").SendKeys(productoHeredado);
                    driver.FindElement(By.XPath("//span[contains(text(), '" + productoHeredado + "')]")).Click();
                    Thread.Sleep(3000);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@aria-label='Cód. admin. servicio heredado']")));
                    driver.FindElement(By.XPath("//input[@aria-label='Cód. admin. servicio heredado']")).SendKeys(Keys.PageDown);
                    Thread.Sleep(6000);

                }

                if (!precioMensual.Equals(""))
                {
                    Utils.SearchWebElement("Producto.selectPrecioMensual").Click();
                    Utils.SearchWebElement("Producto.selectPrecioMensual").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Producto.selectPrecioMensual").SendKeys(Keys.Delete);

                    Utils.SearchWebElement("Producto.selectPrecioMensual").SendKeys(precioMensual);
                }

                if (!metros.Equals(""))
                {
                    Utils.SearchWebElement("Producto.inputMetros").Click();
                    Utils.SearchWebElement("Producto.inputMetros").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Producto.inputMetros").SendKeys(Keys.Delete);
                    Utils.SearchWebElement("Producto.inputMetros").SendKeys(metros);
                }

                if (!precioMetroAnual.Equals(""))
                {
                    Utils.SearchWebElement("Producto.inputPrecioMetroAnual").Click();
                    Utils.SearchWebElement("Producto.inputPrecioMetroAnual").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Producto.inputPrecioMetroAnual").SendKeys(Keys.Delete);

                    Utils.SearchWebElement("Producto.inputPrecioMetroAnual").SendKeys(precioMetroAnual);
                }

               

                if (!duracionContrato.Equals(""))
                {
                    Utils.SearchWebElement("Producto.selectDuracionContrato").Click();
                    Utils.SearchWebElement("Producto.selectDuracionContrato").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Producto.selectDuracionContrato").SendKeys(Keys.Delete);

                    Utils.SearchWebElement("Producto.selectDuracionContrato").SendKeys(duracionContrato);
                }

                if (!nrc.Equals(""))
                {
                    Utils.SearchWebElement("Producto.selectNRC").Click();
                    Utils.SearchWebElement("Producto.selectNRC").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Producto.selectNRC").SendKeys(Keys.Delete);

                    Utils.SearchWebElement("Producto.selectNRC").SendKeys(nrc);
                    Thread.Sleep(3000);
                }

                if (!Venta.Equals(""))
                {
                    Utils.SearchWebElement("Producto.inputUnidaddeVenta").Click();
                    Thread.Sleep(1000);

                    Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Venta);
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Control + "a");
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Delete);
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Venta);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//span[contains(text(), '" + Venta + "')]")).Click();
                    Thread.Sleep(2000);
                }
                

                Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click();
                Thread.Sleep(40000);
                TestContext.WriteLine("El tipo de producto heredado con sus parametros se guarda correctamente");
            }

            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Producto heredado.png", "***productoHeredado, precioMensual, duracionContrato, nrc, metros, precioMetroAnual no se guarda correctamente");
                throw e;
            }
        }


        //Metodo en el que una vez agregado un producto heredado se cumplimentan los campos obligatorios, se guarda y se cierra
        public void Cumplimentar_campos_y_guardar(String linea, String UnidadDEVenta)
        {
            try
            {



                // Seleccionar Uso(Línea de negocio)
                SelectElement drop = new SelectElement(driver.FindElement(By.XPath("//select[contains(@id,'uso')]")));
                drop.SelectByText(linea);
                Thread.Sleep(2000);

                

                Utils.SearchWebElement("Producto.inputUnidaddeVenta").Click();
                Thread.Sleep(1000);

                Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(UnidadDEVenta);
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Delete);
                Thread.Sleep(4000);
                Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(UnidadDEVenta);
                Thread.Sleep(4000);
                driver.FindElement(By.XPath("//span[contains(text(), '" + UnidadDEVenta + "')]")).Click();
                Thread.Sleep(2000);

                Utils.SearchWebElement("Producto.selectNRC").Click();
                Utils.SearchWebElement("Producto.selectNRC").SendKeys("4");
                Thread.Sleep(2000);

                //Thread.Sleep(3000);
                //Utils.SearchWebElement("Producto.inputPrecioMensual").Click();
                //Utils.SearchWebElement("Producto.inputPrecioMensual").SendKeys("10");
                //Thread.Sleep(3000);
                //Utils.SearchWebElement("Producto.inputDuracionContrato").Click();
                //Utils.SearchWebElement("Producto.inputDuracionContrato").SendKeys("3");
                //Utils.SearchWebElement("Producto.inputDuracionContrato").SendKeys(Keys.PageDown);
                //Thread.Sleep(2000);
                //Thread.Sleep(2000);

                Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click();//Guarda y cierra
                Thread.Sleep(10000);

                TestContext.WriteLine("Precio mensual, duracion del contrato y NRC se guardan correctamente");

            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Precio mensual, duracion del contrato y NRC.png", "Precio mensual, duracion del contrato y NRC no se guardan correctamente");
                throw e;
            }
        }

        public void Agregar_Producto_tipo_circuito_de_capacidad(String productoExistente)
        {
            try
            {
                // Click en "+ Agregar producto"
                Utils.SearchWebElement("Producto.buttonAgregarProducto").Click();
                Thread.Sleep(5000);

                // Seleccionar Producto existente del desplegable
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.inputProductoExistente"))));
                Utils.SearchWebElement("Producto.inputProductoExistente").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(productoExistente);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//span[contains(text(), '" + productoExistente + "')]")).Click();
                Thread.Sleep(2000);
                Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click();
                Thread.Sleep(3000);

                TestContext.WriteLine("El producto existente tipo circuito de capacidad se ha añadido correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Agregar_Producto_tipo_circuito_de_capacidad.png", "El producto existente tipo circuito de capacidad no se ha añadido correctamente");
                throw e;
            }
        }

        public void Agregar_Linea_de_negocio_y_Unidad_de_venta(String uso, String NRC)
        {
            // Seleccionar Uso(Línea de negocio)
            SelectElement drop = new SelectElement(driver.FindElement(By.XPath("//select[contains(@id,'uso')]")));
            drop.SelectByText(uso);

            // Seleccionar unidad de venta
            if (utils.EncontrarElemento(By.XPath(Utils.GetIdentifier("Producto.selectNRC"))))
            {
                try
                {
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Producto.selectNRC").SendKeys(Keys.Control + "a");
                    Utils.SearchWebElement("Producto.selectNRC").SendKeys(Keys.Delete);
                    Utils.SearchWebElement("Producto.selectNRC").SendKeys(NRC);
                    Thread.Sleep(8000);

                    Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click(); //guardamos y cerramos
                    Thread.Sleep(18000);

                    TestContext.WriteLine("***El producto se guarda de manera correcta.");


                    //Utils.SearchWebElement("Producto.inputUnidaddeVenta").Click();
                    //Thread.Sleep(1000);

                    //Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(unidadVenta);
                    //Thread.Sleep(1000);
                    //Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Control + "a");
                    //Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Delete);
                    //Utils.SearchWebElement("Producto.inputUnidaddeVenta").SendKeys(unidadVenta);
                    //Thread.Sleep(1000);

                    //driver.FindElement(By.XPath("//span[contains(text(), '" + unidadVenta + "')]")).Click();
                    //Thread.Sleep(2000);
                }
                catch (Exception e)
                {
                    CommonActions.CapturadorExcepcion(e, "AddLineaNegocioProducto.png", "La linea de negocio y la unidad de venta no se han agregado correctamente.");
                    throw e;
                }
            }


        }

        public void Borrado_de_producto()//metodo por el cual borramos una linea de producto que anteriormente hemos dado de alta en añadir producto.
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Oferta.gridSelectAll"))));
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.gridSelectAll").Click();
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.buttonEliminarProductodeOferta").Click();
                Thread.Sleep(2000);
                Utils.SearchWebElement("Oferta.confirmDeleteOferta").Click();
                Thread.Sleep(3000);

                TestContext.WriteLine("El reestablecimiento de la prueba se ha realizado correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ReestablecimientoPrueba.png", "El reestablecimiento de la prueba no se ha realizado correctamente");
                throw e;
            }
        }
        public void Creacion_de_producto_tipo_cambio_de_capacidad(String ProdHeredado, String preciomen, String duracion, String NRC)//Metodo de añadir producto a un tipo de oferta cambio de capacidad
        {
            try
            {
                // Click en "+ Agregar producto"
                Utils.SearchWebElement("Producto.buttonAgregarProducto").Click();
                Thread.Sleep(4000);

                //Metodo para ir cumplimentado los campos
                Utils.SearchWebElement("Producto.inpuServicioHeredado").Click();
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.inpuServicioHeredado").SendKeys(ProdHeredado);
                Thread.Sleep(1000);
                Utils.SearchWebElement("//span[contains(text(), '" + ProdHeredado + "')]").Click();
                Thread.Sleep(6000);
                Utils.SearchWebElement("Producto.inputPrecioMensual").Click();
                Utils.SearchWebElement("Producto.inputPrecioMensual").SendKeys(preciomen);
                Thread.Sleep(3000);
                Utils.SearchWebElement("Producto.inputDuracionContrato").Click();
                Utils.SearchWebElement("Producto.inputDuracionContrato").SendKeys(duracion);
                Utils.SearchWebElement("Producto.inputDuracionContrato").SendKeys(Keys.PageDown);
                Utils.SearchWebElement("Producto.inputNRC").Click();
                Utils.SearchWebElement("Producto.inputNRC").SendKeys(NRC);

                // Guardar y Cerrar Producto actual
                Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click();
                Thread.Sleep(10000);

                TestContext.WriteLine("Se crea correctamente un producto del tipo cambio de capacidad");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Creacion_de_producto_tipo_cambio_de_capacidad.png", "No se crea correctamente un producto del tipo cambio de capacidad");
                throw e;
            }
        }

        /// <summary>
        /// Método para Agregar producto
        /// </summary>
        public void Editar_añadir_producto()
        {
            try
            {
                Utils.SearchWebElement("Producto.buttonAgregarProducto").Click(); //pulsamos sobre agregar producto
                if (utils.EncontrarElemento(By.XPath(Utils.GetIdentifier("Producto.GuardarYCerrar_producto"))))
                {
                    Thread.Sleep(2000);
                    Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click(); //guardamos
                    Thread.Sleep(3000);
                    Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click(); //guardamos y cerramos
                    TestContext.WriteLine("Se pulsa correctamente sobre agregar producto");
                }
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Editar_añadir_producto.png", "No se pulsa correctamente sobre agregar producto");
                throw e;
            }
        }

        /// <summary>
        /// Método para copiar y buscar el codigo administrativo de la primera linea de los productos contratados
        /// </summary>
        public void BuscarCodigo_administrativo1()
        {
            List<string> listaCodigos = new List<string>();

            for (int i = 0; i <= 1; i++)
            {
                //div[contains(@id,'cell-1-3')]//label
                String codigo = driver.FindElement(By.XPath("//div[contains(@id,'cell-" + i + "-3')]//label")).Text;
                listaCodigos.Add(codigo);
            }

            Utils.SearchWebElement("Oferta.GridServiciosContratados").Click();
            Thread.Sleep(3000);
            for (int i = 0; i <= 1; i++)
            {
                Utils.SearchWebElement("Oferta.inputFilter").SendKeys(listaCodigos[i]);
                Utils.SearchWebElement("Oferta.buttonQuickFindOferta").Click();
                Thread.Sleep(3000);
                Assert.AreEqual("En construcción", Utils.SearchWebElement("Producto.labelEnconstruccion").Text);
                //productoConditions.ResBuscarCodigo_administrativo();
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.inputFilter").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputFilter").SendKeys(Keys.Delete);
            }
        }

        /// <summary>
        /// Método para copiar y buscar el codigo administrativo de la segunda linea de los productos contratados
        /// </summary>
        public void BuscarCodigo_administrativo2()
        {
            List<string> listaCodigos = new List<string>();

            for (int i = 0; i <= 1; i++)
            {
                //div[contains(@id,'cell-1-3')]//label
                String codigo = driver.FindElement(By.XPath("//div[contains(@id,'cell-" + i + "-3')]//label")).Text;
                listaCodigos.Add(codigo);
            }

            Utils.SearchWebElement("Oferta.GridServiciosContratados").Click();
            Thread.Sleep(3000);
            for (int i = 0; i <= 1; i++)
            {
                Utils.SearchWebElement("Oferta.inputFilter").SendKeys(listaCodigos[i]);
                Utils.SearchWebElement("Oferta.buttonQuickFindOferta").Click();
                Thread.Sleep(3000);
                //Assert.AreEqual("En construcción", Utils.SearchWebElement("Producto.labelEnservicio").Text);
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.inputFilter").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Oferta.inputFilter").SendKeys(Keys.Delete);
            }
        }

        /// <summary>
        /// Método para copiar codigo de la oferta y buscarlo en la vista rapida
        /// </summary>
        public void BuscarOferta_desde_servicio_contratado()

        {
            try
            {
                var ejemplo = driver.FindElement(By.XPath("//div[contains(@data-id, 'cell-0-7')]")).Text;
                Utils.SearchWebElement("Oferta.ofertaSection").Click();
                Thread.Sleep(3000);
                Utils.SearchWebElement("Oferta.inputFilter").SendKeys(ejemplo);
                Utils.SearchWebElement("Oferta.buttonQuickFindOferta").Click();
                Thread.Sleep(2000);

                TestContext.WriteLine("Se busca la oferta correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "BuscarOferta_desde_servicio_contratado.png", "No se busca la oferta correctamente");
                throw e;
            }
        }

        /// <summary>
        /// Método para crear producto tipo fibra oscura (IRU)
        /// </summary>
        public void creacionproductofibraoscuraIRU(String productoExistente, String uso, String modalidadContratacion, String metros, String nrcIRU)
        {
            Utils.SearchWebElement("Oferta.buttonAgregarProducto").Click();

            // Seleccionar Producto existente del desplegable
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.inputProductoExistente"))));
            Thread.Sleep(3000);
            Utils.SearchWebElement("Producto.inputProductoExistente").Click();
            Thread.Sleep(3000);
            Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(productoExistente);
            Thread.Sleep(3000);
            Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(Keys.Control + "a");
            Thread.Sleep(3000);
            Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(Keys.Delete);
            Thread.Sleep(3000);
            Utils.SearchWebElement("Producto.inputProductoExistente").SendKeys(productoExistente);

            Thread.Sleep(7000);

            driver.FindElement(By.XPath("//span[contains(text(), '" + productoExistente + "')]")).Click();
            Thread.Sleep(6000);

            // Seleccionar Uso(Línea de negocio)
            if (!uso.Equals(""))
            {
                SelectElement drop = new SelectElement(Utils.SearchWebElement("Producto.SelectUsoLineaNegocio"));
                drop.SelectByText(uso);
            }

            // Introduccir metros
            if (!metros.Equals(""))
            {
                Utils.SearchWebElement("Producto.inputMetros").Click();
                Thread.Sleep(1000);

                Utils.SearchWebElement("Producto.inputMetros").SendKeys(metros);
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.inputMetros").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.inputMetros").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.inputMetros").SendKeys(metros);
                Thread.Sleep(1000);
            }

            // Introduccir Modalidad de Contratacion
            if (!modalidadContratacion.Equals(""))
            {
                SelectElement drop = new SelectElement(Utils.SearchWebElement("Producto.SelectModalidadContratacion"));
                drop.SelectByText(modalidadContratacion);
            }

            // Introduccir NRC
            if (!nrcIRU.Equals(""))
            {
                Utils.SearchWebElement("Producto.inputNRC").Click();
                Thread.Sleep(1000);

                Utils.SearchWebElement("Producto.inputNRC").SendKeys(nrcIRU);
                Thread.Sleep(1000);
                Utils.SearchWebElement("Producto.inputNRC").SendKeys(Keys.Control + "a");
                Utils.SearchWebElement("Producto.inputNRC").SendKeys(Keys.Delete);
                Utils.SearchWebElement("Producto.inputNRC").SendKeys(nrcIRU);
                Thread.Sleep(1000);
            }

            // Guardar y Cerrar Producto actual
            Utils.SearchWebElement("Producto.GuardarYCerrar_producto").Click();
            Thread.Sleep(24000);

        }

    }
}
