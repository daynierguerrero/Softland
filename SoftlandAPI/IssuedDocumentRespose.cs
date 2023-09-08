namespace SoftlandAPI
{
    public class IssuedDocumentRespose
    {
        

        public DateTime ERPSynchronizationDatetime { get; set; }
        public int? ERPSynchronizationStatus { get; set; }
        public int? IdAccountingAccount { get; set; }
        public int? IdCostCenter { get; set; }
        public string Environment { get; set; }
        public DateTime LastUpdate { get; set; }
        public string User { get; set; }
        public int? SIIState { get; set; }
        public long MntTotal { get; set; }
        public long IVA { get; set; }
        public decimal TasaIVA { get; set; }
        public string ERPSynchronizationGlosa { get; set; }
        public long MntExe { get; set; }
        public long DescuentoMonto { get; set; }
        public decimal DescuentoPct { get; set; }
        public long MntBruto { get; set; }
        public string RznSocRecep { get; set; }
        public string RUTRecep { get; set; }
        public string RznSoc { get; set; }
        public string RUTEmisor { get; set; }
        public int FmaPago { get; set; }
        public string FchEmis { get; set; }
        public string Folio { get; set; }
        public string TipoDTE { get; set; }
        public long MntNeto { get; set; }
        public int? ERPSynchronizationRepeatCount { get; set; }
    }
}
