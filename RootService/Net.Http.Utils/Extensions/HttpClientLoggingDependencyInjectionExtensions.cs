using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Net.Http.Utils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpClientLoggingDependencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientLogging(this IServiceCollection services)
        {

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, HttpClientMessageHandlerBuilderFilter>());
            return services;
        }
    }
}
