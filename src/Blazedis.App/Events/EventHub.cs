using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Events
{
    public class EventHub
    {
        public event Action OnRedisConfigurationChange;
        public event Action OnRedisConnectionChange;

        public void Publish(EventType eventType)
        {
            var action = eventType switch
            {
                EventType.RedisConfigurationChanged => OnRedisConfigurationChange,
                _ => throw new NotSupportedException(eventType.ToString())
            };

            action?.Invoke();
        }
    }
}
