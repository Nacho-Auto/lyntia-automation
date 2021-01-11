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
            ofertaActions.SeleccionOfertaAPR0001();//hacemos click en una oferta del listado para añadir producto
            ofertaCondition.AccederSeleccionOfertaAPR0001();//accede a la oferta

            //Paso 4
            ofertaActions.Editar_añadir_producto();//se pulsa añadir producto en la pestaña general y realizamos unas comprobaciones
            ofertaCondition.Resultado_Editar_añadir_producto();//se verifican cambios

            //Paso 5
            //actions.Cancelar_y_cerrar();
            //condition.Resultado_Cancelar_y_cerrar();
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

        [Test(Description = "CRM-APR0003 Añadir producto correctamente")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0003_añadir_producto_circuito_capacidad()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-APR0003-AÑADIR-PRODUCTO", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "", "");
            productoCondition.ProductoNoCreado();

            driver.Navigate().Refresh();

            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps");

            ofertaActions.EliminarOfertaActual("Eliminar");
        }

        [Test(Description = "CRM-APR0004 Añadir producto a Oferta de tipo 'Cambio de capacidad'")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0004_añadir_producto_circuito_capacidad_cambio_capacidad()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0004-AÑADIR-PRODUCTO", "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir producto heredado, sin cumplimentar datos obligatorios
            productoActions.HeredarProducto("","","","");

            driver.Navigate().Refresh();

            // Añadir producto heredado correctamente
            productoActions.HeredarProducto("  CC 100 Mbps 22 - 22", "2", "10", "3");

            ofertaActions.EliminarOfertaActual("Eliminar");
        }
    }
}