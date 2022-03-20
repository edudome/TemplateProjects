using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.WinForm.App
{
    public static class Generador
    {
        private static string AppName = "Template";
        private static string PathApi = @"D:\Edu\Empresa\Proyecto Base\Proyectos VS\TemplateProjects\Example.Template.API";
        private static string PathApplication = @"D:\Edu\Empresa\Proyecto Base\Proyectos VS\TemplateProjects\ApplicationLayers\Template.Application";
        private static string PathDomain = @"D:\Edu\Empresa\Proyecto Base\Proyectos VS\TemplateProjects\ApplicationLayers\Template.Domain";
        private static string PathInfrastructure = @"D:\Edu\Empresa\Proyecto Base\Proyectos VS\TemplateProjects\ApplicationLayers\Template.Infrastructure";
        private static string PathTemplates = @"D:\Edu\Empresa\Proyecto Base\Proyectos VS\TemplateProjects\Template.WinForm.App\bin\Debug\net6.0-windows\Templates";

        public static void Crear(GeneradorModel model)
        {
            //if (!string.IsNullOrEmpty(model.Entity))
            //{
            //    CrearEntidad(model.Entity, model.CommandPropiedades);
            //}
            //if (!string.IsNullOrEmpty(model.EntityResult))
            //{
            //    CrearEntityResult(model.EntityResult, model.CommandPropiedades);
            //}
            //if (!string.IsNullOrEmpty(model.Modelo))
            //{
            //    CrearModelo(model.Modelo, model.CommandPropiedades);
            //}
            //if (!string.IsNullOrEmpty(model.LogicName))
            //{
            //    CrearLogic(model.LogicName, model.LogicMetodo, model.LogicResult, model.CommandName);
            //}
            if (!string.IsNullOrEmpty(model.ControllerName))
            {
                CrearController(model.ControllerName, model.ControllerVerbo, model.ControllerMetodo, model.LogicResult, model.CommandName);
            }
            //if (!string.IsNullOrEmpty(model.CommandName))
            //{
            //    CrearHandler(model.CommandName, model.CommandPropiedades, model.LogicName, model.LogicMetodo, model.LogicParametros, model.LogicResult, model.ControllerName);
            //}
        }

        private static void CrearController(string controllerName, string controllerVerbo, string controllerMetodo, string logicResult, string commandName)
        {

        }

        private static void CrearHandler(string commandName, string commandPropiedades, string logicName, string logicMetodo, string logicParametros, string logicResult, string controllerName)
        {
            string handlerTemplate = File.ReadAllText(PathTemplates + @"\Handler.txt");
            if (!string.IsNullOrEmpty(commandName))
            {
                string pathHandler = Path.Combine(PathApplication, "Handlers", controllerName + "s");
                string pathHandlerFile = Path.Combine(pathHandler, commandName + ".cs");
                if (!Directory.Exists(pathHandler))
                {
                    Directory.CreateDirectory(pathHandler);
                }
                if (!File.Exists(pathHandlerFile))
                {
                    handlerTemplate = handlerTemplate.Replace("###ControllerName_Template###", controllerName);
                    handlerTemplate = handlerTemplate.Replace("###CommandName_Template###", commandName);
                    handlerTemplate = handlerTemplate.Replace("###LogicName_Template###", logicName);
                    handlerTemplate = handlerTemplate.Replace("###LogicMetodo_Template###", logicMetodo);
                    handlerTemplate = handlerTemplate.Replace("###LogicResult_Template###", logicResult);
                    handlerTemplate = handlerTemplate.Replace("###CommandPropiedades_Template###", commandPropiedades);

                    string constructor = string.Empty;
                    string propiedadesResult = string.Empty;

                    if (!string.IsNullOrEmpty(commandPropiedades))
                    {
                        string propTemplate = @"public tipo nombre { get; set; }";
                        string[] splitPropiedades = commandPropiedades.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string prop in splitPropiedades)
                        {
                            string[] splitProp = prop.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            string tipo = splitProp[0].ToLower();
                            string nombre = splitProp[1].ToLower();
                            nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1);
                            constructor += "\t\t\t" + nombre + " = " + splitProp[1] + ";" + Environment.NewLine;
                            string propInsert = propTemplate.Replace("tipo", tipo).Replace("nombre", nombre);
                            propiedadesResult += "\t\t" + propInsert;
                            propiedadesResult += Environment.NewLine;
                        }

                        if (constructor.EndsWith("\r\n")) constructor = constructor.Remove(constructor.Length - 2);
                    }

                    handlerTemplate = handlerTemplate.Replace("###Constructor_Template###", constructor);
                    handlerTemplate = handlerTemplate.Replace("###Propiedades_Template###", propiedadesResult);

                    File.WriteAllText(pathHandlerFile, handlerTemplate);
                }
            }
        }

        private static void CrearLogic(string logicName, string logicMetodo, string logicResult, string commandName)
        {
            string logicTemplate = File.ReadAllText(PathTemplates + @"\Logic.txt");
            string logic = logicName.Replace("Logic", "");
            if (!string.IsNullOrEmpty(logicName))
            {
                string pathLogic = Path.Combine(PathApplication, "Logic", logic + "s");
                string pathLogicFile = Path.Combine(pathLogic, logicName + ".cs");
                if (!Directory.Exists(pathLogic))
                {
                    Directory.CreateDirectory(pathLogic);
                }
                if (!File.Exists(pathLogicFile))
                {
                    logicTemplate = logicTemplate.Replace("###LogicName_Template###", logic);
                    logicTemplate = logicTemplate.Replace("###LogicMetodo_Template###", logicMetodo);
                    logicTemplate = logicTemplate.Replace("###LogicResult_Template###", logicResult);
                    logicTemplate = logicTemplate.Replace("###CommandName_Template###", commandName);
                    File.WriteAllText(pathLogicFile, logicTemplate);

                    // UnitLogic //
                    string pathUnitLogic = Path.Combine(PathApplication, "Logic", "UnitLogic.cs");

                    if (File.Exists(pathUnitLogic))
                    {
                        string textUnitLogic = File.ReadAllText(pathUnitLogic);
                        string posText = "public class UnitLogic";
                        int posTextLength = posText.Length;
                        string usingLogic = "using " + AppName + ".Application.Logic." + logic + "s;";
                        string nameSpace = "namespace " + AppName + ".";
                        int posNameSpace = textUnitLogic.IndexOf(nameSpace);
                        textUnitLogic = textUnitLogic.Insert(posNameSpace - 2, usingLogic + Environment.NewLine);

                        int pos = textUnitLogic.IndexOf(posText);
                        if (pos != -1)
                        {
                            textUnitLogic = textUnitLogic.Insert(pos - 12, $"\n\t\t{logic}Logic {logic}Logic {{ get; }}\n\n");
                        }

                        pos = textUnitLogic.IndexOf("public UnitLogic(");
                        string privateVariable = $"private {logic}Logic? _{logic.ToLower()}Logic;";
                        if (pos != -1)
                        {
                            textUnitLogic = textUnitLogic.Insert(pos - 9, "\t\t" + privateVariable + Environment.NewLine);
                        }

                        pos = textUnitLogic.LastIndexOf("}");
                        if (pos != -1)
                        {
                            textUnitLogic = textUnitLogic.Insert(pos - 7, $"\t\tpublic {logic}Logic {logic}Logic => _{logic.ToLower()}Logic ??= new {logic}Logic(_mapper, _unitOfWork);\n");
                        }

                        File.WriteAllText(pathUnitLogic, textUnitLogic);
                    }
                }
            }
        }
        private static void CrearEntidad(string entity, string propiedades)
        {
            string modelTemplate = File.ReadAllText(PathTemplates + @"\Entity.txt");
            string nombreEntity = entity;
            if (!string.IsNullOrEmpty(nombreEntity))
            {
                string pathEntity = Path.Combine(PathDomain, "Entities");
                string pathEntityFile = Path.Combine(pathEntity, entity + ".cs");
                if (!Directory.Exists(pathEntity))
                {
                    Directory.CreateDirectory(pathEntity);
                }
                if (!File.Exists(pathEntityFile))
                {
                    string propiedadesResult = string.Empty;
                    if (!string.IsNullOrEmpty(propiedades))
                    {
                        string propTemplate = @"public tipo nombre { get; set; }";
                        string[] splitPropiedades = propiedades.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string prop in splitPropiedades)
                        {
                            string[] splitProp = prop.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            string tipo = splitProp[0].ToLower();
                            string nombre = splitProp[1].ToLower();
                            nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1);
                            string propInsert = propTemplate.Replace("tipo", tipo).Replace("nombre", nombre);
                            if (!propInsert.Contains('?')) propInsert += " = default!;";
                            propiedadesResult += "\t\t" + propInsert;
                            propiedadesResult += Environment.NewLine;
                        }
                    }
                    modelTemplate = modelTemplate.Replace("###Entity_Template###", nombreEntity);
                    modelTemplate = modelTemplate.Replace("###Propiedades_Template###", propiedadesResult);
                    File.WriteAllText(pathEntityFile, modelTemplate);
                }

                string pathRepository = Path.Combine(PathInfrastructure, "Repository", nombreEntity + "Repository.cs");
                string pathIRepository = Path.Combine(PathDomain, "InterfacesRepository", "I" + nombreEntity + "Repository.cs");
                if (!File.Exists(pathRepository))
                {
                    string repositoryFile = Path.Combine(PathTemplates + @"\Repository.txt");
                    string iRepositoryFile = Path.Combine(PathTemplates + @"\IRepository.txt");
                    string repositoryTemplate = File.ReadAllText(repositoryFile);
                    repositoryTemplate = repositoryTemplate.Replace("###Entity_Template###", nombreEntity);
                    File.WriteAllText(pathRepository, repositoryTemplate);
                    repositoryTemplate = File.ReadAllText(iRepositoryFile);
                    repositoryTemplate = repositoryTemplate.Replace("###Entity_Template###", nombreEntity);
                    File.WriteAllText(pathIRepository, repositoryTemplate);
                }
            }
        }

        private static void CrearEntityResult(string entityResult, string propiedades)
        {
            string modelTemplate = File.ReadAllText(PathTemplates + @"\EntityResult.txt");
            string nombreEntityResult = entityResult.Replace("EntityResult", "");
            if (!string.IsNullOrEmpty(nombreEntityResult))
            {
                string pathEntityResult = Path.Combine(PathDomain, "EntitiesResult");
                string pathEntityResultFile = Path.Combine(pathEntityResult, entityResult + ".cs");
                if (!Directory.Exists(pathEntityResult))
                {
                    Directory.CreateDirectory(pathEntityResult);
                }
                if (!File.Exists(pathEntityResultFile))
                {
                    string propiedadesResult = string.Empty;
                    if (!string.IsNullOrEmpty(propiedades))
                    {
                        string propTemplate = @"public tipo nombre { get; set; }";
                        string[] splitPropiedades = propiedades.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string prop in splitPropiedades)
                        {
                            string[] splitProp = prop.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            string tipo = splitProp[0].ToLower();
                            string nombre = splitProp[1].ToLower();
                            nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1);
                            string propInsert = propTemplate.Replace("tipo", tipo).Replace("nombre", nombre);
                            if (!propInsert.Contains('?')) propInsert += " = default!;";
                            propiedadesResult += "\t\t" + propInsert;
                            propiedadesResult += Environment.NewLine;
                        }
                    }
                    modelTemplate = modelTemplate.Replace("###EntityResult_Template###", nombreEntityResult);
                    modelTemplate = modelTemplate.Replace("###Propiedades_Template###", propiedadesResult);
                    File.WriteAllText(pathEntityResultFile, modelTemplate);

                    // Mapping Profile //

                    string pathMappingProfile = Path.Combine(PathApplication, "Models", "MappingProfile.cs");
                    string pathEntityFile = Path.Combine(PathDomain, "Entities", nombreEntityResult + ".cs");

                    if (File.Exists(pathMappingProfile))
                    {
                        string textMappingProfile = File.ReadAllText(pathMappingProfile);

                        if (File.Exists(pathEntityFile))
                        {
                            string posText = ".ReverseMap();";
                            int postTextLength = posText.Length;

                            int pos = textMappingProfile.LastIndexOf(posText);
                            if (pos != -1)
                            {
                                textMappingProfile = textMappingProfile.Insert(pos + postTextLength, $"\n\n\n\t\t\tCreateMap<{nombreEntityResult}, {entityResult}>()\n\t\t\t\t.ReverseMap();\n");
                            }
                        }

                        File.WriteAllText(pathMappingProfile, textMappingProfile);
                    }
                }
            }
        }

        public static void CrearModelo(string modelo, string propiedades)
        {
            string modelTemplate = File.ReadAllText(PathTemplates + @"\Model.txt");
            string nombreModelo = modelo.Replace("Model", "");
            if (!string.IsNullOrEmpty(nombreModelo))
            {
                string pathModelo = Path.Combine(PathApplication, "Models", nombreModelo + "s");
                string pathModeloFile = Path.Combine(pathModelo, modelo + ".cs");
                if (!Directory.Exists(pathModelo))
                {
                    Directory.CreateDirectory(pathModelo);
                }
                if (!File.Exists(pathModeloFile))
                {
                    string propiedadesResult = string.Empty;
                    if (!string.IsNullOrEmpty(propiedades))
                    {
                        string propTemplate = @"public tipo nombre { get; set; }";
                        string[] splitPropiedades = propiedades.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string prop in splitPropiedades)
                        {
                            string[] splitProp = prop.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            string tipo = splitProp[0].ToLower();
                            string nombre = splitProp[1].ToLower();
                            nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1);
                            string propInsert = propTemplate.Replace("tipo", tipo).Replace("nombre", nombre);
                            if (!propInsert.Contains('?')) propInsert += " = default!;";
                            propiedadesResult += "\t\t" + propInsert;
                            propiedadesResult += Environment.NewLine;
                        }
                    }
                    modelTemplate = modelTemplate.Replace("###Model_Template###", nombreModelo);
                    modelTemplate = modelTemplate.Replace("###Propiedades_Template###", propiedadesResult);
                    File.WriteAllText(pathModeloFile, modelTemplate);

                    // Mapping Profile //

                    string pathMappingProfile = Path.Combine(PathApplication, "Models", "MappingProfile.cs");
                    string pathEntityFile = Path.Combine(PathDomain, "Entities", nombreModelo + ".cs");
                    string pathEntityResultFile = Path.Combine(PathDomain, "EntitiesResult", nombreModelo + "EntityResult.cs");

                    if (File.Exists(pathMappingProfile))
                    {
                        string textMappingProfile = File.ReadAllText(pathMappingProfile);
                        string posText = ".ReverseMap();";
                        int posTextLength = posText.Length;
                        string usingModel = "using " + AppName + ".Application.Models." + nombreModelo + "s;";
                        string nameSpace = "namespace " + AppName + ".";

                        int posNameSpace = textMappingProfile.IndexOf(nameSpace);
                        textMappingProfile = textMappingProfile.Insert(posNameSpace - 2, usingModel + Environment.NewLine);

                        if (File.Exists(pathEntityFile))
                        {
                            int pos = textMappingProfile.LastIndexOf(posText);
                            if (pos != -1)
                            {
                                textMappingProfile = textMappingProfile.Insert(pos + posTextLength, $"\n\n\n\t\t\tCreateMap<{nombreModelo}, {modelo}>()\n\t\t\t\t.ReverseMap();\n");
                            }
                        }
                        if (File.Exists(pathEntityResultFile))
                        {
                            int pos = textMappingProfile.LastIndexOf(posText);
                            if (pos != -1)
                            {
                                textMappingProfile = textMappingProfile.Insert(pos + posTextLength, $"\n\n\n\t\t\tCreateMap<{nombreModelo + "EntityResult"}, {modelo}>()\n\t\t\t\t.ReverseMap();\n");
                            }
                        }

                        File.WriteAllText(pathMappingProfile, textMappingProfile);
                    }
                }
            }
        }
    }
}
