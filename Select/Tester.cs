using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Select
{
    [TestFixture]
    public class Tester
    {
        private Fixture _fixture;
        private ExpressionBuilder _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            GetSut();
        }

        [Test]
        public async Task GetOptionsAsyncReturnsFilteredListOfOptions()
        {
            // Arrange
            var data = _fixture.CreateMany<TestClass>(5).ToList();
            var value = data[0].Name.Substring(10);

            // Act
            var result = await _sut.GetOptionsAsync(data, value, x => x.StringProperty, x => x.Name);

            // Assert
            result.Should().HaveCount(1);
        }

        private void GetSut()
        {
            _sut = new ExpressionBuilder();
        }
    }
}
