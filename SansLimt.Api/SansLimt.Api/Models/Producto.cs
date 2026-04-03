using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace SansLimt.Api.Models
{
    // Permite que la api no falle si faltan atributos en mongo
    [BsonIgnoreExtraElements]
    public class Producto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; } = null!;

        [BsonElement("slug")]
        public string Slug { get; set; } = null!;

        [BsonElement("categoria")]
        public string Categoria { get; set; } = null!;

        [BsonElement("precio")]
        public int Precio { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; } = null!;

        [BsonElement("imagenes")]
        public List<string> Imagenes { get; set; } = new();

        [BsonElement("active")]
        public bool Active { get; set; }

        
        [BsonElement("mililitros")]
        public int? Mililitros { get; set; } 

        
        [BsonElement("variantes")]
        public List<Variante>? Variantes { get; set; }
    }

   
    [BsonIgnoreExtraElements]
    public class Variante
    {
        [BsonElement("color")]
        public string Color { get; set; } = null!;

        [BsonElement("talle")]
        public string Talle { get; set; } = null!;

        [BsonElement("stock")]
        public int Stock { get; set; }

        [BsonElement("sku")]
        public string Sku { get; set; } = null!;
    }
}