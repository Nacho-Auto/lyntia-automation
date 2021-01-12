using Lyntia.TestSet.Actions;
using Lyntia.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Lyntia.TestSet.Conditions
{
	
	public class ProductoConditions
	{
		
        private static IWebDriver driver;
        private static OfertaConditions ofertaCondition;
        private static ProductoActions productoActions;
        private static ProductoConditions productoCondition;
        private static CommonActions commonActions;
        private static CommonConditions commonCondition;
        private static OpenQA.Selenium.Interactions.Actions accionesSelenium;

        public ProductoConditions()
		{
            driver = Utils.driver;
            ofertaCondition = Utils.getOfertaConditions();
            productoActions = Utils.getProductoActions();
            productoCondition = Utils.getProductoConditions();
            commonActions = Utils.getCommonActions();
            commonCondition = Utils.getCommonConditions();
            accionesSelenium = new OpenQA.Selenium.Interactions.Actions(driver);
        }

        public void Resultado_Añadirproducto_vistarapida()
        {
            Assert.AreEqual(true, Utils.searchWebElement("Producto.buttonCrearRegistroNuevo").Enabled);
        }
		
		public void Resultado2_Editar_añadir_producto()
        {
            Utils.searchWebElement("Producto.ButtonGuardar").Click();//Guardar
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.searchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Oferta: Es necesario rellenar los campos obligatorios.", Utils.searchWebElement("Producto.LabelOfertaCamposObligatorios").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.searchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio
            Utils.searchWebElement("Producto.buttonCancelar").Click();//boton cancelar añadir producto
            Utils.searchWebElement("Producto.buttonGuardaryContinuar").Click();//pulsamos en guardar y continuar de se comprueba que siguen faltando los campos

            //se comprueba que los campos continuan sin registro
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.searchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Oferta: Es necesario rellenar los campos obligatorios.", Utils.searchWebElement("Producto.LabelOfertaCamposObligatorios").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.searchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio
            Utils.searchWebElement("Producto.buttonCancelar").Click();//boton cancelar añadir producto
            Utils.searchWebElement("Producto.buttonCerrar").Click();//cerrar
        }
		
        public void Resultado_Añadir_producto_ciercuito_de_capacidad()
        {
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.searchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.searchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio 
            Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", Utils.searchWebElement("Producto.LabelUniVentaCamposObligatorios").Text);//comprobamos advertencia Unidad de venta
        }
        public void Resultado_Añadir_producto_circuito_de_capacidad_con_campos_oblitatorio()
        {
            Assert.AreEqual("Los cambios se han guardado.", Utils.searchWebElement("Producto.LabelCambiosSeHanGuardado").Text);//Mensajes indicando que se ha guardado correctamente
            
        }

        public void ProductoNoCreado()
        {
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.searchWebElement("Producto.notCreatedMessage").Text);//notificacion pendiente
        }

        public void Resultado_Agregar_servicio_heredado_y_guardar()
        {
            Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", Utils.searchWebElement("Producto.LabelNotificacionesPendientes3").Text);//Mensajes indicando que faltan campos
        }
        public void Resultado_Cumplimentar_campos_y_guardar()
        {
            Assert.AreEqual("Los cambios se han guardado.", Utils.searchWebElement("Producto.LabelCambiosSeHanGuardado").Text);//Mensajes indicando que se ha guardado correctamente
        }
        public void Resultado_Agregar_Producto_tipo_circuito_de_capacidad()
        {
            Assert.IsTrue(Utils.searchWebElement("Producto.LabelNotificacionesPendientes2").Text.Contains("notificaciones. Seleccione esta opción para verlas."));//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.searchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio 
            Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", Utils.searchWebElement("Producto.LabelUniVentaCamposObligatorios").Text);//comprobamos advertencia Unidad de venta
        }
        public void Resultado_Agregar_Liena_de_nogocio_y_Unidad_de_venta()
        {
            Assert.AreEqual(true, Utils.searchWebElement("Oferta.LabelGeneralPestaña").Enabled);//la pestaña general esta activa
            Assert.AreEqual("General", Utils.searchWebElement("Oferta.LabelGeneralPestaña").Text);//el texto en General
        }  
    }
}
