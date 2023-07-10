using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Softland;
using System.Xml;
using System;

namespace SoftlandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftandController : ControllerBase
    {
        [HttpGet("Softland")]
        public int Test()
        {

            SoftlandIntegration soflandIntegration= new SoftlandIntegration();
            string doc1 = "FA-33-229013-81140300-8.xml";
            string doc2 = "FA-33-530-77491280-0.xml";
            //Creo los Objetos de tipo XMLDocument
            XmlDocument xmlDocDTE = new XmlDocument();
            XmlDocument xmlDocConfiguration = new XmlDocument();

            //Cargo los XML
            xmlDocDTE.Load(doc2);
            //xmlDocConfiguration.Load(@"tranform.xml");

           
                string cadenaConexion = "Data Source=.\\SQL2017;Initial Catalog=Softland;Persist Security Info=True;User ID=sa;Password=Versat2022*";
            
               

             soflandIntegration.ProcesaCabecera(xmlDocDTE, cadenaConexion, "SpCabecera");
            soflandIntegration.ProcesaDetalle(xmlDocDTE, cadenaConexion,"SpDetalle");

            return 1;
           
        }

    }
}
