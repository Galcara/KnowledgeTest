using AutoMapper;
using BusinessLogicalLayer;
using BusinessLogicalLayer.Interfaces;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationLayerMVC.Models;
using PresentationLayerMVC.Models.CompanyModels;
using PresentationLayerMVC.Models.SupplierModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PresentationLayerMVC
{
    public class Startup
    {
        public static string UrlBase = "http://localhost:5000/";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(c => c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddSession();
            services.AddControllersWithViews();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyInsertViewModel, Company>();
                cfg.CreateMap<Company, CompanyQueryViewModel>();
                cfg.CreateMap<CompanyUpdateViewModel, Company>();
                cfg.CreateMap<CompanyQueryViewModel, CompanyUpdateViewModel>();

                cfg.CreateMap<SupplierCpfInsertViewModel, Supplier>().ForMember(c => c.Companies, c => c.Ignore());
                cfg.CreateMap<Supplier, SupplierQueryViewModel>();
                cfg.CreateMap<SupplierCpfUpdateViewModel, Company>();
                cfg.CreateMap<SupplierDetailViewModel, SupplierCpfUpdateViewModel>();
                //cfg.CreateMap<Administrator, AdminQueryViewModel>();
                //cfg.CreateMap<TeacherInsertViewModel, Teacher>().ForMember(c => c.Subjects, c => c.Ignore());


            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
