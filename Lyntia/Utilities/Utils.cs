using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Lyntia.TestSet.Actions;
using Lyntia.TestSet.Conditions;
using System.Linq;

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
        private static readonly Random random = new Random();

        public static OfertaActions GetOfertaActions()
        {
            return ofertaActions;
        }

        public static String GetRandomString()
        {
            return randomString;
        }

        public static OfertaConditions GetOfertaConditions()
        {
            return ofertaCondition;
        }

        public static ProductoActions GetProductoActions()
        {
            return productoActions;
        }

        public static ProductoConditions GetProductoConditions()
        {
            return productoCondition;
        }

        public static CommonActions GetCommonActions()
        {
            return commonActions;
        }

        public static CommonConditions GetCommonConditions()
        {
            return commonCondition;
        }

        public void Instanciador()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("window-size=1920x1080");

            driver = new ChromeDriver(chromeOptions);

            objRep = ObjectRepositoryUtils.Instance;
            objRep.TestDataReader(@"ObjectRepository.csv");
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
            IWebElement element;
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
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.*?¿%$/()ºªÇ-Ñ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static IWebElement SearchWebElement(String identificador)
        {
            try
            {
                return (objRep.TypeObjectID(identificador)) switch
                {
                    "XPATH" => driver.FindElement(By.XPath(objRep.ObjectID(identificador))),
                    "ID" => driver.FindElement(By.Id(objRep.ObjectID(identificador))),
                    _ => null,
                };
            }
            catch(NoSuchElementException e)
            {
                Console.WriteLine("No se pudo interactuar con el elemento " + identificador + " de tipo " + objRep.TypeObjectID(identificador));
                Console.WriteLine("Excepción : " + e);

                return null;
            }  
        }

        public static String GetIdentifier(String identificador)
        {
            String ident = objRep.ObjectID(identificador);
            return ident;
        }
    }
}

