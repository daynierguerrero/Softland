using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace Softland
{


    public class SoftlandIntegration_Net_Standart
    {


        public string ProcesarDocumentos()
        {


            int contador = 0;

            string configuration = "configurationSoftland.xml";

            //Creo los Objetos de tipo XMLDocument
            
            XmlDocument xmlDocConfiguration = new XmlDocument();

            //Cargo el XML de configuarcion

            xmlDocConfiguration.Load(configuration);

            //Cargo la cadena de conexion

            var cadenaSQL = "Data Source=.\\SQL2017;Initial Catalog=Softland;Persist Security Info=True;User ID=sa;Password=Versat2022*";

            //recorro los documentos a procesar y se envian de uno en uno al metodo que los inserta en la BD

            var documentos = Directory.GetFiles("DocumentosJSON/", "*.json");

            foreach (string file in documentos)
            {
                string jsonString = File.ReadAllText($@"{file}");
                JsonNode documentJSON = JsonNode.Parse(jsonString)!;

                contador += ProcesarDocumento(documentJSON, xmlDocConfiguration, cadenaSQL);

            }
            return $"{contador} Procesados de {documentos.Length}";

        }

        private int ProcesarDocumento(JsonNode documentJSON, XmlDocument xmlDocConfiguration, string cadenaConexion)
        {
            //Se crea la conexion
            SqlConnection connection = new SqlConnection(cadenaConexion);

            //Se abre la conexion
            connection.Open();

            //Se crea una transaccion para la conexion
            SqlTransaction trans = connection.BeginTransaction();

            try
            {
                //Se crea el comando para la cabecera                
                SqlCommand commandCabecera = new SqlCommand("spCabecera", connection);

                //Se le asigna la transaccion creada al comando de la cabecera
                commandCabecera.Transaction = trans;

                //Se define el tipo de comando al comando de la cabecera
                commandCabecera.CommandType = CommandType.StoredProcedure;

                //Se define cultureProvider
                CultureInfo provider = new CultureInfo("es-CL");

                //Obtengo el el listado de nodos donde se especifican los diferentes fields
                XmlNodeList xmlConfigurationTagFieldList = xmlDocConfiguration.SelectNodes("/Transform/field");

                //Itero cada uno de los nodos <field>
                foreach (XmlNode xmlConfigurationTagField in xmlConfigurationTagFieldList)
                {


                    //Procedimiento a utilizar
                    var procedimiento = xmlConfigurationTagField.SelectSingleNode("procedimiento").InnerText;

                    //Nombre del parametro en el SP
                    var parametro = xmlConfigurationTagField.SelectSingleNode("parametro").InnerText;

                    //Tipo de dato del parametro
                    var tipoDato = xmlConfigurationTagField.SelectSingleNode("tipoDato").InnerText;

                    //Valor por defecto
                    var valorPorDefecto = xmlConfigurationTagField.SelectSingleNode("valorPorDefecto").InnerText;

                    //Nombre Etiqueta
                    var nombreAtributo = xmlConfigurationTagField.SelectSingleNode("nombreEtiqueta").InnerText;

                    //Nombre Etiqueta Padre
                    var nombreAtributoPadre = xmlConfigurationTagField.SelectSingleNode("nombreEtiquetaPadre").InnerText;

                    //Procesamiento de la Cabecera
                    if (procedimiento == "SpCabecera")
                    {
                        

                        if (tipoDato == "Int")
                        {
                            if (!(documentJSON[nombreAtributo] is null))
                            {
                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = Convert.ToInt32((documentJSON[nombreAtributo]!).ToString());
                            }
                            else
                            {
                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = DBNull.Value;
                            }
                        }
                        else if (tipoDato == "DateTime")
                        {
                            if (!(documentJSON[nombreAtributo] is null))
                            {

                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.DateTime).Value = DateTime.Parse((documentJSON[nombreAtributo]!).ToString());
                            }
                            else
                            {
                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.DateTime).Value = DBNull.Value;
                            }
                        }
                        else if (tipoDato == "VarChar")
                        {
                            if (!(documentJSON[nombreAtributo] is null))
                            {
                                string x = (documentJSON[nombreAtributo]!).ToString();
                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = (documentJSON[nombreAtributo]!).ToString();
                            }
                            else
                            {
                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = DBNull.Value;
                            }
                        }
                        else if (tipoDato == "NChar")
                        {
                            if (!(documentJSON[nombreAtributo] is null))
                            {

                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.NChar).Value = (documentJSON[nombreAtributo]!).ToString();
                            }
                            else
                            {
                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.NChar).Value = DBNull.Value;
                            }
                        }
                        else if (tipoDato == "Decimal")
                        {
                            if (!(documentJSON[nombreAtributo] is null))
                            {
                                decimal x = Convert.ToDecimal((documentJSON[nombreAtributo]!).ToString());
                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = Convert.ToDecimal((documentJSON[nombreAtributo]!).ToString());
                            }
                            else
                            {
                                commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = DBNull.Value;

                            }
                        }

                    }
                }



                //Se insertan en null los parametros que no aparecen en el DTE
                commandCabecera.Parameters.AddWithValue("@Tara", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@CodUnidMedTara", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@PesoBruto", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@CodUnidPesoBruto", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@PesoNeto", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@CodUnidPesoNeto", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@TotItems", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@TotBultos", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@CodTpoBultos", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@CantBultos", SqlDbType.Int).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@Marcas", SqlDbType.VarChar).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@Sello", SqlDbType.VarChar).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@EmisorSello", SqlDbType.VarChar).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@MntFlete", SqlDbType.VarChar).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@MntSeguro", SqlDbType.VarChar).Value = DBNull.Value;



                commandCabecera.Parameters.AddWithValue("@Estadoley", SqlDbType.VarChar).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@FechaLey", SqlDbType.DateTime).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@FechaRecepcionSII", SqlDbType.DateTime).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = DBNull.Value;
                commandCabecera.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = DBNull.Value;


                var folioReferencia = "";
                JsonArray listaNodosDTE1 = documentJSON["IssuedDocumentReferences"]!.AsArray();

                if (listaNodosDTE1 != null && listaNodosDTE1.Count > 0)
                {
                    for (int i = 0; i < listaNodosDTE1.Count; i++)
                    {
                        JsonNode listaNodosRef = listaNodosDTE1[i]!;

                        folioReferencia = listaNodosRef["Folio"]!.ToString();
                        break;


                    }
                }
                if (!string.IsNullOrEmpty(folioReferencia))
                {
                    commandCabecera.Parameters.AddWithValue("@FolioReferencia", SqlDbType.NChar).Value = folioReferencia;
                }
                else
                {
                    commandCabecera.Parameters.AddWithValue("@FolioReferencia", SqlDbType.NChar).Value = DBNull.Value;
                }

                commandCabecera.CommandTimeout = 0;
                commandCabecera.ExecuteScalar();


                //Procesamiento de los detalles
                int nodo = 0;

                //Se obtienen los nodos Detalle
                
                JsonArray listaNodosDTEDetalles = documentJSON["IssuedDocumentDetails"]!.AsArray();

                //Se recorren los nodos Detalle
                foreach (JsonNode jsonNode in listaNodosDTEDetalles)
                {
                    //Se crea en command para los detalles
                    SqlCommand commandDetalle = new SqlCommand("spDetalle", connection);

                    //Se asigna la transaccion al command
                    commandDetalle.Transaction = trans;
                    commandDetalle.CommandType = CommandType.StoredProcedure;

                    //Itero cada uno de los nodos <field>
                    foreach (XmlNode xmlConfigurationTagField in xmlConfigurationTagFieldList)
                    {

                        //Procedimiento a utilizar
                        var procedimiento = xmlConfigurationTagField.SelectSingleNode("procedimiento").InnerText;

                        //Nombre del parametro en el SP
                        var parametro = xmlConfigurationTagField.SelectSingleNode("parametro").InnerText;

                        //Tipo de dato del parametro
                        var tipoDato = xmlConfigurationTagField.SelectSingleNode("tipoDato").InnerText;

                        //Valor por defecto
                        var valorPorDefecto = xmlConfigurationTagField.SelectSingleNode("valorPorDefecto").InnerText;

                        //Nombre Etiqueta
                        var nombreAtributo = xmlConfigurationTagField.SelectSingleNode("nombreEtiqueta").InnerText;

                        //Nombre Etiqueta Padre
                        var nombreAtributoPadre = xmlConfigurationTagField.SelectSingleNode("nombreEtiquetaPadre").InnerText;

                        //Asigno los parametros al SP Detalle
                        if (procedimiento == "SpDetalle")
                        {
                            if (tipoDato == "Int")
                            {



                                if (!(jsonNode![nombreAtributo] is null))
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = Convert.ToInt32((jsonNode[nombreAtributo]!).ToString());
                                }
                                else
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = DBNull.Value;
                                }

                            }



                            else if (tipoDato == "Decimal" && parametro == "@Factor")
                            {
                                if (!(jsonNode![nombreAtributo] is null))
                                {
                                    commandDetalle.Parameters.Add(new SqlParameter("@Factor", SqlDbType.Decimal)
                                    {
                                        Precision = 18,
                                        Scale = 6
                                    }).Value = Decimal.Parse((jsonNode["factor_conversion"]!).ToString().Replace(".", ","), provider);
                                }
                                else
                                {
                                    commandDetalle.Parameters.AddWithValue("@Factor", SqlDbType.Decimal).Value = 0;
                                }
                            }


                            else if (tipoDato == "Decimal" && parametro != "@Factor")
                            {
                                if (!(jsonNode![nombreAtributo] is null))
                                {
                                    decimal x = Decimal.Parse((jsonNode[nombreAtributo]!).ToString().Replace(".", ","), provider);
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = Decimal.Parse((jsonNode[nombreAtributo]!).ToString().Replace(".", ","), provider);
                                }
                                else
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = DBNull.Value;

                                }
                            }

                            else if (tipoDato == "VarChar")
                            {
                                if (!(jsonNode![nombreAtributo] is null))
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = jsonNode[nombreAtributo]!.ToString();
                                }
                                else
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = DBNull.Value;

                                }

                            }
                        }


                    }

                    if (!(documentJSON["TipoDTE"] is null))
                    {

                        commandDetalle.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = Convert.ToInt32((documentJSON["TipoDTE"]!).ToString());
                    }
                    else
                    {

                        commandDetalle.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = DBNull.Value;

                    }
                    if (!(documentJSON["Folio"] is null))
                    {

                        commandDetalle.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = Convert.ToInt32((documentJSON["Folio"]!).ToString());
                    }
                    else
                    {
                        commandDetalle.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = DBNull.Value;

                    }

                    if (!(documentJSON["RUTEmisor"] is null))
                    {

                        commandDetalle.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = (documentJSON["RUTEmisor"]!).ToString();
                    }

                    else
                    {
                        commandDetalle.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = DBNull.Value;

                    
                    }

                    if (!(documentJSON["RUTReceptor"] is null))
                    {

                        commandDetalle.Parameters.AddWithValue("@RUTReceptor", SqlDbType.VarChar).Value = (documentJSON["RUTReceptor"]!).ToString();
                    }

                    else
                    {
                        commandDetalle.Parameters.AddWithValue("@RUTReceptor", SqlDbType.VarChar).Value = DBNull.Value;

                    }
                    

                    commandDetalle.CommandTimeout = 0;
                    commandDetalle.ExecuteNonQuery();

                }
                trans.Commit();
                connection.Close();

                return 1;

            }
            catch
            {
                trans.Rollback();
                connection.Close();
                return 0;
            }

        }

    }

    public class IssuedDocumentReponse
    {


        public int ID { set; get; }
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

        public IList<IssuedDocumentDetail> IssuedDocumentDetails { get; set; }

        public IList<IssuedDocumentReference> IssuedDocumentReferences { get; set; }

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

        public class IssuedDocumentDetail
        {
            /// <summary>
            /// Numero de la linea de detalle
            /// </summary>
            public int NroLinDet { set; get; }

            /// <summary>
            /// Codigo del Item
            /// </summary>
            public string CdgItem { set; get; }

            /// <summary>
            /// Nombre del Item
            /// </summary>
            public string NmbItem { set; get; }

            /// <summary>
            /// Descripcion del Item
            /// </summary>
            public string DscItem { set; get; }

            /// <summary>
            /// Cantidad  del Item
            /// </summary>
            public int QtyItem { set; get; }

            /// <summary>
            /// Unidad de medida del Item
            /// </summary>
            public string UnmdItem { set; get; }

            /// <summary>
            /// Precio del Item
            /// </summary>
            public decimal PrcItem { set; get; }

            /// <summary>
            /// Código Impuesto o retenciones
            /// </summary>
            public string CodImpAdic { set; get; }

            /// <summary>
            /// Porciento de Descuento del Item
            /// </summary>
            public decimal DescuentoPct { set; get; }

            /// <summary>
            /// Monto del descuento del Item
            /// </summary>
            public long DescuentoMonto { set; get; }

            /// <summary>
            /// Porciento de recargo del Item
            /// </summary>
            public decimal RecargoPct { set; get; }

            /// <summary>
            /// Monto Recargo del Item
            /// </summary>
            public long RecargoMonto { set; get; }

            /// <summary>
            /// Monto de la Linea
            /// </summary>
            public long MontoItem { set; get; }

            /// <summary>
            /// Centro de costo de la Linea
            /// </summary>
            public string CostCenter { set; get; }

            /// <summary>
            /// Cuenta contable de la linea
            /// </summary>
            public string AccountingAccount { set; get; }
        }

        public class IssuedDocumentReference
        {
            /// <summary>
            /// Numero de la Linea de Referencia
            /// </summary>
            public int NroLinRef { set; get; }

            /// <summary>
            /// Tipo deocumento referencia
            /// </summary>
            public string CodDocumentType { get; set; }

            /// <summary>
            /// Folio documento referencia
            /// </summary>
            public string Folio { set; get; }

            /// <summary>
            /// Codigo motivo del documento referencia
            /// </summary>
            public string CodRef { set; get; }

            /// <summary>
            /// Fecha del documento Referencia
            /// </summary>
            public DateTime FchRef { set; get; }

            /// <summary>
            /// Texto libre razon de la referencia
            /// </summary>
            public string RazonRef { set; get; }

            /// <summary>
            /// Sólo si el documento de referencia es
            ///de tipo tributario y fue emitido por otro
            ///contribuyente
            /// </summary>
            public string RUTOtr { set; get; }
        }
    }
}


