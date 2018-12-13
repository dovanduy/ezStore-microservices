﻿using System.Threading.Tasks;

namespace Microservices.ApplicationCore.Interfaces
{
    public interface ICacheService
    {
        Task Set<T>(string key, T data);

        Task<T> Get<T>(string key);
    }
}