using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Services
{
    public interface IRedisConnectionService
    {
        Task<ConnectionMultiplexer> GetByIdAsync(Guid id);
        IServer GetServer(ConnectionMultiplexer connection, EndPoint endPoint);
        Task<bool> TestConnectionAsync(ConfigurationOptions configurationOptions);
    }
}
