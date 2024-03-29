﻿using System;
using System.Threading;
using Lyntia.TestSet.Actions;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lyntia.TestSet.Conditions
{
    public class OfertaConditions
    {
        readonly Utils utils = new Utils();
        private static IWebDriver driver;
        private static WebDriverWait wait;

        public OfertaConditions()
        {
            driver = Utils.driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        }

        public void CreaOferta()
        {
            try
            {
                // Assert de título "Nuevo Oferta" del formulario
                Assert.AreEqual("Nuevo Oferta: Sin guardar", driver.FindElement(By.XPath("//h1[@data-id='header_title']")).Text);

                // Assert de tab por defecto "General"
                Assert.IsTrue(driver.FindElement(By.XPath("//li[@aria-label='General']")).GetAttribute("aria-selected").Equals("true"));

                // Assert de Razón para el estado de la Oferta "En elaboración" 
                //Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//section[@id='quote information']//span[@aria-label='Razón para el estado']//span")).Text);

                // Assert de Tipo de Oferta Nuevo Servicio
                driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).SendKeys(Keys.PageDown);
                Assert.AreEqual(driver.FindElement(By.XPath("//select[contains(@aria-label,'Tipo de oferta')]")).GetAttribute("title"), "Nuevo servicio");

                driver.FindElement(By.XPath("//input[contains(@id,'referencia_oferta')]")).SendKeys(Keys.PageDown);
                driver.FindElement(By.XPath("//input[contains(@id,'referencia_oferta')]")).SendKeys(Keys.PageDown);

                // Assert de Divisa
                Assert.AreEqual("Euro", driver.FindElement(By.XPath("//div[contains(@data-id,'transactioncurrencyid_selected_tag_text')]")).GetAttribute("title"));

                TestContext.WriteLine("***Se cumplen las condiciones de crear oferta");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "CreaOferta.png", "***No se cumplen las condiciones para crear oferta correctamente");
                throw e;
            }

        }

        public void FechasSinInformar()
        {
            try
            {
                // Assert de Fecha de creación vacía
                Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@data-id,'createdon')]")).Text.Equals(""));

                // Assert de Hora de creación vacía
                //Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@aria-label,'Hora de Fecha creación')]")).Text.Equals(""));

                // Assert de Fecha de modificación vacía
                //Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@data-id,'modifiedon')]")).Text.Equals(""));

                // Assert de Hora de modificación vacía
                //Assert.IsTrue(driver.FindElement(By.XPath("//input[contains(@aria-label,'Hora de Fecha de modificación')]")).Text.Equals(""));

                TestContext.WriteLine("***Se cumplen las condiciones con fechas sin informar");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "FechasSinInformar.png", "***No se cumplen las condiciones con fechas sin informar correctamente");
                throw e;
            }

        }

        public void OfertaNoCreada()
        {
            try
            {
                // Assert de alerta con los campos obligatorios sin informar
                //div[@data-id="notificationWrapper"]
                Assert.IsTrue(driver.FindElements(By.XPath("//div[@data-id='notificationWrapper']")).Count > 0);
                driver.FindElement(By.XPath("//div[@data-id='notificationWrapper']")).Click();

                TestContext.WriteLine("***Se cumplen las condiciones de una oferta no creada");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "OfertaNoCreada.png", "***No se cumplen las condiciones de una oferta creada");
                throw e;
            }
        }

        public void FechasInformadasCorrectamente()
        {
            try
            {
                // Assert de Fecha de creación vacía
                Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@data-id,'createdon')]")).GetAttribute("value").Equals(""));

                // Assert de Hora de creación vacía
                Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@aria-label,'Hora de Fecha creación')]")).GetAttribute("value").Equals(""));

                if (utils.EncontrarElemento(By.XPath("//input[contains(@data-id,'modifiedon')]")))
                {
                    // Assert de Fecha de modificación vacía
                    Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@data-id,'modifiedon')]")).GetAttribute("value").Equals(""));

                    // Assert de Hora de modificación vacía
                    Assert.IsFalse(driver.FindElement(By.XPath("//input[contains(@aria-label,'Hora de Fecha de modificación')]")).GetAttribute("value").Equals(""));
                }                

                TestContext.WriteLine("***Las condiciones de fechas informadas correctamente han sido OK");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "FechasInformadascorrectamente.png", "***No se cumplen las condiciones de fechas informadas");
                throw e;
            }
        }

        public void OfertaGuardadaCorrectamente(String nombreOferta, String tipoOferta)
        {
            try
            {
                // Nombre de Oferta
                Thread.Sleep(6000);
                Assert.AreEqual(nombreOferta, driver.FindElement(By.XPath("//input[@aria-label='Nombre oferta']")).GetAttribute("value"));

                // Cliente 
                Assert.AreEqual("CLIENTE INTEGRACION", driver.FindElement(By.XPath("//div[contains(@data-id,'customerid_selected_tag_text')]")).Text);

                // Razon para el estado
                Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//div[@aria-label='Razón para el estado']")).Text);

                driver.FindElement(By.XPath("//div[contains(@data-id,'customerid_selected_tag')]")).SendKeys(Keys.PageDown);

                Assert.AreEqual(tipoOferta, driver.FindElement(By.XPath("//select[@aria-label='Tipo de oferta']")).GetAttribute("title"));
                Thread.Sleep(1000);

                TestContext.WriteLine("***Se cumplen las condiciones de guardado de oferta correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "OfertaGuardacorrectamente.png", "***No se cumplen las condiciones para guardar la oferta");
                throw e;
            }
        }

        public void OfertaGuardadaCorrectamenteEnGrid()
        {
            try
            {
                // Se encuentra en estado borrador
                Assert.AreEqual("Borrador", driver.FindElement(By.XPath("//div[@data-id='cell-0-7']")).GetAttribute("title"));

                // Se encuentra en Razon para el estado En elaboracion
                Assert.AreEqual("En elaboración", driver.FindElement(By.XPath("//div[@data-id='cell-0-8']")).GetAttribute("title"));
                TestContext.WriteLine("***Se cumple la condición de Oferta guardada Correctamente.");
            } catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "CondicionOfertaGuardada.png", "***No se cumple la condición de Oferta guardada Correctamente.");
                throw e;
            }
        }

        public void AccederSeleccionOfertaAPR0001()
        {
            try
            {
                Assert.AreEqual(true, driver.FindElement(By.XPath("//button[contains(@aria-label, 'Agregar producto')]")).Enabled);//comprobamos que añadir producto esta habilitado
                TestContext.WriteLine("***Se cumplen las condiciones de la seleccion de oferta");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AccederSeleccionofertaapr0001.pne", "***No se cumpe la condicion seleccion oferta");
                throw e;
            }
        }

        public void AccederSeleccionOferta()
        {
            try
            {
                Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Enabled);//la pestaña general esta activa
                Assert.AreEqual("General", driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Text);
                TestContext.WriteLine("***Se cumple la condicion");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AccederSeleccionOferta.png", "***No se cumple la condicion");
                throw e;
            }
        }

        public void IntroduccionDatos()//Comprobamos que todos los campos se introducen correctamente
        {
            try
            {
                String OfertaMOD = driver.FindElement(By.LinkText("Automatica_MODPrueba_auto_NO_borrar_MODIFICADA")).Text;//muestra por consola la nueva oferta modificada
                TestContext.WriteLine(OfertaMOD);

                TestContext.WriteLine("***La condicion se cumple");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Introducir datos.png", "***No se cumple la condicion");
                throw e;
            }
        }

        public void IntroduccionDatos2()//comprobamos todas las modificaciones realizadas
        {
            try
            {
                Assert.AreEqual("La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
                Assert.AreEqual("La oferta de tipo “Cambio de precio” no requiere envío a construcción ni cambiar el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
                Assert.AreEqual("La oferta de tipo “Cambio de tecnología” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);
                Assert.AreEqual(" La oferta de tipo “Migración” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(@data-id, 'warningNotification')]")).Text);

                TestContext.WriteLine("***La condicion introduccion de datos se cumple correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Introduccion Datos2.png", "***No se cumple la condicion para introduccir datos");
                throw e;
            }
        }

        public void Aviso_cambiocapacidad()//mensaje por el tipo de oferta
        {
            try
            {
                if(!utils.EncontrarElemento(By.XPath("//span[contains(text(),'Cambio de capacidad')]")))
                    driver.FindElement(By.Id("notificationIcon")).Click();
                Assert.AreEqual("La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(text(),'Cambio de capacidad')]")).Text);
                //               La oferta de tipo “Cambio de capacidad” requiere envío a construcción, pero no cambia el código administrativo

                TestContext.WriteLine("***La condicion de aviso cambio de capacidad se cumple");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Aviso cambio de capacidad.png", "***No se cumple la condicion de cambio de capacidad");
                throw e;
            }

        }

        public void Aviso_Cambiodeprecio()//mensaje por el tipo de oferta
        {
            try
            {
                Thread.Sleep(3000);
                if (!utils.EncontrarElemento(By.XPath("//span[contains(text(),'Cambio de precio')]")))
                    driver.FindElement(By.Id("notificationIcon")).Click();
                Assert.AreEqual("La oferta de tipo “Cambio de precio” no requiere envío a construcción ni cambiar el código administrativo", driver.FindElement(By.XPath("//span[contains(text(),'Cambio de precio')]")).Text);
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//button[contains(@aria-label, 'Guardar')]")).Click();//Guardar

                TestContext.WriteLine("***La condicion aviso cambio de precio se cumple correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Aviso cambio de precio.png", "***No se cumple la condicion de cambio de precio");
                throw e;
            }
        }

        public void Aviso_Cambiodesolucion()//mensaje por el tipo de oferta
        {
            try
            {
                Thread.Sleep(3000);
                if (!utils.EncontrarElemento(By.XPath("//span[contains(text(),'Cambio de tecnología')]")))
                    driver.FindElement(By.Id("notificationIcon")).Click();
                Assert.AreEqual("La oferta de tipo “Cambio de tecnología” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(text(),'Cambio de tecnología')]")).Text);
                driver.FindElement(By.XPath("//button[contains(@aria-label, 'Guardar')]")).Click();//Guardar

                TestContext.WriteLine("***Se cumple la condicion de aviso cambio de solucion");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Aviso cambio solucion.png", "***No se cumple la condicion de aviso cambio de solucion");
                throw e;
            }
        }

        public void Aviso_Cambiodedireccion()//mensaje por el tipo de oferta
        {
            try
            {
                Thread.Sleep(3000);
                if (!utils.EncontrarElemento(By.XPath("//span[contains(text(),'Migración')]")))
                    driver.FindElement(By.Id("notificationIcon")).Click();
                Assert.AreEqual("La oferta de tipo “Migración” requiere el envío a construcción y cambia el código administrativo", driver.FindElement(By.XPath("//span[contains(text(),'Migración')]")).Text);
                TestContext.WriteLine("***La condicion de aviso cambio de direccion es correcta");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Aviso cambio de direccion.png", "***No se cumple la condicion de aviso cambio de direccion");
                throw e;
            }

        }

        public void Resultado_AccederOfertaestado_Adjudicada()
        {
            try
            {
                Assert.AreEqual("Ganada", driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Text);//la oferta esta en estado ganada
                TestContext.WriteLine("***La condicion resultado acceso oferta adjudicada es correcta");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Resultado acceder oferta estado adjudicada.png", "***No se cumple la condicion para acceder a oferta en estado adjudicada");
                throw e;
            }
        }


        public void Resultado_Eliminar_BarraMenu()
        {
            try
            {
                Assert.AreEqual("Confirmar eliminación", driver.FindElement(By.XPath("//h1[contains(@aria-label, 'Confirmar eliminación')]")).Text);//se comprueba texto de la ventana emergente
                TestContext.WriteLine("***La condicion de confirmar borrado se cumple");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Resultado eliminar barra menu.png", "***No se cumple la condicion de confirmar borrado");
                throw e;
            }
        }

        public void Resultado_Cancelar()
        {
            try
            {
                Assert.AreEqual("Ganada", driver.FindElement(By.XPath("//div[contains(@title, 'Ganada')]")).Text);//la oferta esta en estado ganada
                TestContext.WriteLine("***Se cumple la condicion de cancelar");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Resultado cancela.png", "***No se cumple la condicion de cancelar");
                throw e;
            }
        }

        public void Resultado_Eliminar_Popup()
        {
            try
            {
                Assert.AreEqual("Acceso denegado", driver.FindElement(By.XPath("//h1[contains(@aria-label, 'Acceso denegado')]")).Text);//muestra un mensaje informativo
                String AvisoPriv = driver.FindElement(By.XPath("//h1[contains(@aria-label, 'Acceso denegado')]")).Text;//imprime en consola el texto
                TestContext.WriteLine(AvisoPriv);
                String AvisoPriv2 = driver.FindElement(By.XPath("/html/body/section/div/div/div/div/div/div/div[1]/div[2]/h2")).Text;//imprime en consola el texto
                TestContext.WriteLine(AvisoPriv2);
                driver.FindElement(By.XPath("//*[@id='cancelButton']")).Click();
                Thread.Sleep(2000);

                TestContext.WriteLine("***La condicion resultado eliminar pop up se cumple correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Resultado Eliminar popup.png", "***La condicion resultado eliminar pop up no se cumple correctamente");
                throw e;
            }
        }

        public void Resultado_Seleccionofertarazonadjudicada()
        {
            try
            {
                Assert.AreEqual(false, utils.EncontrarElemento(By.XPath("//span[contains(@aria-label, 'Eliminar')]")));//se comprueba que el elemento no esta presente
                Assert.AreEqual(false, utils.EncontrarElemento(By.XPath("//span[contains(@aria-label, 'Cerrar Oferta')]")));//se comprueba que el elemento no esta presente

                TestContext.WriteLine("***La condicion seleccion oferta adjudicada se cumple correctamente");
            }

            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Resultado seleccion oferta adjudicada.png", "***No se cumple la condicion seleccion oferta adjudicada");
                throw e;
            }
        }


        public void CerrarOfertaNoVisible()
        {
            try
            {
                Assert.AreEqual(false, utils.EncontrarElemento(By.XPath("//button[@title='Cerrar Oferta']")));
                TestContext.WriteLine("***La condicion cerrar oferta no visible es correcta");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Cerrar oferta no visible.png", "***La condicion cerrar oferta no visible no es correcta");
                throw e;
            }
        }


        public void OfertaNoCerrada()
        {
            try
            {
                // Se muestra label con mensaje "Por favor, completa los campos obligatorios"
                Assert.AreEqual("Por favor, completa los campos obligatorios", driver.FindElement(By.XPath("//p[@id='error']")).Text);
                TestContext.WriteLine("***La condicion oferta no cerrada se cumple correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Oferta no cerrada.png", "***No se cumple la condicion oferta no cerrada");
                throw e;
            }
        }


        public void OfertaCerradaCorrectamenteEnGrid(String razonEstado)
        {
            try
            {
                // Se encuentra en estado borrador
                Assert.AreEqual("Cerrada", driver.FindElement(By.XPath("//div[@data-id='cell-0-7']")).GetAttribute("title"));

                // Se encuentra en Razon para el estado En elaboracion
                Assert.AreEqual(razonEstado, driver.FindElement(By.XPath("//div[@data-id='cell-0-8']")).GetAttribute("title"));

                TestContext.WriteLine("***La condicion oferta cerrada correctamente en grid funciona correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Oferta cerrada correctamente en grid.png", "***No se cumple la condicion oferta cerrada en grid");
                throw e;
            }
        }

        public void OfertaPresentada()
        {
            try
            {
                Assert.AreEqual("Solo lectura: estado de este registro: Bloqueada", driver.FindElement(By.XPath("//span[@data-id='warningNotification']")).Text);
                TestContext.WriteLine("***Se cumple la condicion de oferta presentada");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Oferta presentada.png", "***No se cumple la condicion de oferta presentada");
                throw e;

            }
        }

        public void OfertaRevisadaCorrectamente()
        {
            try
            {
                Assert.AreEqual("2", Utils.SearchWebElement("Oferta.gridCellsOferta").GetAttribute("data-row-count"));
                TestContext.WriteLine("***Se cumple la condicion oferta revisada");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Oferta revisada correctamente.png", "***No se cumple la condicion oferta revisada correctamente");
                throw e;
            }
        }

        public void Resultado_Seleccion_de_oferta_Borrador()
        {
            try
            {
                Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Enabled);//la pestaña general esta activa
                Assert.AreEqual("General", driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Text);
                TestContext.WriteLine("***Se cumple la condicion resultado seleccion oferta en borrador");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Resultado seleccion oferta en borrador.png", "***No se cumple la condicion de resultado seleccionar oferta en borrador");
                throw e;
            }
        }
        public void Resultado_Agregar_Producto()
        {
            try
            {
                Assert.AreEqual(true, driver.FindElement(By.XPath("//input[contains(@aria-label, 'Producto existente, Búsqueda')]")).Enabled);//el campo producto existente esta habilitado
                TestContext.WriteLine("***Se cumple la condicion de producto agregado");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Resultado agregar producto.png", "***No se cumple la condicion de producto agregado");
                throw e;
            }
        }
        public void Resultado_edicion_de_una_oferta()
        {
            try
            {
                Assert.AreEqual(true, driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Enabled);//la pestaña general esta activa
                Assert.AreEqual("General", driver.FindElement(By.XPath("//li[contains(@aria-label, 'General')]")).Text);
                TestContext.WriteLine("***Se cumple la condicion resultado edicion de una oferta");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Resultado edicion de una oferta.png", "***No se cumple la condicion resultado edicion de una ofera");
                throw e;
            }

        }
        public void OfertaPresentadaCorrectamente()
        {
            try
            {
                // Se encuentra en estado borrador
                Assert.AreEqual("Bloqueada", driver.FindElement(By.XPath("//div[@data-id='cell-0-7']")).GetAttribute("title"));

                // Se encuentra en Razon para el estado En elaboracion
                Assert.AreEqual("Presentada", driver.FindElement(By.XPath("//div[@data-id='cell-0-8']")).GetAttribute("title"));

                TestContext.WriteLine("***Se cumple la condicion de oferta presentada correctamente");

            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Oferta presentada correctamente.png", "***No se cumple la condicion de oferta presentada correctamente");
                throw e;
            }

        }

        public void ResAdjudicarOferta()
        {
            try
            {                
                Assert.AreEqual("Crear Proyecto", Utils.SearchWebElement("Oferta.labelCrearpedido").Text);               
                TestContext.WriteLine("***Se cumple la condicion de oferta adjudicada correctamente");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ResAdjudicarOferta.png", ("*** No Se cumple la condicion de oferta adjudicada correctamente"));
                throw e;
            }
        }       

        public void ResVentanaCrearPedido()
        {
            try
            {
                Assert.AreEqual("Error al introducir fecha de adjudicación: no puede ser anterior a fecha de presentación.", Utils.SearchWebElement("Oferta.labelMensajeCreapedidofechainferior").Text);
                TestContext.WriteLine("***Se cumple la condicion de advertencia, fecha inferior");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ResAdjudicarOferta.png", ("*** No cumple la condicion de advertencia, fecha inferior"));
                throw e;
            }
        }
        /// <summary>
        /// Método para comprobar que no hay datos disponibles de una oferta
        /// </summary>
        public void Datos_disponibles()
        {
            try
            {
                Assert.AreEqual("No hay datos disponibles.", Utils.SearchWebElement("Oferta.labelNOhayDatosDisponibles").Text);
                TestContext.WriteLine("*** Se cumple la condicion de que no existe el dato");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "Datos no disponibles.png", " ***NO se cumple la condicion de que no existe el dato");
                throw e;
            }
        }

        /// <summary>
        /// Método para comprobar que se ha guardado un producto y queda registrado
        /// </summary>
        public void ResultadResVentanaCrearPedidofechaposterior()
        {
            try
            {
                Thread.Sleep(11000);                
                wait.Until(ExpectedConditions.ElementExists(Utils.getByElement("Producto.labelEnconstruccion")));
                Assert.AreEqual("En construcción", Utils.SearchWebElement("Producto.labelEnconstruccion").Text);
                TestContext.WriteLine("Existe en la oferta el producto contratado");
            }
            catch(Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ResultadResVentanaCrearPedidofechaposterior.png", "No existe en la oferta el producto contratado");
                throw e;
            }
        }

        public void ResultadResVentanaCrearPedidofechaposterior(string servicio)
        {
            try
            {
                Thread.Sleep(2000);
                Assert.AreEqual("En construcción", Utils.SearchWebElement("Producto.labelEnconstruccion").Text);
                TestContext.WriteLine("Existe en la oferta el producto contratado");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ResultadResVentanaCrearPedidofechaposterior.png", "No existe en la oferta el producto contratado");
                throw e;
            }
        }

        /// <summary>
        /// Método para comprobar que se ha generado una oferta y queda adjudicada
        /// </summary>
        public void ResBuscarOferta_desde_servicio_contratado()
        {
            try
            {
                Assert.AreEqual("Adjudicada", Utils.SearchWebElement("Oferta.labelAdjudicada").Text);
                TestContext.WriteLine("Existe la oferta en estado adjudicada");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "ResBuscarOferta_desde_servicio_contratad.png", "No Existe la oferta en estado adjudicada");
                throw e;
            }
        }



        /// <summary>
        /// Método para comprobar que se envia correctamente a Jira
        /// </summary>
        public void ResBuscarOferta_enviarJira()
        {
            Assert.AreEqual("Envío correcto.", Utils.SearchWebElement("Producto.EnviaraJiraenviocorrecto").Text);
        }

        /// <summary>
        /// Método para comprobar que se envia correctamente a Jira y el servicio queda cancelado
        /// </summary>
        public void Resultado_Enviar_A_Jira_cancelar()
        {
            Assert.AreEqual("Cancelado", Utils.SearchWebElement("Oferta.firstFromGrid").Text);
            Assert.AreEqual("Cancelado", Utils.SearchWebElement("Oferta.secondFromGrid").Text);
        }
    }

}

    

