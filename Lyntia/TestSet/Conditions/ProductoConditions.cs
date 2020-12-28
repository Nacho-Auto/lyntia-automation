using Lyntia.TestSet.Actions;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Lyntia.TestSet.Conditions
{
	public class ProductoConditions
	{
		
        private static IWebDriver driver;
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;

        public ProductoConditions()
		{
            driver = Utils.driver;
            ofertaCondition = Utils.getOfertaConditions();
            productoActions = Utils.getProductoActions();
            productoCondition = Utils.getProductoConditions();
            commonActions = Utils.getCommonActions();
            commonCondition = Utils.getCommonConditions();
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
        }

        public void Resultado_Añadirproducto_vistarapida() 
        {
            Assert.AreEqual(true, driver.FindElement(By.Id("quickCreateLauncher_buttoncrm_header_global")).Enabled);
        }

		public void Resultado2_Editar_añadir_producto()
        {
            driver.FindElement(By.XPath("//button[contains(@data-id, 'quickCreateSaveAndCloseBtn')]")).Click();//Guardar
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", driver.FindElement(By.XPath("/html/body/section/div/div/div/div/section/div[1]/div/div/div/span[2]")).Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Oferta: Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'quoteid-error-message')]")).Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'lyn_uso-error-message')]")).Text);//comprobamos advertencia Uso linea de negocio
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Cancelar')]")).Click();//boton cancelar añadir producto
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Guardar y continuar')]")).Click();//pulsamos en guardar y continuar de se comprueba que siguen faltando los campos

            //se comprueba que los campos continuan sin registro
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", driver.FindElement(By.XPath("/html/body/section/div/div/div/div/section/div[1]/div/div/div/span[2]")).Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Oferta: Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'quoteid-error-message')]")).Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", driver.FindElement(By.XPath("//span[contains(@data-id, 'lyn_uso-error-message')]")).Text);//comprobamos advertencia Uso linea de negocio
            driver.FindElement(By.XPath("//button[contains(@aria-label, 'Cancelar')]")).Click();//boton cancelar añadir producto
            driver.FindElement(By.XPath("/html/body/section[2]/div/div/div/div/div/div/div[1]/div/button/span")).Click();//cerrar
        }
    }
}
