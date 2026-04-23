using System;
using System.Reflection;
using DotMake.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MProjector.Abstractions.Graphics;
using MProjector.Abstractions.Projections;
using MProjector.CLI.Commands;
using MProjector.Graphics;
using MProjector.Logic.Projections;
using NLog.Extensions.Logging;

Cli.Ext.ConfigureServices(services =>
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddUserSecrets(Assembly.GetExecutingAssembly()).Build();
    
    services.AddSingleton<IConfiguration>(config);
    services.AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
        builder.AddNLog();
    });
    
    services.AddTransient<IBitmap, Bitmap>();
    services.AddScoped<ILambertProjection, LambertProjection>();
});

Cli.Run<RootCliCommand>();


