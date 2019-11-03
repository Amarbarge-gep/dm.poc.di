using System;
using System.Collections.Generic;
using dm.poc.core;
using Microsoft.Extensions.DependencyInjection;

namespace dm.poc.iservice.Helper
{
    public static class RegisterService
    {
        public static void registerServicesDI(ref IServiceCollection services, List<Type> TypesToRegister)
        {
            services.AddScopedDynamic<ICategoryService>(TypesToRegister);
            //services.AddTransient<ICategoryService,CategoryService>();
        }
    }
}
