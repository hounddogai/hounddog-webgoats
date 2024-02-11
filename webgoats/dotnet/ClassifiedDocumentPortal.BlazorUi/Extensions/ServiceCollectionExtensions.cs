using ClassifiedDocumentPortal.BlazorUi.Areas.Identity;
using ClassifiedDocumentPortal.Domain.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;

namespace ClassifiedDocumentPortal.BlazorUi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorUi(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<PortalUser>>();

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomEnd;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = false;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            return services;
        }
    }
}
