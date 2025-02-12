namespace MyRecipes.Transverse.Extension
{
    public static class ICollectionExtension
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            if (collection is null)
                return true;
            if (collection.Count == 0)
                return true;
            return false;
        }
    }
}
