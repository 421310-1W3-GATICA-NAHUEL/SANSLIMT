using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SansLimt.Api.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        
        public string Rol { get; set; } = "Cliente";

      
        public string? Email { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Telefono { get; set; }
        public DireccionEnvio? Direccion { get; set; }
    }

    public class DireccionEnvio
    {
        public string? Calle { get; set; }
        public string? Ciudad { get; set; }
        public string? CodigoPostal { get; set; }
    }
}