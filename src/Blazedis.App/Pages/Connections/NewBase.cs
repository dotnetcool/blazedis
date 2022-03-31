using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Pages.Connections
{
    public class NewBase : ComponentBase
    {
        protected bool success;
        protected string[] errors = { };
        protected MudForm form;

        protected string host;
        protected int? port;

        [Inject]
        public IRedisConfigurationService RedisConfigurationService { get; set; }

        protected void Save()
        {
            host ??= "127.0.0.1";
            port ??= 6379;

            RedisConfigurationService.Add(new BlazedisRedisConfiguration
            {
                Id = Guid.NewGuid(),
                Options = new ConfigurationOptions
                {
                    EndPoints =
                    {
                        {host  , (int)port }
                    },
                    AllowAdmin = true
                }
            });
        }
    }
}
