using HomeworkOS.Responses;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkOS.Formatters
{
	public class ProtobufInputFormatter: TextInputFormatter
    {

        public ProtobufInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/protobuf"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
            => type == typeof(Response);

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context, Encoding effectiveEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<ProtobufInputFormatter>>();

            using var reader = new StreamReader(httpContext.Request.Body, effectiveEncoding);
            string? nameLine = null;

            try
            {
                var resp = new Response();

                //implementace..
                throw new NotImplementedException();

                return await InputFormatterResult.SuccessAsync(resp);
            }
            catch
            {
                logger.LogError("Read failed: nameLine = {nameLine}", nameLine);
                return await InputFormatterResult.FailureAsync();
            }
        }

        private static async Task<string> ReadLineAsync(
            string expectedText, StreamReader reader, InputFormatterContext context,
            ILogger logger)
        {
            var line = await reader.ReadLineAsync();

            if (line is null || !line.StartsWith(expectedText))
            {
                var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

                context.ModelState.TryAddModelError(context.ModelName, errorMessage);
                logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }

            return line;
        }
    }
}
