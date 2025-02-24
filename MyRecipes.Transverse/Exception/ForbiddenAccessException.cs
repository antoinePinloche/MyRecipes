using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class ForbiddenAccessException : ExceptionBase
    {
        public ForbiddenAccessException(string error, string message) : base(error, message)
        {
        }

        public ForbiddenAccessException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public ForbiddenAccessException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public ForbiddenAccessException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
