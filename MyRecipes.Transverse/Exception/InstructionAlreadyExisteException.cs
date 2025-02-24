using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    public class InstructionAlreadyExisteException : ExceptionBase
    {
        public InstructionAlreadyExisteException(string error, string message) : base(error, message)
        {
        }
        public InstructionAlreadyExisteException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, this.Message);

        public InstructionAlreadyExisteException(string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public InstructionAlreadyExisteException(ILogger<object> logger, string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, this.Message);
    }
}
