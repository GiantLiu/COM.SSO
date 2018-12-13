using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using COM.Entitys.SSO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace COM.WebA.SSO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SSOContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("SSO"));
            });
            services.AddDataProtection()
                .PersistKeysToDbContext<SSOContext>()  //把加密数据保存在数据库
                //.PersistKeysToFileSystem(new DirectoryInfo(@"\\server\share\directory\"))  //把加密信息保存大文件夹
                .SetApplicationName("SSO");  //把所有子系统都设置为统一的应用名称

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,options=> {
                    options.Cookie.Name = ".AspNet.SharedCookie";//设置统一的Cookie名称
                    options.Cookie.Domain = ".giant.com";//设置Cookie的域为根域，这样所有子域都可以发现这个Cookie
                });
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
