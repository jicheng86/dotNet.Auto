// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Serilog.Sinks.SystemConsole.Themes;

using System;
using System.Linq;
using System.Text;

namespace Auto.IdentityServer4
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
                  .WriteTo.MSSqlServer(connectionString: @"Data Source=.;Database=DB_Auto;Integrated Security=SSPI;Persist Security Info=False;", sinkOptions: new SinkOptions { TableName = "Serilogs4IdentityServrt4", AutoCreateSqlTable = true }, restrictedToMinimumLevel: LogEventLevel.Warning)
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

            try
            {
                Log.Information("Starting up Successful");
                IHost host = CreateHostBuilder(args).Build();
                //初次启动时：生成数据库及初始种子数据
                if (true)
                {
                    Log.Information("Seeding database...");
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var connectionString = config.GetConnectionString("DefaultConnection");
                    Log.Information($"参数连接字符串DefaultConnection={connectionString}。");
                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        Log.Information($"链接字符串DefaultConnection参数为空，请核实！");
                    }
                    InitializeIdentityServer4.InitializeDatabaseANDEnsureSeedData(connectionString);
                    Log.Information("Done seeding database.");

                }
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
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}