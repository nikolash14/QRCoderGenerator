using QRUtility.Contract;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using QRUtility.Model;

namespace QRUtility.Helpers
{
    public class DocumentHelper : IDocumentHelper
    {
        public void CreateQRCodeDocument(
            List<QRWriterModel> qRWriterModels, 
            string qRWordFileFullPath, 
            string qRFileHeaderText, 
            int height = 177, 
            int width = 177)
        {
            try
            {
                //Create object from file path write in the file
                using (var file = WordprocessingDocument.Create(
                    qRWordFileFullPath,
                    WordprocessingDocumentType.Document))
                {
                    file.AddMainDocumentPart();
                    Text text = new Text(qRFileHeaderText);
                    Run run = new Run(text);
                    Paragraph paragraph = new Paragraph(run);
                    Body body = new Body(paragraph);
                    Document document = new Document(body);

                    //Loop through all data and add QR image, data string to the document
                    foreach (var item in qRWriterModels)
                    {
                        //Add QR data
                        Text qrText = new Text(item.QRDetails);
                        body.AppendChild(new Paragraph(new Run(qrText)));
                        //Add QR Image
                        ImagePart imagePart = file.MainDocumentPart.AddImagePart(ImagePartType.Png);
                        using (FileStream stream = new FileStream(
                            item.QRImageFullPath,
                            FileMode.Open))
                        {
                            imagePart.FeedData(stream);
                        }
                        Drawing imageElement = GetImageElement(
                                                file.MainDocumentPart.GetIdOfPart(imagePart),
                                                item.QRImageFullPath,
                                                item.QRName,
                                                width,
                                                height);

                        body.AppendChild(new Paragraph(new Run(imageElement)));
                    }
                    //Save file and close document
                    file.MainDocumentPart.Document = document;
                    file.MainDocumentPart.Document.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private Drawing GetImageElement(
            string imagePartId,
            string fileName,
            string pictureName,
            double width,
            double height)
        {
            double englishMetricUnitsPerInch = 914400;
            double pixelsPerInch = 96;

            //calculate size in emu
            double emuWidth = width * englishMetricUnitsPerInch / pixelsPerInch;
            double emuHeight = height * englishMetricUnitsPerInch / pixelsPerInch;

            var element = new Drawing(
                new DW.Inline(
                    new DW.Extent { Cx = (Int64Value)emuWidth, Cy = (Int64Value)emuHeight },
                    new DW.EffectExtent { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L },
                    new DW.DocProperties { Id = (UInt32Value)1U, Name = pictureName },
                    new DW.NonVisualGraphicFrameDrawingProperties(
                    new A.GraphicFrameLocks { NoChangeAspect = true }),
                    new A.Graphic(
                        new A.GraphicData(
                            new PIC.Picture(
                                new PIC.NonVisualPictureProperties(
                                    new PIC.NonVisualDrawingProperties { Id = (UInt32Value)0U, Name = fileName },
                                    new PIC.NonVisualPictureDrawingProperties()),
                                new PIC.BlipFill(
                                    new A.Blip(
                                        new A.BlipExtensionList(
                                            new A.BlipExtension { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" }))
                                    {
                                        Embed = imagePartId,
                                        CompressionState = A.BlipCompressionValues.Print
                                    },
                                            new A.Stretch(new A.FillRectangle())),
                                new PIC.ShapeProperties(
                                    new A.Transform2D(
                                        new A.Offset { X = 0L, Y = 0L },
                                        new A.Extents { Cx = (Int64Value)emuWidth, Cy = (Int64Value)emuHeight }),
                                    new A.PresetGeometry(
                                        new A.AdjustValueList())
                                    { Preset = A.ShapeTypeValues.Rectangle })))
                        {
                            Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                        }))
                {
                    DistanceFromTop = 0U,
                    DistanceFromBottom = 0U,
                    DistanceFromLeft = 0U,
                    DistanceFromRight = 0U,
                    EditId = "50D07946"
                });
            return element;
        }
    }
}
