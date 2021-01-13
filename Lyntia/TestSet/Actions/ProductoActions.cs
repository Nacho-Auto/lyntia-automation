using System;
using System.Threading;
using Lyntia.TestSet.Conditions;
using Lyntia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lyntia.TestSet.Actions
{
    public class ProductoActions
    {
        readonly Utils utils = new Utils();

        private static IWebDriver driver;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;

        public ProductoActions()
        {
            driver = Utils.driver;
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
        }

        public void CreacionProducto(String productoExistente, String uso, String unidadVenta)
        {
            // Click en "+ Agregar producto"
            Utils.searchWebElement("Producto.buttonAgregarProducto").Click();
            Thread.Sleep(4000);

            // Seleccionar Producto existente del desplegable
            Utils.searchWebElement("Producto.inputProductoExistente").Click();
            Thread.Sleep(1000);
            Utils.searchWebElement("Producto.inputProductoExistente").SendKeys(productoExistente);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//span[contains(text(), '" + productoExistente + "')]")).Click();
            Thread.Sleep(6000);

            // Seleccionar Uso(Línea de negocio)
            if (!uso.Equals(""))
            {
                SelectElement drop = new SelectElement(Utils.searchWebElement("Producto.SelectUsoLineaNegocio"));
                drop.SelectByText(uso);
            }


            if (!unidadVenta.Equals(""))
            {
                // Seleccionar Producto existente del desplegable si esta vacio
                if (utils.EncontrarElemento(By.XPath("//input[contains(@id,'Dropdown_uomid')]")))
                {

                    Utils.searchWebElement("Producto.inputUnidaddeVenta").Click();
                    Thread.Sleep(1000);

                    Utils.searchWebElement("Producto.inputUnidaddeVenta").SendKeys(unidadVenta);
                    Thread.Sleep(1000);
                    Utils.searchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Control + "a");
                    Utils.searchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Delete);
                    Utils.searchWebElement("Producto.inputUnidaddeVenta").SendKeys(unidadVenta);
                    Thread.Sleep(1000);

                    driver.FindElement(By.XPath("//span[contains(text(), '" + unidadVenta + "')]")).Click();
                    Thread.Sleep(2000);

                }
            }

            // Guardar y Cerrar Producto actual
            Utils.searchWebElement("Producto.GuardarYCerrar_producto").Click();
            Thread.Sleep(10000);
        }

        public void Añadirproducto_vistarapida()
        {
            try
            {
                Utils.searchWebElement("Producto.buttonCrearRegistroNuevo").Click();
                driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[5].Click();
                Thread.Sleep(2000);

                Console.WriteLine("Se añade el producto en la vista rapida correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Añadir_producto_vista_rapida.png", "El producto en vista rapida no se ha añadido correctamente"); 
            }
            
        }

        public void Añadir_producto_circuito_de_capacidad_sin_campos_oblitatorios()//solo vamoa a rellenar el tipo de producto
        {
            Utils.searchWebElement("Producto.buttonAgregarProducto").Click();//pulsamos sobre agregar producto
            Thread.Sleep(2000);
            Utils.searchWebElement("Producto.inputProductoExistente");
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            Thread.Sleep(2000);
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            Utils.searchWebElement("Producto.buttonGuardaryCerrar").Click();//Guarda y cierra
        }

        public void Añadir_producto_circuito_de_capacidad_con_campos_oblitatorio()//completamos todos los campos
        {
            Utils.searchWebElement("Producto.SelectLineaDeNegocio").SendKeys("FT");
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            Utils.searchWebElement("Producto.SelectUnidadDeVenta").SendKeys("10");
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();

            
        }

        //Metodo en el que agregamos un producto a un servicio tipo cambio de capacidad, seleccionamos un producto heredado con campos obligatorios sin rellenar y se guarda.

        public void Agregar_servicio_heredado_y_guardar()
        {
            try
            {
                Thread.Sleep(3000);
                Utils.searchWebElement("Producto.buttonAgregarProducto").Click();//pulsamos sobre agregar producto
                Thread.Sleep(4000);
                Utils.searchWebElement("Producto.inputServicioHeredado").SendKeys("c");
                accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
                Thread.Sleep(3000);
                accionesSelenium.SendKeys(Keys.Enter).Perform();
                Thread.Sleep(2000);
                Utils.searchWebElement("Producto.GuardarYCerrar_producto").Click();//Guarda y cierra

                Console.WriteLine("Se agrega un producto heredado correctamente y se guarda");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Agregar_servicio_heredado_y_guarda.png", "No se agrega un producto heredado correctamente y no se guarda");
            }
            
        }

        public void HeredarProducto(String productoHeredado, String precioMensual, String duracionContrato, String nrc)
        {
            // Click en "+ Agregar producto"
            Utils.searchWebElement("Producto.buttonAgregarProducto").Click();
            Thread.Sleep(4000);

            if (!productoHeredado.Equals(""))
            {
                Utils.searchWebElement("Producto.inputServicioHeredado").Click();
                Utils.searchWebElement("Producto.inputServicioHeredado").SendKeys(Keys.Control + "a");
                Utils.searchWebElement("Producto.inputServicioHeredado").SendKeys(Keys.Delete);

                Utils.searchWebElement("Producto.inputServicioHeredado").SendKeys(productoHeredado);
                Utils.searchWebElement("//span[contains(text(), '" + productoHeredado + "')]").Click();

                Utils.searchWebElement("//input[@aria-label='Cód. admin. servicio heredado']").SendKeys(Keys.PageDown);
                Thread.Sleep(6000);
            }

            if (!precioMensual.Equals(""))
            {
                Utils.searchWebElement("Producto.selectPrecioMensual").Click();
                Utils.searchWebElement("Producto.selectPrecioMensual").SendKeys(Keys.Control + "a");
                Utils.searchWebElement("Producto.selectPrecioMensual").SendKeys(Keys.Delete);

                Utils.searchWebElement("//input[@aria-label='Precio mensual']").SendKeys(precioMensual);

            }

            if (!duracionContrato.Equals(""))
            {
                Utils.searchWebElement("Producto.selectDuracionContrato").Click();
                Utils.searchWebElement("Producto.selectDuracionContrato").SendKeys(Keys.Control + "a");
                Utils.searchWebElement("Producto.selectDuracionContrato").SendKeys(Keys.Delete);

                Utils.searchWebElement("Producto.selectDuracionContrato").SendKeys(duracionContrato);
            }

            if (!nrc.Equals(""))
            {
                Utils.searchWebElement("Producto.selectNRC").Click();
                Utils.searchWebElement("Producto.selectNRC").SendKeys(Keys.Control + "a");
                Utils.searchWebElement("Producto.selectNRC").SendKeys(Keys.Delete);

                Utils.searchWebElement("Producto.selectNRC").SendKeys(nrc);
            }

            Utils.searchWebElement("Producto.GuardarYCerrar_producto").Click();
            Thread.Sleep(10000);
        }

        //Metodo en el que una vez agregado un producto heredado se cumplimentan los campos obligatorios, se guarda y se cierra
        public void Cumplimentar_campos_y_guardar() 
        {
            try
            {
                Thread.Sleep(3000);
                Utils.searchWebElement("Producto.inputPrecioMensual").Click();
                Utils.searchWebElement("Producto.inputPrecioMensual").SendKeys("10");
                Thread.Sleep(3000);
                Utils.searchWebElement("Producto.inputDuracionContrato").Click();
                Utils.searchWebElement("Producto.inputDuracionContrato").SendKeys("3");
                Utils.searchWebElement("Producto.inputDuracionContrato").SendKeys(Keys.PageDown);
                Thread.Sleep(2000);
                Utils.searchWebElement("Producto.inputNRC").Click();
                Utils.searchWebElement("Producto.inputNRC").SendKeys("4");
                Thread.Sleep(2000);
                Utils.searchWebElement("Producto.GuardarYCerrar_producto").Click();//Guarda y cierra
                Thread.Sleep(10000);

                Console.WriteLine("Precio mensual, duracion del contrato y NRC se guardan correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Precio mensual, duracion del contrato y NRC.png", "Precio mensual, duracion del contrato y NRC no se guardan correctamente");
            }
                

        }

        public void Agregar_Producto_tipo_circuito_de_capacidad(String productoExistente)
        {
            try
            {
                // Click en "+ Agregar producto"
                Utils.searchWebElement("Producto.buttonAgregarProducto").Click();
                Thread.Sleep(4000);

                // Seleccionar Producto existente del desplegable

                Utils.searchWebElement("Producto.inputProductoExistente").Click();
                Thread.Sleep(1000);
                Utils.searchWebElement("Producto.inputProductoExistente").SendKeys(productoExistente);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//span[contains(text(), '" + productoExistente + "')]")).Click();
                Thread.Sleep(2000);
                Utils.searchWebElement("Producto.GuardarYCerrar_producto").Click();
                Thread.Sleep(3000);

                Console.WriteLine("El producto existente tipo circuito de capacidad se ha añadido correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Agregar_Producto_tipo_circuito_de_capacidad.png", "El producto existente tipo circuito de capacidad no se ha añadido correctamente");
            }

        }
        public void Agregar_Liena_de_nogocio_y_Unidad_de_venta(String uso, String unidadVenta)
        {
            try
            {
                // Seleccionar Uso(Línea de negocio)
                SelectElement drop = new SelectElement(driver.FindElement(By.XPath("//select[contains(@id,'uso')]")));
                drop.SelectByText(uso);

                // Seleccionar unidad de venta

                Thread.Sleep(2000);
                Utils.searchWebElement("Producto.inputUnidaddeVenta").Click();
                Thread.Sleep(1000);

                Utils.searchWebElement("Producto.inputUnidaddeVenta").SendKeys(unidadVenta);
                Thread.Sleep(1000);
                Utils.searchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Control + "a");
                Utils.searchWebElement("Producto.inputUnidaddeVenta").SendKeys(Keys.Delete);
                Utils.searchWebElement("Producto.inputUnidaddeVenta").SendKeys(unidadVenta);
                Thread.Sleep(1000);

                driver.FindElement(By.XPath("//span[contains(text(), '" + unidadVenta + "')]")).Click();
                Thread.Sleep(2000);
                Utils.searchWebElement("Producto.GuardarYCerrar_producto").Click();//guardamos y cerramos
                Thread.Sleep(14000);

                Console.WriteLine("La linea de negocio y la unidad de venta se han agregado correctamente");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Agregar_Liena_de_nogocio_y_Unidad_de_venta.png", "La linea de negocio y la unidad de venta no se han agregado correctamente");
            }





        }
        public void Borrado_de_producto()//metodo por el cual borramos una linea de producto que anteriormente hemos dado de alta en añadir producto.
        {
            try
            {
                Utils.searchWebElement("Producto.ListProductoOfertaPrimerregistro").Click();
                Thread.Sleep(3000);
                Utils.searchWebElement("Producto.buttonMasComandos").Click();
                Thread.Sleep(3000);
                Utils.searchWebElement("Producto.buttonEliminarProductodeOferta").Click();
                Thread.Sleep(2000);
                Utils.searchWebElement("Producto.buttonEliminarProductodeOfertaConfirm").Click();
                Thread.Sleep(3000);

                Console.WriteLine("El reestablecimiento de la prueba se ha realizado correctamente");
                
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "reestablecimiento de la prueba.png", "El reestablecimiento de la prueba no se ha realizado correctamente");
            }
        }
        public void creacion_de_producto_tipo_cambio_de_capacidad(String ProdHeredado, String preciomen, String duracion, String NRC)//Metodo de añadir producto a un tipo de oferta cambio de capacidad
        {
            // Click en "+ Agregar producto"
            Utils.searchWebElement("Producto.buttonAgregarProducto").Click();
            Thread.Sleep(4000);

            //Metodo para ir cumplimentado los campos

            Utils.searchWebElement("Producto.inpuServicioHeredado").Click();
            Thread.Sleep(1000);
            Utils.searchWebElement("Producto.inpuServicioHeredado").SendKeys(ProdHeredado);
            Thread.Sleep(1000);
            Utils.searchWebElement("//span[contains(text(), '" + ProdHeredado + "')]").Click();
            Thread.Sleep(6000);


            // Metodo para seleccionar el primero de la lista con los parametros elegidos(solo campo producto heredado)

            //Utils.searchWebElement("Producto.inpuServicioHeredado").SendKeys(ProdHeredado);
            //accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            //Thread.Sleep(3000);
            //accionesSelenium.SendKeys(Keys.Enter).Perform();
            //Thread.Sleep(2000);

            Utils.searchWebElement("Producto.inputPrecioMensual").Click();
            Utils.searchWebElement("Producto.inputPrecioMensual").SendKeys(preciomen);
            Thread.Sleep(3000);
            Utils.searchWebElement("Producto.inputDuracionContrato").Click();
            Utils.searchWebElement("Producto.inputDuracionContrato").SendKeys(duracion);
            Utils.searchWebElement("Producto.inputDuracionContrato").SendKeys(Keys.PageDown);
            Utils.searchWebElement("Producto.inputNRC").Click();
            Utils.searchWebElement("Producto.inputNRC").SendKeys(NRC);

            // Guardar y Cerrar Producto actual
            Utils.searchWebElement("Producto.GuardarYCerrar_producto").Click();
            Thread.Sleep(10000);
        }
       
    }
}
