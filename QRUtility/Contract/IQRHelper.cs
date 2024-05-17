using QRCoder;
using System.Drawing.Imaging;

namespace QRUtility.Contract
{
    public interface IQRHelper
    {
        /// <summary>
        /// Create QR Code on the path provided, The Image format should be same as the file type
        /// </summary>
        /// <param name="qRData"></param>
        /// <param name="qRImageFullPath"></param>
        /// <param name="pixelPerModule"></param>
        /// <param name="imageFormat"></param>
        /// <param name="eCCLevel"></param>
        void CreateQRCode(
            string qRData,
            string qRImageFullPath,
            int pixelPerModule,
            ImageFormat imageFormat,
            QRCodeGenerator.ECCLevel eCCLevel = QRCodeGenerator.ECCLevel.Q
            );
    }
}
