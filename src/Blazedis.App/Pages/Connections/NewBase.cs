﻿using Microsoft.AspNetCore.Components;
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
        protected bool isOnSaving = false;

        protected string formHost;
        protected int? formPort;
        protected string formName;

        protected string Host => formHost ??= "127.0.0.1";
        protected int Port => formPort ??= 6379;
        protected string Name => String.IsNullOrWhiteSpace(formName) ? $"{Host}:{Port}" : formName;

        [Inject]
        public IRedisConfigurationService RedisConfigurationService { get; set; }

        [Inject]
        public IRedisConnectionService RedisConnectionService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        protected async Task SaveAsync()
        {
            isOnSaving = true;

            var configurationOption = new ConfigurationOptions
            {
                EndPoints =
                {
                    { Host, Port }
                },
                AllowAdmin = true
            };

            try
            {
                await RedisConnectionService.TestConnectionAsync(configurationOption);
            }
            catch (Exception ex)
            {
                Snackbar.Add(
                    $"Something wrong when connected to the host: {ex}", 
                    Severity.Error,
                    config =>
                    {
                        config.CloseAfterNavigation = true;
                        config.ShowCloseIcon= true;
                        config.RequireInteraction = true;
                        config.HideTransitionDuration = 300;
                    });
                isOnSaving = false;

                return;
            }

            RedisConfigurationService.Add(new BlazedisRedisConfigurationItem
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Options = configurationOption
            });

            isOnSaving = false;
        }

        protected override void OnParametersSet()
        {
            MessagingCenter.Send(new UriChangedMessage(), EventType.UriChanged);
        }
    }
}
