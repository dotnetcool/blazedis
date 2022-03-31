using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Shared
{
    public class NavMenuBase : ComponentBase
    {
        protected List<BlazedisRedisConfiguration> configurations;

        [Inject]
        protected EventHub EventHub { get; set; }

        [Inject]
        protected IRedisConfigurationService RedisConfigurationService { get; set; }

        protected List<BlazedisRedisConfiguration> Configurations => RedisConfigurationService.GetAll();

        protected override void OnInitialized()
        {
            EventHub.OnRedisConfigurationChange += StateHasChanged;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }
    }
}
