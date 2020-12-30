using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using Lyntia.TestSet.Conditions;
using OpenQA.Selenium;
using Lyntia.Utilities;
using Lyntia.TestSet.Actions;

namespace Lyntia.TestSet
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("PRODUCTO")]
    class Producto
    {

        readonly Utils utils = new Utils();

        private static IWebDriver driver;
        private static OfertaActions ofertaActions;
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;

        [SetUp]
        public void Instanciador()
        {
            // Instanciador del driver
            utils.Instanciador();

            driver = Utils.driver;
            ofertaActions = Utils.getOfertaActions();
            ofertaCondition = Utils.getOfertaConditions();
            productoActions = Utils.getProductoActions();
            productoCondition = Utils.getProductoConditions();
            commonActions = Utils.getCommonActions();
            commonCondition = Utils.getCommonConditions();

            // Realizar login
            commonActions.Login();
        }

        [TearDown]
        public void Cierre()
        {
            driver.Quit();
        }

        //CRM-APR0001
        [Test]
        [AllureSubSuite("PRO AÑADIR OFERTA")]
        public void CRM_APR0001_añadir_producto()
        {
            //Paso 1 Login y acceso al modulo gestion de cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Aplicaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            //Paso 2
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            //Paso 3
            ofertaActions.SeleccionOfertaAPR0001();//hacemos click en una oferta del listado para añadir producto
            ofertaCondition.AccederSeleccionOfertaAPR0001();//accede a la oferta

            //Paso 4
            ofertaActions.Editar_añadir_producto();//se pulsa añadir producto en la pestaña general y realizamos unas comprobaciones
            ofertaCondition.Resultado_Editar_añadir_producto();//se verifican cambios

            
        }

        //CRM-APR0002
        [Test]
        [AllureSubSuite("PRO AÑADIR OFERTA")]
        public void CRM_APR0002_añadir_producto_creacion_rapida()
        {
            //Paso 1 Login y acceso al modulo gestion de cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Aplicaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto             

            //Paso 2
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso             

            //Paso 3
            productoActions.Añadirproducto_vistarapida();//entramos en añadir producto vista rapida
            productoCondition.Resultado2_Editar_añadir_producto();//se accede a pantalla añadir producto
        }
        //CRM-APR0003
        [Test]
        [AllureSubSuite("PRO AÑADIR OFERTA")]
        public void CRM_APR0003_Producto_Añadir_CC()
        {
            //Paso 1 Login y acceso al modulo gestion de cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Aplicaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto             

            //Paso 2
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            //Paso 3
            ofertaActions.Seleccion_de_oferta_Borrador();//en el listado seleccionamos una oferta borrardor y pulsamos Enter
            ofertaCondition.Resultado_Seleccion_de_oferta_Borrador();//se realizan comprobaciones

            //Paso 5
            productoActions.Agregar_Producto_tipo_circuito_de_capacidad("Circuitos de capacidad");//Se selecciona solo el producto existente y se guarda
            productoCondition.Resultado_Agregar_Producto_tipo_circuito_de_capacidad();//se realizan comprobaciones

            //Paso 6
            productoActions.Agregar_Liena_de_nogocio_y_Unidad_de_venta("FTTT", "10");
            productoCondition.Resultado_Agregar_Liena_de_nogocio_y_Unidad_de_venta();

            // Reestablece datos
            productoActions.Borrado_de_producto();
        }
    }
}
