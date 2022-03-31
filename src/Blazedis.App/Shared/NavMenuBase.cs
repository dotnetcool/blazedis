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
        protected List<BlazedisRedisConfigurationItem> configurationItems;

        [Inject]
        protected IRedisConfigurationService RedisConfigurationService { get; set; }

        protected override void OnInitialized()
        {
            configurationItems = RedisConfigurationService.GetAll();

            MessagingCenter.Subscribe(
                this,
                EventType.RedisConfigurationChanged,
                (BlazedisRedisConfiguration configuration) =>
                {
                    configurationItems = configuration.Items;
                    StateHasChanged();
                });
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
