using HomeworkOS.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkOS.Formatters
{
	public class ProtobufOutputFormatter: TextOutputFormatter
    {
        public ProtobufOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
            => typeof(Response).IsAssignableFrom(type)
                || typeof(IEnumerable<Response>).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(
            OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<ProtobufOutputFormatter>>();
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Response> contacts)
            {
                foreach (var contact in contacts)
                {
                    Protobuf(buffer, contact, logger);
                }
            }
            else
            {
                Protobuf(buffer, (Response)context.Object!, logger);
            }

            await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void Protobuf(
            StringBuilder buffer, Response response, ILogger logger)
        {
            //implementace
            throw new NotImplementedException();
        }
    }
}
