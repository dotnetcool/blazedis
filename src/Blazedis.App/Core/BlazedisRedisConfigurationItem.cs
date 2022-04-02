using System.Text.Json.Serialization;

namespace Blazedis.App.Core
{
    public class BlazedisRedisConfigurationItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ConfigurationOptions Options { get; set; }

        public string OptionString
        {
            get => Options.ToString();
            set => Options = ConfigurationOptions.Parse(value);
        }
    }
}
