using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class RecipeIngredientNotFoundException : ExceptionBase
    {
        public RecipeIngredientNotFoundException(string error, string message) : base(error, message)
        {
        }
        public RecipeIngredientNotFoundException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public RecipeIngredientNotFoundException(string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public RecipeIngredientNotFoundException(ILogger<object> logger, string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
