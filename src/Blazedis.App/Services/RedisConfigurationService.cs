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
        private const string CONFIG_FILE_NAME = "blazedis.config.json";
        private static BlazedisRedisConfiguration _configurations = new();

        public RedisConfigurationService()
        {
        }

        public List<BlazedisRedisConfigurationItem> GetAll()
        {
            return _configurations.Items;
        }

        public BlazedisRedisConfigurationItem GetById(Guid id)
        {
            return _configurations[id];
        }

        public BlazedisRedisConfigurationItem Add(BlazedisRedisConfigurationItem configuration)
        {
            var result = _configurations.Add(configuration);

            SendChangedMessage();
            _ = SaveAsFileAsync();

            return result;
        }

        public void Update(BlazedisRedisConfigurationItem configuration)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            _configurations.Remove(id);
            SendChangedMessage();

            _ = SaveAsFileAsync();
        }

        public async Task InitConfigurationAsync()
        {
            var items = await LoadFromFileAsync();
            _configurations = BlazedisRedisConfiguration.Init(items);

            SendChangedMessage();
        }

        private static async Task SaveAsFileAsync()
        {
            var configFile = Path.Combine(FileSystem.AppDataDirectory, CONFIG_FILE_NAME);
            var json = JsonSerializer.Serialize(_configurations.Items);

            await File.WriteAllTextAsync(configFile, json);
        }

        private static async Task<List<BlazedisRedisConfigurationItem>> LoadFromFileAsync()
        {
            var configFile = Path.Combine(FileSystem.AppDataDirectory, CONFIG_FILE_NAME);

            if (File.Exists(configFile))
            {
                using var fs = new FileStream(configFile, FileMode.Open, FileAccess.Read);
                var configurations = await JsonSerializer.DeserializeAsync<List<BlazedisRedisConfigurationItem>>(fs);

                return configurations;
            }

            return new();
        }

        private static void SendChangedMessage()
        {
            MessagingCenter.Send(_configurations, EventType.RedisConfigurationChanged);
        }
    }
}
