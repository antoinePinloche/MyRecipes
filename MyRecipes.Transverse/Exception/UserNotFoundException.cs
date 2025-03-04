﻿using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    /// <summary>
    /// Exception lever losqu'une User n'existe pas
    /// </summary>
    public class UserNotFoundException : ExceptionBase
    {
        public UserNotFoundException(string error, string message) : base(error, message)
        {
        }

        public UserNotFoundException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public UserNotFoundException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public UserNotFoundException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
