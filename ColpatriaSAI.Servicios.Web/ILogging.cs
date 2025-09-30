using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ColpatriaSAI.Negocio.Entidades.Informacion;
using System.Diagnostics;

namespace ColpatriaSAI.Servicios.Web
{
    [ServiceContract]
    public interface ILogging
    {
        [OperationContract]
        bool Auditoria(string mensaje, int prioridad, string modulo, InfoAplicacion info);

        [OperationContract]
        bool Error(string mensaje, int prioridad, string modulo, TraceEventType tipoEvento, InfoAplicacion info);
    }
}
