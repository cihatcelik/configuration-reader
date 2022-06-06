using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache.CacheService
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Add(string key, object data);
        void Remove(string key);
        void Clear();
        bool Any(string key);
        void SetExpire(string key, DateTime expire);
    }
}
