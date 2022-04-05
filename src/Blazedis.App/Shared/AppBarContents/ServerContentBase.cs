using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Shared.AppBarContents
{
    public class ServerContentBase : ComponentBase
    {
        protected string serverName;

        [Parameter]
        public ServersDetailUriChangedMessageData Data { get; set; }

        [Inject]
        protected IRedisConfigurationService RedisConfigurationService { get; set; }

        [Inject]
        protected IRedisConnectionService RedisConnectionService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected void DeleteConfiguration()
        {
            RedisConfigurationService.Delete(Data.Id);
            NavigationManager.NavigateTo("/", replace: true);
        }

        protected override void OnParametersSet()
        {
            var configuration = RedisConfigurationService.GetById(Data.Id);
            serverName = configuration.Name;
        }
    }
}
