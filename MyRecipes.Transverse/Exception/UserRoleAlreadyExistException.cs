﻿using Microsoft.Extensions.Logging;

namespace MyRecipes.Transverse.Exception
{
    /// <summary>
    /// Exception lever losqu'une User existe déja
    /// </summary>
    public class UserRoleAlreadyExistException : ExceptionBase
    {
        public UserRoleAlreadyExistException(string error, string message) : base(error, message)
        {
        }
        public UserRoleAlreadyExistException(ILogger<object> logger, string error, string message) : base(message) => logger.LogError(this, message);

        public UserRoleAlreadyExistException(string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}")
        { }

        public UserRoleAlreadyExistException(ILogger<object> logger, string method, string sourceFilePath, string error, string message) :
            base($"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{method} : {message}") => logger.LogError(this, message);
    }
}
