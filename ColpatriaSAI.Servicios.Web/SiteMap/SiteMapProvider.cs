using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using ColpatriaSAI.Negocio.Componentes;

namespace ColpatriaSAI.Servicios.Web.SiteMap
{

    /// <summary>
    /// Summary description for RoleProvider
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SiteMapProvider: System.Web.Services.WebService
        
    {
        
        //protected System.Web.SiteMapProvider GetProvider(string providerName, string applicationName)
        //{

           
        //    System.Web.SiteMapProvider provider;
        //    if ((providerName != null) && (System.Web.SiteMapProvider.Providers[providerName] != null))
        //    {
        //        provider = System.Web.Security.Roles.Providers[providerName];
        //    }
        //    else
        //    {
        //        provider = System.Web.Security.Roles.Provider;
        //    }

        //    if (applicationName != null)


        //    {

        //        provider.
        //        provider.ApplicationName = applicationName;
        //    }

        //    return provider;
        //}
        [WebMethod(Description = "")]
        public string BuildSiteMap()
        {
            SiteMapNode _root;
            SiteMapContextDataContext db = new SiteMapContextDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());

            var siteMpaQuery = from s in db.SITEMAPs
                               orderby s.ID
                               select s;
            IList<SITEMAP> result = siteMpaQuery.ToList<SITEMAP>();
            string xmlstring=Helper.SerializeUnObjecto(result);
            XmlDocument siteMapXml = new XmlDocument();

            siteMapXml.Load(Helper.ConvertStringtoStream(xmlstring));
            string sitemap="";
            
            foreach (XmlNode xmlNode in siteMapXml)
            {
                if (xmlNode.HasChildNodes)
                {
                    sitemap= GetDynamicNodes(xmlNode);
                }
                else
                {
                    sitemap = "No se encontro informacion del sitem en la bd";
                }
            }

            sitemap="<?xml version="+ '\u0022' + "1.0" +'\u0022' + " encoding="+ '\u0022' +"utf-16"+'\u0022'+"?>" + sitemap;
            return sitemap;

            //sitemap= "<?xml version=" + """1.0""" + """ + " encoding=" + "" + "utf-16" +""+"?>" + sitemap;
            XmlWriter writer_xml = null;
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < result.Count; i++)
            {
                SITEMAP item = result[i];

                if (i == 0)
                {
                    HttpContext context = HttpContext.Current;


                    //return node.ToString();
                    context.Response.ContentType = "text/xml";

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Encoding = Encoding.UTF8;
                    settings.Indent = true;

                    writer_xml = XmlWriter.Create(builder, settings);
                    writer_xml.WriteStartDocument();
                    writer_xml.WriteStartElement("siteMap");


                    AddUrl(item, writer_xml);
                }
                else
                {
                    if (i + 1 < result.Count)
                    {
                        if (result[i + 1].PARENT_ID != "000" && result[i].ID == result[i + 1].PARENT_ID)
                        {
                            AddUrl(item, writer_xml, true);

                        }
                        else
                        {
                            if (result[i - 1].PARENT_ID != "000")
                            {
                                writer_xml.WriteEndElement();
                            }
                            AddUrl(item, writer_xml, false);
                            writer_xml.WriteEndElement();

                          

                        }
                    }
                    else
                    {

                        AddUrl(item, writer_xml, false);
                        writer_xml.WriteEndElement();

                    }


                }

            }
            writer_xml.WriteEndElement();
            writer_xml.WriteEndDocument();
            writer_xml.Flush();

