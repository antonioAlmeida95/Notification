using Notification.Adapter.Driven.Database;
using Notification.Adapter.Driving.Service;
using Notification.Adapter.Driving.Service.Settings;
using Notification.Application;
using Notification.Driven.Integrations.Email;

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(configuration);
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
        logging.AddEventSourceLogger();
    })
    .ConfigureServices((ctx, services) =>
    {
        services.Configure<QueueSettings>(opt =>
            ctx.Configuration.GetSection("Apis:RabbitMq:NotificationQueue").Bind(opt));
        services.Configure<CredentialsServer>(opt =>
            ctx.Configuration.GetSection("Credentials:Apis:RabbitMqServer").Bind(opt));
        
        services.AddHostedService<Worker>();
        
        //Adapters
        services.AddNotificacaoDatabaseModule(ctx.Configuration);
        services.AddNotificationEmailModule(ctx.Configuration);
        
        //Core
        services.AddNotificationDependencyModule();

    }).Build();
    
host.Run();