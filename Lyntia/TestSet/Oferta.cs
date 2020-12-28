using Lyntia.TestSet.Conditions;
using Lyntia.Utilities;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using Lyntia.TestSet.Actions;

namespace Lyntia.TestSet
{

	[TestFixture]
    [AllureNUnit]
    [AllureSuite("OFERTA")]
    public class Oferta
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

        [Test(Description = "CRM-COF0001 Acceso a Ofertas")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0001_accesoOfertas()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

        }

        [Test(Description = "CRM-COF0002 Consultar Oferta")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0002_consultaOferta()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2A - Comprobar si hay alguna Oferta para abrir
            IWebElement element = null;
            if (utils.EncontrarElemento(By.XPath("//div[@title='No hay datos disponibles.']"), out element))
            {
                // Paso 2AA - Crear Oferta Nueva
                ofertaActions.AccesoNuevaOferta();
            }
            else
            {
                // Paso 2AB - Abrir Oferta existente 
                //div[@data-id='cell-1-4']
                ofertaActions.abrirOferta();
            }

        }

        [Test(Description = "CRM-COF0003 Creación de Oferta")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0003_creacionOferta()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Paso 3 - Cambiar a la pestaña Fechas
            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasSinInformar();

        }

        [Test(Description = "CRM-COF0004 Creación de Oferta sin informar campos obligatorios")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0004_creacionOfertaSinCamposObligatorios()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Paso 3 - Guardar sin campos obligatorios
            ofertaActions.GuardarOferta();
            ofertaCondition.OfertaNoCreada();

            // Paso 4 - Repetir el paso anterior pero pulsando Guardar y Cerrar
            ofertaActions.GuardarYCerrarOferta();
            ofertaCondition.OfertaNoCreada();

            // Paso 7 - Guardar informando solo el Cliente
            ofertaActions.RellenarCamposOferta("", "CLIENTE INTEGRACION", "", ""); ;
            ofertaActions.GuardarOferta();
            ofertaCondition.OfertaNoCreada();

            // Paso 8 - Guardar y cerrar informando solo el Cliente
            ofertaActions.GuardarYCerrarOferta();
            ofertaCondition.OfertaNoCreada();

            driver.Navigate().Refresh();
            driver.SwitchTo().Alert().Accept();

            // Paso 5 - Guardar informando solo el Nombre
            ofertaActions.RellenarCamposOferta("TEST", "", "", ""); ;
            ofertaActions.GuardarOferta();
            ofertaCondition.OfertaNoCreada();

            // Paso 6 - Guardar y cerrar informando solo el Nombre
            ofertaActions.GuardarYCerrarOferta();
            ofertaCondition.OfertaNoCreada();

        }

        [Test(Description = "CRM-COF0005 Creación de Oferta de tipo Nuevo servicio")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0005_creacionOfertaNuevoServicio()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Paso 3 - Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-COF0005", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0005", "Nuevo servicio");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0005", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0005");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0005");
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0005", "Nuevo servicio");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        [Test(Description = "CRM-COF0006 Creación de Oferta de tipo Cambio de capacidad")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0006_creacionOfertaCambioCapacidad()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Paso 3 - Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-COF0006", "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0006", "Cambio de capacidad (Upgrade/Downgrade)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0006", "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0006");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0006");
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0006", "Cambio de capacidad (Upgrade/Downgrade)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        [Test(Description = "CRM-COF0005 Eliminar Oferta en borrador con producto añadido")]
        [AllureSubSuite("PRO ELIMINAR OFERTA")]
        public void CRM_COF0005_eliminarOfertaProductoAnadido()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0005-ELIMINAR", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps");

            // Paso 3 y 4 - Pulsar Eliminar en la barra de herramientas y cancelar la eliminacion
            ofertaActions.EliminarOfertaActual("Cancelar");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0005-ELIMINAR");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0005-ELIMINAR");

            // Paso 5 - Eliminar definitivamente
            ofertaActions.EliminarOfertaActual("Eliminar");
        }

        [Test(Description = "CRM-COF0006 Eliminar Oferta en borrador con producto añadido desde el grid")]
        [AllureSubSuite("PRO ELIMINAR OFERTA")]
        public void CRM_COF0006_eliminarOfertaProductoAnadidoDesdeGrid()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0006-ELIMINAR", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF0006-ELIMINAR");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();

            // Paso 3 - Pulsar Eliminar en la barra de herramientas y cancelar la eliminacion
            ofertaActions.EliminarOfertaActual("Cancelar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-COF0006-ELIMINAR");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 4 - Repetir paso anterior eliminando la oferta
            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        //CRM-EOF0003
        [Test]
        [AllureSubSuite("PRO EDITAR OFERTA")]
        public void CRM_EOF0003_Editar_campos_de_una_oferta()
        {
            // Paso 1
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            //Paso 2
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            //Paso 3
            ofertaActions.SeleccionOferta();//hacemos click en una oferta del listado
            ofertaCondition.AccederSeleccionOferta();//accede a la oferta

            //Paso 4
            ofertaActions.IntroduccirDatos();//introduccir campos de la oferta
            ofertaCondition.IntroduccionDatos();//los datos se introduccen correctamente

            // Paso - Reestabñecer datos
            //ofertaActions.Restablecimiento_CRM_COF0003(driver);
        }


        //CRM-EOF0004
        [Test]
        [AllureSubSuite("PRO EDITAR OFERTA")]
        public void CRM_EOF0004_Editar_campo_tipo_de_Oferta()
        {
            // Paso 1
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            //Paso 2
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            //Paso 3
            ofertaActions.Tipo_de_oferta_Cambiodecapacidad();
            ofertaCondition.Aviso_cambiocapacidad();

            //Paso 4
            ofertaActions.Tipo_de_oferta_Cambiodeprecio();
            ofertaCondition.Aviso_Cambiodeprecio();

            //Paso 5
            ofertaActions.Tipo_de_oferta_Cambiodesolucion();
            ofertaCondition.Aviso_Cambiodesolucion();

            //Paso 6
            ofertaActions.Tipo_de_oferta_Cambiodedireccion();
            ofertaCondition.Aviso_Cambiodedireccion();

        }
    }
}