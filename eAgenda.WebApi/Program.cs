using eAgenda.WebApi.Config;

namespace eAgenda.WebApi
{
    public class Program
    {
        static string nomeCors = "Desenvolvimento";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigurarValidacao();
            builder.Services.ConfigurarIdentity();
            builder.Services.ConfigurarSerilog(builder.Logging);
            builder.Services.ConfigurarAutoMapper();
            builder.Services.ConfigurarInjecaoDependencia(builder.Configuration);
            builder.Services.ConfigurarSwagger();
            builder.Services.ConfigurarControllers();
            builder.Services.ConfigurarJwt();
            builder.Services.ConfigurarCors(nomeCors);

            var app = builder.Build();

            app.UseMiddleware<ManipuladorExcecoes>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(nomeCors);

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}