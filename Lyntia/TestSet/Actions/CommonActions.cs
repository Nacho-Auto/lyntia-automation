using System;
using Allure.Commons;
using Lyntia.Utilities;
using OpenQA.Selenium;

namespace Lyntia.TestSet.Actions
{
	public class CommonActions
	{
        readonly Utils utils = new Utils();

        private static IWebDriver driver;

        public CommonActions()
        {
            driver = Utils.driver;
        }

        /// <summary>
        /// Acceso a la Gestión de Cliente
        /// </summary>
        public void AccesoGestionCliente()
        {
            try
            { 
                driver.SwitchTo().Frame(Utils.searchWebElement("Modulo.frameModulos")); // Cambiar al frame de Apps   
                Utils.searchWebElement("Modulo.gestionCliente").Click(); //modulo gestion de clientes     

                Console.WriteLine("Se accede correctamente a Gestión de Cliente");

            }
            catch (NoSuchElementException e)
            {
                CapturadorExcepcion(e, "AccesoGestionCliente.png", "No se pudo acceder a Gestión de Cliente");
            }
        } 

        /// <summary>
        /// Acceso a la sección Ofertas en la barra lateral izquierda
        /// </summary>
        public void AccesoOferta()
        {
            try
            {
                driver.FindElement(By.XPath("//li[@title='Ofertas']")).Click();
                Console.WriteLine("Se accede correctamente a sección de Ofertas");

            }
            catch(NoSuchElementException e)
            {
                CapturadorExcepcion(e, "AccesoOfertas.png", "No se pudo acceder a la sección de Ofertas");
            }

        }

        /// <summary>
        /// Login a Lyntia, incluyendo el acceso previo
        /// </summary>
        public void Login()
        {
            try
            { 
                Utils.searchWebElement("Login.firstInput").SendKeys("rgomezs.ext@lyntia.com"); //usuario de lyntia
                Utils.searchWebElement("Login.firstSubmitButton").Click();
                Utils.searchWebElement("Login.secondInput").Clear();
                Utils.searchWebElement("Login.secondInput").SendKeys("rgomezs@lyntia.com"); //usuario de entorno lyntia
                Utils.searchWebElement("Login.thirdInput").SendKeys("W1nter20$"); //pass de entorno lyntia
                Utils.searchWebElement("Login.secondSubmitButton").Click();
                Utils.searchWebElement("Login.notPersistanceButton").Click(); //Desea mantener la sesion iniciada NO

                Console.WriteLine("Se realiza login de manera correcta");

            }
            catch (Exception e)
            {
                CapturadorExcepcion(e, "Login.png", "No se pudo realizar el login de forma correcta");
            }

        }

        /// <summary>
        /// Método capturador de excepciones, extensible a toda la aplicación
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        public static void CapturadorExcepcion(Exception e, String fileName, String message)
        {
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fileName);
            AllureLifecycle.Instance.AddAttachment(fileName);

            Console.WriteLine(message);

            Console.WriteLine("---------------------");
            Console.WriteLine("---------------------");
            Console.WriteLine("Excepción: " + e);
        }
    }
}
