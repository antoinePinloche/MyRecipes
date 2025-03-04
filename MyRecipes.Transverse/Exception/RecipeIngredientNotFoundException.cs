﻿using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    /// <summary>
    /// Exception lever losqu'une RecipeIngredient n'existe pas
    /// </summary>
    public class RecipeIngredientNotFoundException : ExceptionBase
    {
        public RecipeIngredientNotFoundException(string error, string message) : base(error, message)
        {
        }
        public RecipeIngredientNotFoundException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public RecipeIngredientNotFoundException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public RecipeIngredientNotFoundException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
