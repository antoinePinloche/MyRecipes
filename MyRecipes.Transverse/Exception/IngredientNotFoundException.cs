using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class IngredientNotFoundException : ExceptionBase
    {
        public IngredientNotFoundException(string error, string message) : base(error, message)
        {
        }
        public IngredientNotFoundException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public IngredientNotFoundException(string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public IngredientNotFoundException(ILogger<object> logger, string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
