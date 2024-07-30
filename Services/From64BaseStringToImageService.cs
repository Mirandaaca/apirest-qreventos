using Azure.Core;
using QR___Evento.Interfaces.Services;

namespace QR___Evento.Services
{
    public class From64BaseStringToImageService : IFrom64BaseStringToImage
    {
        public MemoryStream ConvertToImage(string base64String)
        {
            byte[] qrImageBytes = Convert.FromBase64String(base64String);
            MemoryStream qrStream = new MemoryStream(qrImageBytes);
            return qrStream;
        }
    }
}
