using Microsoft.AspNetCore.Diagnostics;
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
                    Title = foodTypeNotFoundException.Error,
                    Detail = foodTypeNotFoundException.Message,
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
                    Title = foodTypeAlreadyExistException.Error,
                    Detail = foodTypeAlreadyExistException.Message,
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
            
            if (exception is IngredientNotFoundException ingredientNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = ingredientNotFoundException.Error,
                    Detail = ingredientNotFoundException.Message,
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

            if (exception is IngredientAlreadyExistException ingredientAlreadyExistException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = ingredientAlreadyExistException.Error,
                    Detail = ingredientAlreadyExistException.Message,
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

            #region Recipe
            if (exception is RecipeAlreadyExistException recipeAlreadyExistException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = recipeAlreadyExistException.Error,
                    Detail = recipeAlreadyExistException.Message,
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

            if (exception is RecipeNotFoundException recipeNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = recipeNotFoundException.Error,
                    Detail = recipeNotFoundException.Message,
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
            #endregion

            #region RecipeIngredient
            if (exception is RecipeIngredientAlreadyExistException recipeIngredientAlreadyExistException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = recipeIngredientAlreadyExistException.Error,
                    Detail = recipeIngredientAlreadyExistException.Message,
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

            if (exception is RecipeIngredientNotFoundException recipeIngredientNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = recipeIngredientNotFoundException.Error,
                    Detail = recipeIngredientNotFoundException.Message,
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
            #endregion

            #region Instruction
            if (exception is InstructionAlreadyExisteException instructionAlreadyExisteException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = instructionAlreadyExisteException.Error,
                    Detail = instructionAlreadyExisteException.Message,
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

            if (exception is InstructionNotFoundException instructionNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = instructionNotFoundException.Error,
                    Detail = instructionNotFoundException.Message,
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
            #endregion

            #region
            if (exception is WrongParameterException wrongParameterException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = wrongParameterException.Error,
                    Detail = wrongParameterException.Message,
                    Type = "Conflict"
                };
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return await _problemDetail.TryWriteAsync(
                    new ProblemDetailsContext
                    {
                        HttpContext = httpContext,
                        ProblemDetails = problemDetails
                    });
            }
            #endregion

            #region User
            if (exception is UserNotFoundException userNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = userNotFoundException.Error,
                    Detail = userNotFoundException.Message,
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
            #endregion

            #region UserRole
            if (exception is UserRoleAlreadyExistException userRoleAlreadyExistException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = userRoleAlreadyExistException.Error,
                    Detail = userRoleAlreadyExistException.Message,
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

            if (exception is UserRoleNotFoundException userRoleNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = userRoleNotFoundException.Error,
                    Detail = userRoleNotFoundException.Message,
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
            #endregion
            return false;
        }
    }
}
