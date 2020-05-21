using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KnowledgeBase.Logic;
using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KnowledgeBase
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
            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews().AddViewLocalization().AddDataAnnotationsLocalization(options => {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedDataAnnotationsResource));
            });




            //services.AddSingleton<ISubjectRepository, MemorySubjectRepository>();
            //services.AddSingleton<ITopicRepository, MemoryTopicRepository>();

            services.AddSingleton<IScheduler, Scheduler>();
            
            services.AddScoped<ISubjectRepository, DbSubjectRepository>();
            services.AddScoped<ITopicRepository, DbTopicRepository>();

            services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequiredLength = 5;   
                opts.Password.RequireNonAlphanumeric = false;  
                opts.Password.RequireLowercase = true; 
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = true;
                opts.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders(); ;

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddAuthentication()
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];

                options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                options.ClaimActions.Clear();
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
            }).AddVkontakte(options =>
            {
                options.ClientId = Configuration["Authentication:Vkontakte:ClientId"];
                options.ClientSecret = Configuration["Authentication:Vkontakte:ClientSecret"];


                // Request for permissions https://vk.com/dev/permissions?f=1.%20Access%20Permissions%20for%20User%20Token
                options.Scope.Add("email");

                // Add fields https://vk.com/dev/objects/user
                options.Fields.Add("uid");
                options.Fields.Add("first_name");
                options.Fields.Add("last_name");

                // In this case email will return in OAuthTokenResponse, 
                // but all scope values will be merged with user response
                // so we can claim it as field
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "uid");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
            }).AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile("Logs/myapp-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               // app.UseHsts();
            }

            //app.UseHttpsRedirection();

            var supportedCultures = new[]
            {
                //new CultureInfo("en-US"),
                new CultureInfo("en"),
                //new CultureInfo("ru-RU"),
                new CultureInfo("ru")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".webmanifest"] = "application/manifest+json";

            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider
            });
            


            

            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
