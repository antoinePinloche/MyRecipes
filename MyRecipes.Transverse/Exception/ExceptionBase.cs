using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    /// <summary>
    /// Class contenant l'abstraction des exceptions personnalisé
    /// </summary>
    public abstract class ExceptionBase : System.Exception
    {
        public string Error {  get; set; }
        public string Message {  get; set; }

        internal ExceptionBase(string message) : base(message) { }

        public ExceptionBase(string error, string message) : base(message)
        {
            Error = error;
            Message = message;
        }

        public ExceptionBase(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(message);
    }
}
