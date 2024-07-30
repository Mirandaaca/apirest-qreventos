namespace QR___Evento.Interfaces.Services
{
    public interface IFrom64BaseStringToImage
    {
        public MemoryStream ConvertToImage(string base64String);
    }
}
