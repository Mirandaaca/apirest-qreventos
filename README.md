# API Rest para un evento de la facultad de administracion de mi universidad
## API Rest desarrollada con ASP.NET Core Web Api 8.0 utilizando .NET 8.0
### Objetivo: Llevar un registro de las personas registradas al evento, y al momento de ingresar o recibir su plato de comida en dicho evento, verificar mediante scaneo de su codigo QR. 
### Instrucciones
- Para hacer uso del envío de emails, actualmente se utiliza el host smtp.gmail.com y se debera de agregar un email y una contrasenia de aplicacion (revisar en tu cuenta de google las contrasenias para aplicaciones, sino crear una), asi como tambien definir ssl en true y el puerto por defecto.
- Actualmente no se hace uso de mucha seguridad, solo se hace una validacion muy simple en el frontend. Sin embargo las contrenias estan cifradas en la bd y al obtener la respuesta del api.
- Se aceptan recomendaciones, criticas u opiniones :).
