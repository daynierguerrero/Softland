using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Softland
{


    public class SoftlandIntegration
    {

        public int ProcesarDocumento(XmlDocument doc, XmlDocument xmlDocConfiguration, string cadenaConexion)
        {
            int newProdID = 0;
            SqlConnection connection = new SqlConnection(cadenaConexion);
            connection.Open();
            SqlTransaction trans = connection.BeginTransaction();
            try
            {
                
                
                SqlCommand commandCabecera = new SqlCommand("spCabecera", connection);

                commandCabecera.Transaction = trans;

                XmlNodeList listaNodosRec = doc.GetElementsByTagName("recepcion");

                bool conRec = listaNodosRec.Count == 0 ? false : true;



                commandCabecera.CommandType = CommandType.StoredProcedure;
                
                CultureInfo provider = new CultureInfo("es-CL");
                int tesCounter = 0;


                


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

                    if (procedimiento == "SpCabecera")
                    {
                        XmlNodeList listaNodosDTE = doc.GetElementsByTagName("Encabezado");
                        foreach (XmlNode xmlNode in listaNodosDTE)
                        {


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



                            }
                        }
                    }
                    //Detalle
                    else
                    {

                        

                    }


                }




                ///fin del recorrido de los nodos field

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
                //connection.Open();
                string descError = "Todo OK";

                var res = commandCabecera.ExecuteScalar();
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

                //connection.Close();



                //Ejecuto detalles
                int nodo = 0;
                XmlNodeList listaNodosDTEDetalles = doc.GetElementsByTagName("Detalle");

                foreach (XmlNode xmlNode in listaNodosDTEDetalles)
                {
                    SqlCommand commandDetalle = new SqlCommand("spDetalle", connection);
                    commandDetalle.Transaction = trans;
                    commandDetalle.CommandType = CommandType.StoredProcedure;
                    tesCounter++; 
                    // cantDetalle++;

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


                    


                        #region Fijo

                        // command.Parameters.AddWithValue("@TipoDTE ", SqlDbType.Int).Value = tipoDTE;
                        // command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = folioFactura;
                        // command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.Int).Value = rutEmisor;
                        //command.Parameters.AddWithValue("@RUTReceptor", SqlDbType.Int).Value = rutReceptor;

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


                        #endregion

                    






                    //command.Parameters.AddWithValue("@Ultimo", SqlDbType.Bit).Value = listaNodosDTE.Count == cantDetalle ? 1 : 0;
                    //command.Parameters.AddWithValue("@Recepcion", SqlDbType.BigInt).Value = DBNull.Value;


                    commandDetalle.CommandTimeout = 0;
                    //connection.Open();
                    commandDetalle.ExecuteNonQuery();
                    //connection.Close();

                }






                




                //A pedido del chino donde debe siempre verse como que no tiene oc
                //bool conOC = false;
                bool conOC = true;
                //recepcion
                //XmlNodeList listaNodosRec = doc.GetElementsByTagName("recepcion");
                //bool conRec = listaNodosRec.Count == 0 ? false : true;
                //bool conRec = true;

                // int cantDetalle = 0;



                //if (conOC)
                // {
                //<IdDoc>


                // }



                ///fin detalle
                ///
                trans.Commit();


                return newProdID;

            }
            catch
            {
                trans.Rollback();
                return 1;
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

