using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class UserRoleAlreadyExistException : ExceptionBase
    {
        public UserRoleAlreadyExistException(string error, string message) : base(error, message)
        {
        }
        public UserRoleAlreadyExistException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public UserRoleAlreadyExistException(string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public UserRoleAlreadyExistException(ILogger<object> logger, string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
