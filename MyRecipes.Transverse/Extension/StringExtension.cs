namespace MyRecipes.Transverse.Extension
{
    /// <summary>
    /// Methode d'extension pour la class string
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// permet de savoir si une string est null ou empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            if (str is null)
                return true;
            if (str.Equals(string.Empty))
                return true;
            return false;
        }
    }
}
