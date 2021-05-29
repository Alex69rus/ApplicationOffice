using System;
using System.Linq;
using System.Security.Claims;
using ApplicationOffice.Common.Core.Constants;
using ApplicationOffice.Sso.Data;
using ApplicationOffice.Sso.Data.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ApplicationOffice.Sso.IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<SsoDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<AoIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SsoDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<SsoDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AoIdentityUser>>();
                    var petrov = userMgr.FindByNameAsync("pp.petrov").Result;
                    if (petrov == null)
                    {
                        petrov = new AoIdentityUser
                        {
                            UserName = "pp.petrov",
                            Email = "PP.Petrov@mail.ru",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(petrov, "P@$$w0rd").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(petrov, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Петров Пётр Петрович"),
                            new Claim(JwtClaimTypes.GivenName, "Пётр"),
                            new Claim(JwtClaimTypes.FamilyName, "Петров"),
                            new Claim(AoClaims.UserId, "1"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("PP.Petrov created");
                    }
                    else
                    {
                        Log.Debug("PP.Petrov already exists");
                    }

                    var ivanov = userMgr.FindByNameAsync("ii.ivanov").Result;
                    if (ivanov == null)
                    {
                        ivanov = new AoIdentityUser
                        {
                            UserName = "ii.ivanov",
                            Email = "II.Ivanov@mail.ru",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(ivanov, "P@$$w0rd").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(ivanov, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Иванов Иван"),
                            new Claim(JwtClaimTypes.GivenName, "Иван"),
                            new Claim(JwtClaimTypes.FamilyName, "Иванов"),
                            new Claim(AoClaims.UserId, "2"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("Ivanov created");
                    }
                    else
                    {
                        Log.Debug("Ivanov already exists");
                    }

                    var alexeev = userMgr.FindByNameAsync("aa.alexeev").Result;
                    if (alexeev == null)
                    {
                        alexeev = new AoIdentityUser
                        {
                            UserName = "aa.alexeev",
                            Email = "AA.Alexeev@mail.ru",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(alexeev, "P@$$w0rd").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alexeev, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Алексеев Алексей"),
                            new Claim(JwtClaimTypes.GivenName, "Алексей"),
                            new Claim(JwtClaimTypes.FamilyName, "Алексеевич"),
                            new Claim(AoClaims.UserId, "3"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("Alexeev created");
                    }
                    else
                    {
                        Log.Debug("Alexeev already exists");
                    }
                }
            }
        }
    }
}
