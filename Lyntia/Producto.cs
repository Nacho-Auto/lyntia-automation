using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Lyntia
{
    [TestClass]
    class Producto
    {

    }

    public class ProductoAction
    {
        public void CreacionProducto(String productoExistente, String uso, String unidadVenta, IWebDriver driver)
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
            if (driver.FindElement(By.XPath("//input[contains(@data-id,'uomid')]")).GetAttribute("value").Equals("---"))
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
    }
}
