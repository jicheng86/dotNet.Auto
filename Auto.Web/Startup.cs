using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Auto.Model.autoMapper;
using Auto.Repository;
using Auto.Web.Models.autofac;

using Autofac;

using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;

namespace Auto.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// autofac自动注入配置
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutomaticInjectionModule>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EFDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddControllersWithViews();
            services.AddControllers(
                options =>
                {
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; //.NET Core 3.0 和更高版本中的验证系统将不可为 null 的参数或绑定属性视为具有 [Required] 特性。 decimal 和 int 等值类型是不可为 null 的类型 。设置为true（默认false）则阻止该特性
                }
                ).AddNewtonsoftJson();//添加Json.Net库支持

            //services.AddScoped(Assembly.Load("Auto.IRepository"), Assembly.Load("Auto.Repository"));
            //services.AddScoped(Assembly.Load("Auto.IService"), Assembly.Load("Auto.Service"));

            #region 使用AutoMapper
            // 参数类型是Assembly类型的数组 表示AutoMapper将在这些程序集数组里面遍历寻找所有继承了Profile类的配置文件
            // 在当前作用域的所有程序集里面扫描AutoMapper的配置文件
            //.Map<TDto>(Tsrc)方法的新实现配置
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            //ProjectTo()方法的新实现配置
            //var mapperConfiguration = new MapperConfiguration(e => e.AddProfile(new AutoMapperProfile()));
            //services.AddSingleton(mapperConfiguration);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            //Serilog
            app.UseSerilogRequestLogging(options =>
            {
                // Customize the message template
                options.MessageTemplate = "Handled {RequestPath}";

                // Emit debug-level events instead of the defaults
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

                // Attach additional properties to the request completion event
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=index}/{id?}");
            });
        }
    }
}
