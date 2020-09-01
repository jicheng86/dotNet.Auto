using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Serilog.Sinks.SystemConsole.Themes;

namespace Auto.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //程序启动就开始记录日志
            #region serilog-Profile
            Log.Logger = new LoggerConfiguration()
                  // .Enrich.WithProperty("SourceContext", null) //加入属性SourceContext，也就运行时是调用Logger的具体类
                  .Enrich.FromLogContext() //动态加入属性，主要是针对上面的自定义字段User和Class，当然也可以随时加入别的属性。
                  .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                  .WriteTo.File(formatter: new CompactJsonFormatter(), path: "Serilogs/JsonFormatterlog.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug, retainedFileCountLimit: 365, encoding: Encoding.UTF8, shared: true, buffered: false)
                  .WriteTo.File(path: "Serilogs/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug, retainedFileCountLimit: 365, encoding: Encoding.UTF8, shared: true, buffered: false)
                  .AuditTo.File(path: "Serilogs/audit.txt", restrictedToMinimumLevel: LogEventLevel.Error)
                  .WriteTo.MSSqlServer(connectionString: @"Data Source=.;Database=DB_Auto;Integrated Security=SSPI;Persist Security Info=False;", sinkOptions: new SinkOptions { TableName = "Serilogs4Web", AutoCreateSqlTable = true }, restrictedToMinimumLevel: LogEventLevel.Warning)
                 //.WriteTo.Email(
                 //   fromEmail: "lijc@lx-car.com",
                 //   toEmail: "791457931@qq.com",
                 //   mailServer: "smtp.263.net",
                 //   mailSubject: "系统有错误，已写入日志，请查看！",
                 //   restrictedToMinimumLevel: LogEventLevel.Warning,
                 //   networkCredential: new NetworkCredential(userName: "lijc@lx-car.com", password: "zxc123111"))
                 //.WriteTo.Seq(serverUrl: "http://localhost:5341")
                 .CreateLogger();
            #endregion

            #region autofac-profile 反射组件
            // The Microsoft.Extensions.DependencyInjection.ServiceCollection
            // has extension methods provided by other .NET Core libraries to
            // regsiter services with DI.
            var serviceCollection = new ServiceCollection();

            // The Microsoft.Extensions.Logging package provides this one-liner
            // to add logging services.
            serviceCollection.AddLogging();

            var containerBuilder = new ContainerBuilder();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            containerBuilder.Populate(serviceCollection);

            // Make your Autofac registrations. Order is important!
            // If you make them BEFORE you call Populate, then the
            // registrations in the ServiceCollection will override Autofac
            // registrations; if you make them AFTER Populate, the Autofac
            // registrations will override. You can make registrations
            // before or after Populate, however you choose.
            // containerBuilder.RegisterType<MessageHandler>().As<IHandler>();

            // Creating a new AutofacServiceProvider makes the container
            // available to your app using the Microsoft IServiceProvider
            // interface so you can use those abstractions rather than
            // binding directly to Autofac.
            var container = containerBuilder.Build();
            new AutofacServiceProvider(container);
            #endregion

            try
            {
                Log.Information("Starting up Successful");
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())//使用AutoFac做IOC和AOP
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
