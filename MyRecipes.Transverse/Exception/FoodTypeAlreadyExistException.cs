namespace MyRecipes.Transverse.Exception
{
    public class FoodTypeAlreadyExistException : ExceptionBase
    {
        public FoodTypeAlreadyExistException(string error, string message) : base(error, message)
        {
        }
    }
}
