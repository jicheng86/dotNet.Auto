// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using IdentityServer4.EntityFramework.Storage;
using Serilog;
using System.Reflection;
using Auto.IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityModel;
using System;

namespace Auto.IdentityServer4
{
    /// <summary>
    /// 
    /// </summary>
    public class InitializeIdentityServer4
    {
        /// <summary>
        ///  //创建数据库及初始化种子数据
        /// </summary>
        /// <param name="connectionString"></param>
        public static void InitializeDatabaseANDEnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;//typeof(SeedData).Assembly.FullName
            //add table IdentityServer4
            services.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            });
            services.AddConfigurationDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            });
          
            var serviceProvider = services.BuildServiceProvider();
            //初始化数据库
            using IServiceScope serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            ConfigurationDbContext context = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            var services2 = new ServiceCollection();
            services2.AddLogging();
            //add table .NetCore Identity
            services2.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services2.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            using var serviceProvider2 = services2.BuildServiceProvider();
            using var serviceScope2 = serviceProvider2.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context2 = serviceScope2.ServiceProvider.GetService<ApplicationDbContext>();
            context2.Database.Migrate();


            //初始化种子数据
            EnsureSeedData(context, context2, serviceScope);
        }
        /// <summary>
        /// 初始化种子数据
        /// </summary>
        /// <param name="context"></param>
        private static void EnsureSeedData(ConfigurationDbContext context, ApplicationDbContext context2, IServiceScope serviceScope)
        {
            var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var alice = userMgr.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(alice, "alice").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("alice created");
            }
            else
            {
                Log.Debug("alice already exists");
            }

            var bob = userMgr.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(bob, "bob").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("bob created");
            }
            else
            {
                Log.Debug("bob already exists");
            }

            if (!context.Clients.Any())
            {
                Log.Debug("Clients being populated");
                foreach (var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Log.Debug("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                Log.Debug("IdentityResources being populated");
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Log.Debug("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                Log.Debug("ApiScopes being populated");
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Log.Debug("ApiScopes already populated");
            }
        }
    }
}
