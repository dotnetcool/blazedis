using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Services
{
    public class RedisConnectionService : IRedisConnectionService
    {
        private readonly static Dictionary<Guid, ConnectionMultiplexer> _connections = new();
        private readonly IRedisConfigurationService _redisConfigurationService;

        public RedisConnectionService(
            IRedisConfigurationService redisConfigurationService)
        {
            _redisConfigurationService = redisConfigurationService;
        }

        public ConnectionMultiplexer GetById(Guid id)
        {
            if (!_connections.TryGetValue(id, out var connection) || connection == null)
            {
                var configuration = _redisConfigurationService.GetById(id);

                if (configuration != null)
                {
                    connection = ConnectionMultiplexer.Connect(configuration.Options);
                    _connections[id] = connection;
                }

            }

            return connection;
        }

        public IServer GetServerById(Guid id)
        {
            var connection = GetById(id);
            if (connection != null)
            {
                var configuration = _redisConfigurationService.GetById(id);
                return connection.GetServer(configuration.Options.EndPoints.First());
            }

            return null;
        }
    }
}
