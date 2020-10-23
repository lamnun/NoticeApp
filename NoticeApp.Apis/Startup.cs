using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NoticeApp.Modells;

namespace NoticeApp.Apis
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
            services.AddControllers();

            #region CORS
            //Cors 사용 등록

            services.AddCors(options => 
            {
                options.AddPolicy("AllAnyOrigin", 
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            }); 
            #endregion

            AddDependenctInjectionContainerForNoticeApp(services); //일단 넣어둠 
        }

        /// <summary>
        /// 공지사항(NoriceApp) 관련 의존성(종속성) 주입 관련 코드만 따로 모아서 관리
        /// </summary>
        /// <param name="services"></param>
        private static void AddDependenctInjectionContainerForNoticeApp(IServiceCollection services) //일단 넣어둠 메소드로 만듦 지금은 1개지만 여려개가 생길것을 생각할수 있으니
        {
            services.AddTransient<INoticeRepositoryAsync, NoticeRepositoryAsync>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // [2] Cors 사용 허용
            app.UseCors("AllowAnyOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
