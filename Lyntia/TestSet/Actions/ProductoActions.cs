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
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;

        public ProductoActions()
        {
            driver = Utils.driver;
            ofertaCondition = Utils.getOfertaConditions();
            productoActions = Utils.getProductoActions();
            productoCondition = Utils.getProductoConditions();
            commonActions = Utils.getCommonActions();
            commonCondition = Utils.getCommonConditions();
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
        }

        public void CreacionProducto(String productoExistente, String uso, String unidadVenta)
        {
            // Click en "+ Agregar producto"
            driver.FindElement(By.XPath("//button[contains(@title,'Agregar producto')]")).Click();
            Thread.Sleep(4000);

            // Seleccionar Producto existente del desplegable
            driver.FindElement(By.XPath("//input[contains(@data-id,'productid')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[contains(@data-id,'productid')]")).SendKeys(productoExistente);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//span[contains(text(), '" + productoExistente + "')]")).Click();
            Thread.Sleep(6000);

            // Seleccionar Uso(Línea de negocio)
            SelectElement drop = new SelectElement(driver.FindElement(By.XPath("//select[contains(@id,'uso')]")));
            drop.SelectByText(uso);

            // Seleccionar Producto existente del desplegable si esta vacio
            if (utils.EncontrarElemento(By.XPath("//input[contains(@id,'Dropdown_uomid')]")))
            {

                driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).Click();
                Thread.Sleep(1000);

                driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).SendKeys(unidadVenta);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).SendKeys(Keys.Control + "a");
                driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).SendKeys(Keys.Delete);
                driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).SendKeys(unidadVenta);
                Thread.Sleep(1000);

                driver.FindElement(By.XPath("//span[contains(text(), '" + unidadVenta + "')]")).Click();
                Thread.Sleep(2000);

            }

            // Guardar y Cerrar Producto actual
            driver.FindElement(By.XPath("//button[@id='quickCreateSaveAndCloseBtn']")).Click();
            Thread.Sleep(10000);
        }

        public void Añadirproducto_vistarapida()
        {
            driver.FindElement(By.Id("quickCreateLauncher_buttoncrm_header_global")).Click();
            driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[5].Click();
            Thread.Sleep(2000);
        }

        public void Añadir_producto_circuito_de_capacidad_sin_campos_oblitatorios()//solo vamoa a rellenar el tipo de producto
        {
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Click();//pulsamos sobre agregar producto
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Producto existente, Búsqueda')]"));
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            Thread.Sleep(2000);
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();//Guarda y cierra
        }

        public void Añadir_producto_circuito_de_capacidad_con_campos_oblitatorio()//completamos todos los campos
        {
            driver.FindElement(By.XPath("//select[contains(@aria-label, 'Uso (Línea de negocio)')]")).SendKeys("FT");
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            driver.FindElement(By.XPath("//select[contains(@aria-label, 'Uso (Unidad de venta, Búsqueda)')]")).SendKeys("10");
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();//Guarda y cierra
        }

        //Metodo en el que agregamos un producto a un servicio tipo cambio de capacidad, seleccionamos un producto heredado con campos obligatorios sin rellenar y se guarda. 
        public void Agregar_servicio_heredado_y_guardar()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[contains(@title, 'Cambio de capacidad (Upgrade/Downgrade)')]")).Click();//seleccionamos una oferta en Borrador y pulsamos sobre el ckeck
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Editar')]")).Click();//pulsamos editar la oferta
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Click();//pulsamos sobre agregar producto
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Servicio heredado, Búsqueda')]")).SendKeys("c");
            accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            Thread.Sleep(3000);
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("quickCreateSaveAndCloseBtn")).Click();//Guarda y cierra
            
        }

        //Metodo en el que una vez agregado un producto heredado se cumplimentan los campos obligatorios, se guarda y se cierra
        public void Cumplimentar_campos_y_guardar() 
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Precio mensual')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Precio mensual')]")).SendKeys("10");
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Duración del contrato (meses)')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Duración del contrato (meses)')]")).SendKeys("3");
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Duración del contrato (meses)')]")).SendKeys(Keys.PageDown);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@aria-label='NRC']")).Click();
            driver.FindElement(By.XPath("//input[@aria-label='NRC']")).SendKeys("4");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("quickCreateSaveAndCloseBtn")).Click();//Guarda y cierra
            Thread.Sleep(8000);

        }

        public void Agregar_Producto_tipo_circuito_de_capacidad(String productoExistente)
        {
            // Click en "+ Agregar producto"
            driver.FindElement(By.XPath("//button[contains(@title,'Agregar producto')]")).Click();
            Thread.Sleep(4000);

            // Seleccionar Producto existente del desplegable

            driver.FindElement(By.XPath("//input[contains(@data-id,'productid')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[contains(@data-id,'productid')]")).SendKeys(productoExistente);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//span[contains(text(), '" + productoExistente + "')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@id='quickCreateSaveAndCloseBtn']")).Click();
            Thread.Sleep(3000);

        }
        public void Agregar_Liena_de_nogocio_y_Unidad_de_venta(String uso, String unidadVenta)
        {
            // Seleccionar Uso(Línea de negocio)
            SelectElement drop = new SelectElement(driver.FindElement(By.XPath("//select[contains(@id,'uso')]")));
            drop.SelectByText(uso);

            // Seleccionar unidad de venta

            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).SendKeys(unidadVenta);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).SendKeys(Keys.Delete);
            driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).SendKeys(unidadVenta);
            Thread.Sleep(1000);

            driver.FindElement(By.XPath("//span[contains(text(), '" + unidadVenta + "')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@id='quickCreateSaveAndCloseBtn']")).Click();//guardamos y cerramos
            Thread.Sleep(8000);




        }
        public void Borrado_de_producto()//metodo por el cual borramos una linea de producto que anteriormente hemos dado de alta en añadir producto.
        {
            driver.FindElement(By.XPath("//div[contains(@aria-label, 'Seleccionar fila 2')]")).Click(); 
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Eliminar Producto de oferta')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='confirmButtonText']")).Click();
            Thread.Sleep(3000);
        }
        public void creacion_de_producto_tipo_cambio_de_capacidad(String ProdHeredado, String preciomen, String duracion, String NRC)//Metodo de añadir producto a un tipo de oferta cambio de capacidad
        {
            // Click en "+ Agregar producto"
            driver.FindElement(By.XPath("//button[contains(@title,'Agregar producto')]")).Click();
            Thread.Sleep(4000);

            //Metodo para ir cumplimentado los campos

            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Servicio heredado, Búsqueda')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Servicio heredado, Búsqueda')]")).SendKeys(ProdHeredado);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//span[contains(text(), '" + ProdHeredado + "')]")).Click();
            Thread.Sleep(6000);


            // Metodo para seleccionar el primero de la lista con los parametros elegidos(solo campo producto heredado)
            
            //driver.FindElement(By.XPath("//input[contains(@aria-label, 'Servicio heredado, Búsqueda')]")).SendKeys(ProdHeredado);
            //accionesSelenium.SendKeys(Keys.ArrowDown).Perform();
            //Thread.Sleep(3000);
            //accionesSelenium.SendKeys(Keys.Enter).Perform();
            //Thread.Sleep(2000);
            
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Precio mensual')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Precio mensual')]")).SendKeys(preciomen);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Duración del contrato (meses)')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Duración del contrato (meses)')]")).SendKeys(duracion);
            driver.FindElement(By.XPath("//input[contains(@aria-label, 'Duración del contrato (meses)')]")).SendKeys(Keys.PageDown);
            driver.FindElement(By.XPath("//input[@aria-label='NRC']")).Click();
            driver.FindElement(By.XPath("//input[@aria-label='NRC']")).SendKeys(NRC);

            // Guardar y Cerrar Producto actual
            driver.FindElement(By.XPath("//button[@id='quickCreateSaveAndCloseBtn']")).Click();
            Thread.Sleep(10000);
        }
       
    }
}
