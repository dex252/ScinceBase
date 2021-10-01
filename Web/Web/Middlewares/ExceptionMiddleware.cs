using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //Если потребуется логирование и просмотр содержимого request, то расскоментировать строку
                //context.Request.EnableBuffering();

                await next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var target = exception.TargetSite;

            //Если потребуется чтение содержимого - расскоментировать вместе с EnableBuffering
            #region ReadRequestBody
            //string inParams = null;
            //var methodName = target?.Name;
            //var className = target?.DeclaringType.FullName;
            //if (context.Request.Body.CanRead)
            //{
            //    context.Request.Body.Position = 0;
            //    using (var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 4096, true))
            //    {
            //        inParams = await streamReader.ReadToEndAsync();
            //    }
            //}
            #endregion

            var response = context.Response;
            response.StatusCode = 500;
            response.ContentType = "json/text";
            var message = exception.Message;

            //if (exception is MySqlException)
            //{
            //    var ex = exception as MySqlException;
            //    message = MySqlExceptionExtension.GetExceptionMessage(ex);
            //}

            await response.WriteAsync(JsonConvert.SerializeObject(message));
        }
    }
}
