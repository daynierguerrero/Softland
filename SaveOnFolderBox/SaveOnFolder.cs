using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace SaveOnFolderBox
{
    public class SaveOnFolder
    {


        public async Task<HttpResponseMessage> Guardar()
        {
            var url = "https://localhost:44346/IssuedDocument/listForERPSynchronization/prod";
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var request = new { Page = 1, PageSize = 5, EnterpriseRut = "76129486-5" };
                    var requestSerializada = JsonSerializer.Serialize(request);
                    var content = new StringContent(requestSerializada, Encoding.UTF8, "application/json");
                    var respuesta = await httpClient.PostAsync(url, content);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        var cuerpo = await respuesta.Content.ReadAsStringAsync();
                        ResponseTest responseObject= JsonSerializer.Deserialize<ResponseTest>(cuerpo);
                        
                        //temporal

                        List<IssuedDocumentReponse> listaTemporal= new List<IssuedDocumentReponse>();
                        for (int i = 0; i < 5; i++)
                        {
                            listaTemporal.Add(
                            new IssuedDocumentReponse()
                            {

                                TipoDTE = "33",
                                Folio = "39794",
                                FchEmis = "2022-07-01 00:00:00.0000000",
                                FmaPago = 2,
                                RUTEmisor = "76129486-5",
                                RUTRecep = "96513050-0",
                                DescuentoMonto = 0,
                                DescuentoPct = 0,
                                Environment = "homo",
                                ERPSynchronizationDatetime = DateTime.Now,
                                ERPSynchronizationGlosa = "test",
                                ERPSynchronizationRepeatCount = 3,
                                ERPSynchronizationStatus = 1,
                                IdAccountingAccount = 11,
                                IdCostCenter = 44,
                                IVA = 98737,
                                LastUpdate = DateTime.Now,
                                MntBruto = 519670,
                                MntExe = 0,
                                MntNeto = 519670,
                                MntTotal = 618407,
                                RznSoc = "GDE S.A.",
                                RznSocRecep = "Valle Nevado S.A.",
                                SIIState = 8,
                                TasaIVA = 19,
                                User = "Sistema"
                            });
                        }

                        responseObject.Elements = listaTemporal;
                        responseObject.Pages = 1;
                        responseObject.Total = 5;



                        //end temporal
                        int contador = 0;
                        foreach(var item in responseObject.Elements)
                        {
                            string json = JsonSerializer.Serialize(item);
                            File.WriteAllText(@$"D:\probando\{contador}.json", json);
                            contador++;
                        }
                      
                    }
                    return respuesta;
                }
               
            }
            catch (Exception)
            {

                throw;
            }

          
        }



    }


    public class IssuedDocumentReponse
    {
        /// <summary>
        /// Tipo de DTE
        /// </summary>
        public string TipoDTE { set; get; }

        /// <summary>
        /// Folio del documento
        /// </summary>
        public string Folio { set; get; }

        /// <summary>
        /// Fecha de emision del documento
        /// </summary>
        public string FchEmis { set; get; }

        /// <summary>
        /// Forma de Pago del documento
        /// </summary>
        public int FmaPago { set; get; }

        /// <summary>
        /// Rut Emisor del documento
        /// </summary>
        public string RUTEmisor { set; get; }

        /// <summary>
        /// Razon social del emisor del documento
        /// </summary>
        public string RznSoc { set; get; }

        /// <summary>
        /// Rut del receptor del documento
        /// </summary>
        public string RUTRecep { set; get; }

        /// <summary>
        /// Razon social del receptor del documento
        /// </summary>
        public string RznSocRecep { set; get; }

        /// <summary>
        /// Monto bruto del documento
        /// </summary>
        public long MntBruto { set; get; }

        /// <summary>
        /// Porciento de descuento
        /// </summary>
        public decimal DescuentoPct { set; get; }

        /// <summary>
        /// Monto del descuento
        /// </summary>
        public long DescuentoMonto { set; get; }

        /// <summary>
        /// Monto Neto del documento
        /// MntBruto-DescuentoMonto
        /// </summary>
        public long MntNeto { set; get; }

        /// <summary>
        /// Monto Exento
        /// </summary>
        public long MntExe { set; get; }
        /// <summary>
        /// taza Iva del documento
        /// </summary>
        public decimal TasaIVA { set; get; }

        /// <summary>
        /// Valor del Iva del documento
        /// </summary>
        public long IVA { set; get; }

        /// <summary>
        /// Monto total del documento
        /// </summary>
        public long MntTotal { set; get; }

        /// <summary>
        /// Estado informado al SII debido a revison
        /// </summary>
        public int? SIIState { set; get; }

        /// <summary>
        /// Ultimo usuario que modifica estados del documento
        /// </summary>
        public string User { set; get; }

        public DateTime LastUpdate { set; get; }

        public string Environment { set; get; }

        /// <summary>
        /// Centro de Costo 
        /// </summary>
        public int? IdCostCenter { set; get; }

        /// <summary>
        /// Cuenta Contable
        /// </summary>
        public int? IdAccountingAccount { set; get; }
        public int? ERPSynchronizationStatus { get; set; }
        public DateTime ERPSynchronizationDatetime { get; set; }
        public string ERPSynchronizationGlosa { get; set; }
        public int? ERPSynchronizationRepeatCount { get; set; }
    }

    public class ResponseTest
    {
        public List<IssuedDocumentReponse> Elements { get; set; }
        public int Pages { get; set; }
        public int Total { get; set; }
    }
}

