using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBKBootCamp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BBKBootCamp.Models;
using BBKBootCamp.Servicios;

namespace BBKBootCamp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //////////////COMENTADO//////
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            //////////////COMENTADO//////

            /////////////AÑADIDO MANUALMENTE/////////
            services.AddIdentity<UsuarioInterno, AppRole>(options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            /////////////AÑADIDO MANUALMENTE/////////

            services.AddTransient<TablaDeEntrevista>();/////ES UNA PRUEBA PARA AÑADIR ENTREVISTADOR A TablaDeEntrevista///
            services.AddTransient<UsuarioInterno>();/////ES UNA PRUEBA PARA AÑADIR ENTREVISTADOR A TablaDeEntrevista///

            services.AddTransient<Servicios.TablaTransitoria>();//ESTE SERVICIO LO AÑADO.ESTA VINCULADO A UNA LISTA DE TablaTransitoria////
            services.AddTransient<Models.Solicitante>();//////ESTE SERVICIO LO AÑADO////
            services.AddTransient<Models.HoraDisponible>();
          
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    //template: "{controller=Home}/{action=Index}/{id?}");

                    ////////////ESTO LO MODIFIQUE COMO PRUEBA/////////
                    template: "{controller=Solicitantes}/{action=Create}/{id?}");
                ////////////ESTO LO MODIFIQUE COMO PRUEBA/////////

            });
        }
    }
}
