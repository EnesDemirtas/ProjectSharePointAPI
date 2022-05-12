﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PSP.Api.Contracts.Common;

namespace PSP.Api.Filters {

    public class ValidateModelAttribute : ActionFilterAttribute {

        public override void OnResultExecuting(ResultExecutingContext context) {
            if (!context.ModelState.IsValid) {
                var apiError = new ErrorResponse();
                apiError.StatusCode = 400;
                apiError.StatusPhrase = "Bad Request";
                apiError.Timestamp = DateTime.Now;
                var errors = context.ModelState.AsEnumerable();

                foreach (var error in errors) {
                    foreach (var inner in error.Value.Errors) {
                        apiError.Errors.Add(inner.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
                // TO DO: Make sure Asp.Net Core doesn't override our action result body
            }
        }
    }
}