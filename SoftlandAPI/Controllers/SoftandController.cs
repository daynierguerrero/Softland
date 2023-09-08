using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Softland;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Collections;
using SaveOnFolderBox;
using System.Text.Json;

namespace SoftlandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftandController : ControllerBase
    {

        [HttpGet("Softland_Net_Standart")]
        public string TestNetStandart()
        {
            try
            {
                SoftlandIntegration_Net_Standart soflandIntegration = new SoftlandIntegration_Net_Standart();

                
                return soflandIntegration.ProcesarDocumentos();
            }
            catch (Exception)
            {

                return "ERROR";
            }

        }


        [HttpGet("Softland_Net_462")]
        public void TestNet462()
        {
            try
            {
                SoftlandIntegration_Net462 soflandIntegration = new SoftlandIntegration_Net462();
                
                soflandIntegration.ProcesarDocumentos(); 
            }
            catch (Exception)
            {

               // return "ERROR";
            }

        }


        [HttpGet("GuardarEnCarperta")]
        public string GuardarEnCarperta()
        {
            try
            {
                SaveOnFolder save = new SaveOnFolder();
                 save.Guardar();
                return "ERROR";
            }
            catch (Exception)
            {

                return "ERROR";
            }

        }


    }
}
