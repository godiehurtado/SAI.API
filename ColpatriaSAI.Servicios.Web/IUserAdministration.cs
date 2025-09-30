using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;

namespace ColpatriaSAI.Servicios.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserAdministration" in both code and config file together.
    [ServiceContract]
    public interface IUserAdministration
    {
        [OperationContract]
        string[] GetAllRoles();

        [OperationContract]
        List<MembershipUser> GetAllUsers();

        [OperationContract]
        bool ValidateUser(string username, string password);


        [OperationContract]
        string GetSiteMap(string connect);

    }
}

