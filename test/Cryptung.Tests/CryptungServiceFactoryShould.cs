using Xunit;

namespace Cryptung.Tests
{

    public class CryptungServiceFactoryShould
    {
        [Fact]
        public void ReturnEncryptionService()
        {
            var service = CryptungServiceFactory.Create("some key");
            Assert.NotNull(service);
        }
    }
}