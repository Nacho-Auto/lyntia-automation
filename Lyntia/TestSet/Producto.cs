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
            ofertaActions = Utils.GetOfertaActions();
            ofertaCondition = Utils.GetOfertaConditions();
            productoActions = Utils.GetProductoActions();
            productoCondition = Utils.GetProductoConditions();
            commonActions = Utils.GetCommonActions();
            commonCondition = Utils.GetCommonConditions();

            // Realizar login
            commonActions.Login();
        }

        [TearDown]
        public void Cierre()
        {
            driver.Quit();
        }

        [Test(Description = "CRM-APR0001 Añadir producto")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0001_añadir_producto()
        {
            //Paso 1 Login y acceso al modulo gestion de cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Aplicaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            //Paso 2
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            //Paso 3
            ofertaActions.Filtro_buscarEnestaVista("Prueba de productos");//hacemos click en una oferta del listado para añadir producto(filtro)
            ofertaCondition.AccederSeleccionOfertaAPR0001();//accede a la oferta

            //Paso 4
            productoActions.Editar_añadir_producto();//se pulsa añadir producto en la pestaña general y realizamos unas comprobaciones
            productoCondition.Resultado_Editar_añadir_producto();//se verifican cambios

            
        }

        [Test(Description = "CRM-APR0002 Añadir producto correctamente")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
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
        [Test(Description = "CRM-APR0003 Añadir producto Circuito de Capacidad correctamente")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0003_Producto_Añadir_CC()
        {
            //Paso 1 Login y acceso al modulo gestion de cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Aplicaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto             

            //Paso 2
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            //Paso 3
            ofertaActions.Filtro_buscarEnestaVista("Prueba de productos");//hacemos click en una oferta del listado para añadir producto(filtro)
            ofertaCondition.Resultado_Seleccion_de_oferta_Borrador();//se realizan comprobaciones

            //Paso 5
            productoActions.Agregar_Producto_tipo_circuito_de_capacidad("Circuitos de capacidad");//Se selecciona solo el producto existente y se guarda
            productoCondition.Resultado_Agregar_Producto_tipo_circuito_de_capacidad();//se realizan comprobaciones

            //Paso 6
            productoActions.Agregar_Liena_de_negocio_y_Unidad_de_venta("FTTT", "10");
            productoCondition.Resultado_Agregar_Liena_de_negocio_y_Unidad_de_venta();

            // Reestablece datos
            productoActions.Borrado_de_producto();
        }
        //CRM-APR0008
        [Test(Description = "CRM-APR0008 Añadir producto correctamente, Cambio de Capacidad")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0008_Producto_Cambio_de_Capacidad_CC()
        {
            //Paso 1 Login y acceso al modulo gestion de cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Aplicaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto             

            //Paso 2
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso 

            //Paso 3
            ofertaActions.Filtro_buscarEnestaVista("CRM_APR0008_Producto_Cambio_de_Capacidad_CC");

            //Paso 4
            productoActions.Agregar_servicio_heredado_y_guardar();
            productoCondition.Resultado_Agregar_servicio_heredado_y_guardar();

            //Paso 5
            productoActions.Cumplimentar_campos_y_guardar();
            productoCondition.Resultado_Cumplimentar_campos_y_guardar();

            // Reestablece datos
            productoActions.Borrado_de_producto();

        }
    }
}