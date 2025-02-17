using MyRecipes.Transverse.Extension;
using System.ComponentModel;

namespace MyRecipes.Transverse.UnitTest
{
    public class StringExtensionTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [Description("IsNullOrEmpty extension : true return expected")]
        public void IsNullOrEmptyIsTrueTest(string str)
        {
            Assert.True(str.IsNullOrEmpty());
        }

        [Theory]
        [InlineData("string1")]
        [InlineData("a")]
        [InlineData("string1, et un autre elem")]
        [Description("IsNullOrEmpty extension : false return expected")]
        public void IsNullOrEmptyIsFalseTest(string str)
        {
            Assert.False(str.IsNullOrEmpty());
        }
    }
}
