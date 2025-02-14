﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Web.API
{
    public class ProblemExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetail;

        public ProblemExceptionHandler(IProblemDetailsService problemDetail)
        {
            _problemDetail = problemDetail;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ExceptionBase problemException)
            {
                return true;
            }
            #region FoodType
            if (exception is FoodTypeNotFoundException foodTypeNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = problemException.Error,
                    Detail = problemException.Message,
                    Type = "NotFound"
                };
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return await _problemDetail.TryWriteAsync(
                    new ProblemDetailsContext
                    {
                        HttpContext = httpContext,
                        ProblemDetails = problemDetails
                    });
            }

            if (exception is FoodTypeAlreadyExistException foodTypeAlreadyExistException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = problemException.Error,
                    Detail = problemException.Message,
                    Type = "Conflict"
                };
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                return await _problemDetail.TryWriteAsync(
                    new ProblemDetailsContext
                    {
                        HttpContext = httpContext,
                        ProblemDetails = problemDetails
                    });
            }

            #endregion

            #region Ingredient

            #endregion

            #region Recipe
            #endregion

            #region RecipeIngredient

            #endregion

            #region Instruction

            #endregion
            return false;
        }
    }
}
