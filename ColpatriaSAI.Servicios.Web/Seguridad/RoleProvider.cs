using System.Linq;
using System.Web.Services;
using System.Xml.Linq;
using ColpatriaSAI.Datos;
using ColpatriaSAI.Negocio.Componentes;

namespace ColpatriaSAI.Servicios.Web.Seguridad
{
    /// <summary>
    /// Summary description for RoleProvider
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class RoleProvider : System.Web.Services.WebService
    {
        protected System.Web.Security.RoleProvider GetProvider(string providerName, string applicationName)
        {
            System.Web.Security.RoleProvider provider;
            if ((providerName != null) && (System.Web.Security.Roles.Providers[providerName] != null))
            {
                provider = System.Web.Security.Roles.Providers[providerName];
            }
            else
            {
                provider = System.Web.Security.Roles.Provider;
            }

            if (applicationName != null)
            {
                provider.ApplicationName = applicationName;
            }

            return provider;
        }

        public RoleProvider()
        {
        }

        [WebMethod(Description="")]
        public void AddUsersToRoles(string providerName, string applicationName, string[] usernames, string[] roleNames)
        {
            GetProvider(providerName, applicationName).AddUsersToRoles(usernames, roleNames);
        }

        [WebMethod(Description = "")]
        public void CreateRole(string providerName, string applicationName, string roleName)
        {
            GetProvider(providerName, applicationName).CreateRole(roleName);
        }

        [WebMethod(Description = "")]
        public bool DeleteRole(string providerName, string applicationName, string roleName, bool throwOnPopulatedRole)
        {
            ColpatriaSAI.Negocio.Componentes.Seguridad.Rol role = new ColpatriaSAI.Negocio.Componentes.Seguridad.Rol();
            role.EliminarPermisosRol(roleName);
            return GetProvider(providerName, applicationName).DeleteRole(roleName, throwOnPopulatedRole);
        }

        [WebMethod(Description = "")]
        public string[] FindUsersInRole(string providerName, string applicationName, string roleName, string usernameToMatch)
        {
            return GetProvider(providerName, applicationName).FindUsersInRole(roleName, usernameToMatch);
        }

        [WebMethod(Description = "")]
        public string[] GetAllRoles(string providerName, string applicationName)
        {
            return GetProvider(providerName, applicationName).GetAllRoles();
        }

        [WebMethod(Description = "")]
        public string[] GetRolesForUser(string providerName, string applicationName, string username)
        {
            return GetProvider(providerName, applicationName).GetRolesForUser(username);
        }

        [WebMethod(Description = "")]
        public string[] GetUsersInRole(string providerName, string applicationName, string roleName)
        {
            return GetProvider(providerName, applicationName).GetUsersInRole(roleName);
        }

        [WebMethod(Description = "")]
        public bool IsUserInRole(string providerName, string applicationName, string username, string roleName)
        {
            return GetProvider(providerName, applicationName).IsUserInRole(username, roleName);
        }

        [WebMethod(Description = "")]
        public bool IsUserInRoles(string applicationName, string username, string roleNames)
        {
            bool siono = false;

            using (var context = new SAI_Entities())
            {
                string XMLroles = context.aspnet_UsersInRoles_IsUserInRoles(applicationName, username, roleNames).FirstOrDefault();
                XMLroles = "<Roles>" + XMLroles + "</Roles>";
                
              

              var roles = from c in XElement.Load(Helper.ConvertStringtoStream(XMLroles)).Elements("row")
                            select c;

                foreach (var xElement  in roles)
                {
                   if (xElement.Attribute("ESTADO").Value=="1")
                   {
                       siono = true;
                       break;
                   }
                }


            }

            return siono;
            //return GetProvider(providerName, applicationName).IsUserInRoles(username, roleNames);
        }

        [WebMethod(Description = "")]
        public void RemoveUsersFromRoles(string providerName, string applicationName, string[] usernames, string[] roleNames)
        {
            GetProvider(providerName, applicationName).RemoveUsersFromRoles(usernames, roleNames);
        }

        [WebMethod(Description = "")]
        public bool RoleExists(string providerName, string applicationName, string roleName)
        {
            return GetProvider(providerName, applicationName).RoleExists(roleName);
        }
    }
}

