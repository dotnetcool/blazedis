using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Core
{
    public class BlazedisRedisConfiguration
    {
        public Guid Id { get; set; }
        public ConfigurationOptions Options { get; set; }
    }
}
