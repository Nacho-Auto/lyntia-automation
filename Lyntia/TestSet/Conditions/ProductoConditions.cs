using Lyntia.TestSet.Actions;
using Lyntia.Utilities;
using NUnit.Framework;
using System;
using System.Threading;

namespace Lyntia.TestSet.Conditions
{

    public class ProductoConditions
    {
        public void Resultado2_Editar_añadir_producto()
        {
            try
            {
                Thread.Sleep(2000);
                Utils.SearchWebElement("Producto.ButtonGuardar").Click();//Guardar

                //Assert.AreEqual("Tiene 2 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
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
                TestContext.WriteLine("***Se verifica el resultado de añadir producto.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AssertResultadoAddProducto.png", "***No se verifica el resultado de añadir producto.");
                throw e;
            }
        }

        public void Resultado_Agregar_servicio_heredado_y_guardar()
        {
            try
            {
                //Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes3").Text);//Mensajes indicando que faltan campos
                TestContext.WriteLine("***Se verifica que faltan campos por informar.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AssertResultadoAddServicioHeredado.png", "***No se verifica los mensajes al faltar campos por informar.");
                throw e;
            }
        }

        public void Resultado_Cumplimentar_campos_y_guardar()
        {
            try
            {
                Assert.AreEqual("Los cambios se han guardado.", Utils.SearchWebElement("Producto.LabelCambiosSeHanGuardado").Text);//Mensajes indicando que se ha guardado correctamente
                TestContext.WriteLine("***Se verifica que los cambios del producto se han guardado correctamente.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AssertResultadoCumplimentarCampos.png", "***No se verifica que los cambios del producto se han guardado correctamente.");
                throw e;
            }
        }

        public void Resultado_Agregar_Producto_tipo_circuito_de_capacidad()
        {
            try
            {
                //Assert.IsTrue(Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text.Contains("notificaciones. Seleccione esta opción para verlas."));//Mensajes indicando que faltan campos
                Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio                                                                                                                                                                  
                //Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelUniVentaCamposObligatorios").Text);//comprobamos advertencia Unidad de venta
                TestContext.WriteLine("***Se verifica que quedan campos obligatorios por informar.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AssertAddProductoCC.png", "***No se verifica el mensaje de campos obligatorios por informar.");
                throw e;
            }
        }

        public void Resultado_Agregar_Linea_de_negocio_y_Unidad_de_venta()
        {
            try
            {
                Assert.AreEqual(true, Utils.SearchWebElement("Oferta.LabelGeneralPestaña").Enabled);//la pestaña general esta activa
                Assert.AreEqual("General", Utils.SearchWebElement("Oferta.LabelGeneralPestaña").Text);//el texto en General
                TestContext.WriteLine("***Se verifica que la pestaña general es la pestaña por defecto.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AssertSeccionGeneral.png", "***No se verifica que la pestaña general es la pestaña por defecto.");
                throw e;
            }
        }

        public void Resultado_Editar_añadir_producto()
        {
            try
            {
                //Assert.AreEqual("Creación rápida: Producto de oferta", Utils.SearchWebElement("Producto.h1QuickHeaderTitle").Text);
                //Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
                //Assert.AreEqual("Producto existente: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.selectProductoExistente").Text);//comprobamos advertencia Producto existente
                //Assert.AreEqual("Uso (Línea de negocio): Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelLineaNegCamposObligatorios").Text);//comprobamos advertencia Uso linea de negocio 
                //Assert.AreEqual("Unidad de venta: Es necesario rellenar los campos obligatorios.", Utils.SearchWebElement("Producto.LabelUniVentaCamposObligatorios").Text);//comprobamos advertencia Unidad de venta

                //Cancelamos y cerramos con sus comprobaciones
                Utils.SearchWebElement("Producto.buttonCancelar").Click();//boton cancelar añadir producto
                Assert.AreEqual("¿Desea guardar los cambios antes de salir de esta página?", Utils.SearchWebElement("Producto.LabelMensajeDeseaguardarcambios").Text);//Mensaje pop up
                Utils.SearchWebElement("Producto.buttonGuardaryContinuar").Click();//pulsamos en guardar y continuar de se comprueba que siguen faltando los campos
                                                                                   //Assert.AreEqual("Tiene 3 notificaciones. Seleccione esta opción para verlas.", Utils.SearchWebElement("Producto.LabelNotificacionesPendientes2").Text);//Mensajes indicando que faltan campos
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
                TestContext.WriteLine("***Se verifica la edición del producto.");
            }
            catch (Exception e)
            {
                CommonActions.CapturadorExcepcion(e, "AssertSeccionGenerla.png", "***No se verifica la edición del producto.");
                throw e;
            } 
        }
    }
}
