using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Softland;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Collections;

namespace SoftlandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftandController : ControllerBase
    {
        [HttpGet("Softland")]
        public string Test()
        {

            SoftlandIntegration soflandIntegration= new SoftlandIntegration();

            string configuration = "configurationSoftland.xml";
            string doc2 = "FA-33-537-77491280-0.xml";
            //Creo los Objetos de tipo XMLDocument
            XmlDocument xmlDocDTE = new XmlDocument();
            XmlDocument xmlDocConfiguration = new XmlDocument();

            //Cargo los XML
            
            xmlDocConfiguration.Load(configuration);

            //Cargo la cadena de conexion
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;


            List<string> listDocuments = new List<string>();
            foreach (string file in Directory.GetFiles("Documentos/", "*.xml"))
            {
                // listDocuments.Add(file);

                xmlDocDTE.Load(file);
                soflandIntegration.ProcesarDocumento(xmlDocDTE, xmlDocConfiguration, cadenaSQL);

            }





            return "";
            
                       
           
        }

    }
}
