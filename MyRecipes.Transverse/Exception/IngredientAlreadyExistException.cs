using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class IngredientAlreadyExistException : ExceptionBase
    {
        public IngredientAlreadyExistException(string error, string message) : base(error, message)
        {
        }
        public IngredientAlreadyExistException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public IngredientAlreadyExistException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public IngredientAlreadyExistException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
