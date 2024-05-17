namespace QRUtility.Model
{
    public class QRGenerationModel
    {
        public string PONumber { get; set; }
        public string SONumber { get; set; }
        public string SKU { get; set; }
        public string SSCC { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public QRGenerationModel(
            string pONumber,
            string sONumber,
            string sKU,
            string sSCC,
            int quantity,
            string description
            )
        {
            this.PONumber = pONumber;
            this.SONumber = sONumber;
            this.SKU = sKU;
            this.SSCC = sSCC;
            this.Quantity = quantity;
            this.Description = description;
        }
    }
}
