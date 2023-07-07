using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Softland
{


public class SoftlandIntegration
    {

        public int ProcesaCabecera(XmlDocument doc, string cadenaConexion, string spCabecera)
        {
            int newProdID = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    XmlNodeList listaNodosRec = doc.GetElementsByTagName("recepcion");
                    bool conRec = listaNodosRec.Count == 0 ? false : true;

                    XmlNodeList listaNodosDTE = doc.GetElementsByTagName("Encabezado");
                    foreach (XmlNode xmlNode in listaNodosDTE)
                    {
                        using (SqlCommand command = new SqlCommand(spCabecera, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                        
                            
                           
                            //IDDoc
                            //command.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = tipoDTE;
                            //command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = folioFactura;
                           // command.Parameters.AddWithValue("@FchEmis", SqlDbType.DateTime).Value = fechEmis;
                           
                           if (xmlNode["IdDoc"] != null)
                           {
                                //((((System.Xml.XmlElementList)(xmlNode["IdDoc"].GetElementsByTagName("TipoDespacho")))).curElem).InnerText
                                if (xmlNode["IdDoc"].GetElementsByTagName("TipoDTE").Count != 0)
                                {

                                    command.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = xmlNode["IdDoc"].GetElementsByTagName("TipoDTE")[0].InnerText;
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = DBNull.Value;

                                }
                                if (xmlNode["IdDoc"].GetElementsByTagName("Folio").Count != 0)
                                {

                                    command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = xmlNode["IdDoc"].GetElementsByTagName("Folio")[0].InnerText;
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = DBNull.Value;

                                }
                                if (xmlNode["IdDoc"].GetElementsByTagName("FchEmis").Count != 0)
                                {
                                    command.Parameters.AddWithValue("@FchEmis", SqlDbType.DateTime).Value = DateTime.Parse(xmlNode["IdDoc"].GetElementsByTagName("FchEmis")[0].InnerText);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@FchEmis", SqlDbType.DateTime).Value = DBNull.Value;

                                }

                                if (xmlNode["IdDoc"].GetElementsByTagName("TipoDespacho").Count != 0)
                               {

                                   command.Parameters.AddWithValue("@TipoDespacho", SqlDbType.Int).Value = xmlNode["IdDoc"].GetElementsByTagName("TipoDespacho")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@TipoDespacho", SqlDbType.Int).Value = DBNull.Value;

                               }
                               if (xmlNode["IdDoc"].GetElementsByTagName("IndTraslado").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@IndTraslado", SqlDbType.Int).Value = xmlNode["IdDoc"].GetElementsByTagName("IndTraslado")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@IndTraslado", SqlDbType.Int).Value = DBNull.Value;

                               }
                               if (xmlNode["IdDoc"].GetElementsByTagName("IndServicio").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@IndServicio", SqlDbType.Int).Value = xmlNode["IdDoc"].GetElementsByTagName("IndServicio")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@IndServicio", SqlDbType.Int).Value = DBNull.Value;

                               }
                               if (xmlNode["IdDoc"].GetElementsByTagName("FmaPago").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@FmaPago", SqlDbType.Int).Value = xmlNode["IdDoc"].GetElementsByTagName("FmaPago")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@FmaPago", SqlDbType.Int).Value = DBNull.Value;


                               }
                               if (xmlNode["IdDoc"].GetElementsByTagName("FmaPagExp").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@FmaPagExp", SqlDbType.Int).Value = xmlNode["IdDoc"].GetElementsByTagName("FmaPagExp")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@FmaPagExp", SqlDbType.Int).Value = DBNull.Value;

                               }

                               if (xmlNode["IdDoc"].GetElementsByTagName("FchCancel").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@FchCancel", SqlDbType.DateTime).Value = DateTime.Parse(xmlNode["IdDoc"].GetElementsByTagName("FchCancel")[0].InnerText);
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@FchCancel", SqlDbType.DateTime).Value = DBNull.Value;

                               }
                               if (xmlNode["IdDoc"].GetElementsByTagName("MntCancel").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@MntCancel", SqlDbType.Int).Value = xmlNode["IdDoc"].GetElementsByTagName("MntCancel")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@MntCancel", SqlDbType.Int).Value = DBNull.Value;

                               }

                           }
                            
                        if (xmlNode["MntPagos"] != null)
                        {
                            //MntPagos
                            if (xmlNode["MntPagos"].GetElementsByTagName("FchPago").Count != 0)
                            {
                                command.Parameters.AddWithValue("@FchPago", SqlDbType.DateTime).Value = xmlNode["MntPagos"].GetElementsByTagName("FchPago")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FchPago", SqlDbType.DateTime).Value = DBNull.Value;

                            }
                            if (xmlNode["MntPagos"].GetElementsByTagName("MntPago").Count != 0)
                            {
                                command.Parameters.AddWithValue("@MntPago", SqlDbType.Int).Value = xmlNode["MntPago"].GetElementsByTagName("MntPago")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@MntPago", SqlDbType.Int).Value = DBNull.Value;

                            }
                            if (xmlNode["MntPagos"].GetElementsByTagName("GlosaPagos").Count != 0)
                            {
                                command.Parameters.AddWithValue("@GlosaPagos", SqlDbType.VarChar).Value = xmlNode["MntPagos"].GetElementsByTagName("GlosaPagos")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@GlosaPagos", SqlDbType.VarChar).Value = DBNull.Value;


                            }
                            if (xmlNode["IdDoc"].GetElementsByTagName("FchVenc").Count != 0)
                            {
                                command.Parameters.AddWithValue("@FchVencim", SqlDbType.DateTime).Value = xmlNode["IdDoc"].GetElementsByTagName("FchVenc")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FchVencim", SqlDbType.DateTime).Value = DBNull.Value;

                            }
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@FchPago", SqlDbType.DateTime).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@MntPago", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@GlosaPagos", SqlDbType.VarChar).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@FchVencim", SqlDbType.DateTime).Value = DBNull.Value;
                        }

                        //Emisor
                        if (xmlNode["Emisor"] != null)
                        {
                            if (xmlNode["Emisor"].GetElementsByTagName("RUTEmisor").Count != 0)
                            {
                                command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = xmlNode["Emisor"].GetElementsByTagName("RUTEmisor")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Emisor"].GetElementsByTagName("RznSoc").Count != 0)
                            {
                                command.Parameters.AddWithValue("@RznSoc", SqlDbType.VarChar).Value = xmlNode["Emisor"].GetElementsByTagName("RznSoc")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@RznSoc", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Emisor"].GetElementsByTagName("GiroEmis").Count != 0)
                            {
                                command.Parameters.AddWithValue("@GiroEmis", SqlDbType.VarChar).Value = xmlNode["Emisor"].GetElementsByTagName("GiroEmis")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@GiroEmis", SqlDbType.VarChar).Value = DBNull.Value;

                            }

                            if (xmlNode["Emisor"].GetElementsByTagName("Acteco").Count != 0)
                            {
                                command.Parameters.AddWithValue("@Acteco", SqlDbType.Int).Value = xmlNode["Emisor"].GetElementsByTagName("Acteco")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Acteco", SqlDbType.Int).Value = DBNull.Value;


                            }
                            if (xmlNode["Emisor"].GetElementsByTagName("FolioAut").Count != 0)
                            {
                                command.Parameters.AddWithValue("@FolioAut", SqlDbType.Int).Value = xmlNode["Emisor"].GetElementsByTagName("FolioAut")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FolioAut", SqlDbType.Int).Value = DBNull.Value;

                            }
                            if (xmlNode["Emisor"].GetElementsByTagName("FchAut").Count != 0)
                            {
                                command.Parameters.AddWithValue("@FchAut", SqlDbType.DateTime).Value = DateTime.Parse(xmlNode["Emisor"].GetElementsByTagName("FchAut")[0].InnerText);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FchAut", SqlDbType.DateTime).Value = DBNull.Value;

                            }
                            if (xmlNode["Emisor"].GetElementsByTagName("DirOrigen").Count != 0)
                            {
                                command.Parameters.AddWithValue("@DirOrigen", SqlDbType.VarChar).Value = xmlNode["Emisor"].GetElementsByTagName("DirOrigen")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@DirOrigen", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Emisor"].GetElementsByTagName("CmnaOrigen").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CmnaOrigen", SqlDbType.VarChar).Value = xmlNode["Emisor"].GetElementsByTagName("CmnaOrigen")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CmnaOrigen", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Emisor"].GetElementsByTagName("CiudadOrigen").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CiudadOrigen", SqlDbType.VarChar).Value = xmlNode["Emisor"].GetElementsByTagName("CiudadOrigen")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CiudadOrigen", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Emisor"].GetElementsByTagName("CdgVendedor").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CdgVendedor", SqlDbType.VarChar).Value = xmlNode["Emisor"].GetElementsByTagName("CdgVendedor")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CdgVendedor", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                        }
                        //Receptor
                        if (xmlNode["Receptor"] != null)
                        {
                            if (xmlNode["Receptor"].GetElementsByTagName("RUTRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@RUTRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("RUTRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@RUTRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("CdgIntRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CdgIntRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("CdgIntRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CdgIntRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("RznSocRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@RznSocRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("RznSocRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@RznSocRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("Nacionalidad").Count != 0)
                            {
                                command.Parameters.AddWithValue("@Nacionalidad", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("Nacionalidad")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Nacionalidad", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("GiroRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@GiroRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("GiroRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@GiroRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }

                            if (xmlNode["Receptor"].GetElementsByTagName("Contacto").Count != 0)
                            {
                                command.Parameters.AddWithValue("@Contacto", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("Contacto")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Contacto", SqlDbType.VarChar).Value = DBNull.Value;


                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("CorreoRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CorreoRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("CorreoRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CorreoRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("DirRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@DirRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("DirRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@DirRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("CmnaRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CmnaRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("CmnaRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CmnaRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("CiudadRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CiudadRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("CiudadRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CiudadRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("DirDest").Count != 0)
                            {
                                command.Parameters.AddWithValue("@DirDest", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("DirDest")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@DirDest", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("CmnaDest").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CmnaDest", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("CmnaDest")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CmnaDest", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("CiudadDest").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CiudadDest", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("CiudadDest")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CiudadDest", SqlDbType.VarChar).Value = DBNull.Value;


                            }
                            command.Parameters.AddWithValue("@Tara", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@CodUnidMedTara", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@PesoBruto", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@CodUnidPesoBruto", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@PesoNeto", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@CodUnidPesoNeto", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@TotItems", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@TotBultos", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@CodTpoBultos", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@CantBultos", SqlDbType.Int).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@Marcas", SqlDbType.VarChar).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@Sello", SqlDbType.VarChar).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@EmisorSello", SqlDbType.VarChar).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@MntFlete", SqlDbType.VarChar).Value = DBNull.Value;
                            command.Parameters.AddWithValue("@MntSeguro", SqlDbType.VarChar).Value = DBNull.Value;

                            if (xmlNode["Receptor"].GetElementsByTagName("CodPaisRecep").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CodPaisRecep", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("CodPaisRecep")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CodPaisRecep", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                            if (xmlNode["Receptor"].GetElementsByTagName("CodPaisDestin").Count != 0)
                            {
                                command.Parameters.AddWithValue("@CodPaisDestin", SqlDbType.VarChar).Value = xmlNode["Receptor"].GetElementsByTagName("CodPaisDestin")[0].InnerText;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CodPaisDestin", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                        }

                            
                            //Totales
                            if (xmlNode["Totales"] != null)
                            {
                                
                                if (xmlNode["Totales"].GetElementsByTagName("TpoMoneda").Count != 0)
                                {
                                    command.Parameters.AddWithValue("@TpoMoneda", SqlDbType.VarChar).Value = xmlNode["Totales"].GetElementsByTagName("TpoMoneda")[0].InnerText;
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@TpoMoneda", SqlDbType.VarChar).Value = DBNull.Value;

                                }
                                
                                if (xmlNode["Totales"].GetElementsByTagName("MntNeto").Count != 0)
                                {
                                    command.Parameters.AddWithValue("@MntNeto", SqlDbType.Int).Value = xmlNode["Totales"].GetElementsByTagName("MntNeto")[0].InnerText;
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@MntNeto", SqlDbType.Int).Value = DBNull.Value;

                                }
                               
                                if (xmlNode["Totales"].GetElementsByTagName("MntExe").Count != 0)
                                {
                                 
                                    command.Parameters.AddWithValue("@MntExe", SqlDbType.Int).Value = xmlNode["Totales"].GetElementsByTagName("MntExe")[0].InnerText;
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@MntExe", SqlDbType.Int).Value = DBNull.Value;

                                }
                                
                               if (xmlNode["Totales"].GetElementsByTagName("TasaIVA").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@TasaIVA", SqlDbType.Decimal).Value = xmlNode["Totales"].GetElementsByTagName("TasaIVA")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@TasaIVA", SqlDbType.Decimal).Value = DBNull.Value;

                               }
                               if (xmlNode["Totales"].GetElementsByTagName("IVA").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@IVA", SqlDbType.Int).Value = xmlNode["Totales"].GetElementsByTagName("IVA")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@IVA", SqlDbType.Int).Value = DBNull.Value;

                               }

                               if (xmlNode["Totales"].GetElementsByTagName("MntTotal").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@MntTotal", SqlDbType.Int).Value = xmlNode["Totales"].GetElementsByTagName("MntTotal")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@MntTotal", SqlDbType.Int).Value = DBNull.Value;

                               }

                               if (xmlNode["Totales"].GetElementsByTagName("Observaciones").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@Observaciones", SqlDbType.NChar).Value = xmlNode["Totales"].GetElementsByTagName("Observaciones")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@Observaciones", SqlDbType.NChar).Value = DBNull.Value;

                               }
                               if (xmlNode["Totales"].GetElementsByTagName("NombreObra").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@NombreObra", SqlDbType.NChar).Value = xmlNode["Totales"].GetElementsByTagName("NombreObra")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@NombreObra", SqlDbType.NChar).Value = DBNull.Value;


                               }
                               if (xmlNode["Totales"].GetElementsByTagName("DirObra").Count != 0)
                               {
                                   command.Parameters.AddWithValue("@DirObra", SqlDbType.NChar).Value = xmlNode["Totales"].GetElementsByTagName("DirObra")[0].InnerText;
                               }
                               else
                               {
                                   command.Parameters.AddWithValue("@DirObra", SqlDbType.NChar).Value = DBNull.Value;

                               }
                               
                            }



                            /*
                            listaNodosDTE = doc.GetElementsByTagName("cabecera");
                            XmlNode listaNodosid = listaNodosDTE[0];
                            string estadoley = listaNodosid["dte_ley19883_estado"].InnerText;
                            string fechaley = listaNodosid["dte_ley19883_fecha"].InnerText;
                            string fechaRecepcionSII = listaNodosid["dte_fecha_recepcionsii"].InnerText;
                            command.Parameters.AddWithValue("@EstadoLey", SqlDbType.VarChar).Value = estadoley;

                            if (!string.IsNullOrEmpty(fechaley))
                            {
                                command.Parameters.AddWithValue("@FechaLey", SqlDbType.DateTime).Value = DateTime.Parse(fechaley);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FechaLey", SqlDbType.DateTime).Value = DBNull.Value;

                            }
                            if (!string.IsNullOrEmpty(fechaRecepcionSII))
                            {
                                command.Parameters.AddWithValue("@FechaRecepcionSII", SqlDbType.DateTime).Value = DateTime.Parse(fechaRecepcionSII);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FechaRecepcionSII", SqlDbType.DateTime).Value = DBNull.Value;

                            }

                            string centroCosto = "";
                            string cuentaContable = "";
                            listaNodosDTE = doc.GetElementsByTagName("distribucion");
                            if (listaNodosDTE != null)
                            {
                                for (int i = 0; i < listaNodosDTE.Count; i++)
                                {
                                    XmlNode listaNodosRef = listaNodosDTE[i];
                                    XmlNode listaNodoCC = listaNodosRef["linea"];
                                    if (listaNodoCC != null)
                                    {
                                        centroCosto = listaNodoCC["CENTROCOSTO"].InnerText;
                                        cuentaContable = listaNodoCC["CUENTACONTABLE"].InnerText;
                                        break;
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(centroCosto))
                            {
                                command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = centroCosto;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = DBNull.Value;


                            }
                            if (!string.IsNullOrEmpty(cuentaContable))
                            {
                                command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = cuentaContable;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            */






                            var folioReferencia = "";
                            listaNodosDTE = doc.GetElementsByTagName("Referencia");
                            if (listaNodosDTE != null)
                            {
                                for (int i = 0; i < listaNodosDTE.Count; i++)
                                {
                                    XmlNode listaNodosRef = listaNodosDTE[i];
                                    if (listaNodosRef["TpoDocRef"].InnerText == "33" || listaNodosRef["TpoDocRef"].InnerText == "34")
                                    {
                                        folioReferencia = listaNodosRef["FolioRef"].InnerText;
                                        break;
                                    }

                                }
                            }
                            if (!string.IsNullOrEmpty(folioReferencia))
                            {
                                command.Parameters.AddWithValue("@FolioReferencia", SqlDbType.NChar).Value = folioReferencia;
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FolioReferencia", SqlDbType.NChar).Value = DBNull.Value;
                            }
                            /*
                            command.Parameters.AddWithValue("@ConRecepcion", SqlDbType.Bit).Value = conRec;
                            */

                            command.CommandTimeout = 0;
                            connection.Open();
                            string descError = "Todo OK";

                            var res = command.ExecuteScalar();
                            if (res == null)
                            {
                                res = 0;
                            }
                            newProdID = (Int32)res;
                            if (newProdID != 0)
                            {
                                //Obtener descripcion error
                                //Si no aparece mensaje generico
                                string[] keysRuts = ConfigurationManager.AppSettings.AllKeys;
                                foreach (var llave in keysRuts)
                                {
                                    if (llave.StartsWith("DescripcionError"))
                                    {
                                        var resto = llave.Substring(16, llave.Length - 16);
                                        if (resto.Equals(newProdID.ToString()))
                                        {
                                            descError = ConfigurationManager.AppSettings[llave];
                                            break;
                                        }
                                    }
                                }
                            }
                            //String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");

                            //Que nombre poner
                            /*
                            string nm = string.Concat(rutaError, @"\", nameFile, ".r.xml");
                            XmlDocument docx = new XmlDocument();
                            XmlDeclaration docNodeDec = docx.CreateXmlDeclaration("1.0", "UTF-8", "yes");

                            XmlElement docresp = docx.CreateElement("documentos_respuesta");
                            docx.AppendChild(docresp);
                            docx.InsertBefore(docNodeDec, docresp);


                            XmlNode msj = docx.CreateElement("mensaje");
                            msj.AppendChild(docx.CreateTextNode(newProdID == 0 ? "" : descError));
                            docresp.AppendChild(msj);
                            XmlNode est = docx.CreateElement("estado");
                            est.AppendChild(docx.CreateTextNode(newProdID == 0 ? "0" : "-1"));
                            docresp.AppendChild(est);
                            docx.Save(nm);
                            */
                            connection.Close();

                        }
                    }
                }
                return newProdID;
            }
            catch
            {
                return 1;
            }
            finally
            {
            }
        }

        //Detalle
        protected static void ProcesaDetalle(XmlDocument doc, string cadenaConexion, int tipoDTE, int folioFactura, string rutEmisor, string rutReceptor, string spDetalle)
        {

            CultureInfo provider = new CultureInfo("es-CL");
            //A pedido del chino donde debe siempre verse como que no tiene oc
            bool conOC = false;
            //recepcion
            XmlNodeList listaNodosRec = doc.GetElementsByTagName("recepcion");
            bool conRec = listaNodosRec.Count == 0 ? false : true;

            try
            {
                int cantDetalle = 0;

                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    if (conOC)
                    {
                        //<IdDoc>
                        int nodo = 0;
                        XmlNodeList listaNodosDTE = doc.GetElementsByTagName("items");

                        foreach (XmlNode xmlNode in listaNodosDTE)
                        {
                            cantDetalle++;

                            using (SqlCommand command = new SqlCommand(spDetalle, connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                if (xmlNode["item_linea"] != null)
                                {
                                    command.Parameters.Add("@NroLinDet", SqlDbType.Int);
                                    command.Parameters["@NroLinDet"].Value = int.Parse(xmlNode["item_linea"].InnerText);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@NroLinDet", SqlDbType.Int).Value = nodo;
                                }
                                nodo++;
                                command.Parameters.AddWithValue("@TipoDTE ", SqlDbType.Int).Value = tipoDTE;
                                command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = folioFactura;
                                command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.Int).Value = rutEmisor;
                                command.Parameters.AddWithValue("@RUTReceptor", SqlDbType.Int).Value = rutReceptor;


                                if (xmlNode["factor_conversion"] != null)
                                {
                                    command.Parameters.Add(new SqlParameter("@Factor", SqlDbType.Decimal)
                                    {
                                        Precision = 18,
                                        Scale = 6
                                    }).Value = Decimal.Parse(xmlNode["factor_conversion"].InnerText.Replace(".", ","), provider);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Factor", SqlDbType.Decimal).Value = 0;
                                }
                                if (xmlNode["monto_linea"] != null)
                                {
                                    command.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = Decimal.Parse(xmlNode["monto_linea"].InnerText.Replace(".", ","), provider);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = DBNull.Value;

                                }
                                if (xmlNode["centrocosto"] != null)
                                {
                                    command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = xmlNode["centrocosto"].InnerText;
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = DBNull.Value;

                                }
                                if (xmlNode["cuentacontable"] != null)
                                {
                                    command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = xmlNode["cuentacontable"].InnerText;
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = DBNull.Value;

                                }
                                command.Parameters.AddWithValue("@Ultimo", SqlDbType.Bit).Value = listaNodosDTE.Count == cantDetalle ? 1 : 0;
                                command.Parameters.AddWithValue("@Recepcion", SqlDbType.BigInt).Value = DBNull.Value;


                                command.CommandTimeout = 0;
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                    }
                    else
                    {
                        if (conRec)
                        {
                            foreach (XmlNode xmlNode in listaNodosRec)
                            {
                                cantDetalle++;

                                using (SqlCommand command = new SqlCommand(spDetalle, connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@NroLinDet", SqlDbType.Int);
                                    command.Parameters["@NroLinDet"].Value = cantDetalle;
                                    command.Parameters.AddWithValue("@TipoDTE ", SqlDbType.Int).Value = tipoDTE;
                                    command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = folioFactura;
                                    command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.Int).Value = rutEmisor;
                                    command.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = Decimal.Parse(xmlNode["monto"].InnerText.Replace(".", ","), provider);
                                    command.Parameters.AddWithValue("@Factor", SqlDbType.Decimal).Value = 100;
                                    command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = DBNull.Value;
                                    command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = DBNull.Value;
                                    command.Parameters.AddWithValue("@RUTReceptor", SqlDbType.Int).Value = rutReceptor;
                                    command.Parameters.AddWithValue("@Ultimo", SqlDbType.Bit).Value = listaNodosRec.Count == cantDetalle ? 1 : 0;
                                    command.Parameters.AddWithValue("@Recepcion", SqlDbType.BigInt).Value = long.Parse(xmlNode["codigo"].InnerText);

                                    command.CommandTimeout = 0;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                }
                            }
                        }
                        else
                        {
                            //<IdDoc>
                            XmlNodeList listaNodosDTE = doc.SelectNodes("documento_detalle/distribucion/linea");
                            if (listaNodosDTE.Count != 0)
                            {
                                foreach (XmlNode xmlNode in listaNodosDTE)
                                {
                                    cantDetalle++;


                                    using (SqlCommand command = new SqlCommand(spDetalle, connection))
                                    {
                                        command.CommandType = CommandType.StoredProcedure;
                                        if (xmlNode.Attributes["id"] != null)
                                        {
                                            command.Parameters.Add("@NroLinDet", SqlDbType.Int);
                                            command.Parameters["@NroLinDet"].Value = int.Parse(xmlNode.Attributes["id"].InnerText);
                                        }
                                        command.Parameters.AddWithValue("@TipoDTE ", SqlDbType.Int).Value = tipoDTE;
                                        command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = folioFactura;
                                        command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.Int).Value = rutEmisor;
                                        command.Parameters.AddWithValue("@RUTReceptor", SqlDbType.Int).Value = rutReceptor;

                                        if (xmlNode["factor"] != null)
                                        {
                                            command.Parameters.Add(new SqlParameter("@Factor", SqlDbType.Decimal)
                                            {
                                                Precision = 18,
                                                Scale = 6
                                            }).Value = Decimal.Parse(xmlNode["factor"].InnerText.Replace(".", ","), provider);
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("@Factor", SqlDbType.Decimal).Value = 0;
                                        }
                                        if (xmlNode["monto_linea"] != null)
                                        {
                                            command.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = Decimal.Parse(xmlNode["monto_linea"].InnerText.Replace(".", ","), provider);
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = DBNull.Value;

                                        }
                                        if (xmlNode["CENTROCOSTO"] != null)
                                        {
                                            command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = xmlNode["CENTROCOSTO"].InnerText;
                                        }
                                        else
                                        {
                                            if (xmlNode["CentroCosto"] != null)
                                            {
                                                command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = xmlNode["CentroCosto"].InnerText;
                                            }
                                            else
                                            {
                                                command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = DBNull.Value;
                                            }
                                        }
                                        if (xmlNode["CUENTACONTABLE"] != null)
                                        {
                                            command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = xmlNode["CUENTACONTABLE"].InnerText;
                                        }
                                        else
                                        {
                                            if (xmlNode["CuentaContable"] != null)
                                            {
                                                command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = xmlNode["CuentaContable"].InnerText;
                                            }
                                            else
                                            {
                                                command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = DBNull.Value;
                                            }
                                        }
                                        command.Parameters.AddWithValue("@Ultimo", SqlDbType.Bit).Value = listaNodosDTE.Count == cantDetalle ? 1 : 0;
                                        command.Parameters.AddWithValue("@Recepcion", SqlDbType.BigInt).Value = DBNull.Value;


                                        command.CommandTimeout = 0;
                                        connection.Open();
                                        command.ExecuteNonQuery();
                                        connection.Close();
                                    }
                                }
                            }
                            else
                            {
                                using (SqlCommand command = new SqlCommand(spDetalle, connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@NroLinDet", SqlDbType.Int);
                                    command.Parameters["@NroLinDet"].Value = 0;
                                    command.Parameters.AddWithValue("@TipoDTE ", SqlDbType.Int).Value = tipoDTE;
                                    command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = folioFactura;
                                    command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.Int).Value = rutEmisor;
                                    command.Parameters.AddWithValue("@RUTReceptor", SqlDbType.Int).Value = rutReceptor;
                                    command.Parameters.AddWithValue("@Factor", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = DBNull.Value;
                                    command.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = DBNull.Value;
                                    command.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = DBNull.Value;
                                    command.Parameters.AddWithValue("@Ultimo", SqlDbType.Bit).Value = 0;
                                    command.Parameters.AddWithValue("@Recepcion", SqlDbType.BigInt).Value = 99999;

                                    command.CommandTimeout = 0;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        /// <summary>
        /// Ejecucion SQL
        /// </summary>
        /// <param name="cadena"></param>
        /// <param name="comando"></param>
        /// <returns></returns>
        private static DataTable EjecutarSp(string cadena, string comando)
        {
            var myConnection = new SqlConnection(cadena);

            try
            {
                var myComando = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = String.Concat("Select * From ", comando),
                    CommandTimeout = 0,
                    Connection = myConnection
                };

                myConnection.Open();
                var dt = new DataTable();
                dt.Load(myComando.ExecuteReader());
                return dt;
            }
            finally
            {
                myConnection.Close();
            }
        }

        /// <summary>
        /// Ejecucion SQL
        /// </summary>
        /// <param name="cadena"></param>
        /// <param name="comando"></param>
        /// <returns></returns>
        private static void EjecutarSpSinDevolver(string cadena, string comando)
        {
            var myConnection = new SqlConnection(cadena);

            try
            {
                var myComando = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = comando,
                    CommandTimeout = 0,
                    Connection = myConnection
                };


                myConnection.Open();
                myComando.ExecuteNonQuery();
            }
            finally
            {
                myConnection.Close();

            }
        }

        static string GetFileName(FileInfo fileInfo)
        {
            return Path.GetFileNameWithoutExtension(fileInfo.Name);
        }

    }
}

