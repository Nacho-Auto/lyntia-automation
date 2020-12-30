using System;
using System.Threading;
using Lyntia.TestSet.Actions;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Lyntia.TestSet.Conditions
{
    public class OfertaConditions
    {

        private static IWebDriver driver;
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;

        public OfertaConditions()
        {
            driver = Utils.driver;
            ofertaCondition = Utils.getOfertaConditions();
            productoActions = Utils.getProductoActions();
            productoCondition = Utils.getProductoConditions();
            commonActions = Utils.getCommonActions();
            commonCondition = Utils.getCommonConditions();
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
        }

        public void CreaOferta()
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

        public void FechasSinInformar()
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

        public void OfertaNoCreada()
        {
            // Assert de alerta con los campos obligatorios sin informar
            //div[@data-id="notificationWrapper"]
            Assert.IsTrue(driver.FindElements(By.XPath("//div[@data-id='notificationWrapper']")).Count > 0);
            driver.FindElement(By.XPath("//div[@data-id='notificationWrapper']")).Click();
        }

        internal void FechasInformadasCorrectamente()
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

        internal void OfertaGuardadaCorrectamente(String nombreOferta, String tipoOferta)
        {

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

        internal void OfertaGuardadaCorrectamenteEnGrid()
        {
            // Se encuentra en estado borrador
            Assert.AreEqual("Borrador", driver.FindElement(By.XPath("//div[@data-id='cell-0-7']")).GetAttribute("title"));

            // Se encuentra en Razon para el estado En elaboracion
            Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//div[@data-id='cell-0-8']")).GetAttribute("title"));
        }

        public void AccederSeleccionOfertaAPR0001()
        {
            Assert.AreEqual(true, driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Enabled);//comprobamos que añadir producto esta habilitado
        }

        public void Resultado_Editar_añadir_producto()
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
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Cancelar')]")).Click();//boton cancelar añadir producto
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Descartar cambios')]")).Click();//boton cerrar de creacion producto
            Assert.AreEqual(true, driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Enabled);//volvemos a la pagina de añadir producto
            Thread.Sleep(2000);
        }

        public void AccederSeleccionOferta()
        {
            Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Enabled);//la pestaña general esta activa
            Assert.AreEqual("General", driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Text);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("AccederSeleccionOferta.png");
        }

        public void IntroduccionDatos()//Comprobamos que todos los campos se introducen correctamente
        {
            String OfertaMOD = driver.FindElement(By.LinkText("Automatica_MODPrueba_auto_NO_borrar_MODIFICADA")).Text;//muestra por consola la nueva oferta modificada
            Console.WriteLine(OfertaMOD);
        }

        public void IntroduccionDatos2()//comprobamos todas las modificaciones realizadas
        {
            Assert.AreEqual("La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            Assert.AreEqual("La oferta de tipo “Cambio de precio” no requiere envío a construcción ni cambiar el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            Assert.AreEqual("La oferta de tipo “Cambio de tecnología” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            Assert.AreEqual(" La oferta de tipo “Migración” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
        }

        public void Aviso_cambiocapacidad()//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodeprecio()//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de precio” no requiere envío a construcción ni cambiar el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodesolucion()//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Cambio de tecnología” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
        }

        public void Aviso_Cambiodedireccion()//mensaje por el tipo de oferta
        {
            Thread.Sleep(3000);
            Assert.AreEqual("La oferta de tipo “Migración” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();//Guardar
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();//Guarda y cierra

            //reestablece datos CRM-EOF0004

            driver.FindElement(By.LinkText("Prueba-Auto_NO_borrarCRM-EOF0004")).Click();
            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de dirección (Migración)')]")).Click();

            accionesSelenium.SendKeys(Keys.ArrowUp).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();

            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de solución técnica (Tecnología)')]")).Click();
            accionesSelenium.SendKeys(Keys.ArrowUp).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();

            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de precio/Renovación')]")).Click();
            accionesSelenium.SendKeys(Keys.ArrowUp).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();

            driver.FindElement(By.XPath("//select[contains(@title, 'Cambio de capacidad (Upgrade/Downgrade)')]")).Click();
            accionesSelenium.SendKeys(Keys.ArrowUp).Perform();
            accionesSelenium.SendKeys(Keys.Enter).Perform();
            
            Thread.Sleep(7000);
            driver.FindElement(By.XPath("//li[contains(@aria-label, 'Guardar')]")).Click();
            driver.FindElement(By.XPath("//span[contains(@aria-label, 'Guardar y cerrar')]")).Click();
        }

        public void Resultado_AccederOfertaestado_Adjudicada()
        {
            Assert.AreEqual("Ganada", driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Text);//la oferta esta en estado ganada
        }
		
        public void Resultado_Eliminar_BarraMenu()
        {
            Assert.AreEqual("Confirmar eliminación", driver.FindElement(By.XPath("//h1[contains(@aria-label, 'Confirmar eliminación')]")).Text);//se comprueba texto de la ventana emergente
        }
		
        public void Resultado_Cancelar()
        {
            Assert.AreEqual("Ganada", driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Text);//la oferta esta en estado ganada
        }

        public void Resultado_Eliminar_Popup()
        {
            Assert.AreEqual("Acceso denegado", driver.FindElement(By.XPath("//h1[contains(@aria-label, 'Acceso denegado')]")).Text);//muestra un mensaje informativo
            String AvisoPriv = driver.FindElement(By.XPath("//h1[contains(@aria-label, 'Acceso denegado')]")).Text;//imprime en consola el texto
            Console.WriteLine(AvisoPriv);
            String AvisoPriv2 = driver.FindElement(By.XPath("//h2[contains(@aria-label, 'privilegios')]")).Text;//imprime en consola el texto
            Console.WriteLine(AvisoPriv2);
            
            driver.FindElement(By.XPath("//*[@id='cancelButton']")).Click();
            Thread.Sleep(2000);
        }
		
        public void Resultado_Seleccionofertarazonadjudicada()
        {

            //Assert.IsTrue(false, Utils.EncontrarElemento(By.XPath("//span[contains(@aria-label, 'Eliminar')]")));
		}
		
        public void OfertaNoCerrada()
        {
            // Se muestra label con mensaje "Por favor, completa los campos obligatorios"
            Assert.AreEqual("Por favor, completa los campos obligatorios", driver.FindElement(By.XPath("//p[@id='error']")).Text);
        }

        public void OfertaCerradaCorrectamenteEnGrid(String razonEstado)
        {
            // Se encuentra en estado borrador
            Assert.AreEqual("Cerrada", driver.FindElement(By.XPath("//div[@data-id='cell-0-7']")).GetAttribute("title"));

            // Se encuentra en Razon para el estado En elaboracion
            Assert.AreEqual(razonEstado, driver.FindElement(By.XPath("//div[@data-id='cell-0-8']")).GetAttribute("title"));
        }
    }
}
