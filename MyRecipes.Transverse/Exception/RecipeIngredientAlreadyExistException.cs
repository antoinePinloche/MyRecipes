using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class RecipeIngredientAlreadyExistException : ExceptionBase
    {
        public RecipeIngredientAlreadyExistException(string error, string message) : base(error, message)
        {
        }
        public RecipeIngredientAlreadyExistException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public RecipeIngredientAlreadyExistException(string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public RecipeIngredientAlreadyExistException(ILogger<object> logger, string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, this.Message);
    }
}
