using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Services
{
    public interface IRedisConfigurationService
    {
        Task InitAsync();

        List<BlazedisRedisConfiguration> GetAll();

        BlazedisRedisConfiguration GetById(Guid id);

        void Add(BlazedisRedisConfiguration configuration);

        void Update(BlazedisRedisConfiguration configuration);

        void Delete(Guid id);
    }
}
