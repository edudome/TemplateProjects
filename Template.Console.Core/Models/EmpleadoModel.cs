using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Template.Console.Core.Models;

public class EmpleadoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string NombreCompleto => $"{Nombre} {Apellido}";
}
