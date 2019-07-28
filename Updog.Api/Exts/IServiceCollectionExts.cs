using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Updog.Api {
    public static class IServiceCollectionExts {
        public static TImplementation ConfigurePoco<TInterface, TImplementation>(this IServiceCollection services, IConfiguration config) where TImplementation : class, TInterface, new() where TInterface : class {
            if (services == null) {
                throw new ArgumentNullException("services");
            }

            if (config == null) {
                throw new ArgumentNullException("config");
            }

            var c = new TImplementation();
            config.Bind(c);
            services.AddSingleton<TInterface>(c);
            return c;
        }
    }
}