using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class FoodTypeNotFoundException : ExceptionBase
    {
        public FoodTypeNotFoundException(string error, string message) : base(error, message)
        {
        }
        public FoodTypeNotFoundException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public FoodTypeNotFoundException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public FoodTypeNotFoundException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
