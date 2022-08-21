using HomeworkOS.Formatters;
using HomeworkOS.Services;
using HomeworkOS.Services.Interfaces;
using HomeworkOS.SwaggerExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeworkOS
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

			services.AddControllers(options =>
			{
				options.RespectBrowserAcceptHeader = true;
				options.InputFormatters.Insert(0, new ProtobufInputFormatter());
				options.OutputFormatters.Insert(0, new ProtobufOutputFormatter());
			})
			.AddXmlSerializerFormatters();


			services.AddSwaggerGen();


			services.AddSwaggerGen(c =>
			{
				c.OperationFilter<AddRequiredHeaderParameter>();
			});
			services.AddTransient<iFileService, FileService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllers();
			//});
		}
	}
}
