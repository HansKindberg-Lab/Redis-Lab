using System.ComponentModel;
using System.Net;
using Application.Models.ComponentModel;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using StackExchange.Redis;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information($"Application starting at {DateTime.Now:o} ...");

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.Host.UseSerilog((hostBuilderContext, serviceProvider, loggerConfiguration) =>
	{
		loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
		loggerConfiguration.ReadFrom.Services(serviceProvider);
	});

	TypeDescriptor.AddAttributes(typeof(IPAddress), new TypeConverterAttribute(typeof(IpAddressTypeConverter)));
	TypeDescriptor.AddAttributes(typeof(IPNetwork), new TypeConverterAttribute(typeof(IpNetworkTypeConverter)));

	builder.Services.Configure<ForwardedHeadersOptions>(options =>
	{
		options.AllowedHosts.Clear();
		options.KnownNetworks.Clear();
		options.KnownProxies.Clear();
	});

	var forwardedHeadersSection = builder.Configuration.GetSection("ForwardedHeaders");
	builder.Services.Configure<ForwardedHeadersOptions>(forwardedHeadersSection);

	builder.Services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
	{
		var redisConnectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("Redis");
		return ConnectionMultiplexer.Connect(redisConnectionString);
	});

	builder.Services.AddControllersWithViews();

	var application = builder.Build();

	application
		.UseDeveloperExceptionPage()
		.UseForwardedHeaders()
		.UseStaticFiles()
		.UseRouting()
		.UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapDefaultControllerRoute());

	application.Run();
}
catch(Exception exception)
{
	Log.Fatal(exception, "Unhandled exception during application startup.");
}
finally
{
	Log.Information($"Application stopping at {DateTime.Now:o} ...");
	Log.CloseAndFlush();
}