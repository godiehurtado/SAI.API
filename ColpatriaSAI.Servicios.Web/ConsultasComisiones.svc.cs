using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


using System.Data.Services;
using System.Data.Services.Common;
using System.ServiceModel.Web;
using System.Web;
using System.ServiceModel.Activation;


namespace ColpatriaSAI.Servicios.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ConsultasComisiones" in code, svc and config file together.
    //public class ConsultasComisiones : IConsultasComisiones
    //{
    //    public void DoWork()
    //    {
    //    }
    //}

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ConsultasComisiones : ColpatriaSAI.Negocio.Componentes.Comision.Consultas.BitacoraCalculo
    {

    }
}
