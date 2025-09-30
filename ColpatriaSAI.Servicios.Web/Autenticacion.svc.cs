using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ColpatriaSAI.Negocio.Componentes;
using ColpatriaSAI.Servicios.Web.wsAccesscontrol;
using DynamicProxyLibrary.DynamicProxy;


namespace ColpatriaSAI.Servicios.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ServicioAuthenticacion : IAuthenticacion
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string ValidateUser(string userXml, string tipoIde)
        {
            //Instanciamos el objeto InvocarMetodoWS con la ruta del  wsdl"
           InvocarMetodoWS invocarws = new InvocarMetodoWS(System.Configuration.ConfigurationManager.AppSettings["WsAccesControl"]);
            // llmamos el metodo del webservice  que nos devuelve un resultado
            var resultado = invocarws.Invocarmetodo( "wsAccesscontrolSoap", "user_authentication_web", userXml, tipoIde);
            
           
           
            return resultado;
            // este es un ejemplo del resultado
            //<?xml version="1.0" encoding="utf-8"?><stream><header transaccion="" /><parameters><TipoDoc>CC</TipoDoc><NroDoc>79488100</NroDoc><Nombres>Jfcastroc</Nombres><Apellidos></Apellidos><Email>juan.castro@ui.colpatria.com</Email><IPUltimoIngreso>127.0.0.1</IPUltimoIngreso><UltimoIngreso>Jun 20 2011 11:10AM</UltimoIngreso><ClaveAsesor></ClaveAsesor></parameters><footer><codigo>0</codigo><mensaje>OK</mensaje><resumen>OK</resumen></footer></stream>


        }

    }
    
}
