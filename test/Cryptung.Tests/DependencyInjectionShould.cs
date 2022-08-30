using Cryptung.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cryptung.Tests
{

    public class DependencyInjectionShould
    {
        [Fact]
        public void ResolveEncryptionService()
        {
            var services = new ServiceCollection();
            services.AddCryptungService("some key");
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<ICryptungService>();
            Assert.NotNull(service);
        }

        [Fact]
        public void EncryptAndDecryptUsingResolvedService()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCryptungService("some encryption key");
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var service = serviceProvider.GetService<ICryptungService>();
            var input = "some text";

            Assert.NotNull(service);
            var encrypted = service!.Encrypt(input);
            Assert.NotNull(encrypted);

            var decrypted = service.Decrypt(encrypted);
            Assert.Equal(input, decrypted);
        }
    }
}