namespace Blazedis.App.Core
{
    public class BlazedisRedisConfiguration
    {
        private readonly Dictionary<Guid, BlazedisRedisConfigurationItem> _dics;

        public BlazedisRedisConfiguration() : this(new List<BlazedisRedisConfigurationItem>())
        {
        }

        private BlazedisRedisConfiguration(IEnumerable<BlazedisRedisConfigurationItem> items)
        {
            _dics = items.ToDictionary(i => i.Id, i => i);
        }

        public static BlazedisRedisConfiguration Init(IEnumerable<BlazedisRedisConfigurationItem> items)
        {
            return new BlazedisRedisConfiguration(items);
        }

        public BlazedisRedisConfigurationItem this[Guid id]
        {
            get => _dics.GetValueOrDefault(id);
        }

        public List<BlazedisRedisConfigurationItem> Items => _dics.Values.ToList();

        public BlazedisRedisConfigurationItem Add(BlazedisRedisConfigurationItem item)
        {
            item.Id = Guid.NewGuid();
            _dics.Add(item.Id, item);

            return item;
        }

        public BlazedisRedisConfigurationItem Remove(Guid id)
        {
            _dics.Remove(id, out var item);
            return item;
        }
    }
}
