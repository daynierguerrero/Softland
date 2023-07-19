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

        [HttpGet("Softland_Net_Standart")]
        public string TestNetStandart()
        {
            try
            {
                SoftlandIntegration_Net_Standart soflandIntegration = new SoftlandIntegration_Net_Standart();

                soflandIntegration.ProcesarDocumentos();

                return "OK";
            }
            catch (Exception)
            {

                return "ERROR";
            }

        }


        [HttpGet("Softland_Net_462")]
        public string TestNet462()
        {
            try
            {
                SoftlandIntegration_Net462 soflandIntegration = new SoftlandIntegration_Net462();
                soflandIntegration.ProcesarDocumentos();
                return "OK";
            }
            catch (Exception)
            {

                return "ERROR";
            }

        }

    }
}
