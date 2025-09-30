using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ColpatriaSAI.Negocio.Entidades;
using ColpatriaSAI.Negocio.Componentes.ETLs;
using ColpatriaSAI.Servicios.Web.ETLs;
using ColpatriaSAI.Negocio.Entidades.Informacion;

namespace ColpatriaSAI.Servicios.Web
{
    public class EjecucionETLs : IEjecucionETLs
    {
        public List<TipoETLRemota> ListarTiposETLRemota()
        {
            TiposETLRemota tiposETLRemota = new TiposETLRemota();
            return tiposETLRemota.ListarTiposETLRemota();
        }

        public List<ETLRemota> ListarETLsRemotas()
        {
            ETLsRemotas eTLRemota = new ETLsRemotas();
            return eTLRemota.ListarETLsRemotas();
        }

        public ETLRemota ObtenerETLRemotaporId(int id)
        {
            ETLsRemotas eTLRemota = new ETLsRemotas();
            return eTLRemota.ObtenerETLRemotaporId(id);
        }

        public List<ETLRemota> ListarETLsRemotasporTipo(int tipoETLRemota_id)
        {
            ETLsRemotas eTLRemota = new ETLsRemotas();
            return eTLRemota.ListarETLsRemotasporTipo(tipoETLRemota_id);
        }

        public void EjecutarETL(ETLRemota eTLRemota, Dictionary<string, object> parametros, InfoAplicacion info)
        {
            EjecucionETL ejecucionETL = new EjecucionETL();
            ejecucionETL.EjecutarETL(eTLRemota, parametros, info);
        }
    }
}
