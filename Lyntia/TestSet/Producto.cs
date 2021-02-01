﻿using NUnit.Allure.Attributes;
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
            productoActions.CreacionProducto("Fibra oscura", "Backbone y OTT", "m. x2 fibras", "22", "IRU", "1000", "", "");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0004-ADD-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0004-ADD-PROD SE EJECUTÓ CORRECTAMENTE");
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
            productoActions.HeredarProducto("  CC 100 Mbps 22 - 22", "2000", "2", "1000", "", "");
            
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
            ofertaActions.RellenarCamposOferta("CRM-APR0009-HEREDAR-PROD_" + Utils.GetRandomString(), "CLIENTE INTEGRACION", "Cambio de solución técnica (Tecnología)", "# BizQA");
            ofertaActions.GuardarOferta();

            // Añadir Producto a la Oferta Circuito de Capacidad
            productoActions.HeredarProducto("  CC 100 Mbps 22 - 22", "2000", "2", "1000", "", "");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0009-HEREDAR-PROD_" + Utils.GetRandomString());

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
            productoActions.HeredarProducto("  CC 100 Mbps 22 - 22", "2000", "2", "1000", "", "");
            
            ofertaActions.AccesoOfertasLyntia("Mis Ofertas lyntia");

            ofertaActions.BuscarOfertaEnVista("CRM-APR0011-HEREDAR-PROD_" + Utils.GetRandomString());

            ofertaActions.SeleccionarOfertaGrid();

            ofertaActions.EliminarOfertaActual("Eliminar");

            TestContext.WriteLine("LA PRUEBA CRM-APR0011-HEREDAR-PROD SE EJECUTÓ CORRECTAMENTE");
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

            TestContext.WriteLine("LA PRUEBA CRM-APR0008 SE EJECUTÓ CORRECTAMENTE");
        }
    }
}