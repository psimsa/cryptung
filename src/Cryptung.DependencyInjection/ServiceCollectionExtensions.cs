using Cryptung;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptung.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCryptungService(
            this IServiceCollection services,
            string encryptionKey) =>
            services.AddSingleton(_ => CryptungServiceFactory.Create(encryptionKey));
    }
}