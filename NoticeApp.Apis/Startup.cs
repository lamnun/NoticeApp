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
            //Cors ��� ���

            services.AddCors(options => 
            {
                options.AddPolicy("AllAnyOrigin", 
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            }); 
            #endregion

            AddDependenctInjectionContainerForNoticeApp(services); //�ϴ� �־�� 
        }

        /// <summary>
        /// ��������(NoriceApp) ���� ������(���Ӽ�) ���� ���� �ڵ常 ���� ��Ƽ� ����
        /// </summary>
        /// <param name="services"></param>
        private static void AddDependenctInjectionContainerForNoticeApp(IServiceCollection services) //�ϴ� �־�� �޼ҵ�� ���� ������ 1������ �������� ������� �����Ҽ� ������
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
            // [2] Cors ��� ���
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
