﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestionBussiness.Services;
using SistemaGestionData;

namespace SistemaGestionBussiness;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureBussinessLayer(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.ConfigureDataLayer(configuration);
        services.AddScoped<ProductosService>();
        services.AddScoped<UsuariosService>();
        services.AddScoped<VentasService>();
        services.AddScoped<ProductosVendidosService>();
        services.AddScoped<LoginService>();
        services.AddScoped<TokenService>();

        return services;
    }
}