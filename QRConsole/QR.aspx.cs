using Newtonsoft.Json;
using QRCoder;
using QRUtility.Contract;
using QRUtility.Helpers;
using QRUtility.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QRConsole
{
    public partial class QR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonQR_Click(object sender, EventArgs e)
        {
            try
            {
                int pixelsPerModule = 20;
                string qrFileFullPath = "";
                string fileName = @"D:\QrDocument.docx";
                string qrFilePath = Server.MapPath("~/QRCode");
                qrFilePath += "\\Images\\";
                //string zipFileName = @"D:\QRFolder.zip";
                List<QRGenerationModel> data = GenerateDummyData();
                List<QRWriterModel> domain = new List<QRWriterModel>();
                foreach (QRGenerationModel model in data)
                {
                    string message = JsonConvert.SerializeObject(model);
                    IQRHelper _qRHelper = new QRHelper();
                    qrFileFullPath = qrFilePath + model.SSCC + ".jpeg";
                    _qRHelper.CreateQRCode(
                            message,
                            qrFileFullPath,
                            pixelsPerModule,
                            ImageFormat.Jpeg,
                            QRCodeGenerator.ECCLevel.Q);
                    domain.Add(new QRWriterModel(message, model.SSCC, qrFileFullPath));
                }
                IDocumentHelper _documentHelper = new DocumentHelper();
                _documentHelper.CreateQRCodeDocument(domain, fileName, "QR for PO#", 177, 177);
            }
            catch(Exception ex) { }
        }
        private List<QRGenerationModel> GenerateDummyData()
        {
            List<QRGenerationModel> qRGenerationModels = new List<QRGenerationModel>();
            int count = 100;

            while (count != 120)
            {
                var qRGenerationModel = new QRGenerationModel(
                        "PONumber" + count,
                        "SONumber" + count,
                        "SKUNumber" + count,
                        "SSCCNumberForCartonns" + count,
                        count,
                        "DescriptionInFewWords" + count);
                qRGenerationModels.Add(qRGenerationModel);
                ++count;
            }
            return qRGenerationModels;
        }
    }
}