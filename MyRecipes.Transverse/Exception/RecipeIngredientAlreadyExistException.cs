using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    /// <summary>
    /// Exception lever losqu'une RecipeIngredient existe déja
    /// </summary>
    public class RecipeIngredientAlreadyExistException : ExceptionBase
    {
        public RecipeIngredientAlreadyExistException(string error, string message) : base(error, message)
        {
        }
        public RecipeIngredientAlreadyExistException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public RecipeIngredientAlreadyExistException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public RecipeIngredientAlreadyExistException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, this.Message);
    }
}
