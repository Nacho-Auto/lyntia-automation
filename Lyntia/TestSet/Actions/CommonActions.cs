using System;
using System.Threading;
using Allure.Commons;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lyntia.TestSet.Actions
{
    public class CommonActions
    {
        private static IWebDriver driver;
        private static WebDriverWait wait;

        public CommonActions()
        {
            driver = Utils.driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
        }

        /// <summary>
        /// Acceso a la Gestión de Cliente
        /// </summary>
        public void AccesoGestionCliente()
        {
            try
            {
                driver.SwitchTo().Frame(Utils.SearchWebElement("Modulo.frameModulos")); // Cambiar al frame de Apps   
                Utils.SearchWebElement("Modulo.gestionCliente").Click(); //modulo gestion de clientes     

                TestContext.WriteLine("Se accede correctamente a Gestión de Cliente");
            }
            catch (Exception e)
            {
                CapturadorExcepcion(e, "AccesoGestionCliente.png", "No se pudo acceder a Gestión de Cliente");
                throw e;
            }
        }

        /// <summary>
        /// Acceso a la sección Ofertas en la barra lateral izquierda
        /// </summary>
        public void AccesoOferta()
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(Utils.GetIdentifier("Oferta.ofertaSection"))));
                Utils.SearchWebElement("Oferta.ofertaSection").Click();
                TestContext.WriteLine("Se accede correctamente a sección de Ofertas");
            }
            catch (Exception e)
            {
                CapturadorExcepcion(e, "AccesoOfertas.png", "No se pudo acceder a la sección de Ofertas");
                throw e;
            }
        }

        /// <summary>
        /// Login a Lyntia, incluyendo el acceso previo
        /// </summary>
        public void Login()
        {
            try
            {
                Utils.SearchWebElement("Login.firstInput").SendKeys("rgomezs.ext@lyntia.com"); //usuario de lyntia
                Utils.SearchWebElement("Login.firstSubmitButton").Click();
                Utils.SearchWebElement("Login.secondInput").Clear();
                Utils.SearchWebElement("Login.secondInput").SendKeys("rgomezs@lyntia.com"); //usuario de entorno lyntia
                Utils.SearchWebElement("Login.thirdInput").SendKeys("W1nter21$"); //pass de entorno lyntia
                Utils.SearchWebElement("Login.secondSubmitButton").Click();
                Utils.SearchWebElement("Login.notPersistanceButton").Click(); //Desea mantener la sesion iniciada NO

                TestContext.WriteLine("Se realiza login de manera correcta");
            }
            catch (Exception e)
            {
                CapturadorExcepcion(e, "Login.png", "No se pudo realizar el login de forma correcta");
                throw e;
            }
        }

        /// <summary>
        /// Método capturador de excepciones, extensible a toda la aplicación.
        /// Realiza captura en formato .png del momento del error, histórico de
        /// la prueba y la excepción completa.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        public static void CapturadorExcepcion(Exception e, String fileName, String message)
        {
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fileName);
            AllureLifecycle.Instance.AddAttachment(fileName);
            TestContext.AddTestAttachment(fileName);

            TestContext.WriteLine(message);

            TestContext.WriteLine("-------------------- - ");
            TestContext.WriteLine("-------------------- - ");
            TestContext.WriteLine("Excepción: " + e);
        }
    }
}
