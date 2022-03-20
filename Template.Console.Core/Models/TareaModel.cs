using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Template.Console.Core.Models;

public class TareaModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Descripcion { get; set; }
    public int FrequencyInDays { get; set; }
    public EmpleadoModel? AssignedTo { get; set; }
    public DateTime? LastCompleted { get; set; }
}
