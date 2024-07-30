using QR___Evento.Interfaces.Services;
using QRCoder;
namespace QR___Evento.Services
{
    public class QRGeneratorService : IQRGenerator
    {
        public string GenerarQR(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            string model = Convert.ToBase64String(qrCodeImage);
            return model;
        }
    }
}
