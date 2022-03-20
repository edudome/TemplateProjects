using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Template.Console.Core.Models;

public class TareaHistoryModel
{
    public TareaHistoryModel()
    {

    }
    public TareaHistoryModel(TareaModel chore)
    {
        ChoreId = chore.Id;
        DateCompleted = chore.LastCompleted ?? DateTime.Now;
        WhoCompleted = chore.AssignedTo;
        ChoreText = chore.Descripcion;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? ChoreId { get; set; }
    public string? ChoreText { get; set; }
    public DateTime DateCompleted { get; set; }
    public EmpleadoModel? WhoCompleted { get; set; }
}
