using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace dm.poc.core
{
    public static class ServiceCollectionExtensions
    {
        public static void AddScopedDynamic<TInterface>(this IServiceCollection services, IEnumerable<Type> types)
        {
            services.AddScoped<Func<string, TInterface>>(serviceProvider => tenant =>
            {
                var itype = typeof(TInterface);
                var type = types
                    .Where(p => itype.IsAssignableFrom(p) && (String.IsNullOrEmpty(tenant) ? true : ((TypeInfo)p).ImplementedInterfaces.Count(y => y.Name == tenant) > 0)
                    ).LastOrDefault();
                if (null == type)
                    throw new KeyNotFoundException("No instance found for the given tenant.");

                return (TInterface)serviceProvider.GetService(type);
            });
        }

        //public static void AddSingleton<TInterface>(this IServiceCollection services, IEnumerable<Type> types)
        //{
        //    services.AddSingleton<Func<string, TInterface>>(serviceProvider => tenant =>
        //    {
        //        var itype = typeof(TInterface);
        //        var type = types
        //            .Where(p => itype.IsAssignableFrom(p) && (String.IsNullOrEmpty(tenant) ? true : ((TypeInfo)p).ImplementedInterfaces.Count(y => y.Name == tenant) > 0)
        //            ).LastOrDefault();
        //        if (null == type)
        //            throw new KeyNotFoundException("No instance found for the given tenant.");

        //        return (TInterface)type;
        //    });
        //}
    }
}
