using System;
using System.Collections.Generic;

namespace SoftlandAPI
{
    public class IssuedDocumentReponse
    {


        public class CreateIssuedDocumentRequest
        {
            /// <summary>
            /// Tipo de DTE
            /// </summary>
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
}
