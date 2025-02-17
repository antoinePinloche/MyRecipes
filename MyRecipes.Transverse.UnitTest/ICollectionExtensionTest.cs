using MyRecipes.Transverse.Extension;
using System.ComponentModel;

namespace MyRecipes.Transverse.UnitTest
{
    public sealed class ICollectionExtensionTest
    {
        [Fact]
        [Description("IsNullOrEmpty extension : list<int>")]
        public void IsNullOrEmptyTest()
        {
            List<int> list = new List<int>() {1, 2, 3, 4, 5};
            List<int> nullList = null;
            List<int> emptyList = new List<int>();

            Assert.False(list.IsNullOrEmpty());
            Assert.True(nullList.IsNullOrEmpty());
            Assert.True(emptyList.IsNullOrEmpty());
        }
    }
}
