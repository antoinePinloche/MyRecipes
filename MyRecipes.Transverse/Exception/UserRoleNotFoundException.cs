using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class UserRoleNotFoundException : ExceptionBase
    {
        public UserRoleNotFoundException(string error, string message) : base(error, message)
        {
        }
        public UserRoleNotFoundException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public UserRoleNotFoundException(string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public UserRoleNotFoundException(ILogger<object> logger, string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
