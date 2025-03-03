using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    /// <summary>
    /// Exception lever losqu'un FoodType exciste déja
    /// </summary>
    public class FoodTypeAlreadyExistException : ExceptionBase
    {
        public FoodTypeAlreadyExistException(string error, string message) : base(error, message)
        {
        }
        public FoodTypeAlreadyExistException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public FoodTypeAlreadyExistException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public FoodTypeAlreadyExistException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
