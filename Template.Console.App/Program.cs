
using MongoDB.Driver;
using Template.Console.Core.DataAccess;
using Template.Console.Core.Models;

TareaDataAccess db = new TareaDataAccess();

Console.WriteLine("Base de datos instanciada.");

await db.CreateEmpleado(new EmpleadoModel() { Nombre = "Eduardo", Apellido = "Domenech" });

Console.WriteLine("Usuario nuevo creado.");

var users = await db.GetAllEmpleados();

Console.WriteLine("Obteniendo todos los empleados:");

foreach (var user in users)
{
    Console.WriteLine($"Nombre: {user.Nombre}");
    Console.WriteLine($"Apellido: {user.Apellido}");
    Console.WriteLine("-----");
}


Console.WriteLine("Asignando tarea al primer empleado encontrado.");

var Tarea = new TareaModel()
{
    AssignedTo = users.First(),
    Descripcion = "Tarea X",
    FrequencyInDays = 7
};

await db.CreateTarea(Tarea);

Console.WriteLine("Tarea creada.");

Console.WriteLine("Obteniendo todas las tareas:");

var tareas = await db.GetAllTareas();

foreach (var tarea in tareas)
{
    Console.WriteLine($"Descripcion: {tarea.Descripcion}");
    Console.WriteLine($"Asignada a: {tarea.AssignedTo?.NombreCompleto}");
    Console.WriteLine("-----");
}

Console.WriteLine("Finalizando primer tarea:");

var newTarea = tareas.First();

newTarea.LastCompleted = DateTime.UtcNow;
await db.CompleteTarea(newTarea);

Console.WriteLine($"Tarea '{newTarea.Descripcion}' finalizada.");


Console.ReadKey();
