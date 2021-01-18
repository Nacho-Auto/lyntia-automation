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
        readonly Utils utils = new Utils();

        private static IWebDriver driver;

        public OfertaConditions()
        {
            driver = Utils.driver;
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
            Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@aria-label,'Hora de Fecha de creación')]")).Text.Equals(""));

            // Assert de Fecha de modificación vacía
            Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@data-id,'modifiedon')]")).Text.Equals(""));

            // Assert de Hora de modificación vacía
            Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@aria-label,'Hora de Fecha de modificación')]")).Text.Equals(""));

        }

        public void OfertaNoCreada()
        {
            // Assert de alerta con los campos obligatorios sin informar
            //div[@data-id="notificationWrapper"]
            Assert.IsTrue(driver.FindElements(By.XPath("//div[@data-id='notificationWrapper']")).Count > 0);
            driver.FindElement(By.XPath("//div[@data-id='notificationWrapper']")).Click();
        }

        public void FechasInformadasCorrectamente()
        {

            // Assert de Fecha de creación vacía
            Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@data-id,'createdon')]")).GetAttribute("value").Equals(""));

            // Assert de Hora de creación vacía
            Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@aria-label,'Hora de Fecha de creación')]")).GetAttribute("value").Equals(""));

            // Assert de Fecha de modificación vacía
            Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@data-id,'modifiedon')]")).GetAttribute("value").Equals(""));

            // Assert de Hora de modificación vacía
            Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@aria-label,'Hora de Fecha de modificación')]")).GetAttribute("value").Equals(""));
        }

        public void OfertaGuardadaCorrectamente(String nombreOferta, String tipoOferta)
        {
            // Nombre de Oferta
            Thread.Sleep(6000);
            Assert.AreEqual(nombreOferta, driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).GetAttribute("value"));

            // Cliente 
            Assert.AreEqual("CLIENTE INTEGRACION", driver.FindElement(By.XPath("//div[contains(@data-id,'customerid_selected_tag_text')]")).Text);

            // Razon para el estado
            Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//span[@aria-label='Razón para el estado']")).Text);

            driver.FindElement(By.XPath("//div[contains(@data-id,'customerid_selected_tag')]")).SendKeys(Keys.PageDown);

            Assert.AreEqual(tipoOferta, driver.FindElement(By.XPath("//select[@aria-label='Tipo de oferta']")).GetAttribute("title"));
            Thread.Sleep(1000);
        }

        public void OfertaGuardadaCorrectamenteEnGrid()
        {
            try
            {
                // Se encuentra en estado borrador
                Assert.AreEqual("Borrador", driver.FindElement(By.XPath("//div[@data-id='cell-0-7']")).GetAttribute("title"));

                // Se encuentra en Razon para el estado En elaboracion
                Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//div[@data-id='cell-0-8']")).GetAttribute("title"));
                Console.WriteLine("*Se cumple la condición de Oferta guardada Correctamente.");
            }catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "CondicionOfertaGuardada.png", "**No se cumple la condición de Oferta guardada Correctamente.");
            }
        }

        public void AccederSeleccionOfertaAPR0001()
        {
            Assert.AreEqual(true, driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Enabled);//comprobamos que añadir producto esta habilitado
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
            
            Assert.AreEqual("La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
            
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
            String AvisoPriv2 = driver.FindElement(By.XPath("/html/body/section/div/div/div/div/div/div/div[1]/div[2]/h2")).Text;//imprime en consola el texto
            Console.WriteLine(AvisoPriv2);
            driver.FindElement(By.XPath("//*[@id='cancelButton']")).Click();
            Thread.Sleep(2000);
        }

        public void Resultado_Seleccionofertarazonadjudicada()
        {


            Assert.AreEqual(false, utils.EncontrarElemento(By.XPath("//span[contains(@aria-label, 'Eliminar')]")));//se comprueba que el elemento no esta presente
            Assert.AreEqual(false, utils.EncontrarElemento(By.XPath("//span[contains(@aria-label, 'Cerrar Oferta')]")));//se comprueba que el elemento no esta presente
            

        }


        public void CerrarOfertaNoVisible()
        {
            Assert.AreEqual(false, utils.EncontrarElemento(By.XPath("//button[@title='Cerrar Oferta']")));
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

        public void OfertaPresentada()
        {
            Assert.AreEqual("Solo lectura: estado de este registro: Bloqueada", driver.FindElement(By.XPath("//span[@data-id='warningNotification']")).Text);
        }

        public void OfertaRevisadaCorrectamente()
        {
            Assert.AreEqual("2", Utils.SearchWebElement("Oferta.gridCellsOferta").GetAttribute("data-row-count"));
        }

        public void Resultado_Seleccion_de_oferta_Borrador()
        {
            Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Enabled);//la pestaña general esta activa
            Assert.AreEqual("General", driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Text);
        }
        public void Resultado_Agregar_Producto()
        {
            Assert.AreEqual(true, driver.FindElement(By.XPath("//input[contains(@aria-label, 'Producto existente, Búsqueda')]")).Enabled);//el campo producto existente esta habilitado
        }
        public void Resultado_edicion_de_una_oferta()
        {
            Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Enabled);//la pestaña general esta activa
            Assert.AreEqual("General", driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Text);

		}
        public void OfertaPresentadaCorrectamente()
        {
            // Se encuentra en estado borrador
            Assert.AreEqual("Bloqueada", driver.FindElement(By.XPath("//div[@data-id='cell-0-7']")).GetAttribute("title"));

            // Se encuentra en Razon para el estado En elaboracion
            Assert.AreEqual("Presentada", driver.FindElement(By.XPath("//div[@data-id='cell-0-8']")).GetAttribute("title"));
        }
    }
}
