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
            //productoCondition.Resultado_Editar_añadir_producto();//se verifican cambios

            TestContext.WriteLine("LA PRUEBA CRM-APR0001 SE EJECUTÓ CORRECTAMENTE");
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

            TestContext.WriteLine("LA PRUEBA CRM-APR0002 SE EJECUTÓ CORRECTAMENTE");
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
            productoActions.Agregar_Linea_de_negocio_y_Unidad_de_venta("FTTT", "10");
            productoCondition.Resultado_Agregar_Linea_de_negocio_y_Unidad_de_venta();

            // Reestablece datos
            productoActions.Borrado_de_producto();

            TestContext.WriteLine("LA PRUEBA CRM-APR0003 SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0004
        [Test(Description = "CRM-APR0004 Añadir producto de Fibra Oscura")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0004_Producto_Añadir_FibraOscura()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0004-ADD-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.creacionproductofibraoscuraIRU("Fibra oscura", "FTTT", "IRU", "20","3");     
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0004-ADD-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0004-ADD-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0005
        [Test(Description = "CRM-APR0005 Añadir producto de UbiRed Pro")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0005_Producto_Añadir_UbiredPremium()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0005-ADD-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.CreacionProducto("UbiRed Pro", "FTTT", "1 Gbps", "", "", "4", "", "","");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0005-ADD-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0005-ADD-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0006
        [Test(Description = "CRM-APR0006 Añadir producto de UbiRed Business")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0006_Producto_Añadir_UbiredBusiness()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0006-ADD-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.CreacionProducto("UbiRed Business", "", "", "", "", "", "2", "","");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0006-ADD-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0006-ADD-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0007
        [Test(Description = "CRM-APR0007 Añadir producto de Rack")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0007_Producto_Añadir_Rack()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0007-ADD-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Nuevo servicio", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la UbiRed Pro
            productoActions.CreacionProducto("Co-Location genérico", "", "", "", "", "4", "24", "3", "2U en Rack compartido");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0007-ADD-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0007-ADD-PROD SE EJECUTÓ CORRECTAMENTE");
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
            productoActions.Cumplimentar_campos_y_guardar("FTTT","10 Mbps");
            productoCondition.Resultado_Cumplimentar_campos_y_guardar();

            // Reestablece datos
            productoActions.Borrado_de_producto();

            TestContext.WriteLine("LA PRUEBA CRM-APR0008 SE EJECUTÓ CORRECTAMENTE");
        }

        //CRM-APR0009
        [Test(Description = "CRM-APR0009 Heredar producto Circuito de Capacidad en Oferta Cambio de Precio")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0009_Producto_heredar_CC_CambioPrecio()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0009-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de precio/Renovación", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta Circuito de Capacidad
            productoActions.HeredarProducto("  CC 100 Mbps 22 - 22", "20", "2", "4", "", "", "");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0009-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0009-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0010
        [Test(Description = "CRM-APR0010 Heredar producto Circuito de Capacidad en Oferta Cambio de Precio")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0010_Producto_heredar_CC_CambioPrecio()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0010-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de configuración o producto (Tecnología)", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta Circuito de Capacidad
            productoActions.HeredarProducto("  CC 100 Mbps 22 - 22", "20", "2", "50", "", "","");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0010-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0010-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0011
        [Test(Description = "CRM-APR0011 Heredar producto Circuito de Capacidad en Oferta Cambio de Dirección")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0011_Producto_heredar_CC_CambioDireccion()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0011-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de dirección (Migración)", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta Circuito de Capacidad
            productoActions.HeredarProducto("  CC 100 Mbps 22 - 22", "2000", "2", "1000", "", "","");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0011-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0011-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0012
        [Test(Description = "CRM-APR0012 Heredar producto Fibra Oscura en Oferta Cambio de Capacidad")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0012_Producto_heredar_FO_CambioCapacidad()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0012-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de capacidad (Upgrade/Downgrade)", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta FO
            productoActions.HeredarProducto("  FO m - m", "", "2", "", "1000", "2000","m. x2 fibras");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0012-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0012-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0013
        [Test(Description = "CRM-APR0013 Heredar producto Fibra Oscura en Oferta Cambio de Precio")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0013_Producto_heredar_FO_CambioPrecio()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0013-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de precio/Renovación", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta FO
            productoActions.HeredarProducto("  FO m - m", "", "2", "", "1000", "", "");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0013-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0013-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0014
        [Test(Description = "CRM-APR0014 Heredar producto Fibra Oscura en Oferta Cambio de Solución técnica")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0014_Producto_heredar_FO_CambioSolucion()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0014-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de configuración o producto (Tecnología)", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta FO
            productoActions.HeredarProducto("  FO m - m", "", "2", "", "1000", "2000","");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0014-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0014-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0015
        [Test(Description = "CRM-APR0015 Heredar producto Fibra Oscura en Oferta Cambio de Dirección")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0015_Producto_heredar_FO_CambioDireccion()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0015-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de dirección (Migración)", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta FO
            productoActions.HeredarProducto("  FO m - m", "", "2", "", "1000", "2000", "");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0015-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0015-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }
        
        //CRM-APR0016
        [Test(Description = "CRM-APR0016 Heredar producto UbiRed Pro en Oferta Cambio de Dirección")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0016_Producto_heredar_UbiRedPro_CambioDireccion()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0016-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de dirección (Migración)", "# BizQA");
            ofertaActions.GuardarOferta();

            // TODO: Añadir Producto a la Oferta UbuRed Pro
            productoActions.HeredarProducto("  UP 500 Mbps ddd", "4", "12", "200", "", "","");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0016-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0016-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }

        //CRM-APR0017
        [Test(Description = "CRM-APR0017 Heredar producto UbiRed Pro en Oferta Cambio de Precio")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0017_Producto_heredar_UbiRedPro_CambioPrecio()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0017-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de precio/Renovación", "# BizQA");
            ofertaActions.GuardarOferta();

            // TODO: Añadir Producto a la Oferta UbuRed Pro
            productoActions.HeredarProducto("  UP 500 Mbps ddd", "4", "12", "200", "", "","");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0017-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0017-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }

        //CRM-APR0018
        [Test(Description = "CRM-APR0018 Heredar producto UbiRed Pro en Oferta Cambio de Solución Técnica")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0018_Producto_heredar_UbiRedPro_CambioSolucion()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0018-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de configuración o producto (Tecnología)", "# BizQA");
            ofertaActions.GuardarOferta();

            // TODO: Añadir Producto a la Oferta UbuRed Pro
            productoActions.HeredarProducto("  UP 500 Mbps ddd", "3", "12", "200", "", "","");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0018-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0018-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }

        //CRM-APR0018
        [Test(Description = "CRM-APR0019 Heredar producto UbiRed Pro en Oferta Cambio de Dirección")]
        [AllureSubSuite("PRO AÑADIR PRODUCTO")]
        public void CRM_APR0019_Producto_heredar_UbiRedPro_CambioDireccion()
        {
            // Login y Acceso a Gestión de Cliente
            commonActions.AccesoGestionCliente();
            commonCondition.AccedeGestionCliente();

            // Paso 1 - Hacer click en Ofertas
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            // Paso 2 - Crear Nueva Oferta
            ofertaActions.AccesoNuevaOferta();

            // Rellenar campos y click en Guardar
            ofertaActions.RellenarCamposOferta("CRM-APR0019-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de dirección (Migración)", "# BizQA");
            ofertaActions.GuardarOferta();

            // TODO: Añadir Producto a la Oferta UbuRed Pro
            productoActions.HeredarProducto("  UP 500 Mbps ddd", "3", "12", "200", "", "","");

            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0019-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0019-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
        }
    }
}