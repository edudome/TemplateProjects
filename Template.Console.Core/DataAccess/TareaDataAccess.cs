using MongoDB.Driver;
using Template.Console.Core.Models;

namespace Template.Console.Core.DataAccess
{
    public class TareaDataAccess
    {
        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        private const string DatabaseName = "TemplateDB";
        private const string TareasCollection = "Tareas_board";
        private const string TareasHistoryCollection = "Tareas_history";
        private const string EmpleadosCollection = "Empleados";


        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }

        public Task CreateEmpleado(EmpleadoModel Empleado)
        {
            var EmpleadosCollection = ConnectToMongo<EmpleadoModel>(TareaDataAccess.EmpleadosCollection);
            return EmpleadosCollection.InsertOneAsync(Empleado);
        }

        public async Task<List<EmpleadoModel>> GetAllEmpleados()
        {
            var EmpleadosCollection = ConnectToMongo<EmpleadoModel>(TareaDataAccess.EmpleadosCollection);
            var results = await EmpleadosCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<List<TareaModel>> GetAllTareas()
        {
            var TareasCollection = ConnectToMongo<TareaModel>(TareaDataAccess.TareasCollection);
            var results = await TareasCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<List<TareaModel>> GetAllTareasForAEmpleado(EmpleadoModel Empleado)
        {
            var TareasCollection = ConnectToMongo<TareaModel>(TareaDataAccess.TareasCollection);
            var results = await TareasCollection.FindAsync(c => c.AssignedTo != null && c.AssignedTo.Id == Empleado.Id);
            return results.ToList();
        }

        public Task CreateTarea(TareaModel Tarea)
        {
            var TareasCollection = ConnectToMongo<TareaModel>(TareaDataAccess.TareasCollection);
            return TareasCollection.InsertOneAsync(Tarea);
        }

        public Task UpdateTarea(TareaModel Tarea)
        {
            var TareasCollection = ConnectToMongo<TareaModel>(TareaDataAccess.TareasCollection);
            var filter = Builders<TareaModel>.Filter.Eq("Id", Tarea.Id);
            return TareasCollection.ReplaceOneAsync(filter, Tarea, new ReplaceOptions { IsUpsert = true });
        }

        public Task DeleteTarea(TareaModel Tarea)
        {
            var TareasCollection = ConnectToMongo<TareaModel>(TareaDataAccess.TareasCollection);
            return TareasCollection.DeleteOneAsync(c => c.Id == Tarea.Id);
        }

        public async Task CompleteTarea(TareaModel Tarea)
        {
            try
            {
                var client = new MongoClient(ConnectionString);
                var db = client.GetDatabase(DatabaseName);
                var TareasCollection = db.GetCollection<TareaModel>(TareaDataAccess.TareasCollection);
                var filter = Builders<TareaModel>.Filter.Eq("Id", Tarea.Id);
                await TareasCollection.ReplaceOneAsync(filter, Tarea);

                var TareaHistoryCollection = db.GetCollection<TareaHistoryModel>(TareasHistoryCollection);
                await TareaHistoryCollection.InsertOneAsync(new TareaHistoryModel(Tarea));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
