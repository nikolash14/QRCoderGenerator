using QRCoder;
using QRUtility.Contract;
using System.Drawing.Imaging;
using System.Drawing;

namespace QRUtility.Helpers
{
    public class QRHelper : IQRHelper
    {
        public void CreateQRCode(
            string qrData,
            string qRImageFullPath,
            int pixelsPerModule,
            ImageFormat imageFormat,
            QRCodeGenerator.ECCLevel eCCLevel = QRCodeGenerator.ECCLevel.Q)
        {
            Bitmap qrCodeImage = null;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, eCCLevel);
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                qrCodeImage = qrCode.GetGraphic(pixelsPerModule);
                qrCodeImage.Save(qRImageFullPath, imageFormat);
            }
        }
    }
}