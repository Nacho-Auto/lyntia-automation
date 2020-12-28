using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Lyntia.TestSet.Actions;
using Lyntia.TestSet.Conditions;

namespace Lyntia.Utilities
{
    public class Utils
    {

        public static IWebDriver driver;
        public static ObjectRepositoryUtils objRep;
        public static TestDataUtils dataRep;
        private static OfertaActions ofertaActions;
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;



        public static OfertaActions getOfertaActions()
        {
            return ofertaActions;
        }

        public static OfertaConditions getOfertaConditions()
        {
            return ofertaCondition;
        }

        public static ProductoActions getProductoActions()
        {
            return productoActions;
        }

        public static ProductoConditions getProductoConditions()
        {
            return productoCondition;
        }

        public static CommonActions getCommonActions()
        {
            return commonActions;
        }

        public static CommonConditions getCommonConditions()
        {
            return commonCondition;
        }

        public void Instanciador()
        {
            driver = new ChromeDriver(@"C:\chromedriver");

            objRep = ObjectRepositoryUtils.Instance;
            dataRep = TestDataUtils.Instance;
            ofertaActions = new OfertaActions();
            ofertaCondition = new OfertaConditions();
            productoActions = new ProductoActions();
            productoCondition = new ProductoConditions();
            commonActions = new CommonActions();
            commonCondition = new CommonConditions();

            driver.Navigate().GoToUrl("https://ufinetprep2.crm4.dynamics.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

        }

        public bool EncontrarElemento(By by, out IWebElement element)
        {
            try
            {
                element = driver.FindElement(by);

            }
            catch (NoSuchElementException)
            {
                element = null;
                return false;

            }
            return true;

        }
    }
}
