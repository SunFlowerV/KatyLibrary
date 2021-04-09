using System;
using KatyLibrary.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(KatyLibrary.Areas.Identity.IdentityHostingStartup))]
//namespace KatyLibrary.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) => {
//                services.AddDbContext<KatyLibraryContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("KatyLibraryContextConnection")));

//                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                    .AddEntityFrameworkStores<KatyLibraryContext>();
//            });
//        }
//    }
//}