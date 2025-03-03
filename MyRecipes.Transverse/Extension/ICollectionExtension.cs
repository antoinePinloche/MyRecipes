using System.Collections;

namespace MyRecipes.Transverse.Extension
{
    /// <summary>
    /// Methode d'extension pour les ICollection<T>
    /// </summary>
    public static class ICollectionExtension
    {
        /// <summary>
        /// permet de savoir si une collection est null ou empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
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
