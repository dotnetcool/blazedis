using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Pages.Servers
{
    public class DetailBase : ComponentBase
    {
        protected IGrouping<string, KeyValuePair<string, string>>[] serverInfo;

        [Parameter]
        public Guid Id { get; set; }

        [Inject]
        protected IRedisConnectionService RedisConnectionService { get; set; }

        protected ConnectionMultiplexer Connection => RedisConnectionService.GetById(Id);

        protected override async Task OnInitializedAsync()
        {
        }

        protected override async Task OnParametersSetAsync()
        {
            MessagingCenter.Send(new UriChangedMessage
            {
                Data = new ServersDetailUriChangedMessageData
                {
                    Id = Id
                }
            }, EventType.UriChanged);

            serverInfo = await RedisConnectionService.GetServerById(Id)?.InfoAsync();
        }
    }
}
