﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ezStore.Product.ApplicationCore
{
    public static class HandlerRegister
    {
        public static void Register(IServiceCollection services)
        {
            Ws4vn.Microservices.ApplicationCore.SharedKernel.HandlerRegister.Register(Assembly.GetExecutingAssembly(), services);
        }
    }
}
