﻿namespace PSP.Api.Filters {

    public class PSPExceptionHandler : ExceptionFilterAttribute {

        public override void OnException(ExceptionContext context) {
            var apiError = new ErrorResponse();
            apiError.StatusCode = 500;
            apiError.StatusPhrase = "Internal Server Error";
            apiError.Timestamp = DateTime.Now;
            apiError.Errors.Add(context.Exception.Message);

            context.Result = new JsonResult(apiError) { StatusCode = 500 };
        }
    }
}