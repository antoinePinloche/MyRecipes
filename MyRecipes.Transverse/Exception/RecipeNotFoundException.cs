using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    /// <summary>
    /// Exception lever losqu'une Recipe n'existe pas
    /// </summary>
    public class RecipeNotFoundException : ExceptionBase
    {
        public RecipeNotFoundException(string error, string message) : base(error, message)
        {
        }
        public RecipeNotFoundException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public RecipeNotFoundException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public RecipeNotFoundException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
