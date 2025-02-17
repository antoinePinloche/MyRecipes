using MyRecipes.Transverse.Extension;
using System.ComponentModel;

namespace MyRecipes.Transverse.UnitTest
{
    public class GuidExtensionTest
    {
        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData(null)]
        [Description("IsNullOrEmpty extension : true return expected")]
        public void IsNullOrEmptyIsTrueTest(string? guidTest)
        {
            Guid? guid;
            if (guidTest == null)
                guid = null;
            Guid.TryParse(guidTest, out Guid result);
            guid = (Guid?)result;
            Assert.True(guid.IsNullOrEmpty());
        }

        [Theory]
        [InlineData("6b210ccf-7b6a-47fe-bdcb-874b1a2b7e1c")]
        [Description("IsNullOrEmpty extension : false return expected")]
        public void IsNullOrEmptyIsFalseTest(string? guidTest)
        {
            Guid? guid;
            Guid.TryParse(guidTest, out Guid result);
            guid = (Guid?)result;
            Assert.False(guid.IsNullOrEmpty());
        }

        [Fact]
        [Description("IsEmpty extension")]
        public void IsEmptyTest()
        {
            Guid guidFalse = Guid.NewGuid();
            Guid guidTrue = Guid.Empty;
            Assert.False(guidFalse.IsEmpty());
            Assert.True(guidTrue.IsEmpty());
        }
    }
}