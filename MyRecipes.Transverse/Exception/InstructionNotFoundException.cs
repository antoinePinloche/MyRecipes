using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Transverse.Exception
{
    public class InstructionNotFoundException : ExceptionBase
    {
        public InstructionNotFoundException(string error, string message) : base(error, message)
        {
        }
        public InstructionNotFoundException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, this.Message);

        public InstructionNotFoundException(string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public InstructionNotFoundException(ILogger<object> logger, string method, string sourceFilePath, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, this.Message);
    }
}
