namespace ApplicationClassLibrary.Filters
{
    public class SwaggerApiPadLockFilter
    {
        //public void Apply(OpenApiOperation operation, OperationFilterContext context)
        //{
        //    var hasAuthorizeAttribute = context.MethodInfo.DeclaringType!
        //        .GetCustomAttributes(true)
        //        .Union(context.MethodInfo.GetCustomAttributes(true))
        //        .OfType<AuthorizeAttribute>().Any();

        //    if (hasAuthorizeAttribute)
        //    {
        //        operation.Security = new List<OpenApiSecurityRequirement>
        //        {
        //            new OpenApiSecurityRequirement
        //            {
        //                {
        //                    new OpenApiSecurityScheme
        //                    {
        //                        Reference = new OpenApiReference
        //                        {
        //                            Type = ReferenceType.SecurityScheme,
        //                            Id = "Bearer"
        //                        }
        //                    },
        //                    Array.Empty<string>()
        //                }
        //            }
        //        };
        //    }
        //}
    }
}
