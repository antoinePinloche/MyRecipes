using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class RecipeAlreadyExistException : ExceptionBase
    {
        public RecipeAlreadyExistException(string error, string message) : base(error, message)
        {
        }
        public RecipeAlreadyExistException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public RecipeAlreadyExistException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public RecipeAlreadyExistException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
