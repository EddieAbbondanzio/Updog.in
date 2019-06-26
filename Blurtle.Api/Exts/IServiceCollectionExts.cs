using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blurtle.Api {
    public static class IServiceCollectionExts {
        public static T ConfigurePoco<T>(this IServiceCollection services, IConfiguration config) where T : class, new() {
            if (services == null) {
                throw new ArgumentNullException("services");
            }

            if (config == null) {
                throw new ArgumentNullException("config");
            }

            var c = new T();
            config.Bind(c);
            services.AddSingleton(c);
            return c;
        }
    }
}