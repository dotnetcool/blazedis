namespace Blazedis.App.Services
{
    public interface IRedisConfigurationService
    {
        List<BlazedisRedisConfigurationItem> GetAll();

        BlazedisRedisConfigurationItem GetById(Guid id);

        BlazedisRedisConfigurationItem Add(BlazedisRedisConfigurationItem configuration);

        void Update(BlazedisRedisConfigurationItem configuration);

        void Delete(Guid id);
    }
}
