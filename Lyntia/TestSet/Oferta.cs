using Lyntia.TestSet.Conditions;
using Lyntia.Utilities;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using Lyntia.TestSet.Actions;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;

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
        private static ProductoConditions productoConditions;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;
        private static WebDriverWait wait;

        [SetUp]
        public void Instanciador()
        {
            // Instanciador del driver
            utils.Instanciador();

            driver = Utils.driver;
            ofertaActions = Utils.GetOfertaActions();
            ofertaCondition = Utils.GetOfertaConditions();
            productoActions = Utils.GetProductoActions();
            productoConditions = Utils.GetProductoConditions();
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

        [Test(Description = "CRM-COF0001 Acceso a Ofertas")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0001_accesoOfertas()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas 
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            TestContext.WriteLine(Utils.dataRep.getDataFromRepository("CRM_COF0001", "MESSAGE"));
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
            ofertaActions.AbrirOferta();

            TestContext.WriteLine("LA PRUEBA CRM-COF0002 SE EJECUTÓ CORRECTAMENTE");
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

            TestContext.WriteLine("LA PRUEBA CRM-COF0003 SE EJECUTÓ CORRECTAMENTE");
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

            TestContext.WriteLine("LA PRUEBA CRM-COF0004 SE EJECUTÓ CORRECTAMENTE");
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
            ofertaActions.RellenarCamposOferta("CRM-COF0005_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0005_" + Utils.GetRandomString(), "Nuevo servicio");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0005_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0005");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0005_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0005_" + Utils.GetRandomString(), "Nuevo servicio");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0005 SE EJECUTÓ CORRECTAMENTE");
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
            ofertaActions.RellenarCamposOferta("CRM-COF0006E_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0006E_" + Utils.GetRandomString(), "Cambio de capacidad (Upgrade/Downgrade)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0006E_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0006E_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0006E_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0006E_" + Utils.GetRandomString(), "Cambio de capacidad (Upgrade/Downgrade)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0006 SE EJECUTÓ CORRECTAMENTE");
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
            ofertaActions.RellenarCamposOferta("CRM-COF0007_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de precio/Renovación", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0007_" + Utils.GetRandomString(), "Cambio de precio/Renovación");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0007_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de precio/Renovación", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0007_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0007_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0007_" + Utils.GetRandomString(), "Cambio de precio/Renovación");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0007 SE EJECUTÓ CORRECTAMENTE");
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
            ofertaActions.RellenarCamposOferta("CRM-COF0008_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de solución técnica (Tecnología)", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0008_" + Utils.GetRandomString(), "Cambio de solución técnica (Tecnología)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0008_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de solución técnica (Tecnología)", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0008_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0008_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0008_" + Utils.GetRandomString(), "Cambio de solución técnica (Tecnología)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0008 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-COF0009 Creación de Oferta de tipo Cambio de dirección (Migración)")]
        [AllureSubSuite("PRO CREAR OFERTA")]
        public void CRM_COF0009_creacionOfertaCambioDireccion()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Paso 3 - Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-COF0009_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de dirección (Migración)", "# BizQA");
            ofertaActions.GuardarOferta();

            driver.Navigate().Refresh();

            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0009_" + Utils.GetRandomString(), "Cambio de dirección (Migración)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 4 - Crear Nueva Oferta, pulsando Guardar y cerrar
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0009_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de dirección (Migración)", "# BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0009_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 5 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0009_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamente("CRM-COF0009_" + Utils.GetRandomString(), "Cambio de dirección (Migración)");

            ofertaActions.AccesoFechasOferta();
            ofertaCondition.FechasInformadasCorrectamente();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0009 SE EJECUTÓ CORRECTAMENTE");
        }

        //[Test(Description = "CRM-COF0005 Eliminar Oferta en borrador con producto añadido")]
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

            ofertaActions.RellenarCamposOferta("CRM-COF0005-ELIMINAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps", "", "", "", "", "");

            // Paso 3 y 4 - Pulsar Eliminar en la barra de herramientas y cancelar la eliminacion
            ofertaActions.EliminarOfertaActual("Cancelar");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Buscar Oferta creada
            ofertaActions.BuscarOfertaEnVista("CRM-COF0005-ELIMINAR_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("CRM-COF0005-ELIMINAR_" + Utils.GetRandomString());

            // Paso 5 - Eliminar definitivamente
            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0005 ELIMINAR SE EJECUTÓ CORRECTAMENTE");
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

            ofertaActions.RellenarCamposOferta("CRM-COF0006-ELIMINAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps", "", "", "4", "", "");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF0006-ELIMINAR_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();

            // Paso 3 - Pulsar Eliminar en la barra de herramientas y cancelar la eliminacion
            ofertaActions.EliminarOfertaActual("Cancelar");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-COF0006-ELIMINAR_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Paso 4 - Repetir paso anterior eliminando la oferta
            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0006 ELIMINAR SE EJECUTÓ CORRECTAMENTE");
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

            ofertaActions.RellenarCamposOferta("CRM-COF0007-CIERRE_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps", "", "", "4", "", "");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF0007-CIERRE_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.AbrirOfertaEnVista("CRM-COF0007-CIERRE_" + Utils.GetRandomString());

            // Paso 3 y 4 - Pulsar Cerrar en la barra de herramientas y cancelar el cierre
            ofertaActions.CerrarOfertaActual("Cancelar", "Cancelada", "Sin Información/otro motivo", "01/02/2021");

            // Paso 5 - Repetir el paso anterior pero cerrando sin completar campos obligatorios
            ofertaActions.CerrarOfertaActual("Aceptar", "Cancelada", "", "");
            ofertaCondition.OfertaNoCerrada();

            driver.Navigate().Refresh();

            // Paso 6 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.CerrarOfertaActual("Aceptar", "Cancelada", "Sin Información/otro motivo", "01/02/2021");

            driver.Navigate().Refresh();

            // Acceso a Ofertas y buscar la Oferta Cerrada
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 7 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.BuscarOfertaEnVista("CRM-COF0007-CIERRE_" + Utils.GetRandomString());
            ofertaCondition.OfertaCerradaCorrectamenteEnGrid("Cancelada");

            // Eliminar Oferta
            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0007 CERRAR SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-COF0008 Cerrar Oferta en borrador con producto añadido, No viable")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0008_cerrarOfertaProductoAnadidoNoViable_casoMOD()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0008-CIERRE_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "10 Mbps", "", "", "10", "", "");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF0008-CIERRE_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.AbrirOfertaEnVista("CRM-COF0008-CIERRE_" + Utils.GetRandomString());

            // Paso 6 - Repetir el paso anterior pero cerrando de manera correcta
            // QA nos indica que validemos hasta el paso 6, opcion NO viable ya no se encuentra. Se comenta las opciones afectadas para que salten el paso
            // ofertaActions.CerrarOfertaActual("Aceptar", "No viable", "Sin Información/otro motivo", "01/02/2021");

            driver.Navigate().Refresh();

            // Acceso a Ofertas y buscar la Oferta Cerrada
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 7 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.BuscarOfertaEnVista("CRM-COF0008-CIERRE_" + Utils.GetRandomString());
            // QA nos indica que validemos hasta el paso 6, opcion NO viable ya no se encuentra. Se comenta las opciones afectadas para que salten el paso
            // ofertaCondition.OfertaCerradaCorrectamenteEnGrid("No viable");

            // Eliminar Oferta
            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0008 CERRAR SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-COF0009 Cerrar Oferta en borrador con producto añadido, Perdida")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0009_cerrarOfertaProductoAnadidoPerdida()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Acceder a una Oferta que esté en estado Borrador con producto añadido.
            ofertaActions.AccesoNuevaOferta();

            ofertaActions.RellenarCamposOferta("CRM-COF0009-CIERRE_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "10 Mbps", "", "", "4", "", "");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF0009-CIERRE_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.AbrirOfertaEnVista("CRM-COF0009-CIERRE_" + Utils.GetRandomString());

            ofertaActions.PresentarOferta();

            // Paso 6 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.CerrarOfertaActual("Aceptar", "Perdida", "Sin Información/otro motivo", "01/02/2021");

            driver.Navigate().Refresh();

            // Acceso a Ofertas y buscar la Oferta Cerrada
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 7 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.BuscarOfertaEnVista("CRM-COF0009-CIERRE_" + Utils.GetRandomString());
            ofertaCondition.OfertaCerradaCorrectamenteEnGrid("Perdida");

            // Eliminar Oferta
            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0008 CERRAR SE EJECUTÓ CORRECTAMENTE");
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

            ofertaActions.RellenarCamposOferta("CRM-COF00010-CIERRE_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps", "", "", "4", "", "");

            // Volver al grid
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 Seleccionar la Oferta del grid
            ofertaActions.BuscarOfertaEnVista("CRM-COF00010-CIERRE_" + Utils.GetRandomString());
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            // Seleccionar la Oferta del grid
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.AbrirOfertaEnVista("CRM-COF00010-CIERRE_" + Utils.GetRandomString());

            // Presentar la Oferta
            ofertaActions.PresentarOferta();
            ofertaCondition.OfertaPresentada();

            // Paso 6 - Cerrar la Oferta actual como Revisada
            ofertaActions.CerrarOfertaActual("Aceptar", "Revisada", "", "");

            driver.Navigate().Refresh();

            // Acceso a Ofertas y buscar la Oferta Cerrada
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 7 - Se busca la Oferta, que debe aparecer duplicada
            ofertaActions.BuscarOfertaEnVista("CRM-COF00010-CIERRE_" + Utils.GetRandomString());
            ofertaCondition.OfertaRevisadaCorrectamente();

            ofertaActions.FiltrarPorIDRevision("0");
            ofertaCondition.OfertaCerradaCorrectamenteEnGrid("Revisada");

            ofertaActions.FiltrarPorIDRevision("1");
            ofertaCondition.OfertaGuardadaCorrectamenteEnGrid();

            driver.Navigate().Refresh();

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");
            ofertaActions.BuscarOfertaEnVista("CRM-COF00010-CIERRE_" + Utils.GetRandomString());

            // Eliminar las dos ofertas
            ofertaActions.SeleccionarTodasOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-COF0010 CERRAR ADJUDICADA SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM_EOF0003 Editar campos de una Oferta")]
        [AllureSubSuite("PRO EDI OFERTA")]
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

            // Paso 4 - Abrir la oferta anterior y comprobar datos cumplimentados
            ofertaActions.AbrirOfertaEnVista("Automatica_MOD");
            ofertaCondition.AccederSeleccionOferta(); //accede a la oferta

            // Paso 5
            ofertaActions.IntroduccirDatos(); //introduccir campos de la oferta
            ofertaCondition.IntroduccionDatos(); //los datos se introduccen correctamente

            // Paso 6 - Reestablecer datos
            ofertaActions.Restablecimiento_CRM_COF0003();

            TestContext.WriteLine("LA PRUEBA CRM-EOF0003 SE EJECUTÓ CORRECTAMENTE");
        }

        //CRM-EOF0004
        [Test(Description = "CRM_EOF0004 Editar campo 'Tipo de oferta' de una Oferta")]
        [AllureSubSuite("PRO EDI OFERTA")]
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

            TestContext.WriteLine("LA PRUEBA CRM-EOF0004 SE EJECUTÓ CORRECTAMENTE");
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
            commonActions.AccesoOferta();//Oferta menu
            ofertaActions.AccederOfertaestado_Adjudicada();
            ofertaCondition.Resultado_AccederOfertaestado_Adjudicada();

            // Paso 6 - Repetir pasos 2 y 3 Eliminar 
            ofertaActions.Eliminar_BarraMenu();
            ofertaCondition.Resultado_Eliminar_BarraMenu();

            // Paso 7 - Eliminar la oferta desde popup
            ofertaActions.Eliminar_Popup();
            ofertaCondition.Resultado_Eliminar_Popup();

            //Paso 6 - Regresar al grid de ofertas, seleccionar la Oferta Adjudicada con la que se trabaja.
            commonActions.AccesoOferta();//Oferta menu
            ofertaActions.Seleccionofertarazonadjudicada();

            //ofertaActions.editarOferta();
            ofertaCondition.Resultado_Seleccionofertarazonadjudicada();

            TestContext.WriteLine("LA PRUEBA CRM-COF0011 ELIMINAR SE EJECUTÓ CORRECTAMENTE");
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

            TestContext.WriteLine("LA PRUEBA CRM-COF0012 CERRAR SE EJECUTÓ CORRECTAMENTE");
        }


        [Test(Description = "CRM-POF0001 Presentar Oferta Circuito de Capacidad")]
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
            ofertaActions.RellenarCamposOferta("CRM-POF0001-PRESENTAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta Circuito de Capacidad
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "3 Mbps", "", "", "10", "", "");

            // Paso 3 - Presentar la oferta
            ofertaActions.PresentarOferta();

            // Paso 4 - Regresar al grid, verificar oferta
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-POF0001-PRESENTAR_" + Utils.GetRandomString());
            ofertaCondition.OfertaPresentadaCorrectamente();

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-POF0001 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-POF0002 Presentar Oferta Fibra Oscura")]
        [AllureSubSuite("PRO PRESENTAR OFERTA")]
        public void CRM_POF0002_PresentarOferta()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-POF0002-PRESENTAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta Fibra Oscura
            // Maximo NRC -> 922337203685477,00 €
            productoActions.creacionproductofibraoscuraIRU("Fibra oscura", "Backbone y OTT","IRU", "4", "3");

            // Paso 3 - Presentar la oferta
            ofertaActions.PresentarOferta();

            // Paso 4 - Regresar al grid, verificar oferta
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-POF0002-PRESENTAR_" + Utils.GetRandomString());
            ofertaCondition.OfertaPresentadaCorrectamente();

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-POF0002 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-POF0003 Presentar Oferta UbiRed Pro")]
        [AllureSubSuite("PRO PRESENTAR OFERTA")]
        public void CRM_POF0003_PresentarOferta_UbiRed_Pro()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-POF0003-PRESENTAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.CreacionProducto("UbiRed Pro", "FTTO", "", "", "", "4", "", "");

            // Paso 3 - Presentar la oferta
            ofertaActions.PresentarOferta();

            // Paso 4 - Regresar al grid, verificar oferta
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-POF0003-PRESENTAR_" + Utils.GetRandomString());
            ofertaCondition.OfertaPresentadaCorrectamente();

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-POF0003 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-POF0004 Presentar Oferta UbiRed Business")]
        [AllureSubSuite("PRO PRESENTAR OFERTA")]
        public void CRM_POF0004_PresentarOferta_UbiRed_Business()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-POF0004-PRESENTAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.CreacionProducto("UbiRed Business", "", "", "", "", "", "", "");

            // Paso 3 - Presentar la oferta
            ofertaActions.PresentarOferta();

            // Paso 4 - Regresar al grid, verificar oferta
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-POF0004-PRESENTAR_" + Utils.GetRandomString());
            ofertaCondition.OfertaPresentadaCorrectamente();

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-POF0004 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-POF0005 Presentar Oferta Rack")]
        [AllureSubSuite("PRO PRESENTAR OFERTA")]
        public void CRM_POF0005_PresentarOferta_Rack()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-POF0005-PRESENTAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.CreacionProducto("Rack", "FTTE", "600mm 24U", "", "", "", "12", "");

            // Paso 3 - Presentar la oferta
            ofertaActions.PresentarOferta();

            // Paso 4 - Regresar al grid, verificar oferta
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-POF0005-PRESENTAR_" + Utils.GetRandomString());
            ofertaCondition.OfertaPresentadaCorrectamente();

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-POF0005 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-POAF0001 - PRO ADJUDICAR OFERTA")] //Caso pendiente de desarrollo
        [AllureSubSuite("PRO ADJUDICAR OFERTA")]
        public void CRM_POAF0001_Oferta_Adjudicar_CC()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-POF0005-PRESENTAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTE", "1 Gbps", "", "", "4", "", "");

            // Paso 3 - Presentar la oferta
            ofertaActions.PresentarOferta();

            // Paso 4 - Adjudicar la oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 4 - Crear pedido
            ofertaActions.VentanaCrearPedido("27/04/2021");
            //ofertaCondition.ResVentanaCrearPedido();
        }

        [Test(Description = "CRM-POAF0002 - PRO ADJUDICAR OFERTA")]//Caso pendiente de desarrollo
        [AllureSubSuite("PRO ADJUDICAR OFERTA")]
        public void CRM_POAF0002_Ofeta_Adjudicar_FOC()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-POF0005-PRESENTAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.CreacionProducto("Fibra oscura", "FTTE", "", "3", "Lease", "4", "", "");

            // Paso 3 - Presentar la oferta
            ofertaActions.PresentarOferta();

            // Paso 4 - Adjudicar la oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 4 - Crear pedido
            ofertaActions.VentanaCrearPedido("27/03/2021");
            //ofertaCondition.ResVentanaCrearPedido();
        }

        [Test(Description = "CRM-EOF0001 - Edición_borrador_Sin_datos_obligatorios")]
        [AllureSubSuite("PRO EDI OFERTA")]
        public void CRM_EOF0001_Edición_borrador_Sin_datos_obligatorios()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Paso 4 - Rellenar campos de la oferta
            ofertaActions.RellenarCamposOferta("CRM-EOF0001_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "BizQA");

            // Paso 5 - Guardar oferta
            ofertaActions.GuardarOferta();

            // Paso 6 - Eliminar campos obligatorios
            ofertaActions.Eliminar_campos_obligatorios(1);

            // Paso 7 - Actualizar
            ofertaActions.ActualizarBarramenu();
            ofertaCondition.OfertaNoCreada();

            ofertaActions.EliminarOfertaActual("Eliminar");

            //TODO se realizan cambios en la aplicacion y se modifica en vs, se comenta por si esta solucion no es definitiva

            // Paso 7 - Actualizar y descartar cambios
            //ofertaActions.Actualizar("Descartar");
            //ofertaActions.Eliminar_campos_obligatorios(2);

            // Paso 8 - Eliminar campos, actualizar y guardar cambios
            //ofertaActions.Actualizar("Guardar");
            //ofertaCondition.OfertaNoCreada();

            //ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-EOF0001 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-EOF0002 - Edición_borrador_Con_datos_obligatorio")]
        [AllureSubSuite("PRO EDI OFERTA")]
        public void CRM_EOF0002_Edición_borrador_Con_datos_obligatorios()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Paso 4 - Rellenar campos de la oferta
            ofertaActions.RellenarCamposOferta("Prueba", "CLIENTE INTEGRACION", "Nuevo servicio", "BizQA");
            ofertaActions.GuardarOferta();

            // Paso 5 - Guardar oferta
            ofertaActions.Eliminar_campos_obligatorios(1);
            ofertaActions.Modificar_campos_obligatorios("CRM-EOF0002_" + Utils.GetRandomString(), "2k", "Cambio de dirección(Migración)", "BAP", "Euro", "2019 Porfolio lyntia");
            ofertaActions.GuardarOferta();

            // Paso 6 - Actualizar oferta
            ofertaActions.ActualizarBarramenu();

            // Paso 7 - Guardar y cerrar
            ofertaActions.GuardarYCerrarOferta();

            ofertaActions.BuscarOfertaEnVista("CRM-EOF0002_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-EOF0002 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-COF0001 - Oferta_Eliminar_En_borrador_Sin_Producto")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0001_Oferta_Eliminar_En_borrador_Sin_Producto()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0001-ELIMINAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "BizQA");
            ofertaActions.GuardarOferta();
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "10 Mbps", "", "", "4", "", "");
            ofertaActions.GuardarYCerrarOferta();

            // Paso 4 - Buscar en vista la oferta
            ofertaActions.AbrirOfertaEnVista("CRM-COF0001-ELIMINAR_" + Utils.GetRandomString());
            ofertaCondition.Resultado_edicion_de_una_oferta();

            // Paso 5 - Cancelar la eliminacion de la oferta
            ofertaActions.EliminarOfertaActual("Cancelar");

            // Paso 6 - Eliminar oferta
            ofertaActions.EliminarOfertaActual("Eliminar");
            ofertaActions.BuscarOfertaEnVista("CRM-COF0001-ELIMINAR_" + Utils.GetRandomString());
            ofertaCondition.Datos_disponibles();

            TestContext.WriteLine("LA PRUEBA CRM-COF0001 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-COF0002 - Vista Ofertas_En_borrador_Enelaboración_Sin_Producto")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0002_Vista_Ofertas_En_borrador_En_elaboración_Sin_Producto()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0002-ELIMINAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "BizQA");
            ofertaActions.GuardarYCerrarOferta();

            // Paso 4 - Buscar en vista la oferta
            ofertaActions.AbrirOfertaEnVista("CRM-COF0002-ELIMINAR_" + Utils.GetRandomString());
            ofertaCondition.Resultado_edicion_de_una_oferta();

            // Paso 5 - Cancelar la eliminacion de la oferta
            ofertaActions.EliminarOfertaActual("Cancelar");

            // Paso 6 - Salir al grid y eliminar oferta
            ofertaActions.GuardarYCerrarOferta();
            ofertaActions.BuscarOfertaEnVista("CRM-COF0002-ELIMINAR_" + Utils.GetRandomString());
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.EliminarOfertaActual("Eliminar");
            ofertaCondition.Datos_disponibles();
            TestContext.WriteLine("LA PRUEBA CRM-COF0002 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-COF0003 - Oferta_Cerrar_En_borrador_Sin_Producto_Cancelada")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0003_Oferta_Cerrar_En_borrador_Sin_Producto_Cancelada()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0003-CERRAR" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "BizQA");
            ofertaActions.GuardarOferta();

            // Paso 4 - Cerrar oferta("Cancelar")
            ofertaActions.CerrarOfertaActual("Cancelar", "", "", "");

            // Paso 5 - Cerrar oferta sin cumplimentar campos obligatorios
            ofertaActions.CerrarOfertaActual("Aceptar", "Cancelada", "", "");
            ofertaCondition.OfertaNoCerrada();

            driver.Navigate().Refresh();

            // Paso 6 - Repetir el paso anterior pero cerrando de manera correcta
            ofertaActions.CerrarOfertaActual("Aceptar", "Cancelada", "Sin Información/otro motivo", "01/02/2021");

            // Paso 7 - Accedemos al grid, buscamos la oferta y se comprueba estados
            commonActions.AccesoOferta();
            ofertaActions.BuscarOfertaEnVista("CRM-COF0003-CERRAR" + Utils.GetRandomString());
            ofertaCondition.OfertaCerradaCorrectamenteEnGrid("Cancelada");

            // Reestablecer dato
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.EliminarOfertaActual("Eliminar");
            TestContext.WriteLine("LA PRUEBA CRM-COF0003 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-COF0004 - Oferta_Cerrar_En_borrador_Sin_Producto_No_viable")]
        [AllureSubSuite("PRO ELIMINAR-CERRAR OFERTA")]
        public void CRM_COF0004_Oferta_Cerrar_En_borrador_Sin_Producto_No_viable_caso_MOD()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "BizQA");
            ofertaActions.GuardarOferta();

            // Paso 4 - Cerrar oferta("Cancelar")
            ofertaActions.CerrarOfertaActual("Cancelar", "", "", "");

            // Paso 5 - Cerrar oferta sin cumplimentar campos obligatorios
            // TODO: No se puede dejar sin informar campo obligatorios con la opcion de no viable, por lo que se pone cancelada para mostrar campos obligatorios
            ofertaActions.CerrarOfertaActual("Aceptar", "Cancelada", "", "");
            ofertaCondition.OfertaNoCerrada();

            driver.Navigate().Refresh();

            // Paso 6 - Repetir el paso anterior pero cerrando de manera correcta
            // QA nos indica que validemos hasta el paso 6, opcion NO viable ya no se encuentra. Se comenta las opciones afectadas para que salten el paso
            // ofertaActions.CerrarOfertaActual("Cancelar", "No viable", "Sin información", "01/02/2021"); 

            // Paso 7 - Accedemos al grid, buscamos la oferta y se comprueba estados
            commonActions.AccesoOferta();
            ofertaActions.BuscarOfertaEnVista("CRM-COF0004-CERRAR_" + Utils.GetRandomString());
            // QA nos indica que validemos hasta el paso 6, opcion NO viable ya no se encuentra. Se comenta las opciones afectadas para que salten el paso
            // ofertaCondition.OfertaCerradaCorrectamenteEnGrid("");

            // Reestablecer dato
            ofertaActions.SeleccionarOfertaGrid();
            ofertaActions.EliminarOfertaActual("Eliminar");
            TestContext.WriteLine("LA PRUEBA CRM-COF0004 SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-POAF0001 - Oferta/Adjudicar/varios CC")]
        [AllureSubSuite("PRO ADJUDICAR OFERTA")]
        public void CRM_POAF0001_Oferta_Adjudicar_varios_CC_circuito_de_capacidad()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTE", "10 Mbps", "", "", "4", "", "");
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "10 Mbps", "", "", "4", "", "");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Paso 8 - Copiar codigo administrativo del primer producto y busqueda en servicios contratados
            productoActions.BuscarCodigo_administrativo1();
            productoConditions.ResBuscarCodigo_administrativo();

            // Paso 9 - Buscar una oferta desde el servicio contratado
            productoActions.BuscarOferta_desde_servicio_contratado();
            ofertaCondition.ResBuscarOferta_desde_servicio_contratado();

            TestContext.WriteLine("LA PRUEBA CRM-POAF0001 - Oferta/Adjudicar/varios CC SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-POAF0002 - Oferta/Adjudicar/varios CC")]
        [AllureSubSuite("PRO ADJUDICAR OFERTA")]
        public void CRM_POAF0002_Oferta_Adjudicar_varios_CC_Fibra_oscura()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.creacionproductofibraoscuraIRU("Fibra oscura", "FTTE","IRU","20", "4");
            productoActions.CreacionProducto("Fibra oscura", "FTTT", "20", "2", "Lease", "2", "", "");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Paso 8 - Copiar codigo administrativo del primer producto y busqueda en servicios contratados
            productoActions.BuscarCodigo_administrativo1();

            // Paso 9 - Buscar una oferta desde el servicio contratado
            productoActions.BuscarOferta_desde_servicio_contratado();
            ofertaCondition.ResBuscarOferta_desde_servicio_contratado();

            TestContext.WriteLine("LA PRUEBA CRM-POAF0002 - Oferta/Adjudicar/varios CC SE EJECUTÓ CORRECTAMENTE");
        }


        [Test(Description = "CRM-POAF0003 - Oferta/Adjudicar/varios CC")]
        [AllureSubSuite("PRO ADJUDICAR OFERTA")]
        public void CRM_POAF0003_Oferta_Adjudicar_varios_CC_UbiredPRO()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.CreacionProducto("UbiRed Pro", "FTTT", "", "", "", "4", "", "");
            productoActions.CreacionProducto("UbiRed Pro", "FTTE", "", "", "", "7", "", "");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Paso 8 - Copiar codigo administrativo del primer producto y busqueda en servicios contratados
            productoActions.BuscarCodigo_administrativo1();
            productoConditions.ResBuscarCodigo_administrativo();

            // Paso 9 - Buscar una oferta desde el servicio contratado
            productoActions.BuscarOferta_desde_servicio_contratado();
            ofertaCondition.ResBuscarOferta_desde_servicio_contratado();

            TestContext.WriteLine("LA PRUEBA CRM-POAF0003 - Oferta/Adjudicar/varios CC SE EJECUTÓ CORRECTAMENTE");
        }

        [Test(Description = "CRM-POAF0004 - Oferta/Adjudicar/varios CC")]
        [AllureSubSuite("PRO ADJUDICAR OFERTA")]
        public void CRM_POAF0004_Oferta_Adjudicar_varios_CC_UbiRedBusiness()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.CreacionProducto("UbiRed Business", "", "", "", "", "", "2", "");
            productoActions.CreacionProducto("UbiRed Business", "", "", "", "", "", "4", "");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();


            // Paso 8 - Copiar codigo administrativo del primer producto y busqueda en servicios contratados
            productoActions.BuscarCodigo_administrativo2();


            // Paso 9 - Buscar una oferta desde el servicio contratado
            productoActions.BuscarOferta_desde_servicio_contratado();
            ofertaCondition.ResBuscarOferta_desde_servicio_contratado();

            TestContext.WriteLine("LA PRUEBA CRM-POAF0004 - Oferta/Adjudicar/varios CC SE EJECUTÓ CORRECTAMENTE");


        }


        [Test(Description = "CRM-POAF0005 - Oferta/Adjudicar/varios CC")]
        [AllureSubSuite("PRO ADJUDICAR OFERTA")]
        public void CRM_POAF0005_Oferta_Adjudicar_varios_CC_Rack()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.CreacionProducto("Rack", "FTTT", "", "", "", "", "3", "");
            productoActions.CreacionProducto("Rack", "FTTE", "", "", "", "", "2", "");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Paso 8 - Copiar codigo administrativo del primer producto y busqueda en servicios contratados
            productoActions.BuscarCodigo_administrativo1();
            productoConditions.ResBuscarCodigo_administrativo();

            // Paso 9 - Buscar una oferta desde el servicio contratado
            productoActions.BuscarOferta_desde_servicio_contratado();
            ofertaCondition.ResBuscarOferta_desde_servicio_contratado();

            TestContext.WriteLine("LA PRUEBA CRM-POAF0003 - Oferta/Adjudicar/varios CC SE EJECUTÓ CORRECTAMENTE");


        }

        [Test(Description = "CRM-SECAN0001 - Oferta/Servicio/CANCELACIÓN CC")]
        [AllureSubSuite("SERVICIO CANCELAR")]
        public void CRM_SECAN0001_Oferta_Servicio_CANCELACIÓN_CC()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTE", "1 Mbps", "", "", "4", "", "3");
            productoActions.CreacionProducto("Circuitos de capacidad", "FTTT", "10 Mbps", "", "", "4", "", "");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Paso 8 - Seleccion del primer registro de los 2 productos y cumplimentar datos obligatorios
            ofertaActions.Acceder_line1();
            ofertaActions.CamposObligatoriosProductoCC("BRODYNT");

            // Paso 9 - Seleccion del segundo registro de los 2 productos y cumplimentar datos obligatorios
            ofertaActions.Acceder_line2();
            ofertaActions.CamposObligatoriosProductoCC("BRODYNT");

            // Paso 10 - Enviar a Jira
            ofertaActions.Enviar_A_Jira();
            ofertaCondition.ResBuscarOferta_enviarJira();

            // Paso 11 - Cancelar motivo Jira
            ofertaActions.Enviar_A_Jira_cancelar();

        }

        [Test(Description = "CRM-SECAN0002 - Oferta/Servicio/CANCELACIÓN FO")]
        [AllureSubSuite("SERVICIO CANCELAR")]
        public void CRM_SECAN0002_Oferta_Servicio_CANCELACIÓN_FO()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.CreacionProducto("Fibra oscura", "FTTE", "10", "4", "Lease", "4", "", "");
            productoActions.CreacionProducto("Fibra oscura", "FTTT", "20", "2", "Lease", "2", "", "");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Paso 8 - Seleccion del primer registro de los 2 productos y cumplimentar datos obligatorios
            ofertaActions.Acceder_line1();
            ofertaActions.CamposObligatoriosProductoFIBRA("");

            // Paso 9 - Seleccion del segundo registro de los 2 productos y cumplimentar datos obligatorios
            ofertaActions.Acceder_line2();
            ofertaActions.CamposObligatoriosProductoFIBRA("");

            // Paso 10 - Enviar a Jira
            ofertaActions.Enviar_A_Jira();
            ofertaCondition.ResBuscarOferta_enviarJira();

            // Paso 11 - Cancelar motivo Jira
            ofertaActions.Enviar_A_Jira_cancelar();

        }

        [Test(Description = "CRM-SECAN0003 - Oferta/Servicio/CANCELACIÓN Ubired Pro")]
        [AllureSubSuite("SERVICIO CANCELAR")]
        public void CRM_SECAN0003_Oferta_Servicio_CANCELACIÓN_Ubired_Pro()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.CreacionProducto("UbiRed Pro", "FTTT", "", "", "", "4", "", "10");
            productoActions.CreacionProducto("UbiRed Pro", "FTTT", "", "", "", "4", "", "7");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Paso 8 - Seleccion del primer registro de los 2 productos y cumplimentar datos obligatorios
            ofertaActions.Acceder_line1();
            ofertaActions.CamposObligatoriosProductoUbiredPRO("");

            // Paso 9 - Seleccion del segundo registro de los 2 productos y cumplimentar datos obligatorios
            ofertaActions.Acceder_line2();
            ofertaActions.CamposObligatoriosProductoUbiredPRO("");

            // Paso 10 - Enviar a Jira
            ofertaActions.Enviar_A_Jira();
            ofertaCondition.ResBuscarOferta_enviarJira();

            // Paso 11 - Cancelar motivo Jira
            ofertaActions.Enviar_A_Jira_cancelar();
        }

        [Test(Description = "CRM-SECAN0004 - Oferta/Servicio/CANCELACIÓN RACK")]
        [AllureSubSuite("SERVICIO CANCELAR")]
        public void CRM_SECAN0004_Oferta_Servicio_CANCELACIÓN_RACK()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Paso 3 - Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            // Preparacion de datos de la prueba
            ofertaActions.RellenarCamposOferta("CRM-COF0004-CERRAR_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Paso 4 - Creamos 2 tipos de productos CC
            productoActions.CreacionProducto("Rack", "FTTT", "", "", "", "", "3", "");
            productoActions.CreacionProducto("Rack", "FTTE", "", "", "", "", "2", "");

            // Paso 5 - Presentamos oferta
            ofertaActions.PresentarOferta();

            // Paso 6 - Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Paso 7 - Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Paso 8 - Seleccion del primer registro de los 2 productos y cumplimentar datos obligatorios
            ofertaActions.Acceder_line1();
            ofertaActions.CamposObligatoriosProductoRACK("");

            // Paso 9 - Seleccion del segundo registro de los 2 productos y cumplimentar datos obligatorios
            ofertaActions.Acceder_line2();
            ofertaActions.CamposObligatoriosProductoRACK("");

            // Paso 10 - Enviar a Jira
            ofertaActions.Enviar_A_Jira();
            ofertaCondition.ResBuscarOferta_enviarJira();

            // Paso 11 - Cancelar motivo Jira
            ofertaActions.Enviar_A_Jira_cancelar();
            ofertaCondition.Resultado_Enviar_A_Jira_cancelar();
           
        }

        [Test(Description = "CRM-BILLING001 - Generar/Facturas")]
        public void CRM_BILLING001_Generar_Facturas()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();//Acceso al modulo de Gestion de Cliente(Apliaciones)
            commonCondition.AccedeGestionCliente();//Acceso correcto

            // Paso 1 - Hacer click en Ofertas
            commonActions.AccesoOferta();//Oferta menu
            commonCondition.AccedeOferta();//comprobamos el acceso

            // Nueva Oferta
            ofertaActions.AccesoNuevaOferta();
            ofertaCondition.CreaOferta();

            //Preparacion de datos de la prueba
            string nombreServicio = "CRM-BILLING001_TestPrueba" + DateTime.Now;
            ofertaActions.RellenarCamposOferta(nombreServicio, "CLIENTE INTEGRACION", "Nuevo servicio", "");
            ofertaActions.GuardarOferta();

            // Creamos 2 tipos de productos CC            
            productoActions.CreacionProducto("Fibra oscura", "FTTT", "20", "2", "Lease", "2", "", "");

            // Presentamos oferta
            ofertaActions.PresentarOferta("01/01/2021");

            // Adjudicamos oferta
            ofertaActions.Adjudicar_Oferta();
            ofertaCondition.ResAdjudicarOferta();

            // Creamos el pedido
            ofertaActions.VentanaCrearPedidofechaposterior();
            ofertaCondition.ResultadResVentanaCrearPedidofechaposterior();

            // Seleccion del primer registro de los 2 productos y cumplimentar datos obligatorios
            Utils.SearchWebElement("Oferta.inputUTPRx").Clear();
            Utils.SearchWebElement("Oferta.inputUTPRx").SendKeys("20");
            Utils.SearchWebElement("Oferta.inputCodigoTarea").Clear();
            Utils.SearchWebElement("Oferta.inputCodigoTarea").SendKeys("19");
            ofertaActions.Acceder_line1();
            ofertaActions.CamposObligatoriosProductoFIBRABilling("", "01/01/2021", "Mensual");

            //flujo nuevo
            //Thread.Sleep(2000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(Utils.GetIdentifier("Producto.PestañaRelacionados"))));
            Utils.SearchWebElement("Producto.PestañaRelacionados").Click();
            Utils.driver.FindElement(By.XPath("//div[@aria-label = 'Configuraciones de facturación Relacionados - Billing']")).Click();


            //Paso 4
            //Thread.Sleep(2000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("Producto.SelectColumn2")));
            Utils.SearchWebElement("Producto.SelectColumn2").Click();  
            string texto = Utils.SearchWebElement("Producto.ConfiguracionFacturacion.Cabecera.inputRazonEstado").Text;
            Thread.Sleep(2000);
            Assert.AreEqual("Borrador", Utils.SearchWebElement("Producto.ConfiguracionFacturacion.Cabecera.inputRazonEstado").Text);
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.PestañaDetallesDeLineas").Click();

            //Paso 5
            //Thread.Sleep(2000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@data-id, 'cell-0-2')]")));
            driver.FindElement(By.XPath("//div[contains(@data-id, 'cell-0-2')]")).Click();
            string tantoXCien1 = Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").GetAttribute("value");
            
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").Click();            
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").SendKeys("30");
            Utils.SearchWebElement("BarraHerramientas.buttonGuardaryCerrar").Click();

            //Paso 6 - Validar Oferta
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("Oferta.validar")));
            Utils.SearchWebElement("Oferta.validar").Click();
            Assert.AreEqual("El porcentaje del importe total correspondiente a las líneas normales es distinto de 100.00", Utils.SearchWebElement("Producto.alerta").Text);
            Utils.SearchWebElement("Producto.botonOk").Click();

            //Paso 7 - Modificar lineas para que la suma vuelva a dar 100
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@data-id, 'cell-0-2')]")));
            driver.FindElement(By.XPath("//div[contains(@data-id, 'cell-1-2')]")).Click();
            Thread.Sleep(2000);
            string tantoXCien2 = Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").GetAttribute("value");
            Utils.SearchWebElement("BarraHerramientas.buttonGuardaryCerrar").Click();

            //Thread.Sleep(4000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@data-id, 'cell-0-2')]")));
            driver.FindElement(By.XPath("//div[contains(@data-id, 'cell-0-2')]")).Click();
            Thread.Sleep(4000);
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").Click();            
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").SendKeys(Keys.Control + "a");
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").SendKeys(Keys.Delete);
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.inputTantoXCien").SendKeys(tantoXCien1);
            Utils.SearchWebElement("BarraHerramientas.buttonGuardaryCerrar").Click();
            Assert.AreEqual("100", (Double.Parse(tantoXCien1) + Double.Parse(tantoXCien2)).ToString());
            
            //Paso 8 - Validar Oferta
            Utils.SearchWebElement("Oferta.validar").Click();
            Assert.IsTrue(Utils.driver.FindElement(By.XPath("//button[@aria-label='Comprobar acceso']")).Displayed);            

            //Paso 9 - Comprobar estado Configuración facturacion Validado
            Utils.SearchWebElement("Producto.ConfiguracionFacturacion.PestañaCabecera").Click();
            //Thread.Sleep(3000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("Producto.ConfiguracionFacturacion.Cabecera.inputRazonEstado")));
            Assert.AreEqual("Validado", Utils.SearchWebElement("Producto.ConfiguracionFacturacion.Cabecera.inputRazonEstado").Text);

            //Paso 10 - Acceder a Modulo Billing
            Utils.SearchWebElement("Modulo.gestionCliente").Click();
            driver.SwitchTo().Frame("AppLandingPage");
            Utils.SearchWebElement("Modulo.billing").Click();
            driver.SwitchTo().DefaultContent();

            //Paso 11 - Crear nuevo Generador de Facturacion
            Utils.SearchWebElement("Producto.GeneradorFacturacion").Click();
            Utils.SearchWebElement("BarraHerramientas.buttonNuevo").Click();
            ofertaActions.rellenarCamposNuevoGeneradorFacturacion("CLIENTE INTEGRACION", nombreServicio);

            //Paso 12 - Generar Facturas
            //Thread.Sleep(3000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("BarraHerramientas.GenerarFacturas")));
            Utils.SearchWebElement("BarraHerramientas.GenerarFacturas").Click();
            commonActions.checkAlert();
            Utils.SearchWebElement("BarraHerramientas.GenerarFacturas").Click();
            commonActions.checkAlert();
            commonActions.checkAlert();
            //Thread.Sleep(6000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("Producto.botonOk")));
            Utils.SearchWebElement("Producto.botonOk").Click();
            Utils.SearchWebElement("BarraHerramientas.buttonGuardaryCerrar").Click();

            //Comprobar estado generador de facturacion completado 
            //Thread.Sleep(10000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@data-id, 'cell-0-8')]")));
            Assert.AreEqual("Completado",driver.FindElement(By.XPath("//div[contains(@data-id, 'cell-0-8')]")));

            Utils.SearchWebElement("Facturas").Click();

            driver.FindElement(By.XPath("//div[contains(@data-id, 'cell-0-2')]")).Click();
            Utils.SearchWebElement("BarraHerramientas.buttonMarcarRevisada").Click();
            Assert.IsTrue(Utils.SearchWebElement("BarraHerramientas.buttonEnviarNav").Displayed);
            Utils.SearchWebElement("BarraHerramientas.buttonEnviarNav").Click();
            Thread.Sleep(4000);
            Utils.SearchWebElement("Facturas").Click();
            Assert.AreEqual("Enviada a NAV", driver.FindElement(By.XPath("//div[contains(@data-id, 'cell-0-3')]")).Text);
        }
    }
}


