using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ColpatriaSAI.Servicios.Web.PackagesExecutionService;
using ColpatriaSAI.Negocio.Componentes.ETLs;
using ColpatriaSAI.Negocio.Componentes.Utilidades;
using ColpatriaSAI.Negocio.Entidades;
using ColpatriaSAI.Negocio.Entidades.Informacion;
using System.Diagnostics;

namespace ColpatriaSAI.Servicios.Web.ETLs
{
    public class EjecucionETL
    {
        private InfoAplicacion info;
        private ETLRemota etl;
        private int idProceso;

        /// <summary>
        /// Operación que permite iniciar el llamado asíncrono del servicio PackagesExecutionService
        /// </summary>
        /// <param name="eTLRemota">Objeto que contiene el nombre de la ETL a ejecutar, así como el nombre de su archivo de configuración</param>
        /// <param name="parametros">Listado de parametros en la forma "Nombre-Valor" a enviar a la ETL</param>
        /// <param name="infoAplicacion">Objeto que contiene información del usuario que ejecuta la operación y su IP</param>
        public void EjecutarETL(ETLRemota eTLRemota, Dictionary<string, object> parametros, InfoAplicacion infoAplicacion)
        {
            ProcesoLiquidacion proceso = new ProcesoLiquidacion()
            {
                tipo = 7,
                liquidacion_id = eTLRemota.id,
                fechaInicio = DateTime.Now,
                estadoProceso_id = 20
            };
            idProceso = Proceso.registrarProceso(proceso);

            AppSettingsReader reader = new AppSettingsReader();
            string idApp = reader.GetValue("idPackagesExecutionService", String.Empty.GetType()).ToString();

            List<PackagesExecutionService.Variable> variables = new List<PackagesExecutionService.Variable>();

            foreach (KeyValuePair<string, object> pair in parametros)
            {
                variables.Add(new PackagesExecutionService.Variable() { Key = pair.Key, Value = pair.Value });
            }

            info = infoAplicacion;
            etl = eTLRemota;

            using (PackagesExecutionServiceClient client = new PackagesExecutionServiceClient())
            {
                client.BeginExecuteFromPackageFile(idApp, eTLRemota.packageFileName, eTLRemota.packageConfigFileName, variables.ToArray(), GetDataCallback, client);
            }
        }

        /// <summary>
        /// Delegado que se ejecuta una vez finaliza la llamada asíncrona al servicio PackagesExecutionService, y es usado para control de auditoría y errores
        /// </summary>
        /// <param name="asyncResult">Contiene el resultado del llamado asincrono</param>
        private void GetDataCallback(IAsyncResult asyncResult)
        {
            try
            {
                PackagesExecutionServiceClient client = (PackagesExecutionServiceClient)asyncResult.AsyncState;

                DTSResponse result = client.EndExecuteFromPackageFile(asyncResult);

                LoggingUtil logging = new LoggingUtil();

                if (result.Fail)
                    logging.Error(string.Join(", ", result.DtsErrors), LoggingUtil.Prioridad.Alta, "ETLRemotasSAI", TraceEventType.Error, info);
                else
                    logging.Auditoria(String.Format("La ETL {0}, se ejecutó satisfactoriamente", etl.packageFileName), LoggingUtil.Prioridad.Baja, "ETLRemotasSAI", info);
                Proceso.eliminarProceso(idProceso);
            }
            catch (Exception)
            {
                if(idProceso > 0)
                    Proceso.eliminarProceso(idProceso);
            }

        }
    }
}