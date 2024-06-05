using Microsoft.AspNetCore.Mvc;

namespace WebApi.Utils
{
    public class ActionResultApi<T> : ActionResult, IActionResultApi<T>
    {
        public int StatusCode { get; }
        public string Message { get; }
        public T? Data { get; set; }

        private readonly ActionResult _result;

        public ActionResultApi(int statusCode, string message, T? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;

            var responseObj = new
            {
                statusCode,
                message,
                data
            };

            _result = statusCode switch
            {
                200 => new OkObjectResult(responseObj),                             // OK()
                201 => new ObjectResult(responseObj) { StatusCode = statusCode },   // Created()
                202 => new ObjectResult(responseObj) { StatusCode = statusCode },   // AcceptedResult()
                204 => new ObjectResult(responseObj) { StatusCode = statusCode },   // NoContent()
                400 => new BadRequestObjectResult(responseObj),                     // BadRequest()
                401 => new UnauthorizedObjectResult(responseObj),                   // Unauthorized()
                404 => new NotFoundObjectResult(responseObj),                       // NotFound()
                500 => new ObjectResult(responseObj) { StatusCode = statusCode },   // Server Error
                _ => new ObjectResult(responseObj) { StatusCode = statusCode }      // Sin Definir
            };
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            return _result.ExecuteResultAsync(context);
        }

        public override void ExecuteResult(ActionContext context)
        {
            _result.ExecuteResult(context);
        }
    }

    public class ActionResultApi : ActionResult, IActionResultApi
    {
        public int StatusCode { get; }
        public string Message { get; }

        private readonly ActionResult _result;

        public ActionResultApi(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
            
            var responseObj = new
            {
                StatusCode,
                Message,
            };

            _result = statusCode switch
            {
                200 => new OkObjectResult(responseObj),                             // OK()
                201 => new ObjectResult(responseObj) { StatusCode = statusCode },   // Created()
                202 => new ObjectResult(responseObj) { StatusCode = statusCode },   // AcceptedResult()
                204 => new ObjectResult(responseObj) { StatusCode = statusCode },   // NoContent()
                400 => new BadRequestObjectResult(responseObj),                     // BadRequest()
                401 => new UnauthorizedObjectResult(responseObj),                   // Unauthorized()
                404 => new NotFoundObjectResult(responseObj),                       // NotFound()
                500 => new ObjectResult(responseObj) { StatusCode = statusCode },   // Server Error
                _ => new ObjectResult(responseObj) { StatusCode = statusCode }      // Sin Definir
            };
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            return _result.ExecuteResultAsync(context);
        }

        public override void ExecuteResult(ActionContext context)
        {
            _result.ExecuteResult(context);
        }
    }
}
