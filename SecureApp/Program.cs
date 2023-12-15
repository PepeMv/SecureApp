using VulApp;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }


    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host
        .CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((x, y) =>
        {
            y
            .SetBasePath(Directory.GetCurrentDirectory())
            //.SetBasePath(x.HostingEnvironment.ContentRootPath)
            //.addconsukl
            .AddJsonFile("appsettings.json", true, true);
        })
        .ConfigureWebHostDefaults(config =>
        {
            config
            .UseStartup<Startup>();
        });
}


