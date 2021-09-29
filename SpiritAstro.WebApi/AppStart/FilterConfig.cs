using Microsoft.Extensions.DependencyInjection;

namespace SpiritAstro.WebApi.AppStart
{
    public static class FilterConfig
    {
        public static void ConfigureFilterServices(this IServiceCollection services)
        {
            services.AddMvc(ops =>
            {
                ops.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory());
            });
            // services.AddControllers(options =>
            // {
            //     options.Filters.Add<ExceptionHandlerFilter>();
            // }).ConfigureApiBehaviorOptions(options =>
            // {
            //     options.InvalidModelStateResponseFactory = actionContext =>
            //     {
            //         var mulArgsException = new MultipleArgsException(actionContext.ModelState);
            //         var exceptions = mulArgsException.Errors.Select(x => new ExceptionDetail(x.Key, x.Value.ToArray())).ToList();
            //
            //         return new BadRequestObjectResult(new ExceptionResponse(
            //                 exceptions,
            //                 status: (int)System.Net.HttpStatusCode.BadRequest,
            //                 message: System.Net.HttpStatusCode.BadRequest.ToString(),
            //                 helpLink: "",
            //                 stacktrace: ""
            //                 ));
            //     };
            // }); ;
        }
    }
}
