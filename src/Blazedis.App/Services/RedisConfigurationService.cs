using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazedis.App.Services
{
    public class RedisConfigurationService : IRedisConfigurationService
    {
        private const string CONFIG_FILE_NAME = "./blazedis.config.json";
        private Dictionary<Guid, BlazedisRedisConfiguration> _configurations = null;
        private readonly EventHub _eventHub;

        public RedisConfigurationService(
            EventHub eventHub)
        {
            _eventHub = eventHub;

        }

        public async Task InitAsync()
        {
            if (_configurations == null)
            {
                _configurations = await LoadFromFileAsync();
            }
        }

        public List<BlazedisRedisConfiguration> GetAll()
        {
            return _configurations.Values.ToList();
        }

        public BlazedisRedisConfiguration GetById(Guid id)
        {
            return _configurations.GetValueOrDefault(id);
        }


        public void Add(BlazedisRedisConfiguration configuration)
        {
            if (_configurations.TryAdd(configuration.Id, configuration))
            {
                SaveAsFileAsync();
                _eventHub.Publish(EventType.RedisConfigurationChanged);
            }
        }

        public void Update(BlazedisRedisConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            if (_configurations.Remove(id, out _))
            {

                _eventHub.Publish(EventType.RedisConfigurationChanged);
            }
        }

        private async Task SaveAsFileAsync()
        {
            using var fs = new FileStream(CONFIG_FILE_NAME, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(fs, _configurations);
        }

        private async Task<Dictionary<Guid, BlazedisRedisConfiguration>> LoadFromFileAsync()
        {
            var d = Directory.GetCurrentDirectory();
            using var fs = new FileStream(CONFIG_FILE_NAME, FileMode.OpenOrCreate, FileAccess.Read);
            return await JsonSerializer.DeserializeAsync<Dictionary<Guid, BlazedisRedisConfiguration>>(fs);

        }
    }
}
