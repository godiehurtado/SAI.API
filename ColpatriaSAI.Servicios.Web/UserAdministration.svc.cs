using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using ColpatriaSAI.Seguridad.Proveedores;
using MembershipProvider = ColpatriaSAI.Servicios.Web.Seguridad.MembershipProvider;

namespace ColpatriaSAI.Servicios.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserAdministration" in code, svc and config file together.
    public class UserAdministration : IUserAdministration
    {
        public string[] GetAllRoles()
        {
            WebServiceRoleProvider rolepro = new WebServiceRoleProvider();
            string[] roles = rolepro.GetAllRoles();

            return roles;
        }


        public List<MembershipUser> GetAllUsers()
        {

            ColpatriaSAI.Servicios.Web.Seguridad.MembershipProvider membp = new MembershipProvider();
            int total = 0;

            return membp.GetAllUsers("AspNetSqlMembershipProvider", "/", 0, 10, out total);



           
            // MembershipProvider memp = new MembershipProvider();
            // int total=0;
            //MembershipUserCollection membc= new MembershipUserCollection();
            //  System.Collections.Specialized.NameValueCollection config = new NameValueCollection();

            // MembershipSection section = WebConfigurationManager.GetSection ("system.web/membership") as MembershipSection;


            // memp.Initialize("AspNetSqlMembershipProvider", section.Providers[0].Parameters);

            // membc=memp.GetAllUsers(0, 10, out total);





            //return membc;

        }

        public bool ValidateUser(string username, string password)


        {


            WebServiceMembershipProvider memp = new WebServiceMembershipProvider();

            int total = 0;

            System.Collections.Specialized.NameValueCollection config = new NameValueCollection();

            MembershipSection section = WebConfigurationManager.GetSection("system.web/membership") as MembershipSection;


            memp.Initialize("AspNetSqlMembershipProvider", section.Providers[0].Parameters);


            return memp.ValidateUser(username, password);
        }


        public string GetSiteMap(string connect)
        {

            if (connect==null)
            {
                connect = @"Data Source=EXJFCASTROC\EXJFCASTROC;Initial Catalog=aspnetdb;User ID=aspnetdb; password=aspnetdb";
            }
          
          InvocarMetodoWS invocarws = new InvocarMetodoWS("http://localhost:12280/MvcSQLSitemapProvider.asmx?wsdl");
            // llmamos el metodo del webservice  que nos devuelve un resultado
            var resultado = invocarws.Invocarmetodo("SiteMapProviderSoap", "BuildSiteMap", connect);

        return resultado;
    }

        //    public bool ValidateUser(string username, string password);
    //{

       
        
    //    WebServiceMembershipProvider memp = new WebServiceMembershipProvider();

    //         int total=0;
          
    //          System.Collections.Specialized.NameValueCollection config = new NameValueCollection();

    //         MembershipSection section = WebConfigurationManager.GetSection ("system.web/membership") as MembershipSection;


    //         memp.Initialize("AspNetSqlMembershipProvider", section.Providers[0].Parameters);


    //    return memp.ValidateUser(username, password);
        



    //}

    }
}
