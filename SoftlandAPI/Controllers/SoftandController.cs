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
        [HttpGet("Softland_Net7")]
        public string Test()
        {

            SoftlandIntegration soflandIntegration= new SoftlandIntegration();

            string configuration = "configurationSoftland.xml";
            
            //Creo los Objetos de tipo XMLDocument
            XmlDocument xmlDocDTE = new XmlDocument();
            XmlDocument xmlDocConfiguration = new XmlDocument();

            //Cargo el XML de configuarcion
            
            xmlDocConfiguration.Load(configuration);

            //Cargo la cadena de conexion
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;

            //recorro los documentos a procesar
            
            foreach (string file in Directory.GetFiles("Documentos/", "*.xml"))
            {
                
                xmlDocDTE.Load(file);
                soflandIntegration.ProcesarDocumento(xmlDocDTE, xmlDocConfiguration, cadenaSQL);

            }

            return "";
            
                       
           
        }





        [HttpGet("Softland_Net_Standart")]
        public string TestNetStandart()
        {

            SoftlandIntegration_Net_Standart soflandIntegration = new SoftlandIntegration_Net_Standart();

            string configuration = "configurationSoftland.xml";

            //Creo los Objetos de tipo XMLDocument
            XmlDocument xmlDocDTE = new XmlDocument();
            XmlDocument xmlDocConfiguration = new XmlDocument();

            //Cargo el XML de configuarcion

            xmlDocConfiguration.Load(configuration);

            //Cargo la cadena de conexion
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;

            //recorro los documentos a procesar

            foreach (string file in Directory.GetFiles("Documentos/", "*.xml"))
            {

                xmlDocDTE.Load(file);
                soflandIntegration.ProcesarDocumento(xmlDocDTE, xmlDocConfiguration, cadenaSQL);

            }

            return "";



        }





        [HttpGet("Softland_Net_462")]
        public string TestNet462
            ()
        {

            SoftlandIntegration_Net462 soflandIntegration = new SoftlandIntegration_Net462();

            string configuration = "configurationSoftland.xml";

            //Creo los Objetos de tipo XMLDocument
            XmlDocument xmlDocDTE = new XmlDocument();
            XmlDocument xmlDocConfiguration = new XmlDocument();

            //Cargo el XML de configuarcion

            xmlDocConfiguration.Load(configuration);

            //Cargo la cadena de conexion
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;

            //recorro los documentos a procesar

            foreach (string file in Directory.GetFiles("Documentos/", "*.xml"))
            {

                xmlDocDTE.Load(file);
                soflandIntegration.ProcesarDocumento(xmlDocDTE, xmlDocConfiguration, cadenaSQL);

            }

            return "";



        }

    }
}
