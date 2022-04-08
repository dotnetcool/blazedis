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
        protected bool isLoading = false;
        protected BlazedisExceptionBase exception = null;
        protected IGrouping<string, KeyValuePair<string, string>>[] serverInfo;
        protected List<(RedisKey Key, RedisType Type, TimeSpan? Ttl)> keyList = new();


        [Parameter]
        public Guid Id { get; set; }

        [Inject]
        protected IRedisConnectionService RedisConnectionService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        protected bool HasError => exception != null;

        protected void OnRowClicked()
        {
            Console.WriteLine("click!");
        }

        protected override async Task OnParametersSetAsync()
        {
            isLoading = true;
            exception = null;

            MessagingCenter.Send(new UriChangedMessage
            {
                Data = new ServersDetailUriChangedMessageData
                {
                    Id = Id
                }
            }, EventType.UriChanged);

            ConnectionMultiplexer connection;
            IServer server;

            try
            {
                connection = await RedisConnectionService.GetByIdAsync(Id);
                
                var endPoint = connection.GetEndPoints(true).First();

                server = connection.GetServer(endPoint);
                serverInfo = await server?.InfoAsync();
            }
            catch (RedisConnectionException ex)
            {
                exception = new BlazedisFailedConnectedServerException("Something wrong when Connected to redis.", ex);
                isLoading = false;
                return;
            }

            var dbIndex = 1;
            var db = connection.GetDatabase(dbIndex);
            var offset = 0;
            var keys = server.Keys(dbIndex, "*", 100, CommandFlags.None).Skip(offset * 100).Take(100).ToList();
            keyList = new();

            foreach (var item in keys)
            {
                var type = db.KeyType(item);
                var ttl = db.KeyTimeToLive(item);
                keyList.Add((item, type, ttl));
            }

            isLoading = false;

        }
    }
}
