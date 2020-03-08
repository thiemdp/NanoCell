
using Microsoft.Extensions.DependencyInjection;
using NanoCell.UI.Components.Toast;
using System.Reflection;

namespace NanoCell.UI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUI(this IServiceCollection services)
        {
            services.AddScoped<IToastService, ToastService>();
            return services;
        }
    }
}
