
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using SchoolAPI.Models;

namespace SchoolAPI
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<SchoolDatabaseSettings>(builder.Configuration.GetSection("SchoolDatabaseSettings"));
            builder.Services.AddSingleton<IMongoClient>(_ => {
                /*
                 
                 * This allows for more precise control and customization as we set up our database connection
                 * var settings = new MongoClientSettings()
                 * {
                 *      Scheme = ConnectionStringScheme.MongoDBPlusSrv,
                 *      Server = new MongoServerAddress("localhost")
                 * };
                 * 
                 */

                var connectionString = builder.Configuration.GetSection("SchoolDatabaseSettings:ConnectionString").Value;
                return new MongoClient(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
