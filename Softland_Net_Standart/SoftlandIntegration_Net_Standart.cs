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

namespace Softland
{


    public class SoftlandIntegration_Net_Standart
    {


        public void ProcesarDocumentos()
        {

            string configuration = "configurationSoftland.xml";

            //Creo los Objetos de tipo XMLDocument
            XmlDocument xmlDocDTE = new XmlDocument();
            XmlDocument xmlDocConfiguration = new XmlDocument();

            //Cargo el XML de configuarcion

            xmlDocConfiguration.Load(configuration);

            //Cargo la cadena de conexion

            var cadenaSQL = "Data Source=.\\SQL2017;Initial Catalog=Softland;Persist Security Info=True;User ID=sa;Password=Versat2022*";

            //recorro los documentos a procesar

            foreach (string file in Directory.GetFiles("Documentos/", "*.xml"))
            {

                xmlDocDTE.Load(file);
                ProcesarDocumento(xmlDocDTE, xmlDocConfiguration, cadenaSQL);

            }

        }

        private string ProcesarDocumento(XmlDocument doc, XmlDocument xmlDocConfiguration, string cadenaConexion)
        {
            //int newProdID = 0;
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
                XmlNodeList xmlConfigurationTagFieldList = xmlDocConfiguration.SelectNodes("/Transform/field[position() <= 500]");

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
                    var nombreEtiqueta = xmlConfigurationTagField.SelectSingleNode("nombreEtiqueta").InnerText;

                    //Nombre Etiqueta Padre
                    var nombreEtiquetaPadre = xmlConfigurationTagField.SelectSingleNode("nombreEtiquetaPadre").InnerText;

                    //Procesamiento de la Cabecera
                    if (procedimiento == "SpCabecera")
                    {
                        //Obtengo los nodos del Encbezado
                        XmlNodeList listaNodosDTE = doc.GetElementsByTagName("Encabezado");

                        //Recorro los nodos del encabezado
                        foreach (XmlNode xmlNode in listaNodosDTE)
                        {
                            //Se agregan los valores a los parametros del SP Cabecera
                            if (xmlNode[nombreEtiquetaPadre] != null)
                            {
                                if (tipoDato == "Int")
                                {
                                    if (xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta).Count != 0)
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta)[0].InnerText;
                                    }
                                    else
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = DBNull.Value;
                                    }
                                }
                                else if (tipoDato == "DateTime")
                                {
                                    if (xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta).Count != 0)
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.DateTime).Value = DateTime.Parse(xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta)[0].InnerText);
                                    }
                                    else
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.DateTime).Value = DBNull.Value;
                                    }
                                }
                                else if (tipoDato == "VarChar")
                                {
                                    if (xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta).Count != 0)
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta)[0].InnerText;
                                    }
                                    else
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = DBNull.Value;
                                    }
                                }
                                else if (tipoDato == "NChar")
                                {
                                    if (xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta).Count != 0)
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.NChar).Value = xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta)[0].InnerText;
                                    }
                                    else
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.NChar).Value = DBNull.Value;
                                    }
                                }
                                else if (tipoDato == "Decimal")
                                {
                                    if (xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta).Count != 0)
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta)[0].InnerText;
                                    }
                                    else
                                    {
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = DBNull.Value;

                                    }
                                }
                            }
                            else
                            {

                                if (tipoDato == "Int")
                                {
                                  
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = DBNull.Value;
                                    
                                }


                                else if (tipoDato == "DateTime")
                                {
                                  
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.DateTime).Value = DBNull.Value;
                                    
                                }

                                else if (tipoDato == "VarChar")
                                {
                                   
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = DBNull.Value;
                                    
                                }


                                else if (tipoDato == "NChar")
                                {
                                   
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.NChar).Value = DBNull.Value;
                                    
                                }
                                else if (tipoDato == "Decimal")
                                {
                                 
                                        commandCabecera.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = DBNull.Value;

                                    
                                }




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
                XmlNodeList listaNodosDTE1 = doc.GetElementsByTagName("Encabezado/Referencia");
                if (listaNodosDTE1 != null)
                {
                    for (int i = 0; i < listaNodosDTE1.Count; i++)
                    {
                        XmlNode listaNodosRef = listaNodosDTE1[i];
                        if (listaNodosRef["TpoDocRef"].InnerText == "33" || listaNodosRef["TpoDocRef"].InnerText == "34")
                        {
                            folioReferencia = listaNodosRef["FolioRef"].InnerText;
                            break;
                        }

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
                XmlNodeList listaNodosDTEDetalles = doc.GetElementsByTagName("Detalle");

                //Se recorren los nodos Detalle
                foreach (XmlNode xmlNode in listaNodosDTEDetalles)
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
                        var nombreEtiqueta = xmlConfigurationTagField.SelectSingleNode("nombreEtiqueta").InnerText;

                        //Nombre Etiqueta Padre
                        var nombreEtiquetaPadre = xmlConfigurationTagField.SelectSingleNode("nombreEtiquetaPadre").InnerText;

                        //Asigno los parametros al SP Detalle
                        if (procedimiento == "SpDetalle")
                        {
                            if (tipoDato == "Int")
                            {
                                if (xmlNode[nombreEtiqueta] != null)
                                {
                                    commandDetalle.Parameters.Add(parametro, SqlDbType.Int);
                                    commandDetalle.Parameters[parametro].Value = int.Parse(xmlNode[nombreEtiqueta].InnerText);
                                }
                                else
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = nodo;
                                }
                                nodo++;
                            }

                            if (tipoDato == "Decimal" && parametro == "@Factor")
                            {
                                if (xmlNode[nombreEtiqueta] != null)
                                {
                                    commandDetalle.Parameters.Add(new SqlParameter("@Factor", SqlDbType.Decimal)
                                    {
                                        Precision = 18,
                                        Scale = 6
                                    }).Value = Decimal.Parse(xmlNode["factor_conversion"].InnerText.Replace(".", ","), provider);
                                }
                                else
                                {
                                    commandDetalle.Parameters.AddWithValue("@Factor", SqlDbType.Decimal).Value = 0;
                                }
                            }


                            else if (tipoDato == "Decimal" && parametro != "@Factor")
                            {
                                if (xmlNode[nombreEtiqueta] != null)
                                {
                                    var x = Decimal.Parse(xmlNode[nombreEtiqueta].InnerText.Replace(".", ","), provider);
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = Decimal.Parse(xmlNode[nombreEtiqueta].InnerText.Replace(".", ","), provider);
                                }
                                else
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.Decimal).Value = DBNull.Value;

                                }
                            }

                            else if (tipoDato == "VarChar")
                            {
                                if (xmlNode[nombreEtiqueta] != null)
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = xmlNode[nombreEtiqueta].InnerText;
                                }
                                else
                                {
                                    commandDetalle.Parameters.AddWithValue(parametro, SqlDbType.VarChar).Value = DBNull.Value;

                                }

                            }
                        }


                    }

                    XmlNodeList listaNodosDTEEncabezado = doc.GetElementsByTagName("Encabezado");
                    foreach (XmlNode xmlNodeEncabezado in listaNodosDTEEncabezado)
                    {

                        if (xmlNodeEncabezado["IdDoc"] != null)
                        {
                            //((((System.Xml.XmlElementList)(xmlNode["IdDoc"].GetElementsByTagName("TipoDespacho")))).curElem).InnerText
                            if (xmlNodeEncabezado["IdDoc"].GetElementsByTagName("TipoDTE").Count != 0)
                            {

                                commandDetalle.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = xmlNodeEncabezado["IdDoc"].GetElementsByTagName("TipoDTE")[0].InnerText;
                            }
                            else
                            {
                                commandDetalle.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = DBNull.Value;

                            }
                            if (xmlNodeEncabezado["IdDoc"].GetElementsByTagName("Folio").Count != 0)
                            {

                                commandDetalle.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = xmlNodeEncabezado["IdDoc"].GetElementsByTagName("Folio")[0].InnerText;
                            }
                            else
                            {
                                commandDetalle.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = DBNull.Value;

                            }
                        }
                        if (xmlNodeEncabezado["Emisor"] != null)
                        {
                            if (xmlNodeEncabezado["Emisor"].GetElementsByTagName("RUTEmisor").Count != 0)
                            {
                                commandDetalle.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = xmlNodeEncabezado["Emisor"].GetElementsByTagName("RUTEmisor")[0].InnerText;
                            }
                            else
                            {
                                commandDetalle.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                        }

                        if (xmlNodeEncabezado["Receptor"] != null)
                        {
                            if (xmlNodeEncabezado["Receptor"].GetElementsByTagName("RUTRecep").Count != 0)
                            {
                                commandDetalle.Parameters.AddWithValue("@RUTReceptor", SqlDbType.VarChar).Value = xmlNodeEncabezado["Receptor"].GetElementsByTagName("RUTRecep")[0].InnerText;
                            }
                            else
                            {
                                commandDetalle.Parameters.AddWithValue("@RUTReceptor", SqlDbType.VarChar).Value = DBNull.Value;

                            }
                        }
                    }


                    commandDetalle.CommandTimeout = 0;
                    commandDetalle.ExecuteNonQuery();

                }

                trans.Commit();
                connection.Close();

                return "OK";

            }
            catch
            {
                trans.Rollback();
                connection.Close();
                return "Error";
            }

        }

    }
}
