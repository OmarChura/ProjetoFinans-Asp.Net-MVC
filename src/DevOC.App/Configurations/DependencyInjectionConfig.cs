using DevOC.App.Extensions;
using DevOC.App.Helper;
using DevOC.Business.Interfaces;
using DevOC.Business.Notificacoes;
using DevOC.Business.Services;
using DevOC.Data.Context;
using DevOC.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DevOC.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IOrcamentoPessoalRepository, OrcamentoPessoalRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISessao, Sessao>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IOrcamentoPessoalService, OrcamentoPessoalService>();


            return services;
        }
    }
}
