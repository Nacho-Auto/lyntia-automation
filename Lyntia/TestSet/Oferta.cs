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

            if (utils.EncontrarElemento(By.XPath("//div[@title='No hay datos disponibles.']")))
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
            ofertaActions.RellenarCamposOferta("CRM-COF0005_" + Utils.getRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0005_" + Utils.getRandomString(), "Nuevo servicio");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0005_" + Utils.getRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0005");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0005_" + Utils.getRandomString());
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0005_" + Utils.getRandomString(), "Nuevo servicio");

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

        [Test(Description = "CRM-COF0007 Creación de Oferta de tipo Cambio de Precio/Renovación")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0007_creacionOfertaCambioPrecioRenovacion()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Paso 3 - Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-COF0007", "CLIENTE INTEGRACION", "Cambio de precio/Renovación", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0007", "Cambio de precio/Renovación");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0007", "CLIENTE INTEGRACION", "Cambio de precio/Renovación", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0007");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0007");
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0007", "Cambio de precio/Renovación");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        [Test(Description = "CRM-COF0008 Creación de Oferta de tipo Cambio de Solución Técnica (Tecnología)")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0008_creacionOfertaCambioSolucionTecnica()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Paso 3 - Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-COF0008", "CLIENTE INTEGRACION", "Cambio de solución técnica (Tecnología)", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0008", "Cambio de solución técnica (Tecnología)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0008", "CLIENTE INTEGRACION", "Cambio de solución técnica (Tecnología)", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0008");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0008");
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0008", "Cambio de solución técnica (Tecnología)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        [Test(Description = "CRM-COF0009 Creación de Oferta de tipo Cambio de dirección (Migración)")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0009_creacionOfertaCambioSolucionTecnica()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Paso 3 - Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-COF0009", "CLIENTE INTEGRACION", "Cambio de dirección (Migración)", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0009", "Cambio de dirección (Migración)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0009", "CLIENTE INTEGRACION", "Cambio de dirección (Migración)", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0009");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0009");
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0009", "Cambio de dirección (Migración)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        [Test(Description = "CRM-COF0005 Eliminar Oferta en borrador con producto añadido")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
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
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
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

        [Test(Description = "CRM-COF0007 Cerrar Oferta en borrador con producto añadido")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0007_cerrarOfertaProductoAnadido()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0007-CIERRE", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF0007-CIERRE");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.AbrirOfertaEnVista("CRM-COF0007-CIERRE");

            // Paso 3 y 4 - Pulsar Cerrar en la barra de herramientas y cancelar el cierre
            ofertaActions.CerrarOfertaActual("Cancelar", "Cancelada", "Sin información");

            // Paso 5 - Repetir el paso anterior pero cerrando sin completar campos obligatorios
            ofertaActions.CerrarOfertaActual("Aceptar", "Cancelada", "");
            ofertaCondition.OfertaNoCerrada();

            driver.Navigate().Refresh();

            // Paso 6 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.CerrarOfertaActual("Aceptar", "Cancelada", "Sin información");

            driver.Navigate().Refresh();

            // Acceso a Ofertas y buscar la Oferta Cerrada
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 7 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.BuscarOfertaEnVista("CRM-COF0007-CIERRE");
            ofertaCondition.OfertaCerradaCorrectamenteEnGrid("Cancelada");

            // Eliminar Oferta
            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        [Test(Description = "CRM-COF0008 Cerrar Oferta en borrador con producto añadido, No viable")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0008_cerrarOfertaProductoAnadidoNoViable()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0008-CIERRE", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF0008-CIERRE");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.AbrirOfertaEnVista("CRM-COF0008-CIERRE");

            // Paso 6 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.CerrarOfertaActual("Aceptar", "No viable", "Sin información");

            driver.Navigate().Refresh();

            // Acceso a Ofertas y buscar la Oferta Cerrada
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 7 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.BuscarOfertaEnVista("CRM-COF0008-CIERRE");
            ofertaCondition.OfertaCerradaCorrectamenteEnGrid("No viable");

            // Eliminar Oferta
            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        [Test(Description = "CRM-COF00010 Cerrar Oferta Presentada con producto añadido, Revisada")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF00010_cerrarOfertaPresentadaProductoAnadidoRevisada()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF00010-CIERRE", "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF00010-CIERRE");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.AbrirOfertaEnVista("CRM-COF00010-CIERRE");

            // Presentar la Oferta
            ofertaActions.PresentarOferta();
            ofertaCondition.OfertaPresentada();

            // Paso 6 - Cerrar la Oferta actual como Revisada
            ofertaActions.CerrarOfertaActual("Aceptar", "Revisada", "");

            driver.Navigate().Refresh();

            // Acceso a Ofertas y buscar la Oferta Cerrada
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 7 - Se busca la Oferta, que debe aparecer duplicada
            ofertaActions.BuscarOfertaEnVista("CRM-COF00010-CIERRE");
            ofertaCondition.OfertaRevisadaCorrectamente();

            ofertaActions.FiltrarPorIDRevision("0");
            ofertaCondition.OfertaCerradaCorrectamenteEnGrid("Revisada");

            ofertaActions.FiltrarPorIDRevision("1");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            driver.Navigate().Refresh();

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");
            ofertaActions.BuscarOfertaEnVista("CRM-COF00010-CIERRE");

            // Eliminar las dos ofertas
            ofertaActions.SeleccionarTodasOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

        }

        [Test(Description = "CRM_EOF0003 Editar campos de una Oferta")]
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
            ofertaActions.BuscarOfertaEnVista("Automatica_MOD");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("Automatica_MOD");
            ofertaCondition.AccederSeleccionOferta();//accede a la oferta

            //Paso 4
            ofertaActions.IntroduccirDatos();//introduccir campos de la oferta
            ofertaCondition.IntroduccionDatos();//los datos se introduccen correctamente

            // Paso - Reestabñecer datos
            ofertaActions.Restablecimiento_CRM_COF0003();
        }


        //CRM-EOF0004
        [Test(Description = "CRM_EOF0004 Editar campo 'Tipo de oferta' de una Oferta")]
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

            ofertaActions.GuardarYCerrarOferta();
            ofertaActions.ReestablecerDatosCRM_EOF0004();

        }

        [Test(Description = "CRM-COF0011 Eliminar una Oferta Adjudicada")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0011_eliminarOferta_Adjudicada()
        {

            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            //Paso 2 - Acceder a una Oferta que esté en_en_estado_Adjudicada, con el check del listado;
            ofertaActions.AccederOfertaestado_Adjudicada();
            ofertaCondition.Resultado_AccederOfertaestado_Adjudicada();

            //Paso 3 - Pulsar Eliminar en la barra de herramientas.
            ofertaActions.Eliminar_BarraMenu();
            ofertaCondition.Resultado_Eliminar_BarraMenu();

            //Paso 4 - Cancelar la eliminación de la Oferta haciendo click en Cancelar 
            ofertaActions.Cancelar();
            ofertaCondition.Resultado_Cancelar();

            //Paso 5 - Repetir pasos 2 y 3 Acceso
            ofertaActions.AccederOfertaestado_Adjudicada();
            ofertaCondition.Resultado_AccederOfertaestado_Adjudicada();

            // Paso 6 - Repetir pasos 2 y 3 Eliminar 
            ofertaActions.Eliminar_BarraMenu();
            ofertaCondition.Resultado_Eliminar_BarraMenu();

            // Paso 7 - Eliminar la oferta desde popup

            ofertaActions.Eliminar_Popup();
            ofertaCondition.Resultado_Eliminar_Popup();

            //Paso 6 - Regresar al grid de ofertas, seleccionar la Oferta Adjudicada con la que se trabaja.
            ofertaActions.Seleccionofertarazonadjudicada();

            //ofertaActions.editarOferta();

            ofertaCondition.Resultado_Seleccionofertarazonadjudicada();
        }

        [Test(Description = "CRM-COF0012 Cerrar Oferta Adjudicada")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0012_Oferta_Cerrar_Adjudicada()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            //Paso 2 - Desde el listado de ofertas, seleccionamos una en estado ganada/adjudicada y se pulsa editar
            ofertaActions.Seleccionofertarazonadjudicada();

            ofertaCondition.CerrarOfertaNoVisible();

            ofertaActions.editarOferta();


            ofertaCondition.CerrarOfertaNoVisible();

        }


        [Test(Description = "CRM-POF0001 Presentar Oferta")]
        [AllureSubSuite("PRO PRESENTAR OFERTA")]
        public void CRM_POF0001_PresentarOferta()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-POF0001-PRESENTAR", "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA");
            ofertaActions.GuardarOferta();

            // Paso 3 - Presentar la oferta
            ofertaActions.PresentarOferta();

            // Paso 4 - Regresar al grid, verificar oferta
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-POF0001-PRESENTAR");
            ofertaCondition.OfertaPresentadaCorrectamente();

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");
        }

    }
}