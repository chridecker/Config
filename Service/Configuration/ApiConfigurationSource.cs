using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Configuration
{
    public class ApiConfigurationSource : JsonStreamConfigurationSource
    {
        public int? ReloadSeconds { get; set; }
        public IServiceProvider Services { get; set; }
        public CancellationToken Token { get; set; }

        public Func<IHttpClientFactory , CancellationToken, Task<string>> ReloadStream { get; set; }
        public override IConfigurationProvider Build(IConfigurationBuilder builder) => new ApiConfigurationProvider(this, this.ReloadSeconds)
        {
            ReloadStream = this.ReloadStream,
            Services = this.Services,
            Token = this.Token,
        };
    }
}
