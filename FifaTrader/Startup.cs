using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FifaTrader.Data;
using FifaTrader.Models.ModelBuilders;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.APIHandler;
using FifaTrader.APIHandler.HttpHandlers.GetRequests;
using FifaTrader.APIHandler.HttpHandlers.UrlBuilder;
using FifaTrader.APIHandler.HttpHandlers.PutRequests;
using FifaTrader.APIHandler.HttpHandlers;
using FifaTrader.APIHandler.HttpHandlers.PostRequests;
using FifaTrader.APIHandler.HttpHandlers.DeleteRequests;
using FifaTrader.Models.EnvVariables;

namespace FifaTrader
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddProtectedBrowserStorage();
            services.AddTransient<IBidViewModelBuilder, BidViewModelBuilder>();
            services.AddTransient<IApiGateway, ApiGateway>();
            services.AddTransient<IGetRequestHandler, GetRequestHandler>();
            services.AddTransient<IGetRequestMaker, GetRequestMaker>();
            services.AddTransient<IUrlBuilder, UrlBuilder>();
            services.AddTransient<IPutRequestHandler, PutRequestHandler>();
            services.AddTransient<IPutRequestMaker, PutRequestMaker>();
            services.AddTransient<IStatusCodeHandler, StatusCodeHandler>();
            services.AddTransient<IHttpWrapper, HttpWrapper>();
            services.AddTransient<IPostRequestHandler, PostRequestHandler>();
            services.AddTransient<IPostRequestMaker, PostRequestMaker>();
            services.AddTransient<IDeleteHandler, DeleteHandler>();
            services.AddTransient<IDeleteMaker, DeleteMaker>();
            services.AddTransient<ITradeIdsBuilder, TradeIdsBuilder>();
            services.AddTransient<IPlayerIdModelBuilder, PlayerIdModelBuilder>();

            services.Configure<FifaYear>(Configuration.GetSection(FifaYear.Location));
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
