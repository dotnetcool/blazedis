using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Services
{
    public interface IRedisConnectionService
    {
        ConnectionMultiplexer GetById(Guid id);
        IServer GetServerById(Guid id);
        Task<bool> TestConnectionAsync(ConfigurationOptions configurationOptions);
    }
}
