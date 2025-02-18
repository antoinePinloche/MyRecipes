namespace MyRecipes.Transverse.Exception
{
    public class UserNotFoundException : ExceptionBase
    {
        public UserNotFoundException(string error, string message) : base(error, message)
        {
        }
    }
}
