using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ColpatriaSAI.Negocio.Entidades;
using ColpatriaSAI.Negocio.Entidades.Informacion;

namespace ColpatriaSAI.Servicios.Web
{
    [ServiceContract]
    public interface IEjecucionETLs
    {
        /// <summary>
        /// Operación que obtiene todos los tipos de ETL remota existentes
        /// </summary>
        /// <returns>Listado de entidades de tipo 'TipoETLRemota' obtenidos</returns>
        [OperationContract]
        List<TipoETLRemota> ListarTiposETLRemota();

        /// <summary>
        /// Operación que obtiene todas las ETL remotas existentes
        /// </summary>
        /// <returns>Listado de entidades de tipo 'ETLRemota' obtenidas</returns>
        [OperationContract]
        List<ETLRemota> ListarETLsRemotas();

        /// <summary>
        /// Operación que obtiene una ETL remota dado su identificador
        /// </summary>
        /// <param name="id">Identificador de la ETL a obtener</param>
        /// <returns>Entidades de tipo 'ETLRemota' obtenida para el identificador especificado</returns>
        [OperationContract]
        ETLRemota ObtenerETLRemotaporId(int id);

        /// <summary>
        /// Operación que a partir de un tipo de ETL remota, obtiene todas las ETL remotas asociadas
        /// </summary>
        /// <param name="tipoETLRemota_id">Identificador del tipo de ETL remota a consultar </param>
        /// <returns>Listado de entidades de tipo 'ETLRemota' obtenidas para el tipo especificado</returns>
        [OperationContract]
        List<ETLRemota> ListarETLsRemotasporTipo(int tipoETLRemota_id);

        /// <summary>
        /// Operación que permite ejecutar una ETL remota
        /// </summary>
        /// <param name="eTLRemota">Objeto que contiene el nombre de la ETL a ejecutar, así como el nombre de su archivo de configuración</param>
        /// <param name="parametros">Listado de parametros en la forma "Nombre-Valor" a enviar a la ETL</param>
        /// <param name="info">Objeto que contiene información del usuario que ejecuta la operación y su IP</param>
        [OperationContract(IsOneWay = true)]
        void EjecutarETL(ETLRemota eTLRemota, Dictionary<string, object> parametros, InfoAplicacion info);
    }
}