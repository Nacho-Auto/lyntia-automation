using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Lyntia.TestSet.Actions;
using Lyntia.TestSet.Conditions;
using System.Linq;
using OpenQA.Selenium.Support.UI;

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
        private static String randomString;
        private static Random random = new Random();

        public static OfertaActions getOfertaActions()
        {
            return ofertaActions;
        }

        public static String getRandomString()
        {
            return randomString;
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
            objRep.testDataReader(@"ObjectRepository.csv");
            dataRep = TestDataUtils.Instance;
            dataRep.testDataReader(@"DataRepository.csv");
            ofertaActions = new OfertaActions();
            ofertaCondition = new OfertaConditions();
            productoActions = new ProductoActions();
            productoCondition = new ProductoConditions();
            commonActions = new CommonActions();
            commonCondition = new CommonConditions();
            randomString = RandomString(15);

            driver.Navigate().GoToUrl("https://ufinetprep2.crm4.dynamics.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(45);

        }

        public bool EncontrarElemento(By by)
        {
            IWebElement element = null;
            try
            {
                element = driver.FindElement(by);

            }
            catch (NoSuchElementException)
            {

                return false;

            }
            return true;

        }

        
        public static string RandomString(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static IWebElement searchWebElement(String identificador)
        {
            try
            {
                switch (objRep.TypeObjectID(identificador))
                {
                    case "XPATH":
                        return driver.FindElement(By.XPath(objRep.ObjectID(identificador)));
                    case "ID":
                        return driver.FindElement(By.Id(objRep.ObjectID(identificador)));
                    default:
                        return null;
                }
            }catch(NoSuchElementException e)
            {
                Console.WriteLine("No se pudo interactuar con el elemento " + identificador + " de tipo " + objRep.TypeObjectID(identificador));
                Console.WriteLine("Excepción : " + e);

                return null;
            }
            
        }

        public static String getIdentifier(String identificador)
        {
            String ident = objRep.ObjectID(identificador);
            return ident;
        }
    }
}

