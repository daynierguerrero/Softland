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

        public int ProcesaCabecera(XmlDocument doc, XmlDocument xmlDocConfiguration, string cadenaConexion, string spCabecera)
        {
            int newProdID = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    XmlNodeList listaNodosRec = doc.GetElementsByTagName("recepcion");
                    bool conRec = listaNodosRec.Count == 0 ? false : true;

                    using (SqlCommand command = new SqlCommand(spCabecera, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


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

                            
                            XmlNodeList listaNodosDTE = doc.GetElementsByTagName("Encabezado");
                            foreach (XmlNode xmlNode in listaNodosDTE)
                            {


                                if (xmlNode[nombreEtiquetaPadre] != null)
                                {
                                    
                                    if (xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta).Count != 0)
                                    {

                                        command.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = xmlNode[nombreEtiquetaPadre].GetElementsByTagName(nombreEtiqueta)[0].InnerText;
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue(parametro, SqlDbType.Int).Value = DBNull.Value;

                                    }


                                }
                            }


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
                            command.Parameters.AddWithValue("@FolioReferencia", SqlDbType.NChar).Value = folioReferencia;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@FolioReferencia", SqlDbType.NChar).Value = DBNull.Value;
                        }

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

                        connection.Close();



                        //Ejecuto detalles




                        CultureInfo provider = new CultureInfo("es-CL");
                        //A pedido del chino donde debe siempre verse como que no tiene oc
                        //bool conOC = false;
                        bool conOC = true;
                        //recepcion
                        //XmlNodeList listaNodosRec = doc.GetElementsByTagName("recepcion");
                        //bool conRec = listaNodosRec.Count == 0 ? false : true;
                        //bool conRec = true;

                        int cantDetalle = 0;

                        using (SqlConnection connection2 = new SqlConnection(cadenaConexion))
                        {
                            if (conOC)
                            {
                                //<IdDoc>
                                int nodo = 0;
                                XmlNodeList listaNodosDTE = doc.GetElementsByTagName("Detalle");

                                foreach (XmlNode xmlNode in listaNodosDTE)
                                {
                                    cantDetalle++;

                                    using (SqlCommand command2 = new SqlCommand("spDetalle", connection2))
                                    {
                                        command2.CommandType = CommandType.StoredProcedure;
                                        if (xmlNode["NroLinDet"] != null)
                                        {
                                            command2.Parameters.Add("@NroLinDet", SqlDbType.Int);
                                            command2.Parameters["@NroLinDet"].Value = int.Parse(xmlNode["NroLinDet"].InnerText);
                                        }
                                        else
                                        {
                                            command2.Parameters.AddWithValue("@NroLinDet", SqlDbType.Int).Value = nodo;
                                        }
                                        nodo++;
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

                                                    command2.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = xmlNodeEncabezado["IdDoc"].GetElementsByTagName("TipoDTE")[0].InnerText;
                                                }
                                                else
                                                {
                                                    command2.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = DBNull.Value;

                                                }
                                                if (xmlNodeEncabezado["IdDoc"].GetElementsByTagName("Folio").Count != 0)
                                                {

                                                    command2.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = xmlNodeEncabezado["IdDoc"].GetElementsByTagName("Folio")[0].InnerText;
                                                }
                                                else
                                                {
                                                    command2.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = DBNull.Value;

                                                }
                                            }
                                            if (xmlNodeEncabezado["Emisor"] != null)
                                            {
                                                if (xmlNodeEncabezado["Emisor"].GetElementsByTagName("RUTEmisor").Count != 0)
                                                {
                                                    command2.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = xmlNodeEncabezado["Emisor"].GetElementsByTagName("RUTEmisor")[0].InnerText;
                                                }
                                                else
                                                {
                                                    command2.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = DBNull.Value;

                                                }
                                            }

                                            if (xmlNodeEncabezado["Receptor"] != null)
                                            {
                                                if (xmlNodeEncabezado["Receptor"].GetElementsByTagName("RUTRecep").Count != 0)
                                                {
                                                    command2.Parameters.AddWithValue("@RUTReceptor", SqlDbType.VarChar).Value = xmlNodeEncabezado["Receptor"].GetElementsByTagName("RUTRecep")[0].InnerText;
                                                }
                                                else
                                                {
                                                    command2.Parameters.AddWithValue("@RUTReceptor", SqlDbType.VarChar).Value = DBNull.Value;

                                                }
                                            }
                                        }



                                        if (xmlNode["factor_conversion"] != null)
                                        {
                                            command2.Parameters.Add(new SqlParameter("@Factor", SqlDbType.Decimal)
                                            {
                                                Precision = 18,
                                                Scale = 6
                                            }).Value = Decimal.Parse(xmlNode["factor_conversion"].InnerText.Replace(".", ","), provider);
                                        }
                                        else
                                        {
                                            command2.Parameters.AddWithValue("@Factor", SqlDbType.Decimal).Value = 0;
                                        }
                                        if (xmlNode["MontoItem"] != null)
                                        {
                                            var x = Decimal.Parse(xmlNode["MontoItem"].InnerText.Replace(".", ","), provider);
                                            command2.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = Decimal.Parse(xmlNode["MontoItem"].InnerText.Replace(".", ","), provider);
                                        }
                                        else
                                        {
                                            command2.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = DBNull.Value;

                                        }
                                        if (xmlNode["centrocosto"] != null)
                                        {
                                            command2.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = xmlNode["centrocosto"].InnerText;
                                        }
                                        else
                                        {
                                            command2.Parameters.AddWithValue("@CentroCosto", SqlDbType.VarChar).Value = DBNull.Value;

                                        }
                                        if (xmlNode["cuentacontable"] != null)
                                        {
                                            command2.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = xmlNode["cuentacontable"].InnerText;
                                        }
                                        else
                                        {
                                            command2.Parameters.AddWithValue("@CuentaContable", SqlDbType.VarChar).Value = DBNull.Value;

                                        }
                                        //command.Parameters.AddWithValue("@Ultimo", SqlDbType.Bit).Value = listaNodosDTE.Count == cantDetalle ? 1 : 0;
                                        //command.Parameters.AddWithValue("@Recepcion", SqlDbType.BigInt).Value = DBNull.Value;


                                        command2.CommandTimeout = 0;
                                        connection2.Open();
                                        command2.ExecuteNonQuery();
                                        connection2.Close();
                                    }
                                }

                            }

                        }

                        ///fin detalle

                    }
                    return newProdID;
                }
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
        public void ProcesaDetalle(XmlDocument doc, XmlDocument xmlDocConfiguration, string cadenaConexion, string spDetalle)
        {

            CultureInfo provider = new CultureInfo("es-CL");
            //A pedido del chino donde debe siempre verse como que no tiene oc
            //bool conOC = false;
            bool conOC = true;
            //recepcion
            XmlNodeList listaNodosRec = doc.GetElementsByTagName("recepcion");
            //bool conRec = listaNodosRec.Count == 0 ? false : true;
            bool conRec = true;
            try
            {
                int cantDetalle = 0;

                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    if (conOC)
                    {
                        //<IdDoc>
                        int nodo = 0;
                        XmlNodeList listaNodosDTE = doc.GetElementsByTagName("Detalle");

                        foreach (XmlNode xmlNode in listaNodosDTE)
                        {
                            cantDetalle++;

                            using (SqlCommand command = new SqlCommand(spDetalle, connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                if (xmlNode["NroLinDet"] != null)
                                {
                                    command.Parameters.Add("@NroLinDet", SqlDbType.Int);
                                    command.Parameters["@NroLinDet"].Value = int.Parse(xmlNode["NroLinDet"].InnerText);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@NroLinDet", SqlDbType.Int).Value = nodo;
                                }
                                nodo++;
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

                                            command.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = xmlNodeEncabezado["IdDoc"].GetElementsByTagName("TipoDTE")[0].InnerText;
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("@TipoDTE", SqlDbType.Int).Value = DBNull.Value;

                                        }
                                        if (xmlNodeEncabezado["IdDoc"].GetElementsByTagName("Folio").Count != 0)
                                        {

                                            command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = xmlNodeEncabezado["IdDoc"].GetElementsByTagName("Folio")[0].InnerText;
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("@Folio", SqlDbType.Int).Value = DBNull.Value;

                                        }
                                    }
                                    if (xmlNodeEncabezado["Emisor"] != null)
                                    {
                                        if (xmlNodeEncabezado["Emisor"].GetElementsByTagName("RUTEmisor").Count != 0)
                                        {
                                            command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = xmlNodeEncabezado["Emisor"].GetElementsByTagName("RUTEmisor")[0].InnerText;
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("@RUTEmisor", SqlDbType.VarChar).Value = DBNull.Value;

                                        }
                                    }

                                    if (xmlNodeEncabezado["Receptor"] != null)
                                    {
                                        if (xmlNodeEncabezado["Receptor"].GetElementsByTagName("RUTRecep").Count != 0)
                                        {
                                            command.Parameters.AddWithValue("@RUTReceptor", SqlDbType.VarChar).Value = xmlNodeEncabezado["Receptor"].GetElementsByTagName("RUTRecep")[0].InnerText;
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("@RUTReceptor", SqlDbType.VarChar).Value = DBNull.Value;

                                        }
                                    }
                                }













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
                                if (xmlNode["MontoItem"] != null)
                                {
                                    var x = Decimal.Parse(xmlNode["MontoItem"].InnerText.Replace(".", ","), provider);
                                    command.Parameters.AddWithValue("@MontoItem", SqlDbType.Decimal).Value = Decimal.Parse(xmlNode["MontoItem"].InnerText.Replace(".", ","), provider);
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
                                //command.Parameters.AddWithValue("@Ultimo", SqlDbType.Bit).Value = listaNodosDTE.Count == cantDetalle ? 1 : 0;
                                //command.Parameters.AddWithValue("@Recepcion", SqlDbType.BigInt).Value = DBNull.Value;


                                command.CommandTimeout = 0;
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }

                    }


                    /*

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
                                    //command.Parameters.AddWithValue("@Ultimo", SqlDbType.Bit).Value = 0;
                                    //command.Parameters.AddWithValue("@Recepcion", SqlDbType.BigInt).Value = 99999;

                                    command.CommandTimeout = 0;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                }

                            }
                        }
                    }
                    */
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

