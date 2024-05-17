
namespace QRUtility.Model
{
    public class QRWriterModel
    {
        public string QRDetails { get; set; }
        public string QRName {  get; set; } 
        public string QRImageFullPath {  get; set; }
        public QRWriterModel(
            string qRDetails, 
            string qRName,
            string qRImageFullPath) 
        { 
            this.QRDetails = qRDetails;
            this.QRName = qRName;
            this.QRImageFullPath = qRImageFullPath;
        }
    }
}
