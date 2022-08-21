﻿
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace HomeworkOS.SwaggerExtensions
{
	public class AddRequiredHeaderParameter : IOperationFilter
	{

		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.Parameters == null)
				operation.Parameters = new List<OpenApiParameter>();

			operation.Parameters.Add(new OpenApiParameter
			{
				Name = "Accept",
				In = ParameterLocation.Header,
				Description = "Format of response",
				Required = true,
				Schema = new OpenApiSchema
				{
					Type = "string",
					Default = new OpenApiString("json")
				}
			});
		}
	}
}
