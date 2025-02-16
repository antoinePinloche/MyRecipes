namespace MyRecipes.Transverse.Exception
{
    public class WrongParameterException : ExceptionBase
    {
        public WrongParameterException(string error, string message) : base(error, message)
        {
        }
    }
}
