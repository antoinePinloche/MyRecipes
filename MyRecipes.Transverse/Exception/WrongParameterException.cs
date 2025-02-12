namespace MyRecipes.Transverse.Exception
{
    public class WrongParameterException : System.Exception
    {
        public WrongParameterException(string method, string sourcePath, string message) 
        : base($"{Path.GetFileNameWithoutExtension(sourcePath)}.{method} : {message}") { }
    }
}
