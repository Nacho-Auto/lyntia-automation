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
            if (driver.FindElement(By.XPath("//div[contains(@data-id,'uomid_selected_tag_text')]")).Text.Equals("---"))
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

        public void Añadirproducto_vistarapida() {
            driver.FindElement(By.Id("quickCreateLauncher_buttoncrm_header_global")).Click();
            driver.FindElements(By.XPath("//div[contains(@data-id, '__flyoutRootNode')]//button"))[5].Click();
            Thread.Sleep(2000);
        }
    }
}
