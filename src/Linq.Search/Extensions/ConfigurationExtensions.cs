using System;
using Microsoft.Extensions.DependencyInjection;

namespace CityofEdmonton.Linq.Search.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configure the Linq.Search library using <see cref="SearchConfigurationOptions"/>, but also allow access to the underlying <see cref="IServiceProvider"/>.
        /// <para>Defaults to configuring the <see cref="SearchConfigurationOptions"/> as a <see cref="ServiceLifetime.Singleton"/>.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sc"></param>
        /// <param name="configure"></param>
        /// <param name="lifetime"></param>
        public static void ConfigureLinqSearch<T>(this IServiceCollection sc,
            Action<SearchConfigurationOptions, IServiceProvider> configure = null,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            if (lifetime == ServiceLifetime.Singleton)
            {
                sc.AddSingleton<SearchConfigurationOptions>(implementationFactory);
            }
            else if (lifetime == ServiceLifetime.Transient)
            {
                sc.AddTransient<SearchConfigurationOptions>(implementationFactory);
            }
            else if (lifetime == ServiceLifetime.Scoped)
            {
                sc.AddScoped<SearchConfigurationOptions>(implementationFactory);
            }

            SearchConfigurationOptions implementationFactory(IServiceProvider provider)
            {
                var config = new SearchConfigurationOptions();
                configure?.Invoke(config, provider);
                return config;
            }
        }

        /// <summary>
        /// Configure the Linq.Search library using <see cref="SearchConfigurationOptions"/>.
        /// <para>Defaults to configuring the <see cref="SearchConfigurationOptions"/> as a <see cref="ServiceLifetime.Singleton"/>.</para>
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="sc"></param>
        /// <param name="configure"></param>
        /// <param name="lifetime"></param>
        public static void ConfigureLinqSearch<T>(this IServiceCollection sc,
            Action<SearchConfigurationOptions> configure = null,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            if (lifetime == ServiceLifetime.Singleton)
            {
                sc.AddSingleton<SearchConfigurationOptions>(implementationFactory);
            }
            else if (lifetime == ServiceLifetime.Scoped)
            {
                sc.AddScoped<SearchConfigurationOptions>(implementationFactory);
            }
            else if (lifetime == ServiceLifetime.Transient)
            {
                sc.AddScoped<SearchConfigurationOptions>(implementationFactory);
            }

            SearchConfigurationOptions implementationFactory(IServiceProvider _)
            {
                var config = new SearchConfigurationOptions();
                configure?.Invoke(config);
                return config;
            }
        }
    }
}
