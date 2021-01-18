using Lyntia.Utilities;
using NUnit.Framework;
using System.Threading;

namespace Lyntia.TestSet.Conditions
{

    public class ProductoConditions
    {     
        public void Resultado_Añadirproducto_vistarapida()
        {
            Assert.AreEqual(true, Utils.SearchWebElement("Producto.buttonCrearRegistroNuevo").Enabled);
        }

        public void Resultado2_Editar_añadir_producto()
        {
            Utils.SearchWebElement("Producto.ButtonGuardar").Click();//Guardar
            
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Oferta: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelOfertaCamposObligatorios").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio
            Utils.SearchWebElement("Producto.buttonCancelar").Click();//boton cancelar añadir producto
            Utils.SearchWebElement("Producto.buttonGuardaryContinuar").Click();//pulsamos en guardar y continuar de se comprueba que siguen faltando los campos

            //se comprueba que los campos continuan sin registro
           // Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
           // Assert.AreEqual("Oferta: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelOfertaCamposObligatorios").Text);//Mensajes indicando que faltan campos
            //Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio
           // Utils.SearchWebElement("Producto.buttonCancelar").Click();//boton cancelar añadir producto
           // Utils.SearchWebElement("Producto.buttonCerrar").Click();//cerrar
        }

        public void Resultado_Añadir_producto_ciercuito_de_capacidad()
        {
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio 
            Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelUniVentaCamposObligatorios").Text);//comprobamos advertencia Unidad de venta
        }

        public void Resultado_Añadir_producto_circuito_de_capacidad_con_campos_oblitatorio()
        {
            Assert.AreEqual("Los cambios se han guardado.", Utils.SearchWebElement("Producto.LabelCambiosSeHanGuardado").Text); //Mensajes indicando que se ha guardado correctamente
        }

        public void ProductoNoCreado()
        {
            Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.notCreatedMessage").Text); //notificacion pendiente
        }

        public void Resultado_Agregar_servicio_heredado_y_guardar()
        {
            Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes3").Text);//Mensajes indicando que faltan campos
        }

        public void Resultado_Cumplimentar_campos_y_guardar()
        {
            Assert.AreEqual("Los cambios se han guardado.", Utils.SearchWebElement("Producto.LabelCambiosSeHanGuardado").Text);//Mensajes indicando que se ha guardado correctamente
        }

        public void Resultado_Agregar_Producto_tipo_circuito_de_capacidad()
        {
            //Assert.IsTrue(Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text.Contains("notificaciones. Seleccione esta opción para verlas."));//Mensajes indicando que faltan campos
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio 
            Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelUniVentaCamposObligatorios").Text);//comprobamos advertencia Unidad de venta
        }

        public void Resultado_Agregar_Liena_de_negocio_y_Unidad_de_venta()
        {
            Assert.AreEqual(true, Utils.SearchWebElement("Oferta.LabelGeneralPestaña").Enabled);//la pestaña general esta activa
            Assert.AreEqual("General", Utils.SearchWebElement("Oferta.LabelGeneralPestaña").Text);//el texto en General
        }

        public void Resultado_Editar_añadir_producto()
        {
            Assert.AreEqual("Creación rápida: Producto de oferta", Utils.SearchWebElement("Producto.h1QuickHeaderTitle").Text);
            Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Producto existente: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.selectProductoExistente").Text);//comprobamos advertencia Producto existente
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio 
            Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelUniVentaCamposObligatorios").Text);//comprobamos advertencia Unidad de venta

            //Cancelamos y cerramos con sus comprobaciones
            Utils.SearchWebElement("Producto.buttonCancelar").Click();//boton cancelar añadir producto
            Assert.AreEqual("¿Desea guardar los cambios antes de salir de esta página?", Utils.SearchWebElement("Producto.LabelMensajeDeseaguardarcambios").Text);//Mensaje pop up
            Utils.SearchWebElement("Producto.buttonGuardaryContinuar").Click();//pulsamos en guardar y continuar de se comprueba que siguen faltando los campos
            Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
            Assert.AreEqual("Producto existente: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.selectProductoExistente").Text);//comprobamos advertencia Producto existente
            Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio 
            Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelUniVentaCamposObligatorios").Text);//comprobamos advertencia Unidad de venta
            Utils.SearchWebElement("Producto.buttonCancelar").Click();//boton cancelar añadir producto
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.buttonCerrar").Click();
            Thread.Sleep(2000);
            Utils.SearchWebElement("Producto.buttonCancelar").Click();//boton cancelar añadir producto
            Utils.SearchWebElement("Producto.LabelMensajeDescartarCambios").Click();//boton cerrar de creacion producto
            Assert.AreEqual(true, Utils.SearchWebElement("Oferta.buttonAgregarProducto").Enabled);//volvemos a la pagina de añadir producto
            Thread.Sleep(2000);
        }
    }
}
