using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class WrongParameterException : ExceptionBase
    {

        public WrongParameterException(string error, string message) : base(error, message)
        {
        }

        public WrongParameterException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public WrongParameterException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public WrongParameterException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
