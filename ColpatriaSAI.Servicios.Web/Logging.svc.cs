using ColpatriaSAI.Negocio.Componentes.Utilidades;
using ColpatriaSAI.Negocio.Entidades.Informacion;
using System.Diagnostics;
using System.ServiceModel.Activation;

namespace ColpatriaSAI.Servicios.Web
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Logging : ILogging
    {
        private LoggingUtil _logging;

        public bool Auditoria(string mensaje, int prioridad, string modulo, InfoAplicacion info)
        {
            _logging = new LoggingUtil();
            return _logging.Auditoria(mensaje, (LoggingUtil.Prioridad)prioridad, modulo, info);
        }

        public bool Error(string mensaje, int prioridad, string modulo, TraceEventType tipoEvento, InfoAplicacion info)
        {
            _logging = new LoggingUtil();
            return _logging.Error(mensaje, (LoggingUtil.Prioridad)prioridad, modulo, tipoEvento, info);
        }
    }
}