            return builder.ToString();

        }

        private string GetDynamicNodes(XmlNode Nodes)
        {
            XmlDocument doc= new XmlDocument();
            doc.CreateXmlDeclaration("1.0", "utf-16",null);
            XmlElement sitemapElement = doc.CreateElement("siteMap");
            foreach (XmlNode node in Nodes)
            {
                if (node.HasChildNodes && node.NodeType.ToString() !="Element")
                {
                    GetDynamicNodes(node);
                }
                else
                {

                    string ID = Helper.traerInnerStringNodoXML(node.OuterXml, "ID");
                    string TITLE = Helper.traerInnerStringNodoXML(node.OuterXml, "TITLE");
                    string DESCRIPTION = Helper.traerInnerStringNodoXML(node.OuterXml, "DESCRIPTION");
                    string CONTROLLER = Helper.traerInnerStringNodoXML(node.OuterXml, "CONTROLLER");
                    string ACTION = Helper.traerInnerStringNodoXML(node.OuterXml, "ACTION");
                    string PARAMID = Helper.traerInnerStringNodoXML(node.OuterXml, "PARAMID");
                    string URL = Helper.traerInnerStringNodoXML(node.OuterXml, "URL");
                    string PARENT_ID = Helper.traerInnerStringNodoXML(node.OuterXml, "PARENT_ID");
                    string ROLES = Helper.traerInnerStringNodoXML(node.OuterXml, "ROLES");

                    XmlNode sitemapnode;

                   if (PARENT_ID !="000")
                   {
                      if (sitemapElement.SelectSingleNode("descendant::siteMapNode").HasChildNodes)
                      {


                          sitemapnode = sitemapElement.SelectSingleNode("descendant::mvcSiteMapNode[@id='" + PARENT_ID + "']");
                          //sitemapnode = sitemapElement.SelectSingleNode("descendant::mvcSiteMapNode[id='" + PARENT_ID + "']"); 



                      }
                       else
                       {
                           sitemapnode = sitemapElement.SelectSingleNode("descendant::siteMapNode");
                       }
                       AddDynamicChildElement((XmlElement)sitemapnode, ID, URL, CONTROLLER, ACTION, TITLE, ROLES, "mvcSiteMapNode");
                   }
                   else
                   {
                       if (!sitemapElement.HasChildNodes)
                       {
                           AddDynamicChildElement(sitemapElement, ID, URL, CONTROLLER, ACTION, TITLE, ROLES,"siteMapNode");
                       }
                       else
                       {
                           sitemapnode = sitemapElement.SelectSingleNode("descendant::siteMapNode");
                           AddDynamicChildElement((XmlElement)sitemapnode, ID, URL, CONTROLLER, ACTION, TITLE, ROLES, "mvcSiteMapNode");
                       }
                   }

                   
                }
              
            }
            doc.AppendChild(sitemapElement);
            return doc.OuterXml;
        }


        private static XmlElement AddDynamicChildElement(XmlElement parentElement, String id, String url, String controller, String action, String title, String roles, string SiteMapNodeName)
        {
            // Create new element from the parameters
            XmlElement childElement = parentElement.OwnerDocument.CreateElement(SiteMapNodeName);
            childElement.SetAttribute("id", id);
            childElement.SetAttribute("url", url);
            childElement.SetAttribute("controller", controller);
            childElement.SetAttribute("action", action);
            childElement.SetAttribute("title", title);
            childElement.SetAttribute("roles", roles);

            // Add it to the parent
            parentElement.AppendChild(childElement);
            return childElement;
        }

        public void AddUrl(SITEMAP node, XmlWriter writer_xml, bool tienehijos = false)
        {
            if (tienehijos == false)
            {
                if (node.TITLE.ToUpper() != "ROOT" && node.URL == null)
                {
                    // Open url tag
                    if (node.PARENT_ID == "000")
                    {
                        writer_xml.WriteStartElement("mvcSiteMapNode");
                        // Write location
                        writer_xml.WriteAttributeString("id", node.ID);
                        writer_xml.WriteAttributeString("url", node.URL);
                        writer_xml.WriteAttributeString("controller", node.CONTROLLER);
                        writer_xml.WriteAttributeString("action", node.ACTION);
                        writer_xml.WriteAttributeString("title", node.TITLE);
                        writer_xml.WriteAttributeString("roles", node.ROLES);
                      
                        
                    }
                    else
                    {
                        writer_xml.WriteStartElement("mvcSiteMapNode");
                        // Write location
                        writer_xml.WriteAttributeString("id", node.ID);
                        writer_xml.WriteAttributeString("url", node.URL);
                        writer_xml.WriteAttributeString("controller", node.CONTROLLER);
                        writer_xml.WriteAttributeString("action", node.ACTION);
                        writer_xml.WriteAttributeString("title", node.TITLE);
                        writer_xml.WriteAttributeString("roles", node.ROLES);

                    }
                }

                else
                {
                    writer_xml.WriteStartElement("siteMapNode");
                    writer_xml.WriteAttributeString("id", node.ID);
                    writer_xml.WriteAttributeString("url", node.URL);
                    writer_xml.WriteAttributeString("roles", node.ROLES);

                }
            }
            else
            {
                writer_xml.WriteStartElement("mvcSiteMapNode");
                // Write location
                writer_xml.WriteAttributeString("id", node.ID);
                writer_xml.WriteAttributeString("url", node.URL);
                writer_xml.WriteAttributeString("controller", node.CONTROLLER);
                writer_xml.WriteAttributeString("action", node.ACTION);
                writer_xml.WriteAttributeString("title", node.TITLE);
                writer_xml.WriteAttributeString("roles", node.ROLES);
            }

        }


    }
}