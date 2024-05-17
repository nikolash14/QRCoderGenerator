using QRUtility.Model;
using System.Collections.Generic;

namespace QRUtility.Contract
{
    public interface IDocumentHelper
    {
        void CreateQRCodeDocument(
            List<QRWriterModel> qRWriterModels,
            string qRWordFileFullPath,
            string qRFileHeaderText,
            int height = 177,
            int width = 177);
    }
}
