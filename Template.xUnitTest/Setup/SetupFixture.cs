using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Template.Application.Logic;
using Template.Application.Models;
using Template.Domain.AppSettings;
using Template.Infrastructure.Context;
using Template.Infrastructure.Generics;
using Xunit;
using Xunit.Abstractions;

namespace Template.xUnitTest.Setup
{
    [CollectionDefinition("SetupCollection")]
    public class SetupCollection : ICollectionFixture<SetupFixture> { }

    public class SetupFixture
    {
        public SetupFixture()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
            IConfiguration config = builder.Build();
            var configSettings = new ConfigurationSettings();
            config.GetSection(ConfigurationSettings.Configuration).Bind(configSettings);
            Configuration = Options.Create(configSettings);
            var mapConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            Mapper = mapConfig.CreateMapper();
            Connection = new SqliteConnection(Configuration.Value.ConnectionStrings?.SqlDB);
            Connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(Connection).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();
            UnitOfWork unitOfWork = new UnitOfWork(Configuration, Mapper, dbContext);
            UnitLogic = new UnitLogic(Mapper, unitOfWork);
        }

        public IMapper Mapper { get; }

        public IUnitLogic UnitLogic { get; }

        public IOptions<ConfigurationSettings> Configuration { get; set; }

        public SqliteConnection Connection { get; set; }

        public void Dispose()
        {
            Connection.Close();
        }
    }

    public class BaseTest
    {
        public readonly ITestOutputHelper _outputHelper;
        public readonly SetupFixture _setupfixture;

        public BaseTest(ITestOutputHelper outputHelper, SetupFixture setupfixture)
        {
            _outputHelper = outputHelper;
            _setupfixture = setupfixture;
        }
    }
}
