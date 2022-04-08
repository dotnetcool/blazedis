using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<ConnectionMultiplexer> GetByIdAsync(Guid id)
        {
            if (!_connections.TryGetValue(id, out var connection) || connection == null)
            {
                var configuration = _redisConfigurationService.GetById(id);

                if (configuration != null)
                {
                    connection = await ConnectionMultiplexer.ConnectAsync(configuration.Options);
                    _connections[id] = connection;
                }

            }

            return connection;
        }

        public IServer GetServer(ConnectionMultiplexer connection, EndPoint endPoint)
        {
            if (connection != null)
            {
                return connection.GetServer(endPoint);
            }

            return null;
        }

        public async Task<bool> TestConnectionAsync(ConfigurationOptions configurationOptions)
        {
            try
            {
                await ConnectionMultiplexer.ConnectAsync(configurationOptions);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